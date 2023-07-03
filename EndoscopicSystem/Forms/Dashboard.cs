using EndoscopicSystem.Entities;
using EndoscopicSystem.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EndoscopicSystem.Constants;
using EndoscopicSystem.V2.Forms.src;

namespace EndoscopicSystem
{
    public partial class DashboardForm : Form
    {
        EndoscopicEntities db = new EndoscopicEntities();
        PatientRepository repo = new PatientRepository();
        protected readonly GetDropdownList list = new GetDropdownList();
        private int UserID;
        public string hnNo = "";
        public int procedureId = 0;
        public int appointmentId = 0;
        public int endoscopicId = 0;
        private readonly DropdownListService _dropdownListService = new DropdownListService();
        public DashboardForm(int userID)
        {
            InitializeComponent();
            UserID = userID;
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            if (UserID == 2) btnClearDataForSuperAdmin.Show();
            else btnClearDataForSuperAdmin.Hide();
            DropdownMonths();
            DropdownYear();
            DropdownOrder();
            LoadChartCountProcedure((int)cbbMonth.SelectedValue, (int)cbbYear.SelectedItem);
            LoadData();
            _dropdownListService.DropdownInstrument(cbbInstrument, 0);
            btnDeleteHn.Hide();
        }

        private void LoadData(string hn = "", string fullName = "")
        {
            try
            {
                string sDate = DateTime.Now.ToShortDateString() + " 00:00:00";
                string eDate = DateTime.Now.ToShortDateString() + " 23:59:59";
                DateTime startDate = Convert.ToDateTime(sDate);
                DateTime endDate = Convert.ToDateTime(eDate);
                //var viewAppointmentDetail = db.v_AppointmentDetails
                //                .Where(x => x.AppointmentDate.Value >= startDate && x.AppointmentDate.Value <= endDate)
                //                .AsQueryable();

                var viewAppointmentDetail = (from a in db.Appointments
                                             join p in db.Patients on a.PatientID equals p.PatientID
                                             join e in db.Endoscopics on a.EndoscopicID equals e.EndoscopicID into lE
                                             from e in lE.DefaultIfEmpty()
                                             join pro in db.ProcedureLists on a.ProcedureID equals pro.ProcedureID into lPro
                                             from pro in lPro.DefaultIfEmpty()
                                             join r in db.Rooms on a.RoomID equals r.RoomID into lR
                                             from r in lR.DefaultIfEmpty()
                                             join d in db.Doctors on a.DoctorID equals d.DoctorID into lD
                                             from d in lD.DefaultIfEmpty()
                                             select new AppointmentModel
                                             {
                                                 AppointmentID = a.AppointmentID,
                                                 HN = p.HN,
                                                 Fullname = p.Fullname,
                                                 Symptom = a.Symptom,
                                                 ProcedureID = a.ProcedureID ?? 0,
                                                 Procedure = pro.ProcedureName,
                                                 EndoscopicRoom = r.NameTH ?? r.NameEN,
                                                 Doctor = d.NameTH ?? d.NameEN,
                                                 AppointmentDate = a.AppointmentDate,
                                                 EndoscopicCheck = a.EndoscopicCheck ?? false,
                                                 EndoscopicID = a.EndoscopicID
                                             })
                                             .Where(x => x.AppointmentDate.Value >= startDate && x.AppointmentDate.Value <= endDate)
                                             .OrderBy(x => x.AppointmentDate)
                                             .AsQueryable();

                if (viewAppointmentDetail == null) return;

                if (!string.IsNullOrWhiteSpace(hn))
                {
                    viewAppointmentDetail = viewAppointmentDetail.Where(x => x.HN.Contains(hn)).AsQueryable();
                }
                if (!string.IsNullOrWhiteSpace(fullName))
                {
                    viewAppointmentDetail = viewAppointmentDetail.Where(x => x.Fullname.Contains(fullName)).AsQueryable();
                }

                List<AppointmentModel> listData = new List<AppointmentModel>();

                if (viewAppointmentDetail.Count() > 0)
                {
                    listData = viewAppointmentDetail.ToList();

                    int i = 1;
                    listData.ForEach(model => model.No = i++);
                }

                gridQueue.DataSource = listData;
                gridQueue.Columns["AppointmentID"].Visible = false;
                gridQueue.Columns["ProcedureID"].Visible = false;
                gridQueue.Columns["EndoscopicID"].Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData(txtHN.Text, txtFullName.Text);
        }

        private void LoadChartCountProcedure(int m, int y)
        {
            List<ChartModel> data = new List<ChartModel>();
            data = repo.GetsChart(m, y);
            chart1.DataSource = data;
            chart1.Series["Patient"].XValueMember = "ProcedureName";
            chart1.Series["Patient"].YValueMembers = "CountPatient";
            chart1.DataBind();
        }

        private void gridQueue_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gridQueue.CurrentRow.Index != -1)
                {
                    txtHNNo.Text = gridQueue.CurrentRow.Cells["HN"].Value.ToString();
                    txtProcedureId.Text = gridQueue.CurrentRow.Cells["ProcedureID"].Value.ToString();

                    hnNo = txtHNNo.Text;
                    procedureId = Convert.ToInt32(txtProcedureId.Text);
                    appointmentId = (int)gridQueue.CurrentRow.Cells["AppointmentID"].Value;
                    endoscopicId = Convert.ToInt32(gridQueue.CurrentRow.Cells["EndoscopicID"].Value ?? 0);

                    bool isCheck = (bool)gridQueue.CurrentRow.Cells["EndoscopicCheck"].Value;
                    if (!isCheck)
                        btnDeleteHn.Show();
                    else
                        btnDeleteHn.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void BtnGotoEndoscopic_Click(object sender, EventArgs e)
        {
            try
            {
                // Check Instrument
                var instruments = db.Instruments.ToList();
                if (instruments is null || instruments.Count == 0)
                {
                    this.Hide();
                    V2.Forms.FormSetting formSetting = new V2.Forms.FormSetting(UserID, "menuInstrumentSetting");
                    formSetting.ShowDialog();
                    formSetting = null;
                    this.Show();
                    return;
                }

                if (!string.IsNullOrWhiteSpace(txtHNNo.Text) && !string.IsNullOrWhiteSpace(txtProcedureId.Text))
                {
                    hnNo = txtHNNo.Text;
                    procedureId = Convert.ToInt32(txtProcedureId.Text);

                    //EndoscopicForm endo = new EndoscopicForm(UserID, hnNo, procedureId, endoscopicId, appointmentId);
                    //endo.Show();
                    this.Hide();
                    V2.Forms.FormPatientConfirm formPatientConfirm = new V2.Forms.FormPatientConfirm(UserID, hnNo, procedureId, endoscopicId, appointmentId);
                    formPatientConfirm.ShowDialog();
                    formPatientConfirm = null;

                    this.Show();

                    LoadChartCountProcedure((int)cbbMonth.SelectedValue, (int)cbbYear.SelectedItem);
                    LoadData();
                }
            }
            catch (Exception)
            {

            }
        }

        private void LoadChartCountInstrument(int instrumentId)
        {
            List<ChartInstrumentModel> data = new List<ChartInstrumentModel>();
            data = repo.GetChartInstruments(instrumentId);
            chart1.DataSource = data;
            chart1.Series["Patient"].XValueMember = "InstrumentName";
            chart1.Series["Patient"].YValueMembers = "CountInstrument";
            chart1.DataBind();
        }

        private void btnShowChart_Click(object sender, EventArgs e)
        {
            int month = 0;
            int year = 0;
            if (cbbMonth.Enabled)
            {
                month = Convert.ToInt32(cbbMonth.SelectedValue ?? 0);
                year = Convert.ToInt32(cbbYear.SelectedItem);
            }
            else
            {
                year = Convert.ToInt32(cbbYear.SelectedItem);
            }
            LoadChartCountProcedure(month, year);
        }
        public void DropdownMonths()
        {
            cbbMonth.DataSource = Constant.GetMonths();
            cbbMonth.ValueMember = "MonthID";
            cbbMonth.DisplayMember = "MonthName";
            cbbMonth.SelectedIndex = DateTime.Now.Month - 1;
        }
        public void DropdownYear()
        {
            int year = DateTime.Now.Year;

            for (int i = year - 5; i <= year; i++)
            {
                cbbYear.Items.Add(i);
            }
            cbbYear.SelectedItem = year;
        }

        private void btnDeleteHn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(hnNo) && procedureId > 0)
            {
                if (MessageBox.Show("ต้องการ Delete หรือไม่ ?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var app = db.Appointments.FirstOrDefault(x => x.HN == hnNo && x.ProcedureID == procedureId);
                    if (app != null)
                    {
                        db.Appointments.Remove(app);
                        db.SaveChanges();
                        LoadChartCountProcedure((int)cbbMonth.SelectedValue, (int)cbbYear.SelectedItem);
                        LoadData();
                    }
                }
            }
        }

        private void DropdownOrder()
        {
            var order = new List<OrderModel>();
            order.Insert(0, new OrderModel { ID = 1, Name = "รายเดือน" });
            order.Insert(1, new OrderModel { ID = 2, Name = "รายปี" });
            cbbOrder.DataSource = order;
            cbbOrder.ValueMember = "ID";
            cbbOrder.DisplayMember = "Name";
        }

        private void cbbOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbOrder.SelectedIndex == 1)
            {
                cbbMonth.Enabled = false;
            }
            else
            {
                cbbMonth.Enabled = true;
            }
        }

        private void btnClearDataForSuperAdmin_Click(object sender, EventArgs e)
        {
            if (UserID == 2)
            {
                if (MessageBox.Show("ต้องการล้างข้อมูลหรือไม่ ?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    db.CleanData(1);
                    Application.Exit();
                    Application.ExitThread();
                }
            }
        }

        private void btnShowChartInstrument_Click(object sender, EventArgs e)
        {
            LoadChartCountInstrument(Convert.ToInt32(cbbInstrument.SelectedValue ?? 0));
        }
    }

    public class AppointmentModel
    {
        public int No { get; set; }
        public int AppointmentID { get; set; }
        public string HN { get; set; }
        public string Fullname { get; set; }
        public string Symptom { get; set; }
        public int ProcedureID { get; set; }
        public string Procedure { get; set; }
        public string EndoscopicRoom { get; set; }
        public string Doctor { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public bool EndoscopicCheck { get; set; }
        public int? EndoscopicID { get; set; }
    }

    public class OrderModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
