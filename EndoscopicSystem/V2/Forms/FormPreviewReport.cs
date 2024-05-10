using EndoscopicSystem.Constants;
using EndoscopicSystem.Entities;
using EndoscopicSystem.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
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
    public partial class FormPreviewReport : Form
    {
        private readonly string _pathFolderImage = ConfigurationManager.AppSettings["pathSaveImageCapture"];
        private string _pathFolderImageSave, _hnNo, _pathImg, _fileName = ".jpg", _vdoPath, _multiId;
        private readonly string _initialDirectoryUpload = "C://Desktop";
        private readonly string _titleUpload = "Select image to be upload.";
        private readonly string _filterUpload = "Image Only(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
        private int _id, _procedureId, _appointmentId, _endoscopicId, _patientId, _item, _aspectRatioID = 1;
        public Form formPopup = new Form(), formPopupVdo = new Form();
        private readonly EndoscopicEntities _db = new EndoscopicEntities();
        private readonly GetDropdownList _repo = new GetDropdownList();
        public Dictionary<int, string> _imgPath = new Dictionary<int, string>();
        private bool _isSave = false, _isEgdAndColonoDone = true;
        private DateTime? _startRec, _endRec;

        public FormPreviewReport(int id, string hn, int procId, int appId, int endoId, int patientId, string pathImg, string pathVdo, bool egdAndColonoDone, DateTime? startRec, DateTime? endRec)
        {
            formPopup = this;

            InitializeComponent();

            this._id = id;
            this._hnNo = hn;

            if (procId == 6)
            {
                if (!_isEgdAndColonoDone)
                    this._procedureId = 1;
                else
                    this._procedureId = 2;
            }
            else
            {
                this._procedureId = procId;
            }

            this._appointmentId = appId;
            this._endoscopicId = endoId;
            this._patientId = patientId;
            this._pathFolderImageSave = pathImg;
            this._vdoPath = pathVdo;
            this._isEgdAndColonoDone = egdAndColonoDone;
            this._startRec = startRec;
            this._endRec = endRec;
        }
        private void SetAllowDragDropInPictureBox()
        {
            pictureBoxSaved1.AllowDrop = true;
            pictureBoxSaved2.AllowDrop = true;
            pictureBoxSaved3.AllowDrop = true;
            pictureBoxSaved4.AllowDrop = true;
            pictureBoxSaved5.AllowDrop = true;
            pictureBoxSaved6.AllowDrop = true;
            pictureBoxSaved7.AllowDrop = true;
            pictureBoxSaved8.AllowDrop = true;
            pictureBoxSaved9.AllowDrop = true;
            pictureBoxSaved10.AllowDrop = true;
            pictureBoxSaved11.AllowDrop = true;
            pictureBoxSaved12.AllowDrop = true;
            pictureBoxSaved13.AllowDrop = true;
            pictureBoxSaved14.AllowDrop = true;
            pictureBoxSaved15.AllowDrop = true;
            pictureBoxSaved16.AllowDrop = true;
            pictureBoxSaved17.AllowDrop = true;
            pictureBoxSaved18.AllowDrop = true;
            pictureBoxSaved19.AllowDrop = true;
            pictureBoxSaved20.AllowDrop = true;
            pictureBoxSaved21.AllowDrop = true;
            pictureBoxSaved22.AllowDrop = true;
            pictureBoxSaved23.AllowDrop = true;
            pictureBoxSaved24.AllowDrop = true;
            pictureBoxSaved25.AllowDrop = true;
            pictureBoxSaved26.AllowDrop = true;
            pictureBoxSaved27.AllowDrop = true;
            pictureBoxSaved28.AllowDrop = true;
            pictureBoxSaved29.AllowDrop = true;
            pictureBoxSaved30.AllowDrop = true;
            pictureBoxSaved31.AllowDrop = true;
            pictureBoxSaved32.AllowDrop = true;

            pictureBoxSaved1.DragDrop += new System.Windows.Forms.DragEventHandler(PictureBox_DragDrop);
            pictureBoxSaved2.DragDrop += new System.Windows.Forms.DragEventHandler(PictureBox_DragDrop);
            pictureBoxSaved3.DragDrop += new System.Windows.Forms.DragEventHandler(PictureBox_DragDrop);
            pictureBoxSaved4.DragDrop += new System.Windows.Forms.DragEventHandler(PictureBox_DragDrop);
            pictureBoxSaved5.DragDrop += new System.Windows.Forms.DragEventHandler(PictureBox_DragDrop);
            pictureBoxSaved6.DragDrop += new System.Windows.Forms.DragEventHandler(PictureBox_DragDrop);
            pictureBoxSaved7.DragDrop += new System.Windows.Forms.DragEventHandler(PictureBox_DragDrop);
            pictureBoxSaved8.DragDrop += new System.Windows.Forms.DragEventHandler(PictureBox_DragDrop);
            pictureBoxSaved9.DragDrop += new System.Windows.Forms.DragEventHandler(PictureBox_DragDrop);
            pictureBoxSaved10.DragDrop += new System.Windows.Forms.DragEventHandler(PictureBox_DragDrop);
            pictureBoxSaved11.DragDrop += new System.Windows.Forms.DragEventHandler(PictureBox_DragDrop);
            pictureBoxSaved12.DragDrop += new System.Windows.Forms.DragEventHandler(PictureBox_DragDrop);
            pictureBoxSaved13.DragDrop += new System.Windows.Forms.DragEventHandler(PictureBox_DragDrop);
            pictureBoxSaved14.DragDrop += new System.Windows.Forms.DragEventHandler(PictureBox_DragDrop);
            pictureBoxSaved15.DragDrop += new System.Windows.Forms.DragEventHandler(PictureBox_DragDrop);
            pictureBoxSaved16.DragDrop += new System.Windows.Forms.DragEventHandler(PictureBox_DragDrop);
            pictureBoxSaved17.DragDrop += new System.Windows.Forms.DragEventHandler(PictureBox_DragDrop);
            pictureBoxSaved18.DragDrop += new System.Windows.Forms.DragEventHandler(PictureBox_DragDrop);
            pictureBoxSaved19.DragDrop += new System.Windows.Forms.DragEventHandler(PictureBox_DragDrop);
            pictureBoxSaved20.DragDrop += new System.Windows.Forms.DragEventHandler(PictureBox_DragDrop);
            pictureBoxSaved21.DragDrop += new System.Windows.Forms.DragEventHandler(PictureBox_DragDrop);
            pictureBoxSaved22.DragDrop += new System.Windows.Forms.DragEventHandler(PictureBox_DragDrop);
            pictureBoxSaved23.DragDrop += new System.Windows.Forms.DragEventHandler(PictureBox_DragDrop);
            pictureBoxSaved24.DragDrop += new System.Windows.Forms.DragEventHandler(PictureBox_DragDrop);
            pictureBoxSaved25.DragDrop += new System.Windows.Forms.DragEventHandler(PictureBox_DragDrop);
            pictureBoxSaved26.DragDrop += new System.Windows.Forms.DragEventHandler(PictureBox_DragDrop);
            pictureBoxSaved27.DragDrop += new System.Windows.Forms.DragEventHandler(PictureBox_DragDrop);
            pictureBoxSaved28.DragDrop += new System.Windows.Forms.DragEventHandler(PictureBox_DragDrop);
            pictureBoxSaved29.DragDrop += new System.Windows.Forms.DragEventHandler(PictureBox_DragDrop);
            pictureBoxSaved30.DragDrop += new System.Windows.Forms.DragEventHandler(PictureBox_DragDrop);
            pictureBoxSaved31.DragDrop += new System.Windows.Forms.DragEventHandler(PictureBox_DragDrop);
            pictureBoxSaved32.DragDrop += new System.Windows.Forms.DragEventHandler(PictureBox_DragDrop);

            pictureBoxSaved1.DragEnter += new System.Windows.Forms.DragEventHandler(PictureBox_DragEnter);
            pictureBoxSaved2.DragEnter += new System.Windows.Forms.DragEventHandler(PictureBox_DragEnter);
            pictureBoxSaved3.DragEnter += new System.Windows.Forms.DragEventHandler(PictureBox_DragEnter);
            pictureBoxSaved4.DragEnter += new System.Windows.Forms.DragEventHandler(PictureBox_DragEnter);
            pictureBoxSaved5.DragEnter += new System.Windows.Forms.DragEventHandler(PictureBox_DragEnter);
            pictureBoxSaved6.DragEnter += new System.Windows.Forms.DragEventHandler(PictureBox_DragEnter);
            pictureBoxSaved7.DragEnter += new System.Windows.Forms.DragEventHandler(PictureBox_DragEnter);
            pictureBoxSaved8.DragEnter += new System.Windows.Forms.DragEventHandler(PictureBox_DragEnter);
            pictureBoxSaved9.DragEnter += new System.Windows.Forms.DragEventHandler(PictureBox_DragEnter);
            pictureBoxSaved10.DragEnter += new System.Windows.Forms.DragEventHandler(PictureBox_DragEnter);
            pictureBoxSaved11.DragEnter += new System.Windows.Forms.DragEventHandler(PictureBox_DragEnter);
            pictureBoxSaved12.DragEnter += new System.Windows.Forms.DragEventHandler(PictureBox_DragEnter);
            pictureBoxSaved13.DragEnter += new System.Windows.Forms.DragEventHandler(PictureBox_DragEnter);
            pictureBoxSaved14.DragEnter += new System.Windows.Forms.DragEventHandler(PictureBox_DragEnter);
            pictureBoxSaved15.DragEnter += new System.Windows.Forms.DragEventHandler(PictureBox_DragEnter);
            pictureBoxSaved16.DragEnter += new System.Windows.Forms.DragEventHandler(PictureBox_DragEnter);
            pictureBoxSaved17.DragEnter += new System.Windows.Forms.DragEventHandler(PictureBox_DragEnter);
            pictureBoxSaved18.DragEnter += new System.Windows.Forms.DragEventHandler(PictureBox_DragEnter);
            pictureBoxSaved19.DragEnter += new System.Windows.Forms.DragEventHandler(PictureBox_DragEnter);
            pictureBoxSaved20.DragEnter += new System.Windows.Forms.DragEventHandler(PictureBox_DragEnter);
            pictureBoxSaved21.DragEnter += new System.Windows.Forms.DragEventHandler(PictureBox_DragEnter);
            pictureBoxSaved22.DragEnter += new System.Windows.Forms.DragEventHandler(PictureBox_DragEnter);
            pictureBoxSaved23.DragEnter += new System.Windows.Forms.DragEventHandler(PictureBox_DragEnter);
            pictureBoxSaved24.DragEnter += new System.Windows.Forms.DragEventHandler(PictureBox_DragEnter);
            pictureBoxSaved25.DragEnter += new System.Windows.Forms.DragEventHandler(PictureBox_DragEnter);
            pictureBoxSaved26.DragEnter += new System.Windows.Forms.DragEventHandler(PictureBox_DragEnter);
            pictureBoxSaved27.DragEnter += new System.Windows.Forms.DragEventHandler(PictureBox_DragEnter);
            pictureBoxSaved28.DragEnter += new System.Windows.Forms.DragEventHandler(PictureBox_DragEnter);
            pictureBoxSaved29.DragEnter += new System.Windows.Forms.DragEventHandler(PictureBox_DragEnter);
            pictureBoxSaved30.DragEnter += new System.Windows.Forms.DragEventHandler(PictureBox_DragEnter);
            pictureBoxSaved31.DragEnter += new System.Windows.Forms.DragEventHandler(PictureBox_DragEnter);
            pictureBoxSaved32.DragEnter += new System.Windows.Forms.DragEventHandler(PictureBox_DragEnter);
        }
        private void FormPreviewReport_Load(object sender, EventArgs e)
        {
            cbbProcedureList.ValueMember = "ProcedureID";
            cbbProcedureList.DisplayMember = "ProcedureName";
            cbbProcedureList.DataSource = _repo.GetProcedureList();
            cbbProcedureList.SelectedValue = _procedureId;

            SetAllowDragDropInPictureBox();

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

            #region Set ListView

            DirectoryInfo dinfo = new DirectoryInfo(_pathFolderImageSave);
            FileInfo[] files = (FileInfo[])dinfo.GetFiles("*.jpg").Where(w => w.Name.StartsWith("Image")).ToArray();
            var pathOriginImgList = files.OrderBy(o => o.CreationTime).Select(s => s.FullName).ToList();

            GeneratePictureBoxWwithImages(pathOriginImgList);

            #endregion

            SearchHN(_hnNo, _procedureId);
        }
        private void SearchHN(string hn, int procId = 0)
        {
            _hnNo = hn;
            _procedureId = procId;
            try
            {
                var getPatient = _db.Patients.FirstOrDefault(x => x.HN == hn && (x.IsActive.HasValue && x.IsActive.Value));
                if (getPatient != null)
                {
                    _patientId = getPatient.PatientID;
                    txbHN.Text = getPatient.HN;
                    txbPatientFullName.Text = getPatient.Fullname;
                    txbAge.Text = getPatient.Age.HasValue ? getPatient.Age.ToString() : "";
                    txbSex.Text = getPatient.Sex.HasValue ? getPatient.Sex.Value ? Constant.Male : Constant.FeMale : "";
                    txbDoctor.Text = _db.Doctors.FirstOrDefault(f => f.DoctorID == getPatient.DoctorID)?.NameTH;
                    if (_procedureId == 0)
                    {
                        _procedureId = getPatient.ProcedureID ?? 0;
                    }

                    var app = _db.Appointments.Where(x => x.PatientID == _patientId && txbHN.Text.Equals(x.HN) && x.ProcedureID == _procedureId && x.AppointmentID == _appointmentId).FirstOrDefault();
                    if (app != null)
                    {
                        _appointmentId = app.AppointmentID;
                        txbSymptom.Text = app.Symptom;
                    }

                    PushEndoscopicImage();

                    cbbProcedureList.SelectedValue = _procedureId;
                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมูลผู้ป่วย", "Patient not found.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Controls.ClearControls();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("คุณบันทึกรูปภาพเสร็จแล้ว ?", "Save form", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if (_patientId > 0)
                    {
                        UpdateEndoscopic(_procedureId);
                        btnSave.Visible = false;

                        _isSave = true;

                        FormProceed.Self.txbStep.Text = "2" + "," + _pathFolderImageSave + "," + _vdoPath;
                    }
                    else
                    {
                        _isSave = false;
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                _isSave = false;
                btnSave.Visible = true;
                MessageBox.Show(ex.Message, "Saved data error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void PictureBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }
        private void PictureBox_DragDrop(object sender, DragEventArgs e)
        {
            try
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

                Button[] buttonedit =
                {
                    btnEditPic1,
                    btnEditPic2,
                    btnEditPic3,
                    btnEditPic4,
                    btnEditPic5,
                    btnEditPic6,
                    btnEditPic7,
                    btnEditPic8,
                    btnEditPic9,
                    btnEditPic10,
                    btnEditPic11,
                    btnEditPic12,
                    btnEditPic13,
                    btnEditPic14,
                    btnEditPic15,
                    btnEditPic16,
                    btnEditPic17,
                    btnEditPic18,
                    btnEditPic19,
                    btnEditPic20,
                    btnEditPic21,
                    btnEditPic22,
                    btnEditPic23,
                    btnEditPic24,
                    btnEditPic25,
                    btnEditPic26,
                    btnEditPic27,
                    btnEditPic28,
                    btnEditPic29,
                    btnEditPic30,
                    btnEditPic31,
                    btnEditPic32
                };

                Button[] buttondel =
                {
                    btnDeletePictureBoxSaved1,
                    btnDeletePictureBoxSaved2,
                    btnDeletePictureBoxSaved3,
                    btnDeletePictureBoxSaved4,
                    btnDeletePictureBoxSaved5,
                    btnDeletePictureBoxSaved6,
                    btnDeletePictureBoxSaved7,
                    btnDeletePictureBoxSaved8,
                    btnDeletePictureBoxSaved9,
                    btnDeletePictureBoxSaved10,
                    btnDeletePictureBoxSaved11,
                    btnDeletePictureBoxSaved12,
                    btnDeletePictureBoxSaved13,
                    btnDeletePictureBoxSaved14,
                    btnDeletePictureBoxSaved15,
                    btnDeletePictureBoxSaved16,
                    btnDeletePictureBoxSaved17,
                    btnDeletePictureBoxSaved18,
                    btnDeletePictureBoxSaved19,
                    btnDeletePictureBoxSaved20,
                    btnDeletePictureBoxSaved21,
                    btnDeletePictureBoxSaved22,
                    btnDeletePictureBoxSaved23,
                    btnDeletePictureBoxSaved24,
                    btnDeletePictureBoxSaved25,
                    btnDeletePictureBoxSaved26,
                    btnDeletePictureBoxSaved27,
                    btnDeletePictureBoxSaved28,
                    btnDeletePictureBoxSaved29,
                    btnDeletePictureBoxSaved30,
                    btnDeletePictureBoxSaved31,
                    btnDeletePictureBoxSaved32
                };

                PictureBox pb = (PictureBox)sender;

                if (e.Data.GetDataPresent(DataFormats.Text))
                {
                    var image = (string)e.Data.GetData(DataFormats.Text);
                    if (image != null)
                    {
                        pb.ImageLocation = image;
                        pb.SizeMode = PictureBoxSizeMode.StretchImage;
                        pb.Update();
                    }
                }

                for (int i = 0; i < boxes.Length; i++)
                {
                    if (boxes[i].Image != null)
                    {
                        buttondel[i].Visible = true;
                        buttonedit[i].Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            if (pictureBox != null && e.Button == MouseButtons.Left)
            {
                pictureBox1.ImageLocation = pictureBox.ImageLocation;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Update();

                //pictureBox.DoDragDrop(pictureBox.Image, DragDropEffects.Copy);
            }
        }
        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            if (pictureBox != null && e.Button == MouseButtons.Left)
            {
                pictureBox.DoDragDrop(pictureBox.ImageLocation, DragDropEffects.Copy);
            }
        }

        #region Refresh PictureBox
        private void GeneratePictureBoxWwithImages(List<string> img)
        {
            Panel panel = this.panel1;
            panel.Controls.Clear();

            // Calculate the maximum width and height for each PictureBox, and the spacing between them
            int numCols = 1;
            int maxWidth = (panel.ClientSize.Width - (numCols - 1) * 10) / numCols;
            int spacing = 10;
            for (int i = 0; i < img.Count; i++)
            {
                _item = img.Count;

                // Create a new PictureBox
                System.Windows.Forms.PictureBox pictureBox = new System.Windows.Forms.PictureBox();
                pictureBox.Name = "pictureBox" + i;
                pictureBox.Size = new Size(200, 140);
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                //pictureBox.Image = Image.FromFile(img[i]);
                pictureBox.ImageLocation = img[i];

                // Add Event
                pictureBox.MouseDown += new MouseEventHandler(PictureBox_MouseDown);
                pictureBox.MouseMove += new MouseEventHandler(PictureBox_MouseMove);

                string[] pathArray = img[i].Split('\\');
                string fileName = pathArray[pathArray.Length - 1];

                // Create a new Label
                Label label = new Label();
                label.Name = "label" + i;
                label.Text = fileName;
                label.AutoSize = true;

                // Set the size of the PictureBox based on the aspect ratio of the image
                int pictureBoxWidth = maxWidth;
                int pictureBoxHeight = (int)((double)pictureBoxWidth / 200 * 140);

                // Calculate the location of the PictureBox based on its position in the grid
                int maxHeight = pictureBox.Bottom + 30;
                int row = i / numCols;
                int col = i % numCols;
                int x = col * (maxWidth + spacing);
                int y = row * (maxHeight + spacing);
                pictureBox.Location = new Point(x, y);

                // Add the Label to the Panel, positioned below the PictureBox
                label.Location = new Point(pictureBox.Left, pictureBox.Bottom + 10);

                // Add the PictureBox to the Panel
                panel.Controls.Add(pictureBox);
                panel.Controls.Add(label);
            }
        }
        private void LoadTextBoxAutoComplete(TextBox textBox)
        {
            var findingList = _repo.GetFindingLabels(_procedureId).ToList();
            if (findingList != null && findingList.Count > 0)
            {
                AutoCompleteStringCollection ac = new AutoCompleteStringCollection();
                foreach (var item in findingList)
                {
                    ac.Add(item.Name);
                }
                textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                textBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                textBox.AutoCompleteCustomSource = ac;
            }
        }
        private void pictureBoxRefresh(string pbName)
        {
            List<PictureBox> listPbs = new List<PictureBox>();

            listPbs.Add(this.Controls.Find(pbName, true).FirstOrDefault() as PictureBox);
            listPbs.AddRange(this.Controls.OfType<PictureBox>().ToList());
            listPbs.AddRange(panel1.Controls.OfType<PictureBox>().ToList());
            listPbs.AddRange(panel3.Controls.OfType<PictureBox>().ToList());

            foreach (PictureBox pb in listPbs.Distinct())
            {
                pb.ImageLocation = pb.ImageLocation;
                pb.Refresh();
            }
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
            using (System.Diagnostics.Process ExternalProcess = new System.Diagnostics.Process())
            {
                ExternalProcess.StartInfo.FileName = ("mspaint.exe");
                string imgLo = string.Concat("\"", pictureBox.ImageLocation, "\"");
                ExternalProcess.StartInfo.Arguments = imgLo;
                ExternalProcess.StartInfo.UseShellExecute = true;
                ExternalProcess.Start();
                ExternalProcess.WaitForExit();
            }
            pictureBoxRefresh(pictureBox.Name);
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

        #region Save And Update Table.

        private void UpdateAppointment(int? endoId)
        {
            var data = _db.Appointments.Where(x => x.AppointmentID == _appointmentId && x.PatientID == _patientId).FirstOrDefault();
            if (data != null)
            {
                data.EndoscopicCheck = true;
                data.UpdateBy = _id;
                data.UpdateDate = DateTime.Now;
                data.EndoscopicID = endoId;
                _db.SaveChanges();
            }
        }
        private void SaveVideo(int endoscopicID, int procedureID)
        {
            if (!string.IsNullOrWhiteSpace(_vdoPath))
            {
                var endoVideos = _db.EndoscopicVideos.Where(x => x.EndoscopicID == endoscopicID && x.ProcedureID == procedureID).FirstOrDefault();
                if (endoVideos != null)
                {
                    endoVideos.VideoPath = _vdoPath;
                    endoVideos.UpdateBy = _id;
                    endoVideos.UpdateDate = DateTime.Now;
                }
                else
                {
                    EndoscopicVideo endoscopicVideo = new EndoscopicVideo();
                    endoscopicVideo.EndoscopicID = endoscopicID;
                    endoscopicVideo.ProcedureID = procedureID;
                    endoscopicVideo.VideoPath = _vdoPath;
                    endoscopicVideo.CreateBy = _id;
                    endoscopicVideo.CreateDate = DateTime.Now;
                    endoscopicVideo.UpdateBy = _id;
                    endoscopicVideo.UpdateDate = DateTime.Now;
                    _db.EndoscopicVideos.Add(endoscopicVideo);
                }

                _db.SaveChanges();
            }
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
                String Imgpath = boxes[i].ImageLocation != null ? boxes[i].ImageLocation.ToString() : "";
                var endoImgs = _db.EndoscopicImages.Where(x => x.EndoscopicID == endoscopicID && x.ProcedureID == procedureID && x.Seq == seq).FirstOrDefault();
                if (endoImgs != null)
                {
                    endoImgs.ImagePath = Imgpath;
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

            _db.SaveChanges();
        }
        //private int SaveMultiEndoscopic(string endoscopicIds)
        //{
        //    try
        //    {
        //        MultiEndoscopic multiEndoscopic = new MultiEndoscopic()
        //        {
        //            EndoscopicID = endoscopicIds,
        //            IsSaved = false,
        //            CreateDate = DateTime.Now,
        //        };

        //        if (_db.SaveChanges() > 0)
        //        {
        //            return _db.MultiEndoscopics.LastOrDefault().ID;
        //        }

        //        return 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Save multi endoscopic is error : " + ex.Message);
        //        throw;
        //    }
        //}
        private void UpdateEndoscopic(int procedureId)
        {
            int multiProcedure = 1;
            try
            {
                List<string> idArr = new List<string>();

                Endoscopic endo = new Endoscopic();
                if (_endoscopicId == 0)
                {
                    if (procedureId == 6)
                    {
                        multiProcedure += 1;
                    }

                    for (int i = 0; i < multiProcedure; i++)
                    {
                        Endoscopic endoscopic = new Endoscopic()
                        {
                            PatientID = _patientId,
                            IsSaved = false,
                            ProcedureID = procedureId,
                            StartRecordDate = _startRec,
                            EndRecordDate = _endRec,
                            CreateBy = _id,
                            CreateDate = DateTime.Now
                        };
                        _db.Endoscopics.Add(endoscopic);

                        Finding finding = new Finding() { PatientID = _patientId, CreateBy = _id, CreateDate = DateTime.Now };
                        _db.Findings.Add(finding);
                        if (_db.SaveChanges() > 0)
                        {
                            endo = _db.Endoscopics.LastOrDefault();
                            _endoscopicId = endo.EndoscopicID;

                            if (procedureId == 6)
                            {
                                idArr.Add(endo.EndoscopicID.ToString());
                                _multiId = string.Join(",", idArr);
                            }
                        }
                    }
                }
                else
                {
                    //if (procedureId == 6)
                    //{
                        //var multiEndo = _db.MultiEndoscopics.Where(w => w.ID == _endoscopicId).FirstOrDefault().EndoscopicID;
                        //idArr = multiEndo.Split(',').ToList();
                        //for (int i = 0; i < idArr.Count; i++)
                        //{
                        //    endo = _db.Endoscopics.Where(x => x.EndoscopicID == int.Parse(idArr[i])).FirstOrDefault();
                        //    endo.IsSaved = true;
                        //    endo.StartRecordDate = _startRec;
                        //    endo.EndRecordDate = _endRec;
                        //}
                    //}
                    //else
                    //{
                        endo = _db.Endoscopics.Where(x => x.EndoscopicID == _endoscopicId).FirstOrDefault();
                        endo.IsSaved = true;
                        endo.StartRecordDate = _startRec;
                        endo.EndRecordDate = _endRec;
                    //}
                }

                //if (procedureId == 6)
                //{
                //    _endoscopicId = SaveMultiEndoscopic(_multiId);
                //}

                UpdateAppointment(_endoscopicId);
                SaveImage(_endoscopicId, procedureId);
                SaveVideo(_endoscopicId, procedureId);
            }
            catch (Exception ex)
            {
                throw ex;
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
                    cropedImg.Save(_pathFolderImageSave + nameCapture, ImageFormat.Jpeg);
                    ImgPath = _pathFolderImageSave + nameCapture;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "File Already exits");
            }

            return ImgPath;
        }

        #endregion

    }
}
