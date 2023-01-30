using EndoscopicSystem.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EndoscopicSystem.V2.Forms
{
    public partial class FormProceed : Form
    {
        public static FormProceed Self;
        private int _id = 0, _endoscopicId = 0, _procedureId = 0, _appointmentId = 0, _stepId = 0;
        private string _hn, _pathImage, _pathVideo;
        private Form _activeForm = null, _useFormShow = null;

        public FormProceed(int id, string hn, int procedureId, int endoscopicId, int appointmentId, Form formActive = null)
        {
            InitializeComponent();

            this._id = id;
            this._hn = hn;
            this._procedureId = procedureId;
            this._endoscopicId = endoscopicId;
            this._appointmentId = appointmentId;
            this._useFormShow = formActive;
            Self = this;
        }

        public void OpenChildForm(Form childForm)
        {
            if (_activeForm != null)
                _activeForm.Close();
            _activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChild.Controls.Add(childForm);
            panelChild.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void FormProceed_Load(object sender, EventArgs e)
        {
            if (_useFormShow == null || _id == 0)
            {
                this.Close();
            }

            OpenChildForm(_useFormShow);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txbStep.Text) && txbStep.TextLength > 0)
            {
                try
                {
                    string[] splitParam = txbStep.Text.Split(',');
                    this._stepId = Convert.ToInt32(splitParam[0]);
                    this._pathImage = splitParam[1];
                    this._pathVideo = splitParam[2];

                    if (_stepId == 1)
                    {
                        OpenChildForm(new FormPreviewReport(_id, _hn, _procedureId, _appointmentId, _endoscopicId, _procedureId, _pathImage, _pathVideo));
                    }
                    else if (_stepId == 2)
                    {
                        OpenChildForm(new FormProcedure(_id, _hn, _procedureId, _appointmentId, _endoscopicId, _pathImage, _pathVideo));
                    }
                    else if (_stepId == 3)
                    {
                        OpenChildForm(new ReportEndoscopic(_hn, _procedureId, _endoscopicId));
                    }
                    else
                    {
                        _useFormShow = null;
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
