using System.Windows.Forms;

namespace DauThauApp
{
    partial class AdminForm
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
            this.navPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.btnDashboard = new Guna.UI2.WinForms.Guna2Button();
            this.mainPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.navPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // navPanel
            // 
            this.navPanel.Controls.Add(this.btnDashboard);
            this.navPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.navPanel.FillColor = System.Drawing.Color.LightGray;
            this.navPanel.Location = new System.Drawing.Point(0, 0);
            this.navPanel.Name = "navPanel";
            this.navPanel.Size = new System.Drawing.Size(200, 720);
            this.navPanel.TabIndex = 0;
            // 
            // btnDashboard
            // 
            this.btnDashboard.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDashboard.ForeColor = System.Drawing.Color.White;
            this.btnDashboard.Location = new System.Drawing.Point(12, 42);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(180, 45);
            this.btnDashboard.TabIndex = 0;
            this.btnDashboard.Text = "Dashboard";
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.White;
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(200, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1080, 720);
            this.mainPanel.TabIndex = 1;
            this.mainPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.mainPanel_Paint_1);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            // 
            // AdminForm
            // 
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.navPanel);
            this.Name = "AdminForm";
            this.Text = "Trang quản trị Admin";
            this.Load += new System.EventHandler(this.AdminForm_Load);
            this.navPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion

        private Guna.UI2.WinForms.Guna2Panel navPanel;
        private Guna.UI2.WinForms.Guna2Button btnDashboard;
        private Guna.UI2.WinForms.Guna2Panel mainPanel;
        private System.Windows.Forms.Label label1;
    }
}