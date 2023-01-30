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
    public partial class FormLoading : Form
    {
        private Timer _timer = new Timer();
        public FormLoading()
        {
            InitializeComponent();

            _timer.Interval = 80;
            _timer.Enabled = true;
            _timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < 100)
            {
                progressBar1.Value++;
            }
            else
            {
                _timer.Enabled = false;
                //this.Hide();
                //ReportEndoscopic reportForm = new ReportEndoscopic(_hnNo, _procedureId, _endoscopicId);
                //reportForm.ShowDialog();
                //reportForm = null;

                this.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
    }
}
