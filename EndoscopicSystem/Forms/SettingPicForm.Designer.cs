namespace EndoscopicSystem.Forms
{
    partial class SettingPicForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSimulate = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txtY = new System.Windows.Forms.TextBox();
            this.txtX = new System.Windows.Forms.TextBox();
            this.labelX = new System.Windows.Forms.Label();
            this.labelY = new System.Windows.Forms.Label();
            this.btnSelect = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.txtPosition = new System.Windows.Forms.TextBox();
            this.txtAspectRatio = new System.Windows.Forms.TextBox();
            this.txtCrpY = new System.Windows.Forms.TextBox();
            this.txtCrpX = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txbW = new System.Windows.Forms.TextBox();
            this.txbH = new System.Windows.Forms.TextBox();
            this.txbProfileSetting = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbbInstrument1 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.SerialNumber1 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(9, 7);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(260, 26);
            this.label2.TabIndex = 16;
            this.label2.Text = "Setting Capture Screen";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSave.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSave.Location = new System.Drawing.Point(1330, 693);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(96, 52);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Image Example";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(752, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Result Image";
            // 
            // btnSimulate
            // 
            this.btnSimulate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSimulate.Location = new System.Drawing.Point(460, 108);
            this.btnSimulate.Name = "btnSimulate";
            this.btnSimulate.Size = new System.Drawing.Size(96, 50);
            this.btnSimulate.TabIndex = 24;
            this.btnSimulate.Text = "Crop";
            this.btnSimulate.UseVisualStyleBackColor = true;
            this.btnSimulate.Click += new System.EventHandler(this.btnSimulate_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(148, 724);
            this.txtY.Margin = new System.Windows.Forms.Padding(2);
            this.txtY.Name = "txtY";
            this.txtY.ReadOnly = true;
            this.txtY.Size = new System.Drawing.Size(62, 20);
            this.txtY.TabIndex = 25;
            this.txtY.Visible = false;
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(56, 724);
            this.txtX.Margin = new System.Windows.Forms.Padding(2);
            this.txtX.Name = "txtX";
            this.txtX.ReadOnly = true;
            this.txtX.Size = new System.Drawing.Size(62, 20);
            this.txtX.TabIndex = 26;
            this.txtX.Visible = false;
            // 
            // labelX
            // 
            this.labelX.AutoSize = true;
            this.labelX.Location = new System.Drawing.Point(30, 727);
            this.labelX.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(23, 13);
            this.labelX.TabIndex = 27;
            this.labelX.Text = "X : ";
            this.labelX.Visible = false;
            // 
            // labelY
            // 
            this.labelY.AutoSize = true;
            this.labelY.Location = new System.Drawing.Point(122, 727);
            this.labelY.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelY.Name = "labelY";
            this.labelY.Size = new System.Drawing.Size(23, 13);
            this.labelY.TabIndex = 28;
            this.labelY.Text = "Y : ";
            this.labelY.Visible = false;
            // 
            // btnSelect
            // 
            this.btnSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSelect.Location = new System.Drawing.Point(358, 108);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(96, 50);
            this.btnSelect.TabIndex = 31;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.button1.Location = new System.Drawing.Point(33, 108);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(137, 50);
            this.button1.TabIndex = 33;
            this.button1.Text = "Browse File ...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.White;
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnReset.ForeColor = System.Drawing.Color.Black;
            this.btnReset.Location = new System.Drawing.Point(1228, 694);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(96, 52);
            this.btnReset.TabIndex = 34;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // txtPosition
            // 
            this.txtPosition.Location = new System.Drawing.Point(223, 725);
            this.txtPosition.Margin = new System.Windows.Forms.Padding(2);
            this.txtPosition.Name = "txtPosition";
            this.txtPosition.ReadOnly = true;
            this.txtPosition.Size = new System.Drawing.Size(19, 20);
            this.txtPosition.TabIndex = 35;
            this.txtPosition.Visible = false;
            // 
            // txtAspectRatio
            // 
            this.txtAspectRatio.Location = new System.Drawing.Point(246, 725);
            this.txtAspectRatio.Margin = new System.Windows.Forms.Padding(2);
            this.txtAspectRatio.Name = "txtAspectRatio";
            this.txtAspectRatio.ReadOnly = true;
            this.txtAspectRatio.Size = new System.Drawing.Size(19, 20);
            this.txtAspectRatio.TabIndex = 36;
            this.txtAspectRatio.Visible = false;
            // 
            // txtCrpY
            // 
            this.txtCrpY.Location = new System.Drawing.Point(337, 724);
            this.txtCrpY.Margin = new System.Windows.Forms.Padding(2);
            this.txtCrpY.Name = "txtCrpY";
            this.txtCrpY.ReadOnly = true;
            this.txtCrpY.Size = new System.Drawing.Size(29, 20);
            this.txtCrpY.TabIndex = 38;
            this.txtCrpY.Visible = false;
            // 
            // txtCrpX
            // 
            this.txtCrpX.Location = new System.Drawing.Point(304, 724);
            this.txtCrpX.Margin = new System.Windows.Forms.Padding(2);
            this.txtCrpX.Name = "txtCrpX";
            this.txtCrpX.ReadOnly = true;
            this.txtCrpX.Size = new System.Drawing.Size(29, 20);
            this.txtCrpX.TabIndex = 37;
            this.txtCrpX.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Gainsboro;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(14, 185);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(720, 500);
            this.pictureBox1.TabIndex = 32;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Gainsboro;
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(755, 185);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(720, 500);
            this.pictureBox2.TabIndex = 21;
            this.pictureBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(510, 727);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 42;
            this.label1.Text = "Height : ";
            this.label1.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(389, 727);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 41;
            this.label5.Text = "Width : ";
            this.label5.Visible = false;
            // 
            // txbW
            // 
            this.txbW.Location = new System.Drawing.Point(436, 724);
            this.txbW.Margin = new System.Windows.Forms.Padding(2);
            this.txbW.Name = "txbW";
            this.txbW.ReadOnly = true;
            this.txbW.Size = new System.Drawing.Size(62, 20);
            this.txbW.TabIndex = 40;
            this.txbW.Visible = false;
            // 
            // txbH
            // 
            this.txbH.Location = new System.Drawing.Point(560, 724);
            this.txbH.Margin = new System.Windows.Forms.Padding(2);
            this.txbH.Name = "txbH";
            this.txbH.ReadOnly = true;
            this.txbH.Size = new System.Drawing.Size(62, 20);
            this.txbH.TabIndex = 39;
            this.txbH.Visible = false;
            // 
            // txbProfileSetting
            // 
            this.txbProfileSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txbProfileSetting.Location = new System.Drawing.Point(906, 12);
            this.txbProfileSetting.Name = "txbProfileSetting";
            this.txbProfileSetting.Size = new System.Drawing.Size(383, 26);
            this.txbProfileSetting.TabIndex = 43;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label6.Location = new System.Drawing.Point(752, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(139, 24);
            this.label6.TabIndex = 44;
            this.label6.Text = "Profile Setting";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(885, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 24);
            this.label7.TabIndex = 45;
            this.label7.Text = "*";
            // 
            // cbbInstrument1
            // 
            this.cbbInstrument1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbInstrument1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbInstrument1.FormattingEnabled = true;
            this.cbbInstrument1.Location = new System.Drawing.Point(787, 96);
            this.cbbInstrument1.Margin = new System.Windows.Forms.Padding(2);
            this.cbbInstrument1.Name = "cbbInstrument1";
            this.cbbInstrument1.Size = new System.Drawing.Size(266, 28);
            this.cbbInstrument1.TabIndex = 103;
            this.cbbInstrument1.SelectedIndexChanged += new System.EventHandler(this.cbbInstrument1_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label8.Location = new System.Drawing.Point(1058, 96);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 24);
            this.label8.TabIndex = 102;
            this.label8.Text = "S/n";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label9.Location = new System.Drawing.Point(751, 96);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 24);
            this.label9.TabIndex = 101;
            this.label9.Text = "รุ่น";
            // 
            // SerialNumber1
            // 
            this.SerialNumber1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.SerialNumber1.Location = new System.Drawing.Point(1102, 94);
            this.SerialNumber1.Name = "SerialNumber1";
            this.SerialNumber1.ReadOnly = true;
            this.SerialNumber1.Size = new System.Drawing.Size(286, 26);
            this.SerialNumber1.TabIndex = 100;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label10.Location = new System.Drawing.Point(750, 57);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(122, 25);
            this.label10.TabIndex = 99;
            this.label10.Text = "Instrument";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(868, 57);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 24);
            this.label11.TabIndex = 104;
            this.label11.Text = "*";
            // 
            // SettingPicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(233)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(1484, 755);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cbbInstrument1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.SerialNumber1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txbProfileSetting);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txbW);
            this.Controls.Add(this.txbH);
            this.Controls.Add(this.txtCrpY);
            this.Controls.Add(this.txtCrpX);
            this.Controls.Add(this.txtAspectRatio);
            this.Controls.Add(this.txtPosition);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.labelY);
            this.Controls.Add(this.labelX);
            this.Controls.Add(this.txtX);
            this.Controls.Add(this.txtY);
            this.Controls.Add(this.btnSimulate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SettingPicForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.SettingPicForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSimulate;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.Label labelX;
        private System.Windows.Forms.Label labelY;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.TextBox txtPosition;
        private System.Windows.Forms.TextBox txtAspectRatio;
        private System.Windows.Forms.TextBox txtCrpY;
        private System.Windows.Forms.TextBox txtCrpX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txbW;
        private System.Windows.Forms.TextBox txbH;
        private System.Windows.Forms.TextBox txbProfileSetting;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbbInstrument1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox SerialNumber1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
    }
}