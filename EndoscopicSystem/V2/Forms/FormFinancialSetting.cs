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
    public partial class FormFinancialSetting : Form
    {
        private readonly EndoscopicEntities _db = new EndoscopicEntities();
        public FormFinancialSetting()
        {
            InitializeComponent();
        }

        private void FormFinancialSetting_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtName;
            txtName.Focus();
            GetData();
        }

        private void LoadData(List<Financial> data)
        {
            var list = new List<FinancialModel>();
            if (data != null && data.Count > 0)
            {
                foreach (var item in data)
                {
                    var model = new FinancialModel();
                    model.FinancialID = item.ID;
                    model.FinancialName = item.Name;

                    list.Add(model);
                }
            }
            gridFinancial.DataSource = list;
        }

        private void GetData()
        {
            var data = _db.Financials.ToList();
            LoadData(data);
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
                        var data = _db.Financials.Where(x => x.ID == id).FirstOrDefault();
                        data.Name = txtName.Text;
                    }
                    else
                    {
                        Financial data = new Financial();
                        data.Name = txtName.Text;

                        _db.Financials.Add(data);
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
            var data = _db.Financials.ToList();
            if (!string.IsNullOrWhiteSpace(txtNameSearch.Text))
            {
                data = data.Where(x => (x.Name ?? "").Contains(txtNameSearch.Text)).ToList();
            }
            gridFinancial.DataSource = data;
        }

        private void gridFinancial_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string id = gridFinancial.Rows[e.RowIndex].Cells["OpdID"].FormattedValue.ToString();
            string name = gridFinancial.Rows[e.RowIndex].Cells["OpdName"].FormattedValue.ToString();
            bool isActive = Convert.ToBoolean(gridFinancial.Rows[e.RowIndex].Cells["IsActive"].Value);
            if (!string.IsNullOrWhiteSpace(id))
            {
                txtID.Text = id;
                txtName.Text = name;
            }
        }
    }

    public class FinancialModel
    {
        public int FinancialID { get; set; }
        public string FinancialName { get; set; }
    }
}
