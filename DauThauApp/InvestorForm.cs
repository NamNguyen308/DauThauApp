using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DauThauApp
{
    public partial class InvestorForm : Form
    {
        private User currentUser;
        public InvestorForm(User user)
        {
            InitializeComponent();
            currentUser = user;
            this.Text = "Trang quản trị Admin";
            Label lbl = new Label();
            lbl.Text = $"Xin chào Admin: {user.Username}";
            lbl.Dock = DockStyle.Fill;
            lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lbl.Font = new System.Drawing.Font("Segoe UI", 14);
            this.Controls.Add(lbl);
        }

        private void InvestorForm_Load(object sender, EventArgs e)
        {

        }
    }
}
