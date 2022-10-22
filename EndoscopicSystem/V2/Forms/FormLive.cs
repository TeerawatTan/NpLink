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
        private VideoCaptureDeviceForm _captureDeviceForm;
        private FilterInfoCollection _filterInfoCollection;
        private VideoCaptureDevice _videoCaptureDevice;
        private Bitmap video;
        private string _pathFolderVideo = Application.StartupPath.Replace("\\bin\\Debug", "") + @"\VideoRecord\";
        private string _pathFolderImage = Application.StartupPath.Replace("\\bin\\Debug", "") + @"\ImageCapture\";
        private string _pathFolderSounds = Application.StartupPath.Replace("\\bin\\Debug", "") + @"\Sounds\";
        private string _pathFolderPDF = Application.StartupPath.Replace("\\bin\\Debug", "") + @"\Pdf\";
        private string _pathFolderDicom = Application.StartupPath.Replace("\\bin\\Debug", "") + @"\Dicom\";
        string reportPath = Application.StartupPath.Replace("\\bin\\Debug", "") + @"\Report\";
        private string _initialDirectoryUpload = "C://Desktop";
        private string _titleUpload = "Select image to be upload.";
        private string _filterUpload = "Image Only(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
        private VideoFileWriter _fileWriter = new VideoFileWriter();
        private DateTime? _firstFrameTime;
        private bool _isRecord = false;
        private bool _isPause = false;
        private bool _isStopRecord = false;
        private static bool _needSnapshot = false;
        readonly EndoscopicEntities db = new EndoscopicEntities();
        private int patientId;
        private int AspectRatioID = 1;
        private string PositionCropID = "L";
        private string hnNo = "";
        protected readonly GetDropdownList list = new GetDropdownList();
        public Dictionary<int, string> ImgPath = new Dictionary<int, string>();
        public string VdoPath = "";
        public Form formPopup = new Form();
        public Form formPopupVdo = new Form();
        private DateTime? recordStartDate;
        private DateTime? recordEndDate;
        private int endoscopicId;
        private int? procedureId;
        private int appointmentId = 0;
        private int item = 0;
        private System.Timers.Timer t;
        private int h, m, s;
        private string _pathFolderImageToSave;

        public FormLive(int userID, string hn = "", int procId = 0, int endoId = 0, int apId = 0)
        {
            InitializeComponent();
            UserID = userID;
            hnNo = hn;
            procedureId = procId;
            endoscopicId = endoId;
            appointmentId = apId;
        }

        private void FormLive_Load(object sender, EventArgs e)
        {
            t = new System.Timers.Timer();

            var v = db.Users.Where(x => x.Id == UserID).Select(x => new { x.AspectRatioID, x.PositionCrop }).FirstOrDefault();
            AspectRatioID = (int)(v.AspectRatioID.HasValue ? v.AspectRatioID : 1);
            PositionCropID = (string)(v.PositionCrop != "" ? v.PositionCrop : "L");

            EnableConnectionControls(true);
            //LoadDataHN();
            _filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            cbbProcedureList.ValueMember = "ProcedureID";
            cbbProcedureList.DisplayMember = "ProcedureName";
            cbbProcedureList.DataSource = list.GetProcedureList();
            cbbProcedureList.SelectedIndex = 0;

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
            _captureDeviceForm = new VideoCaptureDeviceForm();
            int index = devicesCombo.FindString("Game Capture");
            devicesCombo.SelectedIndex = index;
            if (index > 0)
            {
                this.Load += connectButton_Click;
            }

            listView1.Items.Clear();
            if (!string.IsNullOrWhiteSpace(_pathFolderImageToSave))
            {
                DirectoryInfo dinfo = new DirectoryInfo(_pathFolderImageToSave);
                FileInfo[] files = dinfo.GetFiles("*.jpg");
                foreach (var item in files.OrderByDescending(o => o.CreationTime).ToList())
                {
                    ListViewItem listView = new ListViewItem();
                    listView.Text = item.Name;
                    listView1.Items.Add(listView);
                }
            }
        }
        
        private void btnNext_Click(object sender, EventArgs e)
        {
            FormPreviewReport formPreviewReport = new FormPreviewReport();
            formPreviewReport.ShowDialog();
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
                _videoCaptureDevice = new VideoCaptureDevice(_filterInfoCollection[devicesCombo.SelectedIndex].MonikerString);
                _videoCaptureDevice.VideoResolution = _videoCaptureDevice.VideoCapabilities[0];
                _videoCaptureDevice.ProvideSnapshots = true;
                _videoCaptureDevice.NewFrame += new NewFrameEventHandler(videoSource_NewFrame);
                EnableConnectionControls(false);

                videoSourcePlayer.VideoSource = _videoCaptureDevice;
                videoSourcePlayer.Start();

                SoundPlayer soundPlayer = new SoundPlayer(_pathFolderSounds + "connect.wav");
                soundPlayer.Play();

                btnCapture.Enabled = true;
                btnRecord.Enabled = true;
                recordStartDate = DateTime.Now;
            }
            catch (Exception)
            {
                return;
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
                SoundPlayer soundDisconnect = new SoundPlayer(_pathFolderSounds + "disconnect.wav");
                soundDisconnect.Play();

                recordEndDate = DateTime.Now;
            }
        }
        
        private void CaptureButton_Click(object sender, EventArgs e)
        {
            if ((_videoCaptureDevice != null) && (_videoCaptureDevice.ProvideSnapshots))
            {
                _needSnapshot = true;

                SoundPlayer soundCapture = new SoundPlayer(_pathFolderSounds + "capture.wav");
                soundCapture.Play();
            }
        }
        
        public delegate void CaptureSnapshotManifast(Bitmap image);
        
        public async void CaptureSnapshot(Bitmap image)
        {
            try
            {
                _needSnapshot = false;
                ++item;
                string namaImage = "Image";
                string nameCapture = String.Format("{0}_{1}.jpg", namaImage, item);

                int aspectRatio_X = 0;
                int aspectRatio_Y = 0;
                int width = image.Width;
                int height = image.Height;
                bool isFullScreen = false;

                switch (AspectRatioID)
                {
                    case 0://Custom
                        var ratio = db.Users.Where(x => x.Id == UserID).Select(x => new { x.CrpX, x.CrpY, x.CrpWidth, x.CrpHeight }).FirstOrDefault();
                        aspectRatio_X = ratio.CrpX ?? 0;
                        aspectRatio_Y = ratio.CrpY ?? 0;
                        width = ratio.CrpWidth ?? width;
                        height = ratio.CrpHeight ?? height;
                        break;
                    default://FullScreen
                        isFullScreen = true;
                        break;
                }
                _pathFolderImageToSave = _pathFolderImage + hnNo + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + cbbProcedureList.Text + @"\" + appointmentId + @"\";
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
                    DirectoryInfo dinfo = new DirectoryInfo(_pathFolderImageToSave);
                    FileInfo[] files = dinfo.GetFiles("*.jpg");
                    foreach (var item in files.OrderByDescending(o => o.CreationTime).ToList())
                    {
                        ListViewItem listView = new ListViewItem();
                        listView.Text = item.Name;
                        listView1.Items.Add(listView);
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

                pictureBoxRecording.Visible = false;
                btnPause.Text = "Resume";
            }
            else
            {
                t.Start();

                pictureBoxRecording.Visible = true;
                btnPause.Text = "Pause";
            }
        }
        
        private void btnRecord_Click(object sender, EventArgs e)
        {
            _captureDeviceForm = new VideoCaptureDeviceForm();

            _isRecord = true;
            _isPause = false;

            int h = _videoCaptureDevice.VideoResolution.FrameSize.Height;
            int w = _videoCaptureDevice.VideoResolution.FrameSize.Width;

            string nameImage = "Video"; //HN
            string _pathFolderVideoToSave = _pathFolderVideo + @"\" + hnNo + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\";
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
            VdoPath = nameCapture;
            btnRecord.Enabled = false;
            btnStop.Enabled = true;
            btnPause.Enabled = true;
            pictureBoxRecording.Visible = true;

            label1.Visible = true;
            lbTime.Visible = true;
            label3.Visible = true;

            t.Interval = 10;
            t.Elapsed += OnTimeEvent;
        }

        private void OnTimeEvent(object sender, ElapsedEventArgs e)
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
                if (m == 5)
                {
                    t.Stop();
                }
                lbTime.Text = String.Format("{0}:{1}", h.ToString().PadLeft(2, '0'), m.ToString().PadLeft(2, '0'));
            }));
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
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
        }

        public Bitmap CloneBitmap(Bitmap bitmap)
        {
            Image bi;
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            ms.Seek(0, SeekOrigin.Begin);
            bi = Image.FromStream(ms);
            ms.Close();
            return (Bitmap)bi;
        }

        private void FormLive_FormClosing(object sender, FormClosingEventArgs e)
        {
            t.Stop();
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
