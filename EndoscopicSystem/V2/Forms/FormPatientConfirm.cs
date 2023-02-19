﻿using EndoscopicSystem.Entities;
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
        private int _id, _patientId, _procedureId, _endoscopicId, _appointmentId;
        private string _hnNo, defaultData = "......";
        private readonly EndoscopicEntities _db = new EndoscopicEntities();
        private readonly DropdownListService _dropdownListService = new DropdownListService();

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
            var v = _db.Users.Where(x => x.Id == _id).Select(x => new { x.AspectRatioID, x.PositionCrop }).FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(_hnNo) && _procedureId > 0)
            {
                SearchHN(_hnNo, _procedureId);
            }
            else
            {
                InitialData(defaultData, defaultData, defaultData, defaultData);
            }
            _dropdownListService.DropdownInstrument(cbbInstrument1, 1);
            if (_procedureId == 6)
            {
                _dropdownListService.DropdownInstrument(cbbInstrument2, 1);
            }
        }

        private void cbbInstrument1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cbbInstrument1.Text))
                return;

            string[] instruSplt = cbbInstrument1.Text.ToString().Split('-');
            if (instruSplt.Length > 0)
            {
                SerialNumber1.Text = instruSplt[1].Trim() ?? "";
            }
        }

        private void cbbInstrument2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cbbInstrument2.Text))
                return;

            string[] instruSplt = cbbInstrument2.Text.ToString().Split('-');
            if (instruSplt.Length > 0)
            {
                SerialNumber2.Text = instruSplt[1].Trim() ?? "";
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

        private void SearchHN(string hn, int procId = 0)
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

                        //Finding getFinding = _db.Findings.Where(x => x.FindingID == getEndo.FindingID).FirstOrDefault();
                        //Indication getIndication = _db.Indications.Where(x => x.IndicationID == getEndo.IndicationID).FirstOrDefault();
                        //Speciman getSpecimen = _db.Specimen.Where(x => x.SpecimenID == getEndo.SpecimenID).FirstOrDefault();
                        //Intervention getIntervention = _db.Interventions.Where(x => x.InterventionID == getEndo.InterventionID).FirstOrDefault();
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
                        _db.Endoscopics.Add(endoscopic);

                        Finding finding = new Finding() { PatientID = getPatient.PatientID, CreateBy = _id, CreateDate = stampNow };
                        _db.Findings.Add(finding);
                        _db.SaveChanges();

                        var endo = _db.Endoscopics.OrderByDescending(x => x.EndoscopicID).FirstOrDefault();
                        if (endo != null)
                        {
                            _endoscopicId = endo.EndoscopicID;
                        }
                    }

                    doctorName = _db.Doctors.FirstOrDefault(f => f.DoctorID == getPatient.DoctorID)?.NameTH;
                    nurseNameList = _db.Nurses.Where(w => w.IsActive.HasValue && w.IsActive.Value && w.NurseID == getPatient.NurseFirstID || w.NurseID == getPatient.NurseSecondID || w.NurseID == getPatient.NurseThirthID).Select(s => s.NameTH).ToArray();

                    string allNurse = nurseNameList == null || nurseNameList.Length == 0 ? "" : string.Join("," + Environment.NewLine, nurseNameList);

                    InitialData(getPatient.Fullname, hn, doctorName, allNurse);
                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมูลผู้ป่วย");
                    hasData = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                hasData = false;
            }

            if (!hasData)
            {
                DashboardForm dashboardForm = new DashboardForm(_id);
                dashboardForm.Show();
                this.Close();
            }
        }

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
                FormProceed formProceed = new FormProceed(_id, _hnNo, _procedureId, _endoscopicId, _appointmentId);
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