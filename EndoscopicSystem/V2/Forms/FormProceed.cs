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
        private int _id = 0, _patientId = 0, _endoscopicId = 0, _procedureId = 0, _appointmentId = 0, _stepId = 0;
        private string _hn, _pathImage, _pathVideo;
        private Form _activeForm = null, _useFormShow = null;
        private bool _isEgdAndColono = false;
        public FormProceed(int id, string hn = null, int patientId = 0, int procedureId = 0, int endoscopicId = 0, int appointmentId = 0, Form formActive = null, bool isEgdAndColono = false)
        {
            InitializeComponent();

            this._id = id;
            this._hn = hn;
            this._patientId = patientId;
            this._procedureId = procedureId;
            this._endoscopicId = endoscopicId;
            this._appointmentId = appointmentId;
            this._useFormShow = formActive;
            this._isEgdAndColono = isEgdAndColono;
            Self = this;
        }

        public void OpenChildForm(Form childForm)
        {
            //if (childForm == null)
            //{
            //    return;
            //}

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
            if (_id == 0)
            {
                this.Close();
            }

            if (_isEgdAndColono)
            {
                txbCheckHasEgdAndColono.Text = "1";
            }
            else
            {
                txbCheckHasEgdAndColono.Text = "0";
            }

            _useFormShow = new FormLive(_id, _hn, _procedureId, _endoscopicId, _appointmentId, false);

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
                    string hasEgdAndColonoStep = txbCheckHasEgdAndColono.Text.Trim();

                    DateTime? dateStartRec = dtRecordStart.Value;
                    DateTime? dateEndRec = dtRecordEnd.Value;

                    if (_stepId == 1)
                    {
                        OpenChildForm(new FormPreviewReport(_id, _hn, _procedureId, _appointmentId, _endoscopicId, _patientId, _pathImage, _pathVideo, true, dateStartRec, dateEndRec));
                    }
                    else if (_stepId == 2)
                    {
                        OpenChildForm(new FormProcedure(_id, _hn, _procedureId, _appointmentId, _endoscopicId, _pathImage, _pathVideo));
                    }
                    else if (_stepId == 3)
                    {
                        ReportEndoscopic reportEndoscopic = new ReportEndoscopic(_hn, _procedureId, _endoscopicId, _appointmentId);
                        reportEndoscopic.ShowDialog();
                        _activeForm = reportEndoscopic;
                        if (_procedureId == 6)
                        {
                            FormReport2 formReport2 = new FormReport2(_hn, _procedureId, _endoscopicId);
                            formReport2.Show();
                        }
                        _activeForm.Dispose();
                        _activeForm.Close();
                    }
                    else if (_stepId == 4)
                    {
                        if (hasEgdAndColonoStep.Equals("1"))
                            OpenChildForm(new FormLive(_id, _hn, _procedureId, _endoscopicId, _appointmentId, true));
                        else if (hasEgdAndColonoStep.Equals("2"))
                            OpenChildForm(new FormPreviewReport(_id, _hn, _procedureId, _appointmentId, _endoscopicId, _patientId, _pathImage, _pathVideo, false, dateStartRec, dateEndRec));
                    }
                    else
                    {
                        _useFormShow = null;
                        if (_activeForm != null)
                        {
                            _activeForm.Close();
                            _activeForm = null;
                        }
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void FormProceed_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormProceed.Self.txbStep.Text = "0" + ",,";
        }
    }
}
