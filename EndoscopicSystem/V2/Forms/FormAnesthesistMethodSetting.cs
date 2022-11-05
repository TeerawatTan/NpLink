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
    public partial class FormAnesthesistMethodSetting : Form
    {
        private readonly EndoscopicEntities _db = new EndoscopicEntities();
        private int _id;
        public FormAnesthesistMethodSetting(int id)
        {
            InitializeComponent();

            this._id = id;
        }

        private void LoadData(List<AnesthesistMethod> data)
        {
            var list = new List<AnesthesistMethodModel>();
            if (data != null && data.Count > 0)
            {
                foreach (var item in data)
                {
                    var model = new AnesthesistMethodModel();
                    model.ID = item.ID;
                    model.Name = item.Name;
                    model.IsActive = item.IsActive ?? false;

                    list.Add(model);
                }
            }
            gridAnesthesistMethod.DataSource = list;
        }

        private void GetData()
        {
            var data = _db.AnesthesistMethods.ToList();
            LoadData(data);
        }

        private void FormAnesthesistMethodSetting_Load(object sender, EventArgs e)
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
                        var data = _db.AnesthesistMethods.Where(x => x.ID == id).FirstOrDefault();
                        data.Name = txtName.Text;
                        data.IsActive = chkEnable.Checked;
                        data.UpdateBy = _id;
                        data.UpdateDate = DateTime.Now;
                    }
                    else
                    {
                        AnesthesistMethod data = new AnesthesistMethod();
                        data.Name = txtName.Text;
                        data.IsActive = chkEnable.Checked;
                        data.CreateBy = _id;
                        data.CreateDate = DateTime.Now;

                        _db.AnesthesistMethods.Add(data);
                    }
                    if (_db.SaveChanges() > 0)
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.Controls.ClearControls();
            txtName.Focus();
            GetData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var data = _db.AnesthesistMethods.ToList();
            if (!string.IsNullOrWhiteSpace(txtNameSearch.Text))
            {
                data = data.Where(x => (x.Name ?? "").Contains(txtNameSearch.Text)).ToList();
            }
            gridAnesthesistMethod.DataSource = data;
        }

        private void gridAnesthesistMethod_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string id = gridAnesthesistMethod.Rows[e.RowIndex].Cells["ID"].FormattedValue.ToString();
            string name = gridAnesthesistMethod.Rows[e.RowIndex].Cells["Name"].FormattedValue.ToString();
            bool isActive = Convert.ToBoolean(gridAnesthesistMethod.Rows[e.RowIndex].Cells["IsActive"].Value);
            if (!string.IsNullOrWhiteSpace(id))
            {
                txtID.Text = id;
                txtName.Text = name;
                chkEnable.Checked = isActive;
            }
        }
    }
    public class AnesthesistMethodModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
