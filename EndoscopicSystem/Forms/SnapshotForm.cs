using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EndoscopicSystem.Forms
{
    public partial class SnapshotForm : Form
    {
        public SnapshotForm()
        {
            InitializeComponent();
        }

        public void SetImage(Bitmap bitmap)
        {
            timeBox.Text = DateTime.Now.ToLongTimeString();

            lock (this)
            {
                Bitmap old = (Bitmap)pictureBox.Image;
                pictureBox.Image = bitmap;

                if (old != null)
                {
                    old.Dispose();
                }
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string ext = Path.GetExtension(saveFileDialog.FileName).ToLower();
                ImageFormat format = ImageFormat.Jpeg;

                if (ext == ".bmp")
                {
                    format = ImageFormat.Bmp;
                }
                else if (ext == ".png")
                {
                    format = ImageFormat.Png;
                }

                try
                {
                    lock (this)
                    {
                        Bitmap image = (Bitmap)pictureBox.Image;

                        image.Save(saveFileDialog.FileName, format);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed saving the snapshot.\n" + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
