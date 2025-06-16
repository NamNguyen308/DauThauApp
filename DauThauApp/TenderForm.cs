using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DauThauApp
{
    public partial class TenderForm : Form
    {
        private User currentUser;
        private List<ThongTinNhanVien> dsNhanVien; // Danh sách gốc nhân viên
        private Dictionary<string, List<ThongTinNhanVien>> dsNhanVienTheoPhongBan; // Nhóm theo phòng ban
        public TenderForm(User user)
        {
            InitializeComponent();

            currentUser = user;

            dsNhanVien = LayDanhSachNhanVien();
            dsNhanVienTheoPhongBan = dsNhanVien
            .GroupBy(nv => nv.PhongBan)
            .ToDictionary(g => g.Key, g => g.ToList());

            this.Text = "Trang quản lý cho Giám đốc";
            this.StartPosition = FormStartPosition.CenterScreen;
            int width = 1280;
            int height = (int)(width * 9.0 / 16); // 720 nếu width là 1280

            this.Size = new Size(1280, 720);
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            ApplyStyleToButton(btnDashboard);
            ApplyStyleToButton(btnDocuments);
            ApplyStyleToButton(btnReminder);
            ApplyStyleToButton(btnDepartment);
            ApplyStyleToButton(btnTaiChinh);

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

            // Gắn sự kiện
            btnDashboard.Click += btnDashboard_Click;
            btnDocuments.Click += btnDocuments_Click;
            btnReminder.Click += btnReminder_Click;
            btnDepartment.Click += btnDepartment_Click;
            btnQuayLai.Click += btnQuayLai_Click;
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


        private List<ThongTinNhanVien> LayDanhSachNhanVien()
        {
            return new List<ThongTinNhanVien>
    {
        // Ban giám đốc
        new ThongTinNhanVien { HoTen = "Nguyễn Văn Tổng", ChucVu = "Giám đốc", Email = "tong@nhaviet.com", GioiTinh = "Nam", MucLuong = 50000000, PhongBan = "Ban giám đốc" },
        new ThongTinNhanVien { HoTen = "Lê Thị Quyết", ChucVu = "Phó Giám đốc", Email = "quyet@nhaviet.com", GioiTinh = "Nữ", MucLuong = 40000000, PhongBan = "Ban giám đốc" },
        new ThongTinNhanVien { HoTen = "Hoàng Văn An", ChucVu = "Phó Giám đốc", Email = "a@nhaviet.com", GioiTinh = "Nam", MucLuong = 35000000, PhongBan = "Ban giám đốc" },
        new ThongTinNhanVien { HoTen = "Trần Thị Bình", ChucVu = "Cố vấn kỹ thuật", Email = "b@nhaviet.com", GioiTinh = "Nữ", MucLuong = 30000000, PhongBan = "Ban giám đốc" },
        new ThongTinNhanVien { HoTen = "Phạm Văn Cường", ChucVu = "Cố vấn kỹ thuật", Email = "c@nhaviet.com", GioiTinh = "Nam", MucLuong = 42000000, PhongBan = "Ban giám đốc" },

        // Phòng đấu thầu
        new ThongTinNhanVien { HoTen = "Nguyễn Hoàng Nam", ChucVu = "Trưởng phòng", Email = "thau@nhaviet.com", GioiTinh = "Nam", MucLuong = 57000000, PhongBan = "Phòng đấu thầu" },
        new ThongTinNhanVien { HoTen = "Võ Trần Nhật Long", ChucVu = "Phó trưởng phòng", Email = "mua@nhaviet.com", GioiTinh = "Nữ", MucLuong = 42000000, PhongBan = "Phòng đấu thầu" },
        new ThongTinNhanVien { HoTen = "Nguyễn Văn Đan", ChucVu = "Phó trưởng phòng", Email = "dan@nhaviet.com", GioiTinh = "Nam", MucLuong = 40000000, PhongBan = "Phòng đấu thầu" },
        new ThongTinNhanVien { HoTen = "Phan Văn Đức", ChucVu = "Chuyên viên", Email = "duc@nhaviet.com", GioiTinh = "Nam", MucLuong = 32000000, PhongBan = "Phòng đấu thầu" },
        new ThongTinNhanVien { HoTen = "Vũ Thị Lệ", ChucVu = "Chuyên viên", Email = "le@nhaviet.com", GioiTinh = "Nữ", MucLuong = 31000000, PhongBan = "Phòng đấu thầu" },
        new ThongTinNhanVien { HoTen = "Ngô Văn Tài", ChucVu = "Chuyên viên", Email = "tai@nhaviet.com", GioiTinh = "Nam", MucLuong = 31000000, PhongBan = "Phòng đấu thầu" },
        new ThongTinNhanVien { HoTen = "Hà Thị Quỳnh", ChucVu = "Chuyên viên", Email = "quynh@nhaviet.com", GioiTinh = "Nữ", MucLuong = 31500000, PhongBan = "Phòng đấu thầu" },
        new ThongTinNhanVien { HoTen = "Lý Văn Anh", ChucVu = "Thực tập", Email = "anh@nhaviet.com", GioiTinh = "Nam", MucLuong = 20000000, PhongBan = "Phòng đấu thầu" },

        // Nhân sự
        new ThongTinNhanVien { HoTen = "Trần Văn Nhân", ChucVu = "Nhân viên", Email = "nhan@nhaviet.com", GioiTinh = "Nam", MucLuong = 32000000, PhongBan = "Nhân sự" },
        new ThongTinNhanVien { HoTen = "Nguyễn Thị Sự", ChucVu = "Quản lý", Email = "su@nhaviet.com", GioiTinh = "Nữ", MucLuong = 37000000, PhongBan = "Nhân sự" },
        new ThongTinNhanVien { HoTen = "Lê Văn A", ChucVu = "Nhân viên", Email = "a1@nhaviet.com", GioiTinh = "Nam", MucLuong = 31000000, PhongBan = "Nhân sự" },
        new ThongTinNhanVien { HoTen = "Phan Thị B", ChucVu = "Nhân viên", Email = "b1@nhaviet.com", GioiTinh = "Nữ", MucLuong = 31500000, PhongBan = "Nhân sự" },
        new ThongTinNhanVien { HoTen = "Đỗ Văn C", ChucVu = "Nhân viên", Email = "c1@nhaviet.com", GioiTinh = "Nam", MucLuong = 30000000, PhongBan = "Nhân sự" },
        new ThongTinNhanVien { HoTen = "Trịnh Thị D", ChucVu = "Nhân viên", Email = "d1@nhaviet.com", GioiTinh = "Nữ", MucLuong = 30500000, PhongBan = "Nhân sự" },

        // Kế toán
        new ThongTinNhanVien { HoTen = "Võ Văn Kế", ChucVu = "Kế toán viên", Email = "ke@nhaviet.com", GioiTinh = "Nam", MucLuong = 36000000, PhongBan = "Kế toán" },
        new ThongTinNhanVien { HoTen = "Lê Thị Toán", ChucVu = "Trưởng phòng", Email = "toan@nhaviet.com", GioiTinh = "Nữ", MucLuong = 42000000, PhongBan = "Kế toán" },
        new ThongTinNhanVien { HoTen = "Nguyễn Văn G", ChucVu = "Nhân viên", Email = "g@nhaviet.com", GioiTinh = "Nam", MucLuong = 31000000, PhongBan = "Kế toán" },
        new ThongTinNhanVien { HoTen = "Trần Thị H", ChucVu = "Nhân viên", Email = "h@nhaviet.com", GioiTinh = "Nữ", MucLuong = 30500000, PhongBan = "Kế toán" },

        // Pháp lý
        new ThongTinNhanVien { HoTen = "Đinh Văn Luật", ChucVu = "Chuyên viên pháp lý", Email = "luat@nhaviet.com", GioiTinh = "Nam", MucLuong = 34000000, PhongBan = "Pháp lý" },
        new ThongTinNhanVien { HoTen = "Nguyễn Thị Quyền", ChucVu = "Nhân viên", Email = "quyen@nhaviet.com", GioiTinh = "Nữ", MucLuong = 31000000, PhongBan = "Pháp lý" },
        new ThongTinNhanVien { HoTen = "Trần Văn Pháp", ChucVu = "Nhân viên", Email = "phap@nhaviet.com", GioiTinh = "Nam", MucLuong = 32000000, PhongBan = "Pháp lý" },
    };
        }





        private void btnDashboard_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            DashboardControl dashboard = new DashboardControl(); // Tự tạo UserControl riêng
            dashboard.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(dashboard);
        }

        private void btnTaiChinh_Click(object sender, EventArgs e)
        {
            // Lấy danh sách nhân viên từ dictionary nhóm theo phòng ban
            var allNhanVien = dsNhanVienTheoPhongBan.Values.SelectMany(list => list).ToList();

            FinanceControl financeControl = new FinanceControl(allNhanVien);
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(financeControl);
        }

        private void btnDocuments_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            DocumentsControl docs = new DocumentsControl(currentUser);
            docs.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(docs);
        }

        private void btnReminder_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            ReminderControl_01 rem = new ReminderControl_01();
            rem.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(rem);
        }

        private void btnDepartment_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            DepartmentControl_01 dept = new DepartmentControl_01();
            dept.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(dept);
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
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
                MessageBox.Show("Không tìm thấy ảnh!");
            }

            avatar.SizeMode = PictureBoxSizeMode.StretchImage;
            avatar.Size = new Size(60, 60);
            avatar.Location = new Point(mainPanel.Width - 80, 10);
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


        private void TenderForm_Load(object sender, EventArgs e)
        {
        }

        private void TenderForm_Load_1(object sender, EventArgs e)
        {

        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnReminder_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        
    }
}
