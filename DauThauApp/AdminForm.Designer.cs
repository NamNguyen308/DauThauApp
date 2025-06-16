
using System;
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
            this.btnQuayLai = new Guna.UI2.WinForms.Guna2Button();
            this.btnReminder = new Guna.UI2.WinForms.Guna2Button();
            this.btnDocuments = new Guna.UI2.WinForms.Guna2Button();
            this.btnDepartment = new Guna.UI2.WinForms.Guna2Button();
            this.btnDashboard = new Guna.UI2.WinForms.Guna2Button();
            this.mainPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBackupRestore = new Guna.UI2.WinForms.Guna2Button();
            this.navPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // navPanel
            // 
            this.navPanel.Controls.Add(this.btnBackupRestore);
            this.navPanel.Controls.Add(this.btnQuayLai);
            this.navPanel.Controls.Add(this.btnReminder);
            this.navPanel.Controls.Add(this.btnDocuments);
            this.navPanel.Controls.Add(this.btnDepartment);
            this.navPanel.Controls.Add(this.btnDashboard);
            this.navPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.navPanel.FillColor = System.Drawing.Color.Transparent;
            this.navPanel.Location = new System.Drawing.Point(0, 0);
            this.navPanel.Name = "navPanel";
            this.navPanel.Size = new System.Drawing.Size(194, 720);
            this.navPanel.TabIndex = 0;
            this.navPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.navPanel_Paint_2);
            // 
            // btnQuayLai
            // 
            this.btnQuayLai.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnQuayLai.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnQuayLai.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnQuayLai.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnQuayLai.FillColor = System.Drawing.Color.Transparent;
            this.btnQuayLai.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btnQuayLai.ForeColor = System.Drawing.Color.Black;
            this.btnQuayLai.Location = new System.Drawing.Point(3, 3);
            this.btnQuayLai.Name = "btnQuayLai";
            this.btnQuayLai.Size = new System.Drawing.Size(50, 35);
            this.btnQuayLai.TabIndex = 0;
            this.btnQuayLai.Text = "←";
            this.btnQuayLai.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnQuayLai.Click += new System.EventHandler(this.btnQuayLai_Click);
            // 
            // btnReminder
            // 
            this.btnReminder.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReminder.ForeColor = System.Drawing.Color.White;
            this.btnReminder.Location = new System.Drawing.Point(12, 392);
            this.btnReminder.Name = "btnReminder";
            this.btnReminder.Size = new System.Drawing.Size(180, 45);
            this.btnReminder.TabIndex = 3;
            this.btnReminder.Text = "Yêu cầu";
            this.btnReminder.Click += new System.EventHandler(this.btnReminder_Click);
            // 
            // btnDocuments
            // 
            this.btnDocuments.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDocuments.ForeColor = System.Drawing.Color.White;
            this.btnDocuments.Location = new System.Drawing.Point(12, 300);
            this.btnDocuments.Name = "btnDocuments";
            this.btnDocuments.Size = new System.Drawing.Size(180, 45);
            this.btnDocuments.TabIndex = 2;
            this.btnDocuments.Text = "Hồ sơ";
            this.btnDocuments.Click += new System.EventHandler(this.btnDocuments_Click);
            // 
            // btnDepartment
            // 
            this.btnDepartment.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDepartment.ForeColor = System.Drawing.Color.White;
            this.btnDepartment.Location = new System.Drawing.Point(12, 210);
            this.btnDepartment.Name = "btnDepartment";
            this.btnDepartment.Size = new System.Drawing.Size(180, 45);
            this.btnDepartment.TabIndex = 1;
            this.btnDepartment.Text = "Phòng ban";
            this.btnDepartment.Click += new System.EventHandler(this.btnDepartment_Click);
            // 
            // btnDashboard
            // 
            this.btnDashboard.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.btnDashboard.ForeColor = System.Drawing.Color.White;
            this.btnDashboard.Location = new System.Drawing.Point(12, 123);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(180, 45);
            this.btnDashboard.TabIndex = 0;
            this.btnDashboard.Text = "Trang chính";
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.White;
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(194, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1086, 720);
            this.mainPanel.TabIndex = 1;
            this.mainPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.mainPanel_Paint_2);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // btnBackupRestore
            // 
            this.btnBackupRestore.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackupRestore.ForeColor = System.Drawing.Color.White;
            this.btnBackupRestore.Location = new System.Drawing.Point(14, 479);
            this.btnBackupRestore.Name = "btnBackupRestore";
            this.btnBackupRestore.Size = new System.Drawing.Size(180, 45);
            this.btnBackupRestore.TabIndex = 4;
            this.btnBackupRestore.Text = "Sao lưu";
            this.btnBackupRestore.Click += new System.EventHandler(this.btnBackupRestore_Click);
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
        private Guna.UI2.WinForms.Guna2Button btnReminder;
        private Guna.UI2.WinForms.Guna2Button btnDocuments;
        private Guna.UI2.WinForms.Guna2Button btnDepartment;
        private Guna.UI2.WinForms.Guna2Button btnQuayLai;
        private Guna.UI2.WinForms.Guna2Button btnBackupRestore;
    }
}
