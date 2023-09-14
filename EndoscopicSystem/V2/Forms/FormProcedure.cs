using Accord.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using EndoscopicSystem.Constants;
using EndoscopicSystem.Entities;
using EndoscopicSystem.Forms;
using EndoscopicSystem.Repository;
using EndoscopicSystem.V2.Forms.src;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EndoscopicSystem.V2.Forms
{
    public partial class FormProcedure : Form
    {
        #region Variables
        private readonly string _initialDirectoryUpload = "C://Desktop";
        private readonly string _titleUpload = "Select image to be upload.";
        private readonly string _filterUpload = "Image Only(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
        private readonly string _pathFolderImage = ConfigurationManager.AppSettings["pathSaveImageCapture"];
        private string _pathFolderImageSave, _hnNo, _pathImg, _fileName = ".jpg", _vdoPath, _markField;
        private int _id, _procedureId, _appointmentId, _patientId, _endoscopicId, _item, _aspectRatioID = 1;
        private bool isSaved = false;
        private readonly GetDropdownList _dropdownRepo = new GetDropdownList();
        private readonly EndoscopicEntities _db = new EndoscopicEntities();
        private DropdownListService _dropdownListService = new DropdownListService();
        public Dictionary<int, string> _imgPath = new Dictionary<int, string>();
        private TextBox lastFocused;
        public Form formPopup = new Form();
        private List<FindingLabel> _findingLabels = new List<FindingLabel>();
        private List<ICD9> _iCD9s = new List<ICD9>();
        private List<ICD10> _iCD10s = new List<ICD10>();
        private List<ProcedureDetail> _procedureDetails = new List<ProcedureDetail>();
        private List<Complication> _complications = new List<Complication>();
        private List<Histopathology> _histopathologies = new List<Histopathology>();
        private List<RapidUreaseTest> _rapidUreaseTests = new List<RapidUreaseTest>();
        private List<Recommendation> _recommendations = new List<Recommendation>();
        #endregion

        public FormProcedure(int id, string hn, int procId, int appId, int endoId, string pathImg, string pathVdo)
        {
            InitializeComponent();

            this._id = id;
            this._hnNo = hn;
            this._procedureId = procId;
            this._appointmentId = appId;
            this._endoscopicId = endoId;
            this._pathFolderImageSave = pathImg;
            this._vdoPath = pathVdo;
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

            listBox1.Items.Clear();

            LoadTextBoxAutoComplete(txtPictureBoxSaved1);
            LoadTextBoxAutoComplete(txtPictureBoxSaved2);
            LoadTextBoxAutoComplete(txtPictureBoxSaved3);
            LoadTextBoxAutoComplete(txtPictureBoxSaved4);
            LoadTextBoxAutoComplete(txtPictureBoxSaved5);
            LoadTextBoxAutoComplete(txtPictureBoxSaved6);
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
            LoadTextBoxAutoComplete(txtPictureBoxSaved19);
            LoadTextBoxAutoComplete(txtPictureBoxSaved20);
            LoadTextBoxAutoComplete(txtPictureBoxSaved21);
            LoadTextBoxAutoComplete(txtPictureBoxSaved22);
            LoadTextBoxAutoComplete(txtPictureBoxSaved23);
            LoadTextBoxAutoComplete(txtPictureBoxSaved24);
            LoadTextBoxAutoComplete(txtPictureBoxSaved25);
            LoadTextBoxAutoComplete(txtPictureBoxSaved26);
            LoadTextBoxAutoComplete(txtPictureBoxSaved27);
            LoadTextBoxAutoComplete(txtPictureBoxSaved28);
            LoadTextBoxAutoComplete(txtPictureBoxSaved29);
            LoadTextBoxAutoComplete(txtPictureBoxSaved30);
            LoadTextBoxAutoComplete(txtPictureBoxSaved31);
            LoadTextBoxAutoComplete(txtPictureBoxSaved32);

            LoadFinding();
            LoadICD9();
            LoadICD10();
            LoadProcedureDetails();
            LoadComplications();
            LoadHistopathologies();
            LoadRapidUreaseTests();
            LoadRecommendations();

            SearchHN(_hnNo, _procedureId);
        }
        private void cbbProcedureList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbProcedureList.SelectedIndex <= 0 || (int?)cbbProcedureList.SelectedValue == null)
            {
                RemoveTabPage();
                btnSave.Visible = false;
                return;
            }
            else
            {
                OpenTabPage((int)cbbProcedureList.SelectedValue);
                btnSave.Visible = true;
            }
        }
        private void SetTextBoxLastFocused(string text)
        {
            if (_markField == Constant.PREDX1 ||
                _markField == Constant.PREDX2 ||
                _markField == Constant.PRINCIPAL_PROCEDURE ||
                _markField == Constant.SUPPLEMENTAL_PROCEDURE_1 ||
                _markField == Constant.SUPPLEMENTAL_PROCEDURE_2 ||
                _markField == Constant.POSTDX1 ||
                _markField == Constant.POSTDX2 ||
                _markField == Constant.POSTDX3 ||
                _markField == Constant.POSTDX4)
            {
                lastFocused.Text = text;
            }
            else if (_markField == Constant.PROCEDURE_DETAIL ||
                _markField == Constant.COMPLICATION ||
                _markField == Constant.HISTOPATHOLOGY ||
                _markField == Constant.RAPIDUREASETEST ||
                _markField == Constant.RECOMMENDATION)
            {
                lastFocused.AppendText(text);
                lastFocused.AppendText(", ");
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1 == null || listBox1.SelectedItem == null)
            {
                return;
            }

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
                    //lastFocused.Text = subStrItem[0].Trim();
                    SetTextBoxLastFocused(subStrItem[0].Trim());
                }
            }

            listBox1.Items.Clear();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("คุณบันทึกข้อมูลเสร็จแล้ว ?", "Save form", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                if (!string.IsNullOrWhiteSpace(_hnNo) && _procedureId > 0 && _endoscopicId > 0)
                {
                    // Save
                    bool isSave = OnSave(_hnNo, _patientId, _procedureId, _endoscopicId);
                    if (isSave)
                    {
                        isSaved = true;
                        btnReport.Visible = true;
                        btnSave.Enabled = false;
                    }
                    else
                    {
                        isSaved = false;
                        btnReport.Visible = false;
                        btnSave.Enabled = true;
                    }
                }
                else
                {
                    isSaved = false;
                    btnReport.Visible = false;
                    return;
                }
            }
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_hnNo) && _procedureId > 0 && _endoscopicId > 0)
            {
                btnSave.Enabled = true;

                FormProceed.Self.txbStep.Text = "3,,";
            }
            else
            {
                return;
            }
        }
        private void btnBackForm_Click(object sender, EventArgs e)
        {
            if (!isSaved)
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to discard the data?", "Save Form", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    FormProceed.Self.txbStep.Text = "1" + "," + _pathFolderImageSave + "," + _vdoPath;
                }
                else
                {
                    return;
                }
            }
            else
            {
                FormProceed.Self.txbStep.Text = "1" + "," + _pathFolderImageSave + "," + _vdoPath;
            }
        }

        #region Search Data

        private void LoadFinding()
        {
            _findingLabels = _dropdownRepo.GetFindingLabels(_procedureId).ToList();
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
        private void LoadProcedureDetails()
        {
            _procedureDetails = _db.ProcedureDetails.Where(w => w.IsActive && w.ProcedureId == _procedureId).OrderBy(o => o.ID).ToList();
        }
        private void LoadComplications()
        {
            _complications = _db.Complications.Where(w => w.IsActive && w.ProcedureId == _procedureId).OrderBy(o => o.ID).ToList();
        }
        private void LoadHistopathologies()
        {
            _histopathologies = _db.Histopathologies.Where(w => w.IsActive && w.ProcedureId == _procedureId).OrderBy(o => o.ID).ToList();
        }
        private void LoadRapidUreaseTests()
        {
            _rapidUreaseTests = _db.RapidUreaseTests.Where(w => w.IsActive).OrderBy(o => o.ID).ToList();
        }
        private void LoadRecommendations()
        {
            _recommendations = _db.Recommendations.Where(w => w.IsActive && w.ProcedureId == _procedureId).OrderBy(o => o.ID).ToList();
        }


        #region Event Handler EGD
        private void txbGeneralDx1ID_EGD_TextChanged(object sender, EventArgs e)
        {
            if (txbGeneralDx1ID_EGD.TextLength > 0)
            {
                ICD10 preDiag1 = _iCD10s.Find(f => f.ID == Convert.ToInt32(txbGeneralDx1ID_EGD.Text));
                if (preDiag1 != null)
                {
                    txbGeneralDx1Code_EGD.Text = preDiag1.Code;
                    txbGeneralDx1Text_EGD.Text = preDiag1.Name;
                }
                else
                {
                    txbGeneralDx1Code_EGD.Clear();
                    txbGeneralDx1Text_EGD.Clear();
                }
            }
            else
            {
                txbGeneralDx1Code_EGD.Clear();
                txbGeneralDx1Text_EGD.Clear();
            }
        }
        private void txbGeneralDx2ID_EGD_TextChanged(object sender, EventArgs e)
        {
            if (txbGeneralDx2ID_EGD.TextLength > 0)
            {
                ICD10 preDiag2 = _iCD10s.Find(f => f.ID == Convert.ToInt32(txbGeneralDx2ID_EGD.Text));
                if (preDiag2 != null)
                {
                    txbGeneralDx2Code_EGD.Text = preDiag2.Code;
                    txbGeneralDx2Text_EGD.Text = preDiag2.Name;
                }
                else
                {
                    txbGeneralDx2Code_EGD.Clear();
                    txbGeneralDx2Text_EGD.Clear();
                }
            }
            else
            {
                txbGeneralDx2Code_EGD.Clear();
                txbGeneralDx2Text_EGD.Clear();
            }
        }
        private void txbGeneralDx1Code_EGD_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd10);
            lastFocused = (TextBox)sender;
            _markField = Constant.PREDX1;
        }
        private void txbGeneralDx2Code_EGD_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd10);
            lastFocused = (TextBox)sender;
            _markField = Constant.PREDX2;
        }
        private void txbFindingPrinncipalProcedureCode_EGD_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd9);
            lastFocused = (TextBox)sender;
            _markField = Constant.PRINCIPAL_PROCEDURE;
        }
        private void txbFindingSupplementalProcedureCode_EGD_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd9);
            lastFocused = (TextBox)sender;
            _markField = Constant.SUPPLEMENTAL_PROCEDURE_1;
        }
        private void txbFindingSupplementalProcedureCode2_EGD_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd9);
            lastFocused = (TextBox)sender;
            _markField = Constant.SUPPLEMENTAL_PROCEDURE_2;
        }
        private void txbFindingDx1Code_EGD_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd10);
            lastFocused = (TextBox)sender;
            _markField = Constant.POSTDX1;
        }
        private void txbFindingDx2Code_EGD_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd10);
            lastFocused = (TextBox)sender;
            _markField = Constant.POSTDX2;
        }
        private void txbFindingDx3Code_EGD_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd10);
            lastFocused = (TextBox)sender;
            _markField = Constant.POSTDX3;
        }
        private void txbFindingDx4Code_EGD_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd10);
            lastFocused = (TextBox)sender;
            _markField = Constant.POSTDX4;
        }
        private void txbGeneralDx1Code_EGD_TextChanged(object sender, EventArgs e)
        {
            if (txbGeneralDx1Code_EGD.TextLength > 0)
            {
                var icd10 = _iCD10s.FirstOrDefault(f => f.Code == txbGeneralDx1Code_EGD.Text);

                if (icd10 != null)
                {
                    txbGeneralDx1Text_EGD.Text = icd10.Name;
                    txbGeneralDx1ID_EGD.Text = icd10.ID.ToString();
                }
                else
                {
                    txbGeneralDx1Text_EGD.Clear();
                    txbGeneralDx1ID_EGD.Clear();
                }
            }
            else
            {
                txbGeneralDx1Text_EGD.Clear();
                txbGeneralDx1ID_EGD.Clear();
            }
        }
        private void txbGeneralDx2Code_EGD_TextChanged(object sender, EventArgs e)
        {
            if (txbGeneralDx2Code_EGD.TextLength > 0)
            {
                var icd10 = _iCD10s.FirstOrDefault(f => f.Code == txbGeneralDx2Code_EGD.Text);

                if (icd10 != null)
                {
                    txbGeneralDx2Text_EGD.Text = icd10.Name;
                    txbGeneralDx2ID_EGD.Text = icd10.ID.ToString();
                }
                else
                {
                    txbGeneralDx2Text_EGD.Clear();
                    txbGeneralDx2ID_EGD.Clear();
                }
            }
            else
            {
                txbGeneralDx2Text_EGD.Clear();
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
        private void txbFindingProcedure_EGD_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.ProcedureDetail);
            lastFocused = (TextBox)sender;
            _markField = Constant.PROCEDURE_DETAIL;
        }
        private void txbFindingComplication_EGD_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Complication);
            lastFocused = (TextBox)sender;
            _markField = Constant.COMPLICATION;
        }
        private void txbFindingHistopathology_EGD_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Histopathology);
            lastFocused = (TextBox)sender;
            _markField = Constant.HISTOPATHOLOGY;
        }
        private void txbFindingRapidUreaseTest_EGD_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.RapidUreaseTest);
            lastFocused = (TextBox)sender;
            _markField = Constant.RAPIDUREASETEST;
        }
        private void txbFindingRecommendation_EGD_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Recommendation);
            lastFocused = (TextBox)sender;
            _markField = Constant.RECOMMENDATION;
        }
        #endregion

        #region Event Handler Colono
        private void txbGeneralDx1ID_Colono_TextChanged(object sender, EventArgs e)
        {
            if (txbGeneralDx1ID_Colono.TextLength > 0)
            {
                ICD10 preDiag1 = _iCD10s.Find(f => f.ID == Convert.ToInt32(txbGeneralDx1ID_Colono.Text));
                if (preDiag1 != null)
                {
                    txbGeneralDx1Code_Colono.Text = preDiag1.Code;
                    txbGeneralDx1Text_Colono.Text = preDiag1.Name;
                }
                else
                {
                    txbGeneralDx1Code_Colono.Clear();
                    txbGeneralDx1Text_Colono.Clear();
                }
            }
            else
            {
                txbGeneralDx1Code_Colono.Clear();
                txbGeneralDx1Text_Colono.Clear();
            }
        }
        private void txbGeneralDx2ID_Colono_TextChanged(object sender, EventArgs e)
        {
            if (txbGeneralDx2ID_Colono.TextLength > 0)
            {
                ICD10 preDiag2 = _iCD10s.Find(f => f.ID == Convert.ToInt32(txbGeneralDx2ID_Colono.Text));
                if (preDiag2 != null)
                {
                    txbGeneralDx2Code_Colono.Text = preDiag2.Code;
                    txbGeneralDx2IText_Colono.Text = preDiag2.Name;
                }
                else
                {
                    txbGeneralDx2Code_Colono.Clear();
                    txbGeneralDx2IText_Colono.Clear();
                }
            }
            else
            {
                txbGeneralDx2Code_Colono.Clear();
                txbGeneralDx2IText_Colono.Clear();
            }
        }
        private void txbGeneralDx1Code_Colono_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd10);
            lastFocused = (TextBox)sender;
            _markField = Constant.PREDX1;
        }
        private void txbGeneralDx2Code_Colono_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd10);
            lastFocused = (TextBox)sender;
            _markField = Constant.PREDX2;
        }
        private void txbFindingPrinncipalProcedureCode_Colono_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd9);
            lastFocused = (TextBox)sender;
            _markField = Constant.PRINCIPAL_PROCEDURE;
        }
        private void txbFindingSupplementalProcedureCode_Colono_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd9);
            lastFocused = (TextBox)sender;
            _markField = Constant.SUPPLEMENTAL_PROCEDURE_1;
        }
        private void txbFindingSupplementalProcedure2Code_Colono_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd9);
            lastFocused = (TextBox)sender;
            _markField = Constant.SUPPLEMENTAL_PROCEDURE_2;
        }
        private void txbFindingDx1Code_Colono_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd10);
            lastFocused = (TextBox)sender;
            _markField = Constant.POSTDX1;
        }
        private void txbFindingDx2Code_Colono_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd10);
            lastFocused = (TextBox)sender;
            _markField = Constant.POSTDX2;
        }
        private void txbFindingDx3Code_Colono_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd10);
            lastFocused = (TextBox)sender;
            _markField = Constant.POSTDX3;
        }
        private void txbFindingDx4Code_Colono_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd10);
            lastFocused = (TextBox)sender;
            _markField = Constant.POSTDX4;
        }
        private void txbGeneralDx1Code_Colono_TextChanged(object sender, EventArgs e)
        {
            if (txbGeneralDx1Code_Colono.TextLength > 0)
            {
                var icd10 = _iCD10s.FirstOrDefault(f => f.Code == txbGeneralDx1Code_Colono.Text);

                if (icd10 != null)
                {
                    txbGeneralDx1Text_Colono.Text = icd10.Name;
                    txbGeneralDx1ID_Colono.Text = icd10.ID.ToString();
                }
                else
                {
                    txbGeneralDx1Text_Colono.Clear();
                    txbGeneralDx1ID_Colono.Clear();
                }
            }
            else
            {
                txbGeneralDx1Text_Colono.Clear();
                txbGeneralDx1ID_Colono.Clear();
            }
        }
        private void txbGeneralDx2Code_Colono_TextChanged(object sender, EventArgs e)
        {
            if (txbGeneralDx2Code_Colono.TextLength > 0)
            {
                var icd10 = _iCD10s.FirstOrDefault(f => f.Code == txbGeneralDx2Code_Colono.Text);

                if (icd10 != null)
                {
                    txbGeneralDx2IText_Colono.Text = icd10.Name;
                    txbGeneralDx2ID_Colono.Text = icd10.ID.ToString();
                }
                else
                {
                    txbGeneralDx2IText_Colono.Clear();
                    txbGeneralDx2ID_Colono.Clear();
                }
            }
            else
            {
                txbGeneralDx2IText_Colono.Clear();
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
        private void txbFindingProcedure_Colono_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.ProcedureDetail);
            lastFocused = (TextBox)sender;
            _markField = Constant.PROCEDURE_DETAIL;
        }
        private void txbFindingComplication_Colono_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Complication);
            lastFocused = (TextBox)sender;
            _markField = Constant.COMPLICATION;
        }
        private void txbFindingHistopathology_Colono_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Histopathology);
            lastFocused = (TextBox)sender;
            _markField = Constant.HISTOPATHOLOGY;
        }
        private void txbFindingRecommendation_Colono_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Recommendation);
            lastFocused = (TextBox)sender;
            _markField = Constant.RECOMMENDATION;
        }

        #endregion

        #region Event Handler ERCP

        private void txbFindingPrinncipalProcedureCode_ERCP_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd9);
            lastFocused = (TextBox)sender;
            _markField = Constant.PRINCIPAL_PROCEDURE;
        }
        private void txbFindingSupplementalProcedureCode_ERCP_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd9);
            lastFocused = (TextBox)sender;
            _markField = Constant.SUPPLEMENTAL_PROCEDURE_1;
        }
        private void txbFindingSupplementalProcedure2Code_ERCP_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd9);
            lastFocused = (TextBox)sender;
            _markField = Constant.SUPPLEMENTAL_PROCEDURE_2;
        }
        private void txbFindingDx1Code_ERCP_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd10);
            lastFocused = (TextBox)sender;
            _markField = Constant.POSTDX1;
        }
        private void txbFindingDx2Code_ERCP_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd10);
            lastFocused = (TextBox)sender;
            _markField = Constant.POSTDX2;
        }
        private void txbFindingDx3Code_ERCP_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd10);
            lastFocused = (TextBox)sender;
            _markField = Constant.POSTDX3;
        }
        private void txbFindingDx4Code_ERCP_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd10);
            lastFocused = (TextBox)sender;
            _markField = Constant.POSTDX4;
        }
        private void txbFindingPrinncipalProcedureCode_ERCP_TextChanged(object sender, EventArgs e)
        {
            if (txbFindingPrinncipalProcedureCode_ERCP.TextLength > 0)
            {
                var icd9 = _iCD9s.FirstOrDefault(f => f.Code == txbFindingPrinncipalProcedureCode_ERCP.Text);
                if (icd9 != null)
                {
                    txbFindingPrinncipalProcedureText_ERCP.Text = icd9.Name;
                    txbFindingPrinncipalProcedureID_ERCP.Text = icd9.ID.ToString();
                }
                else
                {
                    txbFindingPrinncipalProcedureText_ERCP.Clear();
                    txbFindingPrinncipalProcedureID_ERCP.Clear();
                }
            }
            else
            {
                txbFindingPrinncipalProcedureText_ERCP.Clear();
                txbFindingPrinncipalProcedureID_ERCP.Clear();
            }
        }
        private void txbFindingSupplementalProcedureCode_ERCP_TextChanged(object sender, EventArgs e)
        {
            if (txbFindingSupplementalProcedureCode_ERCP.TextLength > 0)
            {
                var icd9 = _iCD9s.FirstOrDefault(f => f.Code == txbFindingSupplementalProcedureCode_ERCP.Text);
                if (icd9 != null)
                {
                    txbFindingSupplementalProcedureText_ERCP.Text = icd9.Name;
                    txbFindingSupplementalProcedureID_ERCP.Text = icd9.ID.ToString();
                }
                else
                {
                    txbFindingSupplementalProcedureText_ERCP.Clear();
                    txbFindingSupplementalProcedureID_ERCP.Clear();
                }
            }
            else
            {
                txbFindingSupplementalProcedureText_ERCP.Clear();
                txbFindingSupplementalProcedureID_ERCP.Clear();
            }
        }
        private void txbFindingSupplementalProcedure2Code_ERCP_TextChanged(object sender, EventArgs e)
        {
            if (txbFindingSupplementalProcedure2Code_ERCP.TextLength > 0)
            {
                var icd9 = _iCD9s.FirstOrDefault(f => f.Code == txbFindingSupplementalProcedure2Code_ERCP.Text);
                if (icd9 != null)
                {
                    txbFindingSupplementalProcedure2Text_ERCP.Text = icd9.Name;
                    txbFindingSupplementalProcedure2ID_ERCP.Text = icd9.ID.ToString();
                }
                else
                {
                    txbFindingSupplementalProcedure2Text_ERCP.Clear();
                    txbFindingSupplementalProcedure2ID_ERCP.Clear();
                }
            }
            else
            {
                txbFindingSupplementalProcedure2Text_ERCP.Clear();
                txbFindingSupplementalProcedure2ID_ERCP.Clear();
            }
        }
        private void txbFindingDx1Code_ERCP_TextChanged(object sender, EventArgs e)
        {
            if (txbFindingDx1Code_ERCP.TextLength > 0)
            {
                var icd10 = _iCD10s.FirstOrDefault(f => f.Code == txbFindingDx1Code_ERCP.Text);
                if (icd10 != null)
                {
                    txbFindingDx1Text_ERCP.Text = icd10.Name;
                    txbFindingDx1ID_ERCP.Text = icd10.ID.ToString();
                }
                else
                {
                    txbFindingDx1Text_ERCP.Clear();
                    txbFindingDx1ID_ERCP.Clear();
                }
            }
            else
            {
                txbFindingDx1Text_ERCP.Clear();
                txbFindingDx1ID_ERCP.Clear();
            }
        }
        private void txbFindingDx2Code_ERCP_TextChanged(object sender, EventArgs e)
        {
            if (txbFindingDx2Code_ERCP.TextLength > 0)
            {
                var icd10 = _iCD10s.FirstOrDefault(f => f.Code == txbFindingDx2Code_ERCP.Text);
                if (icd10 != null)
                {
                    txbFindingDx2Text_ERCP.Text = icd10.Name;
                    txbFindingDx2ID_ERCP.Text = icd10.ID.ToString();
                }
                else
                {
                    txbFindingDx2Text_ERCP.Clear();
                    txbFindingDx2ID_ERCP.Clear();
                }
            }
            else
            {
                txbFindingDx2Text_ERCP.Clear();
                txbFindingDx2ID_ERCP.Clear();
            }
        }
        private void txbFindingDx3Code_ERCP_TextChanged(object sender, EventArgs e)
        {
            if (txbFindingDx3Code_ERCP.TextLength > 0)
            {
                var icd10 = _iCD10s.FirstOrDefault(f => f.Code == txbFindingDx3Code_ERCP.Text);
                if (icd10 != null)
                {
                    txbFindingDx3Text_ERCP.Text = icd10.Name;
                    txbFindingDx3ID_ERCP.Text = icd10.ID.ToString();
                }
                else
                {
                    txbFindingDx3Text_ERCP.Clear();
                    txbFindingDx3ID_ERCP.Clear();
                }
            }
            else
            {
                txbFindingDx3Text_ERCP.Clear();
                txbFindingDx3ID_ERCP.Clear();
            }
        }
        private void txbFindingProcedure_ERCP_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.ProcedureDetail);
            lastFocused = (TextBox)sender;
            _markField = Constant.PROCEDURE_DETAIL;
        }
        private void txbFindingComplication_ERCP_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Complication);
            lastFocused = (TextBox)sender;
            _markField = Constant.COMPLICATION;
        }
        private void txbFindingHistopathology_ERCP_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Histopathology);
            lastFocused = (TextBox)sender;
            _markField = Constant.HISTOPATHOLOGY;
        }
        private void txbFindingRecommendation_ERCP_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Recommendation);
            lastFocused = (TextBox)sender;
            _markField = Constant.RECOMMENDATION;
        }

        #endregion

        #region Event Handler Broncho

        private void txbFindingPrinncipalProcedureCode_Broncho_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd9);
            lastFocused = (TextBox)sender;
            _markField = Constant.PRINCIPAL_PROCEDURE;
        }
        private void txbFindingSupplementalProcedureCode_Broncho_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd9);
            lastFocused = (TextBox)sender;
            _markField = Constant.SUPPLEMENTAL_PROCEDURE_1;
        }
        private void txbFindingSupplementalProcedure2Code_Broncho_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd9);
            lastFocused = (TextBox)sender;
            _markField = Constant.SUPPLEMENTAL_PROCEDURE_2;
        }
        private void txbFindingDx1Code_Broncho_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd10);
            lastFocused = (TextBox)sender;
            _markField = Constant.POSTDX1;
        }
        private void txbFindingDx2Code_Broncho_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd10);
            lastFocused = (TextBox)sender;
            _markField = Constant.POSTDX2;
        }
        private void txbFindingDx3Code_Broncho_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd10);
            lastFocused = (TextBox)sender;
            _markField = Constant.POSTDX3;
        }
        private void txbFindingDx4Code_Broncho_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd10);
            lastFocused = (TextBox)sender;
            _markField = Constant.POSTDX4;
        }
        private void txbFindingPrinncipalProcedureCode_Broncho_TextChanged(object sender, EventArgs e)
        {
            if (txbFindingPrinncipalProcedureCode_Broncho.TextLength > 0)
            {
                var icd9 = _iCD9s.FirstOrDefault(f => f.Code == txbFindingPrinncipalProcedureCode_Broncho.Text);
                if (icd9 != null)
                {
                    txbFindingPrinncipalProcedureText_Broncho.Text = icd9.Name;
                    txbFindingPrinncipalProcedureID_Broncho.Text = icd9.ID.ToString();
                }
                else
                {
                    txbFindingPrinncipalProcedureText_Broncho.Clear();
                    txbFindingPrinncipalProcedureID_Broncho.Clear();
                }
            }
            else
            {
                txbFindingPrinncipalProcedureText_Broncho.Clear();
                txbFindingPrinncipalProcedureID_Broncho.Clear();
            }
        }
        private void txbFindingSupplementalProcedureCode_Broncho_TextChanged(object sender, EventArgs e)
        {
            if (txbFindingSupplementalProcedureCode_Broncho.TextLength > 0)
            {
                var icd9 = _iCD9s.FirstOrDefault(f => f.Code == txbFindingSupplementalProcedureCode_Broncho.Text);
                if (icd9 != null)
                {
                    txbFindingSupplementalProcedureText_Broncho.Text = icd9.Name;
                    txbFindingSupplementalProcedureID_Broncho.Text = icd9.ID.ToString();
                }
                else
                {
                    txbFindingSupplementalProcedureText_Broncho.Clear();
                    txbFindingSupplementalProcedureID_Broncho.Clear();
                }
            }
            else
            {
                txbFindingSupplementalProcedureText_Broncho.Clear();
                txbFindingSupplementalProcedureID_Broncho.Clear();
            }
        }
        private void txbFindingSupplementalProcedure2Code_Broncho_TextChanged(object sender, EventArgs e)
        {
            if (txbFindingSupplementalProcedure2Code_Broncho.TextLength > 0)
            {
                var icd9 = _iCD9s.FirstOrDefault(f => f.Code == txbFindingSupplementalProcedure2Code_Broncho.Text);
                if (icd9 != null)
                {
                    txbFindingSupplementalProcedure2Text_Broncho.Text = icd9.Name;
                    txbFindingSupplementalProcedure2ID_Broncho.Text = icd9.ID.ToString();
                }
                else
                {
                    txbFindingSupplementalProcedure2Text_Broncho.Clear();
                    txbFindingSupplementalProcedure2ID_Broncho.Clear();
                }
            }
            else
            {
                txbFindingSupplementalProcedure2Text_Broncho.Clear();
                txbFindingSupplementalProcedure2ID_Broncho.Clear();
            }
        }
        private void txbFindingDx1Code_Broncho_TextChanged(object sender, EventArgs e)
        {
            if (txbFindingDx1Code_Broncho.TextLength > 0)
            {
                var icd10 = _iCD10s.FirstOrDefault(f => f.Code == txbFindingDx1Code_Broncho.Text);
                if (icd10 != null)
                {
                    txbFindingDx1Text_Broncho.Text = icd10.Name;
                    txbFindingDx1ID_Broncho.Text = icd10.ID.ToString();
                }
                else
                {
                    txbFindingDx1Text_Broncho.Clear();
                    txbFindingDx1ID_Broncho.Clear();
                }
            }
            else
            {
                txbFindingDx1Text_Broncho.Clear();
                txbFindingDx1ID_Broncho.Clear();
            }
        }
        private void txbFindingDx2Code_Broncho_TextChanged(object sender, EventArgs e)
        {
            if (txbFindingDx2Code_Broncho.TextLength > 0)
            {
                var icd10 = _iCD10s.FirstOrDefault(f => f.Code == txbFindingDx2Code_Broncho.Text);
                if (icd10 != null)
                {
                    txbFindingDx2Text_Broncho.Text = icd10.Name;
                    txbFindingDx2ID_Broncho.Text = icd10.ID.ToString();
                }
                else
                {
                    txbFindingDx2Text_Broncho.Clear();
                    txbFindingDx2ID_Broncho.Clear();
                }
            }
            else
            {
                txbFindingDx2Text_Broncho.Clear();
                txbFindingDx2ID_Broncho.Clear();
            }
        }
        private void txbFindingDx3Code_Broncho_TextChanged(object sender, EventArgs e)
        {
            if (txbFindingDx3Code_Broncho.TextLength > 0)
            {
                var icd10 = _iCD10s.FirstOrDefault(f => f.Code == txbFindingDx3Code_Broncho.Text);
                if (icd10 != null)
                {
                    txbFindingDx3Text_Broncho.Text = icd10.Name;
                    txbFindingDx3ID_Broncho.Text = icd10.ID.ToString();
                }
                else
                {
                    txbFindingDx3Text_Broncho.Clear();
                    txbFindingDx3ID_Broncho.Clear();
                }
            }
            else
            {
                txbFindingDx3Text_Broncho.Clear();
                txbFindingDx3ID_Broncho.Clear();
            }
        }
        private void txbFindingProcedure_Broncho_Click(object sender, EventArgs e)
        {
            //SetDataInListBox(Constant.ProcedureDetail);
            //lastFocused = (TextBox)sender;
            //_markField = Constant.PROCEDURE_DETAIL;
        }

        #endregion

        #region Event Handler ENT

        private void txbFindingPrinncipalProcedureCode_ENT_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd9);
            lastFocused = (TextBox)sender;
            _markField = Constant.PRINCIPAL_PROCEDURE;
        }
        private void txbFindingSupplementalProcedureCode_ENT_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd9);
            lastFocused = (TextBox)sender;
            _markField = Constant.SUPPLEMENTAL_PROCEDURE_1;
        }
        private void txbFindingSupplementalProcedureCode2_ENT_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd9);
            lastFocused = (TextBox)sender;
            _markField = Constant.SUPPLEMENTAL_PROCEDURE_2;
        }
        private void txbFindingDx1Code_ENT_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd10);
            lastFocused = (TextBox)sender;
            _markField = Constant.POSTDX1;
        }
        private void txbFindingDx2Code_ENT_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd10);
            lastFocused = (TextBox)sender;
            _markField = Constant.POSTDX2;
        }
        private void txbFindingDx3Code_ENT_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Icd10);
            lastFocused = (TextBox)sender;
            _markField = Constant.POSTDX3;
        }
        private void txbFindingPrinncipalProcedureCode_ENT_TextChanged(object sender, EventArgs e)
        {
            if (txbFindingPrinncipalProcedureCode_Ent.TextLength > 0)
            {
                var icd9 = _iCD9s.FirstOrDefault(f => f.Code == txbFindingPrinncipalProcedureCode_Ent.Text);
                if (icd9 != null)
                {
                    txbFindingPrinncipalProcedureText_Ent.Text = icd9.Name;
                    txbFindingPrinncipalProcedureID_Ent.Text = icd9.ID.ToString();
                }
                else
                {
                    txbFindingPrinncipalProcedureText_Ent.Clear();
                    txbFindingPrinncipalProcedureID_Ent.Clear();
                }
            }
            else
            {
                txbFindingPrinncipalProcedureText_Ent.Clear();
                txbFindingPrinncipalProcedureID_Ent.Clear();
            }
        }
        private void txbFindingSupplementalProcedureCode_ENT_TextChanged(object sender, EventArgs e)
        {
            if (txbFindingSupplementalProcedureCode_Ent.TextLength > 0)
            {
                var icd9 = _iCD9s.FirstOrDefault(f => f.Code == txbFindingSupplementalProcedureCode_Ent.Text);
                if (icd9 != null)
                {
                    txbFindingSupplementalProcedureText_Ent.Text = icd9.Name;
                    txbFindingSupplementalProcedureID_Ent.Text = icd9.ID.ToString();
                }
                else
                {
                    txbFindingSupplementalProcedureText_Ent.Clear();
                    txbFindingSupplementalProcedureID_Ent.Clear();
                }
            }
            else
            {
                txbFindingSupplementalProcedureText_Ent.Clear();
                txbFindingSupplementalProcedureID_Ent.Clear();
            }
        }
        private void txbFindingSupplementalProcedureCode2_ENT_TextChanged(object sender, EventArgs e)
        {
            if (txbFindingSupplementalProcedureCode2_Ent.TextLength > 0)
            {
                var icd9 = _iCD9s.FirstOrDefault(f => f.Code == txbFindingSupplementalProcedureCode2_Ent.Text);
                if (icd9 != null)
                {
                    txbFindingSupplementalProcedureText2_Ent.Text = icd9.Name;
                    txbFindingSupplementalProcedureID2_Ent.Text = icd9.ID.ToString();
                }
                else
                {
                    txbFindingSupplementalProcedureText2_Ent.Clear();
                    txbFindingSupplementalProcedureID2_Ent.Clear();
                }
            }
            else
            {
                txbFindingSupplementalProcedureText2_Ent.Clear();
                txbFindingSupplementalProcedureID2_Ent.Clear();
            }
        }
        private void txbFindingDx1Code_ENT_TextChanged(object sender, EventArgs e)
        {
            if (txbFindingDx1Code_Ent.TextLength > 0)
            {
                var icd10 = _iCD10s.FirstOrDefault(f => f.Code == txbFindingDx1Code_Ent.Text);
                if (icd10 != null)
                {
                    txbFindingDx1Text_Ent.Text = icd10.Name;
                    txbFindingDx1ID_Ent.Text = icd10.ID.ToString();
                }
                else
                {
                    txbFindingDx1Text_Ent.Clear();
                    txbFindingDx1ID_Ent.Clear();
                }
            }
            else
            {
                txbFindingDx1Text_Ent.Clear();
                txbFindingDx1ID_Ent.Clear();
            }
        }
        private void txbFindingDx2Code_ENT_TextChanged(object sender, EventArgs e)
        {
            if (txbFindingDx2Code_Ent.TextLength > 0)
            {
                var icd10 = _iCD10s.FirstOrDefault(f => f.Code == txbFindingDx2Code_Ent.Text);
                if (icd10 != null)
                {
                    txbFindingDx2Text_Ent.Text = icd10.Name;
                    txbFindingDx2ID_Ent.Text = icd10.ID.ToString();
                }
                else
                {
                    txbFindingDx2Text_Ent.Clear();
                    txbFindingDx2ID_Ent.Clear();
                }
            }
            else
            {
                txbFindingDx2Text_Ent.Clear();
                txbFindingDx2ID_Ent.Clear();
            }
        }
        private void txbFindingDx3Code_ENT_TextChanged(object sender, EventArgs e)
        {
            if (txbFindingDx3Code_Ent.TextLength > 0)
            {
                var icd10 = _iCD10s.FirstOrDefault(f => f.Code == txbFindingDx3Code_Ent.Text);
                if (icd10 != null)
                {
                    txbFindingDx3Text_Ent.Text = icd10.Name;
                    txbFindingDx3ID_Ent.Text = icd10.ID.ToString();
                }
                else
                {
                    txbFindingDx3Text_Ent.Clear();
                    txbFindingDx3ID_Ent.Clear();
                }
            }
            else
            {
                txbFindingDx3Text_Ent.Clear();
                txbFindingDx3ID_Ent.Clear();
            }
        }
        private void txbFindingProcedure_ENT_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.ProcedureDetail);
            lastFocused = (TextBox)sender;
            _markField = Constant.PROCEDURE_DETAIL;
        }
        private void txbFindingComplication_ENT_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Complication);
            lastFocused = (TextBox)sender;
            _markField = Constant.COMPLICATION;
        }
        private void txbFindingHistopathology_ENT_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Histopathology);
            lastFocused = (TextBox)sender;
            _markField = Constant.HISTOPATHOLOGY;
        }
        private void txbFindingRecommendation_ENT_Click(object sender, EventArgs e)
        {
            SetDataInListBox(Constant.Recommendation);
            lastFocused = (TextBox)sender;
            _markField = Constant.RECOMMENDATION;
        }
        #endregion

        private Object[] GetIcd9()
        {
            Object[] ItemObject = new Object[0];

            var icd9 = _iCD9s.Where(w => w.ProcedureId == _procedureId || w.ProcedureId == null).OrderBy(o => o.ID).ToList();
            if (icd9 != null && icd9.Count() > 0)
            {
                ItemObject = new Object[icd9.Count];

                for (int i = 0; i < icd9.Count; i++)
                {
                    ItemObject[i] = icd9[i].Code + " - " + icd9[i].Name;
                }
            }

            return ItemObject;
        }
        private Object[] GetIcd10()
        {
            Object[] ItemObject = new Object[0];

            var icd10 = _iCD10s.Where(w => w.ProcedureId == _procedureId || w.ProcedureId == null).OrderBy(o => o.ID).ToList();
            if (icd10 != null && icd10.Count() > 0)
            {
                ItemObject = new Object[icd10.Count];

                for (int i = 0; i < icd10.Count; i++)
                {
                    ItemObject[i] = icd10[i].Code + " - " + icd10[i].Name;
                }
            }

            return ItemObject;
        }
        private Object[] GetProcedureDetail()
        {
            Object[] ItemObject = new Object[0];
            if (_procedureDetails != null && _procedureDetails.Count() > 0)
            {
                ItemObject = new Object[_procedureDetails.Count];
                for (int i = 0; i < _procedureDetails.Count; i++)
                {
                    ItemObject[i] = _procedureDetails[i].Name;
                }
            }
            return ItemObject;
        }
        private Object[] GetComplications()
        {
            Object[] ItemObject = new Object[0];
            if (_complications != null && _complications.Count() > 0)
            {
                ItemObject = new Object[_complications.Count];
                for (int i = 0; i < _complications.Count; i++)
                {
                    ItemObject[i] = _complications[i].Name;
                }
            }
            return ItemObject;
        }
        private Object[] GetHistopathologies()
        {
            Object[] ItemObject = new Object[0];
            if (_histopathologies != null && _histopathologies.Count() > 0)
            {
                ItemObject = new Object[_histopathologies.Count];
                for (int i = 0; i < _histopathologies.Count; i++)
                {
                    ItemObject[i] = _histopathologies[i].Name;
                }
            }
            return ItemObject;
        }
        private Object[] GetRapidUreaseTests()
        {
            Object[] ItemObject = new Object[0];
            if (_rapidUreaseTests != null && _rapidUreaseTests.Count() > 0)
            {
                ItemObject = new Object[_rapidUreaseTests.Count];
                for (int i = 0; i < _rapidUreaseTests.Count; i++)
                {
                    ItemObject[i] = _rapidUreaseTests[i].Name;
                }
            }
            return ItemObject;
        }
        private Object[] GetRecommendations()
        {
            Object[] ItemObject = new Object[0];
            if (_recommendations != null && _recommendations.Count() > 0)
            {
                ItemObject = new Object[_recommendations.Count];
                for (int i = 0; i < _recommendations.Count; i++)
                {
                    ItemObject[i] = _recommendations[i].Name;
                }
            }
            return ItemObject;
        }
        private void SetDataInListBox(int listId)
        {
            var ItemObject = new Object[0];
            listBox1.Items.Clear();
            if (listId == Constant.Icd9)
            {
                ItemObject = GetIcd9();
                listBox1.Items.AddRange(ItemObject);
            }
            else if (listId == Constant.Icd10)
            {
                ItemObject = GetIcd10();
                listBox1.Items.AddRange(ItemObject);
            }
            else if (listId == Constant.ProcedureDetail)
            {
                ItemObject = GetProcedureDetail();
                listBox1.Items.AddRange(ItemObject);
            }
            else if (listId == Constant.Complication)
            {
                ItemObject = GetComplications();
                listBox1.Items.AddRange(ItemObject);
            }
            else if (listId == Constant.Histopathology)
            {
                ItemObject = GetHistopathologies();
                listBox1.Items.AddRange(ItemObject);
            }
            else if (listId == Constant.RapidUreaseTest)
            {
                ItemObject = GetRapidUreaseTests();
                listBox1.Items.AddRange(ItemObject);
            }
            else if (listId == Constant.Recommendation)
            {
                ItemObject = GetRecommendations();
                listBox1.Items.AddRange(ItemObject);
            }
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
                                if (_endoscopicId > 0)
                                {
                                    getEndos = getEndos.Where(x => x.EndoscopicID == _endoscopicId).ToList();
                                }

                                getEndos = (from e in getEndos
                                            join p in _db.Patients on e.PatientID equals p.PatientID
                                            select e).ToList();

                                if (getEndos.Count > 0)
                                {
                                    Endoscopic getEndo = getEndos.OrderByDescending(o => o.CreateDate).FirstOrDefault();
                                    getEndo.FollowUpCase = app.IsFollowCase ?? false;
                                    getEndo.NewCase = app.IsNewCase ?? false;
                                    _endoscopicId = getEndo.EndoscopicID;
                                    Finding getFinding = _db.Findings.Where(x => x.FindingID == getEndo.FindingID).FirstOrDefault();
                                    //Indication getIndication = _db.Indications.Where(x => x.IndicationID == getEndo.IndicationID).FirstOrDefault();
                                    //Speciman getSpecimen = _db.Specimen.Where(x => x.SpecimenID == getEndo.SpecimenID).FirstOrDefault();
                                    //Intervention getIntervention = _db.Interventions.Where(x => x.InterventionID == getEndo.InterventionID).FirstOrDefault();
                                    PushEndoscopicData(_procedureId, getPatient, app, getEndo, getFinding);
                                }
                                else
                                {
                                    PushEndoscopicData(_procedureId, getPatient, app);
                                    Endoscopic endoscopic = new Endoscopic()
                                    {
                                        PatientID = _patientId,
                                        IsSaved = false,
                                        ProcedureID = _procedureId,
                                        NewCase = app.IsNewCase ?? false,
                                        FollowUpCase = app.IsFollowCase ?? false,
                                        CreateBy = _id,
                                        CreateDate = System.DateTime.Now
                                    };
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
                        }
                        btnSave.Visible = true;
                    }
                    else
                    {
                        Reset_Controller();
                    }
                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมูลผู้ป่วย");
                    Reset_Controller();
                    btnSave.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Reset_Controller();
                btnSave.Visible = false;
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
            SetPictureBox(pictureBoxSaved6, txtPictureBoxSaved6, 6);
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
            SetPictureBox(pictureBoxSaved19, txtPictureBoxSaved19, 19);
            SetPictureBox(pictureBoxSaved20, txtPictureBoxSaved20, 20);
            SetPictureBox(pictureBoxSaved21, txtPictureBoxSaved21, 21);
            SetPictureBox(pictureBoxSaved22, txtPictureBoxSaved22, 22);
            SetPictureBox(pictureBoxSaved23, txtPictureBoxSaved23, 23);
            SetPictureBox(pictureBoxSaved24, txtPictureBoxSaved24, 24);
            SetPictureBox(pictureBoxSaved25, txtPictureBoxSaved25, 25);
            SetPictureBox(pictureBoxSaved26, txtPictureBoxSaved26, 26);
            SetPictureBox(pictureBoxSaved27, txtPictureBoxSaved27, 27);
            SetPictureBox(pictureBoxSaved28, txtPictureBoxSaved28, 28);
            SetPictureBox(pictureBoxSaved29, txtPictureBoxSaved29, 29);
            SetPictureBox(pictureBoxSaved30, txtPictureBoxSaved30, 30);
            SetPictureBox(pictureBoxSaved31, txtPictureBoxSaved31, 31);
            SetPictureBox(pictureBoxSaved32, txtPictureBoxSaved32, 32);
        }
        private void SetPictureBox(PictureBox pictureBox, TextBox textBox, int num)
        {
            var list = _db.EndoscopicImages.Where(x => x.EndoscopicID == _endoscopicId && x.ProcedureID == _procedureId && (x.Seq != null && x.Seq.Value == num))
                .OrderByDescending(x => x.EndoscopicImageID).ToList();
            if (list.Count > 0)
            {
                string originalPathImage = list.FirstOrDefault()?.ImagePath;
                string comment = list.FirstOrDefault()?.ImageComment;
                pictureBox.ImageLocation = originalPathImage;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                textBox.Text = comment;
            }
        }
        private void PushEndoscopicData(
           int? procId,
           Patient patient = null,
           Appointment appointment = null,
           Endoscopic endoscopic = null,
           Finding finding = null)
        {
            patient = patient ?? new Patient();
            appointment = appointment ?? new Appointment();
            endoscopic = endoscopic ?? new Endoscopic();
            finding = finding ?? new Finding();

            if (procId == 1 || procId == 3 || procId == 5) // EGD, ERCP, ENT
            {
                // General
                txbHn_EGD.Text = patient.HN;
                txbFullName_EGD.Text = patient.Fullname;
                txbAge_EGD.Text = patient.BirthDate.HasValue ? (DateTime.Today.Year - patient.BirthDate.Value.Year).ToString() : "0";
                txbSex_EGD.Text = patient.Sex.HasValue ? patient.Sex.Value ? Constant.Male : Constant.FeMale : string.Empty;
                cbbGeneralOPD_EGD.SelectedValue = patient.OpdID ?? 0;
                chkNewCase_EGD.Checked = endoscopic.NewCase ?? false;
                chkFollowUpCase_EGD.Checked = endoscopic.FollowUpCase ?? false;
                chkOPD_EGD.Checked = patient.OpdID.HasValue && patient.OpdID.Value > 0 ? true : false;
                cbbGeneralWard_EGD.SelectedValue = patient.WardID ?? 0;
                chkWard_EGD.Checked = patient.WardID.HasValue && patient.WardID.Value > 0 ? true : false;
                cbbGeneralFinancial_EGD.SelectedValue = patient.FinancialID;
                cbbGeneralDoctor_EGD.SelectedValue = patient.DoctorID ?? 0;
                cbbGeneralAnesthesist_EGD.SelectedValue = patient.AnesthesistID ?? 0;
                cbbGeneralNurse1_EGD.SelectedValue = patient.NurseFirstID ?? 0;
                cbbGeneralNurse2_EGD.SelectedValue = patient.NurseSecondID ?? 0;
                cbbGeneralNurse3_EGD.SelectedValue = patient.NurseThirthID ?? 0;
                dpGeneralFrom_EGD.Value = endoscopic.StartRecordDate ?? DateTime.Now;
                dpGeneralTo_EGD.Value = endoscopic.EndRecordDate ?? DateTime.Now.AddMinutes(1);
                cbbInstrument_EGD.SelectedValue = appointment.Instrument1ID ?? 0;
                if (appointment.Instrument1ID > 0)
                {
                    txtGeneralSn_EGD.Text = _db.Instruments.FirstOrDefault(f => f.ID == appointment.Instrument1ID)?.SerialNumber;
                }
                cbbGeneralAnesthesia_EGD.SelectedValue = endoscopic.AnesthesiaID ?? 0;
                txbGeneralAnesthesia_EGD.Text = endoscopic.Anesthesia;
                cbbGeneralMedication_EGD.SelectedValue = endoscopic.MedicationID ?? 0;
                txbGeneralMedication_EGD.Text = endoscopic.MedicationOther;
                if (patient.IndicationID != null)
                {
                    cbbGeneralIndication_EGD.SelectedValue = patient.IndicationID;
                }
                else
                {
                    if (endoscopic.IndicationID != null)
                    {
                        cbbGeneralIndication_EGD.SelectedValue = endoscopic.IndicationID;
                    }
                    else
                    {
                        cbbGeneralIndication_EGD.SelectedIndex = 0;
                    }
                }
                txbGeneralIndication_EGD.Text = endoscopic.IndicationOther;
                if (!patient.PreDiagnosisFirstID.HasValue)
                 {
                    if (!string.IsNullOrWhiteSpace(endoscopic.DxId1Detail))
                    {
                        string[] splitTxt = endoscopic.DxId1Detail.Split('-');
                        if (splitTxt.Length > 1)
                        {
                            txbGeneralDx1Text_EGD.Text = splitTxt[1];
                        }
                    }
                }
                else
                {
                    txbGeneralDx1ID_EGD.Text = patient.PreDiagnosisFirstID.ToString();
                }
                if (!patient.PreDiagnosisSecondID.HasValue)
                {
                    if (!string.IsNullOrWhiteSpace(endoscopic.DxId2Detail))
                    {
                        string[] splitTxt = endoscopic.DxId2Detail.Split('-');
                        if (splitTxt.Length > 1)
                        {
                            txbGeneralDx2Text_EGD.Text = splitTxt[1];
                        }
                    }
                }
                else
                {
                    txbGeneralDx2ID_EGD.Text = patient.PreDiagnosisSecondID.ToString();
                }

                txbBriefHistory_EGD.Text = endoscopic.BriefHistory;

                if (procId == 1) //EGD
                {
                    // Finding
                    cbbFindingOropharynx_EGD.SelectedValue = finding.OropharynxID ?? 1;
                    cbbFindingEsophagus_EGD.SelectedValue = finding.EsophagusID ?? 1;
                    cbbFindingEGJunction_EGD.SelectedValue = finding.EGJunctionID ?? 1;
                    cbbFindingCardia_EGD.SelectedValue = finding.CardiaID ?? 1;
                    cbbFindingFundus_EGD.SelectedValue = finding.FundusID ?? 1;
                    cbbFindingBody_EGD.SelectedValue = finding.BodyID ?? 1;
                    cbbFindingAntrum_EGD.SelectedValue = finding.AntrumID ?? 1;
                    cbbFindingPylorus_EGD.SelectedValue = finding.PylorusID ?? 1;
                    cbbFindingDuodenalBulb_EGD.SelectedValue = finding.DuodenalBulbID ?? 1;
                    cbbFinding2ndPart_EGD.SelectedValue = finding.SecondPartID ?? 1;
                    txbFindingOropharynx_EGD.Text = finding.Oropharynx;
                    txbFindingEsophagus_EGD.Text = finding.Esophagus;
                    txbFindingEGJunction_EGD.Text = finding.EGJunction;
                    txbFindingCardia_EGD.Text = finding.Cardia;
                    txbFindingFundus_EGD.Text = finding.Fundus;
                    txbFindingBody_EGD.Text = finding.Body;
                    txbFindingAntrum_EGD.Text = finding.Antrum;
                    txbFindingPylorus_EGD.Text = finding.Pylorus;
                    txbFindingDuodenalBulb_EGD.Text = finding.DuodenalBulb;
                    txbFinding2ndPart_EGD.Text = finding.SecondPart;
                    txbFindingPrinncipalProcedureID_EGD.Text = Convert.ToString(finding.PrincipalProcedureID ?? 0);
                    string[] principalProcedureSplit = string.IsNullOrWhiteSpace(finding.PrincipalProcedureDetail) ? null : finding.PrincipalProcedureDetail.Split('-');
                    txbFindingPrinncipalProcedureCode_EGD.Text = principalProcedureSplit == null ? string.Empty : principalProcedureSplit[0];
                    txbFindingPrinncipalProcedureText_EGD.Text = principalProcedureSplit == null ? string.Empty : principalProcedureSplit[1];
                    txbFindingSupplementalProcedureID_EGD.Text = Convert.ToString(finding.SupplementalProcedure1ID ?? 0);
                    string[] SupplementalProcedureSplit = string.IsNullOrWhiteSpace(finding.SupplementalProcedure1Detail) ? null : finding.SupplementalProcedure1Detail.Split('-');
                    txbFindingSupplementalProcedureCode_EGD.Text = SupplementalProcedureSplit == null ? string.Empty : SupplementalProcedureSplit[0];
                    txbFindingSupplementalProcedureText_EGD.Text = SupplementalProcedureSplit == null ? string.Empty : SupplementalProcedureSplit[1];
                    string[] SupplementalProcedure2Split = string.IsNullOrWhiteSpace(finding.SupplementalProcedure2Detail) ? null : finding.SupplementalProcedure2Detail.Split('-');
                    txbFindingSupplementalProcedureCode2_EGD.Text = SupplementalProcedure2Split == null ? string.Empty : SupplementalProcedure2Split[0];
                    txbFindingSupplementalProcedureText2_EGD.Text = SupplementalProcedure2Split == null ? string.Empty : SupplementalProcedure2Split[1];
                    txbFindingProcedure_EGD.Text = finding.Procedure;
                    txbFindingDx1ID_EGD.Text = Convert.ToString(finding.DxID1 ?? 0);
                    string[] dx1EGDSplit = string.IsNullOrWhiteSpace(finding.DxID1Detail) ? null : finding.DxID1Detail.Split('-');
                    txbFindingDx1Code_EGD.Text = dx1EGDSplit == null ? string.Empty : dx1EGDSplit[0];
                    txbFindingDx1Text_EGD.Text = dx1EGDSplit == null ? string.Empty : dx1EGDSplit[1];
                    txbFindingDx2ID_EGD.Text = Convert.ToString(finding.DxID2 ?? 0);
                    string[] dx2EGDSplit = string.IsNullOrWhiteSpace(finding.DxID2Detail) ? null : finding.DxID2Detail.Split('-');
                    txbFindingDx2Code_EGD.Text = dx2EGDSplit == null ? string.Empty : dx2EGDSplit[0];
                    txbFindingDx2Text_EGD.Text = dx2EGDSplit == null ? string.Empty : dx2EGDSplit[1];
                    txbFindingDx3ID_EGD.Text = Convert.ToString(finding.DxID3 ?? 0);
                    string[] dx3EGDSplit = string.IsNullOrWhiteSpace(finding.DxID3Detail) ? null : finding.DxID3Detail.Split('-');
                    txbFindingDx3Code_EGD.Text = dx3EGDSplit == null ? string.Empty : dx3EGDSplit[0];
                    txbFindingDx3Text_EGD.Text = dx3EGDSplit == null ? string.Empty : dx3EGDSplit[1];
                    txbFindingComplication_EGD.Text = finding.Complication;
                    txbFindingHistopathology_EGD.Text = finding.Histopathology;
                    txbFindingRapidUreaseTest_EGD.Text = finding.RapidUreaseTest;
                    txbFindingRecommendation_EGD.Text = finding.Recommendation;
                    txbFindingComment_EGD.Text = finding.Comment;
                }
                else if (procId == 3) // ERCP
                {
                    // Finding
                    cbbFindingDuodenum_ERCP.SelectedValue = finding.DuodenumID ?? 1;
                    cbbFindingPapillaMajor_ERCP.SelectedValue = finding.PapillaMajorID ?? 1;
                    cbbFindingPapillaMinor_ERCP.SelectedValue = finding.PapillaMinorID ?? 1;
                    cbbFindingPancreas_ERCP.SelectedValue = finding.PancreasID ?? 1;
                    cbbFindingBiliarySystem_ERCP.SelectedValue = finding.BiliarySystemID ?? 1;
                    cbbFindingOther_ERCP.SelectedValue = finding.OtherID ?? 1;
                    //cbbFindingCholangiogram_ERCP.SelectedValue = finding.CholangiogramID ?? 1;
                    //cbbFindingPancreatogram_ERCP.SelectedValue = finding.PancreatogramID ?? 1;
                    txbFindingDuodenum_ERCP.Text = finding.Duodenum;
                    txbFindingPapillaMajor_ERCP.Text = finding.PapillaMajor;
                    txbFindingPapillaMinor_ERCP.Text = finding.PapillaMinor;
                    txbFindingPancreas_ERCP.Text = finding.Pancreas;
                    txbFindingBiliarySystem_ERCP.Text = finding.BiliarySystem;
                    txbFindingOther_ERCP.Text = finding.OtherDetail;
                    txbFindingCholangiogram_ERCP.Text = finding.Cholangiogram;
                    txbFindingPancreatogram_ERCP.Text = finding.Pancreatogram;
                    txbFindingPrinncipalProcedureID_ERCP.Text = Convert.ToString(finding.PrincipalProcedureID ?? 0);
                    string[] principalProcedureSplit = string.IsNullOrWhiteSpace(finding.PrincipalProcedureDetail) ? null : finding.PrincipalProcedureDetail.Split('-');
                    txbFindingPrinncipalProcedureCode_ERCP.Text = principalProcedureSplit == null ? string.Empty : principalProcedureSplit[0];
                    txbFindingPrinncipalProcedureText_ERCP.Text = principalProcedureSplit == null ? string.Empty : principalProcedureSplit[1];
                    txbFindingSupplementalProcedureID_ERCP.Text = Convert.ToString(finding.SupplementalProcedure1ID ?? 0);
                    string[] SupplementalProcedureSplit = string.IsNullOrWhiteSpace(finding.SupplementalProcedure1Detail) ? null : finding.SupplementalProcedure1Detail.Split('-');
                    txbFindingSupplementalProcedureCode_ERCP.Text = SupplementalProcedureSplit == null ? string.Empty : SupplementalProcedureSplit[0];
                    txbFindingSupplementalProcedureText_ERCP.Text = SupplementalProcedureSplit == null ? string.Empty : SupplementalProcedureSplit[1];
                    string[] SupplementalProcedure2Split = string.IsNullOrWhiteSpace(finding.SupplementalProcedure2Detail) ? null : finding.SupplementalProcedure2Detail.Split('-');
                    txbFindingSupplementalProcedure2Code_ERCP.Text = SupplementalProcedure2Split == null ? string.Empty : SupplementalProcedure2Split[0];
                    txbFindingSupplementalProcedure2Text_ERCP.Text = SupplementalProcedure2Split == null ? string.Empty : SupplementalProcedure2Split[1];
                    txbFindingProcedure_ERCP.Text = finding.Procedure;
                    txbFindingDx1ID_ERCP.Text = Convert.ToString(finding.DxID1 ?? 0);
                    string[] dx1ERCPSplit = string.IsNullOrWhiteSpace(finding.DxID1Detail) ? null : finding.DxID1Detail.Split('-');
                    txbFindingDx1Code_ERCP.Text = dx1ERCPSplit == null ? string.Empty : dx1ERCPSplit[0];
                    txbFindingDx1Text_ERCP.Text = dx1ERCPSplit == null ? string.Empty : dx1ERCPSplit[1];
                    txbFindingDx2ID_ERCP.Text = Convert.ToString(finding.DxID2 ?? 0);
                    string[] dx2ERCPSplit = string.IsNullOrWhiteSpace(finding.DxID2Detail) ? null : finding.DxID2Detail.Split('-');
                    txbFindingDx2Code_ERCP.Text = dx2ERCPSplit == null ? string.Empty : dx2ERCPSplit[0];
                    txbFindingDx2Text_ERCP.Text = dx2ERCPSplit == null ? string.Empty : dx2ERCPSplit[1];
                    txbFindingDx3ID_ERCP.Text = Convert.ToString(finding.DxID3 ?? 0);
                    string[] dx3ERCPSplit = string.IsNullOrWhiteSpace(finding.DxID3Detail) ? null : finding.DxID3Detail.Split('-');
                    txbFindingDx3Code_ERCP.Text = dx3ERCPSplit == null ? string.Empty : dx3ERCPSplit[0];
                    txbFindingDx3Text_ERCP.Text = dx3ERCPSplit == null ? string.Empty : dx3ERCPSplit[1];
                    txbFindingComplication_ERCP.Text = finding.Complication;
                    txbFindingHistopathology_ERCP.Text = finding.Histopathology;
                    txbFindingRecommendation_ERCP.Text = finding.Recommendation;
                    txbFindingComment_ERCP.Text = finding.Comment;
                }
                else if(procId == 5)
                {
                    cbbFindingNose_ENT.SelectedValue = finding.NoseID ?? 1;
                    txbFindingNose_Ent.Text = finding.Nose;
                    cbbFindingNasopharynx_Ent.SelectedValue = finding.NasopharynxID ?? 1;
                    txbFindingNasopharynx_Ent.Text = finding.Nasopharynx;
                    cbbFindingBaseOfTongue_Ent.SelectedValue = finding.BaseOfTongueID ?? 1;
                    txbFindingBaseOfTongue_Ent.Text = finding.BaseOfTongue;
                    cbbFindingSupraglottic_Ent.SelectedValue = finding.SupraglotticID ?? 1;
                    txbFindingSupraglottic_Ent.Text = finding.Supraglottic;
                    cbbFindingPyriform_Ent.SelectedValue = finding.PyriformID ?? 1;
                    txbFindingPyriform_Ent.Text = finding.Pyriform;
                    cbbFindingEsophagus_Ent.SelectedValue = finding.EsophagusID ?? 1;
                    txbFindingEsophagus_Ent.Text = finding.Esophagus;
                    cbbFindingStomach_Ent.SelectedValue = finding.StomachID ?? 1;
                    txbFindingStomach_Ent.Text = finding.Stomach;
                    txbFindingPrinncipalProcedureID_Ent.Text = Convert.ToString(finding.PrincipalProcedureID ?? 0);
                    string[] principalProcedureSplit = string.IsNullOrWhiteSpace(finding.PrincipalProcedureDetail) ? null : finding.PrincipalProcedureDetail.Split('-');
                    txbFindingPrinncipalProcedureCode_Ent.Text = principalProcedureSplit == null ? string.Empty : principalProcedureSplit[0];
                    txbFindingPrinncipalProcedureText_Ent.Text = principalProcedureSplit == null ? string.Empty : principalProcedureSplit[1];
                    txbFindingSupplementalProcedureID_Ent.Text = Convert.ToString(finding.SupplementalProcedure1ID ?? 0);
                    string[] SupplementalProcedureSplit = string.IsNullOrWhiteSpace(finding.SupplementalProcedure1Detail) ? null : finding.SupplementalProcedure1Detail.Split('-');
                    txbFindingSupplementalProcedureCode_Ent.Text = SupplementalProcedureSplit == null ? string.Empty : SupplementalProcedureSplit[0];
                    txbFindingSupplementalProcedureText_Ent.Text = SupplementalProcedureSplit == null ? string.Empty : SupplementalProcedureSplit[1];
                    string[] SupplementalProcedure2Split = string.IsNullOrWhiteSpace(finding.SupplementalProcedure2Detail) ? null : finding.SupplementalProcedure2Detail.Split('-');
                    txbFindingSupplementalProcedureCode2_Ent.Text = SupplementalProcedure2Split == null ? string.Empty : SupplementalProcedure2Split[0];
                    txbFindingSupplementalProcedureText2_Ent.Text = SupplementalProcedure2Split == null ? string.Empty : SupplementalProcedure2Split[1];
                    txbFindingProcedure_Ent.Text = finding.Procedure;
                    txbFindingDx1ID_Ent.Text = Convert.ToString(finding.DxID1 ?? 0);
                    string[] dx1EGDSplit = string.IsNullOrWhiteSpace(finding.DxID1Detail) ? null : finding.DxID1Detail.Split('-');
                    txbFindingDx1Code_Ent.Text = dx1EGDSplit == null ? string.Empty : dx1EGDSplit[0];
                    txbFindingDx1Text_Ent.Text = dx1EGDSplit == null ? string.Empty : dx1EGDSplit[1];
                    txbFindingDx2ID_Ent.Text = Convert.ToString(finding.DxID2 ?? 0);
                    string[] dx2EGDSplit = string.IsNullOrWhiteSpace(finding.DxID2Detail) ? null : finding.DxID2Detail.Split('-');
                    txbFindingDx2Code_Ent.Text = dx2EGDSplit == null ? string.Empty : dx2EGDSplit[0];
                    txbFindingDx2Text_Ent.Text = dx2EGDSplit == null ? string.Empty : dx2EGDSplit[1];
                    txbFindingDx3ID_Ent.Text = Convert.ToString(finding.DxID3 ?? 0);
                    string[] dx3EGDSplit = string.IsNullOrWhiteSpace(finding.DxID3Detail) ? null : finding.DxID3Detail.Split('-');
                    txbFindingDx3Code_Ent.Text = dx3EGDSplit == null ? string.Empty : dx3EGDSplit[0];
                    txbFindingDx3Text_Ent.Text = dx3EGDSplit == null ? string.Empty : dx3EGDSplit[1];
                    txbFindingComplication_Ent.Text = finding.Complication;
                    txbFindingHistopathology_Ent.Text = finding.Histopathology;
                    txbFindingRecommendation_Ent.Text = finding.Recommendation;
                    txbFindingComment_Ent.Text = finding.Comment;
                }
            }
            else if (procId == 2 || procId == 4) // Colono
            {
                // General
                txbHN_Colono.Text = patient.HN;
                txbFullName_Colono.Text = patient.Fullname;
                txbAge_Colono.Text = patient.Age.HasValue ? patient.Age.ToString() : "";
                txbSex_Colono.Text = patient.Sex.HasValue ? patient.Sex.Value ? Constant.Male : Constant.FeMale : string.Empty;
                cbbGeneralOPD_Colono.SelectedValue = patient.OpdID ?? 0;
                chkNewCase_Colono.Checked = endoscopic.NewCase ?? false;
                chkFollowUpCase_Colono.Checked = endoscopic.FollowUpCase ?? false;
                chkOPD_Colono.Checked = patient.OpdID.HasValue && patient.OpdID.Value > 0 ? true : false;
                cbbGeneralWard_Colono.SelectedValue = patient.WardID ?? 0;
                chkWard_Colono.Checked = patient.WardID.HasValue && patient.WardID.Value > 0 ? true : false;
                cbbGeneralFinancial_Colono.SelectedValue = patient.FinancialID;
                cbbGeneralDoctor_Colono.SelectedValue = patient.DoctorID ?? 0;
                cbbGeneralAnesthesist_Colono.SelectedValue = patient.AnesthesistID ?? 0;
                cbbGeneralNurse1_Colono.SelectedValue = patient.NurseFirstID ?? 0;
                cbbGeneralNurse2_Colono.SelectedValue = patient.NurseSecondID ?? 0;
                cbbGeneralNurse3_Colono.SelectedValue = patient.NurseThirthID ?? 0;
                dpGeneralFrom_Colono.Value = endoscopic.StartRecordDate ?? DateTime.Now;
                dpGeneralTo_Colono.Value = endoscopic.EndRecordDate ?? DateTime.Now.AddMinutes(1);
                cbbInstrument_Colono.SelectedValue = appointment.Instrument1ID ?? 0;
                if (appointment.Instrument1ID > 0)
                {
                    txbGeneralSN_Colono.Text = _db.Instruments.FirstOrDefault(f => f.ID == appointment.Instrument1ID)?.SerialNumber;
                }
                cbbGeneralAnesthesia_Colono.SelectedValue = endoscopic.AnesthesiaID ?? 0;
                txbGeneralAnesthesia_Colono.Text = endoscopic.Anesthesia;
                cbbGeneralMedication_Colono.SelectedValue = endoscopic.MedicationID ?? 0;
                txbGeneralMedication_Colono.Text = endoscopic.MedicationOther;
                if (patient.IndicationID != null)
                {
                    cbbGeneralIndication_Colono.SelectedValue = patient.IndicationID;
                }
                else
                {
                    if (endoscopic.IndicationID != null)
                    {
                        cbbGeneralIndication_Colono.SelectedValue = endoscopic.IndicationID;
                        txbGeneralIndication_Colono.Text = endoscopic.IndicationOther;
                    }
                    else
                    {
                        cbbGeneralIndication_Colono.SelectedValue = 0;
                    }
                }
                if (!patient.PreDiagnosisFirstID.HasValue)
                {
                    if (!string.IsNullOrWhiteSpace(endoscopic.DxId1Detail))
                    {
                        string[] splitTxt = endoscopic.DxId1Detail.Split('-');
                        if (splitTxt.Length > 1)
                        {
                            txbGeneralDx1Text_Colono.Text = splitTxt[1];
                        }
                    }
                }
                else
                {
                    txbGeneralDx1ID_Colono.Text = patient.PreDiagnosisFirstID.ToString();
                }
                if (!patient.PreDiagnosisSecondID.HasValue)
                {
                    if (!string.IsNullOrWhiteSpace(endoscopic.DxId2Detail))
                    {
                        string[] splitTxt = endoscopic.DxId2Detail.Split('-');
                        if (splitTxt.Length > 1)
                        {
                            txbGeneralDx2IText_Colono.Text = splitTxt[1];
                        }
                    }
                }
                else
                {
                    txbGeneralDx2ID_Colono.Text = patient.PreDiagnosisSecondID.ToString();
                }
                txbBriefHistory_Colono.Text = endoscopic.BriefHistory;
                txbGeneralBowelPreparationRegimen_Colono.Text = endoscopic.BowelPreparationRegimen;
                txbGeneralBowelPreparationResult_colono.Text = endoscopic.BowelPreparationResult;

                // Finding
                if (procId == 2)
                {
                    cbbFindingAnalCanal_Colono.SelectedValue = finding.AnalCanalID ?? 1;
                    cbbFindingRectum_Colono.SelectedValue = finding.RectumID ?? 1;
                    cbbFindingSigmoid_Colono.SelectedValue = finding.SigmoidColonID ?? 1;
                    cbbFindingDescending_Colono.SelectedValue = finding.DescendingColonID ?? 1;
                    cbbFindingFlexure_Colono.SelectedValue = finding.SplenicFlexureID ?? 1;
                    cbbFindingHepatic_Colono.SelectedValue = finding.HepaticFlexureID ?? 1;
                    cbbFindingAscending_Colono.SelectedValue = finding.AscendingColonID ?? 1;
                    cbbFindingIleocecal_Colono.SelectedValue = finding.IleocecalVolveID ?? 1;
                    cbbFindingTerminal_Colono.SelectedValue = finding.TerminalIleumID ?? 1;
                    cbbFindingCecum_Colono.SelectedValue = finding.CecumID ?? 1;
                    cbbFindingTransverse_Colono.SelectedValue = finding.TransverseColonID ?? 1;
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
                    txbFindingPrinncipalProcedureID_Colono.Text = Convert.ToString(finding.PrincipalProcedureID ?? 0);
                    string[] principalProcedureSplit = string.IsNullOrWhiteSpace(finding.PrincipalProcedureDetail) ? null : finding.PrincipalProcedureDetail.Split('-');
                    txbFindingPrinncipalProcedureCode_Colono.Text = principalProcedureSplit == null ? string.Empty : principalProcedureSplit[0];
                    txbFindingPrinncipalProcedureText_Colono.Text = principalProcedureSplit == null ? string.Empty : principalProcedureSplit[1];
                    txbFindingSupplementalProcedureID_Colono.Text = Convert.ToString(finding.SupplementalProcedure1ID ?? 0);
                    string[] SupplementalProcedureSplit = string.IsNullOrWhiteSpace(finding.SupplementalProcedure1Detail) ? null : finding.SupplementalProcedure1Detail.Split('-');
                    txbFindingSupplementalProcedureCode_Colono.Text = SupplementalProcedureSplit == null ? string.Empty : SupplementalProcedureSplit[0];
                    txbFindingSupplementalProcedureText_Colono.Text = SupplementalProcedureSplit == null ? string.Empty : SupplementalProcedureSplit[1];
                    string[] SupplementalProcedure2Split = string.IsNullOrWhiteSpace(finding.SupplementalProcedure2Detail) ? null : finding.SupplementalProcedure2Detail.Split('-');
                    txbFindingSupplementalProcedure2Code_Colono.Text = SupplementalProcedure2Split == null ? string.Empty : SupplementalProcedure2Split[0];
                    txbFindingSupplementalProcedure2Text_Colono.Text = SupplementalProcedure2Split == null ? string.Empty : SupplementalProcedure2Split[1];
                    txbFindingProcedure_Colono.Text = finding.Procedure;
                    txbFindingDx1ID_Colono.Text = Convert.ToString(finding.DxID1 ?? 0);
                    string[] dx1EGDSplit = string.IsNullOrWhiteSpace(finding.DxID1Detail) ? null : finding.DxID1Detail.Split('-');
                    txbFindingDx1Code_Colono.Text = dx1EGDSplit == null ? string.Empty : dx1EGDSplit[0];
                    txbFindingDx1Text_Colono.Text = dx1EGDSplit == null ? string.Empty : dx1EGDSplit[1];
                    txbFindingDx2ID_Colono.Text = Convert.ToString(finding.DxID2 ?? 0);
                    string[] dx2EGDSplit = string.IsNullOrWhiteSpace(finding.DxID2Detail) ? null : finding.DxID2Detail.Split('-');
                    txbFindingDx2Code_Colono.Text = dx2EGDSplit == null ? string.Empty : dx2EGDSplit[0];
                    txbFindingDx2Text_Colono.Text = dx2EGDSplit == null ? string.Empty : dx2EGDSplit[1];
                    txbFindingDx3ID_Colono.Text = Convert.ToString(finding.DxID3 ?? 0);
                    string[] dx3EGDSplit = string.IsNullOrWhiteSpace(finding.DxID3Detail) ? null : finding.DxID3Detail.Split('-');
                    txbFindingDx3Code_Colono.Text = dx3EGDSplit == null ? string.Empty : dx3EGDSplit[0];
                    txbFindingDx3Text_Colono.Text = dx3EGDSplit == null ? string.Empty : dx3EGDSplit[1];
                    txbFindingComplication_Colono.Text = finding.Complication;
                    txbFindingHistopathology_Colono.Text = finding.Histopathology;
                    txbFindingRecommendation_Colono.Text = finding.Recommendation;
                    txbFindingComment_Colono.Text = finding.Comment;
                }
                else
                {
                    // Finding
                    cbbFindingVocal_Broncho.SelectedValue = finding.VocalCordID ?? 1;
                    cbbFindingTrachea_Broncho.SelectedValue = finding.TracheaID ?? 1;
                    cbbFindingCarina_Broncho.SelectedValue = finding.CarinaID ?? 1;
                    cbbFindingRightMain_Broncho.SelectedValue = finding.RightMainID ?? 1;
                    cbbFindingRightIntermideate_Broncho.SelectedValue = finding.RightIntermideateID ?? 1;
                    cbbFindingRUL_Broncho.SelectedValue = finding.RULID ?? 1;
                    cbbFindingRML_Broncho.SelectedValue = finding.RMLID ?? 1;
                    cbbFindingRLL_Broncho.SelectedValue = finding.RLLID ?? 1;
                    cbbFindingLeftMain_Broncho.SelectedValue = finding.LeftMainID ?? 1;
                    cbbFindingLUL_Broncho.SelectedValue = finding.LULID ?? 1;
                    cbbFindingLingular_Broncho.SelectedValue = finding.LingularID ?? 1;
                    cbbFindingLLL_Broncho.SelectedValue = finding.LLLID ?? 1;
                    txbFindingVocalCord_Broncho.Text = finding.VocalCord;
                    txbFindingTrachea_Broncho.Text = finding.Trachea;
                    txbFindingCarina_Broncho.Text = finding.Carina;
                    txbFindingRightMain_Broncho.Text = finding.RightMain;
                    txbFindingIntermideate_Broncho.Text = finding.RightIntermideate;
                    txbFindingRUL_Broncho.Text = finding.RUL;
                    txbFindingRML_Broncho.Text = finding.RML;
                    txbFindingRLL_Broncho.Text = finding.RLL;
                    txbFindingLeftMain_Broncho.Text = finding.LeftMain;
                    txbFindingLUL_Broncho.Text = finding.LUL;
                    txbFindingLingular_Broncho.Text = finding.Lingular;
                    txbFindingLLL_Broncho.Text = finding.LLL;
                    txbFindingPrinncipalProcedureID_Broncho.Text = Convert.ToString(finding.PrincipalProcedureID ?? 0);
                    string[] principalProcedureSplit = string.IsNullOrWhiteSpace(finding.PrincipalProcedureDetail) ? null : finding.PrincipalProcedureDetail.Split('-');
                    txbFindingPrinncipalProcedureCode_Broncho.Text = principalProcedureSplit == null ? string.Empty : principalProcedureSplit[0];
                    txbFindingPrinncipalProcedureText_Broncho.Text = principalProcedureSplit == null ? string.Empty : principalProcedureSplit[1];
                    txbFindingSupplementalProcedureID_Broncho.Text = Convert.ToString(finding.SupplementalProcedure1ID ?? 0);
                    string[] SupplementalProcedureSplit = string.IsNullOrWhiteSpace(finding.SupplementalProcedure1Detail) ? null : finding.SupplementalProcedure1Detail.Split('-');
                    txbFindingSupplementalProcedureCode_Broncho.Text = SupplementalProcedureSplit == null ? string.Empty : SupplementalProcedureSplit[0];
                    txbFindingSupplementalProcedureText_Broncho.Text = SupplementalProcedureSplit == null ? string.Empty : SupplementalProcedureSplit[1];
                    string[] SupplementalProcedure2Split = string.IsNullOrWhiteSpace(finding.SupplementalProcedure2Detail) ? null : finding.SupplementalProcedure2Detail.Split('-');
                    txbFindingSupplementalProcedure2Code_Broncho.Text = SupplementalProcedure2Split == null ? string.Empty : SupplementalProcedure2Split[0];
                    txbFindingSupplementalProcedure2Text_Broncho.Text = SupplementalProcedure2Split == null ? string.Empty : SupplementalProcedure2Split[1];
                    txbFindingProcedure_Broncho.Text = finding.Procedure;
                    txbFindingDx1ID_Broncho.Text = Convert.ToString(finding.DxID1 ?? 0);
                    string[] dx1EGDSplit = string.IsNullOrWhiteSpace(finding.DxID1Detail) ? null : finding.DxID1Detail.Split('-');
                    txbFindingDx1Code_Broncho.Text = dx1EGDSplit == null ? string.Empty : dx1EGDSplit[0];
                    txbFindingDx1Text_Broncho.Text = dx1EGDSplit == null ? string.Empty : dx1EGDSplit[1];
                    txbFindingDx2ID_Broncho.Text = Convert.ToString(finding.DxID2 ?? 0);
                    string[] dx2EGDSplit = string.IsNullOrWhiteSpace(finding.DxID2Detail) ? null : finding.DxID2Detail.Split('-');
                    txbFindingDx2Code_Broncho.Text = dx2EGDSplit == null ? string.Empty : dx2EGDSplit[0];
                    txbFindingDx2Text_Broncho.Text = dx2EGDSplit == null ? string.Empty : dx2EGDSplit[1];
                    txbFindingDx3ID_Broncho.Text = Convert.ToString(finding.DxID3 ?? 0);
                    string[] dx3EGDSplit = string.IsNullOrWhiteSpace(finding.DxID3Detail) ? null : finding.DxID3Detail.Split('-');
                    txbFindingDx3Code_Broncho.Text = dx3EGDSplit == null ? string.Empty : dx3EGDSplit[0];
                    txbFindingDx3Text_Broncho.Text = dx3EGDSplit == null ? string.Empty : dx3EGDSplit[1];
                    txbFindingComplication_Broncho.Text = finding.Complication;
                    txbFindingHistopathology_Broncho.Text = finding.Histopathology;
                    txbFindingRecommendation_Broncho.Text = finding.Recommendation;
                    txbFindingComment_Broncho.Text = finding.Comment;
                }
            }
            else if (procId == 8)
            {
                // General
                txbHn_Lap.Text = patient.HN;
                txbFullName_Lap.Text = patient.Fullname;
                txbAge_Lap.Text = patient.Age.HasValue ? patient.Age.ToString() : "";
                txbSex_Lap.Text = patient.Sex.HasValue ? patient.Sex.Value ? Constant.Male : Constant.FeMale : string.Empty;
                chkNewCase_Lap.Checked = endoscopic.NewCase ?? false;
                chkFollowUpCase_Lap.Checked = endoscopic.FollowUpCase ?? false;
                cbbGeneralOPD_Lap.SelectedValue = patient.OpdID ?? 0;
                chkOPD_Lap.Checked = patient.OpdID.HasValue && patient.OpdID.Value > 0 ? true : false;
                cbbGeneralWard_Lap.SelectedValue = patient.WardID ?? 0;
                chkWard_Lap.Checked = patient.WardID.HasValue && patient.WardID.Value > 0 ? true : false;
                cbbGeneralFinancial_Lap.SelectedValue = patient.FinancialID;
                cbbGeneralDoctor_Lap.SelectedValue = patient.DoctorID ?? 0;
                cbbGeneralAnesthesist_Lap.SelectedValue = patient.AnesthesistID ?? 0;
                cbbGeneralNurse1_Lap.SelectedValue = patient.NurseFirstID ?? 0;
                cbbGeneralNurse2_Lap.SelectedValue = patient.NurseSecondID ?? 0;
                cbbGeneralNurse3_Lap.SelectedValue = patient.NurseThirthID ?? 0;
                dpGeneralFrom_Lap.Value = endoscopic.StartRecordDate ?? DateTime.Now;
                dpGeneralTo_Lap.Value = endoscopic.EndRecordDate ?? DateTime.Now.AddMinutes(1);
                cbbGeneralInstrument_Lap.SelectedValue = appointment.Instrument1ID ?? 0;
                if (appointment.Instrument1ID > 0)
                {
                    txbGeneralSN_Lap.Text = _db.Instruments.FirstOrDefault(f => f.ID == appointment.Instrument1ID)?.SerialNumber;
                }
                cbbGeneralAnesthesia_Lap.SelectedValue = endoscopic.AnesthesiaID ?? 0;
                txbGeneralAnesthesia_Lap.Text = endoscopic.Anesthesia;
                cbbGeneralMedication_Lap.SelectedValue = endoscopic.MedicationID ?? 0;
                txbGeneralMedication_Lap.Text = endoscopic.MedicationOther;
                if (patient.IndicationID != null)
                {
                    cbbGeneralIndication_Lap.SelectedValue = patient.IndicationID;
                }
                else
                {
                    if (endoscopic.IndicationID != null)
                    {
                        cbbGeneralIndication_Lap.SelectedValue = endoscopic.IndicationID;
                    }
                    else
                    {
                        cbbGeneralIndication_Lap.SelectedValue = 0;
                    }
                }
                txbGeneralBriefHistory_Lap.Text = endoscopic.BriefHistory;

                // Finding
                txbFindingDiagnosis_Lap.Text = endoscopic.Diagnosis;
                txbFindingOperative_Lap.Text = endoscopic.GeneralOperativeFinding;
                txbFindingComment_Lap.Text = endoscopic.Comment;
            }

            //if (endoscopic.StartRecordDate.HasValue) recordStartDate = endoscopic.StartRecordDate.Value;
            //if (endoscopic.EndRecordDate.HasValue) recordEndDate = endoscopic.EndRecordDate.Value;
            PushEndoscopicImage();
        }
        private Patient UpdatePatientInfo(int patientId, int procedureId)
        {
            var patient = _db.Patients.Where(x => x.PatientID == patientId).FirstOrDefault();
            if (patient != null)
            {
                patient.ProcedureID = procedureId;
                patient.UpdateBy = _id;
                patient.UpdateDate = DateTime.Now;
                if (procedureId == 1 || procedureId == 3)
                {
                    patient.Fullname = txbFullName_EGD.Text;
                    patient.OpdID = (int?)cbbGeneralOPD_EGD.SelectedValue;
                    patient.WardID = (int?)cbbGeneralWard_EGD.SelectedValue;
                    patient.FinancialID = (int?)cbbGeneralFinancial_EGD.SelectedValue;
                    patient.DoctorID = (int?)cbbGeneralDoctor_EGD.SelectedValue;
                    patient.NurseFirstID = (int?)cbbGeneralNurse1_EGD.SelectedValue;
                    patient.NurseSecondID = (int?)cbbGeneralNurse2_EGD.SelectedValue;
                    patient.NurseThirthID = (int?)cbbGeneralNurse3_EGD.SelectedValue;
                    patient.AnesthesiaID = (int?)cbbGeneralAnesthesia_EGD.SelectedValue;
                    patient.InstrumentID = (int?)cbbInstrument_EGD.SelectedValue;
                    patient.AnesthesistID = (int?)cbbGeneralAnesthesist_EGD.SelectedValue;
                    patient.PreDiagnosisFirstID = !string.IsNullOrWhiteSpace(txbGeneralDx1ID_EGD.Text) ? Convert.ToInt32(txbGeneralDx1ID_EGD.Text) : 0;
                    patient.PreDiagnosisSecondID = !string.IsNullOrWhiteSpace(txbGeneralDx2ID_EGD.Text) ? Convert.ToInt32(txbGeneralDx2ID_EGD.Text) : 0;
                }
                else if (procedureId == 2 || procedureId == 4)
                {
                    patient.Fullname = txbFullName_Colono.Text;
                    patient.OpdID = (int?)cbbGeneralOPD_Colono.SelectedValue;
                    patient.WardID = (int?)cbbGeneralWard_Colono.SelectedValue;
                    patient.FinancialID = (int?)cbbGeneralFinancial_Colono.SelectedValue;
                    patient.DoctorID = (int?)cbbGeneralDoctor_Colono.SelectedValue;
                    patient.NurseFirstID = (int?)cbbGeneralNurse1_Colono.SelectedValue;
                    patient.NurseSecondID = (int?)cbbGeneralNurse2_Colono.SelectedValue;
                    patient.NurseThirthID = (int?)cbbGeneralNurse3_Colono.SelectedValue;
                    patient.AnesthesiaID = (int?)cbbGeneralAnesthesia_Colono.SelectedValue;
                    patient.InstrumentID = (int?)cbbInstrument_Colono.SelectedValue;
                    patient.AnesthesistID = (int?)cbbGeneralAnesthesist_Colono.SelectedValue;
                    patient.PreDiagnosisFirstID = !string.IsNullOrWhiteSpace(txbGeneralDx1ID_Colono.Text) ? Convert.ToInt32(txbGeneralDx1ID_Colono.Text) : 0;
                    patient.PreDiagnosisSecondID = !string.IsNullOrWhiteSpace(txbGeneralDx2ID_Colono.Text) ? Convert.ToInt32(txbGeneralDx2ID_Colono.Text) : 0;
                }
                else if (procedureId == 8)
                {
                    patient.Fullname = txbFullName_Lap.Text;
                    patient.OpdID = (int?)cbbGeneralOPD_Lap.SelectedValue;
                    patient.WardID = (int?)cbbGeneralWard_Lap.SelectedValue;
                    patient.FinancialID = (int?)cbbGeneralFinancial_Lap.SelectedValue;
                    patient.DoctorID = (int?)cbbGeneralDoctor_Lap.SelectedValue;
                    patient.NurseFirstID = (int?)cbbGeneralNurse1_Lap.SelectedValue;
                    patient.NurseSecondID = (int?)cbbGeneralNurse2_Lap.SelectedValue;
                    patient.NurseThirthID = (int?)cbbGeneralNurse3_Lap.SelectedValue;
                    patient.AnesthesistID = (int?)cbbGeneralAnesthesist_Lap.SelectedValue;
                }
                _db.SaveChanges();
            }
            return patient;
        }
        private void UpdateFinding(int procedureId)
        {
            var findingData = _db.Findings.Where(x => x.PatientID == _patientId).OrderByDescending(o => o.FindingID);
            if (findingData != null)
            {
                Finding finding = findingData.FirstOrDefault();
                if (procedureId == 1)
                {
                    finding.OropharynxID = (int?)cbbFindingOropharynx_EGD.SelectedValue;
                    finding.EsophagusID = (int?)cbbFindingEsophagus_EGD.SelectedValue;
                    finding.EGJunctionID = (int?)cbbFindingEGJunction_EGD.SelectedValue;
                    finding.CardiaID = (int?)cbbFindingCardia_EGD.SelectedValue;
                    finding.FundusID = (int?)cbbFindingFundus_EGD.SelectedValue;
                    finding.BodyID = (int?)cbbFindingBody_EGD.SelectedValue;
                    finding.AntrumID = (int?)cbbFindingAntrum_EGD.SelectedValue;
                    finding.PylorusID = (int?)cbbFindingPylorus_EGD.SelectedValue;
                    finding.DuodenalBulbID = (int?)cbbFindingDuodenalBulb_EGD.SelectedValue;
                    finding.SecondPartID = (int?)cbbFinding2ndPart_EGD.SelectedValue;
                    finding.Oropharynx = txbFindingOropharynx_EGD.Text;
                    finding.Esophagus = txbFindingEsophagus_EGD.Text;
                    finding.EGJunction = txbFindingEGJunction_EGD.Text;
                    finding.Cardia = txbFindingCardia_EGD.Text;
                    finding.Fundus = txbFindingFundus_EGD.Text;
                    finding.Body = txbFindingBody_EGD.Text;
                    finding.Antrum = txbFindingAntrum_EGD.Text;
                    finding.Pylorus = txbFindingPylorus_EGD.Text;
                    finding.DuodenalBulb = txbFindingDuodenalBulb_EGD.Text;
                    finding.SecondPart = txbFinding2ndPart_EGD.Text;
                    finding.PrincipalProcedureID = !string.IsNullOrWhiteSpace(txbFindingPrinncipalProcedureID_EGD.Text) ? Convert.ToInt32(txbFindingPrinncipalProcedureID_EGD.Text) : 0;
                    finding.PrincipalProcedureDetail = txbFindingPrinncipalProcedureCode_EGD.Text + "-" + txbFindingPrinncipalProcedureText_EGD.Text;
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
                    finding.Complication = txbFindingComplication_EGD.Text;
                    finding.Histopathology = txbFindingHistopathology_EGD.Text;
                    finding.Recommendation = txbFindingRecommendation_EGD.Text;
                    finding.Comment = txbFindingComment_EGD.Text;
                    finding.RapidUreaseTest = txbFindingRapidUreaseTest_EGD.Text;
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
                    finding.PrincipalProcedureDetail = txbFindingPrinncipalProcedureCode_Colono.Text + "-" + txbFindingPrinncipalProcedureText_Colono.Text;
                    finding.SupplementalProcedure1ID = !string.IsNullOrWhiteSpace(txbFindingSupplementalProcedureID_Colono.Text) ? Convert.ToInt32(txbFindingSupplementalProcedureID_Colono.Text) : 0;
                    finding.SupplementalProcedure1Detail = txbFindingSupplementalProcedureCode_Colono.Text + "-" + txbFindingSupplementalProcedureText_Colono.Text;
                    finding.SupplementalProcedure2ID = !string.IsNullOrWhiteSpace(txbFindingSupplementalProcedure2ID_Colono.Text) ? Convert.ToInt32(txbFindingSupplementalProcedure2ID_Colono.Text) : 0;
                    finding.SupplementalProcedure2Detail = txbFindingSupplementalProcedure2Code_Colono.Text + "-" + txbFindingSupplementalProcedure2Text_Colono.Text;
                    finding.Procedure = txbFindingProcedure_Colono.Text;
                    finding.DxID1 = !string.IsNullOrWhiteSpace(txbFindingDx1ID_Colono.Text) ? Convert.ToInt32(txbFindingDx1ID_Colono.Text) : 0;
                    finding.DxID1Detail = txbFindingDx1Code_Colono.Text + "-" + txbFindingDx1Text_Colono.Text;
                    finding.DxID2 = !string.IsNullOrWhiteSpace(txbFindingDx2ID_Colono.Text) ? Convert.ToInt32(txbFindingDx2ID_Colono.Text) : 0;
                    finding.DxID2Detail = txbFindingDx2Code_Colono.Text + "-" + txbFindingDx2Text_Colono.Text;
                    finding.DxID3 = !string.IsNullOrWhiteSpace(txbFindingDx3ID_Colono.Text) ? Convert.ToInt32(txbFindingDx3ID_Colono.Text) : 0;
                    finding.DxID3Detail = txbFindingDx3Code_Colono.Text + "-" + txbFindingDx3Text_Colono.Text;
                    finding.Complication = txbFindingComplication_Colono.Text;
                    finding.Histopathology = txbFindingHistopathology_Colono.Text;
                    finding.Recommendation = txbFindingRecommendation_Colono.Text;
                    finding.Comment = txbFindingComment_Colono.Text;
                }
                else if (procedureId == 3)
                {
                    finding.DuodenumID = (int?)cbbFindingDuodenum_ERCP.SelectedValue;
                    finding.PapillaMajorID = (int?)cbbFindingPapillaMajor_ERCP.SelectedValue;
                    finding.PapillaMinorID = (int?)cbbFindingPapillaMinor_ERCP.SelectedValue;
                    finding.PancreasID = (int?)cbbFindingPancreas_ERCP.SelectedValue;
                    finding.BiliarySystemID = (int?)cbbFindingBiliarySystem_ERCP.SelectedValue;
                    finding.OtherID = (int?)cbbFindingOther_ERCP.SelectedValue;
                    finding.Duodenum = txbFindingDuodenum_ERCP.Text;
                    finding.PapillaMajor = txbFindingPapillaMajor_ERCP.Text;
                    finding.PapillaMinor = txbFindingPapillaMinor_ERCP.Text;
                    finding.Pancreas = txbFindingPancreas_ERCP.Text;
                    finding.BiliarySystem = txbFindingBiliarySystem_ERCP.Text;
                    finding.OtherDetail = txbFindingOther_ERCP.Text;
                    finding.Cholangiogram = txbFindingCholangiogram_ERCP.Text;
                    finding.Pancreatogram = txbFindingPancreatogram_ERCP.Text;
                    finding.PrincipalProcedureID = !string.IsNullOrWhiteSpace(txbFindingPrinncipalProcedureID_ERCP.Text) ? Convert.ToInt32(txbFindingPrinncipalProcedureID_ERCP.Text) : 0;
                    finding.PrincipalProcedureDetail = txbFindingPrinncipalProcedureCode_ERCP.Text + "-" + txbFindingPrinncipalProcedureText_ERCP.Text;
                    finding.SupplementalProcedure1ID = !string.IsNullOrWhiteSpace(txbFindingSupplementalProcedureID_ERCP.Text) ? Convert.ToInt32(txbFindingSupplementalProcedureID_ERCP.Text) : 0;
                    finding.SupplementalProcedure1Detail = txbFindingSupplementalProcedureCode_ERCP.Text + "-" + txbFindingSupplementalProcedureText_ERCP.Text;
                    finding.SupplementalProcedure2ID = !string.IsNullOrWhiteSpace(txbFindingSupplementalProcedure2ID_ERCP.Text) ? Convert.ToInt32(txbFindingSupplementalProcedure2ID_ERCP.Text) : 0;
                    finding.SupplementalProcedure2Detail = txbFindingSupplementalProcedure2Code_ERCP.Text + "-" + txbFindingSupplementalProcedure2Text_ERCP.Text;
                    finding.Procedure = txbFindingProcedure_ERCP.Text;
                    finding.DxID1 = !string.IsNullOrWhiteSpace(txbFindingDx1ID_ERCP.Text) ? Convert.ToInt32(txbFindingDx1ID_ERCP.Text) : 0;
                    finding.DxID1Detail = txbFindingDx1Code_ERCP.Text + "-" + txbFindingDx1Text_ERCP.Text;
                    finding.DxID2 = !string.IsNullOrWhiteSpace(txbFindingDx2ID_ERCP.Text) ? Convert.ToInt32(txbFindingDx2ID_ERCP.Text) : 0;
                    finding.DxID2Detail = txbFindingDx2Code_ERCP.Text + "-" + txbFindingDx2Text_ERCP.Text;
                    finding.DxID3 = !string.IsNullOrWhiteSpace(txbFindingDx3ID_ERCP.Text) ? Convert.ToInt32(txbFindingDx3ID_ERCP.Text) : 0;
                    finding.DxID3Detail = txbFindingDx3Code_ERCP.Text + "-" + txbFindingDx3Text_ERCP.Text;
                    finding.Complication = txbFindingComplication_ERCP.Text;
                    finding.Histopathology = txbFindingHistopathology_ERCP.Text;
                    finding.Recommendation = txbFindingRecommendation_ERCP.Text;
                    finding.Comment = txbFindingComment_ERCP.Text;
                }
                else if (procedureId == 4)
                {
                    finding.VocalCordID = (int?)cbbFindingVocal_Broncho.SelectedValue;
                    finding.TracheaID = (int?)cbbFindingTrachea_Broncho.SelectedValue;
                    finding.CarinaID = (int?)cbbFindingCarina_Broncho.SelectedValue;
                    finding.RightMainID = (int?)cbbFindingRightMain_Broncho.SelectedValue;
                    finding.RightIntermideateID = (int?)cbbFindingRightIntermideate_Broncho.SelectedValue;
                    finding.RULID = (int?)cbbFindingRUL_Broncho.SelectedValue;
                    finding.RMLID = (int?)cbbFindingRML_Broncho.SelectedValue;
                    finding.RLLID = (int?)cbbFindingRLL_Broncho.SelectedValue;
                    finding.LeftMainID = (int?)cbbFindingLeftMain_Broncho.SelectedValue;
                    finding.LULID = (int?)cbbFindingLUL_Broncho.SelectedValue;
                    finding.LingularID = (int?)cbbFindingLingular_Broncho.SelectedValue;
                    finding.LLLID = (int?)cbbFindingLLL_Broncho.SelectedValue;
                    finding.VocalCord = txbFindingVocalCord_Broncho.Text;
                    finding.Trachea = txbFindingTrachea_Broncho.Text;
                    finding.Carina = txbFindingCarina_Broncho.Text;
                    finding.RightMain = txbFindingRightMain_Broncho.Text;
                    finding.RightIntermideate = txbFindingIntermideate_Broncho.Text;
                    finding.RUL = txbFindingRUL_Broncho.Text;
                    finding.RML = txbFindingRML_Broncho.Text;
                    finding.RLL = txbFindingRLL_Broncho.Text;
                    finding.LeftMain = txbFindingLeftMain_Broncho.Text;
                    finding.LUL = txbFindingLUL_Broncho.Text;
                    finding.Lingular = txbFindingLingular_Broncho.Text;
                    finding.LLL = txbFindingLLL_Broncho.Text;
                    finding.PrincipalProcedureID = !string.IsNullOrWhiteSpace(txbFindingPrinncipalProcedureID_Broncho.Text) ? Convert.ToInt32(txbFindingPrinncipalProcedureID_Broncho.Text) : 0;
                    finding.PrincipalProcedureDetail = txbFindingPrinncipalProcedureCode_Broncho.Text + "-" + txbFindingPrinncipalProcedureText_Broncho.Text;
                    finding.SupplementalProcedure1ID = !string.IsNullOrWhiteSpace(txbFindingSupplementalProcedureID_Broncho.Text) ? Convert.ToInt32(txbFindingSupplementalProcedureID_Broncho.Text) : 0;
                    finding.SupplementalProcedure1Detail = txbFindingSupplementalProcedureCode_Broncho.Text + "-" + txbFindingSupplementalProcedureText_Broncho.Text;
                    finding.SupplementalProcedure2ID = !string.IsNullOrWhiteSpace(txbFindingSupplementalProcedure2ID_Broncho.Text) ? Convert.ToInt32(txbFindingSupplementalProcedure2ID_Broncho.Text) : 0;
                    finding.SupplementalProcedure2Detail = txbFindingSupplementalProcedure2Code_Broncho.Text + "-" + txbFindingSupplementalProcedure2Text_Broncho.Text;
                    finding.Procedure = txbFindingProcedure_Broncho.Text;
                    finding.DxID1 = !string.IsNullOrWhiteSpace(txbFindingDx1ID_Broncho.Text) ? Convert.ToInt32(txbFindingDx1ID_Broncho.Text) : 0;
                    finding.DxID1Detail = txbFindingDx1Code_Broncho.Text + "-" + txbFindingDx1Text_Broncho.Text;
                    finding.DxID2 = !string.IsNullOrWhiteSpace(txbFindingDx2ID_Broncho.Text) ? Convert.ToInt32(txbFindingDx2ID_Broncho.Text) : 0;
                    finding.DxID2Detail = txbFindingDx2Code_Broncho.Text + "-" + txbFindingDx2Text_Broncho.Text;
                    finding.DxID3 = !string.IsNullOrWhiteSpace(txbFindingDx3ID_Broncho.Text) ? Convert.ToInt32(txbFindingDx3ID_Broncho.Text) : 0;
                    finding.DxID3Detail = txbFindingDx3Code_Broncho.Text + "-" + txbFindingDx3Text_Broncho.Text;
                    finding.Complication = txbFindingComplication_Broncho.Text;
                    finding.Histopathology = txbFindingHistopathology_Broncho.Text;
                    finding.Recommendation = txbFindingRecommendation_Broncho.Text;
                    finding.Comment = txbFindingComment_Broncho.Text;
                }
                else if (procedureId == 5)
                {
                    finding.NoseID = (int?)cbbFindingNose_ENT.SelectedValue;
                    finding.Nose = txbFindingNose_Ent.Text;
                    finding.NasopharynxID = (int?)cbbFindingNasopharynx_Ent.SelectedValue;
                    finding.Nasopharynx = txbFindingNasopharynx_Ent.Text;
                    finding.BaseOfTongueID = (int?)cbbFindingBaseOfTongue_Ent.SelectedValue;
                    finding.BaseOfTongue = txbFindingBaseOfTongue_Ent.Text;
                    finding.SupraglotticID = (int?)cbbFindingSupraglottic_Ent.SelectedValue;
                    finding.Supraglottic = txbFindingSupraglottic_Ent.Text;
                    finding.PyriformID = (int?)cbbFindingPyriform_Ent.SelectedValue;
                    finding.Pyriform = txbFindingPyriform_Ent.Text;
                    finding.EsophagusID = (int?)cbbFindingEsophagus_Ent.SelectedValue;
                    finding.Esophagus = txbFindingEsophagus_Ent.Text;
                    finding.StomachID = (int?)cbbFindingStomach_Ent.SelectedValue;
                    finding.Stomach = txbFindingStomach_Ent.Text;
                    finding.PrincipalProcedureID = !string.IsNullOrWhiteSpace(txbFindingPrinncipalProcedureID_Ent.Text) ? Convert.ToInt32(txbFindingPrinncipalProcedureID_Ent.Text) : 0;
                    finding.PrincipalProcedureDetail = txbFindingPrinncipalProcedureCode_Ent.Text + "-" + txbFindingPrinncipalProcedureText_Ent.Text;
                    finding.SupplementalProcedure1ID = !string.IsNullOrWhiteSpace(txbFindingSupplementalProcedureID_Ent.Text) ? Convert.ToInt32(txbFindingSupplementalProcedureID_Ent.Text) : 0;
                    finding.SupplementalProcedure1Detail = txbFindingSupplementalProcedureCode_Ent.Text + "-" + txbFindingSupplementalProcedureText_Ent.Text;
                    finding.SupplementalProcedure2ID = !string.IsNullOrWhiteSpace(txbFindingSupplementalProcedureID2_Ent.Text) ? Convert.ToInt32(txbFindingSupplementalProcedureID2_Ent.Text) : 0;
                    finding.SupplementalProcedure2Detail = txbFindingSupplementalProcedureCode2_Ent.Text + "-" + txbFindingSupplementalProcedureText2_Ent.Text;
                    finding.Procedure = txbFindingProcedure_Ent.Text;
                    finding.DxID1 = !string.IsNullOrWhiteSpace(txbFindingDx1ID_Ent.Text) ? Convert.ToInt32(txbFindingDx1ID_Ent.Text) : 0;
                    finding.DxID1Detail = txbFindingDx1Code_Ent.Text + "-" + txbFindingDx1Text_Ent.Text;
                    finding.DxID2 = !string.IsNullOrWhiteSpace(txbFindingDx2ID_Ent.Text) ? Convert.ToInt32(txbFindingDx2ID_Ent.Text) : 0;
                    finding.DxID2Detail = txbFindingDx2Code_Ent.Text + "-" + txbFindingDx2Text_Ent.Text;
                    finding.DxID3 = !string.IsNullOrWhiteSpace(txbFindingDx3ID_Ent.Text) ? Convert.ToInt32(txbFindingDx3ID_Ent.Text) : 0;
                    finding.DxID3Detail = txbFindingDx3Code_Ent.Text + "-" + txbFindingDx3Text_Ent.Text;
                    finding.Complication = txbFindingComplication_Ent.Text;
                    finding.Histopathology = txbFindingHistopathology_Ent.Text;
                    finding.Recommendation = txbFindingRecommendation_Ent.Text;
                    finding.Comment = txbFindingComment_Ent.Text;
                }
                finding.UpdateDate = System.DateTime.Now;
                finding.UpdateBy = _id;

                _db.SaveChanges();
            }
        }
        private void UpdateAppointment(int? endoId)
        {
            Appointment data = _db.Appointments.Where(x => x.AppointmentID == _appointmentId && x.PatientID == _patientId && x.HN == _hnNo && x.ProcedureID == _procedureId).FirstOrDefault();
            if (data != null)
            {
                if (_procedureId == 1 || _procedureId == 3)
                {
                    data.IsNewCase = chkNewCase_EGD.Checked;
                    data.IsFollowCase = chkFollowUpCase_EGD.Checked;
                }
                else if (_procedureId == 2 || _procedureId == 4)
                {
                    data.IsNewCase = chkNewCase_Colono.Checked;
                    data.IsFollowCase = chkFollowUpCase_Colono.Checked;
                }
                else if (_procedureId == 8)
                {
                    data.IsNewCase = chkNewCase_Lap.Checked;
                    data.IsFollowCase = chkFollowUpCase_Lap.Checked;
                }
                data.EndoscopicCheck = true;
                data.UpdateBy = _id;
                data.UpdateDate = DateTime.Now;
                data.EndoscopicID = endoId;
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
                    pictureBoxSaved6,
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
                    pictureBoxSaved18,
                    pictureBoxSaved19,
                    pictureBoxSaved20,
                    pictureBoxSaved21,
                    pictureBoxSaved22,
                    pictureBoxSaved23,
                    pictureBoxSaved24,
                    pictureBoxSaved25,
                    pictureBoxSaved26,
                    pictureBoxSaved27,
                    pictureBoxSaved28,
                    pictureBoxSaved29,
                    pictureBoxSaved30,
                    pictureBoxSaved31,
                    pictureBoxSaved32
            };
            System.Windows.Forms.TextBox[] texts =
            {
                    txtPictureBoxSaved1,
                    txtPictureBoxSaved2,
                    txtPictureBoxSaved3,
                    txtPictureBoxSaved4,
                    txtPictureBoxSaved5,
                    txtPictureBoxSaved6,
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
                    txtPictureBoxSaved18,
                    txtPictureBoxSaved19,
                    txtPictureBoxSaved20,
                    txtPictureBoxSaved21,
                    txtPictureBoxSaved22,
                    txtPictureBoxSaved23,
                    txtPictureBoxSaved24,
                    txtPictureBoxSaved25,
                    txtPictureBoxSaved26,
                    txtPictureBoxSaved27,
                    txtPictureBoxSaved28,
                    txtPictureBoxSaved29,
                    txtPictureBoxSaved30,
                    txtPictureBoxSaved31,
                    txtPictureBoxSaved32
            };
            int i = 0;
            int seq = 1;
            foreach (var item in texts)
            {
                string Imgpath = boxes[i].ImageLocation != null ? boxes[i].ImageLocation.ToString() : "";
                var endoImgs = _db.EndoscopicImages.Where(x => x.EndoscopicID == endoscopicID && x.ProcedureID == procedureID && x.Seq == seq).FirstOrDefault();
                if (endoImgs != null)
                {
                    endoImgs.ImagePath = string.IsNullOrWhiteSpace(Imgpath) ? null : Imgpath;
                    endoImgs.ImageComment = item.Text;
                    endoImgs.Seq = i + 1;
                    endoImgs.UpdateBy = _id;
                    endoImgs.UpdateDate = DateTime.Now;
                }
                else
                {
                    EndoscopicImage endoscopicImage = new EndoscopicImage();
                    endoscopicImage.EndoscopicID = endoscopicID;
                    endoscopicImage.ProcedureID = procedureID;
                    endoscopicImage.ImagePath = string.IsNullOrWhiteSpace(Imgpath) ? null : Imgpath;
                    endoscopicImage.ImageComment = string.IsNullOrWhiteSpace(item.Text) ? null : item.Text;
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
        private bool OnSave(string hn, int patientId, int procedureId, int endoscopicId)
        {
            if (string.IsNullOrWhiteSpace(hn) || patientId <= 0 || procedureId <= 0 || endoscopicId <= 0)
                return false;

            try
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

                try
                {
                    var findingData = _db.Findings.Where(x => x.PatientID == patientId).OrderByDescending(o => o.FindingID).FirstOrDefault();
                    endo.FindingID = findingData.FindingID;
                    endo.IsSaved = true;
                    endo.ProcedureID = procedureId;
                    if (procedureId == 1 || procedureId == 3 || procedureId == 5)
                    {
                        endo.EndoscopistID = (int?)cbbGeneralDoctor_EGD.SelectedValue;
                        endo.NurseFirstID = (int?)cbbGeneralNurse1_EGD.SelectedValue;
                        endo.NurseSecondID = (int?)cbbGeneralNurse2_EGD.SelectedValue;
                        endo.NurseThirthID = (int?)cbbGeneralNurse3_EGD.SelectedValue;
                        endo.StartRecordDate = new DateTime(
                                dpGeneralFrom_EGD.Value.Year,
                                dpGeneralFrom_EGD.Value.Month,
                                dpGeneralFrom_EGD.Value.Day,
                                dpGeneralFrom_EGD.Value.Hour,
                                dpGeneralFrom_EGD.Value.Minute,
                                dpGeneralFrom_EGD.Value.Second);
                        endo.EndRecordDate = new DateTime(
                                dpGeneralTo_EGD.Value.Year,
                                dpGeneralTo_EGD.Value.Month,
                                dpGeneralTo_EGD.Value.Day,
                                dpGeneralTo_EGD.Value.Hour,
                                dpGeneralTo_EGD.Value.Minute,
                                dpGeneralTo_EGD.Value.Second);
                        endo.AnesthesiaID = (int?)cbbGeneralAnesthesia_EGD.SelectedValue;
                        endo.MedicationID = (int?)cbbGeneralMedication_EGD.SelectedValue;
                        endo.IndicationID = (int?)cbbGeneralIndication_EGD.SelectedValue;
                        endo.Anesthesia = txbGeneralAnesthesia_EGD.Text;
                        endo.MedicationOther = txbGeneralMedication_EGD.Text;
                        endo.IndicationOther = txbGeneralIndication_EGD.Text;
                        endo.DxId1 = !string.IsNullOrWhiteSpace(txbGeneralDx1ID_EGD.Text) ? Convert.ToInt32(txbGeneralDx1ID_EGD.Text) : 0;
                        endo.DxId1Detail = txbGeneralDx1Code_EGD.Text + "-" + txbGeneralDx1Text_EGD.Text;
                        endo.DxId2 = !string.IsNullOrWhiteSpace(txbGeneralDx2ID_EGD.Text) ? Convert.ToInt32(txbGeneralDx2ID_EGD.Text) : 0;
                        endo.DxId2Detail = txbGeneralDx2Code_EGD.Text + "-" + txbGeneralDx2Text_EGD.Text;
                        endo.BriefHistory = txbBriefHistory_EGD.Text;
                    }
                    else if (procedureId == 2 || procedureId == 4)
                    {
                        endo.EndoscopistID = (int?)cbbGeneralDoctor_Colono.SelectedValue;
                        endo.NurseFirstID = (int?)cbbGeneralNurse1_Colono.SelectedValue;
                        endo.NurseSecondID = (int?)cbbGeneralNurse2_Colono.SelectedValue;
                        endo.NurseThirthID = (int?)cbbGeneralNurse3_Colono.SelectedValue;
                        endo.StartRecordDate = new DateTime(
                                dpGeneralFrom_Colono.Value.Year,
                                dpGeneralFrom_Colono.Value.Month,
                                dpGeneralFrom_Colono.Value.Day,
                                dpGeneralFrom_Colono.Value.Hour,
                                dpGeneralFrom_Colono.Value.Minute,
                                dpGeneralFrom_Colono.Value.Second);
                        endo.EndRecordDate = new DateTime(
                                dpGeneralTo_Colono.Value.Year,
                                dpGeneralTo_Colono.Value.Month,
                                dpGeneralTo_Colono.Value.Day,
                                dpGeneralTo_Colono.Value.Hour,
                                dpGeneralTo_Colono.Value.Minute,
                                dpGeneralTo_Colono.Value.Second);
                        endo.AnesthesiaID = (int?)cbbGeneralAnesthesia_Colono.SelectedValue;
                        endo.MedicationID = (int?)cbbGeneralMedication_Colono.SelectedValue;
                        endo.IndicationID = (int?)cbbGeneralIndication_Colono.SelectedValue;
                        endo.Anesthesia = txbGeneralAnesthesia_Colono.Text;
                        endo.MedicationOther = txbGeneralMedication_Colono.Text;
                        endo.IndicationOther = txbGeneralIndication_Colono.Text;
                        endo.DxId1 = !string.IsNullOrWhiteSpace(txbGeneralDx1ID_Colono.Text) ? Convert.ToInt32(txbGeneralDx1ID_Colono.Text) : 0;
                        endo.DxId1Detail = txbGeneralDx1Code_Colono.Text + "-" + txbGeneralDx1Text_Colono.Text;
                        endo.DxId2 = !string.IsNullOrWhiteSpace(txbGeneralDx2ID_Colono.Text) ? Convert.ToInt32(txbGeneralDx2ID_Colono.Text) : 0;
                        endo.DxId2Detail = txbGeneralDx2Code_Colono.Text + "-" + txbGeneralDx2IText_Colono.Text;
                        endo.BriefHistory = txbBriefHistory_Colono.Text;
                        endo.BowelPreparationRegimen = txbGeneralBowelPreparationRegimen_Colono.Text;
                        endo.BowelPreparationResult = txbGeneralBowelPreparationResult_colono.Text;
                    }
                    else if (procedureId == 8)
                    {
                        endo.EndoscopistID = (int?)cbbGeneralDoctor_Lap.SelectedValue;
                        endo.NurseFirstID = (int?)cbbGeneralNurse1_Colono.SelectedValue;
                        endo.NurseSecondID = (int?)cbbGeneralNurse2_Colono.SelectedValue;
                        endo.NurseThirthID = (int?)cbbGeneralNurse3_Colono.SelectedValue;
                        endo.StartRecordDate = new DateTime(
                                dpGeneralFrom_Colono.Value.Year,
                                dpGeneralFrom_Colono.Value.Month,
                                dpGeneralFrom_Colono.Value.Day,
                                dpGeneralFrom_Colono.Value.Hour,
                                dpGeneralFrom_Colono.Value.Minute,
                                dpGeneralFrom_Colono.Value.Second);
                        endo.EndRecordDate = new DateTime(
                                dpGeneralTo_Colono.Value.Year,
                                dpGeneralTo_Colono.Value.Month,
                                dpGeneralTo_Colono.Value.Day,
                                dpGeneralTo_Colono.Value.Hour,
                                dpGeneralTo_Colono.Value.Minute,
                                dpGeneralTo_Colono.Value.Second);
                        endo.AnesthesiaID = (int?)cbbGeneralAnesthesia_Lap.SelectedValue;
                        endo.MedicationID = (int?)cbbGeneralMedication_Lap.SelectedValue;
                        endo.IndicationID = (int?)cbbGeneralIndication_Lap.SelectedValue;
                        endo.Anesthesia = txbGeneralAnesthesia_Lap.Text;
                        endo.MedicationOther = txbGeneralMedication_Lap.Text;
                        endo.IndicationOther = txbGeneralIndication_Lap.Text;
                        endo.AnesthesiaID = (int?)cbbGeneralAnesthesia_Lap.SelectedValue;
                        endo.BriefHistory = txbGeneralBriefHistory_Lap.Text;
                        endo.Diagnosis = txbFindingDiagnosis_Lap.Text;
                        endo.GeneralOperativeFinding = txbFindingOperative_Lap.Text;
                        endo.Comment = txbFindingComment_Lap.Text;
                    }
                    endo.UpdateDate = System.DateTime.Now;
                    endo.UpdateBy = _id;

                    var patient = UpdatePatientInfo(patientId, procedureId);
                    UpdateFinding(procedureId);
                    UpdateAppointment(endo.EndoscopicID);
                    SaveImage(endo.EndoscopicID, procedureId);
                    SaveLogEndoscopic(endo, patientId, procedureId);
                    SaveLogHistory(patientId, procedureId, patient.DoctorID, endo.EndoscopicID);
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

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

            if (procedureId == 1 || procedureId == 3 || procedureId == 5)   // EGD, ERCP, ENT
            {
                tabControl1.TabPages.Add(tabGeneralEGD);
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
                _dropdownListService.DropdownInstrument(cbbInstrument_EGD);

                // Finding Tab
                if (procedureId == 1)
                {
                    tabControl1.TabPages.Add(tabFindingEGD);

                    _dropdownListService.DropdownOropharynx(cbbFindingOropharynx_EGD);
                    _dropdownListService.DropdownEsophagus(cbbFindingEsophagus_EGD);
                    _dropdownListService.DropdownEGJunction(cbbFindingEGJunction_EGD);
                    _dropdownListService.DropdownCardia(cbbFindingCardia_EGD);
                    _dropdownListService.DropdownFundus(cbbFindingFundus_EGD);
                    _dropdownListService.DropdownBody(cbbFindingBody_EGD);
                    _dropdownListService.DropdownAntrum(cbbFindingAntrum_EGD);
                    _dropdownListService.DropdownPylorus(cbbFindingPylorus_EGD);
                    _dropdownListService.DropdownDuodenalBulb(cbbFindingDuodenalBulb_EGD);
                    _dropdownListService.DropdownSecondPart(cbbFinding2ndPart_EGD);
                }
                else if (procedureId == 5)
                {
                    tabControl1.TabPages.Add(tabFindingENT);

                    _dropdownListService.DropdownEsophagus(cbbFindingNose_ENT);
                    _dropdownListService.DropdownEsophagus(cbbFindingNasopharynx_Ent);
                    _dropdownListService.DropdownEsophagus(cbbFindingBaseOfTongue_Ent);
                    _dropdownListService.DropdownEsophagus(cbbFindingSupraglottic_Ent);
                    _dropdownListService.DropdownEsophagus(cbbFindingPyriform_Ent);
                    _dropdownListService.DropdownEsophagus(cbbFindingEsophagus_Ent);
                    _dropdownListService.DropdownEsophagus(cbbFindingStomach_Ent);
                }
                else
                {
                    tabControl1.TabPages.Add(tabFindingERCP);

                    _dropdownListService.DropdownDuodenum(cbbFindingDuodenum_ERCP);
                    _dropdownListService.DropdownPapillaMajor(cbbFindingPapillaMajor_ERCP);
                    _dropdownListService.DropdownPapillaMinor(cbbFindingPapillaMinor_ERCP);
                    _dropdownListService.DropdownPancrea(cbbFindingPancreas_ERCP);
                    _dropdownListService.DropdownBiliarySystem(cbbFindingBiliarySystem_ERCP);
                    _dropdownListService.DropdownOther(cbbFindingOther_ERCP);
                }
            }
            else if (procedureId == 2 || procedureId == 4)  // Colonoscopy, Bronchoscopy
            {
                tabControl1.TabPages.Add(tabGeneralColonoscopy);
                // General Tab
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
                _dropdownListService.DropdownInstrument(cbbInstrument_Colono);

                // Finding Tab
                if (procedureId == 2)
                {
                    tabControl1.TabPages.Add(tabFindingColonoscopy);

                    _dropdownListService.DropdownAnalCanal(cbbFindingAnalCanal_Colono);
                    _dropdownListService.DropdownRectum(cbbFindingRectum_Colono);
                    _dropdownListService.DropdownSigmoidColon(cbbFindingSigmoid_Colono);
                    _dropdownListService.DropdownDescendingColon(cbbFindingDescending_Colono);
                    _dropdownListService.DropdownSplenicFlexure(cbbFindingFlexure_Colono);
                    _dropdownListService.DropdownTransverseColon(cbbFindingTransverse_Colono);
                    _dropdownListService.DropdownHepaticFlexure(cbbFindingHepatic_Colono);
                    _dropdownListService.DropdownAscendingColon(cbbFindingAscending_Colono);
                    _dropdownListService.DropdownIleocecalValve(cbbFindingIleocecal_Colono);
                    _dropdownListService.DropdownCecum(cbbFindingCecum_Colono);
                    _dropdownListService.DropdownTerminalIleum(cbbFindingTerminal_Colono);
                }
                else
                {
                    tabControl1.TabPages.Add(tabFindingBronchoscopy);

                    _dropdownListService.DropdownVocalCord(cbbFindingVocal_Broncho);
                    _dropdownListService.DropdownTrachea(cbbFindingTrachea_Broncho);
                    _dropdownListService.DropdownCarina(cbbFindingCarina_Broncho);
                    _dropdownListService.DropdownRightMain(cbbFindingRightMain_Broncho);
                    _dropdownListService.DropdownRightIntermideate(cbbFindingRightIntermideate_Broncho);
                    _dropdownListService.DropdownRUL(cbbFindingRUL_Broncho);
                    _dropdownListService.DropdownRML(cbbFindingRML_Broncho);
                    _dropdownListService.DropdownRLL(cbbFindingRLL_Broncho);
                    _dropdownListService.DropdownLeftMain(cbbFindingLeftMain_Broncho);
                    _dropdownListService.DropdownLUL(cbbFindingLUL_Broncho);
                    _dropdownListService.DropdownLingular(cbbFindingLingular_Broncho);
                    _dropdownListService.DropdownLLL(cbbFindingLLL_Broncho);
                }
            }
            else if (procedureId == 8) // Laparoscopy
            {
                tabControl1.TabPages.Add(tabGeneralLab);
                tabControl1.TabPages.Add(tabFindingLab);

                // General Tab
                _dropdownListService.DropdownOPD(cbbGeneralOPD_Lap);
                _dropdownListService.DropdownWard(cbbGeneralWard_Lap);
                _dropdownListService.DropdownDoctor(cbbGeneralDoctor_Lap);
                _dropdownListService.DropdownAnesthesist(cbbGeneralAnesthesist_Lap);
                _dropdownListService.DropdownNurse(cbbGeneralNurse1_Lap);
                _dropdownListService.DropdownNurse(cbbGeneralNurse2_Lap);
                _dropdownListService.DropdownNurse(cbbGeneralNurse3_Lap);
                _dropdownListService.DropdownFinancial(cbbGeneralFinancial_Lap);
                _dropdownListService.DropdownInstrument(cbbGeneralInstrument_Lap);
                _dropdownListService.DropdownAnesthesia(cbbGeneralAnesthesia_Lap);
                _dropdownListService.DropdownMedication(cbbGeneralMedication_Lap);
                _dropdownListService.DropdownIndication(cbbGeneralIndication_Lap);
            }
            else
            {
                RemoveTabPage();
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
            btnReport.Visible = false;
        }

        #endregion

        #region Resizing Image

        private void pictureBoxRefresh()
        {
            //pictureBoxSaved1.ImageLocation = pictureBoxSaved1.ImageLocation;
            //pictureBoxSaved1.Refresh();
            //pictureBoxSaved2.ImageLocation = pictureBoxSaved2.ImageLocation;
            //pictureBoxSaved2.Refresh();
            //pictureBoxSaved3.ImageLocation = pictureBoxSaved3.ImageLocation;
            //pictureBoxSaved3.Refresh();
            //pictureBoxSaved4.ImageLocation = pictureBoxSaved4.ImageLocation;
            //pictureBoxSaved4.Refresh();
            //pictureBoxSaved5.ImageLocation = pictureBoxSaved5.ImageLocation;
            //pictureBoxSaved5.Refresh();
            //pictureBoxSaved6.ImageLocation = pictureBoxSaved6.ImageLocation;
            //pictureBoxSaved6.Refresh();
            //pictureBoxSaved7.ImageLocation = pictureBoxSaved7.ImageLocation;
            //pictureBoxSaved7.Refresh();
            //pictureBoxSaved8.ImageLocation = pictureBoxSaved8.ImageLocation;
            //pictureBoxSaved8.Refresh();
            //pictureBoxSaved9.ImageLocation = pictureBoxSaved9.ImageLocation;
            //pictureBoxSaved9.Refresh();
            //pictureBoxSaved9.ImageLocation = pictureBoxSaved9.ImageLocation;
            //pictureBoxSaved9.Refresh();
            //pictureBoxSaved10.ImageLocation = pictureBoxSaved10.ImageLocation;
            //pictureBoxSaved10.Refresh();
            //pictureBoxSaved11.ImageLocation = pictureBoxSaved11.ImageLocation;
            //pictureBoxSaved11.Refresh();
            //pictureBoxSaved12.ImageLocation = pictureBoxSaved12.ImageLocation;
            //pictureBoxSaved12.Refresh();
            //pictureBoxSaved13.ImageLocation = pictureBoxSaved13.ImageLocation;
            //pictureBoxSaved13.Refresh();
            //pictureBoxSaved14.ImageLocation = pictureBoxSaved14.ImageLocation;
            //pictureBoxSaved14.Refresh();
            //pictureBoxSaved15.ImageLocation = pictureBoxSaved15.ImageLocation;
            //pictureBoxSaved15.Refresh();
            //pictureBoxSaved16.ImageLocation = pictureBoxSaved16.ImageLocation;
            //pictureBoxSaved16.Refresh();
            //pictureBoxSaved17.ImageLocation = pictureBoxSaved17.ImageLocation;
            //pictureBoxSaved17.Refresh();
            //pictureBoxSaved18.ImageLocation = pictureBoxSaved18.ImageLocation;
            //pictureBoxSaved18.Refresh();

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

                    var cropedImg = await ResizeImg(img, aspectRatio_X, aspectRatio_Y, width, height, isFullScreen);
                    if (cropedImg != null)
                    {
                        string newPathUpload = _pathFolderImageSave + "upload_" + nameCapture;
                        cropedImg.Save(newPathUpload, System.Drawing.Imaging.ImageFormat.Jpeg);
                        ImgPath = newPathUpload;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
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

        #endregion

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
        private void btnDeletePictureBoxSaved6_Click(object sender, EventArgs e)
        {
            pictureBoxSaved6.ImageLocation = null;
            pictureBoxSaved6.Update();
            btnDeletePictureBoxSaved6.Visible = false;
            btnEditPic6.Visible = false;
        }
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
        private void btnDeletePictureBoxSaved19_Click(object sender, EventArgs e)
        {
            pictureBoxSaved18.ImageLocation = null;
            pictureBoxSaved18.Update();
            btnDeletePictureBoxSaved18.Visible = false;
            btnEditPic18.Visible = false;
        }
        private void btnDeletePictureBoxSaved20_Click(object sender, EventArgs e)
        {
            pictureBoxSaved20.ImageLocation = null;
            pictureBoxSaved20.Update();
            btnDeletePictureBoxSaved20.Visible = false;
            btnEditPic20.Visible = false;
        }
        private void btnDeletePictureBoxSaved21_Click(object sender, EventArgs e)
        {
            pictureBoxSaved21.ImageLocation = null;
            pictureBoxSaved21.Update();
            btnDeletePictureBoxSaved21.Visible = false;
            btnEditPic21.Visible = false;
        }
        private void btnDeletePictureBoxSaved22_Click(object sender, EventArgs e)
        {
            pictureBoxSaved22.ImageLocation = null;
            pictureBoxSaved22.Update();
            btnDeletePictureBoxSaved22.Visible = false;
            btnEditPic22.Visible = false;
        }
        private void btnDeletePictureBoxSaved23_Click(object sender, EventArgs e)
        {
            pictureBoxSaved23.ImageLocation = null;
            pictureBoxSaved23.Update();
            btnDeletePictureBoxSaved23.Visible = false;
            btnEditPic23.Visible = false;
        }
        private void btnDeletePictureBoxSaved24_Click(object sender, EventArgs e)
        {
            pictureBoxSaved24.ImageLocation = null;
            pictureBoxSaved24.Update();
            btnDeletePictureBoxSaved24.Visible = false;
            btnEditPic24.Visible = false;
        }
        private void btnDeletePictureBoxSaved25_Click(object sender, EventArgs e)
        {
            pictureBoxSaved25.ImageLocation = null;
            pictureBoxSaved25.Update();
            btnDeletePictureBoxSaved25.Visible = false;
            btnEditPic25.Visible = false;
        }
        private void btnDeletePictureBoxSaved26_Click(object sender, EventArgs e)
        {
            pictureBoxSaved26.ImageLocation = null;
            pictureBoxSaved26.Update();
            btnDeletePictureBoxSaved26.Visible = false;
            btnEditPic26.Visible = false;
        }
        private void btnDeletePictureBoxSaved27_Click(object sender, EventArgs e)
        {
            pictureBoxSaved27.ImageLocation = null;
            pictureBoxSaved27.Update();
            btnDeletePictureBoxSaved27.Visible = false;
            btnEditPic27.Visible = false;
        }
        private void btnDeletePictureBoxSaved28_Click(object sender, EventArgs e)
        {
            pictureBoxSaved28.ImageLocation = null;
            pictureBoxSaved28.Update();
            btnDeletePictureBoxSaved28.Visible = false;
            btnEditPic28.Visible = false;
        }
        private void btnDeletePictureBoxSaved29_Click(object sender, EventArgs e)
        {
            pictureBoxSaved29.ImageLocation = null;
            pictureBoxSaved29.Update();
            btnDeletePictureBoxSaved29.Visible = false;
            btnEditPic29.Visible = false;
        }
        private void btnDeletePictureBoxSaved30_Click(object sender, EventArgs e)
        {
            pictureBoxSaved30.ImageLocation = null;
            pictureBoxSaved30.Update();
            btnDeletePictureBoxSaved30.Visible = false;
            btnEditPic30.Visible = false;
        }
        private void btnDeletePictureBoxSaved31_Click(object sender, EventArgs e)
        {
            pictureBoxSaved31.ImageLocation = null;
            pictureBoxSaved31.Update();
            btnDeletePictureBoxSaved31.Visible = false;
            btnEditPic31.Visible = false;
        }
        private void btnDeletePictureBoxSaved32_Click(object sender, EventArgs e)
        {
            pictureBoxSaved32.ImageLocation = null;
            pictureBoxSaved32.Update();
            btnDeletePictureBoxSaved32.Visible = false;
            btnEditPic32.Visible = false;
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
        private void btnEditPic6_Click(object sender, EventArgs e)
        {
            EditPictureCaptureOnClick(pictureBoxSaved6);
        }
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
        private void btnEditPic19_Click(object sender, EventArgs e)
        {
            EditPictureCaptureOnClick(pictureBoxSaved19);
        }
        private void btnEditPic20_Click(object sender, EventArgs e)
        {
            EditPictureCaptureOnClick(pictureBoxSaved20);
        }
        private void btnEditPic21_Click(object sender, EventArgs e)
        {
            EditPictureCaptureOnClick(pictureBoxSaved21);
        }
        private void btnEditPic22_Click(object sender, EventArgs e)
        {
            EditPictureCaptureOnClick(pictureBoxSaved22);
        }
        private void btnEditPic23_Click(object sender, EventArgs e)
        {
            EditPictureCaptureOnClick(pictureBoxSaved23);
        }
        private void btnEditPic24_Click(object sender, EventArgs e)
        {
            EditPictureCaptureOnClick(pictureBoxSaved24);
        }
        private void btnEditPic25_Click(object sender, EventArgs e)
        {
            EditPictureCaptureOnClick(pictureBoxSaved25);
        }
        private void btnEditPic26_Click(object sender, EventArgs e)
        {
            EditPictureCaptureOnClick(pictureBoxSaved26);
        }
        private void btnEditPic27_Click(object sender, EventArgs e)
        {
            EditPictureCaptureOnClick(pictureBoxSaved27);
        }
        private void btnEditPic28_Click(object sender, EventArgs e)
        {
            EditPictureCaptureOnClick(pictureBoxSaved28);
        }
        private void btnEditPic29_Click(object sender, EventArgs e)
        {
            EditPictureCaptureOnClick(pictureBoxSaved29);
        }
        private void btnEditPic30_Click(object sender, EventArgs e)
        {
            EditPictureCaptureOnClick(pictureBoxSaved30);
        }
        private void btnEditPic31_Click(object sender, EventArgs e)
        {
            EditPictureCaptureOnClick(pictureBoxSaved31);
        }
        private void btnEditPic32_Click(object sender, EventArgs e)
        {
            EditPictureCaptureOnClick(pictureBoxSaved32);
        }
        public void EditPictureCaptureOnClick(PictureBox pictureBox)
        {
            //using (System.Diagnostics.Process ExternalProcess = new System.Diagnostics.Process())
            //{
            //    ExternalProcess.StartInfo.FileName = "mspaint.exe";
            //    string imgLo = string.Concat("\"", pictureBox.ImageLocation, "\"");
            //    ExternalProcess.StartInfo.Arguments = imgLo;
            //    ExternalProcess.StartInfo.UseShellExecute = true;
            //    ExternalProcess.Start();
            //    ExternalProcess.WaitForExit();
            //}
            try
            {
                using (System.Diagnostics.Process ExternalProcess = new System.Diagnostics.Process())
                {
                    ExternalProcess.StartInfo.FileName = "mspaint.exe";
                    string imgLo = string.Concat("\"", pictureBox.ImageLocation, "\"");
                    ExternalProcess.StartInfo.Arguments = imgLo;
                    ExternalProcess.StartInfo.UseShellExecute = true;
                    ExternalProcess.Start();
                    ExternalProcess.WaitForExit();

                    //// Load the image file into a Bitmap object
                    //Bitmap image = new Bitmap(pictureBox.ImageLocation);

                    //// Open the file in read/write mode
                    //using (FileStream stream = new FileStream(pictureBox.ImageLocation, FileMode.Open, FileAccess.ReadWrite))
                    //{
                    //    // Save the modified image to the file
                    //    image.Save(stream);
                    //}
                }
            }
            catch (Exception ex)
            {
                // Handle the exception
                //Console.WriteLine("Error: {0}", ex.Message);
                throw ex;
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
            SavePictureBoxOnClick(6, pictureBoxSaved6, btnEditPic6, btnDeletePictureBoxSaved6);
        }
        private void btnPictureBoxSaved7_Click(object sender, EventArgs e)
        {
            SavePictureBoxOnClick(7, pictureBoxSaved7, btnEditPic7, btnDeletePictureBoxSaved7);
        }
        private void btnPictureBoxSaved8_Click(object sender, EventArgs e)
        {
            SavePictureBoxOnClick(8, pictureBoxSaved8, btnEditPic8, btnDeletePictureBoxSaved8);
        }
        private void btnPictureBoxSaved9_Click(object sender, EventArgs e)
        {
            SavePictureBoxOnClick(9, pictureBoxSaved9, btnEditPic9, btnDeletePictureBoxSaved9);
        }
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
        private void btnPictureBoxSaved19_Click(object sender, EventArgs e)
        {
            SavePictureBoxOnClick(19, pictureBoxSaved19, btnEditPic19, btnDeletePictureBoxSaved19);
        }
        private void btnPictureBoxSaved20_Click(object sender, EventArgs e)
        {
            SavePictureBoxOnClick(20, pictureBoxSaved20, btnEditPic20, btnDeletePictureBoxSaved20);
        }
        private void btnPictureBoxSaved21_Click(object sender, EventArgs e)
        {
            SavePictureBoxOnClick(21, pictureBoxSaved21, btnEditPic21, btnDeletePictureBoxSaved21);
        }
        private void btnPictureBoxSaved22_Click(object sender, EventArgs e)
        {
            SavePictureBoxOnClick(22, pictureBoxSaved22, btnEditPic22, btnDeletePictureBoxSaved22);
        }
        private void btnPictureBoxSaved23_Click(object sender, EventArgs e)
        {
            SavePictureBoxOnClick(23, pictureBoxSaved23, btnEditPic23, btnDeletePictureBoxSaved23);
        }
        private void btnPictureBoxSaved24_Click(object sender, EventArgs e)
        {
            SavePictureBoxOnClick(24, pictureBoxSaved24, btnEditPic24, btnDeletePictureBoxSaved24);
        }
        private void btnPictureBoxSaved25_Click(object sender, EventArgs e)
        {
            SavePictureBoxOnClick(25, pictureBoxSaved25, btnEditPic25, btnDeletePictureBoxSaved25);
        }
        private void btnPictureBoxSaved26_Click(object sender, EventArgs e)
        {
            SavePictureBoxOnClick(26, pictureBoxSaved26, btnEditPic26, btnDeletePictureBoxSaved26);
        }
        private void btnPictureBoxSaved27_Click(object sender, EventArgs e)
        {
            SavePictureBoxOnClick(27, pictureBoxSaved27, btnEditPic27, btnDeletePictureBoxSaved27);
        }
        private void btnPictureBoxSaved28_Click(object sender, EventArgs e)
        {
            SavePictureBoxOnClick(28, pictureBoxSaved28, btnEditPic28, btnDeletePictureBoxSaved28);
        }
        private void btnPictureBoxSaved29_Click(object sender, EventArgs e)
        {
            SavePictureBoxOnClick(29, pictureBoxSaved29, btnEditPic29, btnDeletePictureBoxSaved29);
        }
        private void btnPictureBoxSaved30_Click(object sender, EventArgs e)
        {
            SavePictureBoxOnClick(30, pictureBoxSaved30, btnEditPic30, btnDeletePictureBoxSaved30);
        }
        private void btnPictureBoxSaved31_Click(object sender, EventArgs e)
        {
            SavePictureBoxOnClick(31, pictureBoxSaved31, btnEditPic31, btnDeletePictureBoxSaved31);
        }
        private void btnPictureBoxSaved32_Click(object sender, EventArgs e)
        {
            SavePictureBoxOnClick(32, pictureBoxSaved32, btnEditPic32, btnDeletePictureBoxSaved32);
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

        //private void SetDisablePictureBox(int start, int end)
        //{
        //    for (int i = start; i <= end; i++)
        //    {
        //        GroupBox groupBox = (GroupBox)this.Controls.Find("gb" + i.ToString(), true)[0];
        //        groupBox.Visible = false;
        //    }
        //}
        //private void SaveImageForLaparoscopy(int endoscopicID, int procedureID)
        //{
        //    System.Windows.Forms.PictureBox[] boxes =
        //    {
        //            pictureBoxSaved1,
        //            pictureBoxSaved2,
        //            pictureBoxSaved3,
        //            pictureBoxSaved4,
        //            pictureBoxSaved5,
        //            pictureBoxSaved6,
        //            pictureBoxSaved7,
        //            pictureBoxSaved8
        //        };
        //    System.Windows.Forms.TextBox[] texts =
        //    {
        //            txtPictureBoxSaved1,
        //            txtPictureBoxSaved2,
        //            txtPictureBoxSaved3,
        //            txtPictureBoxSaved4,
        //            txtPictureBoxSaved5,
        //            txtPictureBoxSaved6,
        //            txtPictureBoxSaved7,
        //            txtPictureBoxSaved8
        //        };
        //    int i = 0;
        //    int seq = 1;
        //    foreach (var item in texts)
        //    {
        //        string Imgpath = boxes[i].ImageLocation != null ? boxes[i].ImageLocation.ToString() : "";
        //        var endoImgs = _db.EndoscopicImages.Where(x => x.EndoscopicID == endoscopicID && x.ProcedureID == procedureID && x.Seq == seq).FirstOrDefault();
        //        if (endoImgs != null)
        //        {
        //            endoImgs.ImagePath = string.IsNullOrWhiteSpace(Imgpath) ? null : Imgpath;
        //            endoImgs.ImageComment = item.Text;
        //            endoImgs.Seq = i + 1;
        //            endoImgs.UpdateBy = _id;
        //            endoImgs.UpdateDate = DateTime.Now;
        //        }
        //        else
        //        {
        //            EndoscopicImage endoscopicImage = new EndoscopicImage();
        //            endoscopicImage.EndoscopicID = endoscopicID;
        //            endoscopicImage.ProcedureID = procedureID;
        //            endoscopicImage.ImagePath = string.IsNullOrWhiteSpace(Imgpath) ? null : Imgpath;
        //            endoscopicImage.ImageComment = string.IsNullOrWhiteSpace(item.Text) ? null : item.Text;
        //            endoscopicImage.Seq = i + 1;
        //            endoscopicImage.CreateBy = _id;
        //            endoscopicImage.CreateDate = DateTime.Now;
        //            endoscopicImage.UpdateBy = _id;
        //            endoscopicImage.UpdateDate = DateTime.Now;
        //            _db.EndoscopicImages.Add(endoscopicImage);
        //        }
        //        i++;
        //        seq++;
        //    }
        //}


    }
}
