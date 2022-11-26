using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using EndoscopicSystem.Constants;
using EndoscopicSystem.Entities;
using EndoscopicSystem.Repository;
using EndoscopicSystem.V2.Forms.src;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EndoscopicSystem.V2.Forms
{
    public partial class FormProcedure : Form
    {
        private string _reportPath = Application.StartupPath.Replace("\\bin\\Debug", "") + @"\Report\";
        private string _pathFolderPDF = Application.StartupPath.Replace("\\bin\\Debug", "") + @"\Pdf\";
        private string _pathFolderDicom = Application.StartupPath.Replace("\\bin\\Debug", "") + @"\Dicom\";
        private string _hnNo;
        private int _id, _procedureId, _appointmentId, _patientId, _endoscopicId;
        private readonly GetDropdownList _dropdownRepo = new GetDropdownList();
        private readonly EndoscopicEntities _db = new EndoscopicEntities();
        private readonly DropdownListService _dropdownListService = new DropdownListService();
        public Dictionary<int, string> _imgPath = new Dictionary<int, string>();
        private TextBox lastFocused;
        public FormProcedure(int id, string hn, int procId, int appId)
        {
            InitializeComponent();

            this._id = id;
            this._hnNo = hn;
            this._procedureId = procId;
            this._appointmentId = appId;
        }

        private void FormProcedure_Load(object sender, EventArgs e)
        {
            cbbProcedureList.ValueMember = "ProcedureID";
            cbbProcedureList.DisplayMember = "ProcedureName";
            cbbProcedureList.DataSource = _dropdownRepo.GetProcedureList();
            cbbProcedureList.SelectedIndex = 0;
            cbbProcedureList.Enabled = true;
            if (_procedureId <= 0)
            {
                btnReport.Visible = false;
            }
            else
            {
                cbbProcedureList.SelectedValue = _procedureId;
                cbbProcedureList.Enabled = false;
                OpenTabPage(_procedureId);
                SearchHN(_hnNo, _procedureId);

                btnReport.Visible = true;
            }

            listBox1.Items.Clear();

            LoadTextBoxAutoComplete(txtPictureBoxSaved1);
            LoadTextBoxAutoComplete(txtPictureBoxSaved2);
            LoadTextBoxAutoComplete(txtPictureBoxSaved3);
            LoadTextBoxAutoComplete(txtPictureBoxSaved4);
            LoadTextBoxAutoComplete(txtPictureBoxSaved5);
            LoadTextBoxAutoComplete(txtPictureBoxSaved7);
            LoadTextBoxAutoComplete(txtPictureBoxSaved8);
            LoadTextBoxAutoComplete(txtPictureBoxSaved9);
            LoadTextBoxAutoComplete(txtPictureBoxSaved10);
            LoadTextBoxAutoComplete(txtPictureBoxSaved11);
            LoadTextBoxAutoComplete(txtPictureBoxSaved12);
            LoadTextBoxAutoComplete(txtPictureBoxSaved13);
            LoadTextBoxAutoComplete(txtPictureBoxSaved14);
            LoadTextBoxAutoComplete(txtPictureBoxSaved15);
            LoadTextBoxAutoComplete(txtPictureBoxSaved16);
            LoadTextBoxAutoComplete(txtPictureBoxSaved17);
            LoadTextBoxAutoComplete(txtPictureBoxSaved18);
        }

        private void cbbProcedureList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbProcedureList.SelectedIndex <= 0)
            {
                RemoveTabPage();
                btnReport.Visible = false;
                return;
            }
            else
            {
                OpenTabPage(cbbProcedureList.SelectedIndex);
                btnReport.Visible = true;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string curItem = listBox1.SelectedItem.ToString();
            int index = listBox1.FindString(curItem);
            string[] subStrItem = curItem.Split('-');
            if (index == -1)
            {
                return;
            }
            else
            {
                if (lastFocused != null)
                {
                    if (lastFocused.Name == txbPreDiagCode_EGD.Name)
                    {
                        txbPreDiagCode_EGD.Text = subStrItem[0].Trim();
                    }
                    else if (lastFocused.Name == txbICD10Code_EGD.Name)
                    {
                        txbICD10Code_EGD.Text = subStrItem[0].Trim();
                    }
                }
            }

            listBox1.Items.Clear();
        }

        private async void txbPreDiagCode_EGD_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            var ItemObject = await GetIcd10();
            listBox1.Items.AddRange(ItemObject);

            lastFocused = (TextBox)sender;
        }

        private async void txbICD10Code_EGD_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

           var ItemObject = await GetIcd10();
            listBox1.Items.AddRange(ItemObject);
            lastFocused = (TextBox)sender;
        }

        private void ExportEndoscopic(string hn, int proc, int endosId)
        {
            ReportDocument rprt = new ReportDocument();
            TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
            TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
            ConnectionInfo crConnectionInfo = new ConnectionInfo();
            Tables CrTables;

            crConnectionInfo.ServerName = ConfigurationManager.AppSettings["dataSource"];
            crConnectionInfo.DatabaseName = ConfigurationManager.AppSettings["catalog"];
            crConnectionInfo.UserID = ConfigurationManager.AppSettings["loginUser"];
            crConnectionInfo.Password = ConfigurationManager.AppSettings["loginPassword"];

            if (proc == 1)
            {
                rprt.Load(_reportPath + "GastroscoryReport.rpt");
            }
            else if (proc == 2)
            {
                rprt.Load(_reportPath + "ColonoscopyReport.rpt");
            }
            else if (proc == 3)
            {
                rprt.Load(_reportPath + "EndoscopicReport.rpt");
            }
            else if (proc == 4)
            {
                rprt.Load(_reportPath + "BronchoscopyReport.rpt");
            }
            else if (proc == 5)
            {
                rprt.Load(_reportPath + "EntReport.rpt");
            }
            else
            {
                return;
            }

            CrTables = rprt.Database.Tables;
            foreach (Table CrTable in CrTables)
            {
                crtableLogoninfo = CrTable.LogOnInfo;
                crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                CrTable.ApplyLogOnInfo(crtableLogoninfo);
            }
            rprt.SetParameterValue("@hn", hn);
            rprt.SetParameterValue("@procedure", proc);
            rprt.SetParameterValue("@endoscopicId", endosId);

            string _pathFolderPDFToSave = _pathFolderPDF + _hnNo + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\";
            if (!Directory.Exists(_pathFolderPDFToSave))
            {
                Directory.CreateDirectory(_pathFolderPDFToSave);
            }
            string fileNamePDF = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string namaPDF = "pdf";
            string nameSave = namaPDF + "_" + fileNamePDF + ".pdf";
            string path = _pathFolderPDFToSave + nameSave;
            rprt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, path);

            string _pathFolderDicomSave = _pathFolderDicom + _hnNo + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\";
            if (!Directory.Exists(_pathFolderDicomSave))
            {
                Directory.CreateDirectory(_pathFolderDicomSave);
            }
            string namaDicom = "dicom";
            string nameDicomSave = namaDicom + "_" + fileNamePDF + ".dcm";
            string pathDicom = _pathFolderDicomSave + nameDicomSave;
            rprt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pathDicom);
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_hnNo) && _procedureId > 0 && _endoscopicId > 0)
            {
                ExportEndoscopic(_hnNo, _procedureId, _endoscopicId);
            }
            else
            {
                return;
            }
        }

        #region Search Data

        private async Task<Object[]> GetIcd10()
        {
            Object[] ItemObject = new Object[0];

            var icd10 = await _db.ICD10.ToListAsync();
            if (icd10 != null && icd10.Count > 0)
            {
                ItemObject = new Object[icd10.Count];

                for (int i = 0; i < icd10.Count; i++)
                {
                    ItemObject[i] = icd10[i].Code + " - " + icd10[i].Name;
                }
            }

            return await Task.FromResult(ItemObject);
        }

        private void SearchHN(string hn, int procId = 0)
        {
            try
            {
                var getPatient = _db.Patients.FirstOrDefault(x => x.HN == hn && (x.IsActive.HasValue && x.IsActive.Value));
                if (getPatient != null)
                {
                    _patientId = getPatient.PatientID;

                    if (_procedureId == 0)
                    {
                        _procedureId = getPatient.ProcedureID.HasValue ? getPatient.ProcedureID.Value : 0;
                    }

                    Appointment app = new Appointment();
                    var apps = _db.Appointments.Where(x => x.PatientID == _patientId).ToList();
                    if (apps != null)
                    {
                        if (_appointmentId > 0)
                        {
                            apps = apps.Where(w => w.AppointmentID == _appointmentId).ToList();
                            app = apps.FirstOrDefault();
                        }
                        else
                        {
                            app = apps.OrderByDescending(o => o.AppointmentDate).FirstOrDefault();
                            _appointmentId = app.AppointmentID;
                        }

                        cbbProcedureList.SelectedValue = _procedureId;

                        if (app.EndoscopicCheck.HasValue && app.EndoscopicCheck.Value || _endoscopicId > 0)
                        {
                            var getEndos = _db.Endoscopics.Where(e => e.ProcedureID == _procedureId && e.PatientID == _patientId && e.IsSaved).ToList();
                            if (getEndos.Count > 0)
                            {
                                getEndos = (from e in getEndos
                                            join p in _db.Patients on e.PatientID equals p.PatientID
                                            select e).ToList();
                                if (_endoscopicId > 0)
                                {
                                    getEndos = getEndos.Where(x => x.EndoscopicID == _endoscopicId).ToList();
                                }

                                if (getEndos.Count > 0)
                                {
                                    Endoscopic getEndo = getEndos.OrderByDescending(o => o.CreateDate).FirstOrDefault();
                                    if (getEndo.NewCase.HasValue && getEndo.NewCase.Value)
                                    {
                                        getEndo.FollowUpCase = true;
                                        getEndo.NewCase = false;
                                    }
                                    _endoscopicId = getEndo.EndoscopicID;
                                    Finding getFinding = _db.Findings.Where(x => x.FindingID == getEndo.FindingID).FirstOrDefault();
                                    Indication getIndication = _db.Indications.Where(x => x.IndicationID == getEndo.IndicationID).FirstOrDefault();
                                    Speciman getSpecimen = _db.Specimen.Where(x => x.SpecimenID == getEndo.SpecimenID).FirstOrDefault();
                                    Intervention getIntervention = _db.Interventions.Where(x => x.InterventionID == getEndo.InterventionID).FirstOrDefault();
                                    PushEndoscopicData(_procedureId, getPatient, getEndo, getFinding, getIndication, getSpecimen, getIntervention);
                                }
                                else
                                {
                                    PushEndoscopicData(_procedureId, getPatient);
                                    Endoscopic endoscopic = new Endoscopic() { PatientID = _patientId, IsSaved = false, ProcedureID = _procedureId, CreateBy = _id, CreateDate = System.DateTime.Now };
                                    _db.Endoscopics.Add(endoscopic);

                                    Finding finding = new Finding() { PatientID = _patientId, CreateBy = _id, CreateDate = System.DateTime.Now };
                                    _db.Findings.Add(finding);
                                    _db.SaveChanges();

                                    var endos = _db.Endoscopics.ToList();
                                    if (endos.Count > 0)
                                    {
                                        Endoscopic endo = endos.OrderByDescending(x => x.EndoscopicID).FirstOrDefault();
                                        _endoscopicId = endo.EndoscopicID;
                                    }
                                }
                            }
                            else
                            {

                            }
                        }

                        btnReport.Enabled = true;
                    }
                    else
                    {
                        Reset_Controller();
                        btnReport.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมูลผู้ป่วย");
                    Reset_Controller();
                    btnReport.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Reset_Controller();
                btnReport.Enabled = false;
            }
        }

        private void txbPreDiagCode_EGD_TextChanged(object sender, EventArgs e)
        {
            if (txbPreDiagCode_EGD.TextLength > 0)
            {
                var icd10 = _db.ICD10.FirstOrDefault(f => f.Code == txbPreDiagCode_EGD.Text);

                if (icd10 != null)
                {
                    txbPreDiagText_EGD.Text = icd10.Name;
                }
            }
        }

        private void txbICD10Code_EGD_TextChanged(object sender, EventArgs e)
        {
            if (txbPreDiagCode_EGD.TextLength > 0)
            {
                var icd10 = _db.ICD10.FirstOrDefault(f => f.Code == txbPreDiagCode_EGD.Text);

                if (icd10 != null)
                {
                    txbICD10Text_EGD.Text = icd10.Name;
                }
            }
        }

        private void LoadTextBoxAutoComplete(TextBox textBox)
        {
            var findingList = _dropdownRepo.GetFindingLabels(_procedureId);
            if (findingList != null && findingList.Count > 0)
            {
                AutoCompleteStringCollection ac = new AutoCompleteStringCollection();
                foreach (var item in findingList)
                {
                    ac.Add(item.Name);
                }
                textBox.AutoCompleteCustomSource = ac;
            }
        }
        #endregion

        #region Push Data

        private void PushEndoscopicImage()
        {
            SetPictureBox(pictureBoxSaved1, txtPictureBoxSaved1, 1);
            SetPictureBox(pictureBoxSaved2, txtPictureBoxSaved2, 2);
            SetPictureBox(pictureBoxSaved3, txtPictureBoxSaved3, 3);
            SetPictureBox(pictureBoxSaved4, txtPictureBoxSaved4, 4);
            SetPictureBox(pictureBoxSaved5, txtPictureBoxSaved5, 5);
            SetPictureBox(pictureBoxSaved7, txtPictureBoxSaved7, 7);
            SetPictureBox(pictureBoxSaved8, txtPictureBoxSaved8, 8);
            SetPictureBox(pictureBoxSaved9, txtPictureBoxSaved9, 9);
            SetPictureBox(pictureBoxSaved10, txtPictureBoxSaved10, 10);
            SetPictureBox(pictureBoxSaved11, txtPictureBoxSaved11, 11);
            SetPictureBox(pictureBoxSaved12, txtPictureBoxSaved12, 12);
            SetPictureBox(pictureBoxSaved13, txtPictureBoxSaved13, 13);
            SetPictureBox(pictureBoxSaved14, txtPictureBoxSaved14, 14);
            SetPictureBox(pictureBoxSaved15, txtPictureBoxSaved15, 15);
            SetPictureBox(pictureBoxSaved16, txtPictureBoxSaved16, 16);
            SetPictureBox(pictureBoxSaved17, txtPictureBoxSaved17, 17);
            SetPictureBox(pictureBoxSaved18, txtPictureBoxSaved18, 18);
            SetAllPicture();
        }
        private void SetPictureBox(PictureBox pictureBox, TextBox textBox, int num)
        {
            var list = _db.EndoscopicImages.Where(x => x.EndoscopicID == _endoscopicId && x.ProcedureID == _procedureId && (x.Seq != null && x.Seq.Value == num))
                .OrderByDescending(x => x.EndoscopicImageID).ToList();
            if (list.Count > 0)
            {
                string path = list.FirstOrDefault().ImagePath;
                pictureBox.ImageLocation = path;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                textBox.Text = list.FirstOrDefault().ImageComment;
            }
        }
        private void SetAllPicture()
        {
            var list = _db.EndoscopicAllImages.Where(x => x.EndoscopicID == _endoscopicId && x.ProcedureID == _procedureId)
                .OrderByDescending(x => x.EndoscopicAllImageID).ToList();
            if (list.Count > 0)
            {
                int i = 0;
                foreach (var item in list)
                {
                    _imgPath.Add(i, item.ImagePath);
                    i++;
                }
            }
        }


        private void PushEndoscopicData(
           int? procId,
           Patient patient = null,
           Endoscopic endoscopic = null,
           Finding finding = null,
           Indication indication = null,
           Speciman speciman = null,
           Intervention intervention = null)
        {
            patient = patient ?? new Patient();
            endoscopic = endoscopic ?? new Endoscopic();
            finding = finding ?? new Finding();
            indication = indication ?? new Indication();
            speciman = speciman ?? new Speciman();
            intervention = intervention ?? new Intervention();

            if (procId == 1 || procId == 2 || procId == 3 || procId == 5) // EGD, ColonoScopy, ERCP, ENT
            {
                // General
                txbHn_EGD.Text = patient.HN;
                txbFullName_EGD.Text = patient.Fullname;
                txbAge_EGD.Text = patient.Age.HasValue ? patient.Age.ToString() : "";
                txbSex_EGD.Text = patient.Sex.HasValue ? patient.Sex.Value ? Constant.Male : Constant.FeMale : string.Empty;
                cbbGeneralOPD_EGD.SelectedIndex = patient.OpdID ?? 0;
                chkOPD_EGD.Checked = patient.OpdID.HasValue && patient.OpdID.Value > 0 ? true : false;
                cbbGeneralWard_EGD.SelectedIndex = patient.WardID ?? 0;
                chkWard_EGD.Checked = patient.WardID.HasValue && patient.WardID.Value > 0 ? true : false;
                chkRefer_EGD.Checked = patient.ReferCheck ?? false;
                txbRefer_EGD.Text = patient.ReferDetail;
                txbGeneralFinancial_EGD.Text = patient.Financial;
                cbbGeneralDoctor_EGD.SelectedValue = patient.DoctorID ?? 0;
                cbbGeneralAnesthesia_EGD.SelectedValue = patient.AnesthesiaID ?? 0;
                cbbGeneralAnesthesist_EGD.SelectedValue = patient.AnesthesistID ?? 0;
                cbbGeneralNurse1_EGD.SelectedValue = patient.NurseFirstID ?? 0;
                cbbGeneralNurse2_EGD.SelectedValue = patient.NurseSecondID ?? 0;
                cbbGeneralNurse3_EGD.SelectedValue = patient.NurseThirthID ?? 0;
                cbbGeneralIndication_EGD.SelectedValue = patient.IndicationID ?? 0;



                //cbbEndoscopist_1.SelectedValue = patient.DoctorID ?? 0;
                //cbbNurse1_1.SelectedValue = patient.NurseFirstID ?? 0;
                //cbbNurse2_1.SelectedValue = patient.NurseSecondID ?? 0;
                //cbbNurse3_1.SelectedValue = patient.NurseThirthID ?? 0;
                //cbbGeneralIndication_1.SelectedValue = endoscopic.Indication ?? 0;
                //cbbGeneralMedication_1.SelectedValue = endoscopic.MedicationID ?? 0;
                //dtArriveTime_1.Value = endoscopic.Arrive ?? DateTime.Now;
                //txtGeneralInstrument_1.Text = endoscopic.Instrument;
                //txtGeneralAnesthesia_1.Text = endoscopic.Anesthesia;
                //txtAnesNurse_1.Text = endoscopic.AnesNurse;

                if (procId == 1)
                {
                    // Finding
                    cbbFindingOropharynx_3.SelectedValue = finding.OropharynxID ?? 0;
                    cbbFindingEsophagus_3.SelectedValue = finding.EsophagusID ?? 0;
                    cbbFindingEGJunction_3.SelectedValue = finding.EGJunctionID ?? 0;
                    cbbFindingCardia_3.SelectedValue = finding.CardiaID ?? 0;
                    cbbFindingFundus_3.SelectedValue = finding.FundusID ?? 0;
                    cbbFindingBody_3.SelectedValue = finding.BodyID ?? 0;
                    cbbFindingAntrum_3.SelectedValue = finding.AntrumID ?? 0;
                    cbbFindingPylorus_3.SelectedValue = finding.PylorusID ?? 0;
                    cbbFindingDuodenalBulb_3.SelectedValue = finding.DuodenalBulbID ?? 0;
                    cbbFindingSecondPart_3.SelectedValue = finding.SecondPartID ?? 0;
                    txtFindingOropharynx_3.Text = finding.Oropharynx;
                    txtFindingEsophagus_3.Text = finding.Esophagus;
                    txtFindingEGJunction_3.Text = finding.EGJunction;
                    txtFindingCardia_3.Text = finding.Cardia;
                    txtFindingFundus_3.Text = finding.Fundus;
                    txtFindingBody_3.Text = finding.Body;
                    txtFindingAntrum_3.Text = finding.Antrum;
                    txtFindingPylorus_3.Text = finding.Pylorus;
                    txtFindingDuodenalBulb_3.Text = finding.DuodenalBulb;
                    txtFindingSecondPart_3.Text = finding.SecondPart;


                    //Specimen
                    cbCLOTest_3.Checked = speciman.BiopsyforCLOTest ?? false;
                    cbPositive.Checked = speciman.Positive ?? false;
                    cbNagative.Checked = speciman.Nagative ?? false;
                    cbPathologocal_3.Checked = speciman.BiopsyforPathological ?? false;
                    if (!string.IsNullOrWhiteSpace(speciman.OtherDetail1) ||
                        !string.IsNullOrWhiteSpace(speciman.OtherDetail2) ||
                        !string.IsNullOrWhiteSpace(speciman.OtherDetail3) ||
                        !string.IsNullOrWhiteSpace(speciman.OtherDetail4) ||
                        !string.IsNullOrWhiteSpace(speciman.OtherDetail5))
                    {
                        cbOther_3.Checked = true;
                    }
                    txtOther0_3.Text = speciman.OtherDetail1;
                    txtOther1_3.Text = speciman.OtherDetail2;
                    txtOther2_3.Text = speciman.OtherDetail3;
                    txtOther3_3.Text = speciman.OtherDetail4;
                    txtOther4_3.Text = speciman.OtherDetail5;
                }
                else if (procId == 2)
                {
                    // Finding
                    cbbFindingAnalCanal_1.SelectedValue = finding.AnalCanalID ?? 0;
                    cbbFindingRectum_1.SelectedValue = finding.RectumID ?? 0;
                    cbbFindingSigmoid_1.SelectedValue = finding.SigmoidColonID ?? 0;
                    cbbFindingDescending_1.SelectedValue = finding.DescendingColonID ?? 0;
                    cbbFindingFlexure_1.SelectedValue = finding.SplenicFlexureID ?? 0;
                    cbbFindingHepatic_1.SelectedValue = finding.HepaticFlexureID ?? 0;
                    cbbFindingAscending_1.SelectedValue = finding.AscendingColonID ?? 0;
                    cbbFindingIleocecal_1.SelectedValue = finding.IleocecalVolveID ?? 0;
                    cbbFindingTerminal_1.SelectedValue = finding.TerminalIleumID ?? 0;
                    cbbFindingCecum_1.SelectedValue = finding.CecumID ?? 0;
                    cbbFindingTransverse_1.SelectedValue = finding.TransverseColonID ?? 0;
                    txtFindingAnalCanal_1.Text = finding.AnalCanal;
                    txtFindingRectum_1.Text = finding.Rectum;
                    txtFindingSigmoid_1.Text = finding.SigmoidColon;
                    txtFindingDescending_1.Text = finding.DescendingColon;
                    txtFindingFlexure_1.Text = finding.SplenicFlexure;
                    txtFindingTransverse_1.Text = finding.TransverseColon;
                    txtFindingHepatic_1.Text = finding.HepaticFlexure;
                    txtFindingAscending_1.Text = finding.AscendingColon;
                    txtFindingIleocecal_1.Text = finding.IleocecalVolve;
                    txtFindingCecum_1.Text = finding.Cecum;
                    txtFindingTerminal_1.Text = finding.TerminalIleum;

                    //Specimen
                    cbPathological.Checked = speciman.BiopsyforPathological ?? false;
                    if (!string.IsNullOrWhiteSpace(speciman.OtherDetail1) ||
                        !string.IsNullOrWhiteSpace(speciman.OtherDetail2) ||
                        !string.IsNullOrWhiteSpace(speciman.OtherDetail3) ||
                        !string.IsNullOrWhiteSpace(speciman.OtherDetail4) ||
                        !string.IsNullOrWhiteSpace(speciman.OtherDetail5))
                    {
                        cbOther_1.Checked = true;
                    }
                    txtOther0_1.Text = speciman.OtherDetail1;
                    txtOther1_1.Text = speciman.OtherDetail2;
                    txtOther2_1.Text = speciman.OtherDetail3;
                    txtOther3_1.Text = speciman.OtherDetail4;
                    txtOther4_1.Text = speciman.OtherDetail5;
                }
                else if (procId == 3)
                {
                    // Finding
                    cbbFindingEsophagus_2.SelectedValue = finding.EsophagusID ?? 0;
                    cbbFindingStomach_2.SelectedValue = finding.StomachID ?? 0;
                    cbbFindingDuodenum_2.SelectedValue = finding.DuodenumID ?? 0;
                    cbbFindingAmpulla_2.SelectedValue = finding.AmpullaOfVaterID ?? 0;
                    cbbFindingCholangiogram_2.SelectedValue = finding.CholangiogramID ?? 0;
                    cbbFindingPancreatogram_2.SelectedValue = finding.PancreatogramID ?? 0;
                    txtFindingEsophagus_2.Text = finding.Esophagus;
                    txtFindingStomach_2.Text = finding.Stomach;
                    txtFindingDuodenum_2.Text = finding.Duodenum;
                    txtFindingAmpulla_2.Text = finding.AmpullaOfVater;
                    txtFindingCholangiogram_2.Text = finding.Cholangiogram;
                    txtFindingPancreatogram_2.Text = finding.Pancreatogram;

                    // Intervention
                    cbSpincterotomy_2.Checked = intervention.Spincterotomy ?? false;
                    cbStoneExtraction_2.Checked = intervention.StoneExtraction ?? false;
                    cbStentInsertion_2.Checked = intervention.StentInsertion ?? false;
                    cbPlastic_2.Checked = intervention.IsPlastic ?? false;
                    txtPlasticFt.Text = intervention.PlasticFoot;
                    txtPlasticCm.Text = intervention.PlasticCentimeter;
                    cbMetal_2.Checked = intervention.IsMetal ?? false;
                    txtMetalFt.Text = intervention.MetalFoot;
                    txtMetalCm.Text = intervention.MetalCentimeter;
                    cbHemonstasis_2.Checked = intervention.Hemonstasis ?? false;
                    cbAdrenaline_2.Checked = intervention.Adrenaline ?? false;
                    cbCoagulation_2.Checked = intervention.Coagulation ?? false;
                    cbSpecimens_2.Checked = intervention.Specimens ?? false;
                    cbPathological_2.Checked = intervention.BiopsyforPathological ?? false;
                    if (!string.IsNullOrWhiteSpace(intervention.OtherDetail1) ||
                        !string.IsNullOrWhiteSpace(intervention.OtherDetail2) ||
                        !string.IsNullOrWhiteSpace(intervention.OtherDetail3) ||
                        !string.IsNullOrWhiteSpace(intervention.OtherDetail4) ||
                        !string.IsNullOrWhiteSpace(intervention.OtherDetail5))
                    {
                        cbOther_2.Checked = true;
                    }
                    txtOther0_2.Text = intervention.OtherDetail1;
                    txtOther1_2.Text = intervention.OtherDetail2;
                    txtOther2_2.Text = intervention.OtherDetail3;
                    txtOther3_2.Text = intervention.OtherDetail4;
                    txtOther4_2.Text = intervention.OtherDetail5;
                }
                else if (procId == 5)
                {
                    // Finding
                    cbbFiNCL.SelectedValue = finding.NasalCavityLeftID ?? 0;
                    txbFiNCL.Text = finding.NasalCavityLeft;
                    cbbFiNCR.SelectedValue = finding.NasalCavityRightID ?? 0;
                    txbFiNCR.Text = finding.NasalCavityRight;
                    cbbFiSeptum.SelectedValue = finding.SeptumID ?? 0;
                    txbFiSeptum.Text = finding.Septum;
                    cbbFiRoof.SelectedValue = finding.RoofID ?? 0;
                    txbFiRoof.Text = finding.Roof;
                    cbbFiPosteriorWall.SelectedValue = finding.PosteriorWallID ?? 0;
                    txbFiPosteriorWall.Text = finding.PosteriorWall;
                    cbbFiRosenmullerFossa.SelectedValue = finding.RosenmullerFossaID ?? 0;
                    txbFiRosenmullerFossa.Text = finding.RosenmullerFossa;
                    cbbFiETOrificeL.SelectedValue = finding.EustachianTubeOrificeLeftID ?? 0;
                    txbFiETOrificeL.Text = finding.EustachianTubeOrificeLeft;
                    cbbFiETOrificeR.SelectedValue = finding.EustachianTubeOrificeRightID ?? 0;
                    txbFiETOrificeR.Text = finding.EustachianTubeOrificeRight;
                    cbbFiSoftPalate.SelectedValue = finding.SoftPalateID ?? 0;
                    txbFiSoftPalate.Text = finding.SoftPalate;
                    cbbFiUvula.SelectedValue = finding.UvulaID ?? 0;
                    txbFiUvula.Text = finding.Uvula;
                    cbbFiTonsil.SelectedValue = finding.TonsilID ?? 0;
                    txbFiTonsil.Text = finding.Tonsil;
                    cbbFiBaseOfTongue.SelectedValue = finding.BaseOfTongueID ?? 0;
                    txbFiBaseOfTongue.Text = finding.BaseOfTongue;
                    cbbFiVallecula.SelectedValue = finding.ValleculaID ?? 0;
                    txbFiVallecula.Text = finding.Vallecula;
                    cbbFiPyrSinusL.SelectedValue = finding.PyriformSinusLeftID ?? 0;
                    txbFiPyrSinusL.Text = finding.PyriformSinusLeft;
                    cbbFiPyrSinusR.SelectedValue = finding.PyriformSinusRightID ?? 0;
                    txbFiPyrSinusR.Text = finding.PyriformSinusRight;
                    cbbFiPostcricoid.SelectedValue = finding.PostcricoidID ?? 0;
                    txbFiPostcricoid.Text = finding.Postcricoid;
                    cbbFiPosPhaWall.SelectedValue = finding.PosteriorPharyngealWallID ?? 0;
                    txbFiPosPhaWall.Text = finding.PosteriorPharyngealWall;
                    cbbFiSupraglottic.SelectedValue = finding.SupraglotticID ?? 0;
                    txbFiSupraglottic.Text = finding.Supraglottic;
                    cbbFiGlottic.SelectedValue = finding.GlotticID ?? 0;
                    txbFiGlottic.Text = finding.Glottic;
                    cbbFiSubglottic.SelectedValue = finding.SubglotticID ?? 0;
                    txbFiSubglottic.Text = finding.Subglottic;
                    cbbFiUES.SelectedValue = finding.UESID ?? 0;
                    txbFiUES.Text = finding.UES;
                    cbbFiEsophagus.SelectedValue = finding.EsophagusID ?? 0;
                    txbFiEsophagus.Text = finding.Esophagus;
                    cbbFiLES.SelectedValue = finding.LESID ?? 0;
                    txbFiLES.Text = finding.LES;
                    cbbFiStomach.SelectedValue = finding.StomachID ?? 0;
                    txbFiStomach.Text = finding.Stomach;

                    // Specimen
                    cbFiBiopsy_Ent.Checked = speciman.BiopsyforPathological ?? false;
                }
            }
            else if (procId == 4) // Bronchoscopy 
            {
                // General
                //cbbEndoscopist_0.SelectedValue = patient.DoctorID ?? 0;
                //cbbNurse1_0.SelectedValue = patient.NurseFirstID ?? 0;
                //cbbNurse2_0.SelectedValue = patient.NurseSecondID ?? 0;
                //cbbNurse3_0.SelectedValue = patient.NurseThirthID ?? 0;
                //cbbGeneralMedication_0.SelectedValue = endoscopic.MedicationID ?? 0;
                //dtArriveTime_0.Value = endoscopic.Arrive ?? DateTime.Now;
                //txtGeneralInstrument_0.Text = endoscopic.Instrument;
                //txtGeneralAnesthesia_0.Text = endoscopic.Anesthesia;
                //txtAnesNurse_0.Text = endoscopic.AnesNurse;

                //// Finding
                //cbbFindingVocal_0.SelectedValue = finding.VocalCordID ?? 0;
                //cbbFindingTrachea_0.SelectedValue = finding.TracheaID ?? 0;
                //cbbFindingCarina_0.SelectedValue = finding.CarinaID ?? 0;
                //cbbFindingRightMain_0.SelectedValue = finding.RightMainID ?? 0;
                //cbbFindingRightIntermideate_0.SelectedValue = finding.RightIntermideateID ?? 0;
                //cbbFindingRUL_0.SelectedValue = finding.RULID ?? 0;
                //cbbFindingRML_0.SelectedValue = finding.RMLID ?? 0;
                //cbbFindingRLL_0.SelectedValue = finding.RLLID ?? 0;
                //cbbFindingLeftMain_0.SelectedValue = finding.LeftMainID ?? 0;
                //cbbFindingLUL_0.SelectedValue = finding.LULID ?? 0;
                //cbbFindingLingular_0.SelectedValue = finding.LingularID ?? 0;
                //cbbFindingLLL_0.SelectedValue = finding.LLLID ?? 0;
                //finding.VocalCordID = (int?)cbbFindingVocal_0.SelectedValue;
                //finding.TracheaID = (int?)cbbFindingTrachea_0.SelectedValue;
                //finding.CarinaID = (int?)cbbFindingCarina_0.SelectedValue;
                //finding.RightMainID = (int?)cbbFindingRightMain_0.SelectedValue;
                //finding.RightIntermideateID = (int?)cbbFindingRightIntermideate_0.SelectedValue;
                //finding.RULID = (int?)cbbFindingRUL_0.SelectedValue;
                //finding.RMLID = (int?)cbbFindingRML_0.SelectedValue;
                //finding.RLLID = (int?)cbbFindingRLL_0.SelectedValue;
                //finding.LeftMainID = (int?)cbbFindingLeftMain_0.SelectedValue;
                //finding.LULID = (int?)cbbFindingLUL_0.SelectedValue;
                //finding.LingularID = (int?)cbbFindingLingular_0.SelectedValue;
                //finding.LLLID = (int?)cbbFindingLLL_0.SelectedValue;

                //txtFindingVocalCord_0.Text = finding.VocalCord;
                //txtFindingTrachea_0.Text = finding.Trachea;
                //txtFindingCarina_0.Text = finding.Carina;
                //txtFindingRightMain_0.Text = finding.RightMain;
                //txtFindingIntermideate_0.Text = finding.RightIntermideate;
                //txtFindingRUL_0.Text = finding.RUL;
                //txtFindingRML_0.Text = finding.RML;
                //txtFindingRLL_0.Text = finding.RLL;
                //txtFindingLeftMain_0.Text = finding.LeftMain;
                //txtFindingLUL_0.Text = finding.LUL;
                //txtFindingLingular_0.Text = finding.Lingular;
                //txtFindingLLL_0.Text = finding.LLL;

                // Indication
                cbInfiltration_0.Checked = indication.EvaluateLesion_Infiltration ?? false;
                cbPatency_0.Checked = indication.AsscessAirwayPatency ?? false;
                cbHemoptysis_0.Checked = indication.Hemoptysis ?? false;
                cbTherapeutic_0.Checked = indication.Therapeutic ?? false;
                if (!string.IsNullOrWhiteSpace(indication.OtherDetail1) ||
                    !string.IsNullOrWhiteSpace(indication.OtherDetail2) ||
                    !string.IsNullOrWhiteSpace(indication.OtherDetail3) ||
                    !string.IsNullOrWhiteSpace(indication.OtherDetail4) ||
                    !string.IsNullOrWhiteSpace(indication.OtherDetail5))
                {
                    cbOther_0.Checked = true;
                }
                txtIndicationOther0_0.Text = indication.OtherDetail1;
                txtIndicationOther1_0.Text = indication.OtherDetail2;
                txtIndicationOther2_0.Text = indication.OtherDetail3;
                txtIndicationOther3_0.Text = indication.OtherDetail4;
                txtIndicationOther4_0.Text = indication.OtherDetail5;

                // Specimen
                cbBalAt_0.Checked = speciman.BalAt ?? false;
                txtBalAt_0.Text = speciman.BalAtDetail;
                cbBalAt_Cytho.Checked = speciman.BalAtCytho ?? false;
                cbBalAt_Patho.Checked = speciman.BalAtPatho ?? false;
                cbBalAt_Gram.Checked = speciman.BalAtGram ?? false;
                cbBalAt_AFB.Checked = speciman.BalAtAFB ?? false;
                cbBalAt_Mod.Checked = speciman.BalAtModAFB ?? false;

                cbBrushingAt_0.Checked = speciman.BrushingAt ?? false;
                txtBrushing_0.Text = speciman.BrushingAtDetail;
                cbBrushing_Cytho.Checked = speciman.BrushingAtCytho ?? false;
                cbBrushing_Patho.Checked = speciman.BrushingAtPatho ?? false;
                cbBrushing_Gram.Checked = speciman.BrushingAtGram ?? false;
                cbBrushing_AFB.Checked = speciman.BrushingAtAFB ?? false;
                cbBrushing_Mod.Checked = speciman.BrushingAtModAFB ?? false;

                cbEndoproncial_0.Checked = speciman.EndoproncialBiopsyAt ?? false;
                txtEndoproncial_0.Text = speciman.EndoproncialBiopsyAtDetail;
                cbEndoproncial_Cytho.Checked = speciman.EndoproncialBiopsyAtCytho ?? false;
                cbEndoproncial_Patho.Checked = speciman.EndoproncialBiopsyAtPatho ?? false;
                cbEndoproncial_Gram.Checked = speciman.EndoproncialBiopsyAtGram ?? false;
                cbEndoproncial_AFB.Checked = speciman.EndoproncialBiopsyAtAFB ?? false;
                cbEndoproncial_Mod.Checked = speciman.EndoproncialBiopsyAtModAFB ?? false;

                cbTransbroncial_0.Checked = speciman.TransbroncialBiopsyAt ?? false;
                txtTransbroncial_0.Text = speciman.TransbroncialBiopsyAtDetail;
                cbTransbroncial_Cytho.Checked = speciman.TransbroncialBiopsyAtCytho ?? false;
                cbTransbroncial_Patho.Checked = speciman.TransbroncialBiopsyAtPatho ?? false;
                cbTransbroncial_Gram.Checked = speciman.TransbroncialBiopsyAtGram ?? false;
                cbTransbroncial_AFB.Checked = speciman.TransbroncialBiopsyAtAFB ?? false;
                cbTransbroncial_Mod.Checked = speciman.TransbroncialBiopsyAtModAFB ?? false;

                cbTransbroncialNeedle_0.Checked = speciman.Transbroncial ?? false;
                txtTransbroncialNeedle_0.Text = speciman.TransbroncialDetail;
                cbTransbroncialNeedle_Cytho.Checked = speciman.TransbroncialCytho ?? false;
                cbTransbroncialNeedle_Patho.Checked = speciman.TransbroncialPatho ?? false;
                cbTransbroncialNeedle_Gram.Checked = speciman.TransbroncialGram ?? false;
                cbTransbroncialNeedle_AFB.Checked = speciman.TransbroncialAFB ?? false;
                cbTransbroncialNeedle_Mod.Checked = speciman.TransbroncialModAFB ?? false;
            }

            //txtDiagnosis.Text = endoscopic.Diagnosis;
            //txtComplication.Text = endoscopic.Complication;
            //txtComment.Text = endoscopic.Comment;

            //if (endoscopic.StartRecordDate.HasValue) recordStartDate = endoscopic.StartRecordDate.Value;
            //if (endoscopic.EndRecordDate.HasValue) recordEndDate = endoscopic.EndRecordDate.Value;
            PushEndoscopicImage();
            //PushEndoscopicVideo();
        }


        #endregion

        #region Open/Close TabPage

        private void OpenTabPage(int procedureId)
        {
            RemoveTabPage();

            if (procedureId == 1)
            {
                TabPage[] tabs = { tabGeneralEGD, tabFindingEGD };
                tabControl1.TabPages.AddRange(tabs);
                _dropdownListService.DropdownOPD(cbbGeneralOPD_EGD);
                _dropdownListService.DropdownWard(cbbGeneralWard_EGD);
                _dropdownListService.DropdownDoctor(cbbGeneralDoctor_EGD);
                _dropdownListService.DropdownAnesthesia(cbbGeneralAnesthesia_EGD);
                _dropdownListService.DropdownAnesthesist(cbbGeneralAnesthesist_EGD);
                _dropdownListService.DropdownNurse(cbbGeneralNurse1_EGD);
                _dropdownListService.DropdownNurse(cbbGeneralNurse2_EGD);
                _dropdownListService.DropdownNurse(cbbGeneralNurse3_EGD);
                _dropdownListService.DropdownMedication(cbbGeneralMedication_EGD);
                _dropdownListService.DropdownIndication(cbbGeneralIndication_EGD);
            }
        }

        #endregion

        #region Reset Control
        private void RemoveTabPage()
        {
            tabControl1.TabPages.Clear();
        }
        private void Reset_Controller()
        {
            this.Controls.ClearControls();
            RemoveTabPage();
            btnReport.Hide();
        }

        #endregion
    }
}
