using EndoscopicSystem.Entities;
using EndoscopicSystem.Repository;
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
    public partial class SearchPatientForm : Form
    {
        readonly EndoscopicEntities db = new EndoscopicEntities();
        protected readonly GetDropdownList list = new GetDropdownList();
        private readonly int UserID;
        public string hnNo = "";
        public int procedureId = 0;
        public SearchPatientForm(int userID)
        {
            InitializeComponent();
            UserID = userID;
        }

        private void SearchPatientForm_Load(object sender, EventArgs e)
        {
            var data = db.v_PatientList.ToList();
            LoadData(data);
            string sDate = DateTime.Now.ToShortDateString() + " 00:00:00";
            string eDate = DateTime.Now.ToShortDateString() + " 23:59:59";
            DateTime startDate = Convert.ToDateTime(sDate);
            DateTime endDate = Convert.ToDateTime(eDate);
            dtStartDate.Value = startDate;
            dtEndDate.Value = endDate;
            DropdownRoom();
            DropdownDoctor();
        }

        public void DropdownRoom()
        {
            cbbStation.ValueMember = "RoomID";
            cbbStation.DisplayMember = "NameTH";
            cbbStation.DataSource = list.GetRoomList();
            cbbStation.SelectedIndex = 0;
        }
        public void DropdownDoctor()
        {
            cbbDoctor.ValueMember = "DoctorID";
            cbbDoctor.DisplayMember = "NameTH";
            cbbDoctor.DataSource = list.GetEndoscopistList();
            cbbDoctor.SelectedIndex = 0;
        }
        
        private void btnSearch_Click(object sender, EventArgs e)
        {
            var data = db.v_PatientList.ToList();
            if (data.Count > 0)
            {
                if (!string.IsNullOrWhiteSpace(txtHN.Text))
                {
                    data = data.Where(x => x.HN.Equals(txtHN.Text)).ToList();
                }
                if (dtStartDate.Value != null || dtEndDate.Value != null)
                {
                    data = data.Where(x => x.AppointmentDate.Value >= dtStartDate.Value && x.AppointmentDate.Value <= dtEndDate.Value).ToList();
                }
                if (cbbStation.SelectedIndex > 0)
                {
                    data = data.Where(x =>
                    {
                        return x.RoomID == (int)cbbStation.SelectedValue;
                    }).ToList();
                }
                if (cbbDoctor.SelectedIndex > 0)
                {
                    data = data.Where(x =>
                    {
                        return x.DoctorID == (int)cbbDoctor.SelectedValue;
                    }).ToList();
                }
                LoadData(data);
            }
        }

        private void LoadData(List<v_PatientList> data)
        {
            gridPatient.DataSource = data;
            gridPatient.Columns["PatientID"].Visible = false;
            gridPatient.Columns["DoctorID"].Visible = false;
            gridPatient.Columns["ProcedureID"].Visible = false;
            gridPatient.Columns["RoomID"].Visible = false;
        }

        private void gridPatient_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gridPatient.CurrentRow.Index != -1)
                {
                    hnNo = gridPatient.CurrentRow.Cells["HN"].Value.ToString();
                    procedureId = (int)gridPatient.CurrentRow.Cells["ProcedureID"].Value;

                    PatientForm patient = new PatientForm(UserID, hnNo, procedureId);
                    patient.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
