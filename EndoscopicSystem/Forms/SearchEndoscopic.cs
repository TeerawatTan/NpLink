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

namespace EndoscopicSystem.Forms
{
    public partial class SearchEndoscopic : Form
    {
        readonly EndoscopicEntities db = new EndoscopicEntities();
        protected readonly GetDropdownList list = new GetDropdownList();
        private readonly int UserID;
        public string hnNo = "";
        public int procedureId = 0;
        public int endscopicId = 0;

        public SearchEndoscopic(int userID)
        {
            InitializeComponent();
            UserID = userID;
        }

        private void SearchEndoscopic_Load(object sender, EventArgs e)
        {
            DropdownProcedure();
            LoadData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData(txtHN.Text, (int)cbbProcedureList.SelectedValue);
        }

        private void LoadData(string hn = "", int procedureId = 0)
        {
            var data = db.Endoscopics.Where(x => x.IsSaved).ToList();
            if (data.Count > 0)
            {
                var detail = (from en in db.Endoscopics
                              join ap in db.Appointments on en.EndoscopicID equals ap.EndoscopicID
                              join pa in db.Patients on en.PatientID equals pa.PatientID
                              join pl in db.ProcedureLists on en.ProcedureID equals pl.ProcedureID into lpl
                              from pl in lpl.DefaultIfEmpty()
                              join doc in db.Doctors on en.EndoscopistID equals doc.DoctorID into ldoc
                              from doc in ldoc.DefaultIfEmpty()
                              where en.IsSaved
                              select new EndoscopicDetailModel
                              {
                                  EndoscopicID = en.EndoscopicID,
                                  HN = pa.HN,
                                  FullName = pa.Fullname,
                                  ProcedureID = en.ProcedureID ?? 0,
                                  ProcedureName = pl.ProcedureName,
                                  OperationDate = en.CreateDate,
                                  DoctorName = doc.NameTH,
                                  AppointmentID = ap.AppointmentID
                              }).Take(30).OrderByDescending(x => x.OperationDate).ToList();
                if (!string.IsNullOrWhiteSpace(hn)) detail = detail.Where(x => hn.Equals(x.HN)).ToList();
                if (procedureId > 0) detail = detail.Where(x => x.ProcedureID == procedureId).ToList();
                gridEndoscopicList.DataSource = detail;
                gridEndoscopicList.Columns["EndoscopicID"].Visible = false;
                gridEndoscopicList.Columns["ProcedureID"].Visible = false;
                gridEndoscopicList.Columns["AppointmentID"].Visible = false;
            }
            else
            {
                gridEndoscopicList.DataSource = new List<EndoscopicDetailModel>();
                gridEndoscopicList.Columns["EndoscopicID"].Visible = false;
                gridEndoscopicList.Columns["ProcedureID"].Visible = false;
                gridEndoscopicList.Columns["AppointmentID"].Visible = false;
            }
        }
        public void DropdownProcedure()
        {
            cbbProcedureList.ValueMember = "ProcedureID";
            cbbProcedureList.DisplayMember = "ProcedureName";
            cbbProcedureList.DataSource = list.GetProcedureList();
            cbbProcedureList.SelectedIndex = 0;
        }

        private void gridEndoscopicList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gridEndoscopicList.CurrentRow.Index != -1)
                {
                    hnNo = gridEndoscopicList.CurrentRow.Cells["HN"].Value.ToString();
                    procedureId = (int)gridEndoscopicList.CurrentRow.Cells["ProcedureID"].Value;
                    endscopicId = (int)gridEndoscopicList.CurrentRow.Cells["EndoscopicID"].Value;
                    int appId = (int)gridEndoscopicList.CurrentRow.Cells["AppointmentID"].Value;

                    EndoscopicForm endo = new EndoscopicForm(UserID, hnNo, procedureId, endscopicId, appId);
                    endo.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    public class EndoscopicDetailModel
    {
        public int EndoscopicID { get; set; }
        public string HN { get; set; }
        public string FullName { get; set; }
        public int ProcedureID { get; set; }
        public string ProcedureName { get; set; }
        public DateTime? OperationDate { get; set; }
        public string DoctorName { get; set; }
        public int AppointmentID { get; set; }
    }
}
