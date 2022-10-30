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

namespace EndoscopicSystem.V2.Forms
{
    public partial class FormProcedure : Form
    {
        private string _hnNo;
        private int _id, _procedureId, _appointmentId;
        private readonly GetDropdownList list = new GetDropdownList();

        public FormProcedure(int id, string hn, int procId, int appId)
        {
            InitializeComponent();

            this._id = id;
            this._hnNo = hn;
            this._procedureId = procId;
            this._appointmentId = appId;
        }

        private void FormProcedure_Load(object sender, EventArgs e)
        {
            cbbProcedureList.ValueMember = "ProcedureID";
            cbbProcedureList.DisplayMember = "ProcedureName";
            cbbProcedureList.DataSource = list.GetProcedureList();
            cbbProcedureList.SelectedIndex = 0;

            cbbProcedureList.SelectedValue = _procedureId;
        }
    }
}
