using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace DauThauApp
{
    public partial class AdminForm : Form
    {

        private User currentUser;
        public AdminForm(User user)
        {
            InitializeComponent();
            int width = 1280;
            int height = (int)(width * 9.0 / 16); // 720 nếu width là 1280

            this.Size = new Size(1280, 720);
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            btnDashboard.BorderColor = Color.FromArgb(0, 120, 215); // Màu viền
            btnDashboard.BorderThickness = 1;                        // Độ dày viền
            btnDashboard.BorderRadius = 1;                           // Bo góc
            btnDashboard.FillColor = navPanel.BackColor;                  // Màu nền
            btnDashboard.ForeColor = Color.Black;                    // Màu chữ
            btnDashboard.HoverState.BorderColor = Color.FromArgb(0, 153, 255); // Viền khi hover
            btnDashboard.HoverState.FillColor = Color.FromArgb(240, 248, 255); // Màu nền khi hover

            currentUser = user;
            this.Text = "Trang quản trị Admin";
            this.StartPosition = FormStartPosition.CenterScreen;
            ShowUserInfo();
        }

        private void ShowUserInfo()
        {
            PictureBox avatar = new PictureBox();
            try
            {
                avatar.Image = Image.FromFile(@"C:\Users\nhn30\Downloads\lonk.jpg");
            }
            catch
            {
                MessageBox.Show("Không tìm thấy file ảnh!");
            }

            avatar.SizeMode = PictureBoxSizeMode.StretchImage;
            avatar.Size = new Size(60, 60);
            avatar.Location = new Point(mainPanel.Width - 80, 10); // Góc phải
            avatar.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            Label usernameLabel = new Label();
            usernameLabel.Text = $"User: {currentUser.Username}";
            usernameLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            usernameLabel.AutoSize = true;
            usernameLabel.Location = new Point(mainPanel.Width - 200, 15);
            usernameLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            Label roleLabel = new Label();
            roleLabel.Text = $"Role: {currentUser.Role}";
            roleLabel.Font = new Font("Segoe UI", 9);
            roleLabel.AutoSize = true;
            roleLabel.Location = new Point(mainPanel.Width - 200, 40);
            roleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            mainPanel.Controls.Add(avatar);
            mainPanel.Controls.Add(usernameLabel);
            mainPanel.Controls.Add(roleLabel);
        }


        private void AdminForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void navPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear(); // Xoá avatar, tên, role
            DashboardControl dashboard = new DashboardControl();
            dashboard.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(dashboard);
        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void AdminForm_Load_1(object sender, EventArgs e)
        {

        }

        private void mainPanel_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void navPanel_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}