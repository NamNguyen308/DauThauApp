using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DauThauApp
{
    public partial class NhanVienDetailsForm : Form
    {
        private Guna2DataGridView dgvNhanVien;
        private Guna2HtmlLabel lblTieuDe;

        public NhanVienDetailsForm(string tenPhongBan, List<ThongTinNhanVien> dsNhanVien)
        {
            InitializeComponent();

            this.Text = $"Danh sách nhân viên - {tenPhongBan}";
            this.Size = new Size(1000, 600);
            this.BackColor = Color.White;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Layout chính
            var mainLayout = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1,
                BackColor = Color.White
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            this.Controls.Add(mainLayout);

            // Tiêu đề
            lblTieuDe = new Guna2HtmlLabel()
            {
                Text = $"👥 Danh sách nhân viên - {tenPhongBan}",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = Color.DarkSlateBlue,
                BackColor = Color.Transparent,
                TextAlignment = ContentAlignment.MiddleCenter,
                AutoSize = false,
                Padding = new Padding(10)
            };
            mainLayout.Controls.Add(lblTieuDe, 0, 0);

            // Bảng nhân viên
            dgvNhanVien = new Guna2DataGridView()
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.AliceBlue },
                ColumnHeadersHeight = 40,
                RowTemplate = { Height = 36 },
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BorderStyle = BorderStyle.None,
                GridColor = Color.LightGray,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.MidnightBlue,
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 13, FontStyle.Bold),
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                },
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 12),
                    ForeColor = Color.Black,
                    BackColor = Color.White,
                    SelectionBackColor = Color.LightSkyBlue,
                    SelectionForeColor = Color.Black,
                    Alignment = DataGridViewContentAlignment.MiddleLeft
                },
                EnableHeadersVisualStyles = false
            };

            // Cột
            dgvNhanVien.Columns.Add("HoTen", "Họ tên");
            dgvNhanVien.Columns.Add("ChucVu", "Chức vụ");
            dgvNhanVien.Columns.Add("MucLuong", "Mức lương");
            dgvNhanVien.Columns.Add("Email", "Email");
            dgvNhanVien.Columns.Add("GioiTinh", "Giới tính");

            // Dữ liệu
            foreach (var nv in dsNhanVien)
            {
                string mucLuongFormatted = nv.MucLuong.ToString("N0") + " VND"; // Ví dụ: 15,000,000 VND
                dgvNhanVien.Rows.Add(nv.HoTen, nv.ChucVu, mucLuongFormatted, nv.Email, nv.GioiTinh);
            }

            mainLayout.Controls.Add(dgvNhanVien, 0, 1);
        }

        private void NhanVienDetailsForm_Load(object sender, EventArgs e)
        {

        }
    }
}
