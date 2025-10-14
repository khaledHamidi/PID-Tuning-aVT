namespace Stabilization
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btconnect = new System.Windows.Forms.Button();
            this.mtbPortname = new System.Windows.Forms.MaskedTextBox();
            this.arduino = new System.IO.Ports.SerialPort(this.components);
            this.cmBuadrite = new System.Windows.Forms.ComboBox();
            this.btdisconnect = new System.Windows.Forms.Button();
            this.rtbRawData = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.rtbErrors = new System.Windows.Forms.RichTextBox();
            this.nudKpd = new System.Windows.Forms.NumericUpDown();
            this.cbDisableLogs = new System.Windows.Forms.CheckBox();
            this.cb_autoReadParams = new System.Windows.Forms.CheckBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.monitorMode = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label8 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.nudKd = new System.Windows.Forms.NumericUpDown();
            this.nudBaseSpeed = new System.Windows.Forms.NumericUpDown();
            this.nudKi = new System.Windows.Forms.NumericUpDown();
            this.nudLimit = new System.Windows.Forms.NumericUpDown();
            this.nudKp = new System.Windows.Forms.NumericUpDown();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.nudKdd = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.nudKid = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.UIupdater = new System.Windows.Forms.Timer(this.components);
            this.rtbSendData = new System.Windows.Forms.RichTextBox();
            this.rtbInfoData = new System.Windows.Forms.RichTextBox();
            this.rtbError = new System.Windows.Forms.RichTextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.label14 = new System.Windows.Forms.Label();
            this.loger_name = new System.Windows.Forms.TextBox();
            this.log_start = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pb_notconnect = new System.Windows.Forms.PictureBox();
            this.pb_working = new System.Windows.Forms.PictureBox();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.pb_ok = new System.Windows.Forms.PictureBox();
            this.pb_wrong = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.log_unusable = new System.Windows.Forms.Button();
            this.log_usable = new System.Windows.Forms.Button();
            this.label30 = new System.Windows.Forms.Label();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.tbSend = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label23 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label21 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.info_panel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.nudKpd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKid)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_notconnect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_working)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_ok)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_wrong)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            this.panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.info_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btconnect
            // 
            this.btconnect.Location = new System.Drawing.Point(34, 114);
            this.btconnect.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btconnect.Name = "btconnect";
            this.btconnect.Size = new System.Drawing.Size(165, 27);
            this.btconnect.TabIndex = 1;
            this.btconnect.Text = "Connect";
            this.btconnect.UseVisualStyleBackColor = true;
            this.btconnect.Click += new System.EventHandler(this.btconnect_Click);
            // 
            // mtbPortname
            // 
            this.mtbPortname.Location = new System.Drawing.Point(120, 59);
            this.mtbPortname.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.mtbPortname.Mask = "\\COM0";
            this.mtbPortname.Name = "mtbPortname";
            this.mtbPortname.Size = new System.Drawing.Size(79, 23);
            this.mtbPortname.TabIndex = 1;
            this.mtbPortname.Text = "9";
            // 
            // cmBuadrite
            // 
            this.cmBuadrite.FormattingEnabled = true;
            this.cmBuadrite.Items.AddRange(new object[] {
            "9600",
            "115200"});
            this.cmBuadrite.Location = new System.Drawing.Point(120, 85);
            this.cmBuadrite.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.cmBuadrite.Name = "cmBuadrite";
            this.cmBuadrite.Size = new System.Drawing.Size(79, 23);
            this.cmBuadrite.TabIndex = 2;
            this.cmBuadrite.Text = "115200";
            // 
            // btdisconnect
            // 
            this.btdisconnect.Enabled = false;
            this.btdisconnect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btdisconnect.Location = new System.Drawing.Point(209, 114);
            this.btdisconnect.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btdisconnect.Name = "btdisconnect";
            this.btdisconnect.Size = new System.Drawing.Size(93, 27);
            this.btdisconnect.TabIndex = 3;
            this.btdisconnect.Text = "disconnect";
            this.btdisconnect.UseVisualStyleBackColor = true;
            this.btdisconnect.Click += new System.EventHandler(this.btdisconnect_Click);
            // 
            // rtbRawData
            // 
            this.rtbRawData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbRawData.Location = new System.Drawing.Point(0, 0);
            this.rtbRawData.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.rtbRawData.Name = "rtbRawData";
            this.rtbRawData.Size = new System.Drawing.Size(288, 553);
            this.rtbRawData.TabIndex = 5;
            this.rtbRawData.Text = "";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.AutoSize = true;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Location = new System.Drawing.Point(204, 334);
            this.button1.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(68, 25);
            this.button1.TabIndex = 8;
            this.button1.Text = "send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rtbErrors
            // 
            this.rtbErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbErrors.Location = new System.Drawing.Point(5, 3);
            this.rtbErrors.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.rtbErrors.Name = "rtbErrors";
            this.rtbErrors.Size = new System.Drawing.Size(278, 547);
            this.rtbErrors.TabIndex = 9;
            this.rtbErrors.Text = "";
            // 
            // nudKpd
            // 
            this.nudKpd.DecimalPlaces = 3;
            this.nudKpd.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nudKpd.Location = new System.Drawing.Point(161, 63);
            this.nudKpd.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.nudKpd.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.nudKpd.Name = "nudKpd";
            this.nudKpd.Size = new System.Drawing.Size(85, 23);
            this.nudKpd.TabIndex = 12;
            this.nudKpd.Tag = "Kpd";
            this.nudKpd.ValueChanged += new System.EventHandler(this.PIDTune);
            // 
            // cbDisableLogs
            // 
            this.cbDisableLogs.AutoSize = true;
            this.cbDisableLogs.Checked = true;
            this.cbDisableLogs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDisableLogs.Location = new System.Drawing.Point(74, 287);
            this.cbDisableLogs.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.cbDisableLogs.Name = "cbDisableLogs";
            this.cbDisableLogs.Size = new System.Drawing.Size(97, 19);
            this.cbDisableLogs.TabIndex = 19;
            this.cbDisableLogs.Text = "Disable logs";
            this.cbDisableLogs.UseVisualStyleBackColor = true;
            // 
            // cb_autoReadParams
            // 
            this.cb_autoReadParams.AutoSize = true;
            this.cb_autoReadParams.Checked = true;
            this.cb_autoReadParams.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_autoReadParams.Location = new System.Drawing.Point(74, 262);
            this.cb_autoReadParams.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.cb_autoReadParams.Name = "cb_autoReadParams";
            this.cb_autoReadParams.Size = new System.Drawing.Size(209, 19);
            this.cb_autoReadParams.TabIndex = 17;
            this.cb_autoReadParams.Text = "Load parameters at connection.";
            this.cb_autoReadParams.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button6.Location = new System.Drawing.Point(74, 189);
            this.button6.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(165, 27);
            this.button6.TabIndex = 17;
            this.button6.Text = "Update GUI from device";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(74, 146);
            this.button5.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(165, 36);
            this.button5.TabIndex = 15;
            this.button5.Text = "Load parameters from device";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.ReadParams_fun);
            // 
            // monitorMode
            // 
            this.monitorMode.AutoSize = true;
            this.monitorMode.Checked = true;
            this.monitorMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.monitorMode.Location = new System.Drawing.Point(74, 95);
            this.monitorMode.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.monitorMode.Name = "monitorMode";
            this.monitorMode.Size = new System.Drawing.Size(105, 19);
            this.monitorMode.TabIndex = 14;
            this.monitorMode.Text = "monitor mode";
            this.monitorMode.UseVisualStyleBackColor = true;
            this.monitorMode.CheckedChanged += new System.EventHandler(this.monitorMode_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Helvetica Rounded", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(149, 104);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 28);
            this.label7.TabIndex = 1;
            this.label7.Text = "00°";
            // 
            // button7
            // 
            this.button7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button7.Location = new System.Drawing.Point(5, 135);
            this.button7.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(319, 47);
            this.button7.TabIndex = 18;
            this.button7.Text = "Recalibration IMU";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(33, 62);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.trackBar1.Maximum = 60;
            this.trackBar1.Minimum = -60;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(268, 45);
            this.trackBar1.TabIndex = 0;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(73, 168);
            this.label8.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(182, 15);
            this.label8.TabIndex = 35;
            this.label8.Text = "Zeros on the left are not taken";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label18.Location = new System.Drawing.Point(204, 98);
            this.label18.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(86, 15);
            this.label18.TabIndex = 34;
            this.label18.Text = "default : 1500";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label17.Location = new System.Drawing.Point(203, 67);
            this.label17.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(87, 15);
            this.label17.TabIndex = 33;
            this.label17.Text = "~ [1000,2000]";
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.AutoSize = true;
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.Location = new System.Drawing.Point(74, 222);
            this.button3.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(165, 25);
            this.button3.TabIndex = 21;
            this.button3.Text = "Save settings";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(31, 98);
            this.label16.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(61, 15);
            this.label16.TabIndex = 32;
            this.label16.Text = "PID limit:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(34, 72);
            this.label15.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(59, 15);
            this.label15.TabIndex = 31;
            this.label15.Text = "Throttle :";
            // 
            // nudKd
            // 
            this.nudKd.Location = new System.Drawing.Point(93, 123);
            this.nudKd.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.nudKd.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudKd.Name = "nudKd";
            this.nudKd.Size = new System.Drawing.Size(50, 23);
            this.nudKd.TabIndex = 27;
            this.nudKd.Tag = "Kd";
            this.nudKd.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.nudKd.ValueChanged += new System.EventHandler(this.PIDTune);
            // 
            // nudBaseSpeed
            // 
            this.nudBaseSpeed.Location = new System.Drawing.Point(102, 67);
            this.nudBaseSpeed.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.nudBaseSpeed.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudBaseSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nudBaseSpeed.Name = "nudBaseSpeed";
            this.nudBaseSpeed.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.nudBaseSpeed.Size = new System.Drawing.Size(83, 23);
            this.nudBaseSpeed.TabIndex = 28;
            this.nudBaseSpeed.Tag = "Kp";
            this.nudBaseSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudBaseSpeed.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.nudBaseSpeed.Value = new decimal(new int[] {
            1200,
            0,
            0,
            0});
            this.nudBaseSpeed.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // nudKi
            // 
            this.nudKi.Location = new System.Drawing.Point(93, 93);
            this.nudKi.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.nudKi.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudKi.Name = "nudKi";
            this.nudKi.Size = new System.Drawing.Size(50, 23);
            this.nudKi.TabIndex = 26;
            this.nudKi.Tag = "Ki";
            this.nudKi.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.nudKi.ValueChanged += new System.EventHandler(this.PIDTune);
            // 
            // nudLimit
            // 
            this.nudLimit.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudLimit.Location = new System.Drawing.Point(102, 96);
            this.nudLimit.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.nudLimit.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudLimit.Name = "nudLimit";
            this.nudLimit.Size = new System.Drawing.Size(83, 23);
            this.nudLimit.TabIndex = 26;
            this.nudLimit.Tag = "Kp";
            this.nudLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudLimit.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudLimit.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // nudKp
            // 
            this.nudKp.Location = new System.Drawing.Point(93, 63);
            this.nudKp.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.nudKp.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudKp.Name = "nudKp";
            this.nudKp.Size = new System.Drawing.Size(50, 23);
            this.nudKp.TabIndex = 25;
            this.nudKp.Tag = "Kp";
            this.nudKp.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.nudKp.ValueChanged += new System.EventHandler(this.PIDTune);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.AutoSize = true;
            this.button2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.Location = new System.Drawing.Point(74, 58);
            this.button2.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(165, 28);
            this.button2.TabIndex = 14;
            this.button2.Text = "update memory ";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(57, 125);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 16);
            this.label3.TabIndex = 20;
            this.label3.Tag = "Kd";
            this.label3.Text = "Kp";
            // 
            // nudKdd
            // 
            this.nudKdd.DecimalPlaces = 3;
            this.nudKdd.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nudKdd.Location = new System.Drawing.Point(161, 123);
            this.nudKdd.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.nudKdd.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.nudKdd.Name = "nudKdd";
            this.nudKdd.Size = new System.Drawing.Size(85, 23);
            this.nudKdd.TabIndex = 19;
            this.nudKdd.Tag = "Kdd";
            this.nudKdd.ValueChanged += new System.EventHandler(this.PIDTune);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(57, 95);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 16);
            this.label2.TabIndex = 17;
            this.label2.Text = "Ki";
            // 
            // nudKid
            // 
            this.nudKid.DecimalPlaces = 3;
            this.nudKid.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nudKid.Location = new System.Drawing.Point(161, 93);
            this.nudKid.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.nudKid.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.nudKid.Name = "nudKid";
            this.nudKid.Size = new System.Drawing.Size(85, 23);
            this.nudKid.TabIndex = 16;
            this.nudKid.Tag = "Kid";
            this.nudKid.ValueChanged += new System.EventHandler(this.PIDTune);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(57, 65);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 16);
            this.label1.TabIndex = 14;
            this.label1.Text = "Kp";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(146, 128);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 16);
            this.label4.TabIndex = 24;
            this.label4.Tag = "Kd";
            this.label4.Text = "+";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(146, 98);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 16);
            this.label5.TabIndex = 23;
            this.label5.Text = "+";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(146, 68);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 16);
            this.label6.TabIndex = 22;
            this.label6.Text = "+";
            // 
            // UIupdater
            // 
            this.UIupdater.Interval = 10;
            // 
            // rtbSendData
            // 
            this.rtbSendData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbSendData.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbSendData.Location = new System.Drawing.Point(5, 3);
            this.rtbSendData.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.rtbSendData.Name = "rtbSendData";
            this.rtbSendData.Size = new System.Drawing.Size(278, 547);
            this.rtbSendData.TabIndex = 16;
            this.rtbSendData.Text = "";
            // 
            // rtbInfoData
            // 
            this.rtbInfoData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbInfoData.Location = new System.Drawing.Point(5, 3);
            this.rtbInfoData.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.rtbInfoData.Name = "rtbInfoData";
            this.rtbInfoData.Size = new System.Drawing.Size(468, 564);
            this.rtbInfoData.TabIndex = 20;
            this.rtbInfoData.Text = "";
            // 
            // rtbError
            // 
            this.rtbError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbError.Location = new System.Drawing.Point(0, 0);
            this.rtbError.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.rtbError.Name = "rtbError";
            this.rtbError.Size = new System.Drawing.Size(288, 553);
            this.rtbError.TabIndex = 21;
            this.rtbError.Text = "";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(238)))), ((int)(((byte)(242)))));
            this.panel4.Controls.Add(this.pictureBox7);
            this.panel4.Controls.Add(this.label22);
            this.panel4.Controls.Add(this.label20);
            this.panel4.Controls.Add(this.label19);
            this.panel4.Controls.Add(this.pictureBox2);
            this.panel4.Controls.Add(this.pictureBox1);
            this.panel4.Controls.Add(this.btdisconnect);
            this.panel4.Controls.Add(this.btconnect);
            this.panel4.Controls.Add(this.mtbPortname);
            this.panel4.Controls.Add(this.cmBuadrite);
            this.panel4.ForeColor = System.Drawing.Color.Black;
            this.panel4.Location = new System.Drawing.Point(14, 12);
            this.panel4.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(331, 183);
            this.panel4.TabIndex = 23;
            // 
            // pictureBox7
            // 
            this.pictureBox7.Image = global::Stabilization.Properties.Resources.unlink;
            this.pictureBox7.Location = new System.Drawing.Point(213, 22);
            this.pictureBox7.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(89, 72);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox7.TabIndex = 18;
            this.pictureBox7.TabStop = false;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Helvetica", 11F, System.Drawing.FontStyle.Bold);
            this.label22.ForeColor = System.Drawing.Color.Blue;
            this.label22.Location = new System.Drawing.Point(61, 23);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(111, 17);
            this.label22.TabIndex = 17;
            this.label22.Text = "CONNECTION";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(36, 88);
            this.label20.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(65, 16);
            this.label20.TabIndex = 16;
            this.label20.Text = "Buadrate:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(31, 62);
            this.label19.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(70, 16);
            this.label19.TabIndex = 15;
            this.label19.Text = "port name:";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Stabilization.Properties.Resources.serial;
            this.pictureBox2.Location = new System.Drawing.Point(18, 13);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(35, 35);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Stabilization.Properties.Resources.connected;
            this.pictureBox1.Location = new System.Drawing.Point(213, 22);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(89, 72);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(486, 595);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.rtbInfoData);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.tabPage1.Size = new System.Drawing.Size(478, 570);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Response";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.rtbErrors);
            this.tabPage2.Location = new System.Drawing.Point(4, 38);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.tabPage2.Size = new System.Drawing.Size(288, 553);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Plot Points";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.rtbError);
            this.tabPage3.Location = new System.Drawing.Point(4, 38);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(288, 553);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Errors log";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.rtbRawData);
            this.tabPage4.Location = new System.Drawing.Point(4, 38);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(288, 553);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Logger";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.rtbSendData);
            this.tabPage5.Location = new System.Drawing.Point(4, 38);
            this.tabPage5.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.tabPage5.Size = new System.Drawing.Size(288, 553);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Send";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(10, 70);
            this.label14.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(111, 15);
            this.label14.TabIndex = 36;
            this.label14.Text = "experiment name:";
            // 
            // loger_name
            // 
            this.loger_name.Location = new System.Drawing.Point(52, 93);
            this.loger_name.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.loger_name.Name = "loger_name";
            this.loger_name.Size = new System.Drawing.Size(218, 23);
            this.loger_name.TabIndex = 35;
            this.loger_name.Text = "experimentData";
            // 
            // log_start
            // 
            this.log_start.Cursor = System.Windows.Forms.Cursors.Hand;
            this.log_start.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.log_start.ForeColor = System.Drawing.Color.Blue;
            this.log_start.Location = new System.Drawing.Point(158, 14);
            this.log_start.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.log_start.Name = "log_start";
            this.log_start.Size = new System.Drawing.Size(139, 35);
            this.log_start.TabIndex = 34;
            this.log_start.Tag = "create";
            this.log_start.Text = "➕ CREATE ";
            this.log_start.UseVisualStyleBackColor = true;
            this.log_start.Click += new System.EventHandler(this.logactions);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.linkLabel1);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.panel10);
            this.panel2.Controls.Add(this.panel9);
            this.panel2.Controls.Add(this.panel8);
            this.panel2.Controls.Add(this.panel7);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(520, 601);
            this.panel2.TabIndex = 26;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(564, 583);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(122, 15);
            this.linkLabel1.TabIndex = 32;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "toggle info panel >>";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(238)))), ((int)(((byte)(242)))));
            this.panel1.Controls.Add(this.pb_notconnect);
            this.panel1.Controls.Add(this.pb_working);
            this.panel1.Controls.Add(this.pictureBox10);
            this.panel1.Controls.Add(this.pb_ok);
            this.panel1.Controls.Add(this.pb_wrong);
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(369, 398);
            this.panel1.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(320, 187);
            this.panel1.TabIndex = 31;
            // 
            // pb_notconnect
            // 
            this.pb_notconnect.Image = global::Stabilization.Properties.Resources.unlink;
            this.pb_notconnect.Location = new System.Drawing.Point(53, 14);
            this.pb_notconnect.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.pb_notconnect.Name = "pb_notconnect";
            this.pb_notconnect.Size = new System.Drawing.Size(245, 159);
            this.pb_notconnect.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_notconnect.TabIndex = 19;
            this.pb_notconnect.TabStop = false;
            // 
            // pb_working
            // 
            this.pb_working.Image = global::Stabilization.Properties.Resources.load;
            this.pb_working.Location = new System.Drawing.Point(51, 14);
            this.pb_working.Name = "pb_working";
            this.pb_working.Size = new System.Drawing.Size(245, 160);
            this.pb_working.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_working.TabIndex = 10;
            this.pb_working.TabStop = false;
            // 
            // pictureBox10
            // 
            this.pictureBox10.Image = global::Stabilization.Properties.Resources.done;
            this.pictureBox10.Location = new System.Drawing.Point(53, 13);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(245, 160);
            this.pictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox10.TabIndex = 9;
            this.pictureBox10.TabStop = false;
            // 
            // pb_ok
            // 
            this.pb_ok.Image = global::Stabilization.Properties.Resources.done;
            this.pb_ok.Location = new System.Drawing.Point(53, 13);
            this.pb_ok.Name = "pb_ok";
            this.pb_ok.Size = new System.Drawing.Size(245, 160);
            this.pb_ok.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_ok.TabIndex = 8;
            this.pb_ok.TabStop = false;
            // 
            // pb_wrong
            // 
            this.pb_wrong.Image = global::Stabilization.Properties.Resources.emergency;
            this.pb_wrong.Location = new System.Drawing.Point(53, 13);
            this.pb_wrong.Name = "pb_wrong";
            this.pb_wrong.Size = new System.Drawing.Size(245, 160);
            this.pb_wrong.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_wrong.TabIndex = 7;
            this.pb_wrong.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(241)))), ((int)(((byte)(250)))));
            this.panel3.Controls.Add(this.log_start);
            this.panel3.Controls.Add(this.log_unusable);
            this.panel3.Controls.Add(this.label14);
            this.panel3.Controls.Add(this.log_usable);
            this.panel3.Controls.Add(this.label30);
            this.panel3.Controls.Add(this.loger_name);
            this.panel3.Controls.Add(this.pictureBox9);
            this.panel3.ForeColor = System.Drawing.Color.Black;
            this.panel3.Location = new System.Drawing.Point(699, 398);
            this.panel3.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(302, 187);
            this.panel3.TabIndex = 30;
            // 
            // log_unusable
            // 
            this.log_unusable.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("log_unusable.BackgroundImage")));
            this.log_unusable.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.log_unusable.Cursor = System.Windows.Forms.Cursors.Hand;
            this.log_unusable.Enabled = false;
            this.log_unusable.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.log_unusable.ForeColor = System.Drawing.Color.Red;
            this.log_unusable.Location = new System.Drawing.Point(186, 134);
            this.log_unusable.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.log_unusable.Name = "log_unusable";
            this.log_unusable.Size = new System.Drawing.Size(67, 39);
            this.log_unusable.TabIndex = 2;
            this.log_unusable.Tag = "unusable";
            this.log_unusable.UseVisualStyleBackColor = true;
            this.log_unusable.Click += new System.EventHandler(this.logactions);
            // 
            // log_usable
            // 
            this.log_usable.BackgroundImage = global::Stabilization.Properties.Resources.check;
            this.log_usable.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.log_usable.Cursor = System.Windows.Forms.Cursors.Hand;
            this.log_usable.Enabled = false;
            this.log_usable.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.log_usable.ForeColor = System.Drawing.Color.Green;
            this.log_usable.Location = new System.Drawing.Point(52, 134);
            this.log_usable.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.log_usable.Name = "log_usable";
            this.log_usable.Size = new System.Drawing.Size(66, 39);
            this.log_usable.TabIndex = 1;
            this.log_usable.Tag = "accept";
            this.log_usable.UseVisualStyleBackColor = true;
            this.log_usable.Click += new System.EventHandler(this.logactions);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Helvetica", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(61, 23);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(46, 15);
            this.label30.TabIndex = 17;
            this.label30.Text = "logger";
            // 
            // pictureBox9
            // 
            this.pictureBox9.Image = global::Stabilization.Properties.Resources.log;
            this.pictureBox9.Location = new System.Drawing.Point(18, 13);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(35, 35);
            this.pictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox9.TabIndex = 5;
            this.pictureBox9.TabStop = false;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.panel5.Controls.Add(this.label9);
            this.panel5.Controls.Add(this.tbSend);
            this.panel5.Controls.Add(this.button1);
            this.panel5.Controls.Add(this.button6);
            this.panel5.Controls.Add(this.cb_autoReadParams);
            this.panel5.Controls.Add(this.cbDisableLogs);
            this.panel5.Controls.Add(this.monitorMode);
            this.panel5.Controls.Add(this.label25);
            this.panel5.Controls.Add(this.button5);
            this.panel5.Controls.Add(this.button3);
            this.panel5.Controls.Add(this.label28);
            this.panel5.Controls.Add(this.pictureBox8);
            this.panel5.Controls.Add(this.button2);
            this.panel5.ForeColor = System.Drawing.Color.Black;
            this.panel5.Location = new System.Drawing.Point(699, 12);
            this.panel5.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(301, 380);
            this.panel5.TabIndex = 29;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(32, 316);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(105, 15);
            this.label9.TabIndex = 38;
            this.label9.Text = "Send commands";
            // 
            // tbSend
            // 
            this.tbSend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSend.Location = new System.Drawing.Point(36, 334);
            this.tbSend.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.tbSend.Name = "tbSend";
            this.tbSend.Size = new System.Drawing.Size(158, 23);
            this.tbSend.TabIndex = 7;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(49, 126);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(125, 15);
            this.label25.TabIndex = 37;
            this.label25.Text = "interface commands";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Helvetica", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(61, 23);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(76, 15);
            this.label28.TabIndex = 17;
            this.label28.Text = "Commands";
            // 
            // pictureBox8
            // 
            this.pictureBox8.Image = global::Stabilization.Properties.Resources.setting;
            this.pictureBox8.Location = new System.Drawing.Point(18, 13);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(35, 35);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox8.TabIndex = 5;
            this.pictureBox8.TabStop = false;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(249)))), ((int)(((byte)(229)))));
            this.panel10.Controls.Add(this.label12);
            this.panel10.Controls.Add(this.label11);
            this.panel10.Controls.Add(this.label18);
            this.panel10.Controls.Add(this.label26);
            this.panel10.Controls.Add(this.label17);
            this.panel10.Controls.Add(this.pictureBox6);
            this.panel10.Controls.Add(this.nudLimit);
            this.panel10.Controls.Add(this.label16);
            this.panel10.Controls.Add(this.label15);
            this.panel10.Controls.Add(this.nudBaseSpeed);
            this.panel10.ForeColor = System.Drawing.Color.Black;
            this.panel10.Location = new System.Drawing.Point(369, 205);
            this.panel10.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(320, 187);
            this.panel10.TabIndex = 28;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Helvetica", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(30, 141);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(42, 15);
            this.label12.TabIndex = 36;
            this.label12.Text = "Note:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(93, 141);
            this.label11.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(181, 30);
            this.label11.TabIndex = 35;
            this.label11.Text = "output range [1000,2000].\r\nM1,M2 = Throttle +- PID limit.";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Helvetica", 11F, System.Drawing.FontStyle.Bold);
            this.label26.Location = new System.Drawing.Point(61, 23);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(99, 17);
            this.label26.TabIndex = 17;
            this.label26.Text = "PWM LIMITS";
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = global::Stabilization.Properties.Resources.filter;
            this.pictureBox6.Location = new System.Drawing.Point(18, 13);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(35, 35);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox6.TabIndex = 5;
            this.pictureBox6.TabStop = false;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(248)))), ((int)(((byte)(237)))));
            this.panel9.Controls.Add(this.label13);
            this.panel9.Controls.Add(this.label8);
            this.panel9.Controls.Add(this.label24);
            this.panel9.Controls.Add(this.pictureBox5);
            this.panel9.Controls.Add(this.nudKp);
            this.panel9.Controls.Add(this.label6);
            this.panel9.Controls.Add(this.label5);
            this.panel9.Controls.Add(this.label4);
            this.panel9.Controls.Add(this.nudKpd);
            this.panel9.Controls.Add(this.nudKd);
            this.panel9.Controls.Add(this.label1);
            this.panel9.Controls.Add(this.nudKid);
            this.panel9.Controls.Add(this.nudKi);
            this.panel9.Controls.Add(this.label2);
            this.panel9.Controls.Add(this.nudKdd);
            this.panel9.Controls.Add(this.label3);
            this.panel9.ForeColor = System.Drawing.Color.Black;
            this.panel9.Location = new System.Drawing.Point(369, 12);
            this.panel9.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(320, 187);
            this.panel9.TabIndex = 27;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Helvetica", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(19, 157);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(42, 15);
            this.label13.TabIndex = 37;
            this.label13.Text = "Note:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Helvetica", 11F, System.Drawing.FontStyle.Bold);
            this.label24.Location = new System.Drawing.Point(62, 23);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(94, 17);
            this.label24.TabIndex = 17;
            this.label24.Text = "TUNING PID";
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::Stabilization.Properties.Resources.tuning;
            this.pictureBox5.Location = new System.Drawing.Point(18, 13);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(35, 35);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 5;
            this.pictureBox5.TabStop = false;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.label7);
            this.panel8.Controls.Add(this.pictureBox4);
            this.panel8.Controls.Add(this.button7);
            this.panel8.Controls.Add(this.trackBar1);
            this.panel8.Controls.Add(this.label23);
            this.panel8.ForeColor = System.Drawing.Color.Black;
            this.panel8.Location = new System.Drawing.Point(14, 398);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(331, 187);
            this.panel8.TabIndex = 26;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::Stabilization.Properties.Resources.target;
            this.pictureBox4.Location = new System.Drawing.Point(18, 13);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(35, 35);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 6;
            this.pictureBox4.TabStop = false;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Helvetica", 11F, System.Drawing.FontStyle.Bold);
            this.label23.ForeColor = System.Drawing.Color.Green;
            this.label23.Location = new System.Drawing.Point(61, 23);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(82, 17);
            this.label23.TabIndex = 1;
            this.label23.Text = "SETPOINT";
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.pictureBox3);
            this.panel7.Controls.Add(this.label21);
            this.panel7.Controls.Add(this.button4);
            this.panel7.ForeColor = System.Drawing.Color.Black;
            this.panel7.Location = new System.Drawing.Point(14, 201);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(331, 191);
            this.panel7.TabIndex = 25;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Stabilization.Properties.Resources.emergency;
            this.pictureBox3.Location = new System.Drawing.Point(18, 13);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(35, 35);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 6;
            this.pictureBox3.TabStop = false;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Helvetica", 11F, System.Drawing.FontStyle.Bold);
            this.label21.ForeColor = System.Drawing.Color.Red;
            this.label21.Location = new System.Drawing.Point(61, 23);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(131, 17);
            this.label21.TabIndex = 1;
            this.label21.Text = "Emergency Stop";
            // 
            // button4
            // 
            this.button4.BackgroundImage = global::Stabilization.Properties.Resources.emergency_stop;
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.Snow;
            this.button4.Location = new System.Drawing.Point(81, 58);
            this.button4.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(154, 111);
            this.button4.TabIndex = 0;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // info_panel
            // 
            this.info_panel.Controls.Add(this.tabControl1);
            this.info_panel.Dock = System.Windows.Forms.DockStyle.Right;
            this.info_panel.Font = new System.Drawing.Font("Helvetica", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.info_panel.Location = new System.Drawing.Point(520, 0);
            this.info_panel.Name = "info_panel";
            this.info_panel.Padding = new System.Windows.Forms.Padding(3);
            this.info_panel.Size = new System.Drawing.Size(492, 601);
            this.info_panel.TabIndex = 27;
            this.info_panel.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 601);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.info_panel);
            this.Font = new System.Drawing.Font("Helvetica", 9.7F);
            this.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.Name = "Form1";
            this.Text = "Khaled HAMIDI";
            ((System.ComponentModel.ISupportInitialize)(this.nudKpd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKid)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_notconnect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_working)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_ok)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_wrong)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.info_panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btconnect;
        private System.Windows.Forms.MaskedTextBox mtbPortname;
        private System.IO.Ports.SerialPort arduino;
        private System.Windows.Forms.ComboBox cmBuadrite;
        private System.Windows.Forms.Button btdisconnect;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RichTextBox rtbRawData;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox rtbErrors;
        private System.Windows.Forms.NumericUpDown nudKpd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudKdd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudKid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudKd;
        private System.Windows.Forms.NumericUpDown nudKi;
        private System.Windows.Forms.NumericUpDown nudKp;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Timer UIupdater;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.CheckBox monitorMode;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.RichTextBox rtbSendData;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.CheckBox cb_autoReadParams;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.NumericUpDown nudBaseSpeed;
        private System.Windows.Forms.NumericUpDown nudLimit;
        private System.Windows.Forms.RichTextBox rtbInfoData;
        private System.Windows.Forms.RichTextBox rtbError;
        private System.Windows.Forms.CheckBox cbDisableLogs;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button log_unusable;
        private System.Windows.Forms.Button log_usable;
        private System.Windows.Forms.Button log_start;
        private System.Windows.Forms.TextBox loger_name;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel info_panel;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.PictureBox pictureBox9;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tbSend;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox pb_wrong;
        private System.Windows.Forms.PictureBox pb_ok;
        private System.Windows.Forms.PictureBox pictureBox10;
        private System.Windows.Forms.PictureBox pb_working;
        private System.Windows.Forms.PictureBox pb_notconnect;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

