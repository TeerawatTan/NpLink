using EndoscopicSystem.Entities;
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
                    pictureBox_Dashboard.ImageLocation = logo.HospitalLogoPath;
                    pictureBox_Dashboard.SizeMode = PictureBoxSizeMode.StretchImage;
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

            FormLogin formLogin = new FormLogin();
            formLogin.Show();
        }

        private void pictureBox_Dashboard_Click(object sender, EventArgs e)
        {
            DashboardForm dashboardForm  = new DashboardForm(_id);
            dashboardForm.Show();
            ActiveMenuLabel(lb_Dashboard);

            this.Hide();
        }

        private void pictureBox_Patient_Click(object sender, EventArgs e)
        {
            PatientForm patientForm = new PatientForm(_id);
            patientForm.Show();
            ActiveMenuLabel(lb_Patient);

            this.Hide();
        }

        private void pictureBox_EndoscopyRoom_Click(object sender, EventArgs e)
        {
            EndoscopicForm endoscopicForm = new EndoscopicForm(_id);
            endoscopicForm.Show();
            ActiveMenuLabel(lb_EndoscopyRoom);

            this.Hide();
        }

        private void pictureBox_SeaarchPatient_Click(object sender, EventArgs e)
        {
            SearchPatientForm searchPatientForm = new SearchPatientForm(_id);
            searchPatientForm.Show();
            ActiveMenuLabel(lb_SearchPatient);

            this.Hide();
        }

        private void pictureBox_Setting_Click(object sender, EventArgs e)
        {
            FormSetting formSetting = new FormSetting(_id);
            formSetting.Show();

            this.Hide();
        }

        private void FormHome_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            formLogin.Show();
        }
    }
}
