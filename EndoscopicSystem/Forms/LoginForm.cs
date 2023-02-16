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
            catch (Exception)
            {
                return Constant.STATUS_ERROR;
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtUsername.Text) && !string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                string status = CheckLogin(txtUsername.Text, txtPassword.Text);
                if (status.Equals(Constant.LOGIN_SUCCESS))
                {
                    // V.2
                    this.Hide();

                    Home formHome = new Home(userID);
                    formHome.ShowDialog();
                    formHome = null;
                    this.Show();
                }
                else
                {
                    //ClearForm();
                    //MessageBox.Show(status);
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
