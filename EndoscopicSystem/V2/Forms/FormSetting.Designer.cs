
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
            this.Anesthesist = new System.Windows.Forms.Button();
            this.menuWard = new System.Windows.Forms.Button();
            this.menuOPD = new System.Windows.Forms.Button();
            this.menuNurse = new System.Windows.Forms.Button();
            this.menuDoctor = new System.Windows.Forms.Button();
            this.menuUserManage = new System.Windows.Forms.Button();
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.menuAnesthesistMethod = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panelSidebar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelChild
            // 
            this.panelChild.BackColor = System.Drawing.Color.Azure;
            this.panelChild.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChild.Location = new System.Drawing.Point(297, 0);
            this.panelChild.Name = "panelChild";
            this.panelChild.Size = new System.Drawing.Size(1128, 810);
            this.panelChild.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.menuAnesthesistMethod);
            this.panel1.Controls.Add(this.Anesthesist);
            this.panel1.Controls.Add(this.menuWard);
            this.panel1.Controls.Add(this.menuOPD);
            this.panel1.Controls.Add(this.menuNurse);
            this.panel1.Controls.Add(this.menuDoctor);
            this.panel1.Controls.Add(this.menuUserManage);
            this.panel1.Location = new System.Drawing.Point(9, 11);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(279, 751);
            this.panel1.TabIndex = 0;
            // 
            // Anesthesist
            // 
            this.Anesthesist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(80)))), ((int)(((byte)(139)))));
            this.Anesthesist.Dock = System.Windows.Forms.DockStyle.Top;
            this.Anesthesist.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Anesthesist.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Anesthesist.Location = new System.Drawing.Point(0, 315);
            this.Anesthesist.Name = "Anesthesist";
            this.Anesthesist.Size = new System.Drawing.Size(279, 63);
            this.Anesthesist.TabIndex = 12;
            this.Anesthesist.Text = "Anesthesist";
            this.Anesthesist.UseVisualStyleBackColor = false;
            this.Anesthesist.Click += new System.EventHandler(this.menuAnesthesist_Click);
            // 
            // menuWard
            // 
            this.menuWard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(80)))), ((int)(((byte)(139)))));
            this.menuWard.Dock = System.Windows.Forms.DockStyle.Top;
            this.menuWard.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.menuWard.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.menuWard.Location = new System.Drawing.Point(0, 252);
            this.menuWard.Name = "menuWard";
            this.menuWard.Size = new System.Drawing.Size(279, 63);
            this.menuWard.TabIndex = 11;
            this.menuWard.Text = "Ward";
            this.menuWard.UseVisualStyleBackColor = false;
            this.menuWard.Click += new System.EventHandler(this.menuWard_Click);
            // 
            // menuOPD
            // 
            this.menuOPD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(80)))), ((int)(((byte)(139)))));
            this.menuOPD.Dock = System.Windows.Forms.DockStyle.Top;
            this.menuOPD.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.menuOPD.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.menuOPD.Location = new System.Drawing.Point(0, 189);
            this.menuOPD.Name = "menuOPD";
            this.menuOPD.Size = new System.Drawing.Size(279, 63);
            this.menuOPD.TabIndex = 10;
            this.menuOPD.Text = "OPD";
            this.menuOPD.UseVisualStyleBackColor = false;
            this.menuOPD.Click += new System.EventHandler(this.menuOPD_Click);
            // 
            // menuNurse
            // 
            this.menuNurse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(80)))), ((int)(((byte)(139)))));
            this.menuNurse.Dock = System.Windows.Forms.DockStyle.Top;
            this.menuNurse.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.menuNurse.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.menuNurse.Location = new System.Drawing.Point(0, 126);
            this.menuNurse.Name = "menuNurse";
            this.menuNurse.Size = new System.Drawing.Size(279, 63);
            this.menuNurse.TabIndex = 9;
            this.menuNurse.Text = "Nurse";
            this.menuNurse.UseVisualStyleBackColor = false;
            this.menuNurse.Click += new System.EventHandler(this.menuNurse_Click);
            // 
            // menuDoctor
            // 
            this.menuDoctor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(80)))), ((int)(((byte)(139)))));
            this.menuDoctor.Dock = System.Windows.Forms.DockStyle.Top;
            this.menuDoctor.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.menuDoctor.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.menuDoctor.Location = new System.Drawing.Point(0, 63);
            this.menuDoctor.Name = "menuDoctor";
            this.menuDoctor.Size = new System.Drawing.Size(279, 63);
            this.menuDoctor.TabIndex = 8;
            this.menuDoctor.Text = "Doctor";
            this.menuDoctor.UseVisualStyleBackColor = false;
            this.menuDoctor.Click += new System.EventHandler(this.menuDoctor_Click);
            // 
            // menuUserManage
            // 
            this.menuUserManage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(80)))), ((int)(((byte)(139)))));
            this.menuUserManage.Dock = System.Windows.Forms.DockStyle.Top;
            this.menuUserManage.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.menuUserManage.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.menuUserManage.Location = new System.Drawing.Point(0, 0);
            this.menuUserManage.Name = "menuUserManage";
            this.menuUserManage.Size = new System.Drawing.Size(279, 63);
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
            this.panelSidebar.Size = new System.Drawing.Size(297, 810);
            this.panelSidebar.TabIndex = 8;
            // 
            // menuAnesthesistMethod
            // 
            this.menuAnesthesistMethod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(80)))), ((int)(((byte)(139)))));
            this.menuAnesthesistMethod.Dock = System.Windows.Forms.DockStyle.Top;
            this.menuAnesthesistMethod.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.menuAnesthesistMethod.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.menuAnesthesistMethod.Location = new System.Drawing.Point(0, 378);
            this.menuAnesthesistMethod.Name = "menuAnesthesistMethod";
            this.menuAnesthesistMethod.Size = new System.Drawing.Size(279, 63);
            this.menuAnesthesistMethod.TabIndex = 13;
            this.menuAnesthesistMethod.Text = "Anesthesist Method";
            this.menuAnesthesistMethod.UseVisualStyleBackColor = false;
            this.menuAnesthesistMethod.Click += new System.EventHandler(this.menuAnesthesistMethod_Click);
            // 
            // FormSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1425, 810);
            this.Controls.Add(this.panelChild);
            this.Controls.Add(this.panelSidebar);
            this.Name = "FormSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormSetting";
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
        private System.Windows.Forms.Button menuOPD;
        private System.Windows.Forms.Button menuWard;
        private System.Windows.Forms.Button Anesthesist;
        private System.Windows.Forms.Button menuAnesthesistMethod;
    }
}