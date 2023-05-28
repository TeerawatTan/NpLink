using EndoscopicSystem.Entities;
using EndoscopicSystem.Helpers;
using FellowOakDicom;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EndoscopicSystem.V2.Forms
{
    public partial class FormSendPACS : Form
    {
        private readonly EndoscopicEntities _db = new EndoscopicEntities();
        private Patient _patient = new Patient();
        private int _id, _item, _count;
        private string _hnNo;
        private string _dicomPath = ConfigurationManager.AppSettings["pathSaveDicom"];
        private string _hostPACS = ConfigurationManager.AppSettings["hostPACS"];
        private string _port = ConfigurationManager.AppSettings["portPACS"];
        private string _source = ConfigurationManager.AppSettings["sourcePACS"];
        private string _destination = ConfigurationManager.AppSettings["destinationPACS"];

        public FormSendPACS(int userId, string hn)
        {
            InitializeComponent();
            this._id = userId;
            this._hnNo = hn;
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
                pictureBox.BorderStyle = BorderStyle.FixedSingle;

                // Calculate the location of the PictureBox based on its position in the grid
                int maxHeight = pictureBox.Bottom + 50;
                int row = i / numCols;
                int col = i % numCols;
                int x = col * (maxWidth + spacing);
                int y = row * (maxHeight + spacing);
                pictureBox.Location = new Point(x, y);

                string[] pathArray = img[i].Split('\\');
                string fileName = pathArray[pathArray.Length - 1];

                CheckBox checkBox = new CheckBox();
                checkBox.Name = "checkBox" + i;
                checkBox.BackColor = Color.Transparent;
                checkBox.Text = fileName;
                checkBox.Tag = img[i];
                checkBox.Location = new Point(pictureBox.Location.X, pictureBox.Location.Y);
                checkBox.CheckedChanged += CheckBox_CheckedChanged;

                // Set the size of the PictureBox based on the aspect ratio of the image
                int pictureBoxWidth = maxWidth;
                int pictureBoxHeight = (int)((double)pictureBoxWidth / 200 * 140);

                // Add the PictureBox to the Panel
                panel.Controls.Add(pictureBox);
                panel.Controls.Add(checkBox);

                // Add Event
                pictureBox.MouseDown += new MouseEventHandler(PictureBox_MouseDown);

                checkBox.BringToFront();
            }
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;

            if (checkBox.Checked)
            {
                _count++;
                listBox1.Items.Add((string)checkBox.Tag);
            }
            else
            {
                _count--;
                listBox1.Items.Remove((string)checkBox.Tag);
            }

            lbCountSelected.Text = _count.ToString();
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            if (pictureBox != null && e.Button == MouseButtons.Left)
            {
                pictureBox1.ImageLocation = pictureBox.ImageLocation;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Update();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Clear checkBox
            foreach (Control control in panel1.Controls)
            {
                if (control is CheckBox checkBox && checkBox.Checked)
                {
                    checkBox.Checked = false;
                }
            }
            _count = 0;
            lbCountSelected.Text = _count.ToString();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            // Send to PACS

            List<string> imgList = listBox1.Items.Cast<string>().ToList();
            for (int i = 0; i < imgList.Count; i++)
            {
                ConvertToDicomFile(i, imgList[i]);
                bool isSend = await SendToPACS(imgList[i]);
            }

        }

        private void FormSendPACS_Load(object sender, EventArgs e)
        {
            _patient = _db.Patients.FirstOrDefault(f => f.HN == _hnNo);

            if (_patient == null)
            {
                return;
            }

            txbPatitientId.Text = _patient.HN;
            txbPatitentName.Text = _patient.Fullname;

            string imgPathOrigin = (from a in _db.Appointments
                                    join en in _db.EndoscopicImages on a.EndoscopicID equals en.EndoscopicID
                                    where a.PatientID == _patient.PatientID && a.ProcedureID == _patient.ProcedureID && en.ImagePath != null
                                    select en.ImagePath).FirstOrDefault();

            string splitImgPath = ImageHelper.GetUntilOrEmpty(imgPathOrigin, "Image_");

            DirectoryInfo dinfo = new DirectoryInfo($@"{splitImgPath}");
            FileInfo[] files = (FileInfo[])dinfo.GetFiles("*.jpg").Where(w => w.Name.StartsWith("Image")).ToArray();
            var pathOriginImgList = files.OrderBy(o => o.CreationTime).Select(s => s.FullName).ToList();

            GeneratePictureBoxWwithImages(pathOriginImgList);
        }

        private void ConvertToDicomFile(int i, string img)
        {
            try
            {
                DicomFile dicomFile = DicomFile.Open(img);

                // Save the dataset to a DICOM file
                string pathFolderDicomToSave = _dicomPath + _hnNo + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\";
                if (!Directory.Exists(pathFolderDicomToSave))
                {
                    Directory.CreateDirectory(pathFolderDicomToSave);
                }
                string newPathFileDicom = $"{_dicomPath}_{i}.dcm";
                dicomFile.Save(newPathFileDicom);

                listBox2.Items.Add(newPathFileDicom);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<bool> SendToPACS(string img)
        {
            try
            {
                if (_patient == null)
                {
                    return false;
                }

                DicomFile dicomFile = DicomFile.Open(img);

                dicomFile.Dataset.AddOrUpdate(DicomTag.PatientName, _patient.Fullname);
                dicomFile.Dataset.AddOrUpdate(DicomTag.PatientID, _patient.HN);
                dicomFile.Dataset.AddOrUpdate(DicomTag.PatientSex, _patient.Sex.HasValue ? _patient.Sex.Value ? "M" : "F" : "");
                dicomFile.Dataset.AddOrUpdate(DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage);
                dicomFile.Dataset.AddOrUpdate(DicomTag.SOPInstanceUID, DicomUID.Generate());
                dicomFile.Dataset.AddOrUpdate(DicomTag.StudyDate, DateTime.Now.ToString("yyyyMMdd"));
                dicomFile.Dataset.AddOrUpdate(DicomTag.StudyTime, DateTime.Now.ToString("HHmmss"));
                dicomFile.Dataset.AddOrUpdate(DicomTag.StudyDescription, "Snapshot");

                var client = DicomClientFactory.Create(_hostPACS, int.Parse(_port), false, _source, _destination);
                await client.AddRequestAsync(new DicomCStoreRequest(dicomFile));
                await client.SendAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
