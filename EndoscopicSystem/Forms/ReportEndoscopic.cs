using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using EndoscopicSystem.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EndoscopicSystem.Forms
{
    public partial class ReportEndoscopic : Form
    {
        private string hnNo = "";
        private int procedureId = 0;
        private int endoscopicId;
        protected EndoscopicEntities db = new EndoscopicEntities();
        string path = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));
        string reportPath = Application.StartupPath.Replace("\\bin\\Debug", "") + @"\Report\";
        public ReportEndoscopic(string hn, int proc, int endosId)
        {
            InitializeComponent();
            hnNo = hn;
            procedureId = proc;
            endoscopicId = endosId;
        }

        private void ReportEndoscopic_Load(object sender, EventArgs e)
        {
            ReportDocument rprt = new ReportDocument();

            TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
            TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
            ConnectionInfo crConnectionInfo = new ConnectionInfo();
            Tables CrTables;

            crConnectionInfo.ServerName = ConfigurationManager.AppSettings["dataSource"];
            crConnectionInfo.DatabaseName = ConfigurationManager.AppSettings["catalog"];
            crConnectionInfo.UserID = ConfigurationManager.AppSettings["loginUser"];
            crConnectionInfo.Password = ConfigurationManager.AppSettings["loginPassword"];

            if (procedureId == 1)
            {
                rprt.Load(reportPath + "GastroscoryReport.rpt");
            }
            else if (procedureId == 2)
            {
                rprt.Load(reportPath + "ColonoscopyReport.rpt");
            }
            else if (procedureId == 3)
            {
                rprt.Load(reportPath + "EndoscopicReport.rpt");
            }
            else if (procedureId == 4)
            {
                rprt.Load(reportPath + "BronchoscopyReport.rpt");
            }
            else if (procedureId == 5)
            {
                rprt.Load(reportPath + "EntReport.rpt");
            }
            else if (procedureId == 6)
            {
                rprt.Load(reportPath + "LaparoscopicReport.rpt");
            }
            else
            {
                return;
            }

            CrTables = rprt.Database.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
            {
                crtableLogoninfo = CrTable.LogOnInfo;
                crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                CrTable.ApplyLogOnInfo(crtableLogoninfo);
            }
            rprt.SetParameterValue("@hn", hnNo);
            rprt.SetParameterValue("@procedure", procedureId);
            rprt.SetParameterValue("@endoscopicId", endoscopicId);

            crystalReportViewer1.ReportSource = rprt;
            crystalReportViewer1.Refresh();
        }
    }
}
