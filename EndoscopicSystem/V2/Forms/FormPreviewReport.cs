using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EndoscopicSystem.V2.Forms
{
    public partial class FormPreviewReport : Form
    {
        private string _pathFolderImage = Application.StartupPath.Replace("\\bin\\Debug", "") + @"\ImageCapture\";
        private string _pathFolderImageToSave, _hnNo;
        private int _id, _procedureId, _appointmentId;

        public FormPreviewReport(int id, string hn, int procId, int appId)
        {
            InitializeComponent();

            this._id = id;
            this._hnNo = hn;
            this._procedureId = procId;
            this._appointmentId = appId;
        }

        private void FormPreviewReport_Load(object sender, EventArgs e)
        {
            _pathFolderImageToSave = _pathFolderImage + _hnNo + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + _appointmentId + @"\";
            listView1.Items.Clear();
            if (!string.IsNullOrWhiteSpace(_pathFolderImageToSave))
            {
                DirectoryInfo dinfo = new DirectoryInfo(_pathFolderImageToSave);
                FileInfo[] files = dinfo.GetFiles("*.jpg");
                foreach (var item in files.OrderByDescending(o => o.CreationTime).ToList())
                {
                    ListViewItem listView = new ListViewItem();
                    listView.Text = item.Name;
                    listView1.Items.Add(listView);
                }
            }
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            var item = listView1.SelectedItems[0].Text;
            pictureBox1.Image = Image.FromFile(_pathFolderImageToSave + @"\" + item);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Update();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            FormProcedure formProcedure = new FormProcedure(_id, _hnNo, _procedureId, _appointmentId);
            formProcedure.ShowDialog();
        }
    }
}
