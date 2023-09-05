using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using EndoscopicSystem.Entities;
using EndoscopicSystem.Repository;
//using PQScan.PDFToImage;
using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using PdfiumViewer;

namespace EndoscopicSystem.Forms
{
    public partial class ReportEndoscopic : Form
    {
        private string hnNo = "", _fileNameSaved, _procedureName;
        private int procedureId = 0, _appointmentId = 0;
        private int endoscopicId;
        protected EndoscopicEntities _db = new EndoscopicEntities();
        private readonly GetDropdownList list = new GetDropdownList();
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
            var procedureList = list.GetProcedureList();
            if (procedureList != null)
            {
                _procedureName = procedureList.Where(w => w.ProcedureID == procedureId).FirstOrDefault().ProcedureName;
            }

            // Check count image path
            // 1-8 = 1:1
            // 9-20 = 1:2
            // 21-32 = 1:3

            int lastPage = 1;

            int count = _db.EndoscopicImages.Where(w => w.EndoscopicID == endoscopicId && w.ProcedureID == procedureId && !string.IsNullOrEmpty(w.ImagePath)).Select(s => s.ImagePath).AsEnumerable().Count();
            if (count > 8 && count <= 20)
            {
                lastPage = 2;
            }
            else if (count > 20 && count <= 32)
            {
                lastPage = 3;
            }

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

                ReportDocument subReport = new ReportDocument();
                subReport.Load(_reportPath + "ColonoscopyReport.rpt");
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

            DiskFileDestinationOptions dest = new DiskFileDestinationOptions();
            dest.DiskFileName = path;

            PdfFormatOptions formatOpt = new PdfFormatOptions();
            formatOpt.FirstPageNumber = 1;
            formatOpt.LastPageNumber = lastPage;
            //formatOpt.UsePageRange = false;
            formatOpt.UsePageRange = true;
            formatOpt.CreateBookmarksFromGroupTree = false;

            ExportOptions ex = new ExportOptions();
            ex.ExportDestinationType = ExportDestinationType.DiskFile;
            ex.ExportDestinationOptions = dest;
            ex.ExportFormatType = ExportFormatType.PortableDocFormat;
            ex.ExportFormatOptions = formatOpt;

            rprt.Export(ex);

            //rprt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, path);

            ExportToJpegFile(path);
        }

        private void ExportToJpegFile(string pdfPath)
        {
            try
            {
                string fileName = DateTime.Now.ToString("yyyyMMddhhmmsss");
                using (PdfDocument document = PdfDocument.Load(pdfPath))
                {
                    var page = document.PageSizes[0];
                    int pageCount = document.PageCount;

                    for (int i = 0; i < pageCount; i++)
                    {
                        using (var image = document.Render(i, (int)page.Width, (int)page.Height, 300, 300, false))
                        {
                            string pathFolderImgToSave = _pathFolderImage + hnNo + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + _procedureName + @"\" + _appointmentId + @"\";
                            if (!Directory.Exists(pathFolderImgToSave))
                            {
                                Directory.CreateDirectory(pathFolderImgToSave);
                            }

                            string outputFilePath = Path.Combine(pathFolderImgToSave, $"pdf_{fileName}{i+1}.jpg");
                            image.Save(outputFilePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        //private void ExportToJpegFile(string pathPdf)
        //{
        //    try
        //    {
        //        // Create an instance of PQScan.PDFToImage.PDFDocument object.
        //        PDFDocument pdfDoc = new PDFDocument();

        //        // Load PDF document from local file.
        //        pdfDoc.LoadPDF(pathPdf);

        //        // Get the total page count.
        //        int count = pdfDoc.PageCount;

        //        string pathFolderImgToSave = _pathFolderImage + hnNo + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + _procedureName + @"\" + _appointmentId + @"\";
        //        if (!Directory.Exists(pathFolderImgToSave))
        //        {
        //            Directory.CreateDirectory(pathFolderImgToSave);
        //        }

        //        for (int i = 0; i < count; i++)
        //        {
        //            // Convert PDF page to image.
        //            Bitmap jpgImage = pdfDoc.ToImage(i);

        //            string file = $"{pathFolderImgToSave}{_fileNameSaved}_{i}.jpg";

        //            // Save image with jpg file type.
        //            jpgImage.Save(file, System.Drawing.Imaging.ImageFormat.Jpeg);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        
    }
}
