using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DauThauApp
{
    public class ThongTinNhanVien
    {
        public string HoTen { get; set; }
        public string ChucVu { get; set; }
        public string Email { get; set; }
        public string GioiTinh { get; set; }
        public decimal MucLuong { get; set; }
        public string PhongBan { get; set; }

    }

    public class Department
    {
        public string Ten { get; set; }
        public int SoNhanVien { get; set; }
        public string QuyenHan { get; set; }
        public string MoTa { get; set; }
        public DateTime NgayTao { get; set; }
    }

    public partial class DepartmentControl_01 : UserControl
    {
        private Guna2TextBox txtTenPB, txtSoNV, txtQuyenHan, txtMoTa, txtNgayTao;
        private Guna2Button btnCapNhat;
        private Guna2DataGridView dgvDanhSach;
        private List<Department> departments = new List<Department>();

        private Dictionary<string, List<ThongTinNhanVien>> dsNhanVienTheoPhongBan;

        public DepartmentControl_01()
        {
            InitializeComponent();
            InitializeUI();
            LoadDepartments();
            LoadDanhSach();
        }

        private void InitializeUI()
        {
            this.Dock = DockStyle.Fill;

            var mainLayout = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 350));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            this.Controls.Add(mainLayout);

            // Thông tin chi tiết
            var grpThongTin = new Guna2GroupBox()
            {
                Text = "THÔNG TIN PHÒNG BAN",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                FillColor = Color.AliceBlue,
                BorderRadius = 10,
                Dock = DockStyle.Fill
            };

            txtTenPB = CreateTextbox("Tên phòng ban:", 170, 40, 12);
            txtSoNV = CreateTextbox("Số nhân viên:", 170, 80, 12);
            txtQuyenHan = CreateTextbox("Quyền hạn:", 170, 120, 12);
            txtMoTa = CreateTextbox("Mô tả:", 170, 160, 12);
            txtNgayTao = CreateTextbox("Ngày thành lập:", 170, 200, 12);

            grpThongTin.Controls.AddRange(new Control[] {
                txtTenPB, txtSoNV, txtQuyenHan, txtMoTa, txtNgayTao
            });


            // DataGridView setup
            dgvDanhSach = new Guna2DataGridView()
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersHeight = 36,
                RowTemplate = { Height = 35 },
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true
            };
            dgvDanhSach.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            dgvDanhSach.DefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Regular);

            // Add columns ONCE here:
            dgvDanhSach.Columns.Add("Ten", "Tên phòng ban");
            dgvDanhSach.Columns.Add("SoNhanVien", "Số nhân viên");
            dgvDanhSach.Columns.Add("QuyenHan", "Quyền hạn");
            dgvDanhSach.Columns.Add("MoTa", "Mô tả");
            dgvDanhSach.Columns.Add("NgayTao", "Ngày thành lập");

            var btnCol = new DataGridViewButtonColumn
            {
                Name = "XemChiTiet",
                HeaderText = "Xem chi tiết",
                Text = "Xem chi tiết",
                UseColumnTextForButtonValue = true
            };
            dgvDanhSach.Columns.Add(btnCol);

            // Đăng ký sự kiện
            dgvDanhSach.CellContentClick += DgvDanhSach_CellContentClick;
            dgvDanhSach.CellClick += DgvDanhSach_CellClick;

            mainLayout.Controls.Add(grpThongTin, 0, 0);
            mainLayout.Controls.Add(dgvDanhSach, 0, 1);
        }

        private Guna2TextBox CreateTextbox(string placeholder, int x, int y, float fontSize = 10)
        {
            return new Guna2TextBox()
            {
                PlaceholderText = placeholder,
                Location = new Point(x, y),
                Size = new Size(600, 30),
                Font = new Font("Segoe UI", fontSize),
                BorderRadius = 6,
                ReadOnly = true
            };
        }

        private void LoadDepartments()
        {
            dsNhanVienTheoPhongBan = new Dictionary<string, List<ThongTinNhanVien>>()
            {
                ["Ban giám đốc"] = new List<ThongTinNhanVien>
    {
        new ThongTinNhanVien { HoTen = "Nguyễn Văn Tổng", ChucVu = "Giám đốc", Email = "tong@nhaviet.com", GioiTinh = "Nam", MucLuong = 50000000 },
        new ThongTinNhanVien { HoTen = "Lê Thị Quyết", ChucVu = "Phó Giám đốc", Email = "quyet@nhaviet.com", GioiTinh = "Nữ", MucLuong = 40000000 },
        new ThongTinNhanVien { HoTen = "Hoàng Văn An", ChucVu = "Phó Giám đốc", Email = "a@nhaviet.com", GioiTinh = "Nam", MucLuong = 35000000 },
        new ThongTinNhanVien { HoTen = "Trần Thị Bình", ChucVu = "Cố vấn kỹ thuật", Email = "b@nhaviet.com", GioiTinh = "Nữ", MucLuong = 30000000 },
        new ThongTinNhanVien { HoTen = "Phạm Văn Cường", ChucVu = "Cố vấn kỹ thuật", Email = "c@nhaviet.com", GioiTinh = "Nam", MucLuong = 42000000 }
    },
                ["Phòng đấu thầu"] = new List<ThongTinNhanVien>
    {
        new ThongTinNhanVien { HoTen = "Nguyễn Hoàng Nam", ChucVu = "Trưởng phòng", Email = "thau@nhaviet.com", GioiTinh = "Nam", MucLuong = 57000000 },
        new ThongTinNhanVien { HoTen = "Võ Trần Nhật Long", ChucVu = "Phó trưởng phòng", Email = "mua@nhaviet.com", GioiTinh = "Nữ", MucLuong = 42000000 },
        new ThongTinNhanVien { HoTen = "Nguyễn Văn Đan", ChucVu = "Phó trưởng phòng", Email = "dan@nhaviet.com", GioiTinh = "Nam", MucLuong = 40000000 },
        new ThongTinNhanVien { HoTen = "Phan Văn Đức", ChucVu = "Chuyên viên", Email = "duc@nhaviet.com", GioiTinh = "Nam", MucLuong = 32000000 },
        new ThongTinNhanVien { HoTen = "Vũ Thị Lệ", ChucVu = "Chuyên viên", Email = "le@nhaviet.com", GioiTinh = "Nữ", MucLuong = 31000000 },
        new ThongTinNhanVien { HoTen = "Ngô Văn Tài", ChucVu = "Chuyên viên", Email = "tai@nhaviet.com", GioiTinh = "Nam", MucLuong = 31000000 },
        new ThongTinNhanVien { HoTen = "Hà Thị Quỳnh", ChucVu = "Chuyên viên", Email = "quynh@nhaviet.com", GioiTinh = "Nữ", MucLuong = 31500000 },
        new ThongTinNhanVien { HoTen = "Lý Văn Anh", ChucVu = "Thực tập", Email = "anh@nhaviet.com", GioiTinh = "Nam", MucLuong = 20000000 }
    },
                ["Nhân sự"] = new List<ThongTinNhanVien>
    {
        new ThongTinNhanVien { HoTen = "Trần Văn Nhân", ChucVu = "Nhân viên", Email = "nhan@nhaviet.com", GioiTinh = "Nam", MucLuong = 32000000 },
        new ThongTinNhanVien { HoTen = "Nguyễn Thị Sự", ChucVu = "Quản lý", Email = "su@nhaviet.com", GioiTinh = "Nữ", MucLuong = 37000000 },
        new ThongTinNhanVien { HoTen = "Lê Văn A", ChucVu = "Nhân viên", Email = "a1@nhaviet.com", GioiTinh = "Nam", MucLuong = 31000000 },
        new ThongTinNhanVien { HoTen = "Phan Thị B", ChucVu = "Nhân viên", Email = "b1@nhaviet.com", GioiTinh = "Nữ", MucLuong = 31500000 },
        new ThongTinNhanVien { HoTen = "Đỗ Văn C", ChucVu = "Nhân viên", Email = "c1@nhaviet.com", GioiTinh = "Nam", MucLuong = 30000000},
        new ThongTinNhanVien { HoTen = "Trịnh Thị D", ChucVu = "Nhân viên", Email = "d1@nhaviet.com", GioiTinh = "Nữ", MucLuong = 30500000 }
    },
                ["Kế toán"] = new List<ThongTinNhanVien>
    {
        new ThongTinNhanVien { HoTen = "Võ Văn Kế", ChucVu = "Kế toán viên", Email = "ke@nhaviet.com", GioiTinh = "Nam", MucLuong = 36000000 },
        new ThongTinNhanVien { HoTen = "Lê Thị Toán", ChucVu = "Trưởng phòng", Email = "toan@nhaviet.com", GioiTinh = "Nữ", MucLuong = 42000000 },
        new ThongTinNhanVien { HoTen = "Nguyễn Văn G", ChucVu = "Nhân viên", Email = "g@nhaviet.com", GioiTinh = "Nam", MucLuong = 31000000 },
        new ThongTinNhanVien { HoTen = "Trần Thị H", ChucVu = "Nhân viên", Email = "h@nhaviet.com", GioiTinh = "Nữ", MucLuong = 30500000 }
    },
                ["Pháp lý"] = new List<ThongTinNhanVien>
    {
        new ThongTinNhanVien { HoTen = "Đinh Văn Luật", ChucVu = "Chuyên viên pháp lý", Email = "luat@nhaviet.com", GioiTinh = "Nam", MucLuong = 34000000 },
        new ThongTinNhanVien { HoTen = "Nguyễn Thị Quyền", ChucVu = "Nhân viên", Email = "quyen@nhaviet.com", GioiTinh = "Nữ", MucLuong = 31000000 },
        new ThongTinNhanVien { HoTen = "Trần Văn Pháp", ChucVu = "Nhân viên", Email = "phap@nhaviet.com", GioiTinh = "Nam", MucLuong = 32000000 }
    }
            };


            var quyenHanDict = new Dictionary<string, string>()
            {
                ["Ban giám đốc"] = "Quản lý cao cấp",
                ["Phòng đấu thầu"] = "Chuyên trách đấu thầu",
                ["Nhân sự"] = "Quản lý nhân sự",
                ["Kế toán"] = "Phòng tài chính kế toán",
                ["Pháp lý"] = "Phòng pháp chế"
            };

            var moTaDict = new Dictionary<string, string>()
            {
                ["Ban giám đốc"] = "Phòng ban điều hành công ty",
                ["Phòng đấu thầu"] = "Phòng xử lý hồ sơ đấu thầu",
                ["Nhân sự"] = "Phòng quản lý nhân sự",
                ["Kế toán"] = "Phòng tài chính kế toán",
                ["Pháp lý"] = "Phòng xử lý các vấn đề pháp lý"
            };

            var ngayTaoDict = new Dictionary<string, DateTime>()
            {
                ["Ban giám đốc"] = new DateTime(2010, 1, 1),
                ["Phòng đấu thầu"] = new DateTime(2012, 5, 15),
                ["Nhân sự"] = new DateTime(2015, 7, 10),
                ["Kế toán"] = new DateTime(2013, 3, 20),
                ["Pháp lý"] = new DateTime(2016, 9, 1)
            };

            departments = new List<Department>();

            foreach (var pb in dsNhanVienTheoPhongBan.Keys)
            {
                departments.Add(new Department
                {
                    Ten = pb,
                    SoNhanVien = dsNhanVienTheoPhongBan[pb].Count,
                    QuyenHan = quyenHanDict.ContainsKey(pb) ? quyenHanDict[pb] : "",
                    MoTa = moTaDict.ContainsKey(pb) ? moTaDict[pb] : "",
                    NgayTao = ngayTaoDict.ContainsKey(pb) ? ngayTaoDict[pb] : DateTime.MinValue
                });
            }
        }

        private void LoadDanhSach()
        {
            dgvDanhSach.Rows.Clear();

            foreach (var pb in departments)
            {
                dgvDanhSach.Rows.Add(
                    pb.Ten,
                    pb.SoNhanVien,
                    pb.QuyenHan,
                    pb.MoTa,
                    pb.NgayTao.ToString("dd/MM/yyyy")
                );
            }
        }

        private void DgvDanhSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvDanhSach.Columns[e.ColumnIndex].Name == "XemChiTiet")
            {
                string tenPB = dgvDanhSach.Rows[e.RowIndex].Cells["Ten"].Value.ToString();

                if (dsNhanVienTheoPhongBan.ContainsKey(tenPB))
                {
                    var dsNV = dsNhanVienTheoPhongBan[tenPB];

                    var detailsForm = new NhanVienDetailsForm(tenPB, dsNV);
                    detailsForm.StartPosition = FormStartPosition.CenterParent;

                    // Hiện cửa sổ dưới dạng modal để người dùng tập trung xem
                    detailsForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin nhân viên cho phòng này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }


        private void DgvDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvDanhSach.Rows[e.RowIndex];

            txtTenPB.Text = row.Cells["Ten"].Value.ToString();
            txtSoNV.Text = row.Cells["SoNhanVien"].Value.ToString();
            txtQuyenHan.Text = row.Cells["QuyenHan"].Value.ToString();
            txtMoTa.Text = row.Cells["MoTa"].Value.ToString();
            txtNgayTao.Text = row.Cells["NgayTao"].Value.ToString();
        }
    }
}
