using EndoscopicSystem.Constants;
using EndoscopicSystem.Entities;
using EndoscopicSystem.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EndoscopicSystem.V2.Forms
{
    public partial class FormRegister : Form
    {
        private readonly EndoscopicEntities _db = new EndoscopicEntities();
        public FormRegister()
        {
            InitializeComponent();
        }

        private void FormRegister_Load(object sender, EventArgs e)
        {
            this.Controls.ClearControls();

            txbUser.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txbPassword.Text != txbConfirmPassword.Text)
            {
                MessageBox.Show("Fail !", "Confirm password not match.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                User user = new User();
                user.IsAdmin = true;
                user.UserName = txbUser.Text.Trim();
                user.PasswordHash = EncriptHelper.HashPassword(txbConfirmPassword.Text.Trim());

                _db.Users.Add(user);

                this.Controls.ClearControls();

                this.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            txbPassword.UseSystemPasswordChar = !txbPassword.UseSystemPasswordChar;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            txbConfirmPassword.UseSystemPasswordChar = !txbConfirmPassword.UseSystemPasswordChar;
        }
    }
}
