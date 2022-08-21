using EndoscopicSystem.Constants;
using EndoscopicSystem.Repository;
using System;
using System.Windows.Forms;

namespace EndoscopicSystem.Forms
{
    public partial class LoginForm : Form
    {
        protected readonly IUserRepository userRepo;
        public int userID = 0;

        public LoginForm()
        {
            InitializeComponent();
            userRepo = new UserRepository();
            this.ActiveControl = txtUserName;
            txtUserName.Focus();
        }

        private string CheckLogin(string userName, string password)
        {
            try
            {
                var user = userRepo.GetUserLogin(userName);
                if (user != null && user.PasswordHash.Equals(password))
                {
                    userID = user.Id;
                    return Constant.LOGIN_SUCCESS;
                }
                else
                {
                    return Constant.LOGIN_NOT_SUCCESS;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (!txtUserName.Text.Equals("") && !txtPassword.Text.Equals(""))
            {
                string status = CheckLogin(txtUserName.Text, txtPassword.Text);
                if (status.Equals(Constant.LOGIN_SUCCESS))
                {
                    this.Hide();
                    Home openHome = new Home(userID);
                    openHome.ShowDialog();
                }
                else
                {
                    ClearForm();
                    MessageBox.Show(status);
                }
            }
        }

        private void ClearForm()
        {
            txtUserName.Clear();
            txtPassword.Clear();
            txtUserName.Focus();
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtPassword.Focus();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnLogin.PerformClick();
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
            Application.ExitThread();
        }
    }
}
