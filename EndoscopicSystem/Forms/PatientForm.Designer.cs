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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.panel2 = new System.Windows.Forms.Panel();
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
            this.cbbSex.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbSex.FormattingEnabled = true;
            this.cbbSex.Items.AddRange(new object[] {
            "- Select Sex -",
            "Male",
            "FeMale"});
            this.cbbSex.Location = new System.Drawing.Point(75, 76);
            this.cbbSex.Margin = new System.Windows.Forms.Padding(2);
            this.cbbSex.Name = "cbbSex";
            this.cbbSex.Size = new System.Drawing.Size(221, 24);
            this.cbbSex.TabIndex = 5;
            // 
            // txtCID
            // 
            this.txtCID.CausesValidation = false;
            this.txtCID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtCID.Location = new System.Drawing.Point(386, 50);
            this.txtCID.Margin = new System.Windows.Forms.Padding(2);
            this.txtCID.MaxLength = 13;
            this.txtCID.Name = "txtCID";
            this.txtCID.Size = new System.Drawing.Size(273, 23);
            this.txtCID.TabIndex = 4;
            this.txtCID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCID_KeyPress);
            // 
            // txtFullName
            // 
            this.txtFullName.CausesValidation = false;
            this.txtFullName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtFullName.Location = new System.Drawing.Point(386, 20);
            this.txtFullName.Margin = new System.Windows.Forms.Padding(2);
            this.txtFullName.MaxLength = 255;
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(273, 23);
            this.txtFullName.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(327, 50);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "IDCard :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(334, 22);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Name :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.Location = new System.Drawing.Point(32, 79);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "Sex :";
            // 
            // txtAge
            // 
            this.txtAge.CausesValidation = false;
            this.txtAge.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtAge.Location = new System.Drawing.Point(75, 47);
            this.txtAge.Margin = new System.Windows.Forms.Padding(2);
            this.txtAge.MaxLength = 3;
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(221, 23);
            this.txtAge.TabIndex = 3;
            this.txtAge.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAge_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(30, 50);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Age :";
            // 
            // txtHN
            // 
            this.txtHN.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtHN.Location = new System.Drawing.Point(75, 17);
            this.txtHN.Margin = new System.Windows.Forms.Padding(2);
            this.txtHN.MaxLength = 10;
            this.txtHN.Name = "txtHN";
            this.txtHN.Size = new System.Drawing.Size(221, 23);
            this.txtHN.TabIndex = 1;
            this.txtHN.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtHN_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(34, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "HN :";
            // 
            // cbbDoctorName
            // 
            this.cbbDoctorName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbDoctorName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbDoctorName.FormattingEnabled = true;
            this.cbbDoctorName.Location = new System.Drawing.Point(570, 110);
            this.cbbDoctorName.Margin = new System.Windows.Forms.Padding(2);
            this.cbbDoctorName.Name = "cbbDoctorName";
            this.cbbDoctorName.Size = new System.Drawing.Size(287, 24);
            this.cbbDoctorName.TabIndex = 15;
            // 
            // cbbStation
            // 
            this.cbbStation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbStation.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbStation.FormattingEnabled = true;
            this.cbbStation.Location = new System.Drawing.Point(152, 110);
            this.cbbStation.Margin = new System.Windows.Forms.Padding(2);
            this.cbbStation.Name = "cbbStation";
            this.cbbStation.Size = new System.Drawing.Size(268, 24);
            this.cbbStation.TabIndex = 14;
            // 
            // cbbNurseName3
            // 
            this.cbbNurseName3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbNurseName3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbNurseName3.FormattingEnabled = true;
            this.cbbNurseName3.Location = new System.Drawing.Point(152, 197);
            this.cbbNurseName3.Margin = new System.Windows.Forms.Padding(2);
            this.cbbNurseName3.Name = "cbbNurseName3";
            this.cbbNurseName3.Size = new System.Drawing.Size(268, 24);
            this.cbbNurseName3.TabIndex = 18;
            // 
            // cbbNurseName2
            // 
            this.cbbNurseName2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbNurseName2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbNurseName2.FormattingEnabled = true;
            this.cbbNurseName2.Location = new System.Drawing.Point(152, 170);
            this.cbbNurseName2.Margin = new System.Windows.Forms.Padding(2);
            this.cbbNurseName2.Name = "cbbNurseName2";
            this.cbbNurseName2.Size = new System.Drawing.Size(268, 24);
            this.cbbNurseName2.TabIndex = 17;
            // 
            // cbbNurseName1
            // 
            this.cbbNurseName1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbNurseName1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbNurseName1.FormattingEnabled = true;
            this.cbbNurseName1.Location = new System.Drawing.Point(152, 142);
            this.cbbNurseName1.Margin = new System.Windows.Forms.Padding(2);
            this.cbbNurseName1.Name = "cbbNurseName1";
            this.cbbNurseName1.Size = new System.Drawing.Size(268, 24);
            this.cbbNurseName1.TabIndex = 16;
            // 
            // txtStaffName
            // 
            this.txtStaffName.CausesValidation = false;
            this.txtStaffName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtStaffName.Location = new System.Drawing.Point(570, 142);
            this.txtStaffName.Margin = new System.Windows.Forms.Padding(2);
            this.txtStaffName.MaxLength = 255;
            this.txtStaffName.Name = "txtStaffName";
            this.txtStaffName.Size = new System.Drawing.Size(287, 23);
            this.txtStaffName.TabIndex = 19;
            // 
            // dtOperatingDate
            // 
            this.dtOperatingDate.CausesValidation = false;
            this.dtOperatingDate.CustomFormat = "dd MMMM yyyy";
            this.dtOperatingDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dtOperatingDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtOperatingDate.Location = new System.Drawing.Point(152, 47);
            this.dtOperatingDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtOperatingDate.MinDate = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            this.dtOperatingDate.Name = "dtOperatingDate";
            this.dtOperatingDate.Size = new System.Drawing.Size(230, 26);
            this.dtOperatingDate.TabIndex = 10;
            this.dtOperatingDate.Value = new System.DateTime(2020, 9, 16, 12, 23, 4, 768);
            // 
            // dtOperatingTime
            // 
            this.dtOperatingTime.CausesValidation = false;
            this.dtOperatingTime.CustomFormat = "HH:mm";
            this.dtOperatingTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dtOperatingTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtOperatingTime.Location = new System.Drawing.Point(394, 47);
            this.dtOperatingTime.Margin = new System.Windows.Forms.Padding(2);
            this.dtOperatingTime.MinDate = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            this.dtOperatingTime.Name = "dtOperatingTime";
            this.dtOperatingTime.ShowUpDown = true;
            this.dtOperatingTime.Size = new System.Drawing.Size(93, 26);
            this.dtOperatingTime.TabIndex = 11;
            this.dtOperatingTime.Value = new System.DateTime(2020, 9, 16, 12, 23, 4, 770);
            // 
            // dtAppointmentTime
            // 
            this.dtAppointmentTime.CausesValidation = false;
            this.dtAppointmentTime.CustomFormat = "HH:mm";
            this.dtAppointmentTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dtAppointmentTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtAppointmentTime.Location = new System.Drawing.Point(394, 13);
            this.dtAppointmentTime.Margin = new System.Windows.Forms.Padding(2);
            this.dtAppointmentTime.MinDate = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            this.dtAppointmentTime.Name = "dtAppointmentTime";
            this.dtAppointmentTime.ShowUpDown = true;
            this.dtAppointmentTime.Size = new System.Drawing.Size(93, 26);
            this.dtAppointmentTime.TabIndex = 9;
            this.dtAppointmentTime.Value = new System.DateTime(2020, 9, 16, 12, 23, 4, 772);
            // 
            // dtAppointmentDate
            // 
            this.dtAppointmentDate.CausesValidation = false;
            this.dtAppointmentDate.CustomFormat = "dd MMMM yyyy";
            this.dtAppointmentDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dtAppointmentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtAppointmentDate.Location = new System.Drawing.Point(152, 13);
            this.dtAppointmentDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtAppointmentDate.MinDate = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            this.dtAppointmentDate.Name = "dtAppointmentDate";
            this.dtAppointmentDate.Size = new System.Drawing.Size(230, 26);
            this.dtAppointmentDate.TabIndex = 8;
            this.dtAppointmentDate.Value = new System.DateTime(2020, 9, 16, 12, 23, 4, 775);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label11.Location = new System.Drawing.Point(505, 113);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(58, 17);
            this.label11.TabIndex = 0;
            this.label11.Text = "Doctor :";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label17.Location = new System.Drawing.Point(517, 145);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(45, 17);
            this.label17.TabIndex = 0;
            this.label17.Text = "Staff :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label8.Location = new System.Drawing.Point(9, 113);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(130, 17);
            this.label8.TabIndex = 0;
            this.label8.Text = "Endoscopic Room :";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label15.Location = new System.Drawing.Point(77, 145);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(54, 17);
            this.label15.TabIndex = 0;
            this.label15.Text = "Nurse :";
            // 
            // txtSymptom
            // 
            this.txtSymptom.CausesValidation = false;
            this.txtSymptom.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtSymptom.Location = new System.Drawing.Point(152, 81);
            this.txtSymptom.Margin = new System.Windows.Forms.Padding(2);
            this.txtSymptom.MaxLength = 250;
            this.txtSymptom.Name = "txtSymptom";
            this.txtSymptom.Size = new System.Drawing.Size(268, 23);
            this.txtSymptom.TabIndex = 12;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label12.Location = new System.Drawing.Point(58, 84);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(74, 17);
            this.label12.TabIndex = 0;
            this.label12.Text = "Symptom :";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label13.Location = new System.Drawing.Point(25, 54);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(114, 17);
            this.label13.TabIndex = 0;
            this.label13.Text = "Operating Time :";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label14.Location = new System.Drawing.Point(10, 20);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(130, 17);
            this.label14.TabIndex = 0;
            this.label14.Text = "Appointment Time :";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label16.Location = new System.Drawing.Point(4, 11);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(87, 26);
            this.label16.TabIndex = 7;
            this.label16.Text = "Patient";
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnDelete.Location = new System.Drawing.Point(344, 639);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(84, 46);
            this.btnDelete.TabIndex = 20;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSave.Location = new System.Drawing.Point(518, 639);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(84, 46);
            this.btnSave.TabIndex = 18;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnReset.Location = new System.Drawing.Point(245, 639);
            this.btnReset.Margin = new System.Windows.Forms.Padding(2);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(84, 46);
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridPatient.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridPatient.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridPatient.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridPatient.Location = new System.Drawing.Point(9, 431);
            this.gridPatient.Margin = new System.Windows.Forms.Padding(2);
            this.gridPatient.Name = "gridPatient";
            this.gridPatient.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridPatient.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gridPatient.RowHeadersWidth = 51;
            this.gridPatient.RowTemplate.Height = 24;
            this.gridPatient.Size = new System.Drawing.Size(910, 203);
            this.gridPatient.TabIndex = 0;
            this.gridPatient.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridPatient_CellDoubleClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = null;
            this.pictureBox1.Location = new System.Drawing.Point(741, 11);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(158, 145);
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(782, 160);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(2);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(78, 27);
            this.btnBrowse.TabIndex = 20;
            this.btnBrowse.Text = "Browse File";
            this.btnBrowse.UseVisualStyleBackColor = true;
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
            this.panel1.Location = new System.Drawing.Point(9, 39);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(695, 128);
            this.panel1.TabIndex = 22;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            this.panel2.Location = new System.Drawing.Point(9, 191);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(910, 236);
            this.panel2.TabIndex = 8;
            // 
            // chkIPD
            // 
            this.chkIPD.AutoSize = true;
            this.chkIPD.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkIPD.Location = new System.Drawing.Point(675, 201);
            this.chkIPD.Margin = new System.Windows.Forms.Padding(2);
            this.chkIPD.Name = "chkIPD";
            this.chkIPD.Size = new System.Drawing.Size(49, 21);
            this.chkIPD.TabIndex = 89;
            this.chkIPD.Text = "IPD";
            this.chkIPD.UseVisualStyleBackColor = true;
            // 
            // chkOPD
            // 
            this.chkOPD.AutoSize = true;
            this.chkOPD.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkOPD.Location = new System.Drawing.Point(570, 199);
            this.chkOPD.Margin = new System.Windows.Forms.Padding(2);
            this.chkOPD.Name = "chkOPD";
            this.chkOPD.Size = new System.Drawing.Size(57, 21);
            this.chkOPD.TabIndex = 87;
            this.chkOPD.Text = "OPD";
            this.chkOPD.UseVisualStyleBackColor = true;
            // 
            // cbbProcedureList
            // 
            this.cbbProcedureList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbProcedureList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbProcedureList.FormattingEnabled = true;
            this.cbbProcedureList.Location = new System.Drawing.Point(570, 80);
            this.cbbProcedureList.Margin = new System.Windows.Forms.Padding(2);
            this.cbbProcedureList.Name = "cbbProcedureList";
            this.cbbProcedureList.Size = new System.Drawing.Size(287, 24);
            this.cbbProcedureList.TabIndex = 85;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label18.Location = new System.Drawing.Point(460, 82);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(108, 17);
            this.label18.TabIndex = 84;
            this.label18.Text = "Procedure List :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label10.Location = new System.Drawing.Point(514, 170);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 17);
            this.label10.TabIndex = 81;
            this.label10.Text = "Case :";
            // 
            // chkFollowUpCase
            // 
            this.chkFollowUpCase.AutoSize = true;
            this.chkFollowUpCase.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkFollowUpCase.Location = new System.Drawing.Point(675, 170);
            this.chkFollowUpCase.Margin = new System.Windows.Forms.Padding(2);
            this.chkFollowUpCase.Name = "chkFollowUpCase";
            this.chkFollowUpCase.Size = new System.Drawing.Size(122, 21);
            this.chkFollowUpCase.TabIndex = 82;
            this.chkFollowUpCase.Text = "Follow up Case";
            this.chkFollowUpCase.UseVisualStyleBackColor = true;
            // 
            // chkNewCase
            // 
            this.chkNewCase.AutoSize = true;
            this.chkNewCase.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkNewCase.Location = new System.Drawing.Point(570, 169);
            this.chkNewCase.Margin = new System.Windows.Forms.Padding(2);
            this.chkNewCase.Name = "chkNewCase";
            this.chkNewCase.Size = new System.Drawing.Size(90, 21);
            this.chkNewCase.TabIndex = 83;
            this.chkNewCase.Text = "New Case";
            this.chkNewCase.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(452, 83);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 17);
            this.label6.TabIndex = 90;
            this.label6.Text = "*";
            // 
            // PatientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(233)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(928, 695);
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
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "admin";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
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
    }
}