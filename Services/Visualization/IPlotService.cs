using Stabilization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Stabilization.Services.Visualization
{
    public interface IPlotService
    {
        void AddDataPoint(SerialDataPoint dataPoint, double setpoint, double scaledOutput);
        void Clear();
        void UpdateConfiguration(PlotConfiguration config);
    }

    public class PlotConfiguration
    {
        public int MaxPoints { get; set; } = 20;  // #TODO: changable
        public int UpdateIntervalMs { get; set; } = 2;
    }
}

