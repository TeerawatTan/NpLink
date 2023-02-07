
namespace EndoscopicSystem.V2.Forms
{
    partial class FormPatientConfirm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SerialNumber1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbNurseList = new System.Windows.Forms.Label();
            this.lbDoctorList = new System.Windows.Forms.Label();
            this.lbHn = new System.Windows.Forms.Label();
            this.lbName = new System.Windows.Forms.Label();
            this.cbbInstrument1 = new System.Windows.Forms.ComboBox();
            this.cbbInstrument2 = new System.Windows.Forms.ComboBox();
            this.SerialNumber2 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(249, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Patient Confirmation";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(20, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Name :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(20, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "HN :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(20, 154);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "Doctor :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.Location = new System.Drawing.Point(20, 205);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 25);
            this.label5.TabIndex = 4;
            this.label5.Text = "Nurse :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label6.Location = new System.Drawing.Point(33, 490);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(122, 25);
            this.label6.TabIndex = 5;
            this.label6.Text = "Instrument";
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnOk.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnOk.Location = new System.Drawing.Point(335, 671);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(121, 63);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.button1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button1.Location = new System.Drawing.Point(162, 671);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 63);
            this.button1.TabIndex = 7;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SerialNumber1
            // 
            this.SerialNumber1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.SerialNumber1.Location = new System.Drawing.Point(335, 565);
            this.SerialNumber1.Name = "SerialNumber1";
            this.SerialNumber1.ReadOnly = true;
            this.SerialNumber1.Size = new System.Drawing.Size(286, 26);
            this.SerialNumber1.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label7.Location = new System.Drawing.Point(34, 529);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 24);
            this.label7.TabIndex = 10;
            this.label7.Text = "รุ่น";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label8.Location = new System.Drawing.Point(331, 529);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 24);
            this.label8.TabIndex = 11;
            this.label8.Text = "S/n";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbNurseList);
            this.groupBox1.Controls.Add(this.lbDoctorList);
            this.groupBox1.Controls.Add(this.lbHn);
            this.groupBox1.Controls.Add(this.lbName);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(38, 79);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(563, 384);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // lbNurseList
            // 
            this.lbNurseList.AutoSize = true;
            this.lbNurseList.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbNurseList.Location = new System.Drawing.Point(133, 205);
            this.lbNurseList.Name = "lbNurseList";
            this.lbNurseList.Size = new System.Drawing.Size(48, 25);
            this.lbNurseList.TabIndex = 8;
            this.lbNurseList.Text = "......";
            // 
            // lbDoctorList
            // 
            this.lbDoctorList.AutoSize = true;
            this.lbDoctorList.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbDoctorList.Location = new System.Drawing.Point(133, 154);
            this.lbDoctorList.Name = "lbDoctorList";
            this.lbDoctorList.Size = new System.Drawing.Size(48, 25);
            this.lbDoctorList.TabIndex = 7;
            this.lbDoctorList.Text = "......";
            // 
            // lbHn
            // 
            this.lbHn.AutoSize = true;
            this.lbHn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbHn.Location = new System.Drawing.Point(133, 98);
            this.lbHn.Name = "lbHn";
            this.lbHn.Size = new System.Drawing.Size(48, 25);
            this.lbHn.TabIndex = 6;
            this.lbHn.Text = "......";
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbName.Location = new System.Drawing.Point(133, 50);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(48, 25);
            this.lbName.TabIndex = 5;
            this.lbName.Text = "......";
            // 
            // cbbInstrument1
            // 
            this.cbbInstrument1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbInstrument1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbInstrument1.FormattingEnabled = true;
            this.cbbInstrument1.Location = new System.Drawing.Point(38, 563);
            this.cbbInstrument1.Margin = new System.Windows.Forms.Padding(2);
            this.cbbInstrument1.Name = "cbbInstrument1";
            this.cbbInstrument1.Size = new System.Drawing.Size(266, 28);
            this.cbbInstrument1.TabIndex = 98;
            this.cbbInstrument1.SelectedIndexChanged += new System.EventHandler(this.cbbInstrument1_SelectedIndexChanged);
            // 
            // cbbInstrument2
            // 
            this.cbbInstrument2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbInstrument2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbInstrument2.FormattingEnabled = true;
            this.cbbInstrument2.Location = new System.Drawing.Point(38, 628);
            this.cbbInstrument2.Margin = new System.Windows.Forms.Padding(2);
            this.cbbInstrument2.Name = "cbbInstrument2";
            this.cbbInstrument2.Size = new System.Drawing.Size(266, 28);
            this.cbbInstrument2.TabIndex = 102;
            this.cbbInstrument2.Visible = false;
            this.cbbInstrument2.SelectedIndexChanged += new System.EventHandler(this.cbbInstrument2_SelectedIndexChanged);
            // 
            // SerialNumber2
            // 
            this.SerialNumber2.AutoSize = true;
            this.SerialNumber2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.SerialNumber2.Location = new System.Drawing.Point(331, 594);
            this.SerialNumber2.Name = "SerialNumber2";
            this.SerialNumber2.Size = new System.Drawing.Size(38, 24);
            this.SerialNumber2.TabIndex = 101;
            this.SerialNumber2.Text = "S/n";
            this.SerialNumber2.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label10.Location = new System.Drawing.Point(34, 594);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 24);
            this.label10.TabIndex = 100;
            this.label10.Text = "รุ่น";
            this.label10.Visible = false;
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.textBox3.Location = new System.Drawing.Point(335, 630);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(286, 26);
            this.textBox3.TabIndex = 99;
            this.textBox3.Visible = false;
            // 
            // FormPatientConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 744);
            this.Controls.Add(this.cbbInstrument2);
            this.Controls.Add(this.SerialNumber2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.cbbInstrument1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.SerialNumber1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPatientConfirm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Patient Confirmation Page";
            this.Load += new System.EventHandler(this.FormPatientConfirm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox SerialNumber1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbNurseList;
        private System.Windows.Forms.Label lbDoctorList;
        private System.Windows.Forms.Label lbHn;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.ComboBox cbbInstrument1;
        private System.Windows.Forms.ComboBox cbbInstrument2;
        private System.Windows.Forms.Label SerialNumber2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox3;
    }
}