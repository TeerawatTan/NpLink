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

namespace EndoscopicSystem.V2.Forms
{
    public partial class FormWardSetting : Form
    {
        private readonly EndoscopicEntities _db = new EndoscopicEntities();
        public FormWardSetting()
        {
            InitializeComponent();
        }

        private void LoadData(List<WardList> data)
        {
            var list = new List<WardModel>();
            if (data != null && data.Count > 0)
            {
                foreach (var item in data)
                {
                    var model = new WardModel();
                    model.WardID = item.WardID;
                    model.WardName = item.WardName;
                    model.IsActive = item.IsActive ?? false;

                    list.Add(model);
                }
            }
            gridWard.DataSource = list;
        }

        private void GetData()
        {
            var data = _db.WardLists.ToList();
            LoadData(data);
        }

        private void FormWardSetting_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtName;
            txtName.Focus();
            GetData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtName.Text))
                {
                    if (!string.IsNullOrWhiteSpace(txtID.Text))
                    {
                        int id = Convert.ToInt32(txtID.Text);
                        var data = _db.WardLists.Where(x => x.WardID == id).FirstOrDefault();
                        data.WardName = txtName.Text;
                        data.IsActive = chkEnable.Checked;
                    }
                    else
                    {
                        WardList data = new WardList();
                        data.WardName = txtName.Text;
                        data.IsActive = chkEnable.Checked;

                        _db.WardLists.Add(data);
                    }
                    if (_db.SaveChanges() > 0)
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.Controls.ClearControls();
            txtName.Focus();
            GetData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var data = _db.WardLists.ToList();
            if (!string.IsNullOrWhiteSpace(txtNameSearch.Text))
            {
                data = data.Where(x => (x.WardName ?? "").Contains(txtNameSearch.Text)).ToList();
            }
            gridWard.DataSource = data;
        }

        private void gridWard_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string id = gridWard.Rows[e.RowIndex].Cells["WardID"].FormattedValue.ToString();
            string name = gridWard.Rows[e.RowIndex].Cells["WardName"].FormattedValue.ToString();
            bool isActive = Convert.ToBoolean(gridWard.Rows[e.RowIndex].Cells["IsActive"].Value);
            if (!string.IsNullOrWhiteSpace(id))
            {
                txtID.Text = id;
                txtName.Text = name;
                chkEnable.Checked = isActive;
            }
        }
    }

    public class WardModel
    {
        public int WardID { get; set; }
        public string WardName { get; set; }
        public bool IsActive { get; set; }
    }
}
