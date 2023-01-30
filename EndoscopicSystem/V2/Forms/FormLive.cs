using AForge.Controls;
using AForge.Video;
using AForge.Video.DirectShow;
using EndoscopicSystem.Constants;
using EndoscopicSystem.Entities;
using EndoscopicSystem.Forms;
using EndoscopicSystem.Repository;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Media;
using System.Threading.Tasks;
using Accord.IO;
using System.Collections.Generic;
using CrystalDecisions.Shared.Json;
using EndoscopicSystem.Report;
using CrystalDecisions.Shared;
using Accord.Video.FFMPEG;
using System.Diagnostics;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;
using System.Drawing.Drawing2D;
using System.Timers;

namespace EndoscopicSystem.V2.Forms
{
    public partial class FormLive : Form
    {
        private readonly int UserID;
        private readonly EndoscopicEntities _db = new EndoscopicEntities();
        private VideoCaptureDeviceForm _captureDeviceForm;
        private FilterInfoCollection _filterInfoCollection;
        private VideoCaptureDevice _videoCaptureDevice;
        private Bitmap video;
        private string _pathFolderVideo = Application.StartupPath.Replace("\\bin\\Debug", "") + @"\VideoRecord\";
        private string _pathFolderImage = Application.StartupPath.Replace("\\bin\\Debug", "") + @"\ImageCapture\";
        private string _pathFolderSounds = Application.StartupPath.Replace("\\bin\\Debug", "") + @"\Sounds\";
        private VideoFileWriter _fileWriter = new VideoFileWriter();
        private DateTime? _firstFrameTime, recordStartDate, recordEndDate;
        private bool _isRecord = false, _isPause = false, _isStopRecord = false;
        private static bool _needSnapshot = false;
        private int _patientId, _endoscopicId, _procedureId = 0, _appointmentId = 0, _item = 0, _aspectRatioID = 1, h, m, s;
        private string PositionCropID = "L", _hnNo = "", _fileName = ".jpg", _pathFolderImageToSave, _vdoPath;
        protected readonly GetDropdownList list = new GetDropdownList();
        public Dictionary<int, string> ImgPath = new Dictionary<int, string>();
        public Form formPopup = new Form(), formPopupVdo = new Form();
        private System.Timers.Timer t;


        public FormLive(int userID, string hn = "", int procId = 0, int endoId = 0, int apId = 0)
        {
            InitializeComponent();
            UserID = userID;
            _hnNo = hn;
            _procedureId = procId;
            _endoscopicId = endoId;
            _appointmentId = apId;
        }

        private void FormLive_Load(object sender, EventArgs e)
        {
            t = new System.Timers.Timer();

            var v = _db.Users.Where(x => x.Id == UserID).Select(x => new { x.AspectRatioID, x.PositionCrop }).FirstOrDefault();
            _aspectRatioID = (int)(v.AspectRatioID.HasValue ? v.AspectRatioID : 1);
            PositionCropID = (string)(v.PositionCrop != "" ? v.PositionCrop : "L");

            EnableConnectionControls(true);

            _filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            cbbProcedureList.ValueMember = "ProcedureID";
            cbbProcedureList.DisplayMember = "ProcedureName";
            cbbProcedureList.DataSource = list.GetProcedureList();
            cbbProcedureList.SelectedIndex = _procedureId;

            if (_filterInfoCollection.Count != 0)
            {
                foreach (FilterInfo device in _filterInfoCollection)
                {
                    devicesCombo.Items.Add(device.Name);
                }
            }
            else
            {
                devicesCombo.Items.Add("No DirectShow devices found");
            }

            if (devicesCombo.Items.Count > 0)
            {
                try
                {
                    _captureDeviceForm = new VideoCaptureDeviceForm();
                    int index = devicesCombo.FindString("Game Capture");
                    devicesCombo.SelectedIndex = index;
                }
                catch (Exception)
                {
                }

                //if (index >= 0 && !string.IsNullOrWhiteSpace(_hnNo))
                //{
                //    try
                //    {
                //        OnLoadVdoCaptureDevice(_hnNo);
                //    }
                //    catch (Exception ex)
                //    {
                //        throw ex;
                //    }
                //}
            }

            txbHN.Text = _hnNo;

            if (!string.IsNullOrWhiteSpace(txbHN.Text) && _procedureId > 0)
            {
                SearchHN(txbHN.Text, _procedureId);
            }
            else
            {
                txbHN.Focus();
            }
        }

        private string GetPathFolderImageToSaved()
        {
            string path = _pathFolderImage + _hnNo + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + cbbProcedureList.Text + @"\" + _appointmentId + @"\";

            var getEndo = _db.EndoscopicAllImages.FirstOrDefault(f => f.EndoscopicID == _endoscopicId);
            if (getEndo != null)
            {
                var arrStr = getEndo.ImagePath.Split(new string[] { "Image_" }, StringSplitOptions.None);
                if (arrStr == null)
                    return path;
                path = arrStr[0];
            }

            return path;
        }

        private void SearchHN(string hn, int procId = 0)
        {
            connectButton.Enabled = true;
            _hnNo = hn;
            _procedureId = procId;
            try
            {
                var getPatient = _db.Patients.FirstOrDefault(x => x.HN == hn && (x.IsActive.HasValue && x.IsActive.Value));
                if (getPatient != null)
                {
                    connectButton.Enabled = true;
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

                    Appointment app = new Appointment();
                    var apps = _db.Appointments.Where(x => x.PatientID == _patientId && txbHN.Text.Equals(x.HN) && x.ProcedureID == _procedureId).ToList();
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
                        txbSymptom.Text = app.Symptom;
                    }

                    cbbProcedureList.SelectedValue = _procedureId;

                    if (_procedureId > 0 && _appointmentId > 0)
                    {
                        btnNext.Visible = true;

                        _pathFolderImageToSave = GetPathFolderImageToSaved();

                        if (!Directory.Exists(_pathFolderImageToSave))
                        {
                            Directory.CreateDirectory(_pathFolderImageToSave);
                        }

                        listView1.Items.Clear();
                        if (!string.IsNullOrWhiteSpace(_pathFolderImageToSave))
                        {
                            var imageList = new List<Image>();
                            DirectoryInfo dinfo = new DirectoryInfo(_pathFolderImageToSave);
                            FileInfo[] files = (FileInfo[])dinfo.GetFiles("*.jpg").Where(w => w.Name.StartsWith("Image")).ToArray();
                            foreach (var item in files.OrderByDescending(o => o.CreationTime))
                            {
                                Image imgFile = Image.FromFile(item.FullName);
                                imageList.Add(imgFile);
                            }

                            _item = files.Count();

                            ImageList images = new ImageList
                            {
                                ImageSize = new Size(180, 110)
                            };

                            foreach (var img in imageList)
                            {
                                images.Images.Add(img);
                            }

                            listView1.LargeImageList = images;

                            for (int i = 0; i < imageList.Count; i++)
                            {
                                ListViewItem item = new ListViewItem($"Image_{imageList.Count - i}", i);
                                listView1.Items.Add(item);
                            }
                        }
                    }
                    else
                    {
                        btnNext.Visible = false;
                    }
                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมูลผู้ป่วย", "Patient not found.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Reset_Controller();
                    connectButton.Enabled = false;
                    txbHN.Focus();
                    btnNext.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Reset_Controller();
                btnNext.Enabled = false;
            }
        }

        private void Reset_Controller()
        {
            this.Controls.ClearControls();
            txbHN.Focus();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_procedureId > 0 && _appointmentId > 0)
            {
                // Stop VdoSourcePlayer
                Disconnect();
                if (videoSourcePlayer == null)
                    return;
                if (videoSourcePlayer.IsRunning)
                {
                    this.videoSourcePlayer.Stop();
                    _fileWriter.Close();
                }

                //this.Hide();

                FormProceed.Self.txbStep.Text = "1" + "," + _pathFolderImageToSave + "," + _vdoPath;

                //this.Close();
            }
            else
            {
                return;
            }
        }

        private void EnableConnectionControls(bool enable)
        {
            devicesCombo.Enabled = enable;
            connectButton.Enabled = enable;
            disconnectButton.Enabled = !enable;
        }

        private void OnLoadVdoCaptureDevice(string hnNo)
        {
            if (string.IsNullOrWhiteSpace(hnNo)) return;

            _videoCaptureDevice = new VideoCaptureDevice(_filterInfoCollection[devicesCombo.SelectedIndex].MonikerString);
            _videoCaptureDevice.VideoResolution = _videoCaptureDevice.VideoCapabilities[0];
            _videoCaptureDevice.ProvideSnapshots = true;
            _videoCaptureDevice.NewFrame += new NewFrameEventHandler(videoSource_NewFrame);
            EnableConnectionControls(false);

            videoSourcePlayer.VideoSource = _videoCaptureDevice;
            videoSourcePlayer.Start();

            SoundPlayer soundPlayer = new SoundPlayer(_pathFolderSounds + @"\SoundCapture\Connect.wav");
            soundPlayer.Play();

            btnCapture.Enabled = true;
            btnCapture.BackColor = Color.FromArgb(0, 192, 0);
            btnRecord.Enabled = true;
            recordStartDate = DateTime.Now;
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            try
            {
                OnLoadVdoCaptureDevice(_hnNo);
            }
            catch (Exception)
            {
                return;
            }
        }

        private void txtHN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txbHN.Text.Length > 0)
                {
                    SearchHN(txbHN.Text);
                }
                else
                {
                    Reset_Controller();
                    connectButton.Enabled = false;
                }
            }
        }

        private void disconnectButton_Click(object sender, EventArgs e)
        {
            btnStop_Click(sender, e);
            Disconnect();
            btnCapture.Enabled = false;
            btnRecord.Enabled = false;
            btnStop.Enabled = false;
            btnPause.Enabled = false;
        }

        private void Disconnect()
        {
            if (videoSourcePlayer.VideoSource != null)
            {
                videoSourcePlayer.SignalToStop();
                videoSourcePlayer.WaitForStop();
                videoSourcePlayer.VideoSource = null;

                if (_videoCaptureDevice.ProvideSnapshots)
                {
                    _videoCaptureDevice.NewFrame -= new NewFrameEventHandler(videoSource_NewFrame);
                }

                EnableConnectionControls(true);
                SoundPlayer soundDisconnect = new SoundPlayer(_pathFolderSounds + @"\SoundCapture\Disconnecte.wav");
                soundDisconnect.Play();

                recordEndDate = DateTime.Now;
            }
        }

        private void CaptureButton_Click(object sender, EventArgs e)
        {
            if ((_videoCaptureDevice != null) && (_videoCaptureDevice.ProvideSnapshots))
            {
                _needSnapshot = true;
            }
        }

        public delegate void CaptureSnapshotManifast(Bitmap image);

        public async void CaptureSnapshot(Bitmap image)
        {
            try
            {
                ++_item;
                SoundPlayer soundCapture = new SoundPlayer();
                string captureSoundPath;
                if (_item > 0 && _item <= 20)
                {
                    captureSoundPath = _pathFolderSounds + $@"\SoundCapture\{_item}.wav";
                    if (File.Exists(captureSoundPath))
                    {
                        soundCapture = new SoundPlayer(captureSoundPath);
                        soundCapture.Play();
                    }
                    else
                    {
                        captureSoundPath = _pathFolderSounds + "capture.wav";
                        soundCapture = new SoundPlayer(captureSoundPath);
                        soundCapture.Play();
                    }
                }
                else
                {
                    captureSoundPath = _pathFolderSounds + "capture.wav";
                    soundCapture = new SoundPlayer(captureSoundPath);
                    soundCapture.Play();
                }

                //soundCapture.Play();
                //soundCapture.Stop();

                _needSnapshot = false;

                string namaImage = "Image";
                string nameCapture = String.Format("{0}_{1}.jpg", namaImage, _item);

                int aspectRatio_X = 0;
                int aspectRatio_Y = 0;
                int width = image.Width;
                int height = image.Height;
                bool isFullScreen = false;

                switch (_aspectRatioID)
                {
                    case 0://Custom
                        var ratio = _db.Users.Where(x => x.Id == UserID).Select(x => new { x.CrpX, x.CrpY, x.CrpWidth, x.CrpHeight }).FirstOrDefault();
                        aspectRatio_X = ratio.CrpX ?? 0;
                        aspectRatio_Y = ratio.CrpY ?? 0;
                        width = ratio.CrpWidth ?? width;
                        height = ratio.CrpHeight ?? height;
                        break;
                    default://FullScreen
                        isFullScreen = true;
                        break;
                }
                _pathFolderImageToSave = _pathFolderImage + _hnNo + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + cbbProcedureList.Text + @"\" + _appointmentId + @"\";
                if (!Directory.Exists(_pathFolderImageToSave))
                {
                    Directory.CreateDirectory(_pathFolderImageToSave);
                }

                pictureBoxSnapshot.Image = await resizeImg(image, aspectRatio_X, aspectRatio_Y, width, height, isFullScreen);
                pictureBoxSnapshot.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBoxSnapshot.Update();

                string path = _pathFolderImageToSave + nameCapture;

                pictureBoxSnapshot.Image.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);

                int i = ImgPath.Count;
                ImgPath.Add(i, path);
                btnCapture.Enabled = true;

                listView1.Items.Clear();
                if (!string.IsNullOrWhiteSpace(_pathFolderImageToSave))
                {
                    var imageList = new List<Image>();
                    DirectoryInfo dinfo = new DirectoryInfo(_pathFolderImageToSave);
                    FileInfo[] files = (FileInfo[])dinfo.GetFiles("*.jpg").Where(w => w.Name.StartsWith("Image")).ToArray();
                    foreach (var item in files.OrderByDescending(o => o.CreationTime))
                    {
                        var imgFile = (Image)Image.FromFile(item.FullName);
                        imageList.Add(imgFile);
                    }
                    _item = files.Count();

                    ImageList images = new ImageList
                    {
                        ImageSize = new Size(180, 110)
                    };

                    foreach (var img in imageList)
                    {
                        images.Images.Add(img);
                    }

                    listView1.LargeImageList = images;

                    for (int j = 0; j < imageList.Count; j++)
                    {
                        //listView1.Items.Add(new ListViewItem($"Image_{_imageList.Count - j}", j));
                        ListViewItem item = new ListViewItem($"Image_{imageList.Count - j}", j);
                        item.Tag = files[j].FullName; // Store the file path in the Tag property
                        listView1.Items.Add(item);
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<Image> resizeImg(Image img, int x, int y, int width, int height, bool isFullScreen)
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

        private void btn_EnabledChanged(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.ForeColor = btn.Enabled == false ? Color.DarkGray : Color.Black;
            btn.BackColor = Color.Gainsboro;

        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            _isPause = !_isPause;
            if (_isPause)
            {
                t.Stop();

                SoundPlayer soundDisconnect = new SoundPlayer(_pathFolderSounds + @"\SoundCapture\Pause.wav");
                soundDisconnect.Play();

                pictureBoxRecording.Visible = false;
                btnPause.Text = "Resume";
            }
            else
            {
                t.Start();

                SoundPlayer soundDisconnect = new SoundPlayer(_pathFolderSounds + @"\SoundCapture\Record.wav");
                soundDisconnect.Play();

                pictureBoxRecording.Visible = true;
                btnPause.Text = "Pause";
            }
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            _captureDeviceForm = new VideoCaptureDeviceForm();

            _isRecord = true;
            _isPause = false;

            SoundPlayer soundDisconnect = new SoundPlayer(_pathFolderSounds + @"\SoundCapture\Record.wav");
            soundDisconnect.Play();

            int h = _videoCaptureDevice.VideoResolution.FrameSize.Height;
            int w = _videoCaptureDevice.VideoResolution.FrameSize.Width;

            string nameImage = "Video"; //HN
            string _pathFolderVideoToSave = _pathFolderVideo + @"\" + _hnNo + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\";
            string nameCapture = _pathFolderVideoToSave + nameImage + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".mp4";

            if (!Directory.Exists(_pathFolderVideoToSave))
            {
                Directory.CreateDirectory(_pathFolderVideoToSave);
            }

            _fileWriter.Flush();
            _fileWriter.Open(nameCapture, w, h, 25, VideoCodec.MPEG4, 2879000);

            try
            {
                _fileWriter.WriteVideoFrame(video);
            }
            catch (Exception)
            {
                //throw;
            }

            _isStopRecord = false;
            _vdoPath = nameCapture;
            btnRecord.Enabled = false;
            btnStop.Enabled = true;
            btnPause.Enabled = true;
            pictureBoxRecording.Visible = true;

            label1.Visible = true;
            lbTime.Visible = true;
            label3.Visible = true;

            t = new System.Timers.Timer();
            t.Enabled = true;
            t.AutoReset = true;
            t.Interval = 1000;
            t.Elapsed += OnTimeEvent;
        }

        private void OnTimeEvent(object sender, ElapsedEventArgs e)
        {
            try
            {
                Invoke(new Action(() =>
                {
                    s++;
                    if (s == 60)
                    {
                        s = 0;
                        m += 1;
                    }
                    if (m == 60)
                    {
                        m = 0;
                        h += 1;
                    }
                    lbTime.Text = String.Format("{0}:{1}:{2}", h.ToString().PadLeft(2, '0'), m.ToString().PadLeft(2, '0'), s.ToString().PadLeft(2, '0'));
                }));
            }
            catch (Exception)
            {
                t.Stop();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            SoundPlayer soundDisconnect = new SoundPlayer(_pathFolderSounds + @"\SoundCapture\Stop.wav");
            soundDisconnect.Play();

            _isStopRecord = true;
            _isRecord = false;
            _isPause = false;
            if (_isStopRecord)
            {
                if (videoSourcePlayer == null)
                { return; }
                if (videoSourcePlayer.IsRunning)
                {
                    _fileWriter.Close();
                }
            }
            else
            {
                this.videoSourcePlayer.Stop();
                _fileWriter.Close();
            }
            btnRecord.Enabled = true;
            btnStop.Enabled = false;
            btnPause.Enabled = false;
            pictureBoxRecording.Visible = false;

            t.Stop();
            h = 0;
            m = 0;
            s = 0;
            lbTime.Text = String.Format("{0}:{1}:{2}", h.ToString().PadLeft(2, '0'), m.ToString().PadLeft(2, '0'), s.ToString().PadLeft(2, '0'));
        }

        public Bitmap CloneBitmap(Bitmap bitmap)
        {
            Image bi;
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            ms.Seek(0, SeekOrigin.Begin);
            bi = Image.FromStream(ms);
            ms.Close();
            return (Bitmap)bi;
        }

        private void FormLive_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
            if (videoSourcePlayer == null)
            { return; }
            if (videoSourcePlayer.IsRunning)
            {
                this.videoSourcePlayer.Stop();
                _fileWriter.Close();
            }
        }

        private void videoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (_isRecord)
            {
                Bitmap videoRecord = null;
                videoRecord = CloneBitmap((Bitmap)eventArgs.Frame.Clone());

                try
                {
                    if (!_isPause)
                    {
                        _fileWriter.WriteVideoFrame(videoRecord);
                    }
                }
                catch (Exception)
                {

                }
            }
            else
            {
                video = CloneBitmap((Bitmap)eventArgs.Frame.Clone());
            }
        }

        private void videoSourcePlayer_NewFrame(object sender, ref Bitmap image)
        {
            if (IsHandleCreated)
            {
                if (_needSnapshot)
                {
                    this.Invoke(new CaptureSnapshotManifast(CaptureSnapshot), image);
                }
            }
            else
            {

            }
        }

    }
}
