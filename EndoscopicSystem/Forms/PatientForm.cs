using EndoscopicSystem.Constants;
using EndoscopicSystem.Entities;
using EndoscopicSystem.Forms;
using EndoscopicSystem.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EndoscopicSystem
{
    public partial class PatientForm : Form
    {
        readonly EndoscopicEntities _db = new EndoscopicEntities();
        protected readonly GetDropdownList list = new GetDropdownList();
        private int patientId = 0, procedureId = 0;
        private readonly int UserID;
        string path = Application.StartupPath.Replace("\\bin\\Debug", "");
        private string hnNo = "";
        private List<ICD10> _iCD10s = new List<ICD10>();
        public PatientForm(int userID, string hn = "", int procId = 0)
        {
            InitializeComponent();
            UserID = userID;
            hnNo = hn;
            procedureId = procId;
        }

        private void PatientForm_Load(object sender, EventArgs e)
        {
            dtBirthDate.Value = DateTime.Today.AddDays(-1);
            txtAge.Text = (DateTime.Today.Year - dtBirthDate.Value.Year).ToString();
            dtAppointmentDate.Value = DateTime.Today;
            dtAppointmentTime.Value = DateTime.Today;
            dtOperatingDate.Value = DateTime.Now;
            dtOperatingTime.Value = DateTime.Now;
            this.ActiveControl = txtHN;
            txtHN.Focus();
            cbbSex.SelectedIndex = 0;
            DropdownRoom();
            DropdownDoctor();
            DropdownNurse(cbbNurseName1);
            DropdownNurse(cbbNurseName2);
            DropdownNurse(cbbNurseName3);
            DropdownProcedure();
            DropdownOPD();
            DropdownWard();
            DropdownAnesthesist();
            DropdownMasterIndication();
            DropdownFinancial();
            DropdownICD10();

            gridPatient.DataSource = new HistoryModel();

            if (!string.IsNullOrWhiteSpace(hnNo) && procedureId > 0)
            {
                SearchHN(hnNo, procedureId);
                LoadTextBoxAutoComplete(txbPreDiag1Code);
                LoadTextBoxAutoComplete(txbPreDiag2Code);
            }
        }

        private void txtHN_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtHN.Text.Length > 0)
            {
                SearchHN(txtHN.Text);
            }
        }

        private void SearchHN(string hn, int procedureId = 0)
        {
            try
            {
                var list = new List<HistoryModel>();
                var data = _db.Patients.Where(x => x.IsActive.HasValue && x.IsActive.Value).ToList();
                if (!string.IsNullOrWhiteSpace(hn))
                {
                    data = data.Where(x => x.HN == hn).ToList();
                }
                if (procedureId > 0)
                {
                    data = data.Where(x => x.ProcedureID == procedureId).ToList();
                }
                if (data.Count > 0)
                {
                    var response = data.OrderByDescending(x => x.PatientID).FirstOrDefault();
                    patientId = response.PatientID;
                    txtAge.Text = response.Age.HasValue ? response.Age.ToString() : "";
                    txtCID.Text = response.CardID;
                    cbbFinancial.SelectedValue = response.FinancialID;
                    cbbDoctorName.SelectedValue = response.DoctorID ?? 0;
                    txtFullName.Text = response.Fullname;
                    txtHN.Text = response.HN;
                    cbbNurseName1.SelectedValue = response.NurseFirstID ?? 0;
                    cbbNurseName2.SelectedValue = response.NurseSecondID ?? 0;
                    cbbNurseName3.SelectedValue = response.NurseThirthID ?? 0;
                    dtBirthDate.Value = response.BirthDate ?? DateTime.Today.AddDays(-1);
                    if (response.AppointmentDate.HasValue)
                    {
                        string[] appointment = response.AppointmentDate.Value.ToString().Split(' ');
                        dtAppointmentDate.Value = Convert.ToDateTime(appointment[0]);
                        dtAppointmentTime.Value = Convert.ToDateTime(appointment[1]);
                    }
                    else
                    {
                        dtAppointmentDate.Value = DateTime.Now;
                        dtAppointmentTime.Value = DateTime.Now;
                    }
                    if (response.OperationDate.HasValue)
                    {
                        string[] operation = response.OperationDate.Value.ToString().Split(' ');
                        dtOperatingDate.Value = Convert.ToDateTime(operation[0]);
                        dtOperatingTime.Value = Convert.ToDateTime(operation[1]);
                    }
                    else
                    {
                        dtOperatingDate.Value = DateTime.Now;
                        dtOperatingTime.Value = DateTime.Now;
                    }
                    cbbProcedureList.SelectedValue = response.ProcedureID;
                    cbbSex.SelectedIndex = response.Sex.HasValue ? response.Sex.Value ? 1 : 2 : 0;
                    txtStaffName.Text = response.StaffName;
                    cbbStation.SelectedValue = response.RoomID ?? 0;
                    pictureBox1.ImageLocation = response.PicturePath;
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    cbbOPD.SelectedValue = response.OpdID ?? 0;
                    chkWard.Checked = response.WardID != null && response.WardID > 0;
                    cbbWard.SelectedValue = response.WardID ?? 0;
                    chkRefer.Checked = response.ReferCheck ?? false;
                    txbRefer.Text = response.ReferDetail;
                    cbbAnesthesist.SelectedValue = response.AnesthesistID ?? 0;
                    cbbIndication.SelectedValue = response.IndicationID ?? 0;

                    procedureId = response.ProcedureID ?? 0;
                    Appointment app = _db.Appointments.Where(x => txtHN.Text.Equals(x.HN) && x.ProcedureID == procedureId).OrderByDescending(x => x.AppointmentID).FirstOrDefault();
                    if (app != null)
                    {
                        txtSymptom.Text = app.Symptom;
                        chkNewCase.Checked = app.IsNewCase ?? false;
                        chkFollowUpCase.Checked = app.IsFollowCase ?? false;
                        chkOPD.Checked = app.OPD ?? false;
                    }

                    var history = _db.v_HistoryEndoscopic
                                .Where(x => x.PatientID == patientId)
                                .ToList();
                    if (history != null && history.Count > 0)
                    {
                        history = history.OrderByDescending(o => o.OperationDate).ToList();
                        foreach (var item in history)
                        {
                            var model = new HistoryModel();
                            model.EndoScopicID = item.EndoscopicID;
                            model.ProcedureID = item.ProcedureID;
                            model.OperatingDate = item.OperationDate;
                            model.Symptom = item.Symptom;
                            model.Procedure = item.ProcedureName;
                            model.Doctor = item.Doctor;
                            model.Diagnosis = item.Diagnosis;
                            model.AppointmentID = item.AppointmentID;
                            list.Add(model);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมูลผู้ป่วย", "Patient not found.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ResetPictureBox();
                    this.Controls.ClearControls();
                }
                gridPatient.DataSource = list;
                gridPatient.Columns["EndoScopicID"].Visible = false;
                gridPatient.Columns["ProcedureID"].Visible = false;
                gridPatient.Columns["AppointmentID"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int? procedureSelected = (int?)cbbProcedureList.SelectedValue;
            if (procedureSelected == null || procedureSelected == 0)
            {
                MessageBox.Show("กรุณาเลือก Procedure List", "Procedure List", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string status;
            bool? gender;
            if (cbbSex.SelectedItem.ToString().Equals(Constant.Male)) gender = true;
            else if (cbbSex.SelectedItem.ToString().Equals(Constant.FeMale)) gender = false;
            else gender = null;

            if (!string.IsNullOrWhiteSpace(txtHN.Text))
            {
                Patient patient = new Patient();

                try
                {
                    patient = _db.Patients.Where(x => x.HN == txtHN.Text).FirstOrDefault();
                    if (patient != null)
                    {
                        // Update Patient
                        procedureId = (int)cbbProcedureList.SelectedValue;
                        patient.Fullname = txtFullName.Text;
                        patient.CardID = txtCID.Text;
                        patient.Sex = gender;
                        patient.Age = (!string.IsNullOrWhiteSpace(txtAge.Text)) ? Convert.ToInt32(txtAge.Text) : 0;
                        patient.DoctorID = (int?)cbbDoctorName.SelectedValue;
                        patient.NurseFirstID = (int?)cbbNurseName1.SelectedValue;
                        patient.NurseSecondID = (int?)cbbNurseName2.SelectedValue;
                        patient.NurseThirthID = (int?)cbbNurseName3.SelectedValue;
                        patient.ProcedureID = (int?)cbbProcedureList.SelectedValue;
                        patient.StaffName = txtStaffName.Text;
                        patient.RoomID = (int?)cbbStation.SelectedValue;
                        patient.BirthDate = dtBirthDate.Value;
                        patient.AppointmentDate = new DateTime(
                            dtAppointmentDate.Value.Year,
                            dtAppointmentDate.Value.Month,
                            dtAppointmentDate.Value.Day,
                            dtAppointmentTime.Value.Hour,
                            dtAppointmentTime.Value.Minute,
                            dtAppointmentTime.Value.Second);
                        patient.OperationDate = new DateTime(
                            dtOperatingDate.Value.Year,
                            dtOperatingDate.Value.Month,
                            dtOperatingDate.Value.Day,
                            dtOperatingTime.Value.Hour,
                            dtOperatingTime.Value.Minute,
                            dtOperatingTime.Value.Second);

                        patient.PicturePath = SavePicture() ?? patient.PicturePath;
                        patient.FinancialID = (int?)cbbFinancial.SelectedValue;
                        patient.OpdID = (int?)cbbOPD.SelectedValue;
                        patient.WardID = (int?)cbbWard.SelectedValue;
                        patient.ReferCheck = chkRefer.Checked;
                        patient.ReferDetail = txbRefer.Text;
                        patient.AnesthesistID = (int?)cbbAnesthesist.SelectedValue;
                        patient.IndicationID = (int?)cbbIndication.SelectedValue;
                        patient.PreDiagnosisFirstID = !string.IsNullOrWhiteSpace(txbPreDiag1ID.Text) ? Convert.ToInt32(txbPreDiag1ID.Text) : 0;
                        patient.PreDiagnosisSecondID = !string.IsNullOrWhiteSpace(txbPreDiag2ID.Text) ? Convert.ToInt32(txbPreDiag2ID.Text) : 0;
                        patient.UpdateBy = UserID;
                        patient.UpdateDate = DateTime.Now;
                    }
                    else
                    {
                        //New Patient
                        
                        patient = new Patient();

                        if (this.CheckHN(txtHN.Text))
                        {
                            MessageBox.Show("มี HN นี้อยู่ในระบบแล้ว", "Save Form", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.Controls.ClearControls();
                            return;
                        }

                        patient.HN = txtHN.Text;
                        patient.Fullname = txtFullName.Text;
                        patient.CardID = txtCID.Text;
                        patient.Sex = gender;
                        patient.Age = (!string.IsNullOrWhiteSpace(txtAge.Text)) ? Convert.ToInt32(txtAge.Text) : 0;
                        patient.DoctorID = (int?)cbbDoctorName.SelectedValue;
                        patient.NurseFirstID = (int?)cbbNurseName1.SelectedValue;
                        patient.NurseSecondID = (int?)cbbNurseName2.SelectedValue;
                        patient.NurseThirthID = (int?)cbbNurseName3.SelectedValue;
                        patient.ProcedureID = (int?)cbbProcedureList.SelectedValue;
                        patient.StaffName = txtStaffName.Text;
                        patient.RoomID = (int?)cbbStation.SelectedValue;
                        patient.BirthDate = dtBirthDate.Value;
                        patient.AppointmentDate = new DateTime(
                            dtAppointmentDate.Value.Year,
                            dtAppointmentDate.Value.Month,
                            dtAppointmentDate.Value.Day,
                            dtAppointmentTime.Value.Hour,
                            dtAppointmentTime.Value.Minute,
                            dtAppointmentTime.Value.Second);
                        patient.OperationDate = new DateTime(
                            dtOperatingDate.Value.Year,
                            dtOperatingDate.Value.Month,
                            dtOperatingDate.Value.Day,
                            dtOperatingTime.Value.Hour,
                            dtOperatingTime.Value.Minute,
                            dtOperatingTime.Value.Second);
                        patient.PicturePath = SavePicture() ?? patient.PicturePath;
                        patient.FinancialID = (int?)cbbFinancial.SelectedValue;
                        patient.OpdID = (int?)cbbOPD.SelectedValue;
                        patient.WardID = (int?)cbbWard.SelectedValue;
                        patient.ReferCheck = chkRefer.Checked;
                        patient.ReferDetail = txbRefer.Text;
                        patient.AnesthesistID = (int?)cbbAnesthesist.SelectedValue;
                        patient.IndicationID = (int?)cbbIndication.SelectedValue;
                        patient.PreDiagnosisFirstID = !string.IsNullOrWhiteSpace(txbPreDiag1ID.Text) ? Convert.ToInt32(txbPreDiag1ID.Text) : 0;
                        patient.PreDiagnosisSecondID = !string.IsNullOrWhiteSpace(txbPreDiag2ID.Text) ? Convert.ToInt32(txbPreDiag2ID.Text) : 0;
                        patient.IsActive = true;
                        patient.CreateBy = UserID;
                        patient.CreateDate = DateTime.Now;
                        _db.Patients.Add(patient);
                    }

                    int resultSave = 0;
                    try
                    {
                        resultSave = _db.SaveChanges();
                        if (resultSave > 0)
                        {
                            SaveAppointment(txtHN.Text, patient);
                            status = Constant.STATUS_SUCCESS;
                            this.Controls.ClearControls();
                        }
                        else
                        {
                            status = Constant.STATUS_ERROR;
                        }
                    }
                    catch (DbEntityValidationException dbEx)
                    {
                        throw dbEx;
                    }

                    if (status == Constant.STATUS_SUCCESS)
                    {
                        MessageBox.Show(status, "Save Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetForm();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(status, "Error Save Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Exception Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SaveAppointment(string hn, Patient data)
        {
            Appointment ap = _db.Appointments.Where(x => x.HN == hn && x.ProcedureID == procedureId && x.AppointmentDate.HasValue && x.AppointmentDate.Value == data.AppointmentDate).OrderByDescending(x => x.AppointmentID).FirstOrDefault();
            if (ap != null)
            {
                ap.PatientID = data.PatientID;
                ap.HN = data.HN;
                ap.Fullname = data.Fullname;
                ap.CardID = data.CardID;
                ap.Sex = data.Sex;
                ap.Age = data.Age;
                ap.MobileNumber = data.MobileNumber;
                ap.FullAddress = data.FullAddress;
                ap.DoctorID = data.DoctorID;
                ap.NurseFirstID = data.NurseFirstID;
                ap.NurseSecondID = data.NurseSecondID;
                ap.NurseThirthID = data.NurseThirthID;
                ap.ProcedureID = data.ProcedureID;
                ap.StaffName = data.StaffName;
                ap.RoomID = data.RoomID;
                ap.Symptom = txtSymptom.Text;
                ap.AppointmentDate = data.AppointmentDate;
                ap.OperationDate = data.OperationDate;
                ap.IsNewCase = chkNewCase.Checked;
                ap.IsFollowCase = chkFollowUpCase.Checked;
                ap.OPD = chkOPD.Checked;
                ap.CreateDate = data.CreateDate;
                ap.CreateBy = data.CreateBy;
                ap.EndoscopicCheck = false;
            }
            else
            {
                var appointment = new Appointment();
                appointment.PatientID = data.PatientID;
                appointment.HN = data.HN;
                appointment.Fullname = data.Fullname;
                appointment.CardID = data.CardID;
                appointment.Sex = data.Sex;
                appointment.Age = data.Age;
                appointment.MobileNumber = data.MobileNumber;
                appointment.FullAddress = data.FullAddress;
                appointment.DoctorID = data.DoctorID;
                appointment.NurseFirstID = data.NurseFirstID;
                appointment.NurseSecondID = data.NurseSecondID;
                appointment.NurseThirthID = data.NurseThirthID;
                appointment.ProcedureID = data.ProcedureID;
                appointment.StaffName = data.StaffName;
                appointment.RoomID = data.RoomID;
                appointment.Symptom = txtSymptom.Text;
                appointment.AppointmentDate = data.AppointmentDate;
                appointment.OperationDate = data.OperationDate;
                appointment.IsNewCase = chkNewCase.Checked;
                appointment.IsFollowCase = chkFollowUpCase.Checked;
                appointment.OPD = chkOPD.Checked;
                appointment.CreateDate = data.CreateDate;
                appointment.CreateBy = data.CreateBy;
                appointment.EndoscopicCheck = false;
                _db.Appointments.Add(appointment);
            }
            _db.SaveChanges();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete this item ??", "Confirm Delete!!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string status;
                if (patientId > 0)
                {
                    var patient = _db.Patients.FirstOrDefault(x => x.PatientID == patientId);
                    if (patient != null)
                    {
                        _db.Patients.Remove(patient);

                        var appointment = _db.Appointments.FirstOrDefault(x => x.PatientID == patientId);
                        if (appointment != null)
                        {
                            _db.Appointments.Remove(appointment);
                        }
                        var endoscopic = _db.Endoscopics.Where(x => x.PatientID == patientId).ToList();
                        if (endoscopic.Count > 0)
                        {
                            _db.Endoscopics.RemoveRange(endoscopic);
                        }
                        var hist = _db.Histories.Where(x => x.PatientID == patientId).ToList();
                        if (hist.Count > 0)
                        {
                            foreach (var item in hist)
                            {
                                item.IsActive = false;
                            }
                        }
                        var endoLog = _db.Endoscopic_Log.Where(x => x.PatientID == patientId).ToList();
                        if (endoLog != null && endoLog.Count > 0)
                        {
                            foreach (var log in endoLog)
                            {
                                log.IsActive = false;
                            }
                        }

                        if (_db.SaveChanges() > 0) status = Constant.STATUS_SUCCESS; else status = Constant.STATUS_ERROR;
                    }
                    else
                    {
                        status = Constant.STATUS_DATA_NOT_FOUND;
                    }
                }
                else
                {
                    status = Constant.STATUS_DATA_NOT_FOUND;
                }
                MessageBox.Show(status);
                ResetPictureBox();
                this.Controls.ClearControls();
                gridPatient.DataSource = new HistoryModel();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetForm();
            txtHN.Focus();
        }

        private void ResetForm()
        {
            ResetPictureBox();
            this.Controls.ClearControls();
            patientId = 0;
            cbbProcedureList.SelectedIndex = 0;
            cbbDoctorName.SelectedIndex = 0;
            cbbNurseName1.SelectedIndex = 0;
            cbbNurseName2.SelectedIndex = 0;
            cbbNurseName3.SelectedIndex = 0;
            cbbSex.SelectedIndex = 0;
            cbbStation.SelectedIndex = 0;
            cbbOPD.SelectedIndex = 0;
            cbbWard.SelectedIndex = 0;
            cbbAnesthesist.SelectedIndex = 0;
            cbbIndication.SelectedIndex = 0;
            dtBirthDate.Value = DateTime.Today.AddDays(-1);
            gridPatient.DataSource = new HistoryModel();
        }

        private void ResetPictureBox()
        {
            this.pictureBox1.Image = null;
        }

        #region Dropdown
        private void DropdownDoctor()
        {
            cbbDoctorName.ValueMember = "DoctorID";
            cbbDoctorName.DisplayMember = "NameTH";
            cbbDoctorName.DataSource = list.GetEndoscopistList();
            cbbDoctorName.SelectedIndex = 0;
        }
        private void DropdownRoom()
        {
            cbbStation.ValueMember = "RoomID";
            cbbStation.DisplayMember = "NameTH";
            cbbStation.DataSource = list.GetRoomList();
            cbbStation.SelectedIndex = 0;
        }
        private void DropdownNurse(ComboBox comboBox)
        {
            comboBox.ValueMember = "NurseID";
            comboBox.DisplayMember = "NameTH";
            comboBox.DataSource = list.GetNurseList();
            comboBox.SelectedIndex = 0;
        }
        private void DropdownProcedure()
        {
            cbbProcedureList.ValueMember = "ProcedureID";
            cbbProcedureList.DisplayMember = "ProcedureName";
            cbbProcedureList.DataSource = list.GetProcedureList();
            cbbProcedureList.SelectedIndex = 0;
        }
        private void DropdownOPD()
        {
            cbbOPD.ValueMember = "OpdID";
            cbbOPD.DisplayMember = "OpdName";
            cbbOPD.DataSource = list.GetOpdLists();
            cbbOPD.SelectedIndex = 0;
        }
        private void DropdownWard()
        {
            cbbWard.ValueMember = "WardID";
            cbbWard.DisplayMember = "WardName";
            cbbWard.DataSource = list.GetWardLists();
            cbbWard.SelectedIndex = 0;
        }
        private void DropdownAnesthesist()
        {
            cbbAnesthesist.ValueMember = "AnesthesistID";
            cbbAnesthesist.DisplayMember = "NameTH";
            cbbAnesthesist.DataSource = list.GetAnesthesists();
            cbbAnesthesist.SelectedIndex = 0;
        }
        private void DropdownMasterIndication()
        {
            cbbIndication.ValueMember = "IndicationID";
            cbbIndication.DisplayMember = "IndicationName";
            cbbIndication.DataSource = list.GetIndicationList();
            cbbIndication.SelectedIndex = 0;
        }
        private void DropdownFinancial()
        {
            cbbFinancial.ValueMember = "ID";
            cbbFinancial.DisplayMember = "Name";
            cbbFinancial.DataSource = list.GetFinancials();
            cbbFinancial.SelectedIndex = 0;
        }
        private void DropdownICD10()
        {
            _iCD10s = _db.ICD10.ToList();
        }
        
        #endregion

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "C://Desktop";
            openFileDialog1.Title = "Select image to be upload.";
            openFileDialog1.Filter = "Image Only(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            openFileDialog1.FilterIndex = 1;
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (openFileDialog1.CheckFileExists)
                    {
                        System.IO.Path.GetFullPath(openFileDialog1.FileName);
                        pictureBox1.Image = new Bitmap(openFileDialog1.FileName);
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string SavePicture()
        {
            if (!string.IsNullOrWhiteSpace(openFileDialog1.InitialDirectory))
            {
                try
                {
                    string filename = System.IO.Path.GetFileName(openFileDialog1.FileName);
                    string filenameExtension = System.IO.Path.GetExtension(filename);
                    if (filename == null)
                    {
                        MessageBox.Show("Please select a valid image.");
                    }
                    else
                    {
                        string fullDir = path + "\\Images\\Patient\\";
                        if (!Directory.Exists(fullDir))
                        {
                            Directory.CreateDirectory(fullDir);
                        }
                        filename = "Patient-" + DateTime.Now.ToString("ddMMyyyyHHss") + filenameExtension;
                        System.IO.File.Copy(openFileDialog1.FileName, fullDir + filename);

                        string pathImg = path + @"\Images\Patient\" + filename;
                        return pathImg;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "File Already exits");
                }
            }
            return null;
        }

        private void txtCID_KeyPress(object sender, KeyPressEventArgs e)
        {
            Helper.NumberOnly(e);
        }

        private void txtAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            Helper.NumberOnly(e);
        }

        private bool CheckHN(string hn)
        {
            var data = _db.Patients.Where(x => x.HN == hn);
            if (!data.Any()) return false;
            else return true;
        }

        private void gridPatient_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridPatient.CurrentRow.Index != -1)
            {
                string hnNo = txtHN.Text;
                int procedureId = (int)gridPatient.Rows[e.RowIndex].Cells["ProcedureID"].Value;
                int endoscopicId = (int)gridPatient.Rows[e.RowIndex].Cells["EndoScopicID"].Value;
                int appointmentId = (int)gridPatient.Rows[e.RowIndex].Cells["AppointmentID"].Value;

                ReportEndoscopic reportForm = new ReportEndoscopic(hnNo, procedureId, endoscopicId, appointmentId);
                reportForm.Show();
            }
        }

        private void cbbOPD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbOPD.SelectedIndex > 0)
            {
                chkOPD.Checked = true;
            }
            else
            {
                chkOPD.Checked = false;
            }
        }

        private void cbbWard_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbWard.SelectedIndex > 0)
            {
                chkWard.Checked = true;
            }
            else
            {
                chkWard.Checked = false;
            }
        }

        private void txbRefer_KeyUp(object sender, KeyEventArgs e)
        {
            if (txbRefer.TextLength > 0)
            {
                chkRefer.Checked = true;
            }
            else
            {
                chkRefer.Checked = false;
            }
        }

        //private void dtBirthDate_ValueChanged(object sender, EventArgs e)
        //{
        //    txtAge.Text = (DateTime.Today.Year - dtBirthDate.Value.Year).ToString();
        //}

        private void LoadTextBoxAutoComplete(TextBox textBox)
        {
            if (_iCD10s != null && _iCD10s.Count > 0)
            {
                AutoCompleteStringCollection ac = new AutoCompleteStringCollection();
                foreach (var item in _iCD10s)
                {
                    ac.Add(item.Code);
                }
                textBox.AutoCompleteCustomSource = ac;
            }
        }

        private void cbbProcedureList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTextBoxAutoComplete(txbPreDiag1Code);
            LoadTextBoxAutoComplete(txbPreDiag2Code);
        }

        private void txbPreDiag1Code_TextChanged(object sender, EventArgs e)
        {
            if (txbPreDiag1Code.TextLength > 0)
            {
                var selectedIcd10 = _iCD10s.Where(w => w.Code == txbPreDiag1Code.Text).FirstOrDefault();
                if (selectedIcd10 != null)
                {
                    txbPreDiag1ID.Text = selectedIcd10.ID.ToString();
                    txbPreDiag1Text.Text = selectedIcd10.Name;
                }
                else
                {
                    txbPreDiag1ID.Clear();
                    txbPreDiag1Text.Clear();
                }
            }
            else
            {
                txbPreDiag1ID.Clear();
                txbPreDiag1Text.Clear();
            }
        }

        private void txbPreDiag2Code_TextChanged(object sender, EventArgs e)
        {
            if (txbPreDiag2Code.TextLength > 0)
            {
                var selectedIcd10 = _iCD10s.Where(w => w.Code == txbPreDiag2Code.Text).FirstOrDefault();
                if (selectedIcd10 != null)
                {
                    txbPreDiag2ID.Text = selectedIcd10.ID.ToString();
                    txbPreDiag2Text.Text = selectedIcd10.Name;
                }
                else
                {
                    txbPreDiag2ID.Clear();
                    txbPreDiag2Text.Clear();
                }
            }
            else
            {
                txbPreDiag2ID.Clear();
                txbPreDiag2Text.Clear();
            }
        }
    }

    public class HistoryModel
    {
        //public int PatientID { get; set; }
        public int? EndoScopicID { get; set; }
        public int? ProcedureID { get; set; }
        public DateTime? OperatingDate { get; set; }
        public string Symptom { get; set; }
        public string Procedure { get; set; }
        public string Doctor { get; set; }
        public string Diagnosis { get; set; }
        public int AppointmentID { get; set; }
    }
}
