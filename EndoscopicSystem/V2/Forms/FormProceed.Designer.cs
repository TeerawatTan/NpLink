
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
            this.txbStep = new System.Windows.Forms.TextBox();
            this.panelChild.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelChild
            // 
            this.panelChild.BackColor = System.Drawing.Color.DarkGray;
            this.panelChild.Controls.Add(this.txbStep);
            this.panelChild.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChild.Location = new System.Drawing.Point(0, 0);
            this.panelChild.Name = "panelChild";
            this.panelChild.Size = new System.Drawing.Size(1388, 791);
            this.panelChild.TabIndex = 11;
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
    }
}