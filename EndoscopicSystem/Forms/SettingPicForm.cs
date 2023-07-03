using EndoscopicSystem.Constants;
using EndoscopicSystem.Entities;
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
        private readonly int UserID;
        public int AspectRatioID;
        public string PositionCropID;

        private string _initialDirectoryUpload = "C://Desktop";
        private string _titleUpload = "Select image to be upload.";
        private string _filterUpload = "Image Only(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";

        int crpX, crpY, rectW, rectH;
        public Pen crpPen = new Pen(Color.White);
        int RatioX;
        int RatioY;
        private Image File = null;

        public SettingPicForm(int userID)
        {
            InitializeComponent();
            UserID = userID;
        }

        private void SettingPicForm_Load(object sender, EventArgs e)
        {
            var v = db.Users.Where(x => x.Id == UserID).Select(x => new { x.AspectRatioID, x.RatioX, x.RatioY, x.PositionCrop }).FirstOrDefault();
            AspectRatioID = (int)(v.AspectRatioID.HasValue ? v.AspectRatioID : 1);
            txtAspectRatio.Text = AspectRatioID.ToString();
            txtPosition.Text = v.PositionCrop;
            txtX.Text = v.RatioX.ToString();
            txtY.Text = v.RatioY.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            RatioX = txtX.Text == "" ? 0 : Convert.ToInt32(txtX.Text);
            RatioY = txtY.Text == "" ? 0 : Convert.ToInt32(txtY.Text);
            var user = db.Users.FirstOrDefault(x => x.Id == UserID);
            if (user == null)
            {
                MessageBox.Show("User not found.");
                return;
            }
            user.AspectRatioID = txtAspectRatio.Text == "" ? 0 : Convert.ToInt32(txtAspectRatio.Text);
            user.PositionCrop = txtPosition.Text;
            user.CrpX = (int?)(crpX * 1.5);
            user.CrpY = (int?)(crpY * 1.5);
            user.CrpWidth = (int?)(rectW * 1.5);
            user.CrpHeight = (int?)(rectH * 1.5);
            txtCrpX.Text = (crpX * 1.5).ToString();
            txtCrpY.Text = (crpY * 1.5).ToString();
            txbW.Text = (rectW * 1.5).ToString();
            txbH.Text = (rectH * 1.5).ToString();
            try
            {
                db.SaveChanges();

                MessageBox.Show("Saved successfully.", "Save form", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSimulate_Click(object sender, EventArgs e)
        {
            try
            {
                txtX.Text = rectW.ToString();
                txtY.Text = rectH.ToString();
                txbW.Text = rectW.ToString();
                txbH.Text = rectH.ToString();
                txtAspectRatio.Text = "0";
                txtPosition.Text = "L";

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
            txtAspectRatio.Text = "1";
            txtPosition.Text = "L";

            var userData = db.Users.Where(x => x.Id == UserID);
            if (userData != null)
            {
                User user = userData.FirstOrDefault();
                user.AspectRatioID = 1;
                user.PositionCrop = "L";
                try
                {
                    db.SaveChanges();
                    MessageBox.Show("Reset to full screen success.", "Reset settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Error, Not found data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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
