
namespace EndoscopicSystem.V2.Forms
{
    partial class FormLive
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
            this.videoSourcePlayer = new AForge.Controls.VideoSourcePlayer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txbDoctor = new System.Windows.Forms.TextBox();
            this.txbSex = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txbHN = new System.Windows.Forms.TextBox();
            this.txbSymptom = new System.Windows.Forms.TextBox();
            this.txbAge = new System.Windows.Forms.TextBox();
            this.txbPatientFullName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbbProcedureList = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.connectButton = new System.Windows.Forms.Button();
            this.devicesCombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbTime = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCapture = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.pictureBoxRecording = new System.Windows.Forms.PictureBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnRecord = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.pictureBoxSnapshot = new System.Windows.Forms.PictureBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRecording)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSnapshot)).BeginInit();
            this.SuspendLayout();
            // 
            // videoSourcePlayer
            // 
            this.videoSourcePlayer.BackColor = System.Drawing.Color.Black;
            this.videoSourcePlayer.Location = new System.Drawing.Point(11, 118);
            this.videoSourcePlayer.Margin = new System.Windows.Forms.Padding(2);
            this.videoSourcePlayer.Name = "videoSourcePlayer";
            this.videoSourcePlayer.Size = new System.Drawing.Size(1080, 750);
            this.videoSourcePlayer.TabIndex = 86;
            this.videoSourcePlayer.Text = "videoSourcePlayer";
            this.videoSourcePlayer.VideoSource = null;
            this.videoSourcePlayer.NewFrame += new AForge.Controls.VideoSourcePlayer.NewFrameHandler(this.videoSourcePlayer_NewFrame);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txbDoctor);
            this.groupBox1.Controls.Add(this.txbSex);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txbHN);
            this.groupBox1.Controls.Add(this.txbSymptom);
            this.groupBox1.Controls.Add(this.txbAge);
            this.groupBox1.Controls.Add(this.txbPatientFullName);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbbProcedureList);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(965, 100);
            this.groupBox1.TabIndex = 87;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Patient Information";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(2, 87);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 16);
            this.label9.TabIndex = 131;
            this.label9.Text = "Live";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label8.Location = new System.Drawing.Point(407, 55);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 16);
            this.label8.TabIndex = 129;
            this.label8.Text = "Doctor Name :";
            // 
            // txbDoctor
            // 
            this.txbDoctor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txbDoctor.Location = new System.Drawing.Point(518, 52);
            this.txbDoctor.Margin = new System.Windows.Forms.Padding(2);
            this.txbDoctor.MaxLength = 250;
            this.txbDoctor.Name = "txbDoctor";
            this.txbDoctor.ReadOnly = true;
            this.txbDoctor.Size = new System.Drawing.Size(218, 23);
            this.txbDoctor.TabIndex = 130;
            // 
            // txbSex
            // 
            this.txbSex.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txbSex.Location = new System.Drawing.Point(648, 22);
            this.txbSex.Margin = new System.Windows.Forms.Padding(2);
            this.txbSex.MaxLength = 255;
            this.txbSex.Name = "txbSex";
            this.txbSex.ReadOnly = true;
            this.txbSex.Size = new System.Drawing.Size(88, 23);
            this.txbSex.TabIndex = 128;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label7.Location = new System.Drawing.Point(597, 25);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 16);
            this.label7.TabIndex = 127;
            this.label7.Text = "Sex :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label12.Location = new System.Drawing.Point(11, 55);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 16);
            this.label12.TabIndex = 117;
            this.label12.Text = "Symptom :";
            // 
            // txbHN
            // 
            this.txbHN.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txbHN.Location = new System.Drawing.Point(95, 22);
            this.txbHN.Margin = new System.Windows.Forms.Padding(2);
            this.txbHN.MaxLength = 255;
            this.txbHN.Name = "txbHN";
            this.txbHN.Size = new System.Drawing.Size(172, 23);
            this.txbHN.TabIndex = 121;
            this.txbHN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtHN_KeyDown);
            // 
            // txbSymptom
            // 
            this.txbSymptom.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txbSymptom.Location = new System.Drawing.Point(95, 52);
            this.txbSymptom.Margin = new System.Windows.Forms.Padding(2);
            this.txbSymptom.MaxLength = 250;
            this.txbSymptom.Name = "txbSymptom";
            this.txbSymptom.ReadOnly = true;
            this.txbSymptom.Size = new System.Drawing.Size(172, 23);
            this.txbSymptom.TabIndex = 124;
            // 
            // txbAge
            // 
            this.txbAge.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txbAge.Location = new System.Drawing.Point(344, 52);
            this.txbAge.Margin = new System.Windows.Forms.Padding(2);
            this.txbAge.MaxLength = 255;
            this.txbAge.Name = "txbAge";
            this.txbAge.ReadOnly = true;
            this.txbAge.Size = new System.Drawing.Size(54, 23);
            this.txbAge.TabIndex = 123;
            // 
            // txbPatientFullName
            // 
            this.txbPatientFullName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txbPatientFullName.Location = new System.Drawing.Point(343, 22);
            this.txbPatientFullName.Margin = new System.Windows.Forms.Padding(2);
            this.txbPatientFullName.MaxLength = 255;
            this.txbPatientFullName.Name = "txbPatientFullName";
            this.txbPatientFullName.ReadOnly = true;
            this.txbPatientFullName.Size = new System.Drawing.Size(227, 23);
            this.txbPatientFullName.TabIndex = 122;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.Location = new System.Drawing.Point(293, 55);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 16);
            this.label5.TabIndex = 120;
            this.label5.Text = "Age :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(53, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 16);
            this.label2.TabIndex = 118;
            this.label2.Text = "HN :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(282, 25);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 16);
            this.label4.TabIndex = 119;
            this.label4.Text = "Name :";
            // 
            // cbbProcedureList
            // 
            this.cbbProcedureList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbProcedureList.Enabled = false;
            this.cbbProcedureList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbProcedureList.FormattingEnabled = true;
            this.cbbProcedureList.Location = new System.Drawing.Point(533, -6);
            this.cbbProcedureList.Margin = new System.Windows.Forms.Padding(2);
            this.cbbProcedureList.Name = "cbbProcedureList";
            this.cbbProcedureList.Size = new System.Drawing.Size(427, 24);
            this.cbbProcedureList.TabIndex = 126;
            this.cbbProcedureList.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label6.Location = new System.Drawing.Point(415, -4);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 17);
            this.label6.TabIndex = 125;
            this.label6.Text = "Procedure List";
            this.label6.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.disconnectButton);
            this.groupBox2.Controls.Add(this.connectButton);
            this.groupBox2.Controls.Add(this.devicesCombo);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox2.Location = new System.Drawing.Point(984, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(875, 100);
            this.groupBox2.TabIndex = 88;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Devices";
            // 
            // disconnectButton
            // 
            this.disconnectButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.disconnectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.disconnectButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.disconnectButton.Location = new System.Drawing.Point(300, 51);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(101, 43);
            this.disconnectButton.TabIndex = 88;
            this.disconnectButton.Text = "Disconnect";
            this.disconnectButton.UseVisualStyleBackColor = false;
            this.disconnectButton.Click += new System.EventHandler(this.disconnectButton_Click);
            // 
            // connectButton
            // 
            this.connectButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.connectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.connectButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.connectButton.Location = new System.Drawing.Point(193, 51);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(101, 43);
            this.connectButton.TabIndex = 87;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = false;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // devicesCombo
            // 
            this.devicesCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.devicesCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.devicesCombo.FormattingEnabled = true;
            this.devicesCombo.Location = new System.Drawing.Point(6, 21);
            this.devicesCombo.Name = "devicesCombo";
            this.devicesCombo.Size = new System.Drawing.Size(395, 24);
            this.devicesCombo.TabIndex = 86;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(75, 936);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 20);
            this.label1.TabIndex = 90;
            this.label1.Text = "Video Record :";
            this.label1.Visible = false;
            // 
            // lbTime
            // 
            this.lbTime.AutoSize = true;
            this.lbTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbTime.ForeColor = System.Drawing.Color.Red;
            this.lbTime.Location = new System.Drawing.Point(214, 936);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(79, 20);
            this.lbTime.TabIndex = 91;
            this.lbTime.Text = "00:00:00";
            this.lbTime.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(294, 936);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 20);
            this.label3.TabIndex = 92;
            this.label3.Text = "นาที";
            this.label3.Visible = false;
            // 
            // btnCapture
            // 
            this.btnCapture.BackColor = System.Drawing.Color.Gainsboro;
            this.btnCapture.Enabled = false;
            this.btnCapture.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnCapture.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCapture.Location = new System.Drawing.Point(868, 898);
            this.btnCapture.Margin = new System.Windows.Forms.Padding(2);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(226, 88);
            this.btnCapture.TabIndex = 93;
            this.btnCapture.Text = "Capture";
            this.btnCapture.UseVisualStyleBackColor = false;
            this.btnCapture.EnabledChanged += new System.EventHandler(this.btn_EnabledChanged);
            this.btnCapture.Click += new System.EventHandler(this.CaptureButton_Click);
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnNext.ForeColor = System.Drawing.Color.Blue;
            this.btnNext.Location = new System.Drawing.Point(1207, 898);
            this.btnNext.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(135, 85);
            this.btnNext.TabIndex = 94;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // pictureBoxRecording
            // 
            this.pictureBoxRecording.BackColor = System.Drawing.Color.Black;
            this.pictureBoxRecording.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBoxRecording.Image = global::EndoscopicSystem.Properties.Resources.led_blinksred_mini;
            this.pictureBoxRecording.Location = new System.Drawing.Point(1134, 134);
            this.pictureBoxRecording.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxRecording.Name = "pictureBoxRecording";
            this.pictureBoxRecording.Size = new System.Drawing.Size(33, 29);
            this.pictureBoxRecording.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxRecording.TabIndex = 112;
            this.pictureBoxRecording.TabStop = false;
            this.pictureBoxRecording.Visible = false;
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.Gainsboro;
            this.btnStop.Enabled = false;
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.btnStop.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnStop.Location = new System.Drawing.Point(707, 898);
            this.btnStop.Margin = new System.Windows.Forms.Padding(2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(157, 88);
            this.btnStop.TabIndex = 114;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.EnabledChanged += new System.EventHandler(this.btn_EnabledChanged);
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnRecord
            // 
            this.btnRecord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnRecord.Enabled = false;
            this.btnRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.btnRecord.Location = new System.Drawing.Point(385, 898);
            this.btnRecord.Margin = new System.Windows.Forms.Padding(2);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(157, 88);
            this.btnRecord.TabIndex = 113;
            this.btnRecord.Text = "Record";
            this.btnRecord.UseVisualStyleBackColor = false;
            this.btnRecord.EnabledChanged += new System.EventHandler(this.btn_EnabledChanged);
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // btnPause
            // 
            this.btnPause.BackColor = System.Drawing.Color.Gainsboro;
            this.btnPause.Enabled = false;
            this.btnPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.btnPause.Location = new System.Drawing.Point(546, 898);
            this.btnPause.Margin = new System.Windows.Forms.Padding(2);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(157, 88);
            this.btnPause.TabIndex = 115;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = false;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // pictureBoxSnapshot
            // 
            this.pictureBoxSnapshot.BackColor = System.Drawing.Color.Silver;
            this.pictureBoxSnapshot.Location = new System.Drawing.Point(18, 118);
            this.pictureBoxSnapshot.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxSnapshot.Name = "pictureBoxSnapshot";
            this.pictureBoxSnapshot.Size = new System.Drawing.Size(1080, 750);
            this.pictureBoxSnapshot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxSnapshot.TabIndex = 116;
            this.pictureBoxSnapshot.TabStop = false;
            this.pictureBoxSnapshot.Visible = false;
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(1096, 119);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(796, 755);
            this.listView1.TabIndex = 117;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // FormLive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1011);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnRecord);
            this.Controls.Add(this.pictureBoxRecording);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnCapture);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.videoSourcePlayer);
            this.Controls.Add(this.pictureBoxSnapshot);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLive";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Live Page";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormLive_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormLive_FormClosed);
            this.Load += new System.EventHandler(this.FormLive_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRecording)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSnapshot)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AForge.Controls.VideoSourcePlayer videoSourcePlayer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button disconnectButton;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.ComboBox devicesCombo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.PictureBox pictureBoxRecording;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.PictureBox pictureBoxSnapshot;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txbHN;
        private System.Windows.Forms.TextBox txbSymptom;
        private System.Windows.Forms.TextBox txbAge;
        private System.Windows.Forms.TextBox txbPatientFullName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbbProcedureList;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.TextBox txbSex;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txbDoctor;
        private System.Windows.Forms.Label label9;
    }
}