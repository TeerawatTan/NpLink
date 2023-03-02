using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using EndoscopicSystem.Entities;
using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace EndoscopicSystem.Forms
{
    public partial class FormReport2 : Form
    {
        private string hnNo = "";
        private int procedureId = 0;
        private int endoscopicId;
        protected EndoscopicEntities db = new EndoscopicEntities();
        string _reportPath = Application.StartupPath.Replace("\\bin\\Debug", "") + @"\Report\";
        private string _pathFolderPDF = Application.StartupPath.Replace("\\bin\\Debug", "") + @"\Pdf\";

        public FormReport2(string hn, int proc, int endosId)
        {
            InitializeComponent();
            hnNo = hn;
            procedureId = proc;
            endoscopicId = endosId;
        }

        private void FormReport2_Load(object sender, EventArgs e)
        {
            crystalReportViewer1.Refresh();

            ReportDocument rprt = new ReportDocument();

            TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
            TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
            ConnectionInfo crConnectionInfo = new ConnectionInfo();
            Tables CrTables;

            crConnectionInfo.ServerName = ConfigurationManager.AppSettings["dataSource"];
            crConnectionInfo.DatabaseName = ConfigurationManager.AppSettings["catalog"];
            crConnectionInfo.UserID = ConfigurationManager.AppSettings["loginUser"];
            crConnectionInfo.Password = ConfigurationManager.AppSettings["loginPassword"];

            if (procedureId == 6) // EGD + Colonoscopy Report
            {
                rprt.Load(_reportPath + "ColonoscopyReport.rpt");
            }
            else
            {
                this.Close();
            }

            CrTables = rprt.Database.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
            {
                crtableLogoninfo = CrTable.LogOnInfo;
                crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                CrTable.ApplyLogOnInfo(crtableLogoninfo);
            }
            rprt.SetParameterValue("@hn", hnNo);
            rprt.SetParameterValue("@procedure", 2);
            rprt.SetParameterValue("@endoscopicId", endoscopicId);

            crystalReportViewer1.ReportSource = rprt;
            crystalReportViewer1.Refresh();

            string pathFolderPDFToSave = _pathFolderPDF + hnNo + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\";
            if (!Directory.Exists(pathFolderPDFToSave))
            {
                Directory.CreateDirectory(pathFolderPDFToSave);
            }
            string fileNamePDF = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string namaPDF = "pdf";
            string nameSave = namaPDF + "_" + fileNamePDF + ".pdf";
            string path = pathFolderPDFToSave + nameSave;
            rprt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, path);
        }
    }
}
