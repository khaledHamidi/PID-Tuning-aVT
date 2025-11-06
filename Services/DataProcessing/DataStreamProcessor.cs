using Stabilization.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// Services/DataProcessing/DataStreamProcessor.cs
namespace Stabilization.Services.DataProcessing
{
    public class DataStreamProcessor : IDataStreamProcessor
    {
        private readonly IDataParser _parser;
        private readonly ConcurrentQueue<SerialDataPoint> _dataQueue;
        private readonly object _bufferLock = new object();

        private string _serialBuffer = "";
        private CancellationTokenSource _processingCts;
        private Task _processingTask;

        private bool _readingStatus = false;
        private Dictionary<string, double> _currentParams = new Dictionary<string, double>();

        private int _dataPointsReceived = 0;
        private DateTime _lastPerformanceUpdate = DateTime.Now;

        public event EventHandler<SerialDataPoint> DataPointProcessed;
        public event EventHandler<StatusParametersEventArgs> StatusParametersReceived;
        public event EventHandler<string> InformationalMessageReceived;
        public event EventHandler<PerformanceMetrics> PerformanceUpdated;

        public DataStreamProcessor(IDataParser parser)
        {
            _parser = parser ?? throw new ArgumentNullException(nameof(parser));
            _dataQueue = new ConcurrentQueue<SerialDataPoint>();
        }

        public void Start()
        {
            _processingCts = new CancellationTokenSource();
            _processingTask = ProcessDataQueueAsync(_processingCts.Token);
        }

        public void Stop()
        {
            _processingCts?.Cancel();
            if (!_processingTask.Wait(2000)) // Wait up to 5 seconds
            {
                // show box
                Console.WriteLine("Warning: Data processing task did not terminate in time.");


            }

        }

        public void ProcessRawData(string rawData)
        {
            if (string.IsNullOrEmpty(rawData)) return;

            lock (_bufferLock)
            {
                _serialBuffer += rawData;
                string[] lines = _serialBuffer.Split('\n');
                _serialBuffer = lines[lines.Length - 1];

                for (int i = 0; i < lines.Length - 1; i++)
                {
                    ProcessLine(lines[i].Trim('\r', '\n', ' ', '\t'));
                }
            }
        }

        private void ProcessLine(string line)
        {
            if (string.IsNullOrEmpty(line)) return;

            // Handle status parameter reading
            if (line == "STATUS_START")
            {
                _readingStatus = true;
                _currentParams.Clear();
                return;
            }
            else if (line == "STATUS_END")
            {
                _readingStatus = false;
                ProcessStatusParameters();
                return;
            }
            else if (_readingStatus && line.Contains(":"))
            {
                if (_parser.TryParseStatusParameter(line, out var parameter))
                {
                    _currentParams[parameter.Key] = parameter.Value;
                }
                return;
            }

            // Handle informational messages
            if (line.StartsWith(">>"))
            {
                InformationalMessageReceived?.Invoke(this, line);
                return;
            }

            // Parse data points
            if (_parser.TryParse(line, out SerialDataPoint dataPoint))
            {
                _dataQueue.Enqueue(dataPoint);
                Interlocked.Increment(ref _dataPointsReceived);
            }
        }

        private async Task ProcessDataQueueAsync(CancellationToken cancellationToken)
        {
            var batchPoints = new List<SerialDataPoint>();

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    batchPoints.Clear();

                    while (_dataQueue.TryDequeue(out SerialDataPoint point) && batchPoints.Count < 20)
                    {
                        batchPoints.Add(point);
                    }

                    foreach (var point in batchPoints)
                    {
                        DataPointProcessed?.Invoke(this, point);
                    }

                    // Update performance metrics
                    UpdatePerformanceMetrics();

                    await Task.Delay(1, cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    // Log error
                }
            }
        }

        private void ProcessStatusParameters()
        {
            if (_currentParams.Count >= 4)
            {
                var pidParams = new PIDParameters
                {
                    Kp = _currentParams.GetValueOrDefault("Kp", 0),
                    Ki = _currentParams.GetValueOrDefault("Ki", 0),
                    Kd = _currentParams.GetValueOrDefault("Kd", 0),
                    Limit = (int)_currentParams.GetValueOrDefault("Limit", 1),
                    BaseSpeed = (int)_currentParams.GetValueOrDefault("BaseSpeed", 1000),
                    Setpoint = (int)_currentParams.GetValueOrDefault("Target", 0)
                };

                StatusParametersReceived?.Invoke(this,
                    new StatusParametersEventArgs { Parameters = pidParams });
            }
        }

        private void UpdatePerformanceMetrics()
        {
            var now = DateTime.Now;
            var timeDiff = (now - _lastPerformanceUpdate).TotalSeconds;

            if (timeDiff >= 2.0)
            {
                double dataRate = _dataPointsReceived / timeDiff;

                var metrics = new PerformanceMetrics
                {
                    DataRate = dataRate,
                    QueueSize = _dataQueue.Count,
                    BufferSize = _serialBuffer?.Length ?? 0,
                    TotalPointsProcessed = _dataPointsReceived
                };

                PerformanceUpdated?.Invoke(this, metrics);

                _lastPerformanceUpdate = now;
                _dataPointsReceived = 0;
            }
        }

        public void Dispose()
        {
            Stop();
            _processingCts?.Dispose();
        }
    }
}