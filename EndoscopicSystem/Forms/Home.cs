using EndoscopicSystem.Constants;
using EndoscopicSystem.Entities;
using EndoscopicSystem.Forms;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EndoscopicSystem
{
    public partial class Home : Form
    {
        readonly EndoscopicEntities db = new EndoscopicEntities();
        private int UserID;
        private Button currentBtn;
        public Home(int userID)
        {
            InitializeComponent();
            UserID = userID;
            //OpenChildForm(new DashboardForm(userID));
            this.Load += Home_Shown;
        }

        private void Home_Load(object sender, EventArgs e)
        {
            var data = db.Users.Where(x => x.Id == UserID).FirstOrDefault();
            bool isAdmin = data.IsAdmin ?? false;
            HideShowMenu(isAdmin);
            var logo = db.Hospitals.Where(c => c.HospitalID == 1).SingleOrDefault();

            if (logo != null)
            {
                try
                {
                    pictureBox1.ImageLocation = logo.HospitalLogoPath;
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                catch (Exception ex)
                {

                }
            }

        }

        private void HideShowMenu(bool isAdmin)
        {
            if (!isAdmin)
            {
                //menuReport.Hide();
                menuUserManage.Hide();
                menuSettingHospital.Hide();
                menuDoctor.Hide();
                menuNurse.Hide();
                menuEndoscopyRoom.Hide();
            }
            else
            {
                //menuReport.Show();
                menuUserManage.Show();
                menuSettingHospital.Show();
                menuDoctor.Show();
                menuNurse.Show();
                menuEndoscopyRoom.Show();
            }
        }

        public byte[] imgToByteArray(Image img)
        {
            using (MemoryStream mStream = new MemoryStream())
            {
                img.Save(mStream, img.RawFormat);
                return mStream.ToArray();
            }
        }
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream mStream = new MemoryStream(byteArrayIn))
            {
                return Image.FromStream(mStream);
            }
        }

        public Form activeForm = null;

        public void OpenChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChild.Controls.Add(childForm);
            panelChild.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void menuPatient_Click(object sender, EventArgs e)
        {
            OpenChildForm(new PatientForm(UserID));
            ActiveMenuButton(sender);
        }

        private void menuSearchPatient_Click(object sender, EventArgs e)
        {
            OpenChildForm(new SearchPatientForm(UserID, Constant.PageName.SEARCH_PATIENT_PAGE));
            ActiveMenuButton(sender);
        }

        public void menuEndoscopicRecords_Click(object sender, EventArgs e)
        {
            OpenChildForm(new EndoscopicForm(UserID));
            ActiveMenuButton(sender);
        }

        private void menuUserManage_Click(object sender, EventArgs e)
        {
            OpenChildForm(new UserManageForm(UserID));
            ActiveMenuButton(sender);
        }

        private void menuSettingHospital_Click(object sender, EventArgs e)
        {
            OpenChildForm(new SettingHospitalForm(UserID));
            ActiveMenuButton(sender);
        }

        private void menuDocter_Click(object sender, EventArgs e)
        {
            OpenChildForm(new DoctorForm(UserID));
            ActiveMenuButton(sender);
        }

        private void menuNurse_Click(object sender, EventArgs e)
        {
            OpenChildForm(new NurseForm(UserID));
            ActiveMenuButton(sender);
        }

        private void menuEndoscopyRoom_Click(object sender, EventArgs e)
        {
            OpenChildForm(new EndoscopyRoomForm(UserID));
            ActiveMenuButton(sender);
        }

        private void menuLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }

        private void menuDashboard_Click(object sender, EventArgs e)
        {
            OpenChildForm(new DashboardForm(UserID));
            ActiveMenuButton(sender);
        }

        private void menuSettingPicture_Click(object sender, EventArgs e)
        {
            OpenChildForm(new SettingPicForm(UserID));
            ActiveMenuButton(sender);
        }

        private void menuReport_Click(object sender, EventArgs e)
        {
            //OpenChildForm(new ReportEndoscopic());
        }

        private void Home_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
            Application.ExitThread();
        }

        private void menuSearchEndoscopic_Click(object sender, EventArgs e)
        {
            OpenChildForm(new SearchEndoscopic(UserID));
            ActiveMenuButton(sender);
        }

        private void ActiveMenuButton(object senderBtn)
        {
            if (senderBtn != null)
            {
                DisableMenuButton();
                currentBtn = (Button)senderBtn;
                currentBtn.ForeColor = Color.Orange;
            }
        }
        private void DisableMenuButton()
        {
            if (currentBtn != null)
            {
                currentBtn.ForeColor = Color.Black;
            }
        }

        private void Home_Shown(object sender, EventArgs e)
        {
            menuDashboard.PerformClick();
        }
    }
}
