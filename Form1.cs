// Form1.cs - Complete Refactored Version
using Stabilization.Controllers;
using Stabilization.Infrastructure.Serial;
using Stabilization.Models;
using Stabilization.Services.DataProcessing;
using Stabilization.Services.Visualization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stabilization
{
    public partial class Form1 : Form
    {
        private readonly BalancingPlatformController _controller;
        private readonly IPlotService _plotService;
        private AsyncLogger _logger;
        private  System.Timers.Timer _plotTimer;

        private double _setpoint = 0;
        private int _baseSpeed = 1500;
        private int _limit = 400;
        private bool _internalUpdate = false;
        private readonly Queue<SerialDataPoint> _plotDataQueue = new Queue<SerialDataPoint>();
        private readonly object _plotQueueLock = new object();
        private Plot plot = new Plot();


        // Performance monitoring variables
        private int _samplesPerSecond = 0;
        private int _samplesThisSecond = 0;
        private DateTime _lastSecondUpdate = DateTime.Now;
        private readonly List<double> _plotIntervals = new List<double>();
        private double _averagePlotInterval = 0;
        private double _maxJitter = 0;
        private DateTime _lastPlotTime = DateTime.Now;
        private long _totalSamplesPlotted = 0;
        private DateTime _lastPerformanceLog = DateTime.Now;

        private int _serialDataHz = 0;
        private int _serialBytesThisSecond = 0;
        private DateTime _lastSerialRateUpdate = DateTime.Now;
        private int _totalSerialBytes = 0;

        private int _serialMessagesHz = 0;
        private int _serialMessagesThisSecond = 0;

        private int _plotUpdatesCount = 0;
        private DateTime _lastPlotUpdateTime = DateTime.Now;
        private int _serialMessagesReceived = 0;

        public Form1()
        {
            InitializeComponent();

            // Initialize services
            var communicator = new ArduinoSerialCommunicator();
            var parser = new ArduinoDataParser();
            var dataProcessor = new DataStreamProcessor(parser);

            _controller = new BalancingPlatformController(communicator, dataProcessor);
            _plotService = new ChartPlotService(plot); // Use existing plot form

            InitializeUI();
            WireUpEvents();

            // Show plot form
            plot.Show();

            // Auto-connect after delay
            Task.Run(async () =>
            {
                await Task.Delay(500);
                BeginInvoke(new Action(() => btconnect_Click(null, null)));
            });
        }

        private void InitializeUI()
        {
            // Position form
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(
                Screen.PrimaryScreen.Bounds.Width - this.Width,
                Screen.PrimaryScreen.Bounds.Height - this.Height - 30);

            // Load settings
            LoadSettings();

            // Initialize plot timer
            _plotTimer = new System.Timers.Timer(20);
            _plotTimer.Elapsed += PlotTimer_Tick;
        }

        private void LoadSettings()
        {
            mtbPortname.Text = Properties.Settings.Default.PortName;
            cmBuadrite.Text = Properties.Settings.Default.BaudRate.ToString();

            nudKp.Value = Properties.Settings.Default.Kp;
            nudKi.Value = Properties.Settings.Default.Ki;
            nudKd.Value = Properties.Settings.Default.Kd;
            nudKpd.Value = Properties.Settings.Default.Kpd;
            nudKid.Value = Properties.Settings.Default.Kid;
            nudKdd.Value = Properties.Settings.Default.Kdd;
            nudBaseSpeed.Value = Properties.Settings.Default.PWMbase;
            nudLimit.Value = Properties.Settings.Default.PIDOut;
            loger_name.Text = Properties.Settings.Default.loggerName;

            _baseSpeed = (int)nudBaseSpeed.Value;
            _limit = (int)nudLimit.Value;
        }

        private void WireUpEvents()
        {
            _controller.DataPointReceived += OnDataPointReceived;
            _controller.ParametersUpdated += OnParametersUpdated;
            _controller.StatusChanged += OnStatusChanged;
            _controller.MessageReceived += OnMessageReceived;

            // Add serial data rate measurement
            _controller.RawDataReceived += OnRawDataReceived;
        }
        private void OnRawDataReceived(object sender, string rawData)
        {
            // Count complete messages (lines)
            int messageCount = rawData.Count(c => c == '\n');
            _serialMessagesThisSecond += messageCount;

            UpdateSerialDataRate();
        }

        private void UpdateSerialDataRate()
        {
            var now = DateTime.Now;
            var timeSinceLastUpdate = (now - _lastSerialRateUpdate).TotalSeconds;

            if (timeSinceLastUpdate >= 1.0)
            {
                _serialMessagesHz = (int)(_serialMessagesThisSecond / timeSinceLastUpdate);
                _serialMessagesThisSecond = 0;
                _lastSerialRateUpdate = now;

                //   AppendToPerformancLog($"[SERIAL] Messages Rate: {_serialMessagesHz} Hz");
            }
        }

        private async void btconnect_Click(object sender, EventArgs e)
        {
            try
            {
                bool connected = await _controller.ConnectAsync(
                    mtbPortname.Text,
                    Convert.ToInt32(cmBuadrite.Text));

                if (connected)
                {
                    SaveConnectionSettings();
                    UpdateConnectionUI(true);
                    _plotTimer.Start();

                    AppendToErrorLog($"Connected to Arduino on port {mtbPortname.Text} at {cmBuadrite.Text} baud");

                    if (cb_autoReadParams.Checked)
                    {
                        await _controller.RequestStatusAsync();
                        Show(pb_working);
                    }
                    else
                    {
                        Show(pb_ok);
                    }
                }
                else
                {
                    MessageBox.Show("Failed to connect to Arduino", "Connection Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Show(pb_notconnect);
                }
            }
            catch (Exception ex)
            {
                Show(pb_notconnect);
                MessageBox.Show($"Connection Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btdisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                _plotTimer.Stop();
                // await _controller.DisconnectAsync();
                var disconnectTask = _controller?.DisconnectAsync();
                UpdateConnectionUI(false);
                Show(pb_notconnect);
            }
            catch (Exception ex)
            {
                Show(pb_wrong);
                MessageBox.Show($"Disconnection Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateConnectionUI(bool connected)
        {
            btconnect.Enabled = !connected;
            btdisconnect.Enabled = connected;
            pictureBox7.Visible = !connected;
        }

        private void SaveConnectionSettings()
        {
            Properties.Settings.Default.PortName = mtbPortname.Text;
            Properties.Settings.Default.BaudRate = Convert.ToInt32(cmBuadrite.Text);
            Properties.Settings.Default.Save();
        }

        private async void trackBar1_Scroll(object sender, EventArgs e)
        {
            _setpoint = trackBar1.Value;
            await _controller.SetSetpointAsync(trackBar1.Value);

            string sign = trackBar1.Value >= 0 ? "+" : "";
            label7.Text = $"{sign}{trackBar1.Value}°";
        }

        private async void PIDTune(object sender, EventArgs e)
        {
            if (_internalUpdate) return;

            if (sender is NumericUpDown num && num.Tag is string tag)
            {
                int intValue = (int)num.Value;
                double decimalValue = (double)(num.Value - intValue);
                decimalValue = Math.Round(decimalValue, 4) * 10000;
                string decimalStr = decimalValue.ToString("0000", CultureInfo.InvariantCulture);

                string command;
                if (tag == "Kpd" || tag == "Kid" || tag == "Kdd")
                {
                    command = $"{tag}:{decimalStr}";
                }
                else
                {
                    command = $"{tag}:{intValue}";
                }

                await _controller.SendCommandAsync(command);
                BlinkButton(button2, false);
                Show(pb_working);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbSend.Text))
            {
                await _controller.SendCommandAsync(tbSend.Text);
                tbSend.Clear();
                Show(pb_working);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await _controller.SaveParametersAsync();
            BlinkButton(button2, false);
            Show(pb_working);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.Kp = (int)nudKp.Value;
                Properties.Settings.Default.Ki = (int)nudKi.Value;
                Properties.Settings.Default.Kd = (int)nudKd.Value;
                Properties.Settings.Default.Kpd = (int)nudKpd.Value;
                Properties.Settings.Default.Kid = (int)nudKid.Value;
                Properties.Settings.Default.Kdd = (int)nudKdd.Value;
                Properties.Settings.Default.PWMbase = (int)nudBaseSpeed.Value;
                Properties.Settings.Default.PIDOut = (int)nudLimit.Value;

                Properties.Settings.Default.Save();

                AppendToErrorLog("PID values saved successfully.");
                BlinkButton(button2, false);
                Show(pb_ok);
            }
            catch (Exception ex)
            {
                AppendToErrorLog($"Error saving PID values: {ex.Message}");
                BlinkButton(button2, true);
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            await _controller.StopAsync();
            Show(pb_wrong);
        }

        private async void ReadParams_fun(object sender, EventArgs e)
        {
            await _controller.RequestStatusAsync();
            Show(pb_working);
        }

        private async void monitorMode_CheckedChanged(object sender, EventArgs e)
        {
            await _controller.SetMonitorModeAsync(monitorMode.Checked);
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            await _controller.SetMonitorModeAsync(false);
            await Task.Delay(100);
            await _controller.RequestStatusAsync();
            await Task.Delay(100);
            await _controller.SetMonitorModeAsync(true);
            Show(pb_working);
        }

        private async void button7_Click(object sender, EventArgs e)
        {
            await _controller.ResetAsync();
            Show(pb_working);
        }

        private async void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (_internalUpdate) return;
            _limit = Convert.ToInt16(nudLimit.Value);
            await _controller.SendCommandAsync($"limit:{_limit}");
        }

        private async void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (_internalUpdate) return;
            _baseSpeed = Convert.ToInt16(nudBaseSpeed.Value);
            await _controller.SendCommandAsync($"baseSpeed:{_baseSpeed}");
        }

        private void OnDataPointReceived(object sender, SerialDataPoint dataPoint)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => OnDataPointReceived(sender, dataPoint)));
                return;
            }

            // Add to plot queue
            lock (_plotQueueLock)
            {
                _plotDataQueue.Enqueue(dataPoint);
            }

            // Update raw data display (throttled)
            if (!cbDisableLogs.Checked)
            {
                UpdateRawDataDisplay($"{dataPoint.Millis}:{dataPoint.Angle:F2}:{dataPoint.Output}");
            }

            // Log data
            _logger?.Record((int)_setpoint, dataPoint.Angle, dataPoint.Output, dataPoint.Millis);
        }
        bool DEBUG = true;
        private void PlotTimer_Tick(object sender, EventArgs e)
        {
            _plotUpdatesCount++;

           if (!DEBUG) Debug.WriteLine("PlotTimer_Tick:" + DateTime.Now.ToString("ss.fff") + "   " + _plotUpdatesCount);


            try
            {
                var currentTime = DateTime.Now;

                // Calculate time since last plot (for jitter measurement)
                double intervalMs = (currentTime - _lastPlotTime).TotalMilliseconds;
                _lastPlotTime = currentTime;

                // Store interval for jitter calculation
                _plotIntervals.Add(intervalMs);
                if (_plotIntervals.Count > 100) // Keep last 100 samples
                    _plotIntervals.RemoveAt(0);

                SerialDataPoint[] pointsToPlot;
                lock (_plotQueueLock)
                {
                    if (_plotDataQueue.Count == 0) return;

                    pointsToPlot = _plotDataQueue.ToArray();
                    _plotDataQueue.Clear();
                }

                // Update sample counters
                _samplesThisSecond += pointsToPlot.Length;
                _totalSamplesPlotted += pointsToPlot.Length;

                foreach (var point in pointsToPlot)
                {
                    double scaledOutput = 0; 
                    if (_limit > 0) 
                    {
                        scaledOutput = (point.Output - _baseSpeed) * 59.0 / _limit; // ms
                    }

                    _plotService.AddDataPoint(point, _setpoint, scaledOutput); // 14 ms

                }

                // Update performance metrics every second
                UpdatePerformanceMetrics(); // ms

            }
            catch (Exception ex)
            {
                AppendToErrorLog($"Plot timer error: {ex.Message}");
            }  // ms
           if(!DEBUG) Debug.WriteLine("---FINISH:" + DateTime.Now.ToString("ss.fff"));

        }

        private void UpdatePerformanceMetrics()
        {
            var now = DateTime.Now;
            var timeSinceLastUpdate = (now - _lastSecondUpdate).TotalSeconds;

            if (timeSinceLastUpdate >= 1.0) // Update every second
            {
                // Calculate samples per second
                _samplesPerSecond = (int)(_samplesThisSecond / timeSinceLastUpdate);
                _samplesThisSecond = 0;
                _lastSecondUpdate = now;

                // Calculate jitter (standard deviation of intervals)
                if (_plotIntervals.Count > 1)
                {
                    double averageInterval = _plotIntervals.Average();
                    double sumOfSquares = _plotIntervals.Sum(interval =>
                        Math.Pow(interval - averageInterval, 2));
                    double jitterMs = Math.Sqrt(sumOfSquares / (_plotIntervals.Count - 1));

                    _averagePlotInterval = averageInterval;
                    _maxJitter = _plotIntervals.Max() - _plotIntervals.Min();

                    // Update performance display
                    UpdatePerformanceDisplay(_samplesPerSecond, jitterMs, averageInterval);
                }
            }
        }
        private void UpdatePerformanceDisplay(int samplesPerSecond, double jitterMs, double averageIntervalMs)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => UpdatePerformanceDisplay(samplesPerSecond, jitterMs, averageIntervalMs)));
                return;
            }

            if ((DateTime.Now - _lastPerformanceLog).TotalSeconds >= 3.0)
            {
                double elapsed = (DateTime.Now - _lastPlotUpdateTime).TotalSeconds;
                double updatesPerSecond = elapsed > 0 ? _plotUpdatesCount / elapsed : 0.0;

                // تردد الإرسال من Arduino (Serial Hz)
                double serialHz = _serialMessagesHz > 0 ? _serialMessagesHz : 63.0;
                double idealInterval = 1000.0 / serialHz; // الفترة المثالية بالملّي ثانية

                // حساب Jitter الفعلي استناداً إلى الفروق عن الفترة المثالية
                double rmsJitter = 0.0;
                if (_plotIntervals.Count > 1)
                {
                    var diffs = _plotIntervals.Select(x => Math.Pow(x - idealInterval, 2));
                    rmsJitter = Math.Sqrt(diffs.Average());
                }

                string perfLine =
                    $"Updates/S: {updatesPerSecond,4:F1} | " +
                    $"Interval: {averageIntervalMs,6:F2}ms | " +
                    $"Jitter: {rmsJitter,6:F2}ms | " +
                    $"MaxJ: {_plotIntervals.DefaultIfEmpty(0).Max() - _plotIntervals.DefaultIfEmpty(0).Min(),6:F2}ms | " +
                    $"Serial: {serialHz,4:F0} Hz";

                AppendToPerformancLog(perfLine);

                _lastPerformanceLog = DateTime.Now;
                _plotUpdatesCount = 0;
                _lastPlotUpdateTime = DateTime.Now;
            }
        }
        private void ResetPerformanceStats()
        {
            _samplesPerSecond = 0;
            _samplesThisSecond = 0;
            _plotIntervals.Clear();
            _averagePlotInterval = 0;
            _maxJitter = 0;
            _totalSamplesPlotted = 0;
            _lastSecondUpdate = DateTime.Now;
            _lastPlotTime = DateTime.Now;
            _lastPerformanceLog = DateTime.Now;

            AppendToErrorLog("[PERF] Performance statistics reset");
        }

        // أضف زر في الواجهة لاستدعاء هذه الدالة
        private void buttonResetStats_Click(object sender, EventArgs e)
        {
            ResetPerformanceStats();
        }

        private void OnParametersUpdated(object sender, PIDParameters parameters)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => OnParametersUpdated(sender, parameters)));
                return;
            }

            _internalUpdate = true;

            // Extract integer and fractional parts
            int kpInt = (int)Math.Floor(parameters.Kp);
            decimal kpFrac = Convert.ToDecimal(parameters.Kp - kpInt);
            int kiInt = (int)Math.Floor(parameters.Ki);
            decimal kiFrac = Convert.ToDecimal(parameters.Ki - kiInt);
            int kdInt = (int)Math.Floor(parameters.Kd);
            decimal kdFrac = Convert.ToDecimal(parameters.Kd - kdInt);

            nudKp.Value = kpInt;
            nudKpd.Value = kpFrac;
            nudKi.Value = kiInt;
            nudKid.Value = kiFrac;
            nudKd.Value = kdInt;
            nudKdd.Value = kdFrac;

            trackBar1.Value = parameters.Setpoint;
            string sign = trackBar1.Value >= 0 ? "+" : "";
            label7.Text = $"{sign}{trackBar1.Value}°";

            nudLimit.Value = parameters.Limit;
            nudBaseSpeed.Value = parameters.BaseSpeed;

            _baseSpeed = parameters.BaseSpeed;
            _limit = parameters.Limit;

            _internalUpdate = false;
        }

        private void OnStatusChanged(object sender, SystemStatus status)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => OnStatusChanged(sender, status)));
                return;
            }

            // Update status indicators based on connection state
            if (status.IsConnected)
            {
                Show(pb_ok);
            }
            else
            {
                Show(pb_notconnect);
            }
        }

        private void OnMessageReceived(object sender, string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => OnMessageReceived(sender, message)));
                return;
            }

            if (message.StartsWith(">>"))
            {
                AppendToInfoLog(message);
                Show(pb_ok);
            }
            else
            {
                AppendToErrorLog(message);
            }
        }

        #region UI Helper Methods

        private void AppendToInfoLog(string data)
        {
            if (rtbInfoData.Lines.Length > 1000)
            {
                rtbInfoData.Clear();
            }
            rtbInfoData.AppendText($"{data}{Environment.NewLine}");
            rtbInfoData.ScrollToCaret();
        }

        private void UpdateRawDataDisplay(string data)
        {
            if (rtbRawData.Lines.Length > 1000)
            {
                rtbRawData.Clear();
            }

            rtbRawData.AppendText($"{data}{Environment.NewLine}");
            rtbRawData.SelectionStart = rtbRawData.Text.Length;
            rtbRawData.ScrollToCaret();
        }

        private void AppendToPerformancLog(string message)
        {
            rtbPerformance.AppendText($"[{DateTime.Now:HH:mm:ss.fff}] {message}{Environment.NewLine}");
            rtbPerformance.SelectionStart = rtbPerformance.Text.Length;
            rtbPerformance.ScrollToCaret();
        }
        private void AppendToErrorLog(string message)
        {
            if (rtbError.InvokeRequired)
            {
                rtbError.Invoke(new Action(() =>
                {
                    rtbError.AppendText($"[{DateTime.Now:HH:mm:ss.fff}] {message}{Environment.NewLine}");
                    rtbError.SelectionStart = rtbPerformance.Text.Length;
                    rtbError.ScrollToCaret();
                    ;
                }));
            }
            else
            {
                rtbError.AppendText($"[{DateTime.Now:HH:mm:ss.fff}] {message}{Environment.NewLine}");
                rtbError.SelectionStart = rtbPerformance.Text.Length;
                rtbError.ScrollToCaret();

            }
        }

        private void Show(PictureBox pb)
        {
            pb.BringToFront();
        }

        private async void BlinkButton(Button button, bool isError)
        {
            var originalColor = button.BackColor;
            button.BackColor = isError ? Color.Red : Color.Green;

            await Task.Delay(200);

            button.BackColor = originalColor;
        }

        #endregion

        #region Logger Methods

        private void logactions(object sender, EventArgs e)
        {
            var tag = (string)(sender as Button).Tag;

            switch (tag)
            {
                case "create":
                    {
                        _logger?.Dispose();
                        _logger = new AsyncLogger("experimentData");
                        log_usable.Enabled = true;
                        log_unusable.Enabled = true;
                        log_start.Enabled = false;
                        Show(pb_ok);
                        Properties.Settings.Default.loggerName = loger_name.Text;
                        Properties.Settings.Default.Save();
                        return;
                    }
                case "accept":
                    {
                        _logger?.Finish(true);
                        _logger = null;

                        log_usable.Enabled = false;
                        log_unusable.Enabled = false;
                        log_start.Enabled = true;

                        return;
                    }
                case "unusable":
                    {
                        _logger?.Finish(false);
                        _logger = null;
                        log_usable.Enabled = false;
                        log_unusable.Enabled = false;
                        log_start.Enabled = true;
                        return;
                    }
                default:
                    break;
            }
        }

        #endregion

        #region Form Events

        private void panel3_Paint(object sender, PaintEventArgs e) { }
        private void panel2_Paint(object sender, PaintEventArgs e) { }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            info_panel.Visible = !info_panel.Visible;
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {

  
            // Stop timers first
            _plotTimer?.Stop();

            // Dispose without waiting
            _controller?.Dispose();
            _plotTimer?.Dispose();


            _logger?.Finish(false);
            _logger?.Dispose();

            base.OnFormClosing(e);
            }
            catch (Exception)
            {

                Application.Exit();
            }
        }
        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            // OnFormClosing(e);
        }

    }
    // Add the following extension method to provide the missing 'GetCommunicator' functionality.

    public static class BalancingPlatformControllerExtensions
    {
        public static ISerialCommunicator GetCommunicator(this BalancingPlatformController controller)
        {
            // Use reflection to access the private _communicator field
            var field = typeof(BalancingPlatformController).GetField("_communicator",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            return field?.GetValue(controller) as ISerialCommunicator;
        }
    }

    #endregion
}
