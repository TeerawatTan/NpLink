using EndoscopicSystem.Constants;
using EndoscopicSystem.Repository;
using System;
using System.Windows.Forms;

namespace EndoscopicSystem.V2.Forms
{
    public partial class FormLogin : Form
    {
        protected readonly IUserRepository userRepo;
        public int userID = 0;

        public FormLogin()
        {
            InitializeComponent();
            userRepo = new UserRepository();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            ClearForm();
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
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txbUsername.Text) && !string.IsNullOrWhiteSpace(txbPassword.Text))
            {
                string status = CheckLogin(txbUsername.Text, txbPassword.Text);
                if (status.Equals(Constant.LOGIN_SUCCESS))
                {
                    // Hide all forms except subForm
                    //foreach (Form form in Application.OpenForms)
                    //{
                    //    if (form != this)
                    //    {
                    //        form.Hide();
                    //    }
                    //}
                    // V.1
                    //Home openHome = new Home(userID);
                    //openHome.ShowDialog();

                    // V.2
                    this.Hide();

                    FormHome formHome = new FormHome(userID);
                    formHome.ShowDialog();
                    formHome = null;
                    this.Show();
                }
                else
                {
                    //ClearForm();
                    MessageBox.Show(status);
                }
            }
        }

        private void ClearForm()
        {
            txbUsername.Clear();
            txbPassword.Clear();
            txbUsername.Focus();
        }

        private void txbUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txbPassword.Focus();
        }

        private void txbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnLogin.PerformClick();
        }

        private void FormLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Application.Exit();
            //Application.ExitThread();
        }
    }
}
