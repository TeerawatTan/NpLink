﻿using EndoscopicSystem.Entities;
using EndoscopicSystem.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EndoscopicSystem.V2.Forms
{
    public partial class FormHome : Form
    {
        readonly EndoscopicEntities _db = new EndoscopicEntities();
        private Label _currentLabel = null;
        private int _id = 0;
        private string _pathFolderVideo = Application.StartupPath.Replace("\\bin\\Debug", "") + @"\VideoRecord\";
        private string _pathFolderImage = Application.StartupPath.Replace("\\bin\\Debug", "") + @"\ImageCapture\";
        private string _pathFolderDicom = Application.StartupPath.Replace("\\bin\\Debug", "") + @"\Dicom\";
        private string _pathFolderPdf = Application.StartupPath.Replace("\\bin\\Debug", "") + @"\Pdf\";
        public FormHome(int id)
        {
            InitializeComponent();
            _id = id;
        }

        private void FormHome_Load(object sender, EventArgs e)
        {
            var logo = _db.Hospitals.Where(c => c.HospitalID == 1).FirstOrDefault();

            if (logo != null && logo.HospitalLogoPath != null)
            {
                pictureBox_logo.Image = new Bitmap(logo.HospitalLogoPath);
                pictureBox_logo.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            ConfigCreateFolder();
        }

        private void ConfigCreateFolder()
        {
            if (!Directory.Exists(_pathFolderVideo))
            {
                Directory.CreateDirectory(_pathFolderVideo);
            }
            if (!Directory.Exists(_pathFolderImage))
            {
                Directory.CreateDirectory(_pathFolderImage);
            }
            if (!Directory.Exists(_pathFolderDicom))
            {
                Directory.CreateDirectory(_pathFolderDicom);
            }
            if (!Directory.Exists(_pathFolderPdf))
            {
                Directory.CreateDirectory(_pathFolderPdf);
            }
        }

        private void DisableMenuLabel()
        {
            if (_currentLabel != null)
            {
                _currentLabel.ForeColor = Color.Black;
            }
        }

        private void ActiveMenuLabel(object senderLabel)
        {
            if (senderLabel != null)
            {
                DisableMenuLabel();
                _currentLabel = (Label)senderLabel;
                _currentLabel.ForeColor = Color.Orange;
            }
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox_Dashboard_Click(object sender, EventArgs e)
        {
            // Hide all forms except subForm
            //foreach (Form form in Application.OpenForms)
            //{
            //    if (form != this)
            //    {
            //        form.Hide();
            //    }
            //}
            this.Hide();
            DashboardForm dashboardForm = new DashboardForm(_id);
            dashboardForm.ShowDialog();
            dashboardForm = null;
            this.Show();
            //dashboardForm.Show();
            ActiveMenuLabel(lb_Dashboard);
        }

        private void pictureBox_Patient_Click(object sender, EventArgs e)
        {
            this.Hide();
            PatientForm patientForm = new PatientForm(_id);
            patientForm.ShowDialog();
            patientForm = null;
            this.Show();
            ActiveMenuLabel(lb_Patient);
        }

        private void pictureBox_EndoscopyRoom_Click(object sender, EventArgs e)
        {
            this.Hide();
            //FormLive formLive = new FormLive(_id);
            //formLive.ShowDialog();
            //formLive = null;
            FormProceed formProceed = new FormProceed(_id);
            formProceed.ShowDialog();
            formProceed = null;
            this.Show();
            ActiveMenuLabel(lb_EndoscopyRoom);
        }

        private void pictureBox_SeaarchPatient_Click(object sender, EventArgs e)
        {
            this.Hide();
            SearchPatientForm searchPatientForm = new SearchPatientForm(_id);
            searchPatientForm.ShowDialog();
            searchPatientForm = null;
            this.Show();
            ActiveMenuLabel(lb_SearchPatient);
        }

        private void pictureBox_Setting_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormSetting formSetting = new FormSetting(_id);
            formSetting.ShowDialog();
            formSetting = null;
            this.Show();
            ActiveMenuLabel(lb_Setting);
        }
    }
}