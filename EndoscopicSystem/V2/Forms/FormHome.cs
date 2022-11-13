using EndoscopicSystem.Entities;
using EndoscopicSystem.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EndoscopicSystem.V2.Forms
{
    public partial class FormHome : Form
    {
        readonly EndoscopicEntities _db = new EndoscopicEntities();
        private Label _currentLabel = null;
        private int _id = 0;
        public FormHome(int id)
        {
            InitializeComponent();
            _id = id;
        }

        private void FormHome_Load(object sender, EventArgs e)
        {
            var logo = _db.Hospitals.Where(c => c.HospitalID == 1).SingleOrDefault();

            if (logo != null)
            {
                try
                {
                    pictureBox_logo.ImageLocation = logo.HospitalLogoPath;
                    pictureBox_logo.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                catch (Exception)
                {

                }
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
            DashboardForm dashboardForm  = new DashboardForm(_id);
            dashboardForm.ShowDialog();
            ActiveMenuLabel(lb_Dashboard);
        }

        private void pictureBox_Patient_Click(object sender, EventArgs e)
        {
            PatientForm patientForm = new PatientForm(_id);
            patientForm.ShowDialog();
            ActiveMenuLabel(lb_Patient);
        }

        private void pictureBox_EndoscopyRoom_Click(object sender, EventArgs e)
        {
            FormLive formLive = new FormLive(_id);
            formLive.ShowDialog();
            ActiveMenuLabel(lb_EndoscopyRoom);
        }

        private void pictureBox_SeaarchPatient_Click(object sender, EventArgs e)
        {
            SearchPatientForm searchPatientForm = new SearchPatientForm(_id);
            searchPatientForm.ShowDialog();
            ActiveMenuLabel(lb_SearchPatient);
        }

        private void pictureBox_Setting_Click(object sender, EventArgs e)
        {
            FormSetting formSetting = new FormSetting(_id);
            formSetting.ShowDialog();
            ActiveMenuLabel(lb_Setting);
        }

        private void FormHome_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            formLogin.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormProcedure formProcedure = new FormProcedure(_id, "102", 0, 0);
            formProcedure.ShowDialog();
        }
    }
}
