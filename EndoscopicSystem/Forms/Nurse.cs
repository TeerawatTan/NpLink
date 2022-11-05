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
    public partial class NurseForm : Form
    {
        readonly EndoscopicEntities db = new EndoscopicEntities();
        private readonly int UserID;
        public NurseForm(int userID)
        {
            InitializeComponent();
            UserID = userID;
        }

        private void NurseForm_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtNameEN;
            txtNameEN.Focus();
            GetData();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtNameEN.Text) || !string.IsNullOrWhiteSpace(txtNameTH.Text))
                {
                    if (!string.IsNullOrWhiteSpace(txtNurseID.Text))
                    {
                        int id = Convert.ToInt32(txtNurseID.Text);
                        var data = db.Nurses.Where(x => x.NurseID == id).FirstOrDefault();
                        data.NameTH = txtNameTH.Text;
                        data.NameEN = txtNameEN.Text;
                        data.IsActive = chkEnable.Checked;
                        data.UpdateBy = UserID;
                    }
                    else
                    {
                        Nurse nurse = new Nurse();
                        nurse.NameEN = txtNameEN.Text;
                        nurse.NameTH = txtNameTH.Text;
                        nurse.CreateDate = DateTime.Now;
                        nurse.IsActive = chkEnable.Checked;
                        nurse.CreateBy = UserID;

                        db.Nurses.Add(nurse);
                    }
                    if (db.SaveChanges() > 0)
                    {
                        this.Controls.ClearControls();
                        MessageBox.Show("Save successfully.");

                        GetData();
                    }
                    else
                    {
                        MessageBox.Show("Not success!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadData(List<Nurse> data)
        {
            var list = new List<NurseModel>();
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    var model = new NurseModel();
                    model.NurseID = item.NurseID;
                    model.NameEN = item.NameEN;
                    model.NameTH = item.NameTH;
                    model.IsActive = item.IsActive ?? false;

                    list.Add(model);
                }
            }
            gridNurse.DataSource = list;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.Controls.ClearControls();
            txtNameEN.Focus();
            GetData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var data = db.Nurses.ToList();
            if (!string.IsNullOrWhiteSpace(txtFullname.Text))
            {
                data = data.Where(x => (x.NameTH ?? "").Contains(txtFullname.Text) || (x.NameEN ?? "").Contains(txtFullname.Text)).ToList();
            }
            LoadData(data);
        }

        private void gridNurse_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string id = gridNurse.Rows[e.RowIndex].Cells["NurseID"].FormattedValue.ToString();
            string nameTh = gridNurse.Rows[e.RowIndex].Cells["NameTH"].FormattedValue.ToString();
            string nameEn = gridNurse.Rows[e.RowIndex].Cells["NameEN"].FormattedValue.ToString();
            bool isActive = Convert.ToBoolean(gridNurse.Rows[e.RowIndex].Cells["IsActive"].Value);
            if (!string.IsNullOrWhiteSpace(id))
            {
                txtNurseID.Text = id;
                txtNameTH.Text = nameTh;
                txtNameEN.Text = nameEn;
                chkEnable.Checked = isActive;
            }
        }
        private void GetData()
        {
            var data = db.Nurses.ToList();
            LoadData(data);
        }
    }

    public class NurseModel
    {
        public int NurseID { get; set; }
        public string NameEN { get; set; }
        public string NameTH { get; set; }
        public bool IsActive { get; set; }
    }
}
