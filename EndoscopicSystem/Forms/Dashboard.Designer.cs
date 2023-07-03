namespace EndoscopicSystem
{
    partial class DashboardForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.gridQueue = new System.Windows.Forms.DataGridView();
            this.label16 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtHN = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHNNo = new System.Windows.Forms.TextBox();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGotoEndoscope = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnShowChart = new System.Windows.Forms.Button();
            this.cbbYear = new System.Windows.Forms.ComboBox();
            this.cbbMonth = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtProcedureId = new System.Windows.Forms.TextBox();
            this.btnDeleteHn = new System.Windows.Forms.Button();
            this.cbbOrder = new System.Windows.Forms.ComboBox();
            this.btnClearDataForSuperAdmin = new System.Windows.Forms.Button();
            this.cbbInstrument = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnShowChartInstrument = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridQueue)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chart1.BackColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Cursor = System.Windows.Forms.Cursors.Default;
            legend1.BackColor = System.Drawing.Color.White;
            legend1.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(51, 60);
            this.chart1.Margin = new System.Windows.Forms.Padding(2);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            this.chart1.PaletteCustomColors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0))))),
        System.Drawing.Color.PaleTurquoise,
        System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))))};
            series1.BackImageTransparentColor = System.Drawing.Color.White;
            series1.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.Font = new System.Drawing.Font("Microsoft Sans Serif", 5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Millimeter);
            series1.IsXValueIndexed = true;
            series1.Label = "#VAL{D}";
            series1.LabelBackColor = System.Drawing.Color.White;
            series1.LabelBorderColor = System.Drawing.Color.White;
            series1.LabelBorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            series1.Legend = "Legend1";
            series1.Name = "Patient";
            series1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.BrightPastel;
            series1.ShadowOffset = 5;
            series1.SmartLabelStyle.AllowOutsidePlotArea = System.Windows.Forms.DataVisualization.Charting.LabelOutsidePlotAreaStyle.Yes;
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            series1.YValuesPerPoint = 20;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(1273, 303);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            title1.Name = "Title1";
            title1.Text = "ยอดผู้ป่วยแยกตามหัตถการ ประจำเดือน";
            this.chart1.Titles.Add(title1);
            // 
            // gridQueue
            // 
            this.gridQueue.AllowUserToAddRows = false;
            this.gridQueue.AllowUserToDeleteRows = false;
            this.gridQueue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridQueue.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridQueue.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridQueue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridQueue.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridQueue.Location = new System.Drawing.Point(9, 449);
            this.gridQueue.Margin = new System.Windows.Forms.Padding(2);
            this.gridQueue.MultiSelect = false;
            this.gridQueue.Name = "gridQueue";
            this.gridQueue.ReadOnly = true;
            this.gridQueue.RowHeadersWidth = 51;
            this.gridQueue.RowTemplate.Height = 24;
            this.gridQueue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridQueue.Size = new System.Drawing.Size(1351, 360);
            this.gridQueue.TabIndex = 4;
            this.gridQueue.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridQueue_CellClick);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label16.Location = new System.Drawing.Point(4, 371);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(91, 29);
            this.label16.TabIndex = 8;
            this.label16.Text = "Queue";
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSearch.Location = new System.Drawing.Point(438, 398);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(111, 40);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtHN
            // 
            this.txtHN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtHN.Location = new System.Drawing.Point(43, 404);
            this.txtHN.Margin = new System.Windows.Forms.Padding(2);
            this.txtHN.MaxLength = 255;
            this.txtHN.Name = "txtHN";
            this.txtHN.Size = new System.Drawing.Size(115, 26);
            this.txtHN.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(7, 409);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 20);
            this.label3.TabIndex = 27;
            this.label3.Text = "HN";
            // 
            // txtHNNo
            // 
            this.txtHNNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHNNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtHNNo.Location = new System.Drawing.Point(890, 401);
            this.txtHNNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtHNNo.MaxLength = 255;
            this.txtHNNo.Name = "txtHNNo";
            this.txtHNNo.ReadOnly = true;
            this.txtHNNo.Size = new System.Drawing.Size(164, 29);
            this.txtHNNo.TabIndex = 28;
            // 
            // txtFullName
            // 
            this.txtFullName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtFullName.Location = new System.Drawing.Point(227, 404);
            this.txtFullName.Margin = new System.Windows.Forms.Padding(2);
            this.txtFullName.MaxLength = 255;
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(207, 26);
            this.txtFullName.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(172, 409);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 27;
            this.label1.Text = "Name";
            // 
            // btnGotoEndoscope
            // 
            this.btnGotoEndoscope.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGotoEndoscope.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnGotoEndoscope.Location = new System.Drawing.Point(1060, 394);
            this.btnGotoEndoscope.Margin = new System.Windows.Forms.Padding(2);
            this.btnGotoEndoscope.Name = "btnGotoEndoscope";
            this.btnGotoEndoscope.Size = new System.Drawing.Size(163, 41);
            this.btnGotoEndoscope.TabIndex = 3;
            this.btnGotoEndoscope.Text = "Endoscopic Room";
            this.btnGotoEndoscope.UseVisualStyleBackColor = true;
            this.btnGotoEndoscope.Click += new System.EventHandler(this.BtnGotoEndoscopic_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(752, 404);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(134, 24);
            this.label4.TabIndex = 27;
            this.label4.Text = "HN Selected:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnShowChart
            // 
            this.btnShowChart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowChart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnShowChart.Location = new System.Drawing.Point(1245, 17);
            this.btnShowChart.Margin = new System.Windows.Forms.Padding(2);
            this.btnShowChart.Name = "btnShowChart";
            this.btnShowChart.Size = new System.Drawing.Size(79, 34);
            this.btnShowChart.TabIndex = 30;
            this.btnShowChart.Text = "Show";
            this.btnShowChart.UseVisualStyleBackColor = true;
            this.btnShowChart.Click += new System.EventHandler(this.btnShowChart_Click);
            // 
            // cbbYear
            // 
            this.cbbYear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbYear.FormattingEnabled = true;
            this.cbbYear.Location = new System.Drawing.Point(972, 20);
            this.cbbYear.Margin = new System.Windows.Forms.Padding(2);
            this.cbbYear.Name = "cbbYear";
            this.cbbYear.Size = new System.Drawing.Size(94, 28);
            this.cbbYear.TabIndex = 31;
            // 
            // cbbMonth
            // 
            this.cbbMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbMonth.FormattingEnabled = true;
            this.cbbMonth.Items.AddRange(new object[] {
            "มกราคม",
            "กุมภาพันธ์",
            "มีนาคม",
            "เมษายน",
            "พฤษภาคม",
            "มิถุนายน",
            "กรกฎาคม",
            "สิงหาคม",
            "กันยายน",
            "ตุลาคม",
            "พฤศจิกายน",
            "ธันวาคม"});
            this.cbbMonth.Location = new System.Drawing.Point(749, 20);
            this.cbbMonth.Margin = new System.Windows.Forms.Padding(2);
            this.cbbMonth.Name = "cbbMonth";
            this.cbbMonth.Size = new System.Drawing.Size(158, 28);
            this.cbbMonth.TabIndex = 32;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(913, 23);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 20);
            this.label2.TabIndex = 33;
            this.label2.Text = "ปี พ.ศ.";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.Location = new System.Drawing.Point(700, 23);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 20);
            this.label5.TabIndex = 34;
            this.label5.Text = "เดือน";
            // 
            // txtProcedureId
            // 
            this.txtProcedureId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProcedureId.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtProcedureId.Location = new System.Drawing.Point(1315, 367);
            this.txtProcedureId.Margin = new System.Windows.Forms.Padding(2);
            this.txtProcedureId.MaxLength = 255;
            this.txtProcedureId.Name = "txtProcedureId";
            this.txtProcedureId.ReadOnly = true;
            this.txtProcedureId.Size = new System.Drawing.Size(32, 23);
            this.txtProcedureId.TabIndex = 35;
            this.txtProcedureId.Visible = false;
            // 
            // btnDeleteHn
            // 
            this.btnDeleteHn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteHn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnDeleteHn.ForeColor = System.Drawing.Color.Red;
            this.btnDeleteHn.Location = new System.Drawing.Point(1236, 394);
            this.btnDeleteHn.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeleteHn.Name = "btnDeleteHn";
            this.btnDeleteHn.Size = new System.Drawing.Size(90, 41);
            this.btnDeleteHn.TabIndex = 36;
            this.btnDeleteHn.Text = "Delete";
            this.btnDeleteHn.UseVisualStyleBackColor = true;
            this.btnDeleteHn.Click += new System.EventHandler(this.btnDeleteHn_Click);
            // 
            // cbbOrder
            // 
            this.cbbOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbOrder.FormattingEnabled = true;
            this.cbbOrder.Location = new System.Drawing.Point(1081, 20);
            this.cbbOrder.Margin = new System.Windows.Forms.Padding(2);
            this.cbbOrder.Name = "cbbOrder";
            this.cbbOrder.Size = new System.Drawing.Size(158, 28);
            this.cbbOrder.TabIndex = 37;
            this.cbbOrder.SelectedIndexChanged += new System.EventHandler(this.cbbOrder_SelectedIndexChanged);
            // 
            // btnClearDataForSuperAdmin
            // 
            this.btnClearDataForSuperAdmin.BackColor = System.Drawing.Color.Red;
            this.btnClearDataForSuperAdmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnClearDataForSuperAdmin.ForeColor = System.Drawing.Color.White;
            this.btnClearDataForSuperAdmin.Location = new System.Drawing.Point(9, 10);
            this.btnClearDataForSuperAdmin.Margin = new System.Windows.Forms.Padding(2);
            this.btnClearDataForSuperAdmin.Name = "btnClearDataForSuperAdmin";
            this.btnClearDataForSuperAdmin.Size = new System.Drawing.Size(157, 41);
            this.btnClearDataForSuperAdmin.TabIndex = 38;
            this.btnClearDataForSuperAdmin.Text = "Clear Data All";
            this.btnClearDataForSuperAdmin.UseVisualStyleBackColor = false;
            this.btnClearDataForSuperAdmin.Click += new System.EventHandler(this.btnClearDataForSuperAdmin_Click);
            // 
            // cbbInstrument
            // 
            this.cbbInstrument.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbInstrument.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbInstrument.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbInstrument.FormattingEnabled = true;
            this.cbbInstrument.Items.AddRange(new object[] {
            "มกราคม",
            "กุมภาพันธ์",
            "มีนาคม",
            "เมษายน",
            "พฤษภาคม",
            "มิถุนายน",
            "กรกฎาคม",
            "สิงหาคม",
            "กันยายน",
            "ตุลาคม",
            "พฤศจิกายน",
            "ธันวาคม"});
            this.cbbInstrument.Location = new System.Drawing.Point(369, 20);
            this.cbbInstrument.Margin = new System.Windows.Forms.Padding(2);
            this.cbbInstrument.Name = "cbbInstrument";
            this.cbbInstrument.Size = new System.Drawing.Size(158, 28);
            this.cbbInstrument.TabIndex = 39;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label6.Location = new System.Drawing.Point(172, 23);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(193, 20);
            this.label6.TabIndex = 40;
            this.label6.Text = "Procedure/Instrument :";
            // 
            // btnShowChartInstrument
            // 
            this.btnShowChartInstrument.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowChartInstrument.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnShowChartInstrument.Location = new System.Drawing.Point(531, 17);
            this.btnShowChartInstrument.Margin = new System.Windows.Forms.Padding(2);
            this.btnShowChartInstrument.Name = "btnShowChartInstrument";
            this.btnShowChartInstrument.Size = new System.Drawing.Size(79, 34);
            this.btnShowChartInstrument.TabIndex = 41;
            this.btnShowChartInstrument.Text = "Show";
            this.btnShowChartInstrument.UseVisualStyleBackColor = true;
            this.btnShowChartInstrument.Click += new System.EventHandler(this.btnShowChartInstrument_Click);
            // 
            // DashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(233)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(1369, 818);
            this.Controls.Add(this.btnShowChartInstrument);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbbInstrument);
            this.Controls.Add(this.btnClearDataForSuperAdmin);
            this.Controls.Add(this.cbbOrder);
            this.Controls.Add(this.btnDeleteHn);
            this.Controls.Add(this.txtProcedureId);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbbMonth);
            this.Controls.Add(this.cbbYear);
            this.Controls.Add(this.btnShowChart);
            this.Controls.Add(this.btnGotoEndoscope);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtHNNo);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.txtHN);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.gridQueue);
            this.Controls.Add(this.chart1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DashboardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard Page";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.DashboardForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridQueue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView gridQueue;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtHN;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtHNNo;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGotoEndoscope;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button btnShowChart;
        private System.Windows.Forms.ComboBox cbbYear;
        private System.Windows.Forms.ComboBox cbbMonth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtProcedureId;
        private System.Windows.Forms.Button btnDeleteHn;
        private System.Windows.Forms.ComboBox cbbOrder;
        private System.Windows.Forms.Button btnClearDataForSuperAdmin;
        private System.Windows.Forms.ComboBox cbbInstrument;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnShowChartInstrument;
    }
}