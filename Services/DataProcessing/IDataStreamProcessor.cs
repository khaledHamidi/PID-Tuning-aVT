using Stabilization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stabilization.Services.DataProcessing
{
    public interface IDataStreamProcessor : IDisposable
    {
        event EventHandler<SerialDataPoint> DataPointProcessed;
        event EventHandler<StatusParametersEventArgs> StatusParametersReceived;
        event EventHandler<string> InformationalMessageReceived;
        event EventHandler<PerformanceMetrics> PerformanceUpdated;

        void Start();
        void Stop();
        void ProcessRawData(string rawData);
    }

    public class StatusParametersEventArgs : EventArgs
    {
        public PIDParameters Parameters { get; set; }
    }

    public class PerformanceMetrics
    {
        public double DataRate { get; set; }
        public int QueueSize { get; set; }
        public int BufferSize { get; set; }
        public int TotalPointsProcessed { get; set; }
    }
}
