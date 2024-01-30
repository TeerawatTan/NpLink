using Accord.Video.FFMPEG;
using AForge.Video;
using AForge.Video.DirectShow;
using EndoscopicSystem.Constants;
using EndoscopicSystem.Entities;
using EndoscopicSystem.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

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
        private string _pathFolderVideo = ConfigurationManager.AppSettings["pathSaveVideo"];
        private string _pathFolderImage = ConfigurationManager.AppSettings["pathSaveImageCapture"];
        private string _pathFolderSounds = Application.StartupPath.Replace("\\bin\\Debug", "") + @"\Sounds\";
        private VideoFileWriter _fileWriter = new VideoFileWriter();
        private DateTime? _recordStartDate, _recordEndDate;
        private bool _isRecord = false, _isPause = false, _isStopRecord = false, _isEgdAndColonoDone = false;
        private static bool _needSnapshot = false;
        private int _patientId, _procedureId = 0, _appointmentId = 0, _item = 0, h, m, s, _settingId, _crpX, _crpY, _crpWidth, _crpHeight;
        private string  _hnNo = "", _pathFolderImageToSave, _vdoPath, _aspectRatio;
        protected readonly GetDropdownList list = new GetDropdownList();
        public Dictionary<int, string> ImgPath = new Dictionary<int, string>();
        public Form formPopup = new Form(), formPopupVdo = new Form();
        private System.Timers.Timer t;
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        public FormLive(int userID, string hn = "", int procId = 0, int endoId = 0, int apId = 0, bool isEgdAndColonoDone = false, int settingId = 0)
        {
            InitializeComponent();
            UserID = userID;
            _hnNo = hn;
            if (procId == 6)
            {
                if (isEgdAndColonoDone)
                {
                    this._procedureId = 2;
                    _isEgdAndColonoDone = true;
                }
                else
                {
                    this._procedureId = 1;
                    _isEgdAndColonoDone = true;
                }
            }
            else
            {
                this._procedureId = procId;
            }
            _appointmentId = apId;

            _hookID = SetHook(_proc);
            _settingId = settingId;
        }

        private delegate void CaptureSnapshotManifast(Bitmap image);

        private void FormLive_Load(object sender, EventArgs e)
        {
            t = new System.Timers.Timer();

            if (_settingId == 0)
            {
                this.Close();
                return;
            }

            var settingCamera = _db.SettingDevices.Where(x => x.ID == _settingId).FirstOrDefault();
            if (settingCamera == null)
            {
                this.Close();
                return;
            }
            _aspectRatio = settingCamera.AspectRatio;
            _crpX = settingCamera.CrpX ?? 0;
            _crpY = settingCamera.CrpY ?? 0;
            _crpWidth = settingCamera.CrpWidth ?? 0;
            _crpHeight = settingCamera.CrpHeight ?? 0;

            EnableConnectionControls(true);

            _filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            cbbProcedureList.ValueMember = "ProcedureID";
            cbbProcedureList.DisplayMember = "ProcedureName";
            cbbProcedureList.DataSource = list.GetProcedureList();
            cbbProcedureList.SelectedValue = _procedureId;

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

            var query = _db.v_GetImageCapturePath.FirstOrDefault(f => f.AppointmentID == _appointmentId);

            string imgPathOrigin = query?.ImagePath;

            if (!string.IsNullOrEmpty(imgPathOrigin))
            {
                var arrStr = imgPathOrigin.Split(new string[] { "Image_" }, StringSplitOptions.None);
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

                    if (apps != null && apps.Count == 0)
                    {
                        this.Close();
                    }
                    else
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

                        if (!string.IsNullOrWhiteSpace(_pathFolderImageToSave))
                        {
                            DirectoryInfo dinfo = new DirectoryInfo(_pathFolderImageToSave);
                            FileInfo[] files = (FileInfo[])dinfo.GetFiles("*.jpg").Where(w => w.Name.StartsWith("Image")).ToArray();
                            var pathOriginImgList = files.OrderByDescending(o => o.CreationTime).Select(s => s.FullName).ToList();

                            GeneratePictureBoxWwithImages(pathOriginImgList);
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
                btnStop_Click(sender, e);
                Disconnect();

                //FormProceed.Self.txbStep.Text = "0" + ",,";
                FormProceed.Self.dtRecordStart.Value = _recordStartDate.HasValue ? _recordStartDate.Value : DateTime.Now;
                FormProceed.Self.dtRecordEnd.Value = _recordEndDate.HasValue ? _recordEndDate.Value : DateTime.Now;

                if (_procedureId == 1 && _isEgdAndColonoDone)
                {
                    FormProceed.Self.txbStep.Text = "4" + "," + _pathFolderImageToSave + "," + _vdoPath;
                    FormProceed.Self.txbCheckHasEgdAndColono.Text = "2";
                }
                else if (_procedureId == 2 && _isEgdAndColonoDone)
                {
                    FormProceed.Self.txbStep.Text = "4" + "," + _pathFolderImageToSave + "," + _vdoPath;
                    FormProceed.Self.txbCheckHasEgdAndColono.Text = "3";
                }
                else
                {
                    FormProceed.Self.txbStep.Text = "1" + "," + _pathFolderImageToSave + "," + _vdoPath;
                }
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

        private void connectButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_hnNo)) return;

                _videoCaptureDevice = new VideoCaptureDevice(_filterInfoCollection[devicesCombo.SelectedIndex].MonikerString);
                _videoCaptureDevice.VideoResolution = _videoCaptureDevice.VideoCapabilities[0];
                _videoCaptureDevice.ProvideSnapshots = true;
                _videoCaptureDevice.NewFrame += new NewFrameEventHandler(videoSource_NewFrame);
                EnableConnectionControls(false);

                videoSourcePlayer.VideoSource = _videoCaptureDevice;
                videoSourcePlayer.Start();

                SoundPlayer _soundConnect = new SoundPlayer
                {
                    SoundLocation = _pathFolderSounds + @"\SoundCapture\Connect.wav"
                };
                _soundConnect.Play();

                btnCapture.Enabled = true;
                btnCapture.BackColor = Color.FromArgb(0, 192, 0);
                btnRecord.Enabled = true;
                _recordStartDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void FormLive_FormClosed(object sender, FormClosedEventArgs e)
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
                EnableConnectionControls(true);

                SoundPlayer _soundDisconnect = new SoundPlayer
                {
                    SoundLocation = _pathFolderSounds + @"\SoundCapture\Disconnecte.wav"
                };
                _soundDisconnect.Play();

                _recordEndDate = DateTime.Now;

                videoSourcePlayer.SignalToStop();
                videoSourcePlayer.WaitForStop();
                videoSourcePlayer.VideoSource = null;

                if (_videoCaptureDevice.ProvideSnapshots)
                {
                    _videoCaptureDevice.NewFrame -= new NewFrameEventHandler(videoSource_NewFrame);
                }
            }
        }

        private void CaptureButton_Click(object sender, EventArgs e)
        {
            if ((_videoCaptureDevice != null) && (_videoCaptureDevice.ProvideSnapshots))
            {
                _needSnapshot = true;
            }
        }

        public async void CaptureSnapshot(Bitmap image)
        {
            try
            {
                _item = _item + 1;
                string captureSoundPath;
                if (_item > 0 && _item <= 20)
                {
                    captureSoundPath = _pathFolderSounds + $@"\SoundCapture\{_item}.wav";
                    if (File.Exists(captureSoundPath))
                    {
                        SoundPlayer _soundCapture = new SoundPlayer();
                        _soundCapture.SoundLocation = captureSoundPath;
                        _soundCapture.Play();
                    }
                    else
                    {
                        SoundPlayer soundCapture = new SoundPlayer();
                        soundCapture.SoundLocation = _pathFolderSounds + "capture.wav";
                        soundCapture.Play();
                    }
                }
                else
                {
                    SoundPlayer soundCapture = new SoundPlayer();
                    soundCapture.SoundLocation = _pathFolderSounds + "capture.wav";
                    soundCapture.Play();
                }

                _needSnapshot = false;

                string namaImage = "Image";
                string nameCapture = String.Format("{0}_{1}.jpg", namaImage, _item);

                int width = image.Width;
                int height = image.Height;
                bool isFullScreen = false;

                if (_aspectRatio == "Full Screen")
                {
                    isFullScreen = true;
                }
                else
                {
                    width = _crpWidth;
                    height = _crpHeight;
                }

                _pathFolderImageToSave = _pathFolderImage + _hnNo + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + cbbProcedureList.Text + @"\" + _appointmentId + @"\";
                
                if (!Directory.Exists(_pathFolderImageToSave))
                {
                    Directory.CreateDirectory(_pathFolderImageToSave);
                }

                pictureBoxSnapshot.Image = await resizeImg(image, _crpX, _crpY, width, height, isFullScreen);
                pictureBoxSnapshot.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBoxSnapshot.Update();

                string path = _pathFolderImageToSave + nameCapture;

                pictureBoxSnapshot.Image.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);

                int idx = ImgPath.Count;
                ImgPath.Add(idx, path);
                btnCapture.Enabled = true;

                DirectoryInfo dinfo = new DirectoryInfo(_pathFolderImageToSave);
                FileInfo[] files = (FileInfo[])dinfo.GetFiles("*.jpg").Where(w => w.Name.StartsWith("Image")).ToArray();
                var pathOriginImgList = files.OrderByDescending(o => o.CreationTime).Select(s => s.FullName).ToList();

                GeneratePictureBoxWwithImages(pathOriginImgList);

            }
            catch (Exception ex)
            {
                throw ex;
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

                SoundPlayer _soundPause = new SoundPlayer();
                _soundPause.SoundLocation = _pathFolderSounds + @"\SoundCapture\Pause.wav";
                _soundPause.Play();

                pictureBoxRecording.Visible = false;
                btnPause.Text = "Resume";
            }
            else
            {
                t.Start();

                SoundPlayer _soundRecord = new SoundPlayer();
                _soundRecord.SoundLocation = _pathFolderSounds + @"\SoundCapture\Record.wav";
                _soundRecord.Play();

                pictureBoxRecording.Visible = true;
                btnPause.Text = "Pause";
            }
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            _captureDeviceForm = new VideoCaptureDeviceForm();

            _isRecord = true;
            _isPause = false;

            SoundPlayer _soundRecord = new SoundPlayer();
            _soundRecord.SoundLocation = _pathFolderSounds + @"\SoundCapture\Record.wav";
            _soundRecord.Play();

            int height = _videoCaptureDevice.VideoResolution.FrameSize.Height;
            int width = _videoCaptureDevice.VideoResolution.FrameSize.Width;

            string nameImage = "Video"; //HN
            string _pathFolderVideoToSave = _pathFolderVideo + @"\" + _hnNo + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\";
            string nameCapture = _pathFolderVideoToSave + nameImage + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".mp4";

            if (!Directory.Exists(_pathFolderVideoToSave))
            {
                Directory.CreateDirectory(_pathFolderVideoToSave);
            }

            _fileWriter.Flush();
            _fileWriter.Open(nameCapture, width, height, 60, VideoCodec.MPEG4, 2879000);

            try
            {
                _fileWriter.WriteVideoFrame(video);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
            h = 0;
            m = 0;
            s = 0;
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
            if (videoSourcePlayer.VideoSource != null)
            {
                SoundPlayer _soundStopRecord = new SoundPlayer();
                _soundStopRecord.SoundLocation = _pathFolderSounds + @"\SoundCapture\Stop.wav";
                _soundStopRecord.Play();
            }

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
            //h = 0;
            //m = 0;
            //s = 0;
            lbTime.Text = String.Format("{0}:{1}:{2}", h.ToString().PadLeft(2, '0'), m.ToString().PadLeft(2, '0'), s.ToString().PadLeft(2, '0'));
        }

        public Bitmap CloneBitmap(Bitmap bitmap)
        {
            //Image bi;
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            ms.Seek(0, SeekOrigin.Begin);
            //bi = Image.FromStream(ms);
            ms.Close();
            return bitmap;
        }

        private void FormLive_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // base.OnFormClosing(e);
                UnhookWindowsHookEx(_hookID);

                Disconnect();
                if (videoSourcePlayer == null)
                { return; }
                if (videoSourcePlayer.IsRunning)
                {
                    this.videoSourcePlayer.Stop();
                    _fileWriter.Close();
                }
            }
            catch (Exception)
            {

            }
        }

        private void videoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (_isRecord)
            {
                try
                {
                    Bitmap videoRecord = CloneBitmap((Bitmap)eventArgs.Frame.Clone());

                    if (!_isPause)
                    {
                        if (videoRecord != null)
                        {
                            _fileWriter.WriteVideoFrame(videoRecord);
                        }
                    }
                    else
                    {
                        _fileWriter.Close();
                    }
                }
                catch (Exception)
                {
                    //throw new Exception(ex.Message);
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

        private void GeneratePictureBoxWwithImages(List<string> img)
        {
            Panel panel = this.panel1;
            panel.Controls.Clear();

            // Calculate the maximum width and height for each PictureBox, and the spacing between them
            int numCols = 3;
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
                pictureBox.ImageLocation = img[i];

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

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                Keys key = (Keys)Marshal.ReadInt32(lParam);
                //Console.WriteLine("Key pressed: " + key.ToString());

                FormLive form = Application.OpenForms.OfType<FormLive>().FirstOrDefault();
                if (key == Keys.F1)
                {
                    form?.Invoke(new Action(() => form.CaptureButton_Click(null, EventArgs.Empty)));
                }
                else if (key == Keys.F2)
                {
                    form?.Invoke(new Action(() => form.btnRecord_Click(null, EventArgs.Empty)));
                }
                else if (key == Keys.F3)
                {
                    form?.Invoke(new Action(() => form.btnPause_Click(null, EventArgs.Empty)));
                }
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

    }
}
