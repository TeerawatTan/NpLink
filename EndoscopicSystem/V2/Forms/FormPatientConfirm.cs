using EndoscopicSystem.Entities;
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
        private int UserID;
        private string hnNo;
        private int procedureId;
        private int endoscopicId;
        private int appointmentId;
        private string defaultData = "......";
        private readonly EndoscopicEntities db = new EndoscopicEntities();

        public FormPatientConfirm(int userID, string hn = "", int procId = 0, int endoId = 0, int apId = 0)
        {
            InitializeComponent();
            UserID = userID;
            hnNo = hn;
            procedureId = procId;
            endoscopicId = endoId;
            appointmentId = apId;
        }

        private void FormPatientConfirm_Load(object sender, EventArgs e)
        {
            var v = db.Users.Where(x => x.Id == UserID).Select(x => new { x.AspectRatioID, x.PositionCrop }).FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(hnNo) && procedureId > 0)
            {
                SearchHN(hnNo, procedureId);
            }
            else
            {
                InitialData(defaultData, defaultData, defaultData, defaultData);
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
                var getPatient = db.Patients.FirstOrDefault(x => x.HN == hn && (x.IsActive.HasValue && x.IsActive.Value));
                if (getPatient != null)
                {
                    var getEndos = (from e in db.Endoscopics
                                    join p in db.Patients on e.PatientID equals p.PatientID
                                    where e.EndoscopicID == endoscopicId && e.ProcedureID == procedureId && e.PatientID == getPatient.PatientID && e.IsSaved
                                    select e).ToList();
                    if (getEndos != null && getEndos.Count > 0)
                    {
                        Endoscopic getEndo = getEndos.OrderByDescending(o => o.CreateDate).FirstOrDefault();
                        if (getEndo.NewCase.HasValue && getEndo.NewCase.Value)
                        {
                            getEndo.FollowUpCase = true;
                            getEndo.NewCase = false;
                        }
                        endoscopicId = getEndo.EndoscopicID;
                        Finding getFinding = db.Findings.Where(x => x.FindingID == getEndo.FindingID).FirstOrDefault();
                        Indication getIndication = db.Indications.Where(x => x.IndicationID == getEndo.IndicationID).FirstOrDefault();
                        Speciman getSpecimen = db.Specimen.Where(x => x.SpecimenID == getEndo.SpecimenID).FirstOrDefault();
                        Intervention getIntervention = db.Interventions.Where(x => x.InterventionID == getEndo.InterventionID).FirstOrDefault();
                    }
                    else
                    {
                        Endoscopic endoscopic = new Endoscopic()
                        {
                            PatientID = getPatient.PatientID,
                            IsSaved = false,
                            ProcedureID = procedureId,
                            CreateBy = UserID,
                            CreateDate = System.DateTime.Now
                        };
                        db.Endoscopics.Add(endoscopic);

                        Finding finding = new Finding() { PatientID = getPatient.PatientID, CreateBy = UserID, CreateDate = System.DateTime.Now };
                        db.Findings.Add(finding);
                        db.SaveChanges();

                        var endos = db.Endoscopics.ToList();
                        if (endos.Count > 0)
                        {
                            Endoscopic endo = endos.OrderByDescending(x => x.EndoscopicID).FirstOrDefault();
                            endoscopicId = endo.EndoscopicID;
                        }
                    }

                    doctorName = db.Doctors.FirstOrDefault(f => f.DoctorID == getPatient.DoctorID)?.NameTH;
                    nurseNameList = db.Nurses.Where(w => w.IsActive.HasValue && w.IsActive.Value && w.NurseID == getPatient.NurseFirstID || w.NurseID == getPatient.NurseSecondID || w.NurseID == getPatient.NurseThirthID).Select(s => s.NameTH).ToArray();

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
                this.Close();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Hide();

            FormLive formLive = new FormLive(UserID, hnNo, procedureId, endoscopicId, appointmentId);
            formLive.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
