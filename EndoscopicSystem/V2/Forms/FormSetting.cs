using System;
using System.Drawing;
using System.Windows.Forms;

namespace EndoscopicSystem.V2.Forms
{
    public partial class FormSetting : Form
    {
        private int _id = 0;
        private Form _activeForm = null;
        private Button _currentBtn = null;

        public FormSetting(int id)
        {
            InitializeComponent();
            _id = id;
        }

        private void FormSetting_Load(object sender, EventArgs e)
        {
            DisableMenuButton();
        }

        private void DisableMenuButton()
        {
            if (_currentBtn != null)
            {
                _currentBtn.BackColor = Color.FromArgb(51, 80, 139);
                _currentBtn.ForeColor = Color.Black;
            }
        }

        private void ActiveMenuButton(object senderBtn)
        {
            if (senderBtn != null)
            {
                DisableMenuButton();
                _currentBtn = (Button)senderBtn;
                _currentBtn.BackColor = Color.FromArgb(153, 204, 255);
                _currentBtn.ForeColor = Color.Orange;
            }
        }

        public void OpenChildForm(Form childForm)
        {
            if (_activeForm != null)
                _activeForm.Close();
            _activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChild.Controls.Add(childForm);
            panelChild.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void menuUserManage_Click(object sender, EventArgs e)
        {
            OpenChildForm(new UserManageForm(_id));
            ActiveMenuButton(sender);
        }

        private void menuDoctor_Click(object sender, EventArgs e)
        {
            OpenChildForm(new DoctorForm(_id));
            ActiveMenuButton(sender);
        }

        private void menuNurse_Click(object sender, EventArgs e)
        {
            OpenChildForm(new NurseForm(_id));
            ActiveMenuButton(sender);
        }

        private void FormSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormHome formHome = new FormHome(_id);
            formHome.Show();
        }
    }
}
