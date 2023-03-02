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

namespace EndoscopicSystem
{
    public partial class EndoscopicForm : Form
    {
        private readonly int UserID;
        private VideoCaptureDeviceForm _captureDeviceForm;
        private FilterInfoCollection _filterInfoCollection;
        private VideoCaptureDevice _videoCaptureDevice;
        private Bitmap video;
        private string _pathFolderVideo = ConfigurationManager.AppSettings["pathSaveVideo"];
        private string _pathFolderImage = ConfigurationManager.AppSettings["pathSaveImageCapture"];
        private string _pathFolderSounds = Application.StartupPath.Replace("\\bin\\Debug", "") + @"\Sounds\";
        private string _pathFolderPDF = ConfigurationManager.AppSettings["pathSavePdf"];
        private string _pathFolderDicom = ConfigurationManager.AppSettings["pathSaveDicom"];
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

        public EndoscopicForm(int userID, string hn = "", int procId = 0, int endoId = 0, int apId = 0)
        {
            InitializeComponent();
            UserID = userID;
            hnNo = hn;
            procedureId = procId;
            endoscopicId = endoId;
            appointmentId = apId;
        }

        private void EndoscopicForm_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            btnSave.Enabled = false;
            var v = db.Users.Where(x => x.Id == UserID).Select(x => new { x.AspectRatioID, x.PositionCrop }).FirstOrDefault();
            AspectRatioID = (int)(v.AspectRatioID.HasValue ? v.AspectRatioID : 1);
            PositionCropID = (string)(v.PositionCrop != "" ? v.PositionCrop : "L");

            //LoadDataHN();
            _filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            cbbProcedureList.ValueMember = "ProcedureID";
            cbbProcedureList.DisplayMember = "ProcedureName";
            cbbProcedureList.DataSource = list.GetProcedureList();
            cbbProcedureList.SelectedIndex = 0;
            ChangeDropdownList(cbbProcedureList.SelectedValue.ToString());

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

            EnableConnectionControls(true);
            txtHN.Focus();
            txtHN.Text = hnNo;
            connectButton.Enabled = false;
            btnReport.Hide();

            #region Set PictureBox

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

            #endregion


            if (!string.IsNullOrWhiteSpace(txtHN.Text) && procedureId > 0)
            {
                SearchHN(txtHN.Text, procedureId.Value);
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
                _videoCaptureDevice = new VideoCaptureDevice(_filterInfoCollection[devicesCombo.SelectedIndex].MonikerString);
                _videoCaptureDevice.VideoResolution = _videoCaptureDevice.VideoCapabilities[0];
                _videoCaptureDevice.ProvideSnapshots = true;
                //_videoCaptureDevice.SnapshotFrame += new NewFrameEventHandler(videoSource_NewFrame);
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
                    //_videoCaptureDevice.SnapshotFrame -= new NewFrameEventHandler(videoSource_NewFrame);
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
                    pictureBoxSaved18
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
                    btnEditPic18
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
                    btnDeletePictureBoxSaved18
                };
                _needSnapshot = false;

                string namaImage = "Image"; //HN
                string nameCapture = namaImage + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".png";

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
                string _pathFolderImageToSave = _pathFolderImage + hnNo + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + cbbProcedureList.Text + @"\" + appointmentId + @"\";
                if (!Directory.Exists(_pathFolderImageToSave))
                {
                    Directory.CreateDirectory(_pathFolderImageToSave);
                }

                pictureBoxSnapshot.Image = await resizeImg(image, aspectRatio_X, aspectRatio_Y, width, height, isFullScreen);
                pictureBoxSnapshot.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBoxSnapshot.Update();

                string path = _pathFolderImageToSave + nameCapture;

                pictureBoxSnapshot.Image.Save(path, System.Drawing.Imaging.ImageFormat.Png);

                int i = ImgPath.Count;
                ImgPath.Add(i, path);
                btnCapture.Enabled = true;
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

        //Image cropImg(Image img, int aspectRatioX, int aspectRatioY, int width)
        //{
        //    // 4:3 Aspect Ratio. You can also add it as parameters
        //    //double aspectRatio_X = aspectRatioX;
        //    //double aspectRatio_Y = aspectRatioY;

        //    double imgWidth = Convert.ToDouble(img.Width);
        //    double imgHeight = Convert.ToDouble(img.Height);

        //    if (imgWidth / imgHeight > (aspectRatioX / aspectRatioY))
        //    {
        //        double extraWidth = imgWidth - (imgHeight * (aspectRatioX / aspectRatioY));
        //        //double cropStartFrom = extraWidth / 2;
        //        double cropStartFrom = 0;

        //        switch (PositionCropID)
        //        {
        //            case "R"://Right
        //                cropStartFrom = imgWidth - (imgWidth - extraWidth);
        //                break;
        //            case "C"://Center
        //                cropStartFrom = (imgWidth - (imgWidth - extraWidth)) / 2;
        //                break;
        //            default://Lelt
        //                cropStartFrom = 0;
        //                break;
        //        }

        //        Bitmap bmp = new Bitmap((int)(img.Width - extraWidth), img.Height);
        //        Graphics grp = Graphics.FromImage(bmp);
        //        //grp.DrawImage(img, new Rectangle(0, 0, (int)(img.Width - extraWidth), img.Height), new Rectangle((int)cropStartFrom, 0, (int)(imgWidth - extraWidth), img.Height), GraphicsUnit.Pixel);

        //        Rectangle rect = new Rectangle(aspectRatioX, aspectRatioY, width, img.Height);
        //        grp.DrawImage(img, 0, 0, rect, GraphicsUnit.Pixel);

        //        return (Image)bmp;
        //    }
        //    else
        //        return img;
        //}
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
        private void videoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {

            if (_isRecord)
            {
                Bitmap videoRecord = null;
                videoRecord = CloneBitmap((Bitmap)eventArgs.Frame.Clone());

                //using (var video = CloneBitmap((Bitmap)eventArgs.Frame.Clone()))
                //{
                //    //if (_firstFrameTime != null)
                //    //{
                //    //    // pictureBoxSaved2.Image = (Bitmap)eventArgs.Frame.Clone();
                //    //    //AVIwriter.Quality = 0;
                //    //    _fileWriter.WriteVideoFrame(video, DateTime.Now - _firstFrameTime.Value);
                //    //    //AVIwriter.AddFrame(video);
                //    //}
                //    //else
                //    //{
                //    //    _fileWriter.WriteVideoFrame(video);
                //    //    _firstFrameTime = DateTime.Now;
                //    //}

                try
                {
                    //TimeSpan startTimeSpan = new TimeSpan();
                    //var currentTime = DateTime.Now.TimeOfDay;
                    //TimeSpan timeSpan = currentTime - startTimeSpan;
                    if (!_isPause)
                    {
                        _fileWriter.WriteVideoFrame(videoRecord);
                    }
                    //else
                    //{
                    //    _fileWriter.();
                    //}
                    //}
                }
                catch (Exception ex)
                {
                    //throw;
                }
            }
            else
            {
                video = CloneBitmap((Bitmap)eventArgs.Frame.Clone());
                // pictureBoxSaved2.Image = (Bitmap)eventArgs.Frame.Clone();
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
        private void btnRecord_Click(object sender, EventArgs e)
        {
            _captureDeviceForm = new VideoCaptureDeviceForm();

            //if (_captureDeviceForm.ShowDialog(this) == DialogResult.OK)
            //{
            _isRecord = true;
            _isPause = false;

            //int h = _captureDeviceForm.VideoDevice.VideoResolution.FrameSize.Height;
            //int w = _captureDeviceForm.VideoDevice.VideoResolution.FrameSize.Width;

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

            //long currentTick = DateTime.Now.Ticks;
            //StartTick = StartTick ?? currentTick;
            //var frameOffset = new TimeSpan(currentTick - StartTick.Value);

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
            //}
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
            btnAllVideo.Enabled = true;
            btnRecord.Enabled = true;
            btnStop.Enabled = false;
            btnPause.Enabled = false;
            pictureBoxRecording.Visible = false;
            //MessageBox.Show("Stoped record");
        }
        private void btnPauseVdo_Click(object sender, EventArgs e)
        {
            if (videoSourcePlayer.IsRunning)
            {
                videoSourcePlayer.WaitForStop();
            }
        }
        private void EndoscopicForm_FormClosing(object sender, FormClosingEventArgs e)
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (patientId > 0)
                {
                    int procedureItem = (int)cbbProcedureList.SelectedValue;
                    UpdateEndoscopic(procedureItem);
                    ExportEndoscopic(hnNo, procedureId.Value, endoscopicId);
                    MessageBox.Show(Constant.STATUS_SUCCESS);
                    btnSave.Enabled = false;
                    btnReport.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Saved not success.");
            }
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
                rprt.Load(reportPath + "GastroscoryReport.rpt");
            }
            else if (proc == 2)
            {
                rprt.Load(reportPath + "ColonoscopyReport.rpt");
            }
            else if (proc == 3)
            {
                rprt.Load(reportPath + "EndoscopicReport.rpt");
            }
            else if (proc == 4)
            {
                rprt.Load(reportPath + "BronchoscopyReport.rpt");
            }
            else if (proc == 5)
            {
                rprt.Load(reportPath + "EntReport.rpt");
            }
            else
            {
                return;
            }

            CrTables = rprt.Database.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
            {
                crtableLogoninfo = CrTable.LogOnInfo;
                crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                CrTable.ApplyLogOnInfo(crtableLogoninfo);
            }
            rprt.SetParameterValue("@hn", hn);
            rprt.SetParameterValue("@procedure", proc);
            rprt.SetParameterValue("@endoscopicId", endosId);

            string _pathFolderPDFToSave = _pathFolderPDF + hnNo + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\";
            if (!Directory.Exists(_pathFolderPDFToSave))
            {
                Directory.CreateDirectory(_pathFolderPDFToSave);
            }
            string fileNamePDF = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string namaPDF = "pdf"; //HN
            string nameSave = namaPDF + "_" + fileNamePDF + ".pdf";
            string path = _pathFolderPDFToSave + nameSave;
            rprt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, path);

            string _pathFolderDicomSave = _pathFolderDicom + hnNo + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\";
            if (!Directory.Exists(_pathFolderDicomSave))
            {
                Directory.CreateDirectory(_pathFolderDicomSave);
            }
            string namaDicom = "dicom"; //HN
            string nameDicomSave = namaDicom + "_" + fileNamePDF + ".dcm";
            string pathDicom = _pathFolderDicomSave + nameDicomSave;
            rprt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pathDicom);
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset_Controller();
        }
        private void Reset_Controller()
        {
            this.Controls.ClearControls();
            RemoveTabPage();
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
                    btnEditPic18
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
                    btnDeletePictureBoxSaved18
                };
            for (int i = 0; i < buttonedit.Length; i++)
            {
                buttonedit[i].Visible = false;
                buttondel[i].Visible = false;
            }
            txtHN.Focus();
            btnReport.Hide();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete this item ??", "Confirm Delete!!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (patientId > 0)
                {
                    var endoscopic = db.Endoscopics
                        .Where(x => x.PatientID == patientId && x.EndoscopicID == endoscopicId && x.ProcedureID == procedureId).FirstOrDefault();
                    if (endoscopic != null)
                    {
                        endoscopic.IsSaved = false;
                        endoscopic.UpdateBy = UserID;
                        endoscopic.UpdateDate = DateTime.Now;

                        var hist = db.Histories.Where(s => s.EndoscopicID == endoscopicId && s.PatientID == patientId && s.ProcedureID == procedureId).FirstOrDefault();
                        if (hist != null)
                        {
                            hist.IsActive = false;
                        }

                        var app = db.Appointments.FirstOrDefault(x => x.HN == txtHN.Text && x.ProcedureID == procedureId);
                        if (app != null)
                        {
                            db.Appointments.Remove(app);
                        }

                        var endoLog = db.Endoscopic_Log.Where(x => x.PatientID == patientId && x.EndoscopicID == endoscopicId && x.ProcedureID == procedureId).OrderByDescending(o => o.CreateDate).FirstOrDefault();
                        if (endoLog != null)
                        {
                            endoLog.IsActive = false;
                        }
                        db.SaveChanges();
                        MessageBox.Show("Delete success.");
                        Reset_Controller();
                    }
                }
            }
        }
        private void txtHN_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtHN.Text.Length > 0)
                {
                    SearchHN(txtHN.Text);
                }
                else
                {
                    Reset_Controller();
                    connectButton.Enabled = false;
                    btnSave.Enabled = false;
                }
            }
        }
        private void SearchHN(string hn, int procId = 0)
        {
            connectButton.Enabled = true;
            hnNo = hn;
            procedureId = procId;
            try
            {
                var getPatient = db.Patients.FirstOrDefault(x => x.HN == hn && (x.IsActive.HasValue && x.IsActive.Value));
                if (getPatient != null)
                {
                    connectButton.Enabled = true;
                    patientId = getPatient.PatientID;
                    txtHN.Text = getPatient.HN;
                    txtFullName.Text = getPatient.Fullname;
                    txtAge.Text = getPatient.Age.HasValue ? getPatient.Age.ToString() : "";

                    if (procedureId == 0)
                    {
                        procedureId = getPatient.ProcedureID;
                    }

                    Appointment app = new Appointment();
                    var apps = db.Appointments.Where(x => x.PatientID == patientId && txtHN.Text.Equals(x.HN) && x.ProcedureID == procedureId).ToList();
                    if (apps != null)
                    {
                        if (appointmentId > 0)
                        {
                            apps = apps.Where(w => w.AppointmentID == appointmentId).ToList();
                            app = apps.FirstOrDefault();
                        }
                        else
                        {
                            app = apps.OrderByDescending(o => o.AppointmentDate).FirstOrDefault();
                            appointmentId = app.AppointmentID;
                        }
                        txtSymptom.Text = app.Symptom;
                        chkNewCase.Checked = app.IsNewCase ?? false;
                        chkFollowUpCase.Checked = app.IsFollowCase ?? false;
                    }

                    cbbProcedureList.SelectedValue = procedureId;
                    if (procedureId == 4)
                    {
                        cbbEndoscopist_0.SelectedValue = getPatient.DoctorID ?? 0;
                        cbbNurse1_0.SelectedValue = getPatient.NurseFirstID ?? 0;
                        cbbNurse2_0.SelectedValue = getPatient.NurseSecondID ?? 0;
                        cbbNurse3_0.SelectedValue = getPatient.NurseThirthID ?? 0;
                    }
                    else
                    {
                        cbbEndoscopist_1.SelectedValue = getPatient.DoctorID ?? 0;
                        cbbNurse1_1.SelectedValue = getPatient.NurseFirstID ?? 0;
                        cbbNurse2_1.SelectedValue = getPatient.NurseSecondID ?? 0;
                        cbbNurse3_1.SelectedValue = getPatient.NurseThirthID ?? 0;
                    }

                    if (app.EndoscopicCheck.HasValue && app.EndoscopicCheck.Value || endoscopicId > 0)
                    {
                        var getEndos = db.Endoscopics.Where(e => e.ProcedureID == procedureId.Value && e.PatientID == patientId && e.IsSaved).ToList();
                        if (getEndos.Count > 0)
                        {
                            getEndos = (from e in getEndos
                                        join p in db.Patients on e.PatientID equals p.PatientID
                                        select e).ToList();
                            if (endoscopicId > 0)
                            {
                                getEndos = getEndos.Where(x => x.EndoscopicID == endoscopicId).ToList();
                            }

                            if (getEndos.Count > 0)
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
                                PushEndoscopicData(procedureId, getPatient, getEndo, getFinding, getIndication, getSpecimen, getIntervention);
                            }
                            else
                            {
                                PushEndoscopicData(procedureId, getPatient);
                                Endoscopic endoscopic = new Endoscopic() { PatientID = patientId, IsSaved = false, ProcedureID = procedureId, CreateBy = UserID, CreateDate = System.DateTime.Now };
                                db.Endoscopics.Add(endoscopic);

                                Finding finding = new Finding() { PatientID = patientId, CreateBy = UserID, CreateDate = System.DateTime.Now };
                                db.Findings.Add(finding);
                                db.SaveChanges();

                                var endos = db.Endoscopics.ToList();
                                if (endos.Count > 0)
                                {
                                    Endoscopic endo = endos.OrderByDescending(x => x.EndoscopicID).FirstOrDefault();
                                    endoscopicId = endo.EndoscopicID;
                                }
                            }
                        }
                    }

                    btnSave.Enabled = true;
                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมูลผู้ป่วย");
                    Reset_Controller();
                    connectButton.Enabled = false;
                    txtHN.Focus();
                    btnSave.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Reset_Controller();
                btnSave.Enabled = false;
            }
        }
        private void ProcedureList_Changed(object sender, EventArgs e)
        {
            if (procedureId == 0)
            {
                procedureId = (int)cbbProcedureList.SelectedValue;
            }
            ChangeDropdownList(cbbProcedureList.SelectedValue.ToString());
        }
        private void ChangeDropdownList(string id = "")
        {
            RemoveTabPage();

            if ("1".Equals(id)) // EGD
            {
                tabControl1.TabPages.Add(tabGeneralColonoscopy);
                tabControl1.TabPages.Add(tabFindingGastroscopy);
                tabControl1.TabPages.Add(tabSpecimenGastroscopy);
                DropdownEndoscopist(cbbEndoscopist_1);
                DropdownMedication(cbbGeneralMedication_1);
                DropdownIndication(cbbGeneralIndication_1);
                DropdownNurse(cbbNurse1_1);
                DropdownNurse(cbbNurse2_1);
                DropdownNurse(cbbNurse3_1);
                DropdownOropharynx();
                DropdownEsophagus(cbbFindingEsophagus_3);
                DropdownEGJunction();
                DropdownCardia();
                DropdownFundus();
                DropdownBody();
                DropdownAntrum();
                DropdownPylorus();
                DropdownDuodenalBulb();
                DropdownSecondPart();
            }
            else if ("2".Equals(id)) // Colonoscopy
            {
                tabControl1.TabPages.Add(tabGeneralColonoscopy);
                tabControl1.TabPages.Add(tabFindingColonoscopy);
                tabControl1.TabPages.Add(tabSpecimenColonoscopy);
                DropdownEndoscopist(cbbEndoscopist_1);
                DropdownMedication(cbbGeneralMedication_1);
                DropdownIndication(cbbGeneralIndication_1);
                DropdownNurse(cbbNurse1_1);
                DropdownNurse(cbbNurse2_1);
                DropdownNurse(cbbNurse3_1);
                DropdownAnalCanal();
                DropdownRectum();
                DropdownSigmoidColon();
                DropdownDescendingColon();
                DropdownSplenicFlexure();
                DropdownTransverseColon();
                DropdownHepaticFlexure();
                DropdownAscendingColon();
                DropdownIleocecalValve();
                DropdownCecum();
                DropdownTerminalIleum();
            }
            else if ("3".Equals(id)) //  ERCP
            {
                tabControl1.TabPages.Add(tabGeneralColonoscopy);
                tabControl1.TabPages.Add(tabFindingERCP);
                tabControl1.TabPages.Add(tabInterventionERCP);
                DropdownEndoscopist(cbbEndoscopist_1);
                DropdownMedication(cbbGeneralMedication_1);
                DropdownIndication(cbbGeneralIndication_1);
                DropdownNurse(cbbNurse1_1);
                DropdownNurse(cbbNurse2_1);
                DropdownNurse(cbbNurse3_1);
                DropdownEsophagus(cbbFindingEsophagus_2);
                DropdownStomach(cbbFindingStomach_2);
                DropdownDuodenum();
                DropdownAmpulla();
                DropdownCholangiogram();
                DropdownPancreatogram();
            }
            else if ("4".Equals(id)) // Bronchoscopy 
            {
                tabControl1.TabPages.Add(tabGeneralBronchoscopy);
                tabControl1.TabPages.Add(tabFindingBronchoscopy);
                tabControl1.TabPages.Add(tabIndicationBronchoscopy);
                tabControl1.TabPages.Add(tabSpecimenBronchoscopy);
                DropdownEndoscopist(cbbEndoscopist_0);
                DropdownMedication(cbbGeneralMedication_0);
                DropdownNurse(cbbNurse1_0);
                DropdownNurse(cbbNurse2_0);
                DropdownNurse(cbbNurse3_0);
                DropdownVocalCord();
                DropdownTrachea();
                DropdownCarina();
                DropdownRightMain();
                DropdownRightIntermideate();
                DropdownRUL();
                DropdownRML();
                DropdownRLL();
                DropdownLeftMain();
                DropdownLUL();
                DropdownLingular();
                DropdownLLL();
            }
            else if ("5".Equals(id)) // ENT
            {
                tabControl1.TabPages.Add(tabGeneralColonoscopy);
                tabControl1.TabPages.Add(tabFindingENT);
                tabControl1.TabPages.Add(tabFindingENT2);
                DropdownEndoscopist(cbbEndoscopist_1);
                DropdownMedication(cbbGeneralMedication_1);
                DropdownIndication(cbbGeneralIndication_1);
                DropdownNurse(cbbNurse1_1);
                DropdownNurse(cbbNurse2_1);
                DropdownNurse(cbbNurse3_1);
                DropdownNasalCavityLeft();
                DropdownNasalCavityRight();
                DropdownSeptum();
                DropdownNasopharynx();
                DropdownRoof();
                DropdownPosteriorWall();
                DropdownRosenmullerFossa();
                DropdownEustachianTubeOrifice(cbbFiETOrificeL);
                DropdownEustachianTubeOrifice(cbbFiETOrificeR);
                DropdownSoftPalate();
                DropdownUvula();
                DropdownTonsil();
                DropdownBaseOfTongue();
                DropdownVallecula();
                DropdownPyriformSinusLeft();
                DropdownPyriformSinusRight();
                DropdownPostcricoid();
                DropdownPosteriorPharyngealWall();
                DropdownSupraglottic();
                DropdownGlottic();
                DropdownSubglottic();
                DropdownUES();
                DropdownEsophagus(cbbFiEsophagus);
                DropdownLES();
                DropdownStomach(cbbFiStomach);
            }
            else
            {
                RemoveTabPage();
            }
        }
        private void RemoveTabPage()
        {
            tabControl1.TabPages.Clear();
        }
        private void txtOther_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtIndicationOther0_0.TextLength > 0)
                cbOther_0.Checked = true;
            else
                cbOther_0.Checked = false;
        }
        private void txtOther_1_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtOther0_1.TextLength > 0)
                cbOther_1.Checked = true;
            else
                cbOther_1.Checked = false;
        }
        private void txtOther_2_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtOther0_2.TextLength > 0)
                cbOther_2.Checked = true;
            else
                cbOther_2.Checked = false;
        }
        private void txtOther_3_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtOther0_3.TextLength > 0)
                cbOther_3.Checked = true;
            else
                cbOther_3.Checked = false;
        }
        private void EndoscopicForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                CaptureButton_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnRecord_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F3)
            {
                btnPause_Click(sender, e);
            }
        }


        #region Save And Update Table.
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
                string filename = System.IO.Path.GetFileName(openFileDialog1.FileName);
                string filenameExtension = System.IO.Path.GetExtension(filename);
                if (filename == null)
                {
                    MessageBox.Show("Please select a valid image.");
                }
                else
                {
                    int aspectRatio_X = 0;
                    int aspectRatio_Y = 0;
                    int width = img.Width;
                    int height = img.Height;
                    bool isFullScreen = false;

                    string namaImage = "image"; //HN
                    string nameCapture = namaImage + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".png";

                    switch (AspectRatioID)
                    {
                        case 0://Custom
                            var ratio = db.Users.Where(x => x.Id == UserID).Select(x => new { x.CrpX, x.CrpY, x.CrpWidth, x.CrpHeight }).FirstOrDefault();
                            aspectRatio_X = ratio.CrpX ?? 0;
                            aspectRatio_Y = ratio.CrpY ?? 0;
                            width = ratio.CrpWidth == null ? width : img.Width - ratio.CrpWidth.Value;
                            height = img.Height - (ratio.CrpHeight ?? height);
                            break;
                        default://FullScreen
                            isFullScreen = true;
                            break;

                    }
                    string _pathFolderImageToSave = _pathFolderImage + hnNo + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + cbbProcedureList.Text + @"\" + appointmentId + @"\";
                    if (!Directory.Exists(_pathFolderImageToSave))
                    {
                        Directory.CreateDirectory(_pathFolderImageToSave);
                    }

                    cropedImg = await resizeImg(img, aspectRatio_X, aspectRatio_Y, width, height, isFullScreen);
                    cropedImg.Save(_pathFolderImageToSave + nameCapture, System.Drawing.Imaging.ImageFormat.Png);
                    ImgPath = _pathFolderImageToSave + nameCapture;
                    pictureBoxSnapshot.ImageLocation = ImgPath;
                    pictureBoxSnapshot.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBoxSnapshot.Update();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "File Already exits");
            }

            return ImgPath;
        }
        private void UpdateEndoscopic(int dataId)
        {
            Endoscopic endo = new Endoscopic();
            if (endoscopicId == 0)
            {
                Endoscopic endoscopic = new Endoscopic() { PatientID = patientId, IsSaved = false, ProcedureID = procedureId, CreateBy = UserID, CreateDate = System.DateTime.Now };
                db.Endoscopics.Add(endoscopic);

                Finding finding = new Finding() { PatientID = patientId, CreateBy = UserID, CreateDate = System.DateTime.Now };
                db.Findings.Add(finding);
                db.SaveChanges();

                var endos = db.Endoscopics.ToList();
                endo = endos.LastOrDefault();
                endoscopicId = endo.EndoscopicID;
            }
            else
            {
                endo = db.Endoscopics.Where(x => x.EndoscopicID == endoscopicId).FirstOrDefault();
            }
            UpdateDataAll(dataId, endo);

        }
        private void UpdateDataAll(int dataId, Endoscopic endoscopic)
        {
            try
            {
                var findingData = db.Findings.Where(x => x.PatientID == patientId).OrderByDescending(o => o.FindingID).FirstOrDefault();
                endoscopic.FindingID = findingData.FindingID;
                endoscopic.IsSaved = true;
                endoscopic.ProcedureID = dataId;
                endoscopic.Diagnosis = txtDiagnosis.Text;
                endoscopic.Complication = txtComplication.Text;
                endoscopic.Comment = txtComment.Text;
                if (dataId == 1 || dataId == 2 || dataId == 3 || dataId == 5)
                {
                    endoscopic.EndoscopistID = (int?)cbbEndoscopist_1.SelectedValue;
                    endoscopic.Arrive = dtArriveTime_1.Value;
                    endoscopic.Instrument = txtGeneralInstrument_1.Text;
                    endoscopic.MedicationID = (int?)cbbGeneralMedication_1.SelectedValue;
                    endoscopic.Anesthesia = txtGeneralAnesthesia_1.Text;
                    endoscopic.Indication = (int?)cbbGeneralIndication_1.SelectedValue;
                    endoscopic.NurseFirstID = (int?)cbbNurse1_1.SelectedValue;
                    endoscopic.NurseSecondID = (int?)cbbNurse2_1.SelectedValue;
                    endoscopic.NurseThirthID = (int?)cbbNurse3_1.SelectedValue;
                    endoscopic.AnesNurse = txtAnesNurse_1.Text;
                    endoscopic.MedicationOther = txtGeneralMedication_1.Text;
                    endoscopic.IndicationOther = txtGeneralIndication_1.Text;
                    if (dataId == 3) endoscopic.InterventionID = SaveIntervention();
                }
                else
                {
                    endoscopic.EndoscopistID = (int?)cbbEndoscopist_0.SelectedValue;
                    endoscopic.Arrive = dtArriveTime_0.Value;
                    endoscopic.Instrument = txtGeneralInstrument_0.Text;
                    endoscopic.MedicationID = (int?)cbbGeneralMedication_0.SelectedValue;
                    endoscopic.NurseFirstID = (int?)cbbNurse1_0.SelectedValue;
                    endoscopic.NurseSecondID = (int?)cbbNurse2_0.SelectedValue;
                    endoscopic.NurseThirthID = (int?)cbbNurse3_0.SelectedValue;
                    endoscopic.Anesthesia = txtGeneralAnesthesia_0.Text;
                    endoscopic.AnesNurse = txtAnesNurse_0.Text;
                    endoscopic.MedicationOther = txtGeneralMedication_0.Text;
                    endoscopic.IndicationID = SaveIndication();
                }
                endoscopic.SpecimenID = SaveSpecimen(dataId);
                endoscopic.StartRecordDate = recordStartDate;
                endoscopic.EndRecordDate = recordEndDate;
                endoscopic.UpdateDate = System.DateTime.Now;
                endoscopic.UpdateBy = UserID;

                var patient = db.Patients.Where(x => x.PatientID == patientId).FirstOrDefault();
                if (patient != null)
                {
                    patient.Fullname = txtFullName.Text;
                    patient.Age = Convert.ToInt32(txtAge.Text);
                    patient.ProcedureID = dataId;
                    patient.UpdateBy = UserID;
                    patient.UpdateDate = DateTime.Now;
                    if (dataId == 1 || dataId == 2 || dataId == 3)
                    {
                        patient.DoctorID = (int?)cbbEndoscopist_1.SelectedValue;
                        patient.NurseFirstID = (int?)cbbNurse1_1.SelectedValue;
                        patient.NurseSecondID = (int?)cbbNurse2_1.SelectedValue;
                        patient.NurseThirthID = (int?)cbbNurse3_1.SelectedValue;
                    }
                    else
                    {
                        patient.DoctorID = (int?)cbbEndoscopist_0.SelectedValue;
                        patient.NurseFirstID = (int?)cbbNurse1_0.SelectedValue;
                        patient.NurseSecondID = (int?)cbbNurse2_0.SelectedValue;
                        patient.NurseThirthID = (int?)cbbNurse3_0.SelectedValue;
                    }
                }

                UpdateFinding(dataId);
                UpdateAppointment(endoscopicId);
                SaveImage(endoscopic.EndoscopicID, dataId);
                SaveAllImage(endoscopic.EndoscopicID, dataId);
                SaveVideo(endoscopic.EndoscopicID, dataId);
                SaveLogEndoscopic(endoscopic);
                SaveLogHistory(dataId, patient.DoctorID, endoscopic.EndoscopicID);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SaveVideo(int endoscopicID, int procedureID)
        {
            if (VdoPath != null)
            {
                var endoVideos = db.EndoscopicVideos.Where(x => x.EndoscopicID == endoscopicID && x.ProcedureID == procedureID).FirstOrDefault();
                if (endoVideos != null)
                {
                    endoVideos.VideoPath = VdoPath.ToString();
                    endoVideos.UpdateBy = UserID;
                    endoVideos.UpdateDate = DateTime.Now;
                }
                else
                {
                    EndoscopicVideo endoscopicVideo = new EndoscopicVideo();
                    endoscopicVideo.EndoscopicID = endoscopicID;
                    endoscopicVideo.ProcedureID = procedureID;
                    endoscopicVideo.VideoPath = VdoPath.ToString();
                    endoscopicVideo.CreateBy = UserID;
                    endoscopicVideo.CreateDate = DateTime.Now;
                    endoscopicVideo.UpdateBy = UserID;
                    endoscopicVideo.UpdateDate = DateTime.Now;
                    db.EndoscopicVideos.Add(endoscopicVideo);
                }
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
                    pictureBoxSaved18
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
                    txtPictureBoxSaved18
                };
            int i = 0;
            int seq = 1;
            foreach (var item in texts)
            {
                String Imgpath = boxes[i].ImageLocation != null ? boxes[i].ImageLocation.ToString() : "";
                var endoImgs = db.EndoscopicImages.Where(x => x.EndoscopicID == endoscopicID && x.ProcedureID == procedureID && x.Seq == seq).FirstOrDefault();
                if (endoImgs != null)
                {
                    if (Imgpath == "")
                    {
                        db.EndoscopicImages.Remove(endoImgs);
                    }
                    else
                    {
                        endoImgs.ImagePath = Imgpath;
                        endoImgs.ImageComment = item.Text;
                        endoImgs.Seq = i + 1;
                        endoImgs.UpdateBy = UserID;
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
                    endoscopicImage.CreateBy = UserID;
                    endoscopicImage.CreateDate = DateTime.Now;
                    endoscopicImage.UpdateBy = UserID;
                    endoscopicImage.UpdateDate = DateTime.Now;
                    db.EndoscopicImages.Add(endoscopicImage);
                }
                i++;
                seq++;
            }
        }
        private void SaveAllImage(int endoscopicID, int procedureID)
        {
            int i = 0;
            int seq = 1;
            foreach (var item in ImgPath.Values)
            {
                var endoAllImgs = db.EndoscopicAllImages.Where(x => x.EndoscopicID == endoscopicID && x.ProcedureID == procedureID && x.Seq == seq).FirstOrDefault();
                if (item != null)
                {
                    if (endoAllImgs != null)
                    {
                        endoAllImgs.ImagePath = item.ToString();
                        endoAllImgs.UpdateBy = UserID;
                        endoAllImgs.UpdateDate = DateTime.Now;
                    }
                    else
                    {
                        EndoscopicAllImage endoscopicAllImage = new EndoscopicAllImage();
                        endoscopicAllImage.EndoscopicID = endoscopicID;
                        endoscopicAllImage.ProcedureID = procedureID;
                        endoscopicAllImage.ImagePath = item.ToString();
                        endoscopicAllImage.Seq = i + 1;
                        endoscopicAllImage.CreateBy = UserID;
                        endoscopicAllImage.CreateDate = DateTime.Now;
                        endoscopicAllImage.UpdateBy = UserID;
                        endoscopicAllImage.UpdateDate = DateTime.Now;
                        db.EndoscopicAllImages.Add(endoscopicAllImage);
                    }
                    i++;
                    seq++;
                }
                else
                {

                }
            }
        }
        private void UpdateFinding(int dataId)
        {
            var findingData = db.Findings.Where(x => x.PatientID == patientId).OrderByDescending(o => o.FindingID);
            if (findingData != null)
            {
                Finding finding = findingData.FirstOrDefault();
                if (dataId == 1)
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
                }
                else if (dataId == 2)
                {
                    finding.AnalCanalID = (int?)cbbFindingAnalCanal_1.SelectedValue;
                    finding.RectumID = (int?)cbbFindingRectum_1.SelectedValue;
                    finding.SigmoidColonID = (int?)cbbFindingSigmoid_1.SelectedValue;
                    finding.DescendingColonID = (int?)cbbFindingDescending_1.SelectedValue;
                    finding.SplenicFlexureID = (int?)cbbFindingFlexure_1.SelectedValue;
                    finding.TransverseColonID = (int?)cbbFindingTransverse_1.SelectedValue;
                    finding.HepaticFlexureID = (int?)cbbFindingHepatic_1.SelectedValue;
                    finding.AscendingColonID = (int?)cbbFindingAscending_1.SelectedValue;
                    finding.IleocecalVolveID = (int?)cbbFindingIleocecal_1.SelectedValue;
                    finding.CecumID = (int?)cbbFindingCecum_1.SelectedValue;
                    finding.TerminalIleumID = (int?)cbbFindingTerminal_1.SelectedValue;
                    finding.AnalCanal = txtFindingAnalCanal_1.Text;
                    finding.Rectum = txtFindingRectum_1.Text;
                    finding.SigmoidColon = txtFindingSigmoid_1.Text;
                    finding.DescendingColon = txtFindingDescending_1.Text;
                    finding.SplenicFlexure = txtFindingFlexure_1.Text;
                    finding.TransverseColon = txtFindingTransverse_1.Text;
                    finding.HepaticFlexure = txtFindingHepatic_1.Text;
                    finding.AscendingColon = txtFindingAscending_1.Text;
                    finding.IleocecalVolve = txtFindingIleocecal_1.Text;
                    finding.Cecum = txtFindingCecum_1.Text;
                    finding.TerminalIleum = txtFindingTerminal_1.Text;
                }
                else if (dataId == 3)
                {
                    finding.EsophagusID = (int?)cbbFindingEsophagus_2.SelectedValue;
                    finding.StomachID = (int?)cbbFindingStomach_2.SelectedValue;
                    finding.DuodenumID = (int?)cbbFindingDuodenum_2.SelectedValue;
                    finding.AmpullaOfVaterID = (int?)cbbFindingAmpulla_2.SelectedValue;
                    finding.CholangiogramID = (int?)cbbFindingCholangiogram_2.SelectedValue;
                    finding.PancreatogramID = (int?)cbbFindingPancreatogram_2.SelectedValue;
                    finding.Esophagus = txtFindingEsophagus_2.Text;
                    finding.Stomach = txtFindingStomach_2.Text;
                    finding.Duodenum = txtFindingDuodenum_2.Text;
                    finding.AmpullaOfVater = txtFindingAmpulla_2.Text;
                    finding.Cholangiogram = txtFindingCholangiogram_2.Text;
                    finding.Pancreatogram = txtFindingPancreatogram_2.Text;
                }
                else if (dataId == 4)
                {
                    finding.VocalCordID = (int?)cbbFindingVocal_0.SelectedValue;
                    finding.TracheaID = (int?)cbbFindingTrachea_0.SelectedValue;
                    finding.CarinaID = (int?)cbbFindingCarina_0.SelectedValue;
                    finding.RightMainID = (int?)cbbFindingRightMain_0.SelectedValue;
                    finding.RightIntermideateID = (int?)cbbFindingRightIntermideate_0.SelectedValue;
                    finding.RULID = (int?)cbbFindingRUL_0.SelectedValue;
                    finding.RMLID = (int?)cbbFindingRML_0.SelectedValue;
                    finding.RLLID = (int?)cbbFindingRLL_0.SelectedValue;
                    finding.LeftMainID = (int?)cbbFindingLeftMain_0.SelectedValue;
                    finding.LULID = (int?)cbbFindingLUL_0.SelectedValue;
                    finding.LingularID = (int?)cbbFindingLingular_0.SelectedValue;
                    finding.LLLID = (int?)cbbFindingLLL_0.SelectedValue;
                    finding.VocalCord = txtFindingVocalCord_0.Text;
                    finding.Trachea = txtFindingTrachea_0.Text;
                    finding.Carina = txtFindingCarina_0.Text;
                    finding.RightMain = txtFindingRightMain_0.Text;
                    finding.RightIntermideate = txtFindingIntermideate_0.Text;
                    finding.RUL = txtFindingRUL_0.Text;
                    finding.RML = txtFindingRML_0.Text;
                    finding.RLL = txtFindingRLL_0.Text;
                    finding.LeftMain = txtFindingLeftMain_0.Text;
                    finding.LUL = txtFindingLUL_0.Text;
                    finding.Lingular = txtFindingLingular_0.Text;
                    finding.LLL = txtFindingLLL_0.Text;
                }
                else
                {
                    finding.NasalCavityLeftID = (int?)cbbFiNCL.SelectedValue;
                    finding.NasalCavityRightID = (int?)cbbFiNCR.SelectedValue;
                    finding.SeptumID = (int?)cbbFiSeptum.SelectedValue;
                    finding.NasopharynxID = (int?)cbbFiNasopharynx.SelectedValue;
                    finding.RoofID = (int?)cbbFiRoof.SelectedValue;
                    finding.PosteriorWallID = (int?)cbbFiPosteriorWall.SelectedValue;
                    finding.RosenmullerFossaID = (int?)cbbFiRosenmullerFossa.SelectedValue;
                    finding.EustachianTubeOrificeLeftID = (int?)cbbFiETOrificeL.SelectedValue;
                    finding.EustachianTubeOrificeRightID = (int?)cbbFiETOrificeR.SelectedValue;
                    finding.SoftPalateID = (int?)cbbFiSoftPalate.SelectedValue;
                    finding.UvulaID = (int?)cbbFiUvula.SelectedValue;
                    finding.TonsilID = (int?)cbbFiTonsil.SelectedValue;
                    finding.BaseOfTongueID = (int?)cbbFiBaseOfTongue.SelectedValue;
                    finding.ValleculaID = (int?)cbbFiVallecula.SelectedValue;
                    finding.PyriformSinusLeftID = (int?)cbbFiPyrSinusL.SelectedValue;
                    finding.PyriformSinusRightID = (int?)cbbFiPyrSinusR.SelectedValue;
                    finding.PostcricoidID = (int?)cbbFiPostcricoid.SelectedValue;
                    finding.PosteriorPharyngealWallID = (int?)cbbFiPosPhaWall.SelectedValue;
                    finding.SupraglotticID = (int?)cbbFiSupraglottic.SelectedValue;
                    finding.GlotticID = (int?)cbbFiGlottic.SelectedValue;
                    finding.SubglotticID = (int?)cbbFiSubglottic.SelectedValue;
                    finding.UESID = (int?)cbbFiUES.SelectedValue;
                    finding.EsophagusID = (int?)cbbFiEsophagus.SelectedValue;
                    finding.LESID = (int?)cbbFiLES.SelectedValue;
                    finding.StomachID = (int?)cbbFiStomach.SelectedValue;
                    finding.NasalCavityLeft = txbFiNCL.Text;
                    finding.NasalCavityRight = txbFiNCR.Text;
                    finding.Septum = txbFiSeptum.Text;
                    finding.Nasopharynx = txbFiNasopharynx.Text;
                    finding.Roof = txbFiRoof.Text;
                    finding.PosteriorWall = txbFiPosteriorWall.Text;
                    finding.RosenmullerFossa = txbFiRosenmullerFossa.Text;
                    finding.EustachianTubeOrificeLeft = txbFiETOrificeL.Text;
                    finding.EustachianTubeOrificeRight = txbFiETOrificeR.Text;
                    finding.SoftPalate = txbFiSoftPalate.Text;
                    finding.Uvula = txbFiUvula.Text;
                    finding.Tonsil = txbFiTonsil.Text;
                    finding.BaseOfTongue = txbFiBaseOfTongue.Text;
                    finding.Vallecula = txbFiVallecula.Text;
                    finding.PyriformSinusLeft = txbFiPyrSinusL.Text;
                    finding.PyriformSinusRight = txbFiPyrSinusR.Text;
                    finding.Postcricoid = txbFiPostcricoid.Text;
                    finding.PosteriorPharyngealWall = txbFiPosPhaWall.Text;
                    finding.Supraglottic = txbFiSupraglottic.Text;
                    finding.Glottic = txbFiGlottic.Text;
                    finding.Subglottic = txbFiSubglottic.Text;
                    finding.UES = txbFiUES.Text;
                    finding.Esophagus = txbFiEsophagus.Text;
                    finding.LES = txbFiLES.Text;
                    finding.Stomach = txbFiStomach.Text;
                }
                finding.UpdateDate = System.DateTime.Now;
                finding.UpdateBy = UserID;

                db.SaveChanges();
            }
        }
        private int? SaveIndication()
        {
            Indication indication = new Indication();
            indication.EvaluateLesion_Infiltration = cbInfiltration_0.Checked;
            indication.AsscessAirwayPatency = cbPatency_0.Checked;
            indication.Hemoptysis = cbHemoptysis_0.Checked;
            indication.Therapeutic = cbTherapeutic_0.Checked;
            indication.OtherDetail1 = txtIndicationOther0_0.Text;
            indication.OtherDetail2 = txtIndicationOther1_0.Text;
            indication.OtherDetail3 = txtIndicationOther2_0.Text;
            indication.OtherDetail4 = txtIndicationOther3_0.Text;
            indication.OtherDetail5 = txtIndicationOther4_0.Text;
            indication.CreateBy = UserID;
            indication.CreateDate = System.DateTime.Now;
            indication.UpdateBy = UserID;
            indication.UpdateDate = System.DateTime.Now;
            db.Indications.Add(indication);
            db.SaveChanges();
            var data = db.Indications.ToList();
            if (data.Count > 0)
            {
                Indication indi = data.OrderByDescending(x => x.IndicationID).FirstOrDefault();
                return indi.IndicationID;
            }
            return null;
        }
        private int? SaveSpecimen(int dataId)
        {
            Speciman speciman = new Speciman();
            if (dataId == 1)
            {
                speciman.BiopsyforCLOTest = cbCLOTest_3.Checked;
                speciman.Positive = cbPositive.Checked;
                speciman.Nagative = cbNagative.Checked;
                speciman.BiopsyforPathological = cbPathologocal_3.Checked;
                speciman.OtherDetail1 = txtOther0_3.Text;
                speciman.OtherDetail2 = txtOther1_3.Text;
                speciman.OtherDetail3 = txtOther2_3.Text;
                speciman.OtherDetail4 = txtOther3_3.Text;
                speciman.OtherDetail5 = txtOther4_3.Text;
            }
            else if (dataId == 2)
            {
                speciman.BiopsyforPathological = cbPathological.Checked;
                speciman.OtherDetail1 = txtOther0_1.Text;
                speciman.OtherDetail2 = txtOther1_1.Text;
                speciman.OtherDetail3 = txtOther2_1.Text;
                speciman.OtherDetail4 = txtOther3_1.Text;
                speciman.OtherDetail5 = txtOther4_1.Text;
            }
            else if (dataId == 4)
            {
                speciman.BalAt = cbBalAt_0.Checked;
                speciman.BalAtDetail = txtBalAt_0.Text;
                speciman.BalAtCytho = cbBalAt_Cytho.Checked;
                speciman.BalAtPatho = cbBalAt_Patho.Checked;
                speciman.BalAtGram = cbBalAt_Gram.Checked;
                speciman.BalAtAFB = cbBalAt_AFB.Checked;
                speciman.BalAtModAFB = cbBalAt_Mod.Checked;
                speciman.BrushingAt = cbBrushingAt_0.Checked;
                speciman.BrushingAtDetail = txtBrushing_0.Text;
                speciman.BrushingAtCytho = cbBrushing_Cytho.Checked;
                speciman.BrushingAtPatho = cbBrushing_Patho.Checked;
                speciman.BrushingAtGram = cbBrushing_Gram.Checked;
                speciman.BrushingAtAFB = cbBrushing_AFB.Checked;
                speciman.BrushingAtModAFB = cbBrushing_Mod.Checked;
                speciman.EndoproncialBiopsyAt = cbEndoproncial_0.Checked;
                speciman.EndoproncialBiopsyAtDetail = txtEndoproncial_0.Text;
                speciman.EndoproncialBiopsyAtCytho = cbEndoproncial_Cytho.Checked;
                speciman.EndoproncialBiopsyAtPatho = cbEndoproncial_Patho.Checked;
                speciman.EndoproncialBiopsyAtGram = cbEndoproncial_Gram.Checked;
                speciman.EndoproncialBiopsyAtAFB = cbEndoproncial_AFB.Checked;
                speciman.EndoproncialBiopsyAtModAFB = cbEndoproncial_Mod.Checked;
                speciman.TransbroncialBiopsyAt = cbTransbroncial_0.Checked;
                speciman.TransbroncialBiopsyAtDetail = txtTransbroncial_0.Text;
                speciman.TransbroncialBiopsyAtCytho = cbTransbroncial_Cytho.Checked;
                speciman.TransbroncialBiopsyAtPatho = cbTransbroncial_Patho.Checked;
                speciman.TransbroncialBiopsyAtGram = cbTransbroncial_Gram.Checked;
                speciman.TransbroncialBiopsyAtAFB = cbTransbroncial_AFB.Checked;
                speciman.TransbroncialBiopsyAtModAFB = cbTransbroncial_Mod.Checked;
                speciman.Transbroncial = cbTransbroncialNeedle_0.Checked;
                speciman.TransbroncialDetail = txtTransbroncialNeedle_0.Text;
                speciman.TransbroncialCytho = cbTransbroncialNeedle_Cytho.Checked;
                speciman.TransbroncialPatho = cbTransbroncialNeedle_Patho.Checked;
                speciman.TransbroncialGram = cbTransbroncialNeedle_Gram.Checked;
                speciman.TransbroncialAFB = cbTransbroncialNeedle_AFB.Checked;
                speciman.TransbroncialModAFB = cbTransbroncialNeedle_Mod.Checked;
            }
            else
            {
                speciman.BiopsyforPathological = cbFiBiopsy_Ent.Checked;
            }
            db.Specimen.Add(speciman);
            db.SaveChanges();
            var data = db.Specimen.ToList();
            if (data.Count > 0)
            {
                Speciman spec = data.OrderByDescending(x => x.SpecimenID).FirstOrDefault();
                return spec.SpecimenID;
            }
            return null;
        }
        private int? SaveIntervention()
        {
            Intervention intervention = new Intervention();
            intervention.Spincterotomy = cbSpincterotomy_2.Checked;
            intervention.StoneExtraction = cbStoneExtraction_2.Checked;
            intervention.StentInsertion = cbStentInsertion_2.Checked;
            intervention.IsPlastic = cbPlastic_2.Checked;
            intervention.PlasticFoot = txtPlasticFt.Text;
            intervention.PlasticCentimeter = txtPlasticCm.Text;
            intervention.IsMetal = cbMetal_2.Checked;
            intervention.MetalFoot = txtMetalFt.Text;
            intervention.MetalCentimeter = txtMetalCm.Text;
            intervention.Hemonstasis = cbHemonstasis_2.Checked;
            intervention.Adrenaline = cbAdrenaline_2.Checked;
            intervention.Coagulation = cbCoagulation_2.Checked;
            intervention.Specimens = cbSpecimens_2.Checked;
            intervention.BiopsyforPathological = cbPathological_2.Checked;
            intervention.OtherDetail1 = txtOther0_2.Text;
            intervention.OtherDetail2 = txtOther1_2.Text;
            intervention.OtherDetail3 = txtOther2_2.Text;
            intervention.OtherDetail4 = txtOther3_2.Text;
            intervention.OtherDetail5 = txtOther4_2.Text;

            db.Interventions.Add(intervention);
            db.SaveChanges();
            var data = db.Interventions.ToList();
            if (data.Count > 0)
            {
                Intervention inter = data.OrderByDescending(x => x.InterventionID).FirstOrDefault();
                return inter.InterventionID;
            }
            return null;
        }
        private void UpdateAppointment(int? endoId)
        {
            Appointment data = new Appointment();
            var datas = db.Appointments.Where(x => x.PatientID == patientId && txtHN.Text.Equals(x.HN) && x.ProcedureID == procedureId).ToList();
            if (datas != null && datas.Count > 0)
            {
                if (appointmentId > 0)
                {
                    datas = datas.Where(w => w.AppointmentID == appointmentId).ToList();
                    data = datas.FirstOrDefault();
                }
                else
                {
                    data = datas.OrderByDescending(o => o.AppointmentDate).FirstOrDefault();
                }
                data.Symptom = txtSymptom.Text;
                data.IsNewCase = chkNewCase.Checked;
                data.IsFollowCase = chkFollowUpCase.Checked;
                data.EndoscopicCheck = true;
                data.UpdateBy = UserID;
                data.UpdateDate = DateTime.Now;
                data.EndoscopicID = endoId;
                db.SaveChanges();
            }
        }
        private void SaveLogHistory(int procedureId, int? doctorID, int logEndoId = 0)
        {
            History history = new History();
            history.PatientID = patientId;
            history.EndoscopicID = logEndoId;
            history.ProcedureID = procedureId;
            history.DoctorID = doctorID;
            history.Symptom = txtSymptom.Text;
            history.CreateDate = DateTime.Now;
            history.CreateBy = UserID;
            history.IsActive = true;
            db.Histories.Add(history);
            db.SaveChanges();
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
        public void EditPictureCaptureOnClick(System.Windows.Forms.PictureBox pictureBox)
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
            //System.Diagnostics.ProcessStartInfo procInfo = new System.Diagnostics.ProcessStartInfo();
            //procInfo.FileName = ("mspaint.exe");
            //procInfo.Arguments = pictureBoxSaved1.ImageLocation;
            //procInfo.UseShellExecute = true;
            //System.Diagnostics.Process.Start(procInfo);
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
        public async void SavePictureBoxOnClick(int num, System.Windows.Forms.PictureBox pictureBox, Button btnEdit, Button btnDelete)
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


        private void btnAllPhoto_Click(object sender, EventArgs e)
        {
            try
            {
                string pathImgHn = _pathFolderImage + hnNo + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + cbbProcedureList.Text + @"\" + appointmentId;
                OpenFolder(pathImgHn);
            }
            catch (Exception)
            {

            }

        }
        private void OpenFolder(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    Arguments = folderPath,
                    FileName = "explorer.exe"
                };
                Process.Start(startInfo);
            }
            else
            {
                MessageBox.Show(string.Format("{0} Directory does not exist!", folderPath));
            }
        }
        private void btnDeleteDynamic_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            for (int i = 0; i < ImgPath.Count; i++)
            {
                int num = i + 1;
                if (btn.Name == ("btnDeletePictureBoxSaved" + num))
                {
                    System.Windows.Forms.PictureBox pictureBoxSaved = (System.Windows.Forms.PictureBox)formPopup.Controls.Find("pictureBoxSaved" + num, true)[0];
                    Button butnDelete = (Button)formPopup.Controls.Find("btnDeletePictureBoxSaved" + num, true)[0];
                    Button btnEditPic = (Button)formPopup.Controls.Find("btnEditPic" + num, true)[0];
                    pictureBoxSaved.Image = null;
                    pictureBoxSaved.Update();
                    butnDelete.Visible = false;
                    btnEditPic.Visible = false;
                    ImgPath.Remove(i);
                    break;
                }
            }
            if (ImgPath.Count < 30) btnCapture.Enabled = true;
            else btnCapture.Enabled = false;
        }
        private void btnEditPicDynamic_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            for (int i = 0; i < ImgPath.Count; i++)
            {
                int num = i + 1;
                if (btn.Name == ("btnEditPic" + num))
                {
                    //System.Diagnostics.ProcessStartInfo procInfo = new System.Diagnostics.ProcessStartInfo();
                    //procInfo.FileName = ("mspaint.exe");
                    //procInfo.Arguments = ImgPath[i];
                    //System.Diagnostics.Process.Start(procInfo);

                    string image = ImgPath[i].Replace("[0, ", "").Replace("[1, ", "").Replace("[2, ", "").Replace("[3, ", "").Replace("[4, ", "").Replace("[5, ", "").Replace("[6, ", "").Replace("[7, ", "").Replace("[8, ", "").Replace("[9, ", "").TrimEnd(']');

                    using (System.Diagnostics.Process ExternalProcess = new System.Diagnostics.Process())
                    {
                        ExternalProcess.StartInfo.FileName = ("mspaint.exe");
                        string imgLo = string.Concat("\"", image, "\"");
                        ExternalProcess.StartInfo.Arguments = imgLo;
                        ExternalProcess.StartInfo.UseShellExecute = true;
                        ExternalProcess.Start();
                        ExternalProcess.WaitForExit();
                    }
                    pictureBoxRefresh();
                    break;
                }
            }
        }
        private void btnAllVideo_Click(object sender, EventArgs e)
        {
            try
            {
                using (System.Diagnostics.Process ExternalProcess = new System.Diagnostics.Process())
                {
                    ExternalProcess.StartInfo.FileName = ("wmplayer.exe");
                    string imgLo = string.Concat("\"", VdoPath, "\"");
                    ExternalProcess.StartInfo.Arguments = imgLo;
                    ExternalProcess.Start();
                }
            }
            catch (Exception)
            {

            }
        }
        private void BtnReport_Click(object sender, EventArgs e)
        {
            ReportEndoscopic reportForm = new ReportEndoscopic(hnNo, procedureId.Value, endoscopicId);
            reportForm.Show();
        }
        private void PictureBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("FileDrop", false)) e.Effect = DragDropEffects.Copy;
            else e.Effect = DragDropEffects.None;
        }
        private void PictureBox_DragDrop(object sender, DragEventArgs e)
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
                    pictureBoxSaved18
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
                    btnEditPic18
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
                    btnDeletePictureBoxSaved18
                };


            System.Windows.Forms.PictureBox pb = (System.Windows.Forms.PictureBox)sender;
            string[] paths = (string[])(e.Data.GetData("FileDrop", false));
            foreach (string path in paths)
            {
                pb.ImageLocation = path;
            }
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            pb.Update();
            for (int i = 0; i < boxes.Length; i++)
            {
                if (boxes[i].Image != null)
                {
                    buttondel[i].Visible = true;
                    buttonedit[i].Visible = true;
                }
            }
        }
        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.PictureBox pb = (System.Windows.Forms.PictureBox)sender;
            pb.Select();
            pb.DoDragDrop(pb.ImageLocation, DragDropEffects.Copy);
        }
        private void PushEndoscopicData(
            int? id,
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

            if (id == 1 || id == 2 || id == 3 || id == 5) // EGD, ColonoScopy, ERCP, ENT
            {
                // General
                cbbEndoscopist_1.SelectedValue = patient.DoctorID ?? 0;
                cbbNurse1_1.SelectedValue = patient.NurseFirstID ?? 0;
                cbbNurse2_1.SelectedValue = patient.NurseSecondID ?? 0;
                cbbNurse3_1.SelectedValue = patient.NurseThirthID ?? 0;
                cbbGeneralIndication_1.SelectedValue = endoscopic.Indication ?? 0;
                cbbGeneralMedication_1.SelectedValue = endoscopic.MedicationID ?? 0;
                dtArriveTime_1.Value = endoscopic.Arrive ?? DateTime.Now;
                txtGeneralInstrument_1.Text = endoscopic.Instrument;
                txtGeneralAnesthesia_1.Text = endoscopic.Anesthesia;
                txtAnesNurse_1.Text = endoscopic.AnesNurse;

                if (id == 1)
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
                else if (id == 2)
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
                else if (id == 3)
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
                else if (id == 5)
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
            else if (id == 4) // Bronchoscopy 
            {
                // General
                cbbEndoscopist_0.SelectedValue = patient.DoctorID ?? 0;
                cbbNurse1_0.SelectedValue = patient.NurseFirstID ?? 0;
                cbbNurse2_0.SelectedValue = patient.NurseSecondID ?? 0;
                cbbNurse3_0.SelectedValue = patient.NurseThirthID ?? 0;
                cbbGeneralMedication_0.SelectedValue = endoscopic.MedicationID ?? 0;
                dtArriveTime_0.Value = endoscopic.Arrive ?? DateTime.Now;
                txtGeneralInstrument_0.Text = endoscopic.Instrument;
                txtGeneralAnesthesia_0.Text = endoscopic.Anesthesia;
                txtAnesNurse_0.Text = endoscopic.AnesNurse;

                // Finding
                cbbFindingVocal_0.SelectedValue = finding.VocalCordID ?? 0;
                cbbFindingTrachea_0.SelectedValue = finding.TracheaID ?? 0;
                cbbFindingCarina_0.SelectedValue = finding.CarinaID ?? 0;
                cbbFindingRightMain_0.SelectedValue = finding.RightMainID ?? 0;
                cbbFindingRightIntermideate_0.SelectedValue = finding.RightIntermideateID ?? 0;
                cbbFindingRUL_0.SelectedValue = finding.RULID ?? 0;
                cbbFindingRML_0.SelectedValue = finding.RMLID ?? 0;
                cbbFindingRLL_0.SelectedValue = finding.RLLID ?? 0;
                cbbFindingLeftMain_0.SelectedValue = finding.LeftMainID ?? 0;
                cbbFindingLUL_0.SelectedValue = finding.LULID ?? 0;
                cbbFindingLingular_0.SelectedValue = finding.LingularID ?? 0;
                cbbFindingLLL_0.SelectedValue = finding.LLLID ?? 0;
                finding.VocalCordID = (int?)cbbFindingVocal_0.SelectedValue;
                finding.TracheaID = (int?)cbbFindingTrachea_0.SelectedValue;
                finding.CarinaID = (int?)cbbFindingCarina_0.SelectedValue;
                finding.RightMainID = (int?)cbbFindingRightMain_0.SelectedValue;
                finding.RightIntermideateID = (int?)cbbFindingRightIntermideate_0.SelectedValue;
                finding.RULID = (int?)cbbFindingRUL_0.SelectedValue;
                finding.RMLID = (int?)cbbFindingRML_0.SelectedValue;
                finding.RLLID = (int?)cbbFindingRLL_0.SelectedValue;
                finding.LeftMainID = (int?)cbbFindingLeftMain_0.SelectedValue;
                finding.LULID = (int?)cbbFindingLUL_0.SelectedValue;
                finding.LingularID = (int?)cbbFindingLingular_0.SelectedValue;
                finding.LLLID = (int?)cbbFindingLLL_0.SelectedValue;

                txtFindingVocalCord_0.Text = finding.VocalCord;
                txtFindingTrachea_0.Text = finding.Trachea;
                txtFindingCarina_0.Text = finding.Carina;
                txtFindingRightMain_0.Text = finding.RightMain;
                txtFindingIntermideate_0.Text = finding.RightIntermideate;
                txtFindingRUL_0.Text = finding.RUL;
                txtFindingRML_0.Text = finding.RML;
                txtFindingRLL_0.Text = finding.RLL;
                txtFindingLeftMain_0.Text = finding.LeftMain;
                txtFindingLUL_0.Text = finding.LUL;
                txtFindingLingular_0.Text = finding.Lingular;
                txtFindingLLL_0.Text = finding.LLL;

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
            
            txtDiagnosis.Text = endoscopic.Diagnosis;
            txtComplication.Text = endoscopic.Complication;
            txtComment.Text = endoscopic.Comment;

            if (endoscopic.StartRecordDate.HasValue) recordStartDate = endoscopic.StartRecordDate.Value;
            if (endoscopic.EndRecordDate.HasValue) recordEndDate = endoscopic.EndRecordDate.Value;
            PushEndoscopicImage();
            PushEndoscopicVideo();
        }

        private void PushEndoscopicImage()
        {
            SetPictureBox(pictureBoxSaved1, txtPictureBoxSaved1, 1, btnEditPic1, btnDeletePictureBoxSaved1);
            SetPictureBox(pictureBoxSaved2, txtPictureBoxSaved2, 2, btnEditPic2, btnDeletePictureBoxSaved2);
            SetPictureBox(pictureBoxSaved3, txtPictureBoxSaved3, 3, btnEditPic3, btnDeletePictureBoxSaved3);
            SetPictureBox(pictureBoxSaved4, txtPictureBoxSaved4, 4, btnEditPic4, btnDeletePictureBoxSaved4);
            SetPictureBox(pictureBoxSaved5, txtPictureBoxSaved5, 5, btnEditPic5, btnDeletePictureBoxSaved5);
            SetPictureBox(pictureBoxSaved6, txtPictureBoxSaved6, 6, btnEditPic6, btnDeletePictureBoxSaved6);
            SetPictureBox(pictureBoxSaved7, txtPictureBoxSaved7, 7, btnEditPic7, btnDeletePictureBoxSaved7);
            SetPictureBox(pictureBoxSaved8, txtPictureBoxSaved8, 8, btnEditPic8, btnDeletePictureBoxSaved8);
            SetPictureBox(pictureBoxSaved9, txtPictureBoxSaved9, 9, btnEditPic9, btnDeletePictureBoxSaved9);
            SetPictureBox(pictureBoxSaved10, txtPictureBoxSaved10, 10, btnEditPic10, btnDeletePictureBoxSaved10);
            SetPictureBox(pictureBoxSaved11, txtPictureBoxSaved11, 11, btnEditPic11, btnDeletePictureBoxSaved11);
            SetPictureBox(pictureBoxSaved12, txtPictureBoxSaved12, 12, btnEditPic12, btnDeletePictureBoxSaved12);
            SetPictureBox(pictureBoxSaved13, txtPictureBoxSaved13, 13, btnEditPic13, btnDeletePictureBoxSaved13);
            SetPictureBox(pictureBoxSaved14, txtPictureBoxSaved14, 14, btnEditPic14, btnDeletePictureBoxSaved14);
            SetPictureBox(pictureBoxSaved15, txtPictureBoxSaved15, 15, btnEditPic15, btnDeletePictureBoxSaved15);
            SetPictureBox(pictureBoxSaved16, txtPictureBoxSaved16, 16, btnEditPic16, btnDeletePictureBoxSaved16);
            SetPictureBox(pictureBoxSaved17, txtPictureBoxSaved17, 17, btnEditPic17, btnDeletePictureBoxSaved17);
            SetPictureBox(pictureBoxSaved18, txtPictureBoxSaved18, 18, btnEditPic18, btnDeletePictureBoxSaved18);
            SetAllPicture();
        }
        private void SetPictureBox(System.Windows.Forms.PictureBox pictureBox, TextBox textBox, int num, Button btnEdit, Button btnDelete)
        {
            var list = db.EndoscopicImages.Where(x => x.EndoscopicID == endoscopicId && x.ProcedureID == procedureId && (x.Seq != null && x.Seq.Value == num))
                .OrderByDescending(x => x.EndoscopicImageID).ToList();
            if (list.Count > 0)
            {
                string path = list.FirstOrDefault().ImagePath;
                pictureBox.ImageLocation = path;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                textBox.Text = list.FirstOrDefault().ImageComment;
                btnEdit.Visible = true;
                btnDelete.Visible = true;
            }
        }
        private void SetAllPicture()
        {
            var list = db.EndoscopicAllImages.Where(x => x.EndoscopicID == endoscopicId && x.ProcedureID == procedureId)
                .OrderByDescending(x => x.EndoscopicAllImageID).ToList();
            if (list.Count > 0)
            {
                int i = 0;
                foreach (var item in list)
                {
                    ImgPath.Add(i, item.ImagePath);
                    i++;
                }
            }
        }
        private void PushEndoscopicVideo()
        {
            var path = db.EndoscopicVideos.Where(x => x.EndoscopicID == endoscopicId && x.ProcedureID == procedureId).FirstOrDefault();
            if (path != null && path.VideoPath != null)
            {
                VdoPath = path.VideoPath;
                btnAllVideo.Enabled = true;
            }
        }
        private void btn_EnabledChanged(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.ForeColor = btn.Enabled == false ? Color.DarkGray : Color.Black;
            btn.BackColor = Color.Gainsboro;

        }
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
            pictureBoxSaved6.ImageLocation = pictureBoxSaved6.ImageLocation;
            pictureBoxSaved6.Refresh();
            pictureBoxSaved7.ImageLocation = pictureBoxSaved7.ImageLocation;
            pictureBoxSaved7.Refresh();
            pictureBoxSaved8.ImageLocation = pictureBoxSaved8.ImageLocation;
            pictureBoxSaved8.Refresh();
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
                if (i.GetType() == typeof(System.Windows.Forms.PictureBox))
                {
                    System.Windows.Forms.PictureBox p = i as System.Windows.Forms.PictureBox;
                    p.ImageLocation = p.ImageLocation;
                    p.Refresh();
                }
            }
        }
        private void btnPause_Click(object sender, EventArgs e)
        {
            _isPause = !_isPause;
            if (_isPause)
            {
                pictureBoxRecording.Visible = false;
                btnPause.Text = "Resume";
            }
            else
            {
                pictureBoxRecording.Visible = true;
                btnPause.Text = "Pause";
            }
        }
        private int SaveLogEndoscopic(Endoscopic en)
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
                db.Endoscopic_Log.Add(log);
                if (db.SaveChanges() > 0)
                {
                    var logEndoscopic = db.Endoscopic_Log.Where(x => x.ProcedureID == procedureId && x.PatientID == patientId).OrderByDescending(o => o.CreateDate).FirstOrDefault();
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


        #region Dropdown
        public void DropdownEndoscopist(ComboBox comboBox)
        {
            comboBox.ValueMember = "DoctorID";
            comboBox.DisplayMember = "NameTH";
            comboBox.DataSource = list.GetEndoscopistList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownNurse(ComboBox comboBox)
        {
            comboBox.ValueMember = "NurseID";
            comboBox.DisplayMember = "NameTH";
            comboBox.DataSource = list.GetNurseList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownMedication(ComboBox comboBox)
        {
            comboBox.ValueMember = "MedicationID";
            comboBox.DisplayMember = "MedicationName";
            comboBox.DataSource = list.GetMedicationList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownIndication(ComboBox comboBox)
        {
            comboBox.ValueMember = "IndicationID";
            comboBox.DisplayMember = "IndicationName";
            comboBox.DataSource = list.GetIndicationList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownAnalCanal()
        {
            cbbFindingAnalCanal_1.ValueMember = "AnalCanalID";
            cbbFindingAnalCanal_1.DisplayMember = "AnalCanalName";
            cbbFindingAnalCanal_1.DataSource = list.GetAnalCanalList();
            cbbFindingAnalCanal_1.SelectedIndex = 0;
        }
        public void DropdownRectum()
        {
            cbbFindingRectum_1.ValueMember = "RectumID";
            cbbFindingRectum_1.DisplayMember = "RectumName";
            cbbFindingRectum_1.DataSource = list.GetRectumList();
            cbbFindingRectum_1.SelectedIndex = 0;
        }
        public void DropdownSigmoidColon()
        {
            cbbFindingSigmoid_1.ValueMember = "SigmoidColonID";
            cbbFindingSigmoid_1.DisplayMember = "SigmoidColonName";
            cbbFindingSigmoid_1.DataSource = list.GetSigmoidColonList();
            cbbFindingSigmoid_1.SelectedIndex = 0;
        }
        public void DropdownDescendingColon()
        {
            cbbFindingDescending_1.ValueMember = "DescendingColonID";
            cbbFindingDescending_1.DisplayMember = "DescendingColonName";
            cbbFindingDescending_1.DataSource = list.GetDescendingColonList();
            cbbFindingDescending_1.SelectedIndex = 0;
        }
        public void DropdownSplenicFlexure()
        {
            cbbFindingFlexure_1.ValueMember = "SplenicFlexureID";
            cbbFindingFlexure_1.DisplayMember = "SplenicFlexureName";
            cbbFindingFlexure_1.DataSource = list.GetSplenicFlexureList();
            cbbFindingFlexure_1.SelectedIndex = 0;
        }
        public void DropdownTransverseColon()
        {
            cbbFindingTransverse_1.ValueMember = "TransverseColonID";
            cbbFindingTransverse_1.DisplayMember = "TransverseColonName";
            cbbFindingTransverse_1.DataSource = list.GetTransverseColonList();
            cbbFindingTransverse_1.SelectedIndex = 0;
        }
        public void DropdownHepaticFlexure()
        {
            cbbFindingHepatic_1.ValueMember = "HepaticFlexureID";
            cbbFindingHepatic_1.DisplayMember = "HepaticFlexureName";
            cbbFindingHepatic_1.DataSource = list.GetHepaticFlexureList();
            cbbFindingHepatic_1.SelectedIndex = 0;
        }
        public void DropdownAscendingColon()
        {
            cbbFindingAscending_1.ValueMember = "AscendingColonID";
            cbbFindingAscending_1.DisplayMember = "AscendingColonName";
            cbbFindingAscending_1.DataSource = list.GetAscendingColonList();
            cbbFindingAscending_1.SelectedIndex = 0;
        }
        public void DropdownIleocecalValve()
        {
            cbbFindingIleocecal_1.ValueMember = "IleocecalValveID";
            cbbFindingIleocecal_1.DisplayMember = "IleocecalValveName";
            cbbFindingIleocecal_1.DataSource = list.GetIleocecalValveList();
            cbbFindingIleocecal_1.SelectedIndex = 0;
        }
        public void DropdownCecum()
        {
            cbbFindingCecum_1.ValueMember = "CecumID";
            cbbFindingCecum_1.DisplayMember = "CecumName";
            cbbFindingCecum_1.DataSource = list.GetCecumList();
            cbbFindingCecum_1.SelectedIndex = 0;
        }
        public void DropdownTerminalIleum()
        {
            cbbFindingTerminal_1.ValueMember = "TerminalIleumID";
            cbbFindingTerminal_1.DisplayMember = "TerminalIleumName";
            cbbFindingTerminal_1.DataSource = list.GetTerminalIleumList();
            cbbFindingTerminal_1.SelectedIndex = 0;
        }
        public void DropdownEsophagus(ComboBox comboBox)
        {
            comboBox.ValueMember = "EsophagusID";
            comboBox.DisplayMember = "EsophagusName";
            comboBox.DataSource = list.GetEsophagusList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownStomach(ComboBox comboBox)
        {
            comboBox.ValueMember = "StomachID";
            comboBox.DisplayMember = "StomachName";
            comboBox.DataSource = list.GetStomachList();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownDuodenum()
        {
            cbbFindingDuodenum_2.ValueMember = "DuodenumID";
            cbbFindingDuodenum_2.DisplayMember = "DuodenumName";
            cbbFindingDuodenum_2.DataSource = list.GetDuodenumList();
            cbbFindingDuodenum_2.SelectedIndex = 0;
        }
        public void DropdownAmpulla()
        {
            cbbFindingAmpulla_2.ValueMember = "AmpullaOfVaterID";
            cbbFindingAmpulla_2.DisplayMember = "AmpullaOfVaterName";
            cbbFindingAmpulla_2.DataSource = list.GetAmpullaList();
            cbbFindingAmpulla_2.SelectedIndex = 0;
        }
        public void DropdownCholangiogram()
        {
            cbbFindingCholangiogram_2.ValueMember = "CholangiogramID";
            cbbFindingCholangiogram_2.DisplayMember = "CholangiogramName";
            cbbFindingCholangiogram_2.DataSource = list.GetCholangiogramList();
            cbbFindingCholangiogram_2.SelectedIndex = 0;
        }
        public void DropdownPancreatogram()
        {
            cbbFindingPancreatogram_2.ValueMember = "PancreatogramID";
            cbbFindingPancreatogram_2.DisplayMember = "PancreatogramName";
            cbbFindingPancreatogram_2.DataSource = list.GetPancreatogramList();
            cbbFindingPancreatogram_2.SelectedIndex = 0;
        }
        public void DropdownOropharynx()
        {
            cbbFindingOropharynx_3.ValueMember = "OropharynxID";
            cbbFindingOropharynx_3.DisplayMember = "OropharynxName";
            cbbFindingOropharynx_3.DataSource = list.GetOropharynxList();
            cbbFindingOropharynx_3.SelectedIndex = 0;
        }
        public void DropdownEGJunction()
        {
            cbbFindingEGJunction_3.ValueMember = "EGJunctionID";
            cbbFindingEGJunction_3.DisplayMember = "EGJunctionName";
            cbbFindingEGJunction_3.DataSource = list.GetEGJunctionList();
            cbbFindingEGJunction_3.SelectedIndex = 0;
        }
        public void DropdownCardia()
        {
            cbbFindingCardia_3.ValueMember = "CardiaID";
            cbbFindingCardia_3.DisplayMember = "CardiaName";
            cbbFindingCardia_3.DataSource = list.GetCardiaList();
            cbbFindingCardia_3.SelectedIndex = 0;
        }
        public void DropdownFundus()
        {
            cbbFindingFundus_3.ValueMember = "FundusID";
            cbbFindingFundus_3.DisplayMember = "FundusName";
            cbbFindingFundus_3.DataSource = list.GetFundusList();
            cbbFindingFundus_3.SelectedIndex = 0;
        }
        public void DropdownBody()
        {
            cbbFindingBody_3.ValueMember = "BodyID";
            cbbFindingBody_3.DisplayMember = "BodyName";
            cbbFindingBody_3.DataSource = list.GetBodyList();
            cbbFindingBody_3.SelectedIndex = 0;
        }
        public void DropdownAntrum()
        {
            cbbFindingAntrum_3.ValueMember = "AntrumID";
            cbbFindingAntrum_3.DisplayMember = "AntrumName";
            cbbFindingAntrum_3.DataSource = list.GetAntrumList();
            cbbFindingAntrum_3.SelectedIndex = 0;
        }
        public void DropdownPylorus()
        {
            cbbFindingPylorus_3.ValueMember = "PylorusID";
            cbbFindingPylorus_3.DisplayMember = "PylorusName";
            cbbFindingPylorus_3.DataSource = list.GetPylorusList();
            cbbFindingPylorus_3.SelectedIndex = 0;
        }
        public void DropdownDuodenalBulb()
        {
            cbbFindingDuodenalBulb_3.ValueMember = "DuodenalBulbID";
            cbbFindingDuodenalBulb_3.DisplayMember = "DuodenalBulbName";
            cbbFindingDuodenalBulb_3.DataSource = list.GetDuodenalBulbList();
            cbbFindingDuodenalBulb_3.SelectedIndex = 0;
        }
        public void DropdownSecondPart()
        {
            cbbFindingSecondPart_3.ValueMember = "SecondPartID";
            cbbFindingSecondPart_3.DisplayMember = "SecondPartName";
            cbbFindingSecondPart_3.DataSource = list.GetSecondPartList();
            cbbFindingSecondPart_3.SelectedIndex = 0;
        }
        public void DropdownVocalCord()
        {
            cbbFindingVocal_0.ValueMember = "VocalCordID";
            cbbFindingVocal_0.DisplayMember = "VocalCordName";
            cbbFindingVocal_0.DataSource = list.GetVocalCordList();
            cbbFindingVocal_0.SelectedIndex = 0;
        }
        public void DropdownTrachea()
        {
            cbbFindingTrachea_0.ValueMember = "TracheaID";
            cbbFindingTrachea_0.DisplayMember = "TracheaName";
            cbbFindingTrachea_0.DataSource = list.GetTracheaList();
            cbbFindingTrachea_0.SelectedIndex = 0;
        }
        public void DropdownCarina()
        {
            cbbFindingCarina_0.ValueMember = "CarinaID";
            cbbFindingCarina_0.DisplayMember = "CarinaName";
            cbbFindingCarina_0.DataSource = list.GetCarinaList();
            cbbFindingCarina_0.SelectedIndex = 0;
        }
        public void DropdownRightMain()
        {
            cbbFindingRightMain_0.ValueMember = "RightMainID";
            cbbFindingRightMain_0.DisplayMember = "RightMainName";
            cbbFindingRightMain_0.DataSource = list.GetRightMainList();
            cbbFindingRightMain_0.SelectedIndex = 0;
        }
        public void DropdownRightIntermideate()
        {
            cbbFindingRightIntermideate_0.ValueMember = "RightIntermideateID";
            cbbFindingRightIntermideate_0.DisplayMember = "RightIntermideateName";
            cbbFindingRightIntermideate_0.DataSource = list.GetRightIntermideateList();
            cbbFindingRightIntermideate_0.SelectedIndex = 0;
        }
        public void DropdownRUL()
        {
            cbbFindingRUL_0.ValueMember = "RULID";
            cbbFindingRUL_0.DisplayMember = "RULName";
            cbbFindingRUL_0.DataSource = list.GetRULList();
            cbbFindingRUL_0.SelectedIndex = 0;
        }
        public void DropdownRML()
        {
            cbbFindingRML_0.ValueMember = "RMLID";
            cbbFindingRML_0.DisplayMember = "RMLName";
            cbbFindingRML_0.DataSource = list.GetRMLList();
            cbbFindingRML_0.SelectedIndex = 0;
        }
        public void DropdownRLL()
        {
            cbbFindingRLL_0.ValueMember = "RLLID";
            cbbFindingRLL_0.DisplayMember = "RLLName";
            cbbFindingRLL_0.DataSource = list.GetRLLList();
            cbbFindingRLL_0.SelectedIndex = 0;
        }
        public void DropdownLeftMain()
        {
            cbbFindingLeftMain_0.ValueMember = "LeftMainID";
            cbbFindingLeftMain_0.DisplayMember = "LeftMainName";
            cbbFindingLeftMain_0.DataSource = list.GetLeftMainList();
            cbbFindingLeftMain_0.SelectedIndex = 0;
        }
        public void DropdownLUL()
        {
            cbbFindingLUL_0.ValueMember = "LULID";
            cbbFindingLUL_0.DisplayMember = "LULName";
            cbbFindingLUL_0.DataSource = list.GetLULList();
            cbbFindingLUL_0.SelectedIndex = 0;
        }
        public void DropdownLingular()
        {
            cbbFindingLingular_0.ValueMember = "LingularID";
            cbbFindingLingular_0.DisplayMember = "LingularName";
            cbbFindingLingular_0.DataSource = list.GetLingularList();
            cbbFindingLingular_0.SelectedIndex = 0;
        }
        public void DropdownLLL()
        {
            cbbFindingLLL_0.ValueMember = "LLLID";
            cbbFindingLLL_0.DisplayMember = "LLLName";
            cbbFindingLLL_0.DataSource = list.GetLLLList();
            cbbFindingLLL_0.SelectedIndex = 0;
        }
        public void DropdownNasalCavityLeft()
        {
            cbbFiNCL.ValueMember = "NasalCavityID";
            cbbFiNCL.DisplayMember = "NasalCavityName";
            cbbFiNCL.DataSource = list.GetNasalCavities();
            cbbFiNCL.SelectedIndex = 0;
        }
        public void DropdownNasalCavityRight()
        {
            cbbFiNCR.ValueMember = "NasalCavityID";
            cbbFiNCR.DisplayMember = "NasalCavityName";
            cbbFiNCR.DataSource = list.GetNasalCavities();
            cbbFiNCR.SelectedIndex = 0;
        }
        public void DropdownSeptum()
        {
            cbbFiSeptum.ValueMember = "SeptumID";
            cbbFiSeptum.DisplayMember = "SeptumName";
            cbbFiSeptum.DataSource = list.GetSeptums();
            cbbFiSeptum.SelectedIndex = 0;
        }
        public void DropdownNasopharynx()
        {
            cbbFiNasopharynx.ValueMember = "NasopharynxID";
            cbbFiNasopharynx.DisplayMember = "NasopharynxName";
            cbbFiNasopharynx.DataSource = list.GetNasopharynxes();
            cbbFiNasopharynx.SelectedIndex = 0;
        }

        public void DropdownRoof()
        {
            cbbFiRoof.ValueMember = "RoofID";
            cbbFiRoof.DisplayMember = "RoofName";
            cbbFiRoof.DataSource = list.GetRoofs();
            cbbFiRoof.SelectedIndex = 0;
        }
        public void DropdownPosteriorWall()
        {
            cbbFiPosteriorWall.ValueMember = "PosteriorWallID";
            cbbFiPosteriorWall.DisplayMember = "PosteriorWallName";
            cbbFiPosteriorWall.DataSource = list.GetPosteriorWalls();
            cbbFiPosteriorWall.SelectedIndex = 0;
        }
        public void DropdownRosenmullerFossa()
        {
            cbbFiRosenmullerFossa.ValueMember = "RosenmullerFossaID";
            cbbFiRosenmullerFossa.DisplayMember = "RosenmullerFossaName";
            cbbFiRosenmullerFossa.DataSource = list.GetRosenmullerFossas();
            cbbFiRosenmullerFossa.SelectedIndex = 0;
        }
        public void DropdownEustachianTubeOrifice(ComboBox comboBox)
        {
            comboBox.ValueMember = "EustachianTubeOrificeID";
            comboBox.DisplayMember = "EustachianTubeOrificeName";
            comboBox.DataSource = list.GetEustachianTubeOrifices();
            comboBox.SelectedIndex = 0;
        }
        public void DropdownSoftPalate()
        {
            cbbFiSoftPalate.ValueMember = "SoftPalateID";
            cbbFiSoftPalate.DisplayMember = "SoftPalateName";
            cbbFiSoftPalate.DataSource = list.GetSoftPalates();
            cbbFiSoftPalate.SelectedIndex = 0;
        }
        public void DropdownUvula()
        {
            cbbFiUvula.ValueMember = "UvulaID";
            cbbFiUvula.DisplayMember = "UvulaName";
            cbbFiUvula.DataSource = list.GetUvulas();
            cbbFiUvula.SelectedIndex = 0;
        }
        public void DropdownTonsil()
        {
            cbbFiTonsil.ValueMember = "TonsilID";
            cbbFiTonsil.DisplayMember = "TonsilName";
            cbbFiTonsil.DataSource = list.GetTonsils();
            cbbFiTonsil.SelectedIndex = 0;
        }
        public void DropdownBaseOfTongue()
        {
            cbbFiBaseOfTongue.ValueMember = "BaseOfTongueID";
            cbbFiBaseOfTongue.DisplayMember = "BaseOfTongueName";
            cbbFiBaseOfTongue.DataSource = list.GetBaseOfTongues();
            cbbFiBaseOfTongue.SelectedIndex = 0;
        }
        public void DropdownVallecula()
        {
            cbbFiVallecula.ValueMember = "ValleculaID";
            cbbFiVallecula.DisplayMember = "ValleculaName";
            cbbFiVallecula.DataSource = list.GetValleculas();
            cbbFiVallecula.SelectedIndex = 0;
        }
        public void DropdownPyriformSinusLeft()
        {
            cbbFiPyrSinusL.ValueMember = "PyriformSinusID";
            cbbFiPyrSinusL.DisplayMember = "PyriformSinusName";
            cbbFiPyrSinusL.DataSource = list.GetPyriformSinus();
            cbbFiPyrSinusL.SelectedIndex = 0;
        }
        public void DropdownPyriformSinusRight()
        {
            cbbFiPyrSinusR.ValueMember = "PyriformSinusID";
            cbbFiPyrSinusR.DisplayMember = "PyriformSinusName";
            cbbFiPyrSinusR.DataSource = list.GetPyriformSinus();
            cbbFiPyrSinusR.SelectedIndex = 0;
        }
        public void DropdownPostcricoid()
        {
            cbbFiPostcricoid.ValueMember = "PostcricoidID";
            cbbFiPostcricoid.DisplayMember = "PostcricoidName";
            cbbFiPostcricoid.DataSource = list.GetPostcricoids();
            cbbFiPostcricoid.SelectedIndex = 0;
        }
        public void DropdownPosteriorPharyngealWall()
        {
            cbbFiPosPhaWall.ValueMember = "PosteriorPharyngealWallID";
            cbbFiPosPhaWall.DisplayMember = "PosteriorPharyngealWallName";
            cbbFiPosPhaWall.DataSource = list.GetPosteriorPharyngealWalls();
            cbbFiPosPhaWall.SelectedIndex = 0;
        }
        public void DropdownSupraglottic()
        {
            cbbFiSupraglottic.ValueMember = "SupraglotticID";
            cbbFiSupraglottic.DisplayMember = "SupraglotticName";
            cbbFiSupraglottic.DataSource = list.GetSupraglottics();
            cbbFiSupraglottic.SelectedIndex = 0;
        }
        public void DropdownGlottic()
        {
            cbbFiGlottic.ValueMember = "GlotticID";
            cbbFiGlottic.DisplayMember = "GlotticName";
            cbbFiGlottic.DataSource = list.GetGlottics();
            cbbFiGlottic.SelectedIndex = 0;
        }
        public void DropdownSubglottic()
        {
            cbbFiSubglottic.ValueMember = "SubglotticID";
            cbbFiSubglottic.DisplayMember = "SubglotticName";
            cbbFiSubglottic.DataSource = list.GetSubglottics();
            cbbFiSubglottic.SelectedIndex = 0;
        }
        public void DropdownUES()
        {
            cbbFiUES.ValueMember = "UESID";
            cbbFiUES.DisplayMember = "UESName";
            cbbFiUES.DataSource = list.GetUEs();
            cbbFiUES.SelectedIndex = 0;
        }
        public void DropdownLES()
        {
            cbbFiLES.ValueMember = "LESID";
            cbbFiLES.DisplayMember = "LESName";
            cbbFiLES.DataSource = list.GetLEs();
            cbbFiLES.SelectedIndex = 0;
        }
        #endregion
    }
}
