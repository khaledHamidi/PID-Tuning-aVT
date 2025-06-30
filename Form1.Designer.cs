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
            this.btconnect = new System.Windows.Forms.Button();
            this.mtbPortname = new System.Windows.Forms.MaskedTextBox();
            this.arduino = new System.IO.Ports.SerialPort(this.components);
            this.cmBuadrite = new System.Windows.Forms.ComboBox();
            this.btdisconnect = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.rtbRawData = new System.Windows.Forms.RichTextBox();
            this.tbSend = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.rtbErrors = new System.Windows.Forms.RichTextBox();
            this.nudKpd = new System.Windows.Forms.NumericUpDown();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbDisableLogs = new System.Windows.Forms.CheckBox();
            this.cb_autoReadParams = new System.Windows.Forms.CheckBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.monitorMode = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.nudKd = new System.Windows.Forms.NumericUpDown();
            this.nudBaseSpeed = new System.Windows.Forms.NumericUpDown();
            this.nudKi = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.nudLimit = new System.Windows.Forms.NumericUpDown();
            this.nudKp = new System.Windows.Forms.NumericUpDown();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.nudKdd = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
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
            this.panel5 = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.log_usable = new System.Windows.Forms.Button();
            this.log_unusable = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.log_start = new System.Windows.Forms.Button();
            this.loger_name = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKpd)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudKd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKid)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btconnect
            // 
            this.btconnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btconnect.Location = new System.Drawing.Point(273, 3);
            this.btconnect.Name = "btconnect";
            this.btconnect.Size = new System.Drawing.Size(67, 23);
            this.btconnect.TabIndex = 1;
            this.btconnect.Text = "Connect";
            this.btconnect.UseVisualStyleBackColor = true;
            this.btconnect.Click += new System.EventHandler(this.btconnect_Click);
            // 
            // mtbPortname
            // 
            this.mtbPortname.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mtbPortname.Location = new System.Drawing.Point(13, 4);
            this.mtbPortname.Mask = "\\COM0";
            this.mtbPortname.Name = "mtbPortname";
            this.mtbPortname.Size = new System.Drawing.Size(124, 21);
            this.mtbPortname.TabIndex = 1;
            this.mtbPortname.Text = "9";
            // 
            // cmBuadrite
            // 
            this.cmBuadrite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmBuadrite.FormattingEnabled = true;
            this.cmBuadrite.Items.AddRange(new object[] {
            "9600",
            "115200"});
            this.cmBuadrite.Location = new System.Drawing.Point(143, 4);
            this.cmBuadrite.Name = "cmBuadrite";
            this.cmBuadrite.Size = new System.Drawing.Size(124, 21);
            this.cmBuadrite.TabIndex = 2;
            // 
            // btdisconnect
            // 
            this.btdisconnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btdisconnect.Enabled = false;
            this.btdisconnect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btdisconnect.Location = new System.Drawing.Point(346, 3);
            this.btdisconnect.Name = "btdisconnect";
            this.btdisconnect.Size = new System.Drawing.Size(72, 23);
            this.btdisconnect.TabIndex = 3;
            this.btdisconnect.Text = "disconnect";
            this.btdisconnect.UseVisualStyleBackColor = true;
            this.btdisconnect.Click += new System.EventHandler(this.btdisconnect_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.Location = new System.Drawing.Point(424, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 22);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // rtbRawData
            // 
            this.rtbRawData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbRawData.Location = new System.Drawing.Point(0, 0);
            this.rtbRawData.Name = "rtbRawData";
            this.rtbRawData.Size = new System.Drawing.Size(267, 449);
            this.rtbRawData.TabIndex = 5;
            this.rtbRawData.Text = "";
            // 
            // tbSend
            // 
            this.tbSend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSend.Location = new System.Drawing.Point(8, 15);
            this.tbSend.Name = "tbSend";
            this.tbSend.Size = new System.Drawing.Size(289, 21);
            this.tbSend.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.AutoSize = true;
            this.button1.Location = new System.Drawing.Point(303, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rtbErrors
            // 
            this.rtbErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbErrors.Location = new System.Drawing.Point(3, 3);
            this.rtbErrors.Name = "rtbErrors";
            this.rtbErrors.Size = new System.Drawing.Size(261, 443);
            this.rtbErrors.TabIndex = 9;
            this.rtbErrors.Text = "";
            // 
            // nudKpd
            // 
            this.nudKpd.Location = new System.Drawing.Point(101, 19);
            this.nudKpd.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudKpd.Name = "nudKpd";
            this.nudKpd.Size = new System.Drawing.Size(63, 21);
            this.nudKpd.TabIndex = 12;
            this.nudKpd.Tag = "Kpd";
            this.nudKpd.ValueChanged += new System.EventHandler(this.PIDTune);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tbSend);
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Location = new System.Drawing.Point(16, 379);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(395, 41);
            this.groupBox4.TabIndex = 19;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Send commands:";
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.Red;
            this.button4.Location = new System.Drawing.Point(6, 29);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(91, 71);
            this.button4.TabIndex = 0;
            this.button4.Text = "STOP !";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.cbDisableLogs);
            this.panel3.Controls.Add(this.cb_autoReadParams);
            this.panel3.Location = new System.Drawing.Point(13, 33);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(431, 29);
            this.panel3.TabIndex = 18;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // cbDisableLogs
            // 
            this.cbDisableLogs.AutoSize = true;
            this.cbDisableLogs.Checked = true;
            this.cbDisableLogs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDisableLogs.Location = new System.Drawing.Point(13, 3);
            this.cbDisableLogs.Name = "cbDisableLogs";
            this.cbDisableLogs.Size = new System.Drawing.Size(82, 17);
            this.cbDisableLogs.TabIndex = 19;
            this.cbDisableLogs.Text = "Disable logs";
            this.cbDisableLogs.UseVisualStyleBackColor = true;
            // 
            // cb_autoReadParams
            // 
            this.cb_autoReadParams.AutoSize = true;
            this.cb_autoReadParams.Checked = true;
            this.cb_autoReadParams.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_autoReadParams.Location = new System.Drawing.Point(109, 3);
            this.cb_autoReadParams.Name = "cb_autoReadParams";
            this.cb_autoReadParams.Size = new System.Drawing.Size(112, 17);
            this.cb_autoReadParams.TabIndex = 17;
            this.cb_autoReadParams.Text = "Auto read params";
            this.cb_autoReadParams.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(8, 84);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(134, 23);
            this.button6.TabIndex = 17;
            this.button6.Text = "Update from device";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(8, 55);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(134, 23);
            this.button5.TabIndex = 15;
            this.button5.Text = "Read parameters";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.ReadParams_fun);
            // 
            // monitorMode
            // 
            this.monitorMode.AutoSize = true;
            this.monitorMode.Checked = true;
            this.monitorMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.monitorMode.Location = new System.Drawing.Point(8, 29);
            this.monitorMode.Name = "monitorMode";
            this.monitorMode.Size = new System.Drawing.Size(91, 17);
            this.monitorMode.TabIndex = 14;
            this.monitorMode.Text = "monitor mode";
            this.monitorMode.UseVisualStyleBackColor = true;
            this.monitorMode.CheckedChanged += new System.EventHandler(this.monitorMode_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.button7);
            this.groupBox2.Controls.Add(this.trackBar1);
            this.groupBox2.Location = new System.Drawing.Point(13, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(103, 128);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Setpoint:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Blue;
            this.label7.Location = new System.Drawing.Point(57, 58);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 16);
            this.label7.TabIndex = 1;
            this.label7.Text = "00°";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(7, 103);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(89, 23);
            this.button7.TabIndex = 18;
            this.button7.Text = "Reset";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(19, 19);
            this.trackBar1.Maximum = 90;
            this.trackBar1.Minimum = -90;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar1.Size = new System.Drawing.Size(32, 90);
            this.trackBar1.TabIndex = 0;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.nudKd);
            this.groupBox1.Controls.Add(this.nudBaseSpeed);
            this.groupBox1.Controls.Add(this.nudKi);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.nudLimit);
            this.groupBox1.Controls.Add(this.nudKp);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.nudKdd);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.nudKid);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.nudKpd);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(16, 142);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(395, 231);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tunning";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label8.Location = new System.Drawing.Point(181, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(155, 13);
            this.label8.TabIndex = 35;
            this.label8.Text = "Zeros on the left are not taken";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label18.Location = new System.Drawing.Point(227, 150);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(75, 13);
            this.label18.TabIndex = 34;
            this.label18.Text = "default : 1500";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label17.Location = new System.Drawing.Point(215, 126);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(116, 13);
            this.label17.TabIndex = 33;
            this.label17.Text = "~ range : [1000,2000]";
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.AutoSize = true;
            this.button3.Location = new System.Drawing.Point(300, 183);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(78, 42);
            this.button3.TabIndex = 21;
            this.button3.Text = "backup local";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(13, 150);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(47, 13);
            this.label16.TabIndex = 32;
            this.label16.Text = "PID out:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(26, 126);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(34, 13);
            this.label15.TabIndex = 31;
            this.label15.Text = "Base:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(75, 143);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(14, 16);
            this.label12.TabIndex = 29;
            this.label12.Text = "+";
            // 
            // nudKd
            // 
            this.nudKd.Location = new System.Drawing.Point(51, 71);
            this.nudKd.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudKd.Name = "nudKd";
            this.nudKd.Size = new System.Drawing.Size(38, 21);
            this.nudKd.TabIndex = 27;
            this.nudKd.Tag = "Kd";
            this.nudKd.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.nudKd.ValueChanged += new System.EventHandler(this.PIDTune);
            // 
            // nudBaseSpeed
            // 
            this.nudBaseSpeed.Location = new System.Drawing.Point(93, 124);
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
            this.nudBaseSpeed.Size = new System.Drawing.Size(106, 21);
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
            this.nudKi.Location = new System.Drawing.Point(51, 45);
            this.nudKi.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudKi.Name = "nudKi";
            this.nudKi.Size = new System.Drawing.Size(38, 21);
            this.nudKi.TabIndex = 26;
            this.nudKi.Tag = "Ki";
            this.nudKi.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.nudKi.ValueChanged += new System.EventHandler(this.PIDTune);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(13, 105);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 13);
            this.label11.TabIndex = 27;
            this.label11.Text = "PWM limit:";
            // 
            // nudLimit
            // 
            this.nudLimit.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudLimit.Location = new System.Drawing.Point(93, 148);
            this.nudLimit.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudLimit.Name = "nudLimit";
            this.nudLimit.Size = new System.Drawing.Size(106, 21);
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
            this.nudKp.Location = new System.Drawing.Point(51, 19);
            this.nudKp.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudKp.Name = "nudKp";
            this.nudKp.Size = new System.Drawing.Size(38, 21);
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
            this.button2.Location = new System.Drawing.Point(16, 183);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(283, 42);
            this.button2.TabIndex = 14;
            this.button2.Text = "update memory ";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 16);
            this.label3.TabIndex = 20;
            this.label3.Tag = "Kd";
            this.label3.Text = "Kp";
            // 
            // nudKdd
            // 
            this.nudKdd.Location = new System.Drawing.Point(101, 71);
            this.nudKdd.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudKdd.Name = "nudKdd";
            this.nudKdd.Size = new System.Drawing.Size(63, 21);
            this.nudKdd.TabIndex = 19;
            this.nudKdd.Tag = "Kdd";
            this.nudKdd.ValueChanged += new System.EventHandler(this.PIDTune);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 16);
            this.label2.TabIndex = 17;
            this.label2.Text = "Ki";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(76, 152);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(11, 16);
            this.label13.TabIndex = 30;
            this.label13.Text = "-";
            // 
            // nudKid
            // 
            this.nudKid.Location = new System.Drawing.Point(101, 45);
            this.nudKid.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudKid.Name = "nudKid";
            this.nudKid.Size = new System.Drawing.Size(63, 21);
            this.nudKid.TabIndex = 16;
            this.nudKid.Tag = "Kid";
            this.nudKid.ValueChanged += new System.EventHandler(this.PIDTune);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 16);
            this.label1.TabIndex = 14;
            this.label1.Text = "Kp";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(90, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(10, 16);
            this.label4.TabIndex = 24;
            this.label4.Tag = "Kd";
            this.label4.Text = ".";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(90, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(10, 16);
            this.label5.TabIndex = 23;
            this.label5.Text = ".";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(90, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(10, 16);
            this.label6.TabIndex = 22;
            this.label6.Text = ".";
            // 
            // UIupdater
            // 
            this.UIupdater.Interval = 10;
            // 
            // rtbSendData
            // 
            this.rtbSendData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbSendData.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbSendData.Location = new System.Drawing.Point(3, 3);
            this.rtbSendData.Name = "rtbSendData";
            this.rtbSendData.Size = new System.Drawing.Size(261, 443);
            this.rtbSendData.TabIndex = 16;
            this.rtbSendData.Text = "";
            // 
            // rtbInfoData
            // 
            this.rtbInfoData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbInfoData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbInfoData.Location = new System.Drawing.Point(3, 3);
            this.rtbInfoData.Name = "rtbInfoData";
            this.rtbInfoData.Size = new System.Drawing.Size(261, 469);
            this.rtbInfoData.TabIndex = 20;
            this.rtbInfoData.Text = "";
            // 
            // rtbError
            // 
            this.rtbError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbError.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbError.Location = new System.Drawing.Point(0, 0);
            this.rtbError.Name = "rtbError";
            this.rtbError.Size = new System.Drawing.Size(267, 449);
            this.rtbError.TabIndex = 21;
            this.rtbError.Text = "";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel4.Controls.Add(this.pictureBox1);
            this.panel4.Controls.Add(this.btdisconnect);
            this.panel4.Controls.Add(this.btconnect);
            this.panel4.Controls.Add(this.mtbPortname);
            this.panel4.Controls.Add(this.cmBuadrite);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(737, 27);
            this.panel4.TabIndex = 23;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Controls.Add(this.groupBox4);
            this.panel5.Controls.Add(this.groupBox5);
            this.panel5.Controls.Add(this.groupBox3);
            this.panel5.Controls.Add(this.groupBox2);
            this.panel5.Controls.Add(this.groupBox1);
            this.panel5.Location = new System.Drawing.Point(12, 68);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(431, 436);
            this.panel5.TabIndex = 24;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.monitorMode);
            this.groupBox5.Controls.Add(this.button5);
            this.groupBox5.Controls.Add(this.button6);
            this.groupBox5.Location = new System.Drawing.Point(252, 8);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(153, 126);
            this.groupBox5.TabIndex = 16;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Setpoint:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button4);
            this.groupBox3.ForeColor = System.Drawing.Color.Red;
            this.groupBox3.Location = new System.Drawing.Point(131, 8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(103, 128);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Emergency";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Location = new System.Drawing.Point(450, 33);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(275, 501);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.rtbInfoData);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(267, 475);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Response";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.rtbErrors);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(267, 449);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Plot Points";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.rtbError);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(267, 449);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Errors log";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.rtbRawData);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(267, 449);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Logger";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.rtbSendData);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(267, 449);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Send";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.loger_name);
            this.panel1.Controls.Add(this.log_start);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.log_unusable);
            this.panel1.Controls.Add(this.log_usable);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 512);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(737, 34);
            this.panel1.TabIndex = 25;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(346, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(178, 21);
            this.textBox1.TabIndex = 0;
            // 
            // log_usable
            // 
            this.log_usable.Enabled = false;
            this.log_usable.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.log_usable.ForeColor = System.Drawing.Color.Green;
            this.log_usable.Location = new System.Drawing.Point(547, 0);
            this.log_usable.Name = "log_usable";
            this.log_usable.Size = new System.Drawing.Size(75, 34);
            this.log_usable.TabIndex = 1;
            this.log_usable.Tag = "accept";
            this.log_usable.Text = "✔";
            this.log_usable.UseVisualStyleBackColor = true;
            this.log_usable.Click += new System.EventHandler(this.logactions);
            // 
            // log_unusable
            // 
            this.log_unusable.Enabled = false;
            this.log_unusable.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.log_unusable.ForeColor = System.Drawing.Color.Red;
            this.log_unusable.Location = new System.Drawing.Point(650, 0);
            this.log_unusable.Name = "log_unusable";
            this.log_unusable.Size = new System.Drawing.Size(75, 34);
            this.log_unusable.TabIndex = 2;
            this.log_unusable.Tag = "unusable";
            this.log_unusable.Text = "✖";
            this.log_unusable.UseVisualStyleBackColor = true;
            this.log_unusable.Click += new System.EventHandler(this.logactions);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(12, 10);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 16);
            this.label9.TabIndex = 15;
            this.label9.Text = "logger:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(306, 13);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(34, 13);
            this.label10.TabIndex = 33;
            this.label10.Text = "Note:";
            // 
            // log_start
            // 
            this.log_start.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.log_start.ForeColor = System.Drawing.Color.Blue;
            this.log_start.Location = new System.Drawing.Point(217, 0);
            this.log_start.Name = "log_start";
            this.log_start.Size = new System.Drawing.Size(74, 34);
            this.log_start.TabIndex = 34;
            this.log_start.Tag = "create";
            this.log_start.Text = "➕";
            this.log_start.UseVisualStyleBackColor = true;
            this.log_start.Click += new System.EventHandler(this.logactions);
            // 
            // loger_name
            // 
            this.loger_name.Location = new System.Drawing.Point(117, 9);
            this.loger_name.Name = "loger_name";
            this.loger_name.Size = new System.Drawing.Size(94, 21);
            this.loger_name.TabIndex = 35;
            this.loger_name.Text = "experimentData";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(73, 14);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(38, 13);
            this.label14.TabIndex = 36;
            this.label14.Text = "Name:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 546);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel4);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Form1";
            this.Text = "Khaled HAMIDI";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKpd)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudKd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKid)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.TextBox tbSend;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox rtbErrors;
        private System.Windows.Forms.NumericUpDown nudKpd;
        private System.Windows.Forms.GroupBox groupBox1;
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
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Timer UIupdater;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.CheckBox monitorMode;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.RichTextBox rtbSendData;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox cb_autoReadParams;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.NumericUpDown nudBaseSpeed;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown nudLimit;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.RichTextBox rtbInfoData;
        private System.Windows.Forms.RichTextBox rtbError;
        private System.Windows.Forms.CheckBox cbDisableLogs;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button log_unusable;
        private System.Windows.Forms.Button log_usable;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button log_start;
        private System.Windows.Forms.TextBox loger_name;
        private System.Windows.Forms.Label label14;
    }
}

