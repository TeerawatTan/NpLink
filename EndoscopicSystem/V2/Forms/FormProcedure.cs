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
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
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
        private readonly string _initialDirectoryUpload = "C://Desktop";
        private readonly string _titleUpload = "Select image to be upload.";
        private readonly string _filterUpload = "Image Only(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
        private readonly string _pathFolderImage = Application.StartupPath.Replace("\\bin\\Debug", "") + @"\ImageCapture\";
        private string _pathFolderImageSave, _hnNo, _pathImg, _fileName = ".jpg", _vdoPath;
        private int _id, _procedureId, _appointmentId, _patientId, _endoscopicId, _item, _aspectRatioID = 1;
        private readonly GetDropdownList _dropdownRepo = new GetDropdownList();
        private readonly EndoscopicEntities _db = new EndoscopicEntities();
        private readonly DropdownListService _dropdownListService = new DropdownListService();
        public Dictionary<int, string> _imgPath = new Dictionary<int, string>();
        private TextBox lastFocused;
        public Form formPopup = new Form();
        private List<FindingLabel> _findingLabels = new List<FindingLabel>();
        private List<ICD9> _iCD9s = new List<ICD9>();
        private List<ICD10> _iCD10s = new List<ICD10>();
        public FormProcedure(int id, string hn, int procId, int appId, string pathImg)
        {
            InitializeComponent();

            this._id = id;
            this._hnNo = hn;
            this._procedureId = procId;
            this._appointmentId = appId;
            this._pathFolderImageSave = pathImg;
        }

        private void FormProcedure_Load(object sender, EventArgs e)
        {
            btnReport.Visible = false;

            cbbProcedureList.ValueMember = "ProcedureID";
            cbbProcedureList.DisplayMember = "ProcedureName";
            cbbProcedureList.DataSource = _dropdownRepo.GetProcedureList();
            cbbProcedureList.SelectedIndex = 0;
            cbbProcedureList.Enabled = true;
            cbbProcedureList.SelectedValue = _procedureId;
            cbbProcedureList.Enabled = false;
            OpenTabPage(_procedureId);
            SearchHN(_hnNo, _procedureId);

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

            LoadFinding();
            LoadICD9();
            LoadICD10();
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
                    lastFocused.Text = subStrItem[0].Trim();
                }
            }

            listBox1.Items.Clear();
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_hnNo) && _procedureId > 0 && _endoscopicId > 0)
            {
                // Save
                bool isSave = OnSave(_hnNo, _patientId, _procedureId, _endoscopicId);
                if (isSave)
                {
                    btnReport.Visible = true;
                }
            }
            else
            {
                btnReport.Visible = false;
                return;
            }
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_hnNo) && _procedureId > 0 && _endoscopicId > 0)
            {
                // Export
                ExportEndoscopic(_hnNo, _procedureId, _endoscopicId);
            }
            else
            {
                return;
            }
        }

        #region Search Data

        private void SetDataInListBox(int listId)
        {
            listBox1.Items.Clear();
            if (listId == Constant.Icd9)
            {
                var ItemObject = GetIcd9();
                listBox1.Items.AddRange(ItemObject);
            }
            else if (listId == Constant.Icd10)
            {
                var ItemObject = GetIcd10();
                listBox1.Items.AddRange(ItemObject);
            }
        }
        private Object[] GetIcd9()
        {
            Object[] ItemObject = new Object[0];

            var icd9 = _iCD9s.Where(w => w.ProcedureId == _procedureId || w.ProcedureId == null).OrderBy(o => o.ID);
            if (icd9 != null && icd9.Count() > 0)
            {
                var data = icd9.ToList();
                ItemObject = new Object[data.Count];

                for (int i = 0; i < data.Count; i++)
                {
                    ItemObject[i] = data[i].Code + " - " + data[i].Name;
                }
            }

            return ItemObject;
        }
        private Object[] GetIcd10()
        {
            Object[] ItemObject = new Object[0];

            var icd10 = _iCD10s.Where(w => w.ProcedureId == _procedureId || w.ProcedureId == null).OrderBy(o => o.ID);
            if (icd10 != null && icd10.Count() > 0)
            {
                var data = icd10.ToList();
                ItemObject = new Object[data.Count];

                for (int i = 0; i < data.Count; i++)
                {
                    ItemObject[i] = data[i].Code + " - " + data[i].Name;
                }
            }

            return ItemObject;
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
        private void txbPreDiagCode_EGD_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd10);
            lastFocused = (TextBox)sender;
        }
        private void txbICD10Code_EGD_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd10);
            lastFocused = (TextBox)sender;
        }
        private void txbFindingPrinncipalProcedureCode_EGD_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd9);
            lastFocused = (TextBox)sender;
        }
        private void txbFindingSupplementalProcedureCode_EGD_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd9);
            lastFocused = (TextBox)sender;
        }
        private void txbFindingSupplementalProcedureCode2_EGD_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd9);
            lastFocused = (TextBox)sender;
        }
        private void txbFindingDx1Code_EGD_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd10);
            lastFocused = (TextBox)sender;
        }
        private void txbFindingDx2Code_EGD_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd10);
            lastFocused = (TextBox)sender;
        }
        private void txbFindingDx3Code_EGD_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd10);
            lastFocused = (TextBox)sender;
        }
        private void txbFindingDx4Code_EGD_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd10);
            lastFocused = (TextBox)sender;
        }
        private void txbPreDiagCode_Colono_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd10);
            lastFocused = (TextBox)sender;
        }
        private void txbICD10Code_Colono_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd10);
            lastFocused = (TextBox)sender;
        }
        private void txbFindingPrinncipalProcedureCode_Colono_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd9);
            lastFocused = (TextBox)sender;
        }
        private void txbFindingSupplementalProcedureCode_Colono_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd9);
            lastFocused = (TextBox)sender;
        }
        private void txbFindingSupplementalProcedure2Code_Colono_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd9);
            lastFocused = (TextBox)sender;
        }
        private void txbFindingDx1Code_Colono_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd10);
            lastFocused = (TextBox)sender;
        }
        private void txbFindingDx2Code_Colono_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd10);
            lastFocused = (TextBox)sender;
        }
        private void txbFindingDx3Code_Colono_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd10);
            lastFocused = (TextBox)sender;
        }
        private void txbFindingDx4Code_Colono_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd10);
            lastFocused = (TextBox)sender;
        }
        private void txbPreDiagCode_EGD_TextChanged(object sender, EventArgs e)
        {
            if (txbPreDiagCode_EGD.TextLength > 0)
            {
                var icd10 = _iCD10s.FirstOrDefault(f => f.Code == txbPreDiagCode_EGD.Text);

                if (icd10 != null)
                {
                    txbPreDiagText_EGD.Text = icd10.Name;
                    txbGeneralDx1ID_EGD.Text = icd10.ID.ToString();
                }
                else
                {
                    txbPreDiagText_EGD.Clear();
                    txbGeneralDx1ID_EGD.Clear();
                }
            }
            else
            {
                txbPreDiagText_EGD.Clear();
                txbGeneralDx1ID_EGD.Clear();
            }
        }
        private void txbICD10Code_EGD_TextChanged(object sender, EventArgs e)
        {
            if (txbICD10Code_EGD.TextLength > 0)
            {
                var icd10 = _iCD10s.FirstOrDefault(f => f.Code == txbICD10Code_EGD.Text);

                if (icd10 != null)
                {
                    txbICD10Text_EGD.Text = icd10.Name;
                    txbGeneralDx2ID_EGD.Text = icd10.ID.ToString();
                }
                else
                {
                    txbICD10Text_EGD.Clear();
                    txbGeneralDx2ID_EGD.Clear();
                }
            }
            else
            {
                txbICD10Text_EGD.Clear();
                txbGeneralDx2ID_EGD.Clear();
            }
        }
        private void txbFindingPrinncipalProcedureCode_EGD_TextChanged(object sender, EventArgs e)
        {
            if (txbFindingPrinncipalProcedureCode_EGD.TextLength > 0)
            {
                var icd9 = _iCD9s.FirstOrDefault(f => f.Code == txbFindingPrinncipalProcedureCode_EGD.Text);
                if (icd9 != null)
                {
                    txbFindingPrinncipalProcedureText_EGD.Text = icd9.Name;
                    txbFindingPrinncipalProcedureID_EGD.Text = icd9.ID.ToString();
                }
                else
                {
                    txbFindingPrinncipalProcedureText_EGD.Clear();
                    txbFindingPrinncipalProcedureID_EGD.Clear();
                }
            }
            else
            {
                txbFindingPrinncipalProcedureText_EGD.Clear();
                txbFindingPrinncipalProcedureID_EGD.Clear();
            }
        }
        private void txbFindingSupplementalProcedureCode_EGD_TextChanged(object sender, EventArgs e)
        {
            if (txbFindingSupplementalProcedureCode_EGD.TextLength > 0)
            {
                var icd9 = _iCD9s.FirstOrDefault(f => f.Code == txbFindingSupplementalProcedureCode_EGD.Text);
                if (icd9 != null)
                {
                    txbFindingSupplementalProcedureText_EGD.Text = icd9.Name;
                    txbFindingSupplementalProcedureID_EGD.Text = icd9.ID.ToString();
                }
                else
                {
                    txbFindingSupplementalProcedureText_EGD.Clear();
                    txbFindingSupplementalProcedureID_EGD.Clear();
                }
            }
            else
            {
                txbFindingSupplementalProcedureText_EGD.Clear();
                txbFindingSupplementalProcedureID_EGD.Clear();
            }
        }
        private void txbFindingSupplementalProcedureCode2_EGD_TextChanged(object sender, EventArgs e)
        {
            if (txbFindingSupplementalProcedureCode2_EGD.TextLength > 0)
            {
                var icd9 = _iCD9s.FirstOrDefault(f => f.Code == txbFindingSupplementalProcedureCode2_EGD.Text);
                if (icd9 != null)
                {
                    txbFindingSupplementalProcedureText2_EGD.Text = icd9.Name;
                    txbFindingSupplementalProcedureID2_EGD.Text = icd9.ID.ToString();
                }
                else
                {
                    txbFindingSupplementalProcedureText2_EGD.Clear();
                    txbFindingSupplementalProcedureID2_EGD.Clear();
                }
            }
            else
            {
                txbFindingSupplementalProcedureText2_EGD.Clear();
                txbFindingSupplementalProcedureID2_EGD.Clear();
            }
        }
        private void txbFindingDx1Code_EGD_TextChanged(object sender, EventArgs e)
        {
            if (txbFindingDx1Code_EGD.TextLength > 0)
            {
                var icd10 = _iCD10s.FirstOrDefault(f => f.Code == txbFindingDx1Code_EGD.Text);
                if (icd10 != null)
                {
                    txbFindingDx1Text_EGD.Text = icd10.Name;
                    txbFindingDx1ID_EGD.Text = icd10.ID.ToString();
                }
                else
                {
                    txbFindingDx1Text_EGD.Clear();
                    txbFindingDx1ID_EGD.Clear();
                }
            }
            else
            {
                txbFindingDx1Text_EGD.Clear();
                txbFindingDx1ID_EGD.Clear();
            }
        }
        private void txbFindingDx2Code_EGD_TextChanged(object sender, EventArgs e)
        {
            if (txbFindingDx2Code_EGD.TextLength > 0)
            {
                var icd10 = _iCD10s.FirstOrDefault(f => f.Code == txbFindingDx2Code_EGD.Text);
                if (icd10 != null)
                {
                    txbFindingDx2Text_EGD.Text = icd10.Name;
                    txbFindingDx2ID_EGD.Text = icd10.ID.ToString();
                }
                else
                {
                    txbFindingDx2Text_EGD.Clear();
                    txbFindingDx2ID_EGD.Clear();
                }
            }
            else
            {
                txbFindingDx2Text_EGD.Clear();
                txbFindingDx2ID_EGD.Clear();
            }
        }
        private void txbFindingDx3Code_EGD_TextChanged(object sender, EventArgs e)
        {
            if (txbFindingDx3Code_EGD.TextLength > 0)
            {
                var icd10 = _iCD10s.FirstOrDefault(f => f.Code == txbFindingDx3Code_EGD.Text);
                if (icd10 != null)
                {
                    txbFindingDx3Text_EGD.Text = icd10.Name;
                    txbFindingDx3ID_EGD.Text = icd10.ID.ToString();
                }
                else
                {
                    txbFindingDx3Text_EGD.Clear();
                    txbFindingDx3ID_EGD.Clear();
                }
            }
            else
            {
                txbFindingDx3Text_EGD.Clear();
                txbFindingDx3ID_EGD.Clear();
            }

        }
        private void txbFindingDx4Code_EGD_TextChanged(object sender, EventArgs e)
        {
            if (txbFindingDx4Code_EGD.TextLength > 0)
            {
                var icd10 = _iCD10s.FirstOrDefault(f => f.Code == txbFindingDx4Code_EGD.Text);
                if (icd10 != null)
                {
                    txbFindingDx4Text_EGD.Text = icd10.Name;
                    txbFindingDx4ID_EGD.Text = icd10.ID.ToString();
                }
                else
                {
                    txbFindingDx4Text_EGD.Clear();
                    txbFindingDx4ID_EGD.Clear();
                }
            }
            else
            {
                txbFindingDx4Text_EGD.Clear();
                txbFindingDx4ID_EGD.Clear();
            }
        }
        private void txbPreDiagCode_Colono_TextChanged(object sender, EventArgs e)
        {
            if (txbPreDiagCode_Colono.TextLength > 0)
            {
                var icd10 = _iCD10s.FirstOrDefault(f => f.Code == txbPreDiagCode_Colono.Text);

                if (icd10 != null)
                {
                    txbPreDiagText_Colono.Text = icd10.Name;
                    txbGeneralDx1ID_Colono.Text = icd10.ID.ToString();
                }
                else
                {
                    txbPreDiagText_Colono.Clear();
                    txbGeneralDx1ID_Colono.Clear();
                }
            }
            else
            {
                txbPreDiagText_Colono.Clear();
                txbGeneralDx1ID_Colono.Clear();
            }
        }
        private void txbICD10Code_Colono_TextChanged(object sender, EventArgs e)
        {
            if (txbICD10Code_Colono.TextLength > 0)
            {
                var icd10 = _iCD10s.FirstOrDefault(f => f.Code == txbICD10Code_Colono.Text);

                if (icd10 != null)
                {
                    txbICD10Text_Colono.Text = icd10.Name;
                    txbGeneralDx2ID_Colono.Text = icd10.ID.ToString();
                }
                else
                {
                    txbICD10Text_Colono.Clear();
                    txbGeneralDx2ID_Colono.Clear();
                }
            }
            else
            {
                txbICD10Text_Colono.Clear();
                txbGeneralDx2ID_Colono.Clear();
            }
        }
        private void txbFindingPrinncipalProcedureCode_Colono_TextChanged(object sender, EventArgs e)
        {
            if (txbFindingPrinncipalProcedureCode_Colono.TextLength > 0)
            {
                var icd9 = _iCD9s.FirstOrDefault(f => f.Code == txbFindingPrinncipalProcedureCode_Colono.Text);
                if (icd9 != null)
                {
                    txbFindingPrinncipalProcedureText_Colono.Text = icd9.Name;
                    txbFindingPrinncipalProcedureID_Colono.Text = icd9.ID.ToString();
                }
                else
                {
                    txbFindingPrinncipalProcedureText_Colono.Clear();
                    txbFindingPrinncipalProcedureID_Colono.Clear();
                }
            }
            else
            {
                txbFindingPrinncipalProcedureText_Colono.Clear();
                txbFindingPrinncipalProcedureID_Colono.Clear();
            }
        }
        private void txbFindingSupplementalProcedureCode_Colono_TextChanged(object sender, EventArgs e)
        {
            if (txbFindingSupplementalProcedureCode_Colono.TextLength > 0)
            {
                var icd9 = _iCD9s.FirstOrDefault(f => f.Code == txbFindingSupplementalProcedureCode_Colono.Text);
                if (icd9 != null)
                {
                    txbFindingSupplementalProcedureText_Colono.Text = icd9.Name;
                    txbFindingSupplementalProcedureID_Colono.Text = icd9.ID.ToString();
                }
                else
                {
                    txbFindingSupplementalProcedureText_Colono.Clear();
                    txbFindingSupplementalProcedureID_Colono.Clear();
                }
            }
            else
            {
                txbFindingSupplementalProcedureText_Colono.Clear();
                txbFindingSupplementalProcedureID_Colono.Clear();
            }
        }
        private void txbFindingSupplementalProcedure2Code_Colono_TextChanged(object sender, EventArgs e)
        {
            if (txbFindingSupplementalProcedure2Code_Colono.TextLength > 0)
            {
                var icd9 = _iCD9s.FirstOrDefault(f => f.Code == txbFindingSupplementalProcedure2Code_Colono.Text);
                if (icd9 != null)
                {
                    txbFindingSupplementalProcedure2Text_Colono.Text = icd9.Name;
                    txbFindingSupplementalProcedure2ID_Colono.Text = icd9.ID.ToString();
                }
                else
                {
                    txbFindingSupplementalProcedure2Text_Colono.Clear();
                    txbFindingSupplementalProcedure2ID_Colono.Clear();
                }
            }
            else
            {
                txbFindingSupplementalProcedure2Text_Colono.Clear();
                txbFindingSupplementalProcedure2ID_Colono.Clear();
            }
        }
        private void txbFindingDx1Code_Colono_TextChanged(object sender, EventArgs e)
        {
            if (txbFindingDx1Code_Colono.TextLength > 0)
            {
                var icd10 = _iCD10s.FirstOrDefault(f => f.Code == txbFindingDx1Code_Colono.Text);
                if (icd10 != null)
                {
                    txbFindingDx1Text_Colono.Text = icd10.Name;
                    txbFindingDx1ID_Colono.Text = icd10.ID.ToString();
                }
                else
                {
                    txbFindingDx1Text_Colono.Clear();
                    txbFindingDx1ID_Colono.Clear();
                }
            }
            else
            {
                txbFindingDx1Text_Colono.Clear();
                txbFindingDx1ID_Colono.Clear();
            }
        }
        private void txbFindingDx2Code_Colono_TextChanged(object sender, EventArgs e)
        {
            if (txbFindingDx2Code_Colono.TextLength > 0)
            {
                var icd10 = _iCD10s.FirstOrDefault(f => f.Code == txbFindingDx2Code_Colono.Text);
                if (icd10 != null)
                {
                    txbFindingDx2Text_Colono.Text = icd10.Name;
                    txbFindingDx2ID_Colono.Text = icd10.ID.ToString();
                }
                else
                {
                    txbFindingDx2Text_Colono.Clear();
                    txbFindingDx2ID_Colono.Clear();
                }
            }
            else
            {
                txbFindingDx2Text_Colono.Clear();
                txbFindingDx2ID_Colono.Clear();
            }
        }
        private void txbFindingDx3Code_Colono_TextChanged(object sender, EventArgs e)
        {
            if (txbFindingDx3Code_Colono.TextLength > 0)
            {
                var icd10 = _iCD10s.FirstOrDefault(f => f.Code == txbFindingDx3Code_Colono.Text);
                if (icd10 != null)
                {
                    txbFindingDx3Text_Colono.Text = icd10.Name;
                    txbFindingDx3ID_Colono.Text = icd10.ID.ToString();
                }
                else
                {
                    txbFindingDx3Text_Colono.Clear();
                    txbFindingDx3ID_Colono.Clear();
                }
            }
            else
            {
                txbFindingDx3Text_Colono.Clear();
                txbFindingDx3ID_Colono.Clear();
            }
        }
        private void txbFindingDx4Code_Colono_TextChanged(object sender, EventArgs e)
        {
            if (txbFindingDx4Code_Colono.TextLength > 0)
            {
                var icd10 = _iCD10s.FirstOrDefault(f => f.Code == txbFindingDx4Code_Colono.Text);
                if (icd10 != null)
                {
                    txbFindingDx4Text_Colono.Text = icd10.Name;
                    txbFindingDx4ID_Colono.Text = icd10.ID.ToString();
                }
                else
                {
                    txbFindingDx4Text_Colono.Clear();
                    txbFindingDx4ID_Colono.Clear();
                }
            }
            else
            {
                txbFindingDx4Text_Colono.Clear();
                txbFindingDx4ID_Colono.Clear();
            }
        }
        private void LoadFinding()
        {
            _findingLabels = _dropdownRepo.GetFindingLabels(_procedureId);
        }
        private void LoadICD9()
        {
            _iCD9s = _db.ICD9.ToList();
        }
        private void LoadICD10()
        {
            _iCD10s = _db.ICD10.ToList();
        }
        private void LoadTextBoxAutoComplete(TextBox textBox)
        {
            if (_findingLabels != null && _findingLabels.Count > 0)
            {
                AutoCompleteStringCollection ac = new AutoCompleteStringCollection();
                foreach (var item in _findingLabels)
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
                cbbGeneralFinancial_EGD.SelectedValue = patient.FinancialID;
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
                    cbbFindingAnalCanal_Colono.SelectedValue = finding.AnalCanalID ?? 0;
                    cbbFindingRectum_Colono.SelectedValue = finding.RectumID ?? 0;
                    cbbFindingSigmoid_Colono.SelectedValue = finding.SigmoidColonID ?? 0;
                    cbbFindingDescending_Colono.SelectedValue = finding.DescendingColonID ?? 0;
                    cbbFindingFlexure_Colono.SelectedValue = finding.SplenicFlexureID ?? 0;
                    cbbFindingHepatic_Colono.SelectedValue = finding.HepaticFlexureID ?? 0;
                    cbbFindingAscending_Colono.SelectedValue = finding.AscendingColonID ?? 0;
                    cbbFindingIleocecal_Colono.SelectedValue = finding.IleocecalVolveID ?? 0;
                    cbbFindingTerminal_Colono.SelectedValue = finding.TerminalIleumID ?? 0;
                    cbbFindingCecum_Colono.SelectedValue = finding.CecumID ?? 0;
                    cbbFindingTransverse_Colono.SelectedValue = finding.TransverseColonID ?? 0;
                    txtFindingAnalCanal_Colono.Text = finding.AnalCanal;
                    txtFindingRectum_Colono.Text = finding.Rectum;
                    txtFindingSigmoid_Colono.Text = finding.SigmoidColon;
                    txtFindingDescending_Colono.Text = finding.DescendingColon;
                    txtFindingFlexure_Colono.Text = finding.SplenicFlexure;
                    txtFindingTransverse_Colono.Text = finding.TransverseColon;
                    txtFindingHepatic_Colono.Text = finding.HepaticFlexure;
                    txtFindingAscending_Colono.Text = finding.AscendingColon;
                    txtFindingIleocecal_Colono.Text = finding.IleocecalVolve;
                    txtFindingCecum_Colono.Text = finding.Cecum;
                    txtFindingTerminal_Colono.Text = finding.TerminalIleum;

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

        private void UpdateFinding(int procedureId)
        {
            var findingData = _db.Findings.Where(x => x.PatientID == _patientId).OrderByDescending(o => o.FindingID);
            if (findingData != null)
            {
                Finding finding = findingData.FirstOrDefault();
                if (procedureId == 1)
                {
                    finding.OropharynxID = (int?)cbbFindingOropharynx_3.SelectedValue;
                    finding.EsophagusID = (int?)cbbFindingEsophagus_3.SelectedValue;
                    finding.EGJunctionID = (int?)cbbFindingEGJunction_3.SelectedValue;
                    finding.CardiaID = (int?)cbbFindingCardia_3.SelectedValue;
                    finding.FundusID = (int?)cbbFindingFundus_3.SelectedValue;
                    finding.BodyID = (int?)cbbFindingBody_3.SelectedValue;
                    finding.AntrumID = (int?)cbbFindingAntrum_3.SelectedValue;
                    finding.PylorusID = (int?)cbbFindingPylorus_3.SelectedValue;
                    finding.DuodenalBulbID = (int?)cbbFindingDuodenalBulb_3.SelectedValue;
                    finding.SecondPartID = (int?)cbbFindingSecondPart_3.SelectedValue;
                    finding.Oropharynx = txtFindingOropharynx_3.Text;
                    finding.Esophagus = txtFindingEsophagus_3.Text;
                    finding.EGJunction = txtFindingEGJunction_3.Text;
                    finding.Cardia = txtFindingCardia_3.Text;
                    finding.Fundus = txtFindingFundus_3.Text;
                    finding.Body = txtFindingBody_3.Text;
                    finding.Antrum = txtFindingAntrum_3.Text;
                    finding.Pylorus = txtFindingPylorus_3.Text;
                    finding.DuodenalBulb = txtFindingDuodenalBulb_3.Text;
                    finding.SecondPart = txtFindingSecondPart_3.Text;
                    finding.PrincipalProcedureID = !string.IsNullOrWhiteSpace(txbFindingPrinncipalProcedureID_EGD.Text) ? Convert.ToInt32(txbFindingPrinncipalProcedureID_EGD.Text) : 0;
                    finding.PrincipalProcedureDetail = txbFindingPrinncipalProcedureCode_EGD + "-" + txbFindingPrinncipalProcedureText_EGD.Text;
                    finding.SupplementalProcedure1ID = !string.IsNullOrWhiteSpace(txbFindingSupplementalProcedureID_EGD.Text) ? Convert.ToInt32(txbFindingSupplementalProcedureID_EGD.Text) : 0;
                    finding.SupplementalProcedure1Detail = txbFindingSupplementalProcedureCode_EGD.Text + "-" + txbFindingSupplementalProcedureText_EGD.Text;
                    finding.SupplementalProcedure2ID = !string.IsNullOrWhiteSpace(txbFindingSupplementalProcedureID2_EGD.Text) ? Convert.ToInt32(txbFindingSupplementalProcedureID2_EGD.Text) : 0;
                    finding.SupplementalProcedure2Detail = txbFindingSupplementalProcedureCode2_EGD.Text + "-" + txbFindingSupplementalProcedureText2_EGD.Text;
                    finding.Procedure = txbFindingProcedure_EGD.Text;
                    finding.DxID1 = !string.IsNullOrWhiteSpace(txbFindingDx1ID_EGD.Text) ? Convert.ToInt32(txbFindingDx1ID_EGD.Text) : 0;
                    finding.DxID1Detail = txbFindingDx1Code_EGD.Text + "-" + txbFindingDx1Text_EGD.Text;
                    finding.DxID2 = !string.IsNullOrWhiteSpace(txbFindingDx2ID_EGD.Text) ? Convert.ToInt32(txbFindingDx2ID_EGD.Text) : 0;
                    finding.DxID2Detail = txbFindingDx2Code_EGD.Text + "-" + txbFindingDx2Text_EGD.Text;
                    finding.DxID3 = !string.IsNullOrWhiteSpace(txbFindingDx3ID_EGD.Text) ? Convert.ToInt32(txbFindingDx3ID_EGD.Text) : 0;
                    finding.DxID3Detail = txbFindingDx3Code_EGD.Text + "-" + txbFindingDx3Text_EGD.Text;
                    finding.DxID4 = !string.IsNullOrWhiteSpace(txbFindingDx4ID_EGD.Text) ? Convert.ToInt32(txbFindingDx4ID_EGD.Text) : 0;
                    finding.DxID4Detail = txbFindingDx4Code_EGD.Text + "-" + txbFindingDx4Text_EGD.Text;
                    finding.Complication = txbFindingComplication_EGD.Text;
                    finding.Histopathology = txbFindingHistopathology_EGD.Text;
                    finding.Recommendation = txbFindingRecommendation_EGD.Text;
                    finding.Comment = txbFindingComment_EGD.Text;
                }
                else if (procedureId == 2)
                {
                    finding.AnalCanalID = (int?)cbbFindingAnalCanal_Colono.SelectedValue;
                    finding.RectumID = (int?)cbbFindingRectum_Colono.SelectedValue;
                    finding.SigmoidColonID = (int?)cbbFindingSigmoid_Colono.SelectedValue;
                    finding.DescendingColonID = (int?)cbbFindingDescending_Colono.SelectedValue;
                    finding.SplenicFlexureID = (int?)cbbFindingFlexure_Colono.SelectedValue;
                    finding.TransverseColonID = (int?)cbbFindingTransverse_Colono.SelectedValue;
                    finding.HepaticFlexureID = (int?)cbbFindingHepatic_Colono.SelectedValue;
                    finding.AscendingColonID = (int?)cbbFindingAscending_Colono.SelectedValue;
                    finding.IleocecalVolveID = (int?)cbbFindingIleocecal_Colono.SelectedValue;
                    finding.CecumID = (int?)cbbFindingCecum_Colono.SelectedValue;
                    finding.TerminalIleumID = (int?)cbbFindingTerminal_Colono.SelectedValue;
                    finding.AnalCanal = txtFindingAnalCanal_Colono.Text;
                    finding.Rectum = txtFindingRectum_Colono.Text;
                    finding.SigmoidColon = txtFindingSigmoid_Colono.Text;
                    finding.DescendingColon = txtFindingDescending_Colono.Text;
                    finding.SplenicFlexure = txtFindingFlexure_Colono.Text;
                    finding.TransverseColon = txtFindingTransverse_Colono.Text;
                    finding.HepaticFlexure = txtFindingHepatic_Colono.Text;
                    finding.AscendingColon = txtFindingAscending_Colono.Text;
                    finding.IleocecalVolve = txtFindingIleocecal_Colono.Text;
                    finding.Cecum = txtFindingCecum_Colono.Text;
                    finding.TerminalIleum = txtFindingTerminal_Colono.Text;
                    finding.PrincipalProcedureID = !string.IsNullOrWhiteSpace(txbFindingPrinncipalProcedureID_Colono.Text) ? Convert.ToInt32(txbFindingPrinncipalProcedureID_Colono.Text) : 0;
                    finding.PrincipalProcedureDetail = txbFindingPrinncipalProcedureCode_Colono + "-" + txbFindingPrinncipalProcedureText_Colono.Text;
                    finding.SupplementalProcedure1ID = !string.IsNullOrWhiteSpace(txbFindingSupplementalProcedureID_Colono.Text) ? Convert.ToInt32(txbFindingSupplementalProcedureID_Colono.Text) : 0;
                    finding.SupplementalProcedure1Detail = txbFindingSupplementalProcedureCode_Colono.Text + "-" + txbFindingSupplementalProcedureText_Colono.Text;
                    finding.SupplementalProcedure2ID = !string.IsNullOrWhiteSpace(txbFindingSupplementalProcedure2ID_Colono.Text) ? Convert.ToInt32(txbFindingSupplementalProcedure2ID_Colono.Text) : 0;
                    finding.SupplementalProcedure2Detail = txbFindingSupplementalProcedure2Code_Colono.Text + "-" + txbFindingSupplementalProcedure2Text_Colono.Text;
                    finding.Procedure = txbFindingProcedure_Colono.Text;
                    finding.DxID1 = !string.IsNullOrWhiteSpace(txbFindingDx1ID_Colono.Text) ? Convert.ToInt32(txbFindingDx1ID_Colono.Text) : 0;
                    finding.DxID1Detail = txbFindingPrinncipalProcedureCode_Colono.Text + "-" + txbFindingPrinncipalProcedureText_Colono.Text;
                    finding.DxID2 = !string.IsNullOrWhiteSpace(txbFindingSupplementalProcedureID_Colono.Text) ? Convert.ToInt32(txbFindingSupplementalProcedureID_Colono.Text) : 0;
                    finding.DxID2Detail = txbFindingSupplementalProcedureCode_Colono.Text + "-" + txbFindingSupplementalProcedureText_Colono.Text;
                    finding.DxID3 = !string.IsNullOrWhiteSpace(txbFindingDx3ID_Colono.Text) ? Convert.ToInt32(txbFindingDx3ID_Colono.Text) : 0;
                    finding.DxID3Detail = txbFindingDx3Code_Colono.Text + "-" + txbFindingDx3Text_Colono.Text;
                    finding.DxID4 = !string.IsNullOrWhiteSpace(txbFindingDx4ID_Colono.Text) ? Convert.ToInt32(txbFindingDx4ID_Colono.Text) : 0;
                    finding.DxID4Detail = txbFindingDx4Code_Colono.Text + "-" + txbFindingDx4Text_Colono.Text;
                    finding.Complication = txbFindingComplication_Colono.Text;
                    finding.Histopathology = txbFindingHistopathology_Colono.Text;
                    finding.Recommendation = txbFindingRecommendation_Colono.Text;
                    finding.Comment = txbFindingComment_Colono.Text;
                }
                //else if (procedureId == 3)
                //{
                //    finding.EsophagusID = (int?)cbbFindingEsophagus_2.SelectedValue;
                //    finding.StomachID = (int?)cbbFindingStomach_2.SelectedValue;
                //    finding.DuodenumID = (int?)cbbFindingDuodenum_2.SelectedValue;
                //    finding.AmpullaOfVaterID = (int?)cbbFindingAmpulla_2.SelectedValue;
                //    finding.CholangiogramID = (int?)cbbFindingCholangiogram_2.SelectedValue;
                //    finding.PancreatogramID = (int?)cbbFindingPancreatogram_2.SelectedValue;
                //    finding.Esophagus = txtFindingEsophagus_2.Text;
                //    finding.Stomach = txtFindingStomach_2.Text;
                //    finding.Duodenum = txtFindingDuodenum_2.Text;
                //    finding.AmpullaOfVater = txtFindingAmpulla_2.Text;
                //    finding.Cholangiogram = txtFindingCholangiogram_2.Text;
                //    finding.Pancreatogram = txtFindingPancreatogram_2.Text;
                //}
                //else if (procedureId == 4)
                //{
                //    finding.VocalCordID = (int?)cbbFindingVocal_0.SelectedValue;
                //    finding.TracheaID = (int?)cbbFindingTrachea_0.SelectedValue;
                //    finding.CarinaID = (int?)cbbFindingCarina_0.SelectedValue;
                //    finding.RightMainID = (int?)cbbFindingRightMain_0.SelectedValue;
                //    finding.RightIntermideateID = (int?)cbbFindingRightIntermideate_0.SelectedValue;
                //    finding.RULID = (int?)cbbFindingRUL_0.SelectedValue;
                //    finding.RMLID = (int?)cbbFindingRML_0.SelectedValue;
                //    finding.RLLID = (int?)cbbFindingRLL_0.SelectedValue;
                //    finding.LeftMainID = (int?)cbbFindingLeftMain_0.SelectedValue;
                //    finding.LULID = (int?)cbbFindingLUL_0.SelectedValue;
                //    finding.LingularID = (int?)cbbFindingLingular_0.SelectedValue;
                //    finding.LLLID = (int?)cbbFindingLLL_0.SelectedValue;
                //    finding.VocalCord = txtFindingVocalCord_0.Text;
                //    finding.Trachea = txtFindingTrachea_0.Text;
                //    finding.Carina = txtFindingCarina_0.Text;
                //    finding.RightMain = txtFindingRightMain_0.Text;
                //    finding.RightIntermideate = txtFindingIntermideate_0.Text;
                //    finding.RUL = txtFindingRUL_0.Text;
                //    finding.RML = txtFindingRML_0.Text;
                //    finding.RLL = txtFindingRLL_0.Text;
                //    finding.LeftMain = txtFindingLeftMain_0.Text;
                //    finding.LUL = txtFindingLUL_0.Text;
                //    finding.Lingular = txtFindingLingular_0.Text;
                //    finding.LLL = txtFindingLLL_0.Text;
                //}
                //else
                //{
                //    finding.NasalCavityLeftID = (int?)cbbFiNCL.SelectedValue;
                //    finding.NasalCavityRightID = (int?)cbbFiNCR.SelectedValue;
                //    finding.SeptumID = (int?)cbbFiSeptum.SelectedValue;
                //    finding.NasopharynxID = (int?)cbbFiNasopharynx.SelectedValue;
                //    finding.RoofID = (int?)cbbFiRoof.SelectedValue;
                //    finding.PosteriorWallID = (int?)cbbFiPosteriorWall.SelectedValue;
                //    finding.RosenmullerFossaID = (int?)cbbFiRosenmullerFossa.SelectedValue;
                //    finding.EustachianTubeOrificeLeftID = (int?)cbbFiETOrificeL.SelectedValue;
                //    finding.EustachianTubeOrificeRightID = (int?)cbbFiETOrificeR.SelectedValue;
                //    finding.SoftPalateID = (int?)cbbFiSoftPalate.SelectedValue;
                //    finding.UvulaID = (int?)cbbFiUvula.SelectedValue;
                //    finding.TonsilID = (int?)cbbFiTonsil.SelectedValue;
                //    finding.BaseOfTongueID = (int?)cbbFiBaseOfTongue.SelectedValue;
                //    finding.ValleculaID = (int?)cbbFiVallecula.SelectedValue;
                //    finding.PyriformSinusLeftID = (int?)cbbFiPyrSinusL.SelectedValue;
                //    finding.PyriformSinusRightID = (int?)cbbFiPyrSinusR.SelectedValue;
                //    finding.PostcricoidID = (int?)cbbFiPostcricoid.SelectedValue;
                //    finding.PosteriorPharyngealWallID = (int?)cbbFiPosPhaWall.SelectedValue;
                //    finding.SupraglotticID = (int?)cbbFiSupraglottic.SelectedValue;
                //    finding.GlotticID = (int?)cbbFiGlottic.SelectedValue;
                //    finding.SubglotticID = (int?)cbbFiSubglottic.SelectedValue;
                //    finding.UESID = (int?)cbbFiUES.SelectedValue;
                //    finding.EsophagusID = (int?)cbbFiEsophagus.SelectedValue;
                //    finding.LESID = (int?)cbbFiLES.SelectedValue;
                //    finding.StomachID = (int?)cbbFiStomach.SelectedValue;
                //    finding.NasalCavityLeft = txbFiNCL.Text;
                //    finding.NasalCavityRight = txbFiNCR.Text;
                //    finding.Septum = txbFiSeptum.Text;
                //    finding.Nasopharynx = txbFiNasopharynx.Text;
                //    finding.Roof = txbFiRoof.Text;
                //    finding.PosteriorWall = txbFiPosteriorWall.Text;
                //    finding.RosenmullerFossa = txbFiRosenmullerFossa.Text;
                //    finding.EustachianTubeOrificeLeft = txbFiETOrificeL.Text;
                //    finding.EustachianTubeOrificeRight = txbFiETOrificeR.Text;
                //    finding.SoftPalate = txbFiSoftPalate.Text;
                //    finding.Uvula = txbFiUvula.Text;
                //    finding.Tonsil = txbFiTonsil.Text;
                //    finding.BaseOfTongue = txbFiBaseOfTongue.Text;
                //    finding.Vallecula = txbFiVallecula.Text;
                //    finding.PyriformSinusLeft = txbFiPyrSinusL.Text;
                //    finding.PyriformSinusRight = txbFiPyrSinusR.Text;
                //    finding.Postcricoid = txbFiPostcricoid.Text;
                //    finding.PosteriorPharyngealWall = txbFiPosPhaWall.Text;
                //    finding.Supraglottic = txbFiSupraglottic.Text;
                //    finding.Glottic = txbFiGlottic.Text;
                //    finding.Subglottic = txbFiSubglottic.Text;
                //    finding.UES = txbFiUES.Text;
                //    finding.Esophagus = txbFiEsophagus.Text;
                //    finding.LES = txbFiLES.Text;
                //    finding.Stomach = txbFiStomach.Text;
                //}
                finding.UpdateDate = System.DateTime.Now;
                finding.UpdateBy = _id;

                _db.SaveChanges();
            }
        }
        private int SaveLogEndoscopic(Endoscopic en, int patientId, int procedureId)
        {
            if (en == null) return 0;

            try
            {
                Endoscopic_Log log = new Endoscopic_Log()
                {
                    CreateDate = DateTime.Now,
                    EndoscopicID = en.EndoscopicID,
                    AnesNurse = en.AnesNurse,
                    Anesthesia = en.Anesthesia,
                    Arrive = en.Arrive,
                    Assistant1 = en.Assistant1,
                    Assistant2 = en.Assistant2,
                    Comment = en.Comment,
                    Complication = en.Complication,
                    Diagnosis = en.Diagnosis,
                    EndoscopistID = en.EndoscopistID,
                    EndRecordDate = en.EndRecordDate,
                    FindingID = en.FindingID,
                    FluDose = en.FluDose,
                    FollowUpCase = en.FollowUpCase,
                    History = en.History,
                    InCase = en.InCase,
                    Indication = en.Indication,
                    IndicationID = en.IndicationID,
                    IndicationOther = en.IndicationOther,
                    Instrument = en.Instrument,
                    Intervention = en.Intervention,
                    InterventionID = en.InterventionID,
                    MedicationID = en.MedicationID,
                    MedicationOther = en.MedicationOther,
                    NewCase = en.NewCase,
                    NurseFirstID = en.NurseFirstID,
                    NurseSecondID = en.NurseSecondID,
                    NurseThirthID = en.NurseThirthID,
                    PatientID = en.PatientID,
                    ProcedureID = en.ProcedureID,
                    ReferringPhysicain = en.ReferringPhysicain,
                    SpecimenID = en.SpecimenID,
                    StartRecordDate = en.StartRecordDate,
                    IsActive = true
                };
                _db.Endoscopic_Log.Add(log);
                if (_db.SaveChanges() > 0)
                {
                    var logEndoscopic = _db.Endoscopic_Log.Where(x => x.ProcedureID == procedureId && x.PatientID == patientId).OrderByDescending(o => o.CreateDate).FirstOrDefault();
                    if (logEndoscopic != null)
                    {
                        return logEndoscopic.ID;
                    }
                }
                return 0;

            }
            catch (Exception)
            {
                return 0;
            }
        }
        private void SaveLogHistory(int patientId, int procedureId, int? doctorID, int logEndoId = 0)
        {
            History history = new History();
            history.PatientID = patientId;
            history.EndoscopicID = logEndoId;
            history.ProcedureID = procedureId;
            history.DoctorID = doctorID;
            history.CreateDate = DateTime.Now;
            history.CreateBy = _id;
            history.IsActive = true;
            _db.Histories.Add(history);
            _db.SaveChanges();
        }
        private void SaveImage(int endoscopicID, int procedureID)
        {
            System.Windows.Forms.PictureBox[] boxes =
            {
                    pictureBoxSaved1,
                    pictureBoxSaved2,
                    pictureBoxSaved3,
                    pictureBoxSaved4,
                    pictureBoxSaved5,
                    pictureBoxSaved7,
                    pictureBoxSaved8,
                    pictureBoxSaved9,
                    pictureBoxSaved10,
                    pictureBoxSaved11,
                    pictureBoxSaved12,
                    pictureBoxSaved13,
                    pictureBoxSaved14,
                    pictureBoxSaved15,
                    pictureBoxSaved16,
                    pictureBoxSaved17,
                    pictureBoxSaved18
                };
            System.Windows.Forms.TextBox[] texts =
            {
                    txtPictureBoxSaved1,
                    txtPictureBoxSaved2,
                    txtPictureBoxSaved3,
                    txtPictureBoxSaved4,
                    txtPictureBoxSaved5,
                    txtPictureBoxSaved7,
                    txtPictureBoxSaved8,
                    txtPictureBoxSaved9,
                    txtPictureBoxSaved10,
                    txtPictureBoxSaved11,
                    txtPictureBoxSaved12,
                    txtPictureBoxSaved13,
                    txtPictureBoxSaved14,
                    txtPictureBoxSaved15,
                    txtPictureBoxSaved16,
                    txtPictureBoxSaved17,
                    txtPictureBoxSaved18
                };
            int i = 0;
            int seq = 1;
            foreach (var item in texts)
            {
                String Imgpath = boxes[i].ImageLocation != null ? boxes[i].ImageLocation.ToString() : "";
                var endoImgs = _db.EndoscopicImages.Where(x => x.EndoscopicID == endoscopicID && x.ProcedureID == procedureID && x.Seq == seq).FirstOrDefault();
                if (endoImgs != null)
                {
                    if (Imgpath == "")
                    {
                        _db.EndoscopicImages.Remove(endoImgs);
                    }
                    else
                    {
                        endoImgs.ImagePath = Imgpath;
                        endoImgs.ImageComment = item.Text;
                        endoImgs.Seq = i + 1;
                        endoImgs.UpdateBy = _id;
                        endoImgs.UpdateDate = DateTime.Now;
                    }
                }
                else
                {
                    EndoscopicImage endoscopicImage = new EndoscopicImage();
                    endoscopicImage.EndoscopicID = endoscopicID;
                    endoscopicImage.ProcedureID = procedureID;
                    endoscopicImage.ImagePath = Imgpath;
                    endoscopicImage.ImageComment = item.Text;
                    endoscopicImage.Seq = i + 1;
                    endoscopicImage.CreateBy = _id;
                    endoscopicImage.CreateDate = DateTime.Now;
                    endoscopicImage.UpdateBy = _id;
                    endoscopicImage.UpdateDate = DateTime.Now;
                    _db.EndoscopicImages.Add(endoscopicImage);
                }
                i++;
                seq++;
            }
        }
        private void SaveAllImage(int endoscopicID, int procedureID)
        {
            int i = 0;
            int seq = 1;
            foreach (var item in _imgPath.Values)
            {
                var endoAllImgs = _db.EndoscopicAllImages.Where(x => x.EndoscopicID == endoscopicID && x.ProcedureID == procedureID && x.Seq == seq).FirstOrDefault();
                if (item != null)
                {
                    if (endoAllImgs != null)
                    {
                        endoAllImgs.ImagePath = item.ToString();
                        endoAllImgs.UpdateBy = _id;
                        endoAllImgs.UpdateDate = DateTime.Now;
                    }
                    else
                    {
                        EndoscopicAllImage endoscopicAllImage = new EndoscopicAllImage();
                        endoscopicAllImage.EndoscopicID = endoscopicID;
                        endoscopicAllImage.ProcedureID = procedureID;
                        endoscopicAllImage.ImagePath = item.ToString();
                        endoscopicAllImage.Seq = i + 1;
                        endoscopicAllImage.CreateBy = _id;
                        endoscopicAllImage.CreateDate = DateTime.Now;
                        endoscopicAllImage.UpdateBy = _id;
                        endoscopicAllImage.UpdateDate = DateTime.Now;
                        _db.EndoscopicAllImages.Add(endoscopicAllImage);
                    }
                    i++;
                    seq++;
                }
                else
                {

                }
            }
        }

        private void UpdateDataAll(int procedureId, int patientId, Endoscopic endoscopic)
        {
            try
            {
                var findingData = _db.Findings.Where(x => x.PatientID == patientId).OrderByDescending(o => o.FindingID).FirstOrDefault();
                endoscopic.FindingID = findingData.FindingID;
                endoscopic.IsSaved = true;
                endoscopic.ProcedureID = procedureId;
                //endoscopic.Diagnosis = txtDiagnosis.Text;
                //endoscopic.Complication = txtComplication.Text;
                //endoscopic.Comment = txtComment.Text;
                if (procedureId == 1)
                {
                    endoscopic.EndoscopistID = (int?)cbbGeneralDoctor_EGD.SelectedValue;
                    endoscopic.Arrive = dpGeneralFrom_EGD.Value;
                    endoscopic.Instrument = txbGeneralInstrument_EGD.Text;
                    endoscopic.MedicationID = (int?)cbbGeneralMedication_EGD.SelectedValue;
                    endoscopic.Indication = (int?)cbbGeneralIndication_EGD.SelectedValue;
                    endoscopic.NurseFirstID = (int?)cbbGeneralNurse1_EGD.SelectedValue;
                    endoscopic.NurseSecondID = (int?)cbbGeneralNurse2_EGD.SelectedValue;
                    endoscopic.NurseThirthID = (int?)cbbGeneralNurse3_EGD.SelectedValue;
                    endoscopic.StartRecordDate = new DateTime(
                            dpGeneralFrom_EGD.Value.Year,
                            dpGeneralFrom_EGD.Value.Month,
                            dpGeneralFrom_EGD.Value.Day,
                            dpGeneralFrom_EGD.Value.Hour,
                            dpGeneralFrom_EGD.Value.Minute,
                            dpGeneralFrom_EGD.Value.Second);
                    endoscopic.EndRecordDate = new DateTime(
                            dpGeneralTo_EGD.Value.Year,
                            dpGeneralTo_EGD.Value.Month,
                            dpGeneralTo_EGD.Value.Day,
                            dpGeneralTo_EGD.Value.Hour,
                            dpGeneralTo_EGD.Value.Minute,
                            dpGeneralTo_EGD.Value.Second);
                    endoscopic.MedicationOther = txbGeneralMedication_EGD.Text;
                    endoscopic.IndicationOther = txbGeneralIndication_EGD.Text;
                    endoscopic.DxId1 = !string.IsNullOrWhiteSpace(txbGeneralDx1ID_EGD.Text) ? Convert.ToInt32(txbGeneralDx1ID_EGD.Text) : 0;
                    endoscopic.DxId1Detail = txbPreDiagCode_EGD.Text + "-" + txbPreDiagText_EGD.Text;
                    endoscopic.DxId2 = !string.IsNullOrWhiteSpace(txbGeneralDx2ID_EGD.Text) ? Convert.ToInt32(txbGeneralDx2ID_EGD.Text) : 0;
                    endoscopic.DxId2Detail = txbICD10Code_EGD.Text + "-" + txbICD10Text_EGD.Text;
                    endoscopic.BriefHistory = txbBriefHistory_EGD.Text;
                }
                else if (procedureId == 2)
                {
                    endoscopic.EndoscopistID = (int?)cbbGeneralDoctor_Colono.SelectedValue;
                    endoscopic.Arrive = dpGeneralFrom_Colono.Value;
                    endoscopic.Instrument = txbGeneralInstrument_Colono.Text;
                    endoscopic.MedicationID = (int?)cbbGeneralMedication_Colono.SelectedValue;
                    endoscopic.NurseFirstID = (int?)cbbGeneralNurse1_Colono.SelectedValue;
                    endoscopic.NurseSecondID = (int?)cbbGeneralNurse2_Colono.SelectedValue;
                    endoscopic.NurseThirthID = (int?)cbbGeneralNurse3_Colono.SelectedValue;
                    endoscopic.StartRecordDate = new DateTime(
                            dpGeneralFrom_Colono.Value.Year,
                            dpGeneralFrom_Colono.Value.Month,
                            dpGeneralFrom_Colono.Value.Day,
                            dpGeneralFrom_Colono.Value.Hour,
                            dpGeneralFrom_Colono.Value.Minute,
                            dpGeneralFrom_Colono.Value.Second);
                    endoscopic.EndRecordDate = new DateTime(
                            dpGeneralTo_Colono.Value.Year,
                            dpGeneralTo_Colono.Value.Month,
                            dpGeneralTo_Colono.Value.Day,
                            dpGeneralTo_Colono.Value.Hour,
                            dpGeneralTo_Colono.Value.Minute,
                            dpGeneralTo_Colono.Value.Second);
                    endoscopic.MedicationOther = txbGeneralMedication_Colono.Text;
                    endoscopic.IndicationOther = txbGeneralIndication_Colono.Text;
                    endoscopic.DxId1 = !string.IsNullOrWhiteSpace(txbGeneralDx1ID_Colono.Text) ? Convert.ToInt32(txbGeneralDx1ID_Colono.Text) : 0;
                    endoscopic.DxId1Detail = txbPreDiagCode_Colono.Text + "-" + txbPreDiagText_Colono.Text;
                    endoscopic.DxId2 = !string.IsNullOrWhiteSpace(txbGeneralDx2ID_Colono.Text) ? Convert.ToInt32(txbGeneralDx2ID_Colono.Text) : 0;
                    endoscopic.DxId2Detail = txbICD10Code_Colono.Text + "-" + txbICD10Text_Colono.Text;
                    endoscopic.BriefHistory = txbBriefHistory_Colono.Text;
                    endoscopic.BowelPreparationRegimen = txbGeneralBowelPreparationRegimen_Colono.Text;
                    endoscopic.BowelPreparationResult = txbGeneralBowelPreparationResult_colono.Text;

                }
                //endoscopic.SpecimenID = SaveSpecimen(procedureId);
                //endoscopic.StartRecordDate = recordStartDate;
                //endoscopic.EndRecordDate = recordEndDate;
                endoscopic.UpdateDate = System.DateTime.Now;
                endoscopic.UpdateBy = _id;

                var patient = _db.Patients.Where(x => x.PatientID == patientId).FirstOrDefault();
                if (patient != null)
                {
                    patient.Fullname = procedureId == 1 ? txbFullName_EGD.Text : txbFullName_Colono.Text;
                    patient.Age = procedureId == 1 ? Convert.ToInt32(txbAge_EGD.Text) : Convert.ToInt32(txbAge_Colono);
                    patient.ProcedureID = procedureId;
                    patient.UpdateBy = _id;
                    patient.UpdateDate = DateTime.Now;
                    if (procedureId == 1) // || procedureId == 2 || procedureId == 3)
                    {
                        patient.DoctorID = (int?)cbbGeneralDoctor_EGD.SelectedValue;
                        patient.NurseFirstID = (int?)cbbGeneralNurse1_EGD.SelectedValue;
                        patient.NurseSecondID = (int?)cbbGeneralNurse2_EGD.SelectedValue;
                        patient.NurseThirthID = (int?)cbbGeneralNurse3_EGD.SelectedValue;
                        patient.AnesthesiaID = (int?)cbbGeneralAnesthesia_EGD.SelectedValue;
                    }
                    else
                    {
                        patient.DoctorID = (int?)cbbGeneralDoctor_Colono.SelectedValue;
                        patient.NurseFirstID = (int?)cbbGeneralNurse1_Colono.SelectedValue;
                        patient.NurseSecondID = (int?)cbbGeneralNurse2_Colono.SelectedValue;
                        patient.NurseThirthID = (int?)cbbGeneralNurse3_Colono.SelectedValue;
                        patient.AnesthesiaID = (int?)cbbGeneralAnesthesia_Colono.SelectedValue;
                    }
                }

                UpdateFinding(procedureId);
                //UpdateAppointment(endoscopicId);
                SaveImage(endoscopic.EndoscopicID, procedureId);
                SaveAllImage(endoscopic.EndoscopicID, procedureId);
                //SaveVideo(endoscopic.EndoscopicID, procedureId);
                SaveLogEndoscopic(endoscopic, patientId, procedureId);
                SaveLogHistory(patientId, procedureId, patient.DoctorID, endoscopic.EndoscopicID);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UpdateEndoscopic(int patientId, int procedureId, int endoscopicId)
        {
            Endoscopic endo = new Endoscopic();
            if (endoscopicId == 0)
            {
                Endoscopic endoscopic = new Endoscopic() { PatientID = patientId, IsSaved = false, ProcedureID = procedureId, CreateBy = _id, CreateDate = System.DateTime.Now };
                _db.Endoscopics.Add(endoscopic);

                Finding finding = new Finding() { PatientID = patientId, CreateBy = _id, CreateDate = System.DateTime.Now };
                _db.Findings.Add(finding);
                _db.SaveChanges();

                var endos = _db.Endoscopics.ToList();
                endo = endos.LastOrDefault();
                endoscopicId = endo.EndoscopicID;
            }
            else
            {
                endo = _db.Endoscopics.Where(x => x.EndoscopicID == endoscopicId).FirstOrDefault();
            }
            UpdateDataAll(procedureId, patientId, endo);

        }
        private bool OnSave(string hn, int patientId, int procedureId, int endoscopicId)
        {
            if (string.IsNullOrWhiteSpace(hn) || patientId <= 0 || procedureId <= 0 || endoscopicId <= 0)
                return false;

            try
            {
                UpdateEndoscopic(patientId, procedureId, endoscopicId);

                MessageBox.Show(Constant.STATUS_SUCCESS, "Save Form", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region Open/Close TabPage

        private void OpenTabPage(int procedureId)
        {
            RemoveTabPage();

            if (procedureId == 1)   // EGD
            {
                TabPage[] tabs = { tabGeneralEGD, tabFindingEGD };
                tabControl1.TabPages.AddRange(tabs);
                // General Tab
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
                _dropdownListService.DropdownFinancial(cbbGeneralFinancial_EGD);

                // Finding Tab
                _dropdownListService.DropdownOropharynx(cbbFindingOropharynx_EGD);
            }
            else if (procedureId == 2)  // Colonoscopy
            {
                TabPage[] tabs = { tabGeneralColonoscopy, tabFindingColonoscopy };
                tabControl1.TabPages.AddRange(tabs);
                _dropdownListService.DropdownOPD(cbbGeneralOPD_Colono);
                _dropdownListService.DropdownWard(cbbGeneralWard_Colono);
                _dropdownListService.DropdownDoctor(cbbGeneralDoctor_Colono);
                _dropdownListService.DropdownAnesthesia(cbbGeneralAnesthesia_Colono);
                _dropdownListService.DropdownAnesthesist(cbbGeneralAnesthesist_Colono);
                _dropdownListService.DropdownNurse(cbbGeneralNurse1_Colono);
                _dropdownListService.DropdownNurse(cbbGeneralNurse2_Colono);
                _dropdownListService.DropdownNurse(cbbGeneralNurse3_Colono);
                _dropdownListService.DropdownMedication(cbbGeneralMedication_Colono);
                _dropdownListService.DropdownIndication(cbbGeneralIndication_Colono);
                _dropdownListService.DropdownFinancial(cbbGeneralFinancial_Colono);
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


        private void pictureBoxRefresh()
        {
            pictureBoxSaved1.ImageLocation = pictureBoxSaved1.ImageLocation;
            pictureBoxSaved1.Refresh();
            pictureBoxSaved2.ImageLocation = pictureBoxSaved2.ImageLocation;
            pictureBoxSaved2.Refresh();
            pictureBoxSaved3.ImageLocation = pictureBoxSaved3.ImageLocation;
            pictureBoxSaved3.Refresh();
            pictureBoxSaved4.ImageLocation = pictureBoxSaved4.ImageLocation;
            pictureBoxSaved4.Refresh();
            pictureBoxSaved5.ImageLocation = pictureBoxSaved5.ImageLocation;
            pictureBoxSaved5.Refresh();

            pictureBoxSaved7.ImageLocation = pictureBoxSaved7.ImageLocation;
            pictureBoxSaved7.Refresh();
            pictureBoxSaved8.ImageLocation = pictureBoxSaved8.ImageLocation;
            pictureBoxSaved8.Refresh();
            pictureBoxSaved9.ImageLocation = pictureBoxSaved9.ImageLocation;
            pictureBoxSaved9.Refresh();
            pictureBoxSaved9.ImageLocation = pictureBoxSaved9.ImageLocation;
            pictureBoxSaved9.Refresh();
            pictureBoxSaved10.ImageLocation = pictureBoxSaved10.ImageLocation;
            pictureBoxSaved10.Refresh();
            pictureBoxSaved11.ImageLocation = pictureBoxSaved11.ImageLocation;
            pictureBoxSaved11.Refresh();
            pictureBoxSaved12.ImageLocation = pictureBoxSaved12.ImageLocation;
            pictureBoxSaved12.Refresh();
            pictureBoxSaved13.ImageLocation = pictureBoxSaved13.ImageLocation;
            pictureBoxSaved13.Refresh();
            pictureBoxSaved14.ImageLocation = pictureBoxSaved14.ImageLocation;
            pictureBoxSaved14.Refresh();
            pictureBoxSaved15.ImageLocation = pictureBoxSaved15.ImageLocation;
            pictureBoxSaved15.Refresh();
            pictureBoxSaved16.ImageLocation = pictureBoxSaved16.ImageLocation;
            pictureBoxSaved16.Refresh();
            pictureBoxSaved17.ImageLocation = pictureBoxSaved17.ImageLocation;
            pictureBoxSaved17.Refresh();
            pictureBoxSaved18.ImageLocation = pictureBoxSaved18.ImageLocation;
            pictureBoxSaved18.Refresh();

            foreach (var i in formPopup.Controls)
            {
                if (i.GetType() == typeof(PictureBox))
                {
                    PictureBox p = i as PictureBox;
                    p.ImageLocation = p.ImageLocation;
                    p.Refresh();
                }
            }
        }
        private async Task<Bitmap> ResizeImgToPictureBox(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            //create new destImage object
            var destImage = new Bitmap(width, height);

            //maintains DPI regardless of physical size
            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                //determines whether pixels from a source image overwrite or are combined with background pixels.
                graphics.CompositingMode = CompositingMode.SourceCopy;
                //determines the rendering quality level of layered images.
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                // determines how intermediate values between two endpoints are calculated
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                //specifies whether lines, curves, and the edges of filled areas use smoothing 
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                //affects rendering quality when drawing the new image
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    //prevents ghosting around the image borders
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            return await Task.FromResult(destImage);
        }

        private async Task<Image> ResizeImg(Image img, int x, int y, int width, int height, bool isFullScreen)
        {
            Bitmap reImg = await ResizeImgToPictureBox(img, pictureBoxSnapshot.Width, pictureBoxSnapshot.Height);

            Bitmap crpImg = null;
            if (!isFullScreen)
            {
                crpImg = new Bitmap(pictureBoxSnapshot.Width, pictureBoxSnapshot.Height);
                Graphics grp = Graphics.FromImage(crpImg);
                Rectangle rect = new Rectangle(x, y, width, height);
                Rectangle dest = new Rectangle(0, 0, pictureBoxSnapshot.Width, pictureBoxSnapshot.Height);
                grp.DrawImage(reImg, dest, rect, GraphicsUnit.Pixel);
                grp.Dispose();
            }
            else
            {
                crpImg = new Bitmap(img, img.Width, img.Height);
            }
            return (Image)crpImg;
        }

        private async Task<string> saveImageFile(Bitmap img, int num)
        {
            Image cropedImg = null;
            string ImgPath = null;

            try
            {
                ++_item;
                string filename = System.IO.Path.GetFileName(openFileDialog1.FileName);
                string filenameExtension = System.IO.Path.GetExtension(filename);
                if (filename == null)
                {
                    MessageBox.Show("Please select a valid image.");
                }
                else
                {
                    string namaImage = "Image";
                    string nameCapture = String.Format("{0}_{1}.jpg", namaImage, _item);

                    int aspectRatio_X = 0;
                    int aspectRatio_Y = 0;
                    int width = img.Width;
                    int height = img.Height;
                    bool isFullScreen = false;

                    switch (_aspectRatioID)
                    {
                        case 0://Custom
                            var ratio = _db.Users.Where(x => x.Id == _id).Select(x => new { x.CrpX, x.CrpY, x.CrpWidth, x.CrpHeight }).FirstOrDefault();
                            aspectRatio_X = ratio.CrpX ?? 0;
                            aspectRatio_Y = ratio.CrpY ?? 0;
                            width = ratio.CrpWidth ?? width;
                            height = ratio.CrpHeight ?? height;
                            break;
                        default://FullScreen
                            isFullScreen = true;
                            break;
                    }
                    _pathFolderImageSave = _pathFolderImage + _hnNo + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + cbbProcedureList.Text + @"\" + _appointmentId + @"\";
                    if (!Directory.Exists(_pathFolderImageSave))
                    {
                        Directory.CreateDirectory(_pathFolderImageSave);
                    }

                    cropedImg = await ResizeImg(img, aspectRatio_X, aspectRatio_Y, width, height, isFullScreen);
                    cropedImg.Save(_pathFolderImageSave + nameCapture, System.Drawing.Imaging.ImageFormat.Png);
                    ImgPath = _pathFolderImageSave + nameCapture;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "File Already exits");
            }

            return ImgPath;
        }

        private async Task<string> uploadImageFile(int num)
        {
            openFileDialog1.InitialDirectory = _initialDirectoryUpload;
            openFileDialog1.Title = _titleUpload;
            openFileDialog1.Filter = _filterUpload;
            openFileDialog1.FilterIndex = 1;

            string cropedImgPath = null;

            try
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog1.CheckFileExists)
                    {
                        Bitmap Imageupload = new Bitmap(openFileDialog1.FileName);
                        cropedImgPath = await saveImageFile(Imageupload, num);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return cropedImgPath;
        }


        #region btnDeletePictureBoxSaved_Click
        private void btnDeletePictureBoxSaved1_Click(object sender, EventArgs e)
        {
            pictureBoxSaved1.ImageLocation = null;
            pictureBoxSaved1.Update();
            btnDeletePictureBoxSaved1.Visible = false;
            btnEditPic1.Visible = false;
        }
        private void btnDeletePictureBoxSaved2_Click(object sender, EventArgs e)
        {
            pictureBoxSaved2.ImageLocation = null;
            pictureBoxSaved2.Update();
            btnDeletePictureBoxSaved2.Visible = false;
            btnEditPic2.Visible = false;
        }
        private void btnDeletePictureBoxSaved3_Click(object sender, EventArgs e)
        {
            pictureBoxSaved3.ImageLocation = null;
            pictureBoxSaved3.Update();
            btnDeletePictureBoxSaved3.Visible = false;
            btnEditPic3.Visible = false;
        }
        private void btnDeletePictureBoxSaved4_Click(object sender, EventArgs e)
        {
            pictureBoxSaved4.ImageLocation = null;
            pictureBoxSaved4.Update();
            btnDeletePictureBoxSaved4.Visible = false;
            btnEditPic4.Visible = false;
        }
        private void btnDeletePictureBoxSaved5_Click(object sender, EventArgs e)
        {

            pictureBoxSaved5.ImageLocation = null;
            pictureBoxSaved5.Update();
            btnDeletePictureBoxSaved5.Visible = false;
            btnEditPic5.Visible = false;
        }
        //private void btnDeletePictureBoxSaved6_Click(object sender, EventArgs e)
        //{
        //    pictureBoxSaved6.ImageLocation = null;
        //    pictureBoxSaved6.Update();
        //    btnDeletePictureBoxSaved6.Visible = false;
        //    btnEditPic6.Visible = false;
        //}
        private void btnDeletePictureBoxSaved7_Click(object sender, EventArgs e)
        {
            pictureBoxSaved7.ImageLocation = null;
            pictureBoxSaved7.Update();
            btnDeletePictureBoxSaved7.Visible = false;
            btnEditPic7.Visible = false;
        }
        private void btnDeletePictureBoxSaved8_Click(object sender, EventArgs e)
        {
            pictureBoxSaved8.ImageLocation = null;
            pictureBoxSaved8.Update();
            btnDeletePictureBoxSaved8.Visible = false;
            btnEditPic8.Visible = false;
        }
        private void btnDeletePictureBoxSaved9_Click(object sender, EventArgs e)
        {
            pictureBoxSaved9.ImageLocation = null;
            pictureBoxSaved9.Update();
            btnDeletePictureBoxSaved9.Visible = false;
            btnEditPic9.Visible = false;
        }
        private void btnDeletePictureBoxSaved10_Click(object sender, EventArgs e)
        {
            pictureBoxSaved10.ImageLocation = null;
            pictureBoxSaved10.Update();
            btnDeletePictureBoxSaved10.Visible = false;
            btnEditPic10.Visible = false;
        }
        private void btnDeletePictureBoxSaved11_Click(object sender, EventArgs e)
        {
            pictureBoxSaved11.ImageLocation = null;
            pictureBoxSaved11.Update();
            btnDeletePictureBoxSaved11.Visible = false;
            btnEditPic11.Visible = false;
        }
        private void btnDeletePictureBoxSaved12_Click(object sender, EventArgs e)
        {
            pictureBoxSaved12.ImageLocation = null;
            pictureBoxSaved12.Update();
            btnDeletePictureBoxSaved12.Visible = false;
            btnEditPic12.Visible = false;
        }
        private void btnDeletePictureBoxSaved13_Click(object sender, EventArgs e)
        {
            pictureBoxSaved13.ImageLocation = null;
            pictureBoxSaved13.Update();
            btnDeletePictureBoxSaved13.Visible = false;
            btnEditPic13.Visible = false;
        }
        private void btnDeletePictureBoxSaved14_Click(object sender, EventArgs e)
        {
            pictureBoxSaved14.ImageLocation = null;
            pictureBoxSaved14.Update();
            btnDeletePictureBoxSaved14.Visible = false;
            btnEditPic14.Visible = false;
        }
        private void btnDeletePictureBoxSaved15_Click(object sender, EventArgs e)
        {
            pictureBoxSaved15.ImageLocation = null;
            pictureBoxSaved15.Update();
            btnDeletePictureBoxSaved15.Visible = false;
            btnEditPic15.Visible = false;
        }
        private void btnDeletePictureBoxSaved16_Click(object sender, EventArgs e)
        {
            pictureBoxSaved16.ImageLocation = null;
            pictureBoxSaved16.Update();
            btnDeletePictureBoxSaved16.Visible = false;
            btnEditPic16.Visible = false;
        }
        private void btnDeletePictureBoxSaved17_Click(object sender, EventArgs e)
        {
            pictureBoxSaved17.ImageLocation = null;
            pictureBoxSaved17.Update();
            btnDeletePictureBoxSaved17.Visible = false;
            btnEditPic17.Visible = false;
        }
        private void btnDeletePictureBoxSaved18_Click(object sender, EventArgs e)
        {
            pictureBoxSaved18.ImageLocation = null;
            pictureBoxSaved18.Update();
            btnDeletePictureBoxSaved18.Visible = false;
            btnEditPic18.Visible = false;
        }
        #endregion

        #region BtnEditPic_Click
        private void btnEditPic1_Click(object sender, EventArgs e)
        {
            EditPictureCaptureOnClick(pictureBoxSaved1);
        }
        private void btnEditPic2_Click(object sender, EventArgs e)
        {
            EditPictureCaptureOnClick(pictureBoxSaved2);
        }
        private void btnEditPic3_Click(object sender, EventArgs e)
        {
            EditPictureCaptureOnClick(pictureBoxSaved3);
        }
        private void btnEditPic4_Click(object sender, EventArgs e)
        {
            EditPictureCaptureOnClick(pictureBoxSaved4);
        }
        private void btnEditPic5_Click(object sender, EventArgs e)
        {
            EditPictureCaptureOnClick(pictureBoxSaved5);
        }
        //private void btnEditPic6_Click(object sender, EventArgs e)
        //{
        //    EditPictureCaptureOnClick(pictureBoxSaved6);
        //}
        private void btnEditPic7_Click(object sender, EventArgs e)
        {
            EditPictureCaptureOnClick(pictureBoxSaved7);
        }
        private void btnEditPic8_Click(object sender, EventArgs e)
        {
            EditPictureCaptureOnClick(pictureBoxSaved8);
        }
        private void btnEditPic9_Click(object sender, EventArgs e)
        {
            EditPictureCaptureOnClick(pictureBoxSaved9);
        }
        private void btnEditPic10_Click(object sender, EventArgs e)
        {
            EditPictureCaptureOnClick(pictureBoxSaved10);
        }
        private void btnEditPic11_Click(object sender, EventArgs e)
        {
            EditPictureCaptureOnClick(pictureBoxSaved11);
        }
        private void btnEditPic12_Click(object sender, EventArgs e)
        {
            EditPictureCaptureOnClick(pictureBoxSaved12);
        }
        private void btnEditPic13_Click(object sender, EventArgs e)
        {
            EditPictureCaptureOnClick(pictureBoxSaved13);
        }
        private void btnEditPic14_Click(object sender, EventArgs e)
        {
            EditPictureCaptureOnClick(pictureBoxSaved14);
        }
        private void btnEditPic15_Click(object sender, EventArgs e)
        {
            EditPictureCaptureOnClick(pictureBoxSaved15);
        }
        private void btnEditPic16_Click(object sender, EventArgs e)
        {
            EditPictureCaptureOnClick(pictureBoxSaved16);
        }
        private void btnEditPic17_Click(object sender, EventArgs e)
        {
            EditPictureCaptureOnClick(pictureBoxSaved17);
        }
        private void btnEditPic18_Click(object sender, EventArgs e)
        {
            EditPictureCaptureOnClick(pictureBoxSaved18);
        }
        public void EditPictureCaptureOnClick(PictureBox pictureBox)
        {
            using (System.Diagnostics.Process ExternalProcess = new System.Diagnostics.Process())
            {
                ExternalProcess.StartInfo.FileName = ("mspaint.exe");
                string imgLo = string.Concat("\"", pictureBox.ImageLocation, "\"");
                ExternalProcess.StartInfo.Arguments = imgLo;
                ExternalProcess.StartInfo.UseShellExecute = true;
                ExternalProcess.Start();
                ExternalProcess.WaitForExit();
            }
            pictureBoxRefresh();
        }

        #endregion

        #region BtnPictureBoxSaved_Click
        private void btnPictureBoxSaved1_Click(object sender, EventArgs e)
        {
            SavePictureBoxOnClick(1, pictureBoxSaved1, btnEditPic1, btnDeletePictureBoxSaved1);
        }
        private void btnPictureBoxSaved2_Click(object sender, EventArgs e)
        {
            SavePictureBoxOnClick(2, pictureBoxSaved2, btnEditPic2, btnDeletePictureBoxSaved2);
        }
        private void btnPictureBoxSaved3_Click(object sender, EventArgs e)
        {
            SavePictureBoxOnClick(3, pictureBoxSaved3, btnEditPic3, btnDeletePictureBoxSaved3);
        }
        private void btnPictureBoxSaved4_Click(object sender, EventArgs e)
        {
            SavePictureBoxOnClick(4, pictureBoxSaved4, btnEditPic4, btnDeletePictureBoxSaved4);
        }
        private void btnPictureBoxSaved5_Click(object sender, EventArgs e)
        {
            SavePictureBoxOnClick(5, pictureBoxSaved5, btnEditPic5, btnDeletePictureBoxSaved5);
        }
        private void btnPictureBoxSaved6_Click(object sender, EventArgs e)
        {
            SavePictureBoxOnClick(6, pictureBoxSaved7, btnEditPic7, btnDeletePictureBoxSaved7);
        }
        private void btnPictureBoxSaved7_Click(object sender, EventArgs e)
        {
            SavePictureBoxOnClick(7, pictureBoxSaved8, btnEditPic8, btnDeletePictureBoxSaved8);
        }
        private void btnPictureBoxSaved8_Click(object sender, EventArgs e)
        {
            SavePictureBoxOnClick(8, pictureBoxSaved9, btnEditPic9, btnDeletePictureBoxSaved9);
        }

        //private void btnPictureBoxSaved9_Click(object sender, EventArgs e)
        //{
        //    SavePictureBoxOnClick(9, pictureBoxSaved9, btnEditPic9, btnDeletePictureBoxSaved9);
        //}
        private void btnPictureBoxSaved10_Click(object sender, EventArgs e)
        {
            SavePictureBoxOnClick(10, pictureBoxSaved10, btnEditPic10, btnDeletePictureBoxSaved10);
        }
        private void btnPictureBoxSaved11_Click(object sender, EventArgs e)
        {
            SavePictureBoxOnClick(11, pictureBoxSaved11, btnEditPic11, btnDeletePictureBoxSaved11);
        }
        private void btnPictureBoxSaved12_Click(object sender, EventArgs e)
        {
            SavePictureBoxOnClick(12, pictureBoxSaved12, btnEditPic12, btnDeletePictureBoxSaved12);
        }
        private void btnPictureBoxSaved13_Click(object sender, EventArgs e)
        {
            SavePictureBoxOnClick(13, pictureBoxSaved13, btnEditPic13, btnDeletePictureBoxSaved13);
        }
        private void btnPictureBoxSaved14_Click(object sender, EventArgs e)
        {
            SavePictureBoxOnClick(14, pictureBoxSaved14, btnEditPic14, btnDeletePictureBoxSaved14);
        }
        private void btnPictureBoxSaved15_Click(object sender, EventArgs e)
        {
            SavePictureBoxOnClick(15, pictureBoxSaved15, btnEditPic15, btnDeletePictureBoxSaved15);
        }
        private void btnPictureBoxSaved16_Click(object sender, EventArgs e)
        {
            SavePictureBoxOnClick(16, pictureBoxSaved16, btnEditPic16, btnDeletePictureBoxSaved16);
        }
        private void btnPictureBoxSaved17_Click(object sender, EventArgs e)
        {
            SavePictureBoxOnClick(17, pictureBoxSaved17, btnEditPic17, btnDeletePictureBoxSaved17);
        }
        private void btnPictureBoxSaved18_Click(object sender, EventArgs e)
        {
            SavePictureBoxOnClick(18, pictureBoxSaved18, btnEditPic18, btnDeletePictureBoxSaved18);
        }
        public async void SavePictureBoxOnClick(int num, PictureBox pictureBox, Button btnEdit, Button btnDelete)
        {
            string FilePath = await uploadImageFile(num);
            if (FilePath != null)
            {
                pictureBox.ImageLocation = FilePath;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Update();
                btnEdit.Visible = true;
                btnDelete.Visible = true;
            }
        }
        #endregion

    }
}
