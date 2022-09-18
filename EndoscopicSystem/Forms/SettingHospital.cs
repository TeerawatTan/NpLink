using EndoscopicSystem.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EndoscopicSystem
{
    public partial class SettingHospitalForm : Form
    {
        private readonly int UserID;
        public SettingHospitalForm(int userID)
        {
            InitializeComponent();
            UserID = userID;
        }

        EndoscopicEntities db = new EndoscopicEntities();

        private void btnBrowseFile_Click(object sender, EventArgs e)
        {

            openFileDialog1.InitialDirectory = "C://Desktop";
            openFileDialog1.Title = "Select image to be upload.";
            openFileDialog1.Filter = "Image Only(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            openFileDialog1.FilterIndex = 1;
            try
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog1.CheckFileExists)
                    {
                        string path = System.IO.Path.GetFullPath(openFileDialog1.FileName);
                        lblPath.Text = path;
                        pictureBox1.Image = new Bitmap(openFileDialog1.FileName);
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUploadFile_Click(object sender, EventArgs e)
        {
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
                    filename = "logo-" + DateTime.Now.ToString("ddMMyyyyHHss") + filenameExtension;
                    string path = Application.StartupPath.Replace("\\bin\\Debug", "");
                    string fullDir = path + "\\Images\\Logo\\";
                    if (!Directory.Exists(fullDir))
                    {
                        Directory.CreateDirectory(fullDir);
                    }
                    System.IO.File.Copy(openFileDialog1.FileName, fullDir + filename);

                    var logo = db.Hospitals.Where(c => c.HospitalID == 1).SingleOrDefault();

                    if (logo != null)
                    {
                        logo.HospitalLogoPath = path + @"\Images\Logo\" + filename;
                        logo.UpdateDate = DateTime.Now;
                        db.Entry(logo).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                    }
                    else
                    {
                        Hospital Hospital = new Hospital();
                        Hospital.CreateDate = DateTime.Now;
                        Hospital.HospitalLogoPath = path + @"\Images\Logo\" + filename;
                        db.Hospitals.Add(Hospital);
                        db.SaveChanges();
                    }

                    MessageBox.Show("Image uploaded successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "File Already exits");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var hospitalUpdate = db.Hospitals.OrderByDescending(x => x.HospitalID).FirstOrDefault();

            if (hospitalUpdate != null)
            {
                hospitalUpdate.HospitalNameEN = txtHospitalNameEN.Text;
                hospitalUpdate.HospitalNameTH = txtHospitalNameTH.Text;
                hospitalUpdate.Address1 = txtAddress1.Text;
                hospitalUpdate.Address2 = txtAddress2.Text;
                hospitalUpdate.Address3 = txtAddress3.Text;
                hospitalUpdate.Tel = txtTel.Text;
                hospitalUpdate.UpdateDate = DateTime.Now;
                hospitalUpdate.UpdateBy = UserID;
                db.Entry(hospitalUpdate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

            }
            else
            {
                Hospital hospital = new Hospital();

                hospital.HospitalNameEN = txtHospitalNameEN.Text;
                hospital.HospitalNameTH = txtHospitalNameTH.Text;
                hospital.Address1 = txtAddress1.Text;
                hospital.Address2 = txtAddress2.Text;
                hospital.Address3 = txtAddress3.Text;
                hospital.Tel = txtTel.Text;
                hospital.CreateDate = DateTime.Now;
                hospital.CreateBy = UserID;
                db.Hospitals.Add(hospital);
                db.SaveChanges();
            }

            MessageBox.Show("Save successfully.");
        }

        private void SettingHospitalForm_Load(object sender, EventArgs e)
        {
            var hospital = db.Hospitals.OrderByDescending(x => x.HospitalID).FirstOrDefault();

            if (hospital != null)
            {

                try
                {
                    pictureBox1.ImageLocation = hospital.HospitalLogoPath;
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    txtAddress1.Text = hospital.Address1;
                    txtAddress2.Text = hospital.Address2;
                    txtAddress3.Text = hospital.Address3;
                    txtHospitalNameEN.Text = hospital.HospitalNameEN;
                    txtHospitalNameTH.Text = hospital.HospitalNameTH;
                    txtTel.Text = hospital.Tel;

                }
                catch (Exception ex)
                {


                }
            }
        }

        private void SettingHospitalForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
    }
}
