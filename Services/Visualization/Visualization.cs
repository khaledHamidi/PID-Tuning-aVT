using Stabilization.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stabilization.Services.Visualization
{
    public class ChartPlotService : IPlotService
    {
        private readonly Plot _plotForm;
        private readonly Queue<SerialDataPoint> _dataBuffer;
        private PlotConfiguration _config;

        public ChartPlotService(Plot plotForm)
        {
            _plotForm = plotForm ?? throw new ArgumentNullException(nameof(plotForm));
            _dataBuffer = new Queue<SerialDataPoint>();
            _config = new PlotConfiguration();
        }

        public void AddDataPoint(SerialDataPoint dataPoint, double setpoint, double scaledOutput)
        {

            if (_plotForm?.chart1?.Series == null) return;

            try
            {
                _plotForm.Invoke((MethodInvoker)delegate
                { 

            var seriesAngle = _plotForm.chart1.Series["Series1"];
                var seriesOutput = _plotForm.chart1.Series["out"];
                var seriesTarget = _plotForm.chart1.Series["target"];

                _plotForm.chart1.SuspendLayout();

                // Maintain sliding window
                if (seriesAngle.Points.Count >= _config.MaxPoints)
                {
                    seriesAngle.Points.RemoveAt(0);
                    if (seriesOutput.Points.Count > 0) seriesOutput.Points.RemoveAt(0);
                    if (seriesTarget.Points.Count > 0) seriesTarget.Points.RemoveAt(0);
                }

                seriesAngle.Points.AddY(dataPoint.Angle);
                seriesOutput.Points.AddY(scaledOutput);
                seriesTarget.Points.AddY(setpoint);

                // Update labels
                _plotForm.label1.Text = dataPoint.Angle.ToString("F2", CultureInfo.InvariantCulture);
                _plotForm.label2.Text = dataPoint.Output.ToString();
                _plotForm.label3.Text = setpoint.ToString();
                _plotForm.label7.Text = dataPoint.Millis.ToString();

                _plotForm.chart1.ResumeLayout();
                _plotForm.chart1.Invalidate();
                });

            }
            catch (Exception)
            {
                _plotForm.chart1.ResumeLayout();
            }
        }

        public void Clear()
        {
            if (_plotForm?.chart1?.Series == null) return;

            foreach (var series in _plotForm.chart1.Series)
            {
                series.Points.Clear();
            }
        }

        public void UpdateConfiguration(PlotConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }
    }
}
