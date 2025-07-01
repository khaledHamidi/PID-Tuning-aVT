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
        // 1. تحسين معالجة البيانات الواردة
        private void Arduino_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (!arduino.IsOpen) return; // تحقق من حالة الاتصال

                string newData = arduino.ReadExisting();
                if (string.IsNullOrEmpty(newData)) return;

                lock (_bufferLock)
                {
                    _serialBuffer += newData;

                    // معالجة جميع الأسطر المكتملة
                    string[] lines = _serialBuffer.Split('\n');

                    // الاحتفاظ بآخر سطر غير مكتمل
                    _serialBuffer = lines[lines.Length - 1];

                    // معالجة جميع الأسطر المكتملة
                    for (int i = 0; i < lines.Length - 1; i++)
                    {
                        string completeLine = lines[i].Trim('\r', '\n', ' ', '\t');

                        if (string.IsNullOrEmpty(completeLine)) continue;

                        // تسجيل البيانات الخام للتشخيص
                        if (_dataPointsReceived % 50 == 0 && !cbDisableLogs.Checked)
                        {
                            Task.Run(() => AppendToErrorLog($"Raw data: '{completeLine}'"));
                        }

                        // تحليل ووضع البيانات في الطابور
                        if (TryParseDataPoint(completeLine, out SerialDataPoint dataPoint))
                        {
                            _dataQueue.Enqueue(dataPoint);
                            Interlocked.Increment(ref _dataPointsReceived);

                            // تعيين علامة توفر بيانات جديدة
                            lock (_dataLock)
                            {
                                _newDataAvailable = true;
                            }
                        }
                        else if (!cbDisableLogs.Checked)
                        {
                            BeginInvoke(new Action(() =>
                                rtbError.AppendText($"Parse failed: '{completeLine}'{Environment.NewLine}")
                            ));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (!cbDisableLogs.Checked)
                    Task.Run(() => AppendToErrorLog($"Data receive error: {ex.Message}"));
            }
        }
        #endregion

        #region High-Performance Data Processing

        // Buffer for handling partial data
        private string _serialBuffer = "";
        private readonly object _bufferLock = new object();


        // 2. تحسين تحليل البيانات
        private bool TryParseDataPoint(string data, out SerialDataPoint dataPoint)
        {
            dataPoint = default;

            try
            {
                // تنظيف البيانات
                data = data.Trim('\r', '\n', ' ', '\t');

                if (string.IsNullOrEmpty(data) || data.Length < 3)
                    return false;

                // معالجة حالة قراءة المعاملات (STATUS)
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
                        ProcessStatusParameters();
                        return false;
                    }
                    else if (_readingStatus && data.Contains(":"))
                    {
                        ParseStatusParameter(data);
                        return false;
                    }
                }

                // تخطي الرسائل المعلوماتية
                if (data.StartsWith(">>"))
                {
                    BeginInvoke(new Action(() =>
                    {
                        rtbInfoData.AppendText($"{data}{Environment.NewLine}");
                        rtbInfoData.ScrollToCaret();
                    }));
                    Show(pb_ok);
                    return false;
                }

                // تحليل البيانات بصيغة "millis:angle:output"
                var parts = data.Split(':');
                if (parts.Length != 3)
                {
                    // المحاولة بالفاصلة كبديل
                    parts = data.Split(',');
                    if (parts.Length != 3)
                        return false;
                }

                // تحليل القيم الثلاث
                if (long.TryParse(parts[0].Trim(), out long millis) &&
                    double.TryParse(parts[1].Trim(),
                                  NumberStyles.Float,
                                  CultureInfo.InvariantCulture, out double angle) &&
                    int.TryParse(parts[2].Trim(),
                                NumberStyles.Integer,
                                CultureInfo.InvariantCulture, out int output))
                {
                    // التحقق من صحة النطاقات (تحذير فقط، لا رفض)
                    if (Math.Abs(angle) > 180 || Math.Abs(output) > 5000)
                    {
                        if (!cbDisableLogs.Checked)
                            Task.Run(() => AppendToErrorLog(
                                $"Unusual values - Millis: {millis}, Angle: {angle:F2}, Output: {output}"));
                    }

                    dataPoint = new SerialDataPoint(millis, angle, output, _dataPointsReceived);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                if (!cbDisableLogs.Checked)
                    Task.Run(() => AppendToErrorLog($"Parse exception for '{data}': {ex.Message}"));
                return false;
            }
        }
        // 3. تحسين معالجة البيانات في المهمة الخلفية
        private void StartDataProcessingTask()
        {
            Task.Run(async () =>
            {
                var batchPoints = new List<SerialDataPoint>();

                while (!_processingCancellation.Token.IsCancellationRequested)
                {
                    try
                    {
                        batchPoints.Clear();

                        // جمع دفعة من النقاط لمعالجة أكثر كفاءة
                        while (_dataQueue.TryDequeue(out SerialDataPoint point) && batchPoints.Count < 20)
                        {
                            batchPoints.Add(point);
                        }

                        if (batchPoints.Count > 0)
                        {
                            // تحديث عرض البيانات الخام (مخفف)
                            //if (_dataPointsReceived % 5 != 0)
                            if(cbDisableLogs.Checked ==false)
                            {
                                var lastPoint = batchPoints.Last();
                                var displayText = $"{lastPoint.Millis}:{lastPoint.Angle:F2}:{lastPoint.Output}";
                                BeginInvoke(new Action(() => UpdateRawDataDisplay(displayText)));
                            }

                            // تحديث إشارة البيانات الجديدة للرسم
                            lock (_dataLock)
                            {
                                _newDataAvailable = true;
                            }
                        }

                        // تأخير قصير لمنع استهلاك CPU مرتفع
                        await Task.Delay(1, _processingCancellation.Token);
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                    catch (Exception ex)
                    {
                        Show(pb_wrong);
                        AppendToErrorLog($"Data processing error: {ex.Message}");
                        await Task.Delay(100); // تأخير أطول عند حدوث خطأ
                    }
                }
            }, _processingCancellation.Token);
        }


        #endregion

        #region Real-Time Plotting

        // 4. تحسين مؤقت الرسم
        private void PlotTimer_Tick(object sender, EventArgs e)
        {
            bool hasNewData;
            lock (_dataLock)
            {
                hasNewData = _newDataAvailable;
                _newDataAvailable = false; // إعادة تعيين العلامة
            }

            if (!hasNewData || plot?.chart1 == null)
                return;

            try
            {
                // معالجة دفعة من النقاط لضمان الأداء السلس
                var pointsToProcess = new List<SerialDataPoint>();
                int maxPointsPerUpdate = 10; // تحديد الحد الأقصى لنقاط كل تحديث

                while (_dataQueue.TryDequeue(out SerialDataPoint point) &&
                       pointsToProcess.Count < maxPointsPerUpdate)
                {
                    pointsToProcess.Add(point);
                }

                if (pointsToProcess.Count > 0)
                {
                    UpdatePlotWithNewData(pointsToProcess);

                    // إعادة تعيين العلامة إذا بقيت نقاط في الطابور
                    lock (_dataLock)
                    {
                        _newDataAvailable = _dataQueue.Count > 0;
                    }
                }

                // تحديث عداد الأداء
                if ((DateTime.Now - _lastPerformanceUpdate).TotalSeconds >= 2.0)
                {
                    UpdatePerformanceDisplay();
                }
            }
            catch (Exception ex)
            {
                Show(pb_wrong);
                AppendToErrorLog($"Plot timer error: {ex.Message}");
            }
        }

        // 5. تحسين تحديث الرسم البياني
        private void UpdatePlotWithNewData(List<SerialDataPoint> newPoints)
        {
            if (plot?.chart1?.Series == null || newPoints.Count == 0)
                return;

            try
            {
                var seriesAngle = plot.chart1.Series["Series1"];
                var seriesOutput = plot.chart1.Series["out"];
                var seriesTarget = plot.chart1.Series["target"];

                // تعليق التحديث لتحسين الأداء
                plot.chart1.SuspendLayout();

                foreach (var point in newPoints)
                {
                    // الحفاظ على نافذة متحركة
                    if (seriesAngle.Points.Count >= MAX_PLOT_POINTS)
                    {
                        seriesAngle.Points.RemoveAt(0);
                        if (seriesOutput.Points.Count > 0) seriesOutput.Points.RemoveAt(0);
                        if (seriesTarget.Points.Count > 0) seriesTarget.Points.RemoveAt(0);
                    }

                    // إضافة نقطة الزاوية
                    seriesAngle.Points.AddY(point.Angle);

                    // تحديث تسميات العرض
                    plot.label1.Text = point.Angle.ToString("F2", CultureInfo.InvariantCulture);
                    plot.label2.Text = point.Output.ToString();
                    plot.label3.Text = Setpoint.ToString();
                    plot.label7.Text = point.Millis.ToString();

                    // حساب الإخراج المقيس
                    double scaledOutput = 0;
                    int baseSpeed = (int)nudBaseSpeed.Value;
                    int limit = (int)nudLimit.Value;

                    if (limit > 0)
                    {
                        scaledOutput = (point.Output - baseSpeed) * 59.0 / limit;
                    }

                    seriesOutput.Points.AddY(scaledOutput);
                    seriesTarget.Points.AddY(Setpoint);

                    // تسجيل البيانات
                    logger?.Record((int)Setpoint, point.Angle, point.Output, point.Millis);
                }

                // استئناف التحديث وإعادة رسم واحدة
                plot.chart1.ResumeLayout();
                plot.chart1.Invalidate();

                // تسجيل تشخيصي
                if (_dataPointsReceived % 100 == 0 && !cbDisableLogs.Checked)
                {
                    var lastPoint = newPoints.Last();
                    AppendToErrorLog($"Plotted batch: {newPoints.Count} points, " +
                                   $"Last: Millis={lastPoint.Millis}, Angle={lastPoint.Angle:F2}");
                }
            }
            catch (Exception ex)
            {
                plot.chart1.ResumeLayout(); // تأكد من استئناف التخطيط حتى عند الخطأ
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
                    pictureBox7.Hide();
                    // Start real-time processing
                    _plotTimer.Start();
                    Show(pb_ok);
                    AppendToErrorLog($"Connected to Arduino on port {mtbPortname.Text} at {cmBuadrite.Text} baud");
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
                    pictureBox1.Parent = panel4;
                    btconnect.Enabled = true;
                    btdisconnect.Enabled = false;
                    pictureBox7.Show();
                    // Restart cancellation tokens for next connection
                    _processingCancellation = new CancellationTokenSource();
                    _plottingCancellation = new CancellationTokenSource();
                    StartDataProcessingTask();
                    StartCommandProcessingTask();
                    Show(pb_notconnect);
                    //  MessageBox.Show("Disconnected from Arduino", "Connection Status",
                    //           MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Show(pb_wrong);

                MessageBox.Show($"Disconnection Error: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbSend.Text))
            {
                SendCommandAsync(tbSend.Text);
                tbSend.Clear();
                Show(pb_working);
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
                Show(pb_working);

            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            SendCommandAsync("save");
            BlinkButton(button2, false);
            Show(pb_working);

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
                Show(pb_ok);

            }
            catch (Exception ex)
            {
                AppendToErrorLog($"Error saving PID values: {ex.Message}");
                BlinkButton(button2, true);
            }
        }

        #endregion

        #region UI Helper Methods
        // 6. إضافة طرق مساعدة لمعالجة المعاملات
        private void ProcessStatusParameters()
        {
            if (_currentParams.Count >= 4)
            {
                var pidParams = new PIDParameters
                {
                    Kp = _currentParams.GetValueOrDefault("Kp", 0),
                    Ki = _currentParams.GetValueOrDefault("Ki", 0),
                    Kd = _currentParams.GetValueOrDefault("Kd", 0),
                    limit = (int)_currentParams.GetValueOrDefault("Limit", 0),
                    baseSpeed = (int)_currentParams.GetValueOrDefault("baseSpeed", 1000),
                    setpoint = (int)_currentParams.GetValueOrDefault("Target", 0)
                };

                ShowPIDParameters(pidParams);
            }
        }

        private void ParseStatusParameter(string data)
        {
            var parts = data.Split(':');
            if (parts.Length == 2)
            {
                string paramName = parts[0].Trim();
                if (double.TryParse(parts[1].Trim(),
                                  NumberStyles.Float,
                                  CultureInfo.InvariantCulture,
                                  out double value))
                {
                    _currentParams[paramName] = value;
                }
                else if (int.TryParse(parts[1].Trim(),
                                  NumberStyles.Float,
                                  CultureInfo.InvariantCulture,
                                  out int value2))
                {
                    _currentParams[paramName] = value2;
                }
            }
        }

        // 7. تحسين عرض الأداء
        private void UpdatePerformanceDisplay()
        {
            var now = DateTime.Now;
            var timeDiff = (now - _lastPerformanceUpdate).TotalSeconds;

            if (timeDiff > 0)
            {
                double dataRate = _dataPointsReceived / timeDiff;
                _lastPerformanceUpdate = now;

                int currentDataCount = _dataPointsReceived;
                _dataPointsReceived = 0; // إعادة تعيين العداد

                BeginInvoke(new Action(() =>
                {
                    AppendToErrorLog($"Performance: {dataRate:F1} Hz | " +
                                   $"Queue: {_dataQueue.Count} | " +
                                   $"Buffer: {_serialBuffer?.Length ?? 0} | " +
                                   $"Points: {currentDataCount}");
                }));
            }
        }

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
        private void Show(PictureBox pb)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => Show(pb)));
                return;
            }
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
            Show(pb_wrong);
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
            Show(pb_working);

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
                        Show(pb_ok);
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

    // إضافة method للحصول على القيم مع قيمة افتراضية
    public static class DictionaryExtensions
    {
        public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary,
                                                            TKey key, TValue defaultValue)
        {
            return dictionary.ContainsKey(key) ? dictionary[key] : defaultValue;
        }
    }
}