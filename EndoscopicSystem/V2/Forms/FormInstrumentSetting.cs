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
    public partial class FormInstrumentSetting : Form
    {
        private readonly EndoscopicEntities _db = new EndoscopicEntities();
        private int _id;
        public FormInstrumentSetting(int id)
        {
            InitializeComponent();

            this._id = id;
        }

        private void LoadData(List<Instrument> data)
        {
            var list = new List<InstrumentModel>();
            if (data != null && data.Count > 0)
            {
                foreach (var item in data)
                {
                    var model = new InstrumentModel();
                    model.No = item.ID;
                    model.InstrumentName = item.Code;
                    model.SerialNumber = item.SerialNumber;
                    model.IsActive = item.IsActive;

                    list.Add(model);
                }
            }
            gridInstrument.DataSource = list;
        }

        private void GetData()
        {
            var data = _db.Instruments.ToList();
            LoadData(data);
        }

        private void FormInstrumentSetting_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txbName;
            txbName.Focus();
            GetData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txbName.Text))
                {
                    if (!string.IsNullOrWhiteSpace(txtID.Text))
                    {
                        int id = Convert.ToInt32(txtID.Text);
                        var data = _db.Instruments.Where(x => x.ID == id).FirstOrDefault();
                        data.Code = txbName.Text;
                        data.SerialNumber = txbSerialNo.Text;
                        data.IsActive = chkEnable.Checked;
                    }
                    else
                    {
                        Instrument data = new Instrument();
                        data.Code = txbName.Text;
                        data.SerialNumber = txbSerialNo.Text;
                        data.IsActive = chkEnable.Checked;

                        _db.Instruments.Add(data);
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
            txbName.Focus();
            GetData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var data = _db.Instruments.ToList();
            if (!string.IsNullOrWhiteSpace(txtNameSearch.Text) && txtNameSearch.TextLength > 0)
            {
                data = data.Where(x => x.Code.Contains(txtNameSearch.Text) || x.SerialNumber.Contains(txtNameSearch.Text)).ToList();
            }
            gridInstrument.DataSource = data;
        }
        private void gridInstrument_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string id = gridInstrument.Rows[e.RowIndex].Cells["No"].FormattedValue.ToString();
            string instrumentName = gridInstrument.Rows[e.RowIndex].Cells["InstrumentName"].FormattedValue.ToString();
            string serialNumber = gridInstrument.Rows[e.RowIndex].Cells["SerialNumber"].FormattedValue.ToString();
            bool isActive = Convert.ToBoolean(gridInstrument.Rows[e.RowIndex].Cells["IsActive"].Value);
            if (!string.IsNullOrWhiteSpace(id))
            {
                txtID.Text = id;
                txbName.Text = instrumentName;
                txbSerialNo.Text = serialNumber;
                chkEnable.Checked = isActive;
            }
        }
    }

    public class InstrumentModel
    {
        public int No { get; set; }
        public string InstrumentName { get; set; }
        public string SerialNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
