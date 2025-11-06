using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Ports;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stabilization.Infrastructure.Serial
{
    public class ArduinoSerialCommunicator : ISerialCommunicator
    {
        private readonly SerialPort _serialPort;
        private readonly ConcurrentQueue<string> _commandQueue;
        private CancellationTokenSource _commandProcessingCts;
        private Task _commandProcessingTask;

        public event EventHandler<DataReceivedEventArgs> DataReceived;
        public event EventHandler<ErrorEventArgs> ErrorOccurred;
        public event EventHandler<ConnectionEventArgs> ConnectionChanged;

        public bool IsConnected => _serialPort?.IsOpen ?? false;
        public SerialConnectionConfig Config { get; private set; }

        public ArduinoSerialCommunicator()
        {
            _serialPort = new SerialPort();
            _commandQueue = new ConcurrentQueue<string>();
        }

        public async Task<bool> ConnectAsync(SerialConnectionConfig config, CancellationToken cancellationToken = default)
        {
            try
            {
                Config = config;

                _serialPort.PortName = config.PortName;
                _serialPort.BaudRate = config.BaudRate;
                _serialPort.ReadBufferSize = config.ReadBufferSize;
                _serialPort.WriteBufferSize = config.WriteBufferSize;
                _serialPort.ReadTimeout = config.ReadTimeout;
                _serialPort.WriteTimeout = config.WriteTimeout;
                _serialPort.NewLine = "\n";

                _serialPort.DataReceived += OnSerialDataReceived;
                _serialPort.ErrorReceived += OnSerialErrorReceived;

                await Task.Run(() => _serialPort.Open(), cancellationToken);

                if (_serialPort.IsOpen)
                {
                    _commandProcessingCts = new CancellationTokenSource();
                    _commandProcessingTask = ProcessCommandQueueAsync(_commandProcessingCts.Token);

                    ConnectionChanged?.Invoke(this, new ConnectionEventArgs
                    {
                        IsConnected = true,
                        Message = $"Connected to {config.PortName}"
                    });

                    return true;
                }

                return false;
            }
            catch (System.IO.IOException ex)
            {
                MessageBox.Show(ex.Message);
                return false; // Ensure a return value in this catch block
            }
            catch (Exception ex)
            {
                ErrorOccurred?.Invoke(this, new ErrorEventArgs(ex));
                return false; // Ensure a return value in this catch block
            }
        }

        public void Disconnect()
        {
            try
            {
                _commandProcessingCts?.Cancel();

                /*if (_commandProcessingTask != null)
                    await _commandProcessingTask;
                */
                if (_serialPort.IsOpen)
                {
                    _serialPort.DataReceived -= OnSerialDataReceived;
                    _serialPort.ErrorReceived -= OnSerialErrorReceived;

                    _serialPort.DiscardInBuffer();
                    _serialPort.DiscardOutBuffer();
                    _serialPort.Close();
                }

                ConnectionChanged?.Invoke(this, new ConnectionEventArgs
                {
                    IsConnected = false,
                    Message = "Disconnected"
                });
            }
            catch (Exception ex)
            {
                ErrorOccurred?.Invoke(this, new ErrorEventArgs(ex));
            }

        }
        public async Task DisconnectAsync()
        {
            try
            {
                _commandProcessingCts?.Cancel();

                /*if (_commandProcessingTask != null)
                    await _commandProcessingTask;
                */
                if (_serialPort.IsOpen)
                {
                    _serialPort.DataReceived -= OnSerialDataReceived;
                    _serialPort.ErrorReceived -= OnSerialErrorReceived;

                    _serialPort.DiscardInBuffer();
                    _serialPort.DiscardOutBuffer();
                    _serialPort.Close();
                }

                ConnectionChanged?.Invoke(this, new ConnectionEventArgs
                {
                    IsConnected = false,
                    Message = "Disconnected"
                });
            }
            catch (Exception ex)
            {
                ErrorOccurred?.Invoke(this, new ErrorEventArgs(ex));
            }
        }

        public async Task SendCommandAsync(string command, CancellationToken cancellationToken = default)
        {
            if (!string.IsNullOrWhiteSpace(command))
            {
                _commandQueue.Enqueue(command);
            }
            await Task.CompletedTask;
        }

        public void SendCommand(string command)
        {
            if (!string.IsNullOrWhiteSpace(command) && _serialPort.IsOpen)
            {
                _serialPort.WriteLine(command);
            }
        }

        private async Task ProcessCommandQueueAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    if (_commandQueue.TryDequeue(out string command) && _serialPort.IsOpen)
                    {
                        _serialPort.WriteLine(command);
                    }

                    await Task.Delay(1, cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    ErrorOccurred?.Invoke(this, new ErrorEventArgs(ex));
                }
            }
        }

        private void OnSerialDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (_serialPort.IsOpen)
                {
                    string data = _serialPort.ReadExisting();
                    DataReceived?.Invoke(this, new DataReceivedEventArgs
                    {
                        RawData = data,
                        Timestamp = DateTime.Now
                    });
                }
            }
            catch (Exception ex)
            {
                ErrorOccurred?.Invoke(this, new ErrorEventArgs(ex));
            }
        }

        private void OnSerialErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            ErrorOccurred?.Invoke(this, new ErrorEventArgs(
                new Exception($"Serial error: {e.EventType}")));
        }

        public void Dispose()
        {
            DisconnectAsync();
            _commandProcessingCts?.Dispose();
            _serialPort?.Dispose();
        }
    }
}