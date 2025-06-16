using Guna.UI2.WinForms;
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
    public partial class ContractorForm : Form
    {
        private User currentUser;
        public ContractorForm(User user)
        {
            InitializeComponent();
            currentUser = user;
            this.Text = "Trang quản trị Pháp lý";
            int width = 1280;
            int height = (int)(width * 9.0 / 16); // 720 nếu width là 1280
            this.Size = new Size(1280, 720);
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            this.btnDocuments.Click += new System.EventHandler(this.btnDocuments_Click);
            this.btnReminder.Click += new System.EventHandler(this.btnReminder_Click);
            this.btnDepartment.Click += new System.EventHandler(this.btnDepartment_Click);
            btnDashboard.BorderColor = Color.FromArgb(0, 120, 215); // Màu viền
            btnDashboard.BorderThickness = 1;                        // Độ dày viền
            btnDashboard.BorderRadius = 10;                           // Bo góc
            btnDashboard.FillColor = navPanel.BackColor;                  // Màu nền
            btnDashboard.ForeColor = Color.Black;                    // Màu chữ
            btnDashboard.HoverState.BorderColor = Color.FromArgb(0, 153, 255); // Viền khi hover
            btnDashboard.HoverState.FillColor = Color.FromArgb(240, 248, 255); // Màu nền khi hover
            ApplyStyleToButton(btnDashboard);
            ApplyStyleToButton(btnDocuments);
            ApplyStyleToButton(btnReminder);
            ApplyStyleToButton(btnDepartment); // Nếu có
            ShowUserInfo();
            // Tạo nút Đăng xuất và thêm vào navPanel
            Guna2Button btnDangXuat = new Guna2Button()
            {
                Text = "Đăng xuất",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(120, 35),
                BorderRadius = 10,
                FillColor = Color.IndianRed,
                ForeColor = Color.White,
                Location = new Point(navPanel.Width - 130, 10),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            // Gắn sự kiện Click để quay lại LoginForm
            btnDangXuat.Click += (s, e) =>
            {
                this.Hide(); // Ẩn AdminForm
                LoginForm loginForm = new LoginForm();
                loginForm.FormClosed += (s2, e2) => this.Close(); // Đóng AdminForm khi loginForm đóng
                loginForm.Show();
            };

            // Thêm nút vào navPanel
            navPanel.Controls.Add(btnDangXuat);

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

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear(); // Xoá avatar, tên, role
            DashboardControl dashboard = new DashboardControl();
            dashboard.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(dashboard);
        }

        private void btnDepartment_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            DepartmentControl dept = new DepartmentControl();
            dept.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(dept);
        }

        private void btnDocuments_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear(); // Xóa mọi nội dung hiện có
            DocumentsControl docControl = new DocumentsControl(currentUser);
            docControl.Dock = DockStyle.Fill; // Lấp đầy mainPanel
            mainPanel.Controls.Add(docControl); // Thêm vào panel
        }

        private void btnReminder_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            ReminderControl reminder = new ReminderControl();
            reminder.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(reminder);
        }
        private void ApplyStyleToButton(Guna2Button btn)
        {
            btn.BorderColor = Color.FromArgb(0, 120, 215);
            btn.BorderThickness = 1;
            btn.BorderRadius = 20; // GÓC BO TRÒN
            btn.FillColor = navPanel.BackColor;
            btn.ForeColor = Color.Black;

            btn.HoverState.BorderColor = Color.FromArgb(0, 153, 255);
            btn.HoverState.FillColor = Color.FromArgb(240, 248, 255);
            btn.HoverState.ForeColor = Color.Black;

            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.Size = new Size(160, 45);
        }

        private void ContractorForm_Load(object sender, EventArgs e)
        {

        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
