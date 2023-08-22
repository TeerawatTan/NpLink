using EndoscopicSystem.Constants;
using EndoscopicSystem.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EndoscopicSystem
{
    public partial class UserManageForm : Form
    {
        readonly EndoscopicEntities db = new EndoscopicEntities();
        private readonly int UserID;
        public UserManageForm(int userID)
        {
            InitializeComponent();
            UserID = userID;
        }

        private void UserManageForm_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtUsername;
            txtUsername.Focus();
            GetData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtUsername.Text) || !string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    if (!string.IsNullOrWhiteSpace(txtUserID.Text))
                    {
                        int id = Convert.ToInt32(txtUserID.Text);
                        var data = db.Users.Where(x => x.Id == id).FirstOrDefault();
                        data.UserName = txtUsername.Text;
                        data.Fullname = txtFullName.Text;
                        if (!string.IsNullOrWhiteSpace(txtPassword.Text)) data.PasswordHash = txtPassword.Text;
                        data.IsAdmin = chkIsAdmin.Checked;
                    }
                    else
                    {
                        User user = new User();
                        user.Fullname = txtFullName.Text;
                        user.UserName = txtUsername.Text;
                        user.PasswordHash = txtPassword.Text;
                        user.IsAdmin = chkIsAdmin.Checked;
                        db.Users.Add(user);
                    }
                    if (db.SaveChanges() > 0)
                    {
                        MessageBox.Show("Saved successfully.", "Save form", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Controls.ClearControls();

                        GetData();
                    }
                    else
                    {
                        MessageBox.Show("There was a problem saving the data.", "Error save form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var data = db.Users.ToList();
            if (!string.IsNullOrWhiteSpace(txtSearchName.Text))
            {
                data = data.Where(x => (x.Fullname ?? "").Contains(txtSearchName.Text)).ToList();
            }
            LoadData(data);
        }

        private void GetData()
        {
            var data = db.Users.ToList();
            LoadData(data);
        }

        public void LoadData(List<User> data)
        {
            var list = new List<UserModel>();
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    var model = new UserModel();
                    model.Id = item.Id;
                    model.UserName = item.UserName;
                    model.Fullname = item.Fullname;
                    model.IsAdmin = item.IsAdmin ?? false;

                    list.Add(model);
                }
            }
            gridUser.DataSource = list;
        }

        private void gridUser_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int id = Convert.ToInt32(gridUser.Rows[e.RowIndex].Cells["Id"].Value);
            string userName = gridUser.Rows[e.RowIndex].Cells["UserName"].FormattedValue.ToString();
            string fullname = gridUser.Rows[e.RowIndex].Cells["Fullname"].FormattedValue.ToString();
            bool isAdmin = Convert.ToBoolean(gridUser.Rows[e.RowIndex].Cells["IsAdmin"].Value);
            txtUserID.Text = id.ToString();
            txtUsername.Text = userName;
            txtFullName.Text = fullname;
            chkIsAdmin.Checked = isAdmin;
            string password = db.Users.FirstOrDefault(x => x.Id == id).PasswordHash;
            txtPassword.Text = password;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.Controls.ClearControls();
            txtUsername.Focus();
            GetData();
        }
    }
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Fullname { get; set; }
        public bool IsAdmin { get; set; }
    }
}
