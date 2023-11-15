
namespace EndoscopicSystem.V2.Forms
{
    partial class FormProceed
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
            this.panelChild = new System.Windows.Forms.Panel();
            this.txbCheckHasEgdAndColono = new System.Windows.Forms.TextBox();
            this.txbStep = new System.Windows.Forms.TextBox();
            this.dtRecordStart = new System.Windows.Forms.DateTimePicker();
            this.dtRecordEnd = new System.Windows.Forms.DateTimePicker();
            this.panelChild.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelChild
            // 
            this.panelChild.BackColor = System.Drawing.Color.DarkGray;
            this.panelChild.Controls.Add(this.dtRecordEnd);
            this.panelChild.Controls.Add(this.dtRecordStart);
            this.panelChild.Controls.Add(this.txbCheckHasEgdAndColono);
            this.panelChild.Controls.Add(this.txbStep);
            this.panelChild.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChild.Location = new System.Drawing.Point(0, 0);
            this.panelChild.Name = "panelChild";
            this.panelChild.Size = new System.Drawing.Size(1388, 791);
            this.panelChild.TabIndex = 11;
            // 
            // txbCheckHasEgdAndColono
            // 
            this.txbCheckHasEgdAndColono.Location = new System.Drawing.Point(12, 38);
            this.txbCheckHasEgdAndColono.Name = "txbCheckHasEgdAndColono";
            this.txbCheckHasEgdAndColono.Size = new System.Drawing.Size(34, 20);
            this.txbCheckHasEgdAndColono.TabIndex = 1;
            this.txbCheckHasEgdAndColono.Visible = false;
            // 
            // txbStep
            // 
            this.txbStep.Location = new System.Drawing.Point(12, 12);
            this.txbStep.Name = "txbStep";
            this.txbStep.Size = new System.Drawing.Size(1070, 20);
            this.txbStep.TabIndex = 0;
            this.txbStep.Visible = false;
            this.txbStep.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // dtRecordStart
            // 
            this.dtRecordStart.CustomFormat = "dd MMMM yyyy HH:mm:ss";
            this.dtRecordStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtRecordStart.Location = new System.Drawing.Point(13, 65);
            this.dtRecordStart.Name = "dtRecordStart";
            this.dtRecordStart.Size = new System.Drawing.Size(200, 20);
            this.dtRecordStart.TabIndex = 2;
            this.dtRecordStart.Value = new System.DateTime(2023, 9, 29, 16, 22, 42, 0);
            this.dtRecordStart.Visible = false;
            // 
            // dtRecordEnd
            // 
            this.dtRecordEnd.CustomFormat = "dd MMMM yyyy HH:mm:ss";
            this.dtRecordEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtRecordEnd.Location = new System.Drawing.Point(12, 91);
            this.dtRecordEnd.Name = "dtRecordEnd";
            this.dtRecordEnd.Size = new System.Drawing.Size(200, 20);
            this.dtRecordEnd.TabIndex = 3;
            this.dtRecordEnd.Value = new System.DateTime(2023, 9, 29, 16, 22, 42, 0);
            this.dtRecordEnd.Visible = false;
            // 
            // FormProceed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1388, 791);
            this.Controls.Add(this.panelChild);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormProceed";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Proceed Page";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormProceed_FormClosing);
            this.Load += new System.EventHandler(this.FormProceed_Load);
            this.panelChild.ResumeLayout(false);
            this.panelChild.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelChild;
        public System.Windows.Forms.TextBox txbStep;
        public System.Windows.Forms.TextBox txbCheckHasEgdAndColono;
        public System.Windows.Forms.DateTimePicker dtRecordEnd;
        public System.Windows.Forms.DateTimePicker dtRecordStart;
    }
}