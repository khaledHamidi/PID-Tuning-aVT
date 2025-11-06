using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Models/SerialDataPoint.cs
namespace Stabilization.Models
{
    public struct SerialDataPoint
    {
        public long Millis { get; set; }
        public double Angle { get; set; }
        public int Output { get; set; }
        public DateTime Timestamp { get; set; }
        public long SequenceNumber { get; set; }

        public SerialDataPoint(long millis, double angle, int output, long sequenceNumber = 0)
        {
            Millis = millis;
            Angle = angle;
            Output = output;
            Timestamp = DateTime.Now;
            SequenceNumber = sequenceNumber;
        }
    }

    public struct PIDParameters
    {
        public double Kp { get; set; }
        public double Ki { get; set; }
        public double Kd { get; set; }
        public int Limit { get; set; }
        public int BaseSpeed { get; set; }
        public int Setpoint { get; set; }
    }

    public class SystemStatus
    {
        public bool IsConnected { get; set; }
        public bool IsMonitoring { get; set; }
        public bool IsLogging { get; set; }
        public int DataRate { get; set; }
        public int QueueSize { get; set; }
    }
}
