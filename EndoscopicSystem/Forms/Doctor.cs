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
    public partial class DoctorForm : Form
    {
        EndoscopicEntities db = new EndoscopicEntities();
        private readonly int UserID;
        public DoctorForm(int userID)
        {
            InitializeComponent();
            UserID = userID;
        }

        private void DoctorForm_Load(object sender, EventArgs e)
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
                    if (!string.IsNullOrWhiteSpace(txtDoctorID.Text))
                    {
                        int id = Convert.ToInt32(txtDoctorID.Text);
                        var data = db.Doctors.Where(x => x.DoctorID == id).FirstOrDefault();
                        data.NameTH = txtNameTH.Text;
                        data.NameEN = txtNameEN.Text;
                        data.IsActive = chkEnable.Checked;
                        data.UpdateBy = UserID;
                        data.UpdateDate = DateTime.Now;
                    }
                    else
                    {
                        Doctor doctor = new Doctor();
                        doctor.NameEN = txtNameEN.Text;
                        doctor.NameTH = txtNameTH.Text;
                        doctor.IsActive = chkEnable.Checked;
                        doctor.CreateBy = UserID;
                        doctor.CreateDate = DateTime.Now;

                        db.Doctors.Add(doctor);
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

        private void GetData()
        {
            var data = db.Doctors.ToList();
            LoadData(data);
        }

        private void LoadData(List<Doctor> data)
        {
            var list = new List<DoctorModel>();
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    var model = new DoctorModel();
                    model.DoctorID = item.DoctorID;
                    model.NameTH = item.NameTH;
                    model.NameEN = item.NameEN;
                    model.IsActive = item.IsActive ?? false;

                    list.Add(model);
                }
            }
            gridDoctor.DataSource = list;
        }

        private void gridDoctor_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string id = gridDoctor.Rows[e.RowIndex].Cells["DoctorID"].FormattedValue.ToString();
            string nameTh = gridDoctor.Rows[e.RowIndex].Cells["NameTH"].FormattedValue.ToString();
            string nameEn = gridDoctor.Rows[e.RowIndex].Cells["NameEN"].FormattedValue.ToString();
            bool isActive = Convert.ToBoolean(gridDoctor.Rows[e.RowIndex].Cells["IsActive"].Value);
            if (!string.IsNullOrWhiteSpace(id))
            {
                txtDoctorID.Text = id;
                txtNameTH.Text = nameTh;
                txtNameEN.Text = nameEn;
                chkEnable.Checked = isActive;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var data = db.Doctors.ToList();
            if (!string.IsNullOrWhiteSpace(txtFullname.Text))
            {
                data = data.Where(x => (x.NameTH ?? "").Contains(txtFullname.Text) || (x.NameEN ?? "").Contains(txtFullname.Text)).ToList();
            }
            LoadData(data);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.Controls.ClearControls();
            txtNameEN.Focus();
            GetData();
        }
    }

    public class DoctorModel
    {
        public int DoctorID { get; set; }
        public string NameEN { get; set; }
        public string NameTH { get; set; }
        public bool IsActive { get; set; }
    }
}
