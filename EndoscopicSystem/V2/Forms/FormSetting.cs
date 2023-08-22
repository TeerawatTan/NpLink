using EndoscopicSystem.Forms;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EndoscopicSystem.V2.Forms
{
    public partial class FormSetting : Form
    {
        private int _id = 0;
        private Form _activeForm = null;
        private Button _currentBtn = null;

        public FormSetting(int id, string actionMenu = null)
        {
            InitializeComponent();
            _id = id;
            
            if (actionMenu == "menuInstrumentSetting")
            {
                PerformButtonClick(actionMenu);
            }
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
                _currentBtn.ForeColor = Color.White;
            }
        }

        private void ActiveMenuButton(object senderBtn)
        {
            if (senderBtn != null)
            {
                DisableMenuButton();
                _currentBtn = (Button)senderBtn;
                _currentBtn.BackColor = Color.WhiteSmoke;    //Color.FromArgb(153, 204, 255);
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

        private void menuOPD_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormOpdSetting(_id));
            ActiveMenuButton(sender);
        }

        private void menuWard_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormWardSetting());
            ActiveMenuButton(sender);
        }

        private void menuAnesthesist_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormAnesthesistSetting(_id));
            ActiveMenuButton(sender);
        }

        private void menuAnesthesistMethod_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormAnesthesistMethodSetting(_id));
            ActiveMenuButton(sender);
        }

        private void menuCameraSetting_Click(object sender, EventArgs e)
        {
            OpenChildForm(new SettingPicForm(_id));
            ActiveMenuButton(sender);
        }

        private void menuHospitalSetting_Click(object sender, EventArgs e)
        {
            OpenChildForm(new SettingHospitalForm(_id));
            ActiveMenuButton(sender);
        }

        private void menuFinancialSetting_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormFinancialSetting());
            ActiveMenuButton(sender);
        }

        private void menuInstrumentSetting_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormInstrumentSetting(_id));
            ActiveMenuButton(sender);
        }

        private void menuEndoscopicRoom_Click(object sender, EventArgs e)
        {
            OpenChildForm(new EndoscopyRoomForm(_id));
            ActiveMenuButton(sender);
        }

        private void PerformButtonClick(string buttonName)
        {
            Button button = this.panel1.Controls.OfType<Button>().FirstOrDefault(f => f.Name == buttonName);
            
            if (button != null)
            {
                if (button.Name == "menuInstrumentSetting")
                    menuInstrumentSetting_Click(button, EventArgs.Empty);
            }
            else
            {
                return;
            }
        }
    }
}
