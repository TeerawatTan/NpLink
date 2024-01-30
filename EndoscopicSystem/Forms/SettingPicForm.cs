using EndoscopicSystem.Constants;
using EndoscopicSystem.Entities;
using EndoscopicSystem.V2.Forms.src;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EndoscopicSystem.Forms
{
    public partial class SettingPicForm : Form
    {
        readonly EndoscopicEntities db = new EndoscopicEntities();
        private int UserID;
        public string PositionCropID;

        private string _initialDirectoryUpload = "C://Desktop";
        private string _titleUpload = "Select image to be upload.";
        private string _filterUpload = "Image Only(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";

        int crpX, crpY, rectW, rectH;
        public Pen crpPen = new Pen(Color.White);
        //int RatioX;
        //int RatioY;
        private Image File = null;
        private readonly DropdownListService _dropdownListService = new DropdownListService();

        public SettingPicForm(int userID)
        {
            InitializeComponent();
            UserID = userID;
        }

        private void SettingPicForm_Load(object sender, EventArgs e)
        {
            _dropdownListService.DropdownInstrumentIdAndCode(cbbInstrument1, 1);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbProfileSetting.Text.Trim()))
            {
                MessageBox.Show("Profile Setting is required.", "Validate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if ((int?)cbbInstrument1.SelectedValue == null || (int?)cbbInstrument1.SelectedValue == 0)
            {
                MessageBox.Show("Instrument is required.", "Validate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                int cropX = Convert.ToInt32(crpX * 1.5);
                int cropY = Convert.ToInt32(crpY * 1.5);
                int rectWidth = Convert.ToInt32(rectW * 1.5);
                int rectHeight = Convert.ToInt32(rectH * 1.5);

                SettingDevice setting = new SettingDevice();
                setting.Name = txbProfileSetting.Text;
                setting.AspectRatio = "Custom";
                setting.CrpX = cropX;
                setting.CrpY = cropY;
                setting.CrpWidth = rectWidth;
                setting.CrpHeight = rectHeight;
                setting.PositionCrop = "L";
                setting.Instrument1ID = (int?)cbbInstrument1.SelectedValue;
                setting.IsActive = true;

                db.SettingDevices.Add(setting);
                db.SaveChanges();

                MessageBox.Show("Saved successfully.", "Save form", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnSimulate_Click(object sender, EventArgs e)
        {
            if (rectW == 0 && rectH == 0)
            {
                return;
            }

            try
            {
                Cursor = Cursors.Default;

                Bitmap bmp2 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                pictureBox1.DrawToBitmap(bmp2, pictureBox1.ClientRectangle);

                Bitmap crpImg = new Bitmap(rectW, rectH);

                for (int i = 0; i < rectW; i++)
                {
                    for (int j = 0; j < rectH; j++)
                    {
                        Color pxlclr = bmp2.GetPixel(crpX + i, crpY + j);
                        crpImg.SetPixel(i, j, pxlclr);
                    }
                }

                pictureBox2.Image = (Image)crpImg;
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch (Exception)
            {
                return;
            }
        }
        private Image uploadImageFile()
        {
            openFileDialog1.InitialDirectory = _initialDirectoryUpload;
            openFileDialog1.Title = _titleUpload;
            openFileDialog1.Filter = _filterUpload;
            openFileDialog1.FilterIndex = 1;

            Bitmap Imageupload = null;

            try
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog1.CheckFileExists)
                    {
                        Imageupload = new Bitmap(openFileDialog1.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return (Image)Imageupload;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            pictureBox1.MouseDown += new MouseEventHandler(pictureBox1_MouseDown);

            pictureBox1.MouseMove += new MouseEventHandler(pictureBox1_MouseMove);

            pictureBox1.MouseEnter += new EventHandler(pictureBox1_MouseEnter);
            Controls.Add(pictureBox1);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Cursor = Cursors.Cross;
                crpPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                crpX = e.X;
                crpY = e.Y;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.File = uploadImageFile();
            if (this.File != null)
            {
                pictureBox1.Image = File;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Update();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtAspectRatio.Text = "Full Screen";
            txbProfileSetting.Clear();
            cbbInstrument1.SelectedIndex = 0;
        }

        private void cbbInstrument1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? instruDdlId = (int?)cbbInstrument1.SelectedValue;

            if (instruDdlId == null || instruDdlId < 0)
                return;

            var instrument = db.Instruments.Where(w => w.ID == instruDdlId).FirstOrDefault();
            if (instrument != null)
                SerialNumber1.Text = instrument.SerialNumber;
            else
                SerialNumber1.Clear();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            base.OnMouseEnter(e);
            Cursor = Cursors.Cross;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                pictureBox1.Refresh();
                rectW = e.X - crpX;
                rectH = e.Y - crpY;
                Graphics g = pictureBox1.CreateGraphics();
                g.DrawRectangle(crpPen, crpX, crpY, rectW, rectH);
                g.Dispose();
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Cursor = Cursors.Default;
        }
    }
}
