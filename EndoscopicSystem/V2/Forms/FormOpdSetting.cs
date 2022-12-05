﻿using EndoscopicSystem.Constants;
using EndoscopicSystem.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace EndoscopicSystem.V2.Forms
{
    public partial class FormOpdSetting : Form
    {
        private readonly EndoscopicEntities _db = new EndoscopicEntities();
        public FormOpdSetting()
        {
            InitializeComponent();
        }

        private void LoadData(List<OpdList> data)
        {
            var list = new List<OpdModel>();
            if (data != null && data.Count > 0)
            {
                foreach (var item in data)
                {
                    var model = new OpdModel();
                    model.OpdID = item.OpdID;
                    model.OpdName = item.OpdName;
                    model.IsActive = item.IsActive ?? false;

                    list.Add(model);
                }
            }
            gridOPD.DataSource = list;
        }

        private void GetData()
        {
            var data = _db.OpdLists.ToList();
            LoadData(data);
        }

        private void FormOpdSetting_Load(object sender, EventArgs e)
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
                        var data = _db.OpdLists.Where(x => x.OpdID == id).FirstOrDefault();
                        data.OpdName = txtName.Text;
                        data.IsActive = chkEnable.Checked;
                    }
                    else
                    {
                        OpdList data = new OpdList();
                        data.OpdName = txtName.Text;
                        data.IsActive = chkEnable.Checked;

                        _db.OpdLists.Add(data);
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
            var data = _db.OpdLists.ToList();
            if (!string.IsNullOrWhiteSpace(txtNameSearch.Text))
            {
                data = data.Where(x => (x.OpdName ?? "").Contains(txtNameSearch.Text)).ToList();
            }
            gridOPD.DataSource = data;
        }

        private void gridOPD_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string id = gridOPD.Rows[e.RowIndex].Cells["OpdID"].FormattedValue.ToString();
            string name = gridOPD.Rows[e.RowIndex].Cells["OpdName"].FormattedValue.ToString();
            bool isActive = Convert.ToBoolean(gridOPD.Rows[e.RowIndex].Cells["IsActive"].Value);
            if (!string.IsNullOrWhiteSpace(id))
            {
                txtID.Text = id;
                txtName.Text = name;
                chkEnable.Checked = isActive;
            }
        }
    }

    public class OpdModel
    {
        public int OpdID { get; set; }
        public string OpdName { get; set; }
        public bool IsActive { get; set; }
    }
}