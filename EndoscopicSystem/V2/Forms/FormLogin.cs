using EndoscopicSystem.Constants;
using EndoscopicSystem.Helpers;
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
                var checkUser = userRepo.CountUser();
                if (checkUser == 0)
                {
                    FormRegister formRegister = new FormRegister();
                    formRegister.ShowDialog();
                }

                var user = userRepo.GetUserLogin(userName);
                if (user != null && EncriptHelper.VerifyPassword(password, user.PasswordHash))
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
                    //MessageBox.Show(status);
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

        private void label4_Click(object sender, EventArgs e)
        {
            FormRegister formRegister = new FormRegister();
            formRegister.ShowDialog();
        }

        private void label4_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            txbPassword.UseSystemPasswordChar = !txbPassword.UseSystemPasswordChar;
        }
    }
}
