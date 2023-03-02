namespace EndoscopicSystem
{
    partial class PatientForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cbbSex = new System.Windows.Forms.ComboBox();
            this.txtCID = new System.Windows.Forms.TextBox();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHN = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbbDoctorName = new System.Windows.Forms.ComboBox();
            this.cbbNurseName3 = new System.Windows.Forms.ComboBox();
            this.cbbNurseName2 = new System.Windows.Forms.ComboBox();
            this.cbbNurseName1 = new System.Windows.Forms.ComboBox();
            this.txtStaffName = new System.Windows.Forms.TextBox();
            this.dtOperatingDate = new System.Windows.Forms.DateTimePicker();
            this.dtOperatingTime = new System.Windows.Forms.DateTimePicker();
            this.dtAppointmentTime = new System.Windows.Forms.DateTimePicker();
            this.dtAppointmentDate = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtSymptom = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.gridPatient = new System.Windows.Forms.DataGridView();
            this.pictureBox1 = new AForge.Controls.PictureBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.dtBirthDate = new System.Windows.Forms.DateTimePicker();
            this.cbbFinancial = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label22 = new System.Windows.Forms.Label();
            this.cbbIndication = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txbRefer = new System.Windows.Forms.TextBox();
            this.chkRefer = new System.Windows.Forms.CheckBox();
            this.cbbWard = new System.Windows.Forms.ComboBox();
            this.chkWard = new System.Windows.Forms.CheckBox();
            this.cbbOPD = new System.Windows.Forms.ComboBox();
            this.cbbAnesthesist = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chkOPD = new System.Windows.Forms.CheckBox();
            this.cbbProcedureList = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.chkFollowUpCase = new System.Windows.Forms.CheckBox();
            this.chkNewCase = new System.Windows.Forms.CheckBox();
            this.cbbStation = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txbPreDiag2ID = new System.Windows.Forms.TextBox();
            this.txbPreDiag1ID = new System.Windows.Forms.TextBox();
            this.txbPreDiag2Text = new System.Windows.Forms.TextBox();
            this.label100 = new System.Windows.Forms.Label();
            this.txbPreDiag2Code = new System.Windows.Forms.TextBox();
            this.txbPreDiag1Text = new System.Windows.Forms.TextBox();
            this.label99 = new System.Windows.Forms.Label();
            this.txbPreDiag1Code = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridPatient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbbSex
            // 
            this.cbbSex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbSex.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbSex.FormattingEnabled = true;
            this.cbbSex.Items.AddRange(new object[] {
            "- Select Sex -",
            "Male",
            "FeMale"});
            this.cbbSex.Location = new System.Drawing.Point(102, 116);
            this.cbbSex.Margin = new System.Windows.Forms.Padding(2);
            this.cbbSex.Name = "cbbSex";
            this.cbbSex.Size = new System.Drawing.Size(140, 28);
            this.cbbSex.TabIndex = 5;
            // 
            // txtCID
            // 
            this.txtCID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCID.CausesValidation = false;
            this.txtCID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtCID.Location = new System.Drawing.Point(574, 52);
            this.txtCID.Margin = new System.Windows.Forms.Padding(2);
            this.txtCID.MaxLength = 13;
            this.txtCID.Name = "txtCID";
            this.txtCID.Size = new System.Drawing.Size(384, 26);
            this.txtCID.TabIndex = 4;
            this.txtCID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCID_KeyPress);
            // 
            // txtFullName
            // 
            this.txtFullName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFullName.CausesValidation = false;
            this.txtFullName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtFullName.Location = new System.Drawing.Point(574, 22);
            this.txtFullName.Margin = new System.Windows.Forms.Padding(2);
            this.txtFullName.MaxLength = 255;
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(384, 26);
            this.txtFullName.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(494, 52);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "IDCard :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(478, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "FullName :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.Location = new System.Drawing.Point(49, 119);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "Sex :";
            // 
            // txtAge
            // 
            this.txtAge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAge.CausesValidation = false;
            this.txtAge.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtAge.Location = new System.Drawing.Point(102, 87);
            this.txtAge.Margin = new System.Windows.Forms.Padding(2);
            this.txtAge.MaxLength = 3;
            this.txtAge.Name = "txtAge";
            this.txtAge.ReadOnly = true;
            this.txtAge.Size = new System.Drawing.Size(66, 26);
            this.txtAge.TabIndex = 3;
            this.txtAge.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAge_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(47, 89);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Age :";
            // 
            // txtHN
            // 
            this.txtHN.BackColor = System.Drawing.Color.LightCyan;
            this.txtHN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtHN.Location = new System.Drawing.Point(102, 22);
            this.txtHN.Margin = new System.Windows.Forms.Padding(2);
            this.txtHN.MaxLength = 10;
            this.txtHN.Name = "txtHN";
            this.txtHN.Size = new System.Drawing.Size(313, 26);
            this.txtHN.TabIndex = 1;
            this.txtHN.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtHN_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(54, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "HN :";
            // 
            // cbbDoctorName
            // 
            this.cbbDoctorName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbDoctorName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbDoctorName.FormattingEnabled = true;
            this.cbbDoctorName.Location = new System.Drawing.Point(789, 110);
            this.cbbDoctorName.Margin = new System.Windows.Forms.Padding(2);
            this.cbbDoctorName.Name = "cbbDoctorName";
            this.cbbDoctorName.Size = new System.Drawing.Size(446, 28);
            this.cbbDoctorName.TabIndex = 15;
            // 
            // cbbNurseName3
            // 
            this.cbbNurseName3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbNurseName3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbNurseName3.FormattingEnabled = true;
            this.cbbNurseName3.Location = new System.Drawing.Point(192, 200);
            this.cbbNurseName3.Margin = new System.Windows.Forms.Padding(2);
            this.cbbNurseName3.Name = "cbbNurseName3";
            this.cbbNurseName3.Size = new System.Drawing.Size(440, 28);
            this.cbbNurseName3.TabIndex = 18;
            // 
            // cbbNurseName2
            // 
            this.cbbNurseName2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbNurseName2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbNurseName2.FormattingEnabled = true;
            this.cbbNurseName2.Location = new System.Drawing.Point(192, 171);
            this.cbbNurseName2.Margin = new System.Windows.Forms.Padding(2);
            this.cbbNurseName2.Name = "cbbNurseName2";
            this.cbbNurseName2.Size = new System.Drawing.Size(440, 28);
            this.cbbNurseName2.TabIndex = 17;
            // 
            // cbbNurseName1
            // 
            this.cbbNurseName1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbNurseName1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbNurseName1.FormattingEnabled = true;
            this.cbbNurseName1.Location = new System.Drawing.Point(192, 142);
            this.cbbNurseName1.Margin = new System.Windows.Forms.Padding(2);
            this.cbbNurseName1.Name = "cbbNurseName1";
            this.cbbNurseName1.Size = new System.Drawing.Size(440, 28);
            this.cbbNurseName1.TabIndex = 16;
            // 
            // txtStaffName
            // 
            this.txtStaffName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStaffName.CausesValidation = false;
            this.txtStaffName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtStaffName.Location = new System.Drawing.Point(789, 142);
            this.txtStaffName.Margin = new System.Windows.Forms.Padding(2);
            this.txtStaffName.MaxLength = 255;
            this.txtStaffName.Name = "txtStaffName";
            this.txtStaffName.Size = new System.Drawing.Size(446, 26);
            this.txtStaffName.TabIndex = 19;
            // 
            // dtOperatingDate
            // 
            this.dtOperatingDate.CausesValidation = false;
            this.dtOperatingDate.CustomFormat = "dd MMMM yyyy";
            this.dtOperatingDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dtOperatingDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtOperatingDate.Location = new System.Drawing.Point(192, 47);
            this.dtOperatingDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtOperatingDate.MinDate = new System.DateTime(2022, 9, 18, 0, 0, 0, 0);
            this.dtOperatingDate.Name = "dtOperatingDate";
            this.dtOperatingDate.Size = new System.Drawing.Size(313, 26);
            this.dtOperatingDate.TabIndex = 10;
            this.dtOperatingDate.Value = new System.DateTime(2022, 9, 18, 0, 0, 0, 0);
            // 
            // dtOperatingTime
            // 
            this.dtOperatingTime.CausesValidation = false;
            this.dtOperatingTime.CustomFormat = "HH:mm";
            this.dtOperatingTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dtOperatingTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtOperatingTime.Location = new System.Drawing.Point(527, 47);
            this.dtOperatingTime.Margin = new System.Windows.Forms.Padding(2);
            this.dtOperatingTime.MinDate = new System.DateTime(2022, 9, 18, 16, 56, 37, 0);
            this.dtOperatingTime.Name = "dtOperatingTime";
            this.dtOperatingTime.ShowUpDown = true;
            this.dtOperatingTime.Size = new System.Drawing.Size(93, 26);
            this.dtOperatingTime.TabIndex = 11;
            this.dtOperatingTime.Value = new System.DateTime(2022, 9, 18, 16, 56, 37, 0);
            // 
            // dtAppointmentTime
            // 
            this.dtAppointmentTime.CausesValidation = false;
            this.dtAppointmentTime.CustomFormat = "HH:mm";
            this.dtAppointmentTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dtAppointmentTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtAppointmentTime.Location = new System.Drawing.Point(527, 13);
            this.dtAppointmentTime.Margin = new System.Windows.Forms.Padding(2);
            this.dtAppointmentTime.MinDate = new System.DateTime(2022, 9, 18, 16, 56, 37, 0);
            this.dtAppointmentTime.Name = "dtAppointmentTime";
            this.dtAppointmentTime.ShowUpDown = true;
            this.dtAppointmentTime.Size = new System.Drawing.Size(93, 26);
            this.dtAppointmentTime.TabIndex = 9;
            this.dtAppointmentTime.Value = new System.DateTime(2022, 9, 18, 16, 56, 37, 0);
            // 
            // dtAppointmentDate
            // 
            this.dtAppointmentDate.CausesValidation = false;
            this.dtAppointmentDate.CustomFormat = "dd MMMM yyyy";
            this.dtAppointmentDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dtAppointmentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtAppointmentDate.Location = new System.Drawing.Point(192, 13);
            this.dtAppointmentDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtAppointmentDate.MinDate = new System.DateTime(2022, 9, 18, 0, 0, 0, 0);
            this.dtAppointmentDate.Name = "dtAppointmentDate";
            this.dtAppointmentDate.Size = new System.Drawing.Size(313, 26);
            this.dtAppointmentDate.TabIndex = 8;
            this.dtAppointmentDate.Value = new System.DateTime(2022, 9, 18, 0, 0, 0, 0);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label11.Location = new System.Drawing.Point(712, 113);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(73, 20);
            this.label11.TabIndex = 0;
            this.label11.Text = "Doctor :";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label17.Location = new System.Drawing.Point(726, 145);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(59, 20);
            this.label17.TabIndex = 0;
            this.label17.Text = "Staff :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label8.Location = new System.Drawing.Point(24, 113);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(164, 20);
            this.label8.TabIndex = 0;
            this.label8.Text = "Endoscopic Room :";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label15.Location = new System.Drawing.Point(122, 145);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(66, 20);
            this.label15.TabIndex = 0;
            this.label15.Text = "Nurse :";
            // 
            // txtSymptom
            // 
            this.txtSymptom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSymptom.CausesValidation = false;
            this.txtSymptom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtSymptom.Location = new System.Drawing.Point(192, 81);
            this.txtSymptom.Margin = new System.Windows.Forms.Padding(2);
            this.txtSymptom.MaxLength = 250;
            this.txtSymptom.Name = "txtSymptom";
            this.txtSymptom.Size = new System.Drawing.Size(440, 26);
            this.txtSymptom.TabIndex = 12;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label12.Location = new System.Drawing.Point(95, 83);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(93, 20);
            this.label12.TabIndex = 0;
            this.label12.Text = "Symptom :";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label13.Location = new System.Drawing.Point(47, 52);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(141, 20);
            this.label13.TabIndex = 0;
            this.label13.Text = "Operating Time :";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label14.Location = new System.Drawing.Point(24, 18);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(164, 20);
            this.label14.TabIndex = 0;
            this.label14.Text = "Appointment Time :";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label16.Location = new System.Drawing.Point(4, 11);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(202, 25);
            this.label16.TabIndex = 7;
            this.label16.Text = "Patient Infomation";
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.BackColor = System.Drawing.Color.Red;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnDelete.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDelete.Location = new System.Drawing.Point(538, 747);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(112, 62);
            this.btnDelete.TabIndex = 20;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSave.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSave.Location = new System.Drawing.Point(714, 743);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(128, 66);
            this.btnSave.TabIndex = 18;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnReset.Location = new System.Drawing.Point(410, 747);
            this.btnReset.Margin = new System.Windows.Forms.Padding(2);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(112, 62);
            this.btnReset.TabIndex = 19;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // gridPatient
            // 
            this.gridPatient.AllowUserToAddRows = false;
            this.gridPatient.AllowUserToDeleteRows = false;
            this.gridPatient.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridPatient.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridPatient.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gridPatient.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridPatient.DefaultCellStyle = dataGridViewCellStyle5;
            this.gridPatient.Location = new System.Drawing.Point(9, 620);
            this.gridPatient.Margin = new System.Windows.Forms.Padding(2);
            this.gridPatient.Name = "gridPatient";
            this.gridPatient.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridPatient.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.gridPatient.RowHeadersWidth = 51;
            this.gridPatient.RowTemplate.Height = 24;
            this.gridPatient.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridPatient.Size = new System.Drawing.Size(1248, 117);
            this.gridPatient.TabIndex = 0;
            this.gridPatient.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridPatient_CellDoubleClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = null;
            this.pictureBox1.Location = new System.Drawing.Point(1043, 11);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(173, 177);
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnBrowse.Location = new System.Drawing.Point(1080, 192);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(2);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(103, 35);
            this.btnBrowse.TabIndex = 20;
            this.btnBrowse.Text = "Browse File";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.dtBirthDate);
            this.panel1.Controls.Add(this.cbbFinancial);
            this.panel1.Controls.Add(this.label24);
            this.panel1.Controls.Add(this.label23);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.cbbSex);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtHN);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtCID);
            this.panel1.Controls.Add(this.txtAge);
            this.panel1.Controls.Add(this.txtFullName);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(9, 51);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(982, 161);
            this.panel1.TabIndex = 22;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label19.Location = new System.Drawing.Point(2, 55);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(96, 20);
            this.label19.TabIndex = 108;
            this.label19.Text = "BirthDate :";
            // 
            // dtBirthDate
            // 
            this.dtBirthDate.CausesValidation = false;
            this.dtBirthDate.CustomFormat = "dd MMMM yyyy";
            this.dtBirthDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dtBirthDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtBirthDate.Location = new System.Drawing.Point(102, 52);
            this.dtBirthDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtBirthDate.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtBirthDate.Name = "dtBirthDate";
            this.dtBirthDate.Size = new System.Drawing.Size(313, 29);
            this.dtBirthDate.TabIndex = 109;
            this.dtBirthDate.Value = new System.DateTime(2023, 2, 10, 0, 0, 0, 0);
            this.dtBirthDate.ValueChanged += new System.EventHandler(this.dtBirthDate_ValueChanged);
            // 
            // cbbFinancial
            // 
            this.cbbFinancial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbFinancial.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbFinancial.FormattingEnabled = true;
            this.cbbFinancial.Location = new System.Drawing.Point(574, 82);
            this.cbbFinancial.Margin = new System.Windows.Forms.Padding(2);
            this.cbbFinancial.Name = "cbbFinancial";
            this.cbbFinancial.Size = new System.Drawing.Size(384, 28);
            this.cbbFinancial.TabIndex = 107;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.Transparent;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label24.ForeColor = System.Drawing.Color.Red;
            this.label24.Location = new System.Drawing.Point(557, 25);
            this.label24.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(15, 20);
            this.label24.TabIndex = 107;
            this.label24.Text = "*";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.Transparent;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label23.ForeColor = System.Drawing.Color.Red;
            this.label23.Location = new System.Drawing.Point(87, 25);
            this.label23.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(15, 20);
            this.label23.TabIndex = 107;
            this.label23.Text = "*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label7.Location = new System.Drawing.Point(479, 85);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 20);
            this.label7.TabIndex = 6;
            this.label7.Text = "Financial :";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label22);
            this.panel2.Controls.Add(this.cbbIndication);
            this.panel2.Controls.Add(this.label20);
            this.panel2.Controls.Add(this.txbRefer);
            this.panel2.Controls.Add(this.chkRefer);
            this.panel2.Controls.Add(this.cbbWard);
            this.panel2.Controls.Add(this.chkWard);
            this.panel2.Controls.Add(this.cbbOPD);
            this.panel2.Controls.Add(this.cbbAnesthesist);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.chkOPD);
            this.panel2.Controls.Add(this.cbbProcedureList);
            this.panel2.Controls.Add(this.label18);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.chkFollowUpCase);
            this.panel2.Controls.Add(this.chkNewCase);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.cbbDoctorName);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.cbbStation);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.cbbNurseName3);
            this.panel2.Controls.Add(this.txtSymptom);
            this.panel2.Controls.Add(this.cbbNurseName2);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.cbbNurseName1);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.txtStaffName);
            this.panel2.Controls.Add(this.dtOperatingDate);
            this.panel2.Controls.Add(this.label17);
            this.panel2.Controls.Add(this.dtOperatingTime);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.dtAppointmentTime);
            this.panel2.Controls.Add(this.dtAppointmentDate);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txbPreDiag2ID);
            this.panel2.Controls.Add(this.txbPreDiag1ID);
            this.panel2.Controls.Add(this.txbPreDiag2Text);
            this.panel2.Controls.Add(this.label100);
            this.panel2.Controls.Add(this.txbPreDiag2Code);
            this.panel2.Controls.Add(this.txbPreDiag1Text);
            this.panel2.Controls.Add(this.label99);
            this.panel2.Controls.Add(this.txbPreDiag1Code);
            this.panel2.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel2.Location = new System.Drawing.Point(9, 231);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1248, 370);
            this.panel2.TabIndex = 8;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label22.ForeColor = System.Drawing.Color.Red;
            this.label22.Location = new System.Drawing.Point(770, 84);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(15, 20);
            this.label22.TabIndex = 106;
            this.label22.Text = "*";
            // 
            // cbbIndication
            // 
            this.cbbIndication.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbIndication.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbIndication.FormattingEnabled = true;
            this.cbbIndication.Location = new System.Drawing.Point(192, 263);
            this.cbbIndication.Margin = new System.Windows.Forms.Padding(2);
            this.cbbIndication.Name = "cbbIndication";
            this.cbbIndication.Size = new System.Drawing.Size(440, 28);
            this.cbbIndication.TabIndex = 102;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label20.Location = new System.Drawing.Point(83, 266);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(98, 20);
            this.label20.TabIndex = 101;
            this.label20.Text = "Indication :";
            // 
            // txbRefer
            // 
            this.txbRefer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txbRefer.CausesValidation = false;
            this.txbRefer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txbRefer.Location = new System.Drawing.Point(867, 265);
            this.txbRefer.Margin = new System.Windows.Forms.Padding(2);
            this.txbRefer.MaxLength = 255;
            this.txbRefer.Name = "txbRefer";
            this.txbRefer.Size = new System.Drawing.Size(368, 26);
            this.txbRefer.TabIndex = 97;
            this.txbRefer.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txbRefer_KeyUp);
            // 
            // chkRefer
            // 
            this.chkRefer.AutoSize = true;
            this.chkRefer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkRefer.Location = new System.Drawing.Point(790, 265);
            this.chkRefer.Margin = new System.Windows.Forms.Padding(2);
            this.chkRefer.Name = "chkRefer";
            this.chkRefer.Size = new System.Drawing.Size(73, 24);
            this.chkRefer.TabIndex = 96;
            this.chkRefer.Text = "Refer";
            this.chkRefer.UseVisualStyleBackColor = true;
            // 
            // cbbWard
            // 
            this.cbbWard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbWard.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbWard.FormattingEnabled = true;
            this.cbbWard.Location = new System.Drawing.Point(867, 234);
            this.cbbWard.Margin = new System.Windows.Forms.Padding(2);
            this.cbbWard.Name = "cbbWard";
            this.cbbWard.Size = new System.Drawing.Size(368, 28);
            this.cbbWard.TabIndex = 95;
            this.cbbWard.SelectedIndexChanged += new System.EventHandler(this.cbbWard_SelectedIndexChanged);
            // 
            // chkWard
            // 
            this.chkWard.AutoSize = true;
            this.chkWard.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkWard.Location = new System.Drawing.Point(790, 236);
            this.chkWard.Margin = new System.Windows.Forms.Padding(2);
            this.chkWard.Name = "chkWard";
            this.chkWard.Size = new System.Drawing.Size(70, 24);
            this.chkWard.TabIndex = 94;
            this.chkWard.Text = "Ward";
            this.chkWard.UseVisualStyleBackColor = true;
            // 
            // cbbOPD
            // 
            this.cbbOPD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbOPD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbOPD.FormattingEnabled = true;
            this.cbbOPD.Location = new System.Drawing.Point(867, 204);
            this.cbbOPD.Margin = new System.Windows.Forms.Padding(2);
            this.cbbOPD.Name = "cbbOPD";
            this.cbbOPD.Size = new System.Drawing.Size(368, 28);
            this.cbbOPD.TabIndex = 93;
            this.cbbOPD.SelectedIndexChanged += new System.EventHandler(this.cbbOPD_SelectedIndexChanged);
            // 
            // cbbAnesthesist
            // 
            this.cbbAnesthesist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbAnesthesist.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbAnesthesist.FormattingEnabled = true;
            this.cbbAnesthesist.Location = new System.Drawing.Point(192, 231);
            this.cbbAnesthesist.Margin = new System.Windows.Forms.Padding(2);
            this.cbbAnesthesist.Name = "cbbAnesthesist";
            this.cbbAnesthesist.Size = new System.Drawing.Size(440, 28);
            this.cbbAnesthesist.TabIndex = 92;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label9.Location = new System.Drawing.Point(68, 234);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(114, 20);
            this.label9.TabIndex = 91;
            this.label9.Text = "Anesthesist :";
            // 
            // chkOPD
            // 
            this.chkOPD.AutoSize = true;
            this.chkOPD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkOPD.Location = new System.Drawing.Point(789, 206);
            this.chkOPD.Margin = new System.Windows.Forms.Padding(2);
            this.chkOPD.Name = "chkOPD";
            this.chkOPD.Size = new System.Drawing.Size(65, 24);
            this.chkOPD.TabIndex = 87;
            this.chkOPD.Text = "OPD";
            this.chkOPD.UseVisualStyleBackColor = true;
            // 
            // cbbProcedureList
            // 
            this.cbbProcedureList.BackColor = System.Drawing.Color.Gainsboro;
            this.cbbProcedureList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbProcedureList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbProcedureList.FormattingEnabled = true;
            this.cbbProcedureList.Location = new System.Drawing.Point(789, 80);
            this.cbbProcedureList.Margin = new System.Windows.Forms.Padding(2);
            this.cbbProcedureList.Name = "cbbProcedureList";
            this.cbbProcedureList.Size = new System.Drawing.Size(446, 28);
            this.cbbProcedureList.TabIndex = 85;
            this.cbbProcedureList.SelectedIndexChanged += new System.EventHandler(this.cbbProcedureList_SelectedIndexChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label18.Location = new System.Drawing.Point(650, 84);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(135, 20);
            this.label18.TabIndex = 84;
            this.label18.Text = "Procedure List :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label10.Location = new System.Drawing.Point(726, 179);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 20);
            this.label10.TabIndex = 81;
            this.label10.Text = "Case :";
            // 
            // chkFollowUpCase
            // 
            this.chkFollowUpCase.AutoSize = true;
            this.chkFollowUpCase.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkFollowUpCase.Location = new System.Drawing.Point(924, 178);
            this.chkFollowUpCase.Margin = new System.Windows.Forms.Padding(2);
            this.chkFollowUpCase.Name = "chkFollowUpCase";
            this.chkFollowUpCase.Size = new System.Drawing.Size(150, 24);
            this.chkFollowUpCase.TabIndex = 82;
            this.chkFollowUpCase.Text = "Follow up Case";
            this.chkFollowUpCase.UseVisualStyleBackColor = true;
            // 
            // chkNewCase
            // 
            this.chkNewCase.AutoSize = true;
            this.chkNewCase.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkNewCase.Location = new System.Drawing.Point(790, 178);
            this.chkNewCase.Margin = new System.Windows.Forms.Padding(2);
            this.chkNewCase.Name = "chkNewCase";
            this.chkNewCase.Size = new System.Drawing.Size(108, 24);
            this.chkNewCase.TabIndex = 83;
            this.chkNewCase.Text = "New Case";
            this.chkNewCase.UseVisualStyleBackColor = true;
            // 
            // cbbStation
            // 
            this.cbbStation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbStation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbStation.FormattingEnabled = true;
            this.cbbStation.Location = new System.Drawing.Point(192, 110);
            this.cbbStation.Margin = new System.Windows.Forms.Padding(2);
            this.cbbStation.Name = "cbbStation";
            this.cbbStation.Size = new System.Drawing.Size(440, 28);
            this.cbbStation.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(492, 83);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 20);
            this.label6.TabIndex = 90;
            this.label6.Text = "*";
            // 
            // txbPreDiag2ID
            // 
            this.txbPreDiag2ID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbPreDiag2ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txbPreDiag2ID.Location = new System.Drawing.Point(192, 322);
            this.txbPreDiag2ID.Margin = new System.Windows.Forms.Padding(2);
            this.txbPreDiag2ID.MaxLength = 200;
            this.txbPreDiag2ID.Name = "txbPreDiag2ID";
            this.txbPreDiag2ID.Size = new System.Drawing.Size(38, 23);
            this.txbPreDiag2ID.TabIndex = 140;
            this.txbPreDiag2ID.Visible = false;
            // 
            // txbPreDiag1ID
            // 
            this.txbPreDiag1ID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbPreDiag1ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txbPreDiag1ID.Location = new System.Drawing.Point(192, 295);
            this.txbPreDiag1ID.Margin = new System.Windows.Forms.Padding(2);
            this.txbPreDiag1ID.MaxLength = 200;
            this.txbPreDiag1ID.Name = "txbPreDiag1ID";
            this.txbPreDiag1ID.Size = new System.Drawing.Size(38, 23);
            this.txbPreDiag1ID.TabIndex = 139;
            this.txbPreDiag1ID.Visible = false;
            // 
            // txbPreDiag2Text
            // 
            this.txbPreDiag2Text.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbPreDiag2Text.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txbPreDiag2Text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txbPreDiag2Text.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txbPreDiag2Text.Location = new System.Drawing.Point(362, 322);
            this.txbPreDiag2Text.Margin = new System.Windows.Forms.Padding(2);
            this.txbPreDiag2Text.MaxLength = 200;
            this.txbPreDiag2Text.Name = "txbPreDiag2Text";
            this.txbPreDiag2Text.Size = new System.Drawing.Size(501, 23);
            this.txbPreDiag2Text.TabIndex = 138;
            // 
            // label100
            // 
            this.label100.AutoSize = true;
            this.label100.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label100.Location = new System.Drawing.Point(19, 325);
            this.label100.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label100.Name = "label100";
            this.label100.Size = new System.Drawing.Size(169, 20);
            this.label100.TabIndex = 137;
            this.label100.Text = "Pre-Diagnosis (Dx2)";
            // 
            // txbPreDiag2Code
            // 
            this.txbPreDiag2Code.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbPreDiag2Code.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txbPreDiag2Code.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txbPreDiag2Code.BackColor = System.Drawing.SystemColors.Window;
            this.txbPreDiag2Code.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txbPreDiag2Code.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txbPreDiag2Code.Location = new System.Drawing.Point(192, 322);
            this.txbPreDiag2Code.Margin = new System.Windows.Forms.Padding(2);
            this.txbPreDiag2Code.MaxLength = 200;
            this.txbPreDiag2Code.Name = "txbPreDiag2Code";
            this.txbPreDiag2Code.Size = new System.Drawing.Size(166, 23);
            this.txbPreDiag2Code.TabIndex = 136;
            this.txbPreDiag2Code.TextChanged += new System.EventHandler(this.txbPreDiag2Code_TextChanged);
            // 
            // txbPreDiag1Text
            // 
            this.txbPreDiag1Text.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbPreDiag1Text.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txbPreDiag1Text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txbPreDiag1Text.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txbPreDiag1Text.Location = new System.Drawing.Point(362, 295);
            this.txbPreDiag1Text.Margin = new System.Windows.Forms.Padding(2);
            this.txbPreDiag1Text.MaxLength = 200;
            this.txbPreDiag1Text.Name = "txbPreDiag1Text";
            this.txbPreDiag1Text.Size = new System.Drawing.Size(501, 23);
            this.txbPreDiag1Text.TabIndex = 135;
            // 
            // label99
            // 
            this.label99.AutoSize = true;
            this.label99.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label99.Location = new System.Drawing.Point(19, 298);
            this.label99.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(169, 20);
            this.label99.TabIndex = 134;
            this.label99.Text = "Pre-Diagnosis (Dx1)";
            // 
            // txbPreDiag1Code
            // 
            this.txbPreDiag1Code.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbPreDiag1Code.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txbPreDiag1Code.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txbPreDiag1Code.BackColor = System.Drawing.SystemColors.Window;
            this.txbPreDiag1Code.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txbPreDiag1Code.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txbPreDiag1Code.Location = new System.Drawing.Point(192, 295);
            this.txbPreDiag1Code.Margin = new System.Windows.Forms.Padding(2);
            this.txbPreDiag1Code.MaxLength = 200;
            this.txbPreDiag1Code.Name = "txbPreDiag1Code";
            this.txbPreDiag1Code.Size = new System.Drawing.Size(166, 23);
            this.txbPreDiag1Code.TabIndex = 133;
            this.txbPreDiag1Code.TextChanged += new System.EventHandler(this.txbPreDiag1Code_TextChanged);
            // 
            // PatientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(1266, 820);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.gridPatient);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.btnBrowse);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "PatientForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Patient Page";
            this.Load += new System.EventHandler(this.PatientForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridPatient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtCID;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtHN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView gridPatient;
        private System.Windows.Forms.TextBox txtStaffName;
        private System.Windows.Forms.DateTimePicker dtOperatingDate;
        private System.Windows.Forms.DateTimePicker dtAppointmentDate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtSymptom;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.ComboBox cbbSex;
        private System.Windows.Forms.DateTimePicker dtAppointmentTime;
        private System.Windows.Forms.DateTimePicker dtOperatingTime;
        private System.Windows.Forms.ComboBox cbbDoctorName;
        private System.Windows.Forms.ComboBox cbbNurseName3;
        private System.Windows.Forms.ComboBox cbbNurseName2;
        private System.Windows.Forms.ComboBox cbbNurseName1;
        private AForge.Controls.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chkFollowUpCase;
        private System.Windows.Forms.CheckBox chkNewCase;
        private System.Windows.Forms.ComboBox cbbProcedureList;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.CheckBox chkOPD;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbbAnesthesist;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbbIndication;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txbRefer;
        private System.Windows.Forms.CheckBox chkRefer;
        private System.Windows.Forms.ComboBox cbbWard;
        private System.Windows.Forms.CheckBox chkWard;
        private System.Windows.Forms.ComboBox cbbOPD;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.ComboBox cbbFinancial;
        private System.Windows.Forms.ComboBox cbbStation;
        private System.Windows.Forms.TextBox txbPreDiag2ID;
        private System.Windows.Forms.TextBox txbPreDiag1ID;
        private System.Windows.Forms.TextBox txbPreDiag2Text;
        private System.Windows.Forms.Label label100;
        private System.Windows.Forms.TextBox txbPreDiag2Code;
        private System.Windows.Forms.TextBox txbPreDiag1Text;
        private System.Windows.Forms.Label label99;
        private System.Windows.Forms.TextBox txbPreDiag1Code;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.DateTimePicker dtBirthDate;
    }
}