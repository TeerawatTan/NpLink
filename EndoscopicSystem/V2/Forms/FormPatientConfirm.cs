using EndoscopicSystem.Entities;
using EndoscopicSystem.V2.Forms.src;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EndoscopicSystem.V2.Forms
{
    public partial class FormPatientConfirm : Form
    {
        private int _id, _patientId, _procedureId, _endoscopicId, _appointmentId, _settingId;
        private string _hnNo, defaultData = "......";
        private readonly EndoscopicEntities _db = new EndoscopicEntities();
        private readonly DropdownListService _dropdownListService = new DropdownListService();
        private bool _isEgdAndColono = false;

        public FormPatientConfirm(int userID, string hn = "", int procId = 0, int endoId = 0, int apId = 0)
        {
            InitializeComponent();
            _id = userID;
            _hnNo = hn;
            _procedureId = procId;
            _endoscopicId = endoId;
            _appointmentId = apId;
        }

        private void FormPatientConfirm_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_hnNo) && _procedureId > 0)
            {
                SearchHN(_hnNo);
            }
            else
            {
                InitialData(defaultData, defaultData, defaultData, defaultData);
            }
            _dropdownListService.DropdownInstrumentIdAndCode(cbbInstrument1, 1);
            if (_procedureId == 6)
            {
                _isEgdAndColono = true;

                _dropdownListService.DropdownInstrumentIdAndCode(cbbInstrument2, 1);
                cbbInstrument2.Visible = true;
                SerialNumber2.Visible = true;
            }

            _dropdownListService.DropdownProfileSettingCamera(cbbProfileSettingCanera);
            _settingId = cbbProfileSettingCanera.SelectedValue == null ? 0 : Convert.ToInt32(cbbProfileSettingCanera.SelectedValue);
        }

        private void cbbInstrument1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? instruDdlId = (int?)cbbInstrument1.SelectedValue;

            if (instruDdlId == null || instruDdlId < 0)
                return;

            var instrument = _db.Instruments.Where(w => w.ID == instruDdlId).FirstOrDefault();
            if (instrument != null)
                SerialNumber1.Text = instrument.SerialNumber;
            else
                SerialNumber1.Clear();
        }

        private void cbbInstrument2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? instruDdlId = (int?)cbbInstrument2.SelectedValue;

            if (instruDdlId == null || instruDdlId < 0)
                return;

            var instrument = _db.Instruments.Where(w => w.ID == instruDdlId).FirstOrDefault();
            if (instrument != null)
                SerialNumber2.Text = instrument.SerialNumber;
            else
                SerialNumber2.Clear();
        }

        private void cbbProfileSettingCanera_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? settingId = (int?)cbbProfileSettingCanera.SelectedValue;
            if (settingId == null || settingId < 0)
                return;

            var settingCamera = _db.SettingDevices.Where(w => w.ID == settingId).FirstOrDefault();
            if (settingCamera != null)
            {
                if ((settingCamera.CrpWidth == null || settingCamera.CrpWidth == 0) && (settingCamera.CrpHeight == null || settingCamera.CrpHeight == 0))
                {
                    txbProfileSettingCanera.Text = settingCamera.AspectRatio;
                }
                else
                {
                    txbProfileSettingCanera.Text = string.Format("{0}*{1} px", settingCamera.CrpWidth, settingCamera.CrpHeight);
                }
                _settingId = settingCamera.ID;
            }
            else
            {
                txbProfileSettingCanera.Clear();
            }
        }

        private void InitialData(string fullName, string hnNum, string doctorList, string nurseList)
        {
            // Initial data
            lbName.Text = fullName;
            lbHn.Text = hnNum;
            lbDoctorList.Text = doctorList;
            lbNurseList.Text = nurseList;
        }

        private void SearchHN(string hn)
        {
            bool hasData = true;
            string doctorName = "";
            string[] nurseNameList = null;
            try
            {
                var getPatient = _db.Patients.FirstOrDefault(x => x.HN == hn && (x.IsActive.HasValue && x.IsActive.Value));
                if (getPatient != null)
                {
                    var getEndos = (from e in _db.Endoscopics
                                    join p in _db.Patients on e.PatientID equals p.PatientID
                                    where e.EndoscopicID == _endoscopicId && e.ProcedureID == _procedureId && e.PatientID == getPatient.PatientID && e.IsSaved
                                    select e).ToList();
                    if (getEndos != null && getEndos.Count > 0)
                    {
                        Endoscopic getEndo = getEndos.OrderByDescending(o => o.CreateDate).FirstOrDefault();
                        if (getEndo.NewCase.HasValue && getEndo.NewCase.Value)
                        {
                            getEndo.FollowUpCase = true;
                            getEndo.NewCase = false;
                        }
                        _endoscopicId = getEndo.EndoscopicID;
                    }
                    else
                    {
                        DateTime stampNow = System.DateTime.Now;

                        Endoscopic endoscopic = new Endoscopic()
                        {
                            PatientID = getPatient.PatientID,
                            IsSaved = false,
                            ProcedureID = _procedureId,
                            CreateBy = _id,
                            CreateDate = stampNow
                        };

                        int resultInsertEndo = InsertEndoscopic(endoscopic);
                        if (resultInsertEndo < 0)
                        {
                            MessageBox.Show("Error, Insert Endoscopic isn't completed.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        var endo = _db.Endoscopics.OrderByDescending(x => x.EndoscopicID).FirstOrDefault();
                        if (endo != null)
                        {
                            _endoscopicId = endo.EndoscopicID;
                        }

                        //if (_procedureId == 9)
                        //{
                        //    FindingCystoscope findingCystoscope = new FindingCystoscope()
                        //    {
                        //        EndoscopicID = _endoscopicId
                        //    };
                        //    _ = InsertFindingCystoscope(findingCystoscope);
                        //}
                        //else
                        //{
                        //    Finding finding = new Finding()
                        //    {
                        //        PatientID = getPatient.PatientID,
                        //        CreateBy = _id,
                        //        CreateDate = stampNow,
                        //        EndoscopicID = _endoscopicId
                        //    };
                        //    _ = InsertFinding(finding);
                        //}
                    }

                    doctorName = _db.Doctors.FirstOrDefault(f => f.DoctorID == getPatient.DoctorID)?.NameTH;
                    nurseNameList = _db.Nurses.Where(w => w.IsActive.HasValue && w.IsActive.Value && w.NurseID == getPatient.NurseFirstID || w.NurseID == getPatient.NurseSecondID || w.NurseID == getPatient.NurseThirthID).Select(s => s.NameTH).ToArray();

                    string allNurse = nurseNameList == null || nurseNameList.Length == 0 ? "" : string.Join("," + Environment.NewLine, nurseNameList);

                    InitialData(getPatient.Fullname, hn, doctorName, allNurse);
                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมูลผู้ป่วย", "Data Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    hasData = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                hasData = false;
            }

            if (!hasData)
            {
                DashboardForm dashboardForm = new DashboardForm(_id);
                dashboardForm.Show();
                this.Close();
            }
        }

        private int InsertEndoscopic(Endoscopic endoscopic)
        {
            _db.Endoscopics.Add(endoscopic);
            return _db.SaveChanges();
        }

        //private int InsertFinding(Finding finding)
        //{
        //    _db.Findings.Add(finding);
        //    return _db.SaveChanges();
        //}

        //private int InsertFindingCystoscope(FindingCystoscope finding)
        //{
        //    _db.FindingCystoscopes.Add(finding);
        //    return _db.SaveChanges();
        //}

        private bool UpdateAllData(int appointmentId)
        {
            try
            {
                Appointment getAppointment = _db.Appointments.Where(w => w.AppointmentID == appointmentId).FirstOrDefault();
                if (getAppointment != null && getAppointment.AppointmentID > 0)
                {
                    getAppointment.Instrument1ID = (int?)cbbInstrument1.SelectedValue;
                    getAppointment.Instrument2ID = (int?)cbbInstrument2.SelectedValue;
                }

                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            bool isSave = UpdateAllData(_appointmentId);
            if (!isSave)
            {
                MessageBox.Show("Update data is not completed.", "Save Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.Hide();

            try
            {
                // Form Panel
                FormProceed formProceed = new FormProceed(_id, _hnNo, _patientId, _procedureId, _endoscopicId, _appointmentId, null, _isEgdAndColono, _settingId);
                formProceed.ShowDialog();
                formProceed = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public class InstrumentConfirmModel
    {
        public int No { get; set; }
        public string Name { get; set; }
    }
}
