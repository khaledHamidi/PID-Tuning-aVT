using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Stabilization.Infrastructure.Serial
{
    public interface ISerialCommunicator : IDisposable
    {
        event EventHandler<DataReceivedEventArgs> DataReceived;
        event EventHandler<ErrorEventArgs> ErrorOccurred;
        event EventHandler<ConnectionEventArgs> ConnectionChanged;

        bool IsConnected { get; }
        SerialConnectionConfig Config { get; }

        Task<bool> ConnectAsync(SerialConnectionConfig config, CancellationToken cancellationToken = default);
        Task DisconnectAsync();
        Task SendCommandAsync(string command, CancellationToken cancellationToken = default);
        void SendCommand(string command);
        void Disconnect();
    }

    public class SerialConnectionConfig
    {
        public string PortName { get; set; }
        public int BaudRate { get; set; }
        public int ReadBufferSize { get; set; } = 8192;
        public int WriteBufferSize { get; set; } = 2048;
        public int ReadTimeout { get; set; } = 100;
        public int WriteTimeout { get; set; } = 100;
    }

    public class DataReceivedEventArgs : EventArgs
    {
        public string RawData { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class ConnectionEventArgs : EventArgs
    {
        public bool IsConnected { get; set; }
        public string Message { get; set; }
    }
}
 
