
namespace EndoscopicSystem.V2.Forms
{
    partial class FormSetting
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuNurse = new System.Windows.Forms.Button();
            this.menuDoctor = new System.Windows.Forms.Button();
            this.menuUserManage = new System.Windows.Forms.Button();
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panelSidebar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelChild
            // 
            this.panelChild.BackColor = System.Drawing.Color.Azure;
            this.panelChild.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChild.Location = new System.Drawing.Point(258, 0);
            this.panelChild.Name = "panelChild";
            this.panelChild.Size = new System.Drawing.Size(958, 810);
            this.panelChild.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.menuNurse);
            this.panel1.Controls.Add(this.menuDoctor);
            this.panel1.Controls.Add(this.menuUserManage);
            this.panel1.Location = new System.Drawing.Point(12, 11);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(231, 194);
            this.panel1.TabIndex = 0;
            // 
            // menuNurse
            // 
            this.menuNurse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(80)))), ((int)(((byte)(139)))));
            this.menuNurse.Dock = System.Windows.Forms.DockStyle.Top;
            this.menuNurse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.menuNurse.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.menuNurse.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.menuNurse.Location = new System.Drawing.Point(0, 126);
            this.menuNurse.Name = "menuNurse";
            this.menuNurse.Size = new System.Drawing.Size(231, 63);
            this.menuNurse.TabIndex = 9;
            this.menuNurse.Text = "Nurse";
            this.menuNurse.UseVisualStyleBackColor = false;
            this.menuNurse.Click += new System.EventHandler(this.menuNurse_Click);
            // 
            // menuDoctor
            // 
            this.menuDoctor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(80)))), ((int)(((byte)(139)))));
            this.menuDoctor.Dock = System.Windows.Forms.DockStyle.Top;
            this.menuDoctor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.menuDoctor.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.menuDoctor.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.menuDoctor.Location = new System.Drawing.Point(0, 63);
            this.menuDoctor.Name = "menuDoctor";
            this.menuDoctor.Size = new System.Drawing.Size(231, 63);
            this.menuDoctor.TabIndex = 8;
            this.menuDoctor.Text = "Doctor";
            this.menuDoctor.UseVisualStyleBackColor = false;
            this.menuDoctor.Click += new System.EventHandler(this.menuDoctor_Click);
            // 
            // menuUserManage
            // 
            this.menuUserManage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(80)))), ((int)(((byte)(139)))));
            this.menuUserManage.Dock = System.Windows.Forms.DockStyle.Top;
            this.menuUserManage.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.menuUserManage.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.menuUserManage.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.menuUserManage.Location = new System.Drawing.Point(0, 0);
            this.menuUserManage.Name = "menuUserManage";
            this.menuUserManage.Size = new System.Drawing.Size(231, 63);
            this.menuUserManage.TabIndex = 6;
            this.menuUserManage.Text = "User Manage";
            this.menuUserManage.UseVisualStyleBackColor = false;
            this.menuUserManage.Click += new System.EventHandler(this.menuUserManage_Click);
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.Color.Transparent;
            this.panelSidebar.Controls.Add(this.panel1);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 0);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(258, 810);
            this.panelSidebar.TabIndex = 8;
            // 
            // FormSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 810);
            this.Controls.Add(this.panelChild);
            this.Controls.Add(this.panelSidebar);
            this.Name = "FormSetting";
            this.Text = "FormSetting";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormSetting_FormClosed);
            this.Load += new System.EventHandler(this.FormSetting_Load);
            this.panel1.ResumeLayout(false);
            this.panelSidebar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelChild;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button menuNurse;
        private System.Windows.Forms.Button menuDoctor;
        private System.Windows.Forms.Button menuUserManage;
        private System.Windows.Forms.Panel panelSidebar;
    }
}