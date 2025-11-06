using Stabilization.Infrastructure.Serial;
using Stabilization.Models;
using Stabilization.Services.DataProcessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging; // Add this namespace for ILogger

namespace Stabilization.Controllers
{
    public class BalancingPlatformController : IDisposable
    {
        private readonly ISerialCommunicator _communicator;
        private readonly IDataStreamProcessor _dataProcessor;
        private readonly ILogger _logger;
        
        public event EventHandler<SerialDataPoint> DataPointReceived;
        public event EventHandler<PIDParameters> ParametersUpdated;
        public event EventHandler<SystemStatus> StatusChanged;
        public event EventHandler<string> MessageReceived;

        public event EventHandler<string> RawDataReceived;


        public SystemStatus CurrentStatus { get; private set; }

        public BalancingPlatformController(
            ISerialCommunicator communicator,
            IDataStreamProcessor dataProcessor,
            ILogger logger = null)
        {
            _communicator = communicator ?? throw new ArgumentNullException(nameof(communicator));
            _dataProcessor = dataProcessor ?? throw new ArgumentNullException(nameof(dataProcessor));
            _logger = logger;

            CurrentStatus = new SystemStatus();

            // Wire up events
            _communicator.DataReceived += OnCommunicatorDataReceived;
            _communicator.ConnectionChanged += OnConnectionChanged;
            _communicator.ErrorOccurred += OnCommunicatorError;

            _dataProcessor.DataPointProcessed += OnDataPointProcessed;
            _dataProcessor.StatusParametersReceived += OnStatusParametersReceived;
            _dataProcessor.InformationalMessageReceived += OnInformationalMessage;
        }



        public async Task<bool> ConnectAsync(string portName, int baudRate)
        {
            var config = new SerialConnectionConfig
            {
                PortName = portName,
                BaudRate = baudRate
            };

            bool connected = await _communicator.ConnectAsync(config);

            if (connected)
            {
                _dataProcessor.Start();
            }

            return connected;
        }
        public  void Disconnect()
        {
            _dataProcessor.Stop();
              _communicator.Disconnect();
        }


        public async Task DisconnectAsync()
        {
            _dataProcessor.Stop();
            await _communicator.DisconnectAsync();
        }




        public async Task SetSetpointAsync(int setpoint)
        {
            await _communicator.SendCommandAsync($"target:{setpoint}");
        }

        public async Task UpdatePIDParametersAsync(PIDParameters parameters)
        {
            await _communicator.SendCommandAsync($"Kp:{parameters.Kp}");
            await _communicator.SendCommandAsync($"Ki:{parameters.Ki}");
            await _communicator.SendCommandAsync($"Kd:{parameters.Kd}");
            await _communicator.SendCommandAsync($"limit:{parameters.Limit}");
            await _communicator.SendCommandAsync($"baseSpeed:{parameters.BaseSpeed}");
        }

        public async Task SaveParametersAsync()
        {
            await _communicator.SendCommandAsync("save");
        }

        public async Task RequestStatusAsync()
        {
            await _communicator.SendCommandAsync("status");
        }

        public async Task SetMonitorModeAsync(bool enabled)
        {
            await _communicator.SendCommandAsync(enabled ? "monitor:on" : "monitor:off");
            CurrentStatus.IsMonitoring = enabled;
            StatusChanged?.Invoke(this, CurrentStatus);
        }
        // In BalancingPlatformController.cs - Add this method
        public async Task SendCommandAsync(string command)
        {
            await _communicator.SendCommandAsync(command);
        }
        public async Task ResetAsync()
        {
            await _communicator.SendCommandAsync("reset");
        }

        public async Task StopAsync()
        {
            await _communicator.SendCommandAsync("stop");
            _communicator.SendCommand("stop"); // Send synchronously as backup
        }

        private void OnCommunicatorDataReceived(object sender, DataReceivedEventArgs e)
        {
            // Trigger raw data event
            RawDataReceived?.Invoke(this, e.RawData);

            _dataProcessor.ProcessRawData(e.RawData);
        }

        private void OnConnectionChanged(object sender, ConnectionEventArgs e)
        {
            CurrentStatus.IsConnected = e.IsConnected;
            StatusChanged?.Invoke(this, CurrentStatus);
            MessageReceived?.Invoke(this, e.Message);
        }

        private void OnCommunicatorError(object sender, ErrorEventArgs e)
        {
            _logger?.LogError(e.GetException(), e.GetException().Message);
            MessageReceived?.Invoke(this, $"Error: {e.GetException().Message}");
        }

        private void OnDataPointProcessed(object sender, SerialDataPoint e)
        {
            DataPointReceived?.Invoke(this, e);
        }

        private void OnStatusParametersReceived(object sender, StatusParametersEventArgs e)
        {
            ParametersUpdated?.Invoke(this, e.Parameters);
        }

        private void OnInformationalMessage(object sender, string message)
        {
            MessageReceived?.Invoke(this, message);
        }

        public void Dispose()
        {
            _dataProcessor?.Dispose();
            _communicator?.Dispose();
        }
    }
}
