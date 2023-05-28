using EndoscopicSystem.Constants;
using EndoscopicSystem.Entities;
using EndoscopicSystem.Repository;
using EndoscopicSystem.V2.Forms.src;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EndoscopicSystem.V2.Forms
{
    public partial class FormDashboardStatistic : Form
    {
        EndoscopicEntities _db = new EndoscopicEntities();
        PatientRepository _repo = new PatientRepository();
        protected readonly GetDropdownList _dropdownList = new GetDropdownList();
        private int _id;
        private readonly DropdownListService _dropdownListService = new DropdownListService();

        public FormDashboardStatistic(int userId)
        {
            InitializeComponent();
            this._id = userId;
        }

        private void FormDashboardStatistic_Load(object sender, EventArgs e)
        {
            DropdownMonths();
            DropdownYear();
            DropdownOrder();
            _dropdownListService.DropdownInstrument(cbbInstrument, 0);
            LoadChartCountProcedure((int)cbbMonth.SelectedValue, (int)cbbYear.SelectedItem);
            LoadChartCountInstrument(Convert.ToInt32(cbbInstrument.SelectedValue ?? 0));
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

        private void btnShowChartInstrument_Click(object sender, EventArgs e)
        {
            LoadChartCountInstrument(Convert.ToInt32(cbbInstrument.SelectedValue ?? 0));
        }


        private void LoadChartCountProcedure(int m, int y)
        {
            List<ChartModel> data = new List<ChartModel>();
            data = _repo.GetsChart(m, y);
            chart1.DataSource = data;
            chart1.Series["Patient"].XValueMember = "ProcedureName";
            chart1.Series["Patient"].YValueMembers = "CountPatient";
            chart1.DataBind();
        }

        private void LoadChartCountInstrument(int instrumentId)
        {
            List<ChartInstrumentModel> data = new List<ChartInstrumentModel>();
            data = _repo.GetChartInstruments(instrumentId);
            chart2.DataSource = data;
            chart2.Series["Patient"].XValueMember = "InstrumentName";
            chart2.Series["Patient"].YValueMembers = "CountInstrument";
            chart2.DataBind();
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
        private void DropdownOrder()
        {
            var order = new List<OrderModel>();
            order.Insert(0, new OrderModel { ID = 1, Name = "รายเดือน" });
            order.Insert(1, new OrderModel { ID = 2, Name = "รายปี" });
            cbbOrder.DataSource = order;
            cbbOrder.ValueMember = "ID";
            cbbOrder.DisplayMember = "Name";
        }
    }
}
