using EndoscopicSystem.Constants;
using EndoscopicSystem.Entities;
using EndoscopicSystem.Forms;
using EndoscopicSystem.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EndoscopicSystem
{
    public partial class PatientForm : Form
    {
        readonly EndoscopicEntities db = new EndoscopicEntities();
        protected readonly GetDropdownList list = new GetDropdownList();
        private int patientId = 0;
        private int procedureId = 0;
        private readonly int UserID;
        string path = Application.StartupPath.Replace("\\bin\\Debug", "");
        private string hnNo = "";
        public PatientForm(int userID, string hn = "", int procId = 0)
        {
            InitializeComponent();
            UserID = userID;
            hnNo = hn;
            procedureId = procId;
        }

        private void PatientForm_Load(object sender, EventArgs e)
        {
            dtAppointmentDate.Value = DateTime.Now;
            dtAppointmentTime.Value = DateTime.Now;
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
            DropdownAnesthesistMethod(cbbAnesthesiaMethod1);
            DropdownAnesthesistMethod(cbbAnesthesiaMethod2);
            DropdownMasterIndication();
            DropdownPreDiagnosis(cbbPreDiagnosis1);
            DropdownPreDiagnosis(cbbPreDiagnosis2);

            gridPatient.DataSource = new HistoryModel();

            if (!string.IsNullOrWhiteSpace(hnNo) && procedureId > 0)
            {
                SearchHN(hnNo, procedureId);
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
                var data = db.Patients.Where(x => x.IsActive.HasValue && x.IsActive.Value).ToList();
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
                    txbFinancial.Text = response.Financial;
                    cbbDoctorName.SelectedValue = response.DoctorID ?? 0;
                    txtFullName.Text = response.Fullname;
                    txtHN.Text = response.HN;
                    cbbNurseName1.SelectedValue = response.NurseFirstID ?? 0;
                    cbbNurseName2.SelectedValue = response.NurseSecondID ?? 0;
                    cbbNurseName3.SelectedValue = response.NurseThirthID ?? 0;
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
                    cbbOPD.SelectedIndex = response.OpdID ?? 0;
                    cbbWard.SelectedIndex = response.WardID ?? 0;
                    cbbAnesthesist.SelectedIndex = response.AnesthesistID ?? 0;
                    cbbAnesthesiaMethod1.SelectedIndex = response.AnesthesistMethodFirstID ?? 0;
                    cbbAnesthesiaMethod2.SelectedIndex = response.AnesthesistMethodSecondID ?? 0;
                    cbbIndication.SelectedIndex = response.IndicationID ?? 0;

                    procedureId = response.ProcedureID ?? 0;
                    Appointment app = db.Appointments.Where(x => txtHN.Text.Equals(x.HN) && x.ProcedureID == procedureId).OrderByDescending(x => x.AppointmentID).FirstOrDefault();
                    if (app != null)
                    {
                        txtSymptom.Text = app.Symptom;
                        chkNewCase.Checked = app.IsNewCase ?? false;
                        chkFollowUpCase.Checked = app.IsFollowCase ?? false;
                        chkOPD.Checked = app.OPD ?? false;
                        chkIPD.Checked = app.IPD ?? false;
                    }

                    var history = db.v_HistoryEndoscopic
                                .Where(x => x.PatientID == patientId)
                                .ToList();
                    if (history != null && history.Count > 0)
                    {
                        history = history.OrderByDescending(o => o.CreateDate).ToList();
                        foreach (var item in history)
                        {
                            var model = new HistoryModel();
                            model.EndoScopicID = item.EndoscopicID;
                            model.ProcedureID = item.ProcedureID;
                            model.OperatingDate = item.CreateDate;
                            model.Symptom = item.Symptom;
                            model.Procedure = item.ProcedureName;
                            model.Doctor = item.Doctor;
                            model.Diagnosis = item.Diagnosis;
                            list.Add(model);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมูลผู้ป่วย");
                    ResetPictureBox();
                    this.Controls.ClearControls();
                }
                gridPatient.DataSource = list;
                gridPatient.Columns["EndoScopicID"].Visible = false;
                gridPatient.Columns["ProcedureID"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if ((int)cbbProcedureList.SelectedValue == 0)
            {
                MessageBox.Show("กรุณาเลือก Procedure List");
                return;
            }

            string status;
            bool? gender;
            if (cbbSex.SelectedItem.ToString().Equals(Constant.Male)) gender = true;
            else if (cbbSex.SelectedItem.ToString().Equals(Constant.FeMale)) gender = false;
            else gender = null;

            if (!string.IsNullOrWhiteSpace(txtHN.Text))
            {
                try
                {
                    var data = db.Patients.Where(x => x.HN == txtHN.Text).FirstOrDefault();
                    if (data != null)
                    {
                        procedureId = (int)cbbProcedureList.SelectedValue;
                        data.Fullname = txtFullName.Text;
                        data.CardID = txtCID.Text;
                        data.Sex = gender;
                        data.Age = (!string.IsNullOrWhiteSpace(txtAge.Text)) ? Convert.ToInt32(txtAge.Text) : 0;
                        data.DoctorID = (int?)cbbDoctorName.SelectedValue;
                        data.NurseFirstID = (int?)cbbNurseName1.SelectedValue;
                        data.NurseSecondID = (int?)cbbNurseName2.SelectedValue;
                        data.NurseThirthID = (int?)cbbNurseName3.SelectedValue;
                        data.ProcedureID = (int?)cbbProcedureList.SelectedValue;
                        data.StaffName = txtStaffName.Text;
                        data.RoomID = (int?)cbbStation.SelectedValue;
                        data.AppointmentDate = new DateTime(
                            dtAppointmentDate.Value.Year,
                            dtAppointmentDate.Value.Month,
                            dtAppointmentDate.Value.Day,
                            dtAppointmentTime.Value.Hour,
                            dtAppointmentTime.Value.Minute,
                            dtAppointmentTime.Value.Second);
                        data.OperationDate = new DateTime(
                            dtOperatingDate.Value.Year,
                            dtOperatingDate.Value.Month,
                            dtOperatingDate.Value.Day,
                            dtOperatingTime.Value.Hour,
                            dtOperatingTime.Value.Minute,
                            dtOperatingTime.Value.Second);

                        data.PicturePath = SavePicture() ?? data.PicturePath;
                        data.Financial = txbFinancial.Text;
                        data.OpdID = (int?)cbbOPD.SelectedValue;
                        data.WardID = (int?)cbbWard.SelectedValue;
                        data.ReferCheck = chkRefer.Checked;
                        data.ReferDetail = txbRefer.Text;
                        data.AnesthesistID = (int?)cbbAnesthesist.SelectedValue;
                        data.AnesthesistMethodFirstID = (int?)cbbAnesthesiaMethod1.SelectedValue;
                        data.AnesthesistMethodSecondID = (int?)cbbAnesthesiaMethod2.SelectedValue;
                        data.IndicationID = (int?)cbbIndication.SelectedValue;
                        data.PreDiagnosisFirstID = (int?)cbbPreDiagnosis1.SelectedValue;
                        data.PreDiagnosisSecondID = (int?)cbbPreDiagnosis2.SelectedValue;
                        data.UpdateBy = UserID;
                        data.UpdateDate = DateTime.Now;
                        db.Entry(data).State = System.Data.Entity.EntityState.Modified;
                        if (db.SaveChanges() > 0)
                        {
                            SaveAppointment(txtHN.Text, data);
                            status = Constant.STATUS_SUCCESS;
                            this.Controls.ClearControls();
                        }
                        else
                        {
                            status = Constant.STATUS_ERROR;
                        }
                    }
                    else
                    {
                        //New Patient
                        if (this.CheckHN(txtHN.Text))
                        {
                            MessageBox.Show("มี HN นี้อยู่ในระบบแล้ว");
                            this.Controls.ClearControls();
                            return;
                        }
                        Patient patient = new Patient();
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
                        patient.Financial = txbFinancial.Text;
                        patient.OpdID = (int?)cbbOPD.SelectedValue;
                        patient.WardID = (int?)cbbWard.SelectedValue;
                        patient.ReferCheck = chkRefer.Checked;
                        patient.ReferDetail = txbRefer.Text;
                        patient.AnesthesistID = (int?)cbbAnesthesist.SelectedValue;
                        patient.AnesthesistMethodFirstID = (int?)cbbAnesthesiaMethod1.SelectedValue;
                        patient.AnesthesistMethodSecondID = (int?)cbbAnesthesiaMethod2.SelectedValue;
                        patient.IndicationID = (int?)cbbIndication.SelectedValue;
                        patient.PreDiagnosisFirstID = (int?)cbbPreDiagnosis1.SelectedValue;
                        patient.PreDiagnosisSecondID = (int?)cbbPreDiagnosis2.SelectedValue;
                        patient.IsActive = true;
                        patient.CreateBy = UserID;
                        patient.CreateDate = DateTime.Now;
                        db.Patients.Add(patient);
                        if (db.SaveChanges() > 0)
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
                    ResetPictureBox();
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
                    cbbAnesthesiaMethod1.SelectedIndex = 0;
                    cbbAnesthesiaMethod2.SelectedIndex = 0;
                    cbbIndication.SelectedIndex = 0;
                    cbbPreDiagnosis1.SelectedIndex = 0;
                    cbbPreDiagnosis2.SelectedIndex = 0;
                    MessageBox.Show(status);
                    gridPatient.DataSource = new HistoryModel();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void SaveAppointment(string hn, Patient data)
        {
            Appointment ap = db.Appointments.Where(x => hn.Equals(x.HN) && x.ProcedureID == procedureId && x.AppointmentDate.HasValue && x.AppointmentDate == data.AppointmentDate).OrderByDescending(x => x.AppointmentID).FirstOrDefault();
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
                ap.IPD = chkIPD.Checked;
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
                appointment.IPD = chkIPD.Checked;
                appointment.CreateDate = data.CreateDate;
                appointment.CreateBy = data.CreateBy;
                appointment.EndoscopicCheck = false;
                db.Appointments.Add(appointment);
            }
            db.SaveChanges();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete this item ??", "Confirm Delete!!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string status;
                if (patientId > 0)
                {
                    var patient = db.Patients.FirstOrDefault(x => x.PatientID == patientId);
                    if (patient != null)
                    {
                        db.Patients.Remove(patient);

                        var appointment = db.Appointments.FirstOrDefault(x => x.PatientID == patientId);
                        if (appointment != null)
                        {
                            db.Appointments.Remove(appointment);
                        }
                        var endoscopic = db.Endoscopics.Where(x => x.PatientID == patientId).ToList();
                        if (endoscopic.Count > 0)
                        {
                            db.Endoscopics.RemoveRange(endoscopic);
                        }
                        var hist = db.Histories.Where(x => x.PatientID == patientId).ToList();
                        if (hist.Count > 0)
                        {
                            foreach (var item in hist)
                            {
                                item.IsActive = false;
                            }
                        }
                        var endoLog = db.Endoscopic_Log.Where(x => x.PatientID == patientId).ToList();
                        if (endoLog != null && endoLog.Count > 0)
                        {
                            foreach (var log in endoLog)
                            {
                                log.IsActive = false;
                            }
                        }

                        if (db.SaveChanges() > 0) status = Constant.STATUS_SUCCESS; else status = Constant.STATUS_ERROR;
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
            this.Controls.ClearControls();
            ResetPictureBox();
            patientId = 0;
            gridPatient.DataSource = new HistoryModel();
            txtHN.Focus();
        }

        private void ResetPictureBox()
        {
            this.pictureBox1.Image = null;
        }

        #region Dropdown
        public void DropdownDoctor()
        {
            cbbDoctorName.ValueMember = "DoctorID";
            cbbDoctorName.DisplayMember = "NameTH";
            cbbDoctorName.DataSource = list.GetEndoscopistList();
            cbbDoctorName.SelectedIndex = 0;
        }
        public void DropdownRoom()
        {
            cbbStation.ValueMember = "RoomID";
            cbbStation.DisplayMember = "NameTH";
            cbbStation.DataSource = list.GetRoomList();
            cbbStation.SelectedIndex = 0;
        }
        public void DropdownNurse(ComboBox comboBox)
        {
            comboBox.ValueMember = "NurseID";
            comboBox.DisplayMember = "NameTH";
            comboBox.DataSource = list.GetNurseList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownProcedure()
        {
            cbbProcedureList.ValueMember = "ProcedureID";
            cbbProcedureList.DisplayMember = "ProcedureName";
            cbbProcedureList.DataSource = list.GetProcedureList();
            cbbProcedureList.SelectedIndex = 0;
        }
        public void DropdownOPD()
        {
            cbbOPD.ValueMember = "OpdID";
            cbbOPD.DisplayMember = "OpdName";
            cbbOPD.DataSource = list.GetOpdLists();
            cbbOPD.SelectedIndex = 0;
        }
        public void DropdownWard()
        {
            cbbWard.ValueMember = "WardID";
            cbbWard.DisplayMember = "WardName";
            cbbWard.DataSource = list.GetWardLists();
            cbbWard.SelectedIndex = 0;
        }
        public void DropdownAnesthesist()
        {
            cbbAnesthesist.ValueMember = "AnesthesistID";
            cbbAnesthesist.DisplayMember = "NameTH";
            cbbAnesthesist.DataSource = list.GetAnesthesists();
            cbbAnesthesist.SelectedIndex = 0;
        }
        public void DropdownAnesthesistMethod(ComboBox comboBox)
        {
            comboBox.ValueMember = "ID";
            comboBox.DisplayMember = "Name";
            comboBox.DataSource = list.GetAnesthesistMethods();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownMasterIndication()
        {
            cbbIndication.ValueMember = "IndicationID";
            cbbIndication.DisplayMember = "IndicationName";
            cbbIndication.DataSource = list.GetIndicationList();
            cbbIndication.SelectedIndex = 0;
        }
        public void DropdownPreDiagnosis(ComboBox comboBox)
        {
            comboBox.ValueMember = "ID";
            comboBox.DisplayMember = "Name";
            comboBox.DataSource = list.GetICD10s();
            comboBox.SelectedIndex = 0;
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
            var data = db.Patients.Where(x => x.HN == hn);
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

                ReportEndoscopic reportForm = new ReportEndoscopic(hnNo, procedureId, endoscopicId);
                reportForm.Show();
            }
        }

        private void PatientForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
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
    }
}
