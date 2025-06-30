using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stabilization
{
    public partial class Form1 : Form
    {
        #region Fields and Properties

        // Thread-safe collections for high-performance data handling
        private readonly ConcurrentQueue<SerialDataPoint> _dataQueue = new ConcurrentQueue<SerialDataPoint>();
        private readonly ConcurrentQueue<string> _commandQueue = new ConcurrentQueue<string>();

        // Cancellation tokens for clean shutdown
        private CancellationTokenSource _processingCancellation = new CancellationTokenSource();
        private CancellationTokenSource _plottingCancellation = new CancellationTokenSource();

        private bool _readingStatus = false;
        private Dictionary<string, double> _currentParams = new Dictionary<string, double>();


        // High-resolution timer for consistent plotting
        private System.Windows.Forms.Timer _plotTimer;

        // Buffered data for smooth plotting
        private readonly object _dataLock = new object();
        private volatile bool _newDataAvailable = false;

        // Performance counters
        private int _dataPointsReceived = 0;
        private DateTime _lastPerformanceUpdate = DateTime.Now;

        // Plot configuration
        private const int MAX_PLOT_POINTS = 200;
        private const int PLOT_UPDATE_INTERVAL_MS = 16; // ~60 FPS
        private const int DATA_PROCESSING_DELAY_MS = 1; // Minimal delay for CPU efficiency

        public double Setpoint { get; set; } = 0;

        Plot plot = new Plot();

        #endregion

        #region Data Structures

        public struct SerialDataPoint
        {
            public double Angle;
            public int Output;
            public DateTime Timestamp;
            public long SequenceNumber;
            public long Millis;      // Arduino timestamp in milliseconds

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
            public int limit { get; set; }
            public int baseSpeed { get; set; }

            public int setpoint { get; set; }
        }

        public Form1()
        {
            InitializeComponent();
            InitializeSettings();
            InitializeRealTimeProcessing();
            plot.Show();
            // Move delay and connect to a background thread to avoid blocking the UI
            Task.Run(() =>
            {
                Thread.Sleep(500);
                // Use BeginInvoke to ensure UI thread access
                BeginInvoke(new Action(() => btconnect_Click(null, null)));
            });
        }

        private void InitializeSettings()
        {
            // this lcoation is on right and down.
            this.StartPosition = FormStartPosition.Manual;
            // calculat postion to be all window of right and down.
            // get this width and hiens to calculation postion.
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, Screen.PrimaryScreen.Bounds.Height - this.Height - 30);
            //is.Location = new Point(627, 202);
            // Load settings (your existing code)
            mtbPortname.Text = Properties.Settings.Default.PortName;
            cmBuadrite.Text = Properties.Settings.Default.BaudRate.ToString();

            // Load PID values
            nudKp.Value = Properties.Settings.Default.Kp;
            nudKi.Value = Properties.Settings.Default.Ki;
            nudKd.Value = Properties.Settings.Default.Kd;
            nudKpd.Value = Properties.Settings.Default.Kpd;
            nudKid.Value = Properties.Settings.Default.Kid;
            nudKdd.Value = Properties.Settings.Default.Kdd;
            nudBaseSpeed.Value = Properties.Settings.Default.PWMbase;
            nudLimit.Value = Properties.Settings.Default.PIDOut;
            loger_name.Text = Properties.Settings.Default.loggerName;


            // Clear the graph
            plot.chart1.Series["Series1"].Points.Clear();
        }

        private void InitializeRealTimeProcessing()
        {
            // Configure high-performance timer for plotting
            _plotTimer = new System.Windows.Forms.Timer();
            _plotTimer.Interval = PLOT_UPDATE_INTERVAL_MS;
            _plotTimer.Tick += PlotTimer_Tick;

            // Start background processing tasks
            StartDataProcessingTask();
            StartCommandProcessingTask();
        }

        #endregion

        #region Connection Management

        private void btconnect_Click(object sender, EventArgs e)
        {
            try
            {
                arduino.PortName = mtbPortname.Text;
                arduino.BaudRate = Convert.ToInt32(cmBuadrite.Text);

                // Configure serial port for optimal performance
                arduino.ReadBufferSize = 8192; // Larger buffer
                arduino.WriteBufferSize = 2048;
                arduino.ReadTimeout = 100;
                arduino.WriteTimeout = 100;
                arduino.NewLine = "\n";

                arduino.DataReceived += Arduino_DataReceived;
                arduino.ErrorReceived += Arduino_ErrorReceived;

                arduino.Open();

                if (arduino.IsOpen)
                {
                    // Save settings
                    Properties.Settings.Default.PortName = mtbPortname.Text;
                    Properties.Settings.Default.BaudRate = Convert.ToInt32(cmBuadrite.Text);
                    Properties.Settings.Default.Save();

                    // Update UI
                    btconnect.Enabled = false;
                    btdisconnect.Enabled = true;
                    pictureBox1.BackColor = Color.Green;
                    pictureBox7.Hide();
                    // Start real-time processing
                    _plotTimer.Start();
                    pb_ok.BringToFront();
                    AppendToErrorLog($"Connected to Arduino on port {mtbPortname.Text} at {cmBuadrite.Text} baud");
                }
                else
                {
                    pictureBox1.BackColor = Color.Red;
                    MessageBox.Show("Failed to connect to Arduino", "Connection Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    pb_notconnect.BringToFront();
                }
            }
            catch (Exception ex)
            {
                pb_notconnect.BringToFront();

                MessageBox.Show($"Connection Error: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btdisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (arduino?.IsOpen == true || btconnect.Enabled)
                {
                    // Stop processing
                    _plotTimer.Stop();
                    _processingCancellation.Cancel();
                    _plottingCancellation.Cancel();

                    // Cleanup serial port
                    arduino.DataReceived -= Arduino_DataReceived;
                    arduino.ErrorReceived -= Arduino_ErrorReceived;
                    arduino.DiscardInBuffer();
                    arduino.DiscardOutBuffer();
                    arduino.Close();

                    // Update UI
                    pictureBox1.BackColor = Color.Gray;
                    btconnect.Enabled = true;
                    btdisconnect.Enabled = false;
                    pictureBox7.Show();
                    // Restart cancellation tokens for next connection
                    _processingCancellation = new CancellationTokenSource();
                    _plottingCancellation = new CancellationTokenSource();
                    StartDataProcessingTask();
                    StartCommandProcessingTask();
                    pb_notconnect.BringToFront();
                    //  MessageBox.Show("Disconnected from Arduino", "Connection Status",
                    //           MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                pb_wrong.BringToFront();

                MessageBox.Show($"Disconnection Error: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        #endregion

        #region High-Performance Data Processing

        // Buffer for handling partial data
        private string _serialBuffer = "";
        private readonly object _bufferLock = new object();

        private void Arduino_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string newData = arduino.ReadExisting();
                // here check if monitor modeoff 

                if (string.IsNullOrEmpty(newData)) return;

                lock (_bufferLock)
                {
                    // Append new data to buffer
                    _serialBuffer += newData;

                    // Process complete lines
                    while (_serialBuffer.Contains('\n'))
                    {
                        int lineEnd = _serialBuffer.IndexOf('\n');
                        string completeLine = _serialBuffer.Substring(0, lineEnd).Trim();
                        _serialBuffer = _serialBuffer.Substring(lineEnd + 1);

                        if (string.IsNullOrEmpty(completeLine)) continue;

                        // Debug: Log raw data for troubleshooting
                        if (_dataPointsReceived % 100 == 0) // Log every 100th line
                        {
                            if (!cbDisableLogs.Checked)
                                Task.Run(() => AppendToErrorLog($"data: '{completeLine}'"));
                        }

                        // Parse and enqueue data
                        if (TryParseDataPoint(completeLine, out SerialDataPoint dataPoint))
                        {
                            _dataQueue.Enqueue(dataPoint);
                            Interlocked.Increment(ref _dataPointsReceived);
                            _newDataAvailable = true;
                        }
                        else
                        {
                            if (cbDisableLogs.Checked) return; // Skip logging if disabled

                            BeginInvoke(new Action(() => rtbError.AppendText(($"Failed to parse: '{completeLine + Environment.NewLine}'"))));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (cbDisableLogs.Checked) return; // Skip logging if disabled

                Task.Run(() => AppendToErrorLog($"Data receive error: {ex.Message}"));
            }
        }

        // 2. Update the TryParseDataPoint method
        private bool TryParseDataPoint(string data, out SerialDataPoint dataPoint)
        {
            dataPoint = default;

            try
            {
                // Clean the data - remove any non-printable characters
                data = data.Trim('\r', '\n', ' ', '\t');

                // Handle status reading (existing code remains the same)
                if (READ_PARAMS)
                {
                    if (data == "STATUS_START")
                    {
                        _readingStatus = true;
                        _currentParams.Clear();
                        return false;
                    }
                    else if (data == "STATUS_END")
                    {
                        _readingStatus = false;
                        READ_PARAMS = false;

                        // Process collected parameters
                        if (_currentParams.Count >= 4)
                        {
                            var pidParams = new PIDParameters
                            {
                                Kp = _currentParams.ContainsKey("Kp") ? _currentParams["Kp"] : 0,
                                Ki = _currentParams.ContainsKey("Ki") ? _currentParams["Ki"] : 0,
                                Kd = _currentParams.ContainsKey("Kd") ? _currentParams["Kd"] : 0,
                                limit = (int)(_currentParams.ContainsKey("limit") ? _currentParams["limit"] : 0),
                                baseSpeed = (int)(_currentParams.ContainsKey("baseSpeed") ? _currentParams["baseSpeed"] : 1000),
                                setpoint = (int)(_currentParams.ContainsKey("setpoint") ? _currentParams["setpoint"] : 0)
                            };

                            ShowPIDParameters(pidParams);
                        }
                        return false;
                    }
                    else if (_readingStatus && data.Contains(":"))
                    {
                        // Parse parameter line
                        var partss = data.Split(':');
                        if (partss.Length == 2)
                        {
                            string paramName = partss[0].Trim();
                            if (double.TryParse(partss[1].Trim(),
                                              System.Globalization.NumberStyles.Float,
                                              System.Globalization.CultureInfo.InvariantCulture,
                                              out double value))
                            {
                                _currentParams[paramName] = value;
                            }
                        }
                        return false;
                    }
                }

                // Skip empty lines or obvious non-data lines
                if (string.IsNullOrEmpty(data) || data.Length < 3) return false;

                if (data.StartsWith(">>"))
                {
                    // this is info so write it into rtbInfoData
                    BeginInvoke(new Action(() => rtbInfoData.AppendText($"{data}{Environment.NewLine}")));
                    BeginInvoke(new Action(() => rtbRawData.ScrollToCaret()));
                    return false;
                }

                // NEW: Parse three-value format "millis():Angle:output"
                var parts = data.Split(':');
                if (parts.Length != 3)  // Changed from 2 to 3
                {
                    // Try comma separator as fallback for three values
                    parts = data.Split(',');
                    if (parts.Length != 3) return false;  // Changed from 2 to 3
                }

                // Parse all three values using invariant culture
                if (long.TryParse(parts[0].Trim(), out long millis) &&  // Parse millis as long
                    double.TryParse(parts[1].Trim(), System.Globalization.NumberStyles.Float,
                                  System.Globalization.CultureInfo.InvariantCulture, out double angle) &&
                    int.TryParse(parts[2].Trim(), System.Globalization.NumberStyles.Integer,
                                  System.Globalization.CultureInfo.InvariantCulture, out int output))
                {
                    // Validate ranges to catch obviously wrong data
                    if (Math.Abs(angle) > 90 || Math.Abs(output) > 2000)
                    {
                        if (!cbDisableLogs.Checked)
                            Task.Run(() => AppendToErrorLog($"out of range - Millis: {millis}, Angle: {angle}, Output: {output}"));
                        return false;
                    }

                    dataPoint = new SerialDataPoint(millis, angle, output, _dataPointsReceived);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                if (!cbDisableLogs.Checked)
                    Task.Run(() => AppendToErrorLog($"Parse error for '{data}': {ex.Message}"));
                Task.Run(() => pb_wrong.BringToFront());




                return false;
            }
        }
        // 3. Update the UpdateRawDataDisplay method call in StartDataProcessingTask
        private void StartDataProcessingTask()
        {
            Task.Run(async () =>
            {
                while (!_processingCancellation.Token.IsCancellationRequested)
                {
                    try
                    {
                        // Process UI updates in batches for better performance
                        if (_dataQueue.TryDequeue(out SerialDataPoint dataPoint))
                        {
                            // Update raw data display (throttled)
                            if (_dataPointsReceived % 10 == 0) // Update every 10th data point
                            {
                                // Updated to include millis in display
                                var displayText = $"{dataPoint.Millis}:{dataPoint.Angle:F2}:{dataPoint.Output:F2}";
                                BeginInvoke(new Action(() => UpdateRawDataDisplay(displayText)));
                            }
                        }

                        // Small delay to prevent CPU overload
                        await Task.Delay(DATA_PROCESSING_DELAY_MS, _processingCancellation.Token);
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                    catch (Exception ex)
                    {
                        pb_wrong.BringToFront();
                        AppendToErrorLog($"Data processing error: {ex.Message}");
                    }
                }
            }, _processingCancellation.Token);
        }


        #endregion

        #region Real-Time Plotting

        private void PlotTimer_Tick(object sender, EventArgs e)
        {
            if (!_newDataAvailable || plot?.chart1 == null) return;

            try
            {
                // Process all queued data points
                var newPoints = new List<SerialDataPoint>();
                while (_dataQueue.TryDequeue(out SerialDataPoint point) && newPoints.Count < 50)
                {
                    newPoints.Add(point);
                }

                if (newPoints.Count == 0) return;

                UpdatePlotWithNewData(newPoints);
                _newDataAvailable = _dataQueue.Count > 0;

                // Update performance counter periodically
                if ((DateTime.Now - _lastPerformanceUpdate).TotalSeconds >= 1.0)
                {
                    UpdatePerformanceDisplay();
                }
            }
            catch (Exception ex)
            {
                pb_wrong.BringToFront();
                AppendToErrorLog($"Plotting error: {ex.Message}");
            }
        }
        // 4. Update the UpdatePlotWithNewData method debug logging
        /// <summary>
        ///  ploting...
        /// </summary>
        /// <param name="newPoints"></param>
        private void UpdatePlotWithNewData(List<SerialDataPoint> newPoints)
        {
            if (plot?.chart1?.Series == null) return;

            try
            {
                var seriesAngle = plot.chart1.Series["Series1"];
                var seriesOutput = plot.chart1.Series["out"];
                var seriesTarget = plot.chart1.Series["target"];

                foreach (var point in newPoints)
                {
                    // Debug: Log some values periodically (updated to include millis)
                    if (_dataPointsReceived % 200 == 0)
                    {
                        AppendToErrorLog($"Plotting - Millis: {point.Millis}, Angle: {point.Angle:F2}, Output: {point.Output:F2}");
                    }

                    // Maintain rolling window
                    if (seriesAngle.Points.Count >= MAX_PLOT_POINTS)
                    {
                        seriesAngle.Points.RemoveAt(0);
                        if (seriesOutput.Points.Count > 0) seriesOutput.Points.RemoveAt(0);
                        if (seriesTarget.Points.Count > 0) seriesTarget.Points.RemoveAt(0);
                    }

                    // Add angle point (raw value)
                    seriesAngle.Points.AddY(point.Angle);
                    plot.label1.Text = point.Angle.ToString("F2", CultureInfo.InvariantCulture);
                    plot.label3.Text = Setpoint.ToString();

                    plot.label2.Text = point.Output.ToString("D4");
                    plot.label7.Text = point.Millis.ToString();

                    // Scale output appropriately - adjust this based on your Arduino output range
                    double scaledOutput;

                    // Convert from range (nudBaseSpeed.Value - nudLimit.Value, nudBaseSpeed.Value + nudLimit.Value) into (-59,59).
                    scaledOutput = (point.Output - (int)nudBaseSpeed.Value) * 59 / (int)nudLimit.Value;

                    seriesOutput.Points.AddY(scaledOutput);
                    seriesTarget.Points.AddY(Setpoint);

                    // Update logger to include millis if needed
                    logger?.Record((int)Setpoint, point.Angle, point.Output, point.Millis);
                }

                // Efficient chart update
                plot.chart1.Invalidate();
            }
            catch (Exception ex)
            {
                AppendToErrorLog($"Plot update error: {ex.Message}");
            }
        }

        #endregion

        #region Command Processing

        private void StartCommandProcessingTask()
        {
            Task.Run(async () =>
            {
                while (!_processingCancellation.Token.IsCancellationRequested)
                {
                    try
                    {
                        if (_commandQueue.TryDequeue(out string command))
                        {
                            if (arduino?.IsOpen == true)
                            {
                                arduino.WriteLine(command);
                                BeginInvoke(new Action(() =>
                                    rtbSendData.AppendText($"Sent: {command}{Environment.NewLine}")));
                            }
                        }

                        await Task.Delay(1, _processingCancellation.Token);
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                    catch (Exception ex)
                    {
                        if (!cbDisableLogs.Checked)
                            AppendToErrorLog($"Command processing error: {ex.Message}");
                    }
                }
            }, _processingCancellation.Token);
        }

        // Thread-safe command sending
        public void SendCommandAsync(string command)
        {
            if (!string.IsNullOrWhiteSpace(command))
            {
                _commandQueue.Enqueue(command);
            }
        }
        public void SendCommand(string command)
        {
            if (!string.IsNullOrWhiteSpace(command))
            {
                arduino.WriteLine(command);
            }
        }

        #endregion

        #region UI Event Handlers

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbSend.Text))
            {
                SendCommandAsync(tbSend.Text);
                tbSend.Clear();
                pb_working.BringToFront();
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Setpoint = trackBar1.Value;
            SendCommandAsync($"target:{trackBar1.Value}");

            // Update label
            string sign = trackBar1.Value >= 0 ? "+" : "";
            label7.Text = $"{sign}{trackBar1.Value}°";
        }
        bool _internal;

        private void PIDTune(object sender, EventArgs e)
        {
            if (_internal) return;

            if (sender is NumericUpDown num && num.Tag is string tag)
            {
                string command = $"{tag}:{num.Value.ToString(CultureInfo.InvariantCulture)}";
                SendCommandAsync(command);
                BlinkButton(button2, false);
                pb_working.BringToFront();

            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            SendCommandAsync("save");
            BlinkButton(button2, false);
            pb_working.BringToFront();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Save PID values to settings
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
                pb_ok.BringToFront();

            }
            catch (Exception ex)
            {
                AppendToErrorLog($"Error saving PID values: {ex.Message}");
                BlinkButton(button2, true);
            }
        }

        #endregion

        #region UI Helper Methods

        private void UpdateRawDataDisplay(string data)
        {
            if (rtbRawData.Lines.Length > 1000) // Limit display size
            {
                rtbRawData.Clear();
            }

            rtbRawData.AppendText($"{data}{Environment.NewLine}");
            rtbRawData.SelectionStart = rtbRawData.Text.Length;
            rtbRawData.ScrollToCaret();
        }

        private void AppendToErrorLog(string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => AppendToErrorLog(message)));
                return;
            }
            rtbErrors.AppendText($"[{DateTime.Now:HH:mm:ss.fff}] {message}{Environment.NewLine}");
            rtbErrors.SelectionStart = rtbErrors.Text.Length;
            rtbErrors.ScrollToCaret();
        }

        private void UpdatePerformanceDisplay()
        {
            var now = DateTime.Now;
            var timeDiff = (now - _lastPerformanceUpdate).TotalSeconds;
            if (timeDiff > 0)
            {
                double dataRate = _dataPointsReceived / timeDiff;
                _lastPerformanceUpdate = now;
                _dataPointsReceived = 0; // Reset counter

                BeginInvoke(new Action(() =>
                {
                    // Add performance info to error log periodically
                    AppendToErrorLog($"Performance: {dataRate:F1} Hz | Queue: {_dataQueue.Count} | Buffer: {_serialBuffer.Length}");
                }));
            }
        }

        private async void BlinkButton(Button button, bool isError)
        {
            var originalColor = button.BackColor;
            button.BackColor = isError ? Color.Red : Color.Green;

            await Task.Delay(200);

            button.BackColor = originalColor;
        }

        #endregion

        #region Error Handling

        private void Arduino_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            AppendToErrorLog($"Serial error: {e.EventType}");
        }

        #endregion

        #region Cleanup

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                _plotTimer?.Stop();
                _processingCancellation?.Cancel();
                _plottingCancellation?.Cancel();

                if (arduino?.IsOpen == true)
                {
                    arduino.Close();
                }

                _processingCancellation?.Dispose();
                _plottingCancellation?.Dispose();
                _plotTimer?.Dispose();
            }
            catch (Exception ex)
            {
                // Log cleanup errors but don't prevent closing
                System.Diagnostics.Debug.WriteLine($"Cleanup error: {ex.Message}");
            }

            base.OnFormClosing(e);
        }

        #endregion
        //22l
        private void button4_Click(object sender, EventArgs e)
        {
            SendCommandAsync("stop");
            SendCommand("stop");
            pb_wrong.BringToFront();
        }
        bool READ_PARAMS = false;
        private void ReadParams_fun(object sender, EventArgs e)
        {
            SendCommandAsync("status");
            READ_PARAMS = true;
            _readingStatus = false;
            _currentParams.Clear();

        }

        private void monitorMode_CheckedChanged(object sender, EventArgs e)
        {
            SendCommandAsync(monitorMode.Checked ? "monitor:on" : "monitor:off");
        }


        private void ShowPIDParameters(PIDParameters pidParams)
        {
            // Kp
            int kpInt = (int)Math.Floor(pidParams.Kp);
            int kpFrac = (int)Math.Round((pidParams.Kp - kpInt) * 10000);
            int kiInt = (int)Math.Floor(pidParams.Ki);
            int kiFrac = (int)Math.Round((pidParams.Ki - kiInt) * 10000);
            int kdInt = (int)Math.Floor(pidParams.Kd);
            int kdFrac = (int)Math.Round((pidParams.Kd - kdInt) * 10000);
            // update the UI thread:
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    _internal = true; // Prevent recursive calls
                    nudKp.Value = kpInt;
                    nudKpd.Value = kpFrac;
                    // Ki
                    nudKi.Value = kiInt;
                    nudKid.Value = kiFrac;

                    // Kd
                    nudKd.Value = kdInt;
                    nudKdd.Value = kdFrac;

                    // Setpoint
                    trackBar1.Value = Convert.ToInt16(pidParams.setpoint);
                    string sign = trackBar1.Value >= 0 ? "+" : "";
                    label7.Text = $"{sign}{trackBar1.Value}°";

                    // limit
                    nudLimit.Value = pidParams.limit;

                    //baseSpeed
                    nudBaseSpeed.Value = pidParams.baseSpeed;
                    _internal = false;
                }));
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SendCommandAsync("monitor:off");
            Thread.Sleep(101);
            SendCommandAsync("status");
            READ_PARAMS = true;
            _readingStatus = false;
            _currentParams.Clear();
            SendCommandAsync("monitor:on");

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            SendCommandAsync("reset");
            pb_working.BringToFront();

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (_internal) return;      // يُنفَّذ فقط عند تعديل المستخدم
            // sendlimit: command with the value of this.
            SendCommandAsync($"limit:{Convert.ToInt16(nudLimit.Value)}");

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (_internal) return;      // يُنفَّذ فقط عند تعديل المستخدم

            SendCommandAsync($"baseSpeed:{Convert.ToInt16(nudBaseSpeed.Value)}");

        }



        #region logger
        private AsyncLogger logger;
        private int timestep;
        // Add this field to Form1 to store the initial timestamp for logging
        private DateTime? _logStartTime = null;

        private void logactions(object sender, EventArgs e)
        {
            var tag = (string)(sender as Button).Tag;

            switch (tag)
            {
                case "create":
                    {
                        logger?.Dispose();
                        timestep = 0;
                        logger = new AsyncLogger("experimentData");
                        log_usable.Enabled = true;
                        log_unusable.Enabled = true;
                        log_start.Enabled = false;
                        pb_ok.BringToFront();
                        _logStartTime = null;
                        Properties.Settings.Default.loggerName = loger_name.Text;
                        Properties.Settings.Default.Save();
                        return;
                    }
                case "accept":
                    {
                        logger?.Finish(true);
                        logger = null;

                        log_usable.Enabled = false;
                        log_unusable.Enabled = false;
                        log_start.Enabled = true;

                        return;
                    }
                case "unusable":
                    {
                        logger?.Finish(false);
                        logger = null;
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            info_panel.Visible = !info_panel.Visible;
        }
    }
}