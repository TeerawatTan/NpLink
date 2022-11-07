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
            this.cbbStation = new System.Windows.Forms.ComboBox();
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
            this.txbFinancial = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbbPreDiagnosis2 = new System.Windows.Forms.ComboBox();
            this.cbbPreDiagnosis1 = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.cbbIndication = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.cbbAnesthesiaMethod2 = new System.Windows.Forms.ComboBox();
            this.cbbAnesthesiaMethod1 = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txbRefer = new System.Windows.Forms.TextBox();
            this.chkRefer = new System.Windows.Forms.CheckBox();
            this.cbbWard = new System.Windows.Forms.ComboBox();
            this.chkWard = new System.Windows.Forms.CheckBox();
            this.cbbOPD = new System.Windows.Forms.ComboBox();
            this.cbbAnesthesist = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chkIPD = new System.Windows.Forms.CheckBox();
            this.chkOPD = new System.Windows.Forms.CheckBox();
            this.cbbProcedureList = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.chkFollowUpCase = new System.Windows.Forms.CheckBox();
            this.chkNewCase = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
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
            this.cbbSex.Location = new System.Drawing.Point(78, 81);
            this.cbbSex.Margin = new System.Windows.Forms.Padding(2);
            this.cbbSex.Name = "cbbSex";
            this.cbbSex.Size = new System.Drawing.Size(140, 28);
            this.cbbSex.TabIndex = 5;
            // 
            // txtCID
            // 
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
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(504, 53);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "IDCard :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(511, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Name :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.Location = new System.Drawing.Point(32, 84);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "Sex :";
            // 
            // txtAge
            // 
            this.txtAge.CausesValidation = false;
            this.txtAge.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtAge.Location = new System.Drawing.Point(80, 52);
            this.txtAge.Margin = new System.Windows.Forms.Padding(2);
            this.txtAge.MaxLength = 3;
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(138, 26);
            this.txtAge.TabIndex = 3;
            this.txtAge.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAge_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(30, 55);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Age :";
            // 
            // txtHN
            // 
            this.txtHN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtHN.Location = new System.Drawing.Point(78, 22);
            this.txtHN.Margin = new System.Windows.Forms.Padding(2);
            this.txtHN.MaxLength = 10;
            this.txtHN.Name = "txtHN";
            this.txtHN.Size = new System.Drawing.Size(365, 26);
            this.txtHN.TabIndex = 1;
            this.txtHN.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtHN_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(34, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 20);
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
            this.cbbDoctorName.Size = new System.Drawing.Size(440, 28);
            this.cbbDoctorName.TabIndex = 15;
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
            this.txtStaffName.CausesValidation = false;
            this.txtStaffName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtStaffName.Location = new System.Drawing.Point(789, 142);
            this.txtStaffName.Margin = new System.Windows.Forms.Padding(2);
            this.txtStaffName.MaxLength = 255;
            this.txtStaffName.Name = "txtStaffName";
            this.txtStaffName.Size = new System.Drawing.Size(440, 26);
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
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label11.Location = new System.Drawing.Point(720, 113);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 20);
            this.label11.TabIndex = 0;
            this.label11.Text = "Doctor :";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label17.Location = new System.Drawing.Point(733, 145);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(52, 20);
            this.label17.TabIndex = 0;
            this.label17.Text = "Staff :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label8.Location = new System.Drawing.Point(29, 112);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(147, 20);
            this.label8.TabIndex = 0;
            this.label8.Text = "Endoscopic Room :";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label15.Location = new System.Drawing.Point(117, 145);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(59, 20);
            this.label15.TabIndex = 0;
            this.label15.Text = "Nurse :";
            // 
            // txtSymptom
            // 
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
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label12.Location = new System.Drawing.Point(92, 83);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(84, 20);
            this.label12.TabIndex = 0;
            this.label12.Text = "Symptom :";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label13.Location = new System.Drawing.Point(51, 52);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(125, 20);
            this.label13.TabIndex = 0;
            this.label13.Text = "Operating Time :";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label14.Location = new System.Drawing.Point(30, 19);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(146, 20);
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
            this.label16.Size = new System.Drawing.Size(86, 25);
            this.label16.TabIndex = 7;
            this.label16.Text = "Patient";
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
            this.panel1.Controls.Add(this.txbFinancial);
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
            // txbFinancial
            // 
            this.txbFinancial.CausesValidation = false;
            this.txbFinancial.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txbFinancial.Location = new System.Drawing.Point(574, 81);
            this.txbFinancial.Margin = new System.Windows.Forms.Padding(2);
            this.txbFinancial.MaxLength = 13;
            this.txbFinancial.Name = "txbFinancial";
            this.txbFinancial.Size = new System.Drawing.Size(384, 26);
            this.txbFinancial.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label7.Location = new System.Drawing.Point(492, 84);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 20);
            this.label7.TabIndex = 6;
            this.label7.Text = "Financial :";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.cbbPreDiagnosis2);
            this.panel2.Controls.Add(this.cbbPreDiagnosis1);
            this.panel2.Controls.Add(this.label21);
            this.panel2.Controls.Add(this.cbbIndication);
            this.panel2.Controls.Add(this.label20);
            this.panel2.Controls.Add(this.cbbAnesthesiaMethod2);
            this.panel2.Controls.Add(this.cbbAnesthesiaMethod1);
            this.panel2.Controls.Add(this.label19);
            this.panel2.Controls.Add(this.txbRefer);
            this.panel2.Controls.Add(this.chkRefer);
            this.panel2.Controls.Add(this.cbbWard);
            this.panel2.Controls.Add(this.chkWard);
            this.panel2.Controls.Add(this.cbbOPD);
            this.panel2.Controls.Add(this.cbbAnesthesist);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.chkIPD);
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
            this.panel2.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel2.Location = new System.Drawing.Point(9, 231);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1248, 370);
            this.panel2.TabIndex = 8;
            // 
            // cbbPreDiagnosis2
            // 
            this.cbbPreDiagnosis2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbPreDiagnosis2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbPreDiagnosis2.FormattingEnabled = true;
            this.cbbPreDiagnosis2.Location = new System.Drawing.Point(789, 327);
            this.cbbPreDiagnosis2.Margin = new System.Windows.Forms.Padding(2);
            this.cbbPreDiagnosis2.Name = "cbbPreDiagnosis2";
            this.cbbPreDiagnosis2.Size = new System.Drawing.Size(440, 28);
            this.cbbPreDiagnosis2.TabIndex = 105;
            // 
            // cbbPreDiagnosis1
            // 
            this.cbbPreDiagnosis1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbPreDiagnosis1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbPreDiagnosis1.FormattingEnabled = true;
            this.cbbPreDiagnosis1.Location = new System.Drawing.Point(789, 295);
            this.cbbPreDiagnosis1.Margin = new System.Windows.Forms.Padding(2);
            this.cbbPreDiagnosis1.Name = "cbbPreDiagnosis1";
            this.cbbPreDiagnosis1.Size = new System.Drawing.Size(440, 28);
            this.cbbPreDiagnosis1.TabIndex = 104;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label21.Location = new System.Drawing.Point(669, 298);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(116, 20);
            this.label21.TabIndex = 103;
            this.label21.Text = "Pre-Diagnosis :";
            // 
            // cbbIndication
            // 
            this.cbbIndication.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbIndication.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbIndication.FormattingEnabled = true;
            this.cbbIndication.Location = new System.Drawing.Point(192, 327);
            this.cbbIndication.Margin = new System.Windows.Forms.Padding(2);
            this.cbbIndication.Name = "cbbIndication";
            this.cbbIndication.Size = new System.Drawing.Size(440, 28);
            this.cbbIndication.TabIndex = 102;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label20.Location = new System.Drawing.Point(90, 330);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(86, 20);
            this.label20.TabIndex = 101;
            this.label20.Text = "Indication :";
            // 
            // cbbAnesthesiaMethod2
            // 
            this.cbbAnesthesiaMethod2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbAnesthesiaMethod2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbAnesthesiaMethod2.FormattingEnabled = true;
            this.cbbAnesthesiaMethod2.Location = new System.Drawing.Point(192, 295);
            this.cbbAnesthesiaMethod2.Margin = new System.Windows.Forms.Padding(2);
            this.cbbAnesthesiaMethod2.Name = "cbbAnesthesiaMethod2";
            this.cbbAnesthesiaMethod2.Size = new System.Drawing.Size(440, 28);
            this.cbbAnesthesiaMethod2.TabIndex = 100;
            // 
            // cbbAnesthesiaMethod1
            // 
            this.cbbAnesthesiaMethod1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbAnesthesiaMethod1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbAnesthesiaMethod1.FormattingEnabled = true;
            this.cbbAnesthesiaMethod1.Location = new System.Drawing.Point(192, 263);
            this.cbbAnesthesiaMethod1.Margin = new System.Windows.Forms.Padding(2);
            this.cbbAnesthesiaMethod1.Name = "cbbAnesthesiaMethod1";
            this.cbbAnesthesiaMethod1.Size = new System.Drawing.Size(440, 28);
            this.cbbAnesthesiaMethod1.TabIndex = 99;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label19.Location = new System.Drawing.Point(21, 266);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(155, 20);
            this.label19.TabIndex = 98;
            this.label19.Text = "Anesthesia Method :";
            // 
            // txbRefer
            // 
            this.txbRefer.CausesValidation = false;
            this.txbRefer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txbRefer.Location = new System.Drawing.Point(861, 265);
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
            this.chkRefer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkRefer.Location = new System.Drawing.Point(789, 265);
            this.chkRefer.Margin = new System.Windows.Forms.Padding(2);
            this.chkRefer.Name = "chkRefer";
            this.chkRefer.Size = new System.Drawing.Size(68, 24);
            this.chkRefer.TabIndex = 96;
            this.chkRefer.Text = "Refer";
            this.chkRefer.UseVisualStyleBackColor = true;
            // 
            // cbbWard
            // 
            this.cbbWard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbWard.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbWard.FormattingEnabled = true;
            this.cbbWard.Location = new System.Drawing.Point(861, 234);
            this.cbbWard.Margin = new System.Windows.Forms.Padding(2);
            this.cbbWard.Name = "cbbWard";
            this.cbbWard.Size = new System.Drawing.Size(368, 28);
            this.cbbWard.TabIndex = 95;
            this.cbbWard.SelectedIndexChanged += new System.EventHandler(this.cbbWard_SelectedIndexChanged);
            // 
            // chkWard
            // 
            this.chkWard.AutoSize = true;
            this.chkWard.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkWard.Location = new System.Drawing.Point(789, 237);
            this.chkWard.Margin = new System.Windows.Forms.Padding(2);
            this.chkWard.Name = "chkWard";
            this.chkWard.Size = new System.Drawing.Size(66, 24);
            this.chkWard.TabIndex = 94;
            this.chkWard.Text = "Ward";
            this.chkWard.UseVisualStyleBackColor = true;
            // 
            // cbbOPD
            // 
            this.cbbOPD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbOPD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbOPD.FormattingEnabled = true;
            this.cbbOPD.Location = new System.Drawing.Point(861, 204);
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
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label9.Location = new System.Drawing.Point(75, 234);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 20);
            this.label9.TabIndex = 91;
            this.label9.Text = "Anesthesist :";
            // 
            // chkIPD
            // 
            this.chkIPD.AutoSize = true;
            this.chkIPD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkIPD.Location = new System.Drawing.Point(1045, 177);
            this.chkIPD.Margin = new System.Windows.Forms.Padding(2);
            this.chkIPD.Name = "chkIPD";
            this.chkIPD.Size = new System.Drawing.Size(55, 24);
            this.chkIPD.TabIndex = 89;
            this.chkIPD.Text = "IPD";
            this.chkIPD.UseVisualStyleBackColor = true;
            this.chkIPD.Visible = false;
            // 
            // chkOPD
            // 
            this.chkOPD.AutoSize = true;
            this.chkOPD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkOPD.Location = new System.Drawing.Point(789, 207);
            this.chkOPD.Margin = new System.Windows.Forms.Padding(2);
            this.chkOPD.Name = "chkOPD";
            this.chkOPD.Size = new System.Drawing.Size(62, 24);
            this.chkOPD.TabIndex = 87;
            this.chkOPD.Text = "OPD";
            this.chkOPD.UseVisualStyleBackColor = true;
            // 
            // cbbProcedureList
            // 
            this.cbbProcedureList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbProcedureList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbProcedureList.FormattingEnabled = true;
            this.cbbProcedureList.Location = new System.Drawing.Point(789, 80);
            this.cbbProcedureList.Margin = new System.Windows.Forms.Padding(2);
            this.cbbProcedureList.Name = "cbbProcedureList";
            this.cbbProcedureList.Size = new System.Drawing.Size(440, 28);
            this.cbbProcedureList.TabIndex = 85;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label18.Location = new System.Drawing.Point(666, 82);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(119, 20);
            this.label18.TabIndex = 84;
            this.label18.Text = "Procedure List :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label10.Location = new System.Drawing.Point(731, 178);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 20);
            this.label10.TabIndex = 81;
            this.label10.Text = "Case :";
            // 
            // chkFollowUpCase
            // 
            this.chkFollowUpCase.AutoSize = true;
            this.chkFollowUpCase.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkFollowUpCase.Location = new System.Drawing.Point(894, 178);
            this.chkFollowUpCase.Margin = new System.Windows.Forms.Padding(2);
            this.chkFollowUpCase.Name = "chkFollowUpCase";
            this.chkFollowUpCase.Size = new System.Drawing.Size(136, 24);
            this.chkFollowUpCase.TabIndex = 82;
            this.chkFollowUpCase.Text = "Follow up Case";
            this.chkFollowUpCase.UseVisualStyleBackColor = true;
            // 
            // chkNewCase
            // 
            this.chkNewCase.AutoSize = true;
            this.chkNewCase.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkNewCase.Location = new System.Drawing.Point(789, 177);
            this.chkNewCase.Margin = new System.Windows.Forms.Padding(2);
            this.chkNewCase.Name = "chkNewCase";
            this.chkNewCase.Size = new System.Drawing.Size(100, 24);
            this.chkNewCase.TabIndex = 83;
            this.chkNewCase.Text = "New Case";
            this.chkNewCase.UseVisualStyleBackColor = true;
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
            // PatientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(233)))), ((int)(((byte)(242)))));
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
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PatientForm_FormClosed);
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
        private System.Windows.Forms.ComboBox cbbStation;
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
        private System.Windows.Forms.CheckBox chkIPD;
        private System.Windows.Forms.CheckBox chkOPD;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txbFinancial;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbbAnesthesist;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbbPreDiagnosis2;
        private System.Windows.Forms.ComboBox cbbPreDiagnosis1;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox cbbIndication;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox cbbAnesthesiaMethod2;
        private System.Windows.Forms.ComboBox cbbAnesthesiaMethod1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txbRefer;
        private System.Windows.Forms.CheckBox chkRefer;
        private System.Windows.Forms.ComboBox cbbWard;
        private System.Windows.Forms.CheckBox chkWard;
        private System.Windows.Forms.ComboBox cbbOPD;
    }
}