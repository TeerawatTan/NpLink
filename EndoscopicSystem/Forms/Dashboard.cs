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
        List<AppointmentModel> listData;
        List<v_AppointmentDetails> appList;
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
            _dropdownListService.DropdownInstrument(cbbInstrument, 1);
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
                appList = db.v_AppointmentDetails != null ?
                    db.v_AppointmentDetails
                        .Where(x => x.AppointmentDate.Value >= startDate && x.AppointmentDate.Value <= endDate)
                        .ToList() : new List<v_AppointmentDetails>();
                if (appList == null) return;

                if (!string.IsNullOrWhiteSpace(hn))
                {
                    appList = appList.Where(x => x.HN.Contains(hn)).ToList();
                }
                if (!string.IsNullOrWhiteSpace(fullName))
                {
                    appList = appList.Where(x => x.Fullname.Contains(fullName)).ToList();
                }
                listData = new List<AppointmentModel>();
                if (appList.Count > 0)
                {
                    int i = 1;
                    foreach (var item in appList.OrderBy(x => x.AppointmentDate))
                    {
                        var model = new AppointmentModel();
                        model.No = i++;
                        model.AppointmentID = item.AppointmentID;
                        model.HN = item.HN;
                        model.Fullname = item.Fullname;
                        model.Symptom = item.Symptom;
                        model.ProcedureID = item.ProcedureID.Value;
                        model.Procedure = item.ProcedureName;
                        model.EndoscopicRoom = item.EndoscopicRoom;
                        model.Doctor = item.Doctor;
                        model.EndoscopicCheck = item.EndoscopicCheck ?? false;
                        model.AppointmentDate = item.AppointmentDate;
                        model.EndoscopicID = item.EndoscopicID ?? 0;
                        listData.Add(model);
                    }
                }
                gridQueue.DataSource = listData;
                gridQueue.Columns["AppointmentID"].Visible = false;
                gridQueue.Columns["ProcedureID"].Visible = false;
                gridQueue.Columns["EndoscopicID"].Visible = false;
            }
            catch (Exception)
            {

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
                }
            }
            catch (Exception)
            {

            }
        }

        private void LoadChartCountInstrument(int instrumentId)
        {
            List<ChartInstrumentModel> data = new List<ChartInstrumentModel>()
            {
                new ChartInstrumentModel() { InstrumentID = 1, CountInstrument = 5, InstrumentName = "Test1" },
                new ChartInstrumentModel() { InstrumentID = 2, CountInstrument = 8, InstrumentName = "Test2" },
                new ChartInstrumentModel() { InstrumentID = 3, CountInstrument = 2, InstrumentName = "Test3" },
            };
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
