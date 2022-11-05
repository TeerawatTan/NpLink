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
    public partial class FormAnesthesistSetting : Form
    {
        private readonly EndoscopicEntities _db = new EndoscopicEntities();
        private int _id;
        public FormAnesthesistSetting(int id)
        {
            InitializeComponent();

            this._id = id;
        }

        private void LoadData(List<Anesthesist> data)
        {
            var list = new List<AnesthesistModel>();
            if (data != null && data.Count > 0)
            {
                foreach (var item in data)
                {
                    var model = new AnesthesistModel();
                    model.AnesthesistID = item.AnesthesistID;
                    model.NameEN = item.NameEN;
                    model.NameTH = item.NameTH;
                    model.IsActive = item.IsActive ?? false;

                    list.Add(model);
                }
            }
            gridAnesthesist.DataSource = list;
        }

        private void GetData()
        {
            var data = _db.Anesthesists.ToList();
            LoadData(data);
        }

        private void FormAnesthesistSetting_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtNameEN;
            txtNameEN.Focus();
            GetData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtNameEN.Text) || !string.IsNullOrWhiteSpace(txtNameTH.Text))
                {
                    if (!string.IsNullOrWhiteSpace(txtID.Text))
                    {
                        int id = Convert.ToInt32(txtID.Text);
                        var data = _db.Nurses.Where(x => x.NurseID == id).FirstOrDefault();
                        data.NameTH = txtNameTH.Text;
                        data.NameEN = txtNameEN.Text;
                        data.IsActive = chkEnable.Checked;
                        data.UpdateBy = _id;
                        data.UpdateDate = DateTime.Now;
                    }
                    else
                    {
                        Anesthesist anest = new Anesthesist();
                        anest.NameEN = txtNameEN.Text;
                        anest.NameTH = txtNameTH.Text;
                        anest.IsActive = chkEnable.Checked;
                        anest.CreateBy = _id;
                        anest.CreateDate = DateTime.Now;

                        _db.Anesthesists.Add(anest);
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
            txtNameEN.Focus();
            GetData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var data = _db.Anesthesists.ToList();
            if (!string.IsNullOrWhiteSpace(txtNameSearch.Text))
            {
                data = data.Where(x => (x.NameTH ?? "").Contains(txtNameSearch.Text) || (x.NameEN ?? "").Contains(txtNameSearch.Text)).ToList();
            }
            gridAnesthesist.DataSource = data;
        }

        private void gridAnesthesist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string id = gridAnesthesist.Rows[e.RowIndex].Cells["OpdID"].FormattedValue.ToString();
            string nameTh = gridAnesthesist.Rows[e.RowIndex].Cells["NameTH"].FormattedValue.ToString();
            string nameEn = gridAnesthesist.Rows[e.RowIndex].Cells["NameEN"].FormattedValue.ToString();
            bool isActive = Convert.ToBoolean(gridAnesthesist.Rows[e.RowIndex].Cells["IsActive"].Value);
            if (!string.IsNullOrWhiteSpace(id))
            {
                txtID.Text = id;
                txtNameTH.Text = nameTh;
                txtNameEN.Text = nameEn;
                chkEnable.Checked = isActive;
            }
        }
    }

    public class AnesthesistModel
    {
        public int AnesthesistID { get; set; }
        public string NameEN { get; set; }
        public string NameTH { get; set; }
        public bool IsActive { get; set; }
    }
}
