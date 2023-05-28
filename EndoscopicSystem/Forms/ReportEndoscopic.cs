using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using EndoscopicSystem.Entities;
using PQScan.PDFToImage;
using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EndoscopicSystem.Forms
{
    public partial class ReportEndoscopic : Form
    {
        private string hnNo = "", _fileNameSaved;
        private int procedureId = 0, _appointmentId = 0;
        private int endoscopicId;
        protected EndoscopicEntities db = new EndoscopicEntities();
        private string _reportPath = Application.StartupPath.Replace("\\bin\\Debug", "") + @"\Report\";
        private string _pathFolderPDF = ConfigurationManager.AppSettings["pathSavePdf"];
        private string _dicomPath = Application.StartupPath.Replace("\\bin\\Debug", "") + @"\Dicom\";
        private string _pathFolderImage = ConfigurationManager.AppSettings["pathSaveImageCapture"];
        public ReportEndoscopic(string hn, int proc, int endosId, int appointmentId)
        {
            InitializeComponent();
            hnNo = hn;
            procedureId = proc;
            endoscopicId = endosId;
            _appointmentId = appointmentId;
        }

        private void ReportEndoscopic_Load(object sender, EventArgs e)
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

            if (procedureId == 1) // Gastoscopy Report
            {
                rprt.Load(_reportPath + "GastroscoryReport.rpt");
            }
            else if (procedureId == 2) // Colonoscopy Report
            {
                rprt.Load(_reportPath + "ColonoscopyReport.rpt");
            }
            else if (procedureId == 3) // Endoscopic Report
            {
                rprt.Load(_reportPath + "EndoscopicReport.rpt");
            }
            else if (procedureId == 4) // Bronchoscopy Report
            {
                rprt.Load(_reportPath + "BronchoscopyReport.rpt");
            }
            else if (procedureId == 5) // Ent Report
            {
                rprt.Load(_reportPath + "EntReport.rpt");
            }
            else if (procedureId == 6) // EGD + Colonoscopy Report
            {
                rprt.Load(_reportPath + "GastroscoryReport.rpt");
            }
            else if (procedureId == 7) // EUS Report
            {
                //rprt.Load(_reportPath + "");
            }
            else if (procedureId == 8) // Laparoscopic Report
            {
                rprt.Load(_reportPath + "LaparoscopicReport.rpt");
            }
            else
            {
                throw new Exception("Error : Not found report.");
            }

            CrTables = rprt.Database.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
            {
                crtableLogoninfo = CrTable.LogOnInfo;
                crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                CrTable.ApplyLogOnInfo(crtableLogoninfo);
            }
            rprt.SetParameterValue("@hn", hnNo);
            rprt.SetParameterValue("@procedure", procedureId == 6 ? 1 : procedureId);
            rprt.SetParameterValue("@endoscopicId", endoscopicId);

            crystalReportViewer1.ReportSource = rprt;
            crystalReportViewer1.Refresh();

            string _pathFolderPDFToSave = _pathFolderPDF + hnNo + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\";
            if (!Directory.Exists(_pathFolderPDFToSave))
            {
                Directory.CreateDirectory(_pathFolderPDFToSave);
            }
            string fileNamePDF = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string namaPDF = "pdf";
            _fileNameSaved = namaPDF + "_" + fileNamePDF + ".pdf";
            string path = _pathFolderPDFToSave + _fileNameSaved;
            rprt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, path);

            ExportToDicomFile(path);
        }

        private void ExportToDicomFile(string pathPdf)
        {
            try
            {
                // Create an instance of PQScan.PDFToImage.PDFDocument object.
                PDFDocument pdfDoc = new PDFDocument();

                // Load PDF document from local file.
                pdfDoc.LoadPDF(pathPdf);

                // Get the total page count.
                int count = pdfDoc.PageCount;

                string pathFolderImgToSave = _pathFolderImage + hnNo + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + procedureId + @"\" + _appointmentId + @"\";
                if (!Directory.Exists(pathFolderImgToSave))
                {
                    Directory.CreateDirectory(pathFolderImgToSave);
                }

                for (int i = 0; i < count; i++)
                {
                    // Convert PDF page to image.
                    Bitmap jpgImage = pdfDoc.ToImage(i);

                    string file = $"{pathFolderImgToSave}{_fileNameSaved}_{i}.jpg";

                    // Save image with jpg file type.
                    jpgImage.Save(file, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
    }
}
