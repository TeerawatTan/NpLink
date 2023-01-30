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
    public partial class FormLoading2 : Form
    {
        private Timer _timer = new Timer();

        public FormLoading2()
        {
            InitializeComponent();

            _timer.Interval = 100;
            _timer.Enabled = true;
            _timer.Tick += Timer1_Tick;
        }

        private void FormLoading2_Load(object sender, EventArgs e)
        {
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < 100)
            {
                progressBar1.Value++;

            }
            else
            {
                _timer.Enabled = false;

                this.Close();
            }
        }
    }
}
