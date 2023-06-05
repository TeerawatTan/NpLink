using EndoscopicSystem.Constants;
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
        public string hnNo = "", _pageName = "";
        public int patientId = 0, procedureId = 0, endoscopicId = 0, appointmentId = 0;
        public SearchPatientForm(int userID, string page)
        {
            InitializeComponent();
            UserID = userID;
            _pageName = page;
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
            gridPatient.Columns["EndoscopicID"].Visible = false;
            gridPatient.Columns["AppointmentID"].Visible = false;

            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            gridPatient.Columns.Add(btn);
            btn.HeaderText = "";

            if (_pageName == Constant.PageName.SEARCH_PATIENT_PAGE)
            {
                btn.Text = "Endoscopic Room";
                btn.Name = "btnGotoEndoscopicRoom";
            }
            else
            {
                btn.Text = "Send To PACS";
                btn.Name = "btnGotoSendToPACS";
            }

            btn.UseColumnTextForButtonValue = true;
        }

        private void gridPatient_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gridPatient.CurrentRow.Index >= 0)
                {
                    hnNo = gridPatient.CurrentRow.Cells["HN"].Value.ToString();
                    procedureId = (int)gridPatient.CurrentRow.Cells["ProcedureID"].Value;

                    if (_pageName == Constant.PageName.SEARCH_PATIENT_PAGE)
                    {
                        PatientForm patient = new PatientForm(UserID, hnNo, procedureId);
                        patient.ShowDialog();
                    }
                    else if (_pageName == Constant.PageName.SEND_PACS_PAGE)
                    {
                        V2.Forms.FormSendPACS formSendPACS = new V2.Forms.FormSendPACS(UserID, appointmentId, hnNo);
                        formSendPACS.ShowDialog();
                        formSendPACS = null;
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SearchPatientForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void gridPatient_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridPatient.CurrentRow.Index >= 0 && e.ColumnIndex == 13)
            {
                try
                {
                    hnNo = gridPatient.CurrentRow.Cells["HN"].Value.ToString();
                    patientId = (int?)gridPatient.CurrentRow.Cells["PatientID"].Value ?? 0;
                    procedureId = (int?)gridPatient.CurrentRow.Cells["ProcedureID"].Value ?? 0;
                    endoscopicId = (int?)gridPatient.CurrentRow.Cells["EndoscopicID"].Value ?? 0;
                    appointmentId = (int?)gridPatient.CurrentRow.Cells["AppointmentID"].Value ?? 0;

                    if (string.IsNullOrWhiteSpace(hnNo))
                        return;
                    if (procedureId <= 0)
                        return;
                    if (endoscopicId <= 0)
                        return;

                    this.Close();

                    try
                    {
                        if (_pageName == Constant.PageName.SEARCH_PATIENT_PAGE)
                        {
                            // Form Panel
                            V2.Forms.FormProceed formProceed = new V2.Forms.FormProceed(UserID, hnNo, procedureId, endoscopicId, appointmentId);
                            formProceed.ShowDialog();
                            formProceed = null;
                        }
                        else if (_pageName == Constant.PageName.SEND_PACS_PAGE)
                        {
                            V2.Forms.FormSendPACS formSendPACS = new V2.Forms.FormSendPACS(UserID, appointmentId, hnNo);
                            formSendPACS.ShowDialog();
                            formSendPACS = null;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
