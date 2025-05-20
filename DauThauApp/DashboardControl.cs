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
    public partial class DashboardControl : UserControl
    {
        public DashboardControl()
        {
            InitializeComponent();

        }


        private void DashboardControl_Load(object sender, EventArgs e)
        {
            LoadDashboardData();
        }

        private void LoadDashboardData()
        {
            var tenders = new List<(string Ma, string Ten, decimal SoTien, DateTime ThoiHan, string ChuDauTu, string Loai, string TrangThai)>
    {
        ("G11-TXD-2025", "Gói thầu số 11: Thi công xây dựng và lắp đặt thiết bị công trình A", 120000000, DateTime.Now.AddDays(20), "Sở GTVT TP.HCM", "Xây lắp", "Đang mời thầu"),
        ("G12-TV-2025", "Gói thầu B: Tư vấn giám sát thi công tuyến đường QL50", 90000000, DateTime.Now.AddDays(10), "Ban QLDA Giao Thông", "Tư vấn", "Đang xét thầu"),
        ("G13-MSHH-2025", "Gói thầu C: Mua sắm thiết bị văn phòng cho UBND quận", 150000000, DateTime.Now.AddDays(30), "UBND Quận 1", "Mua sắm hàng hóa", "Đã đóng"),
        ("G14-TXD-2025", "Gói thầu D: Thi công cải tạo Trường Tiểu học Nguyễn Du", 200000000, DateTime.Now.AddDays(15), "Phòng GD&ĐT Quận 5", "Xây lắp", "Đang mời thầu")
    };

            // Tạo FlowLayoutPanel chứa các card
            FlowLayoutPanel flow = new FlowLayoutPanel();
            flow.Dock = DockStyle.Fill;
            flow.AutoScroll = true;
            flow.WrapContents = false; // Không xuống dòng ➝ xếp dọc
            flow.FlowDirection = FlowDirection.TopDown;
            flow.Padding = new Padding(30);
            flow.BackColor = Color.White;

            this.Controls.Clear();        // Xóa mọi control cũ
            this.Controls.Add(flow);      // Thêm FlowLayoutPanel vào giao diện

            foreach (var t in tenders)
            {
                Panel card = new Panel();
                card.Size = new Size(900, 180);
                card.Margin = new Padding(15);
                card.Padding = new Padding(15);
                card.BackColor = Color.WhiteSmoke;
                card.BorderStyle = BorderStyle.FixedSingle;

                // Mã + Tên gói
                Label lblTen = new Label();
                lblTen.Text = $"[{t.Ma}] {t.Ten}";
                lblTen.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                lblTen.AutoSize = true;
                lblTen.MaximumSize = new Size(card.Width - 30, 0);
                lblTen.Location = new Point(15, 15);

                // Loại + Chủ đầu tư
                Label lblLoai = new Label();
                lblLoai.Text = $"🔹 Loại: {t.Loai} | Chủ đầu tư: {t.ChuDauTu}";
                lblLoai.Font = new Font("Segoe UI", 11);
                lblLoai.ForeColor = Color.DarkSlateGray;
                lblLoai.AutoSize = true;
                lblLoai.Location = new Point(15, 60);

                // Số tiền
                Label lblTien = new Label();
                lblTien.Text = $"💰 Số tiền: {t.SoTien:N0} VND";
                lblTien.Font = new Font("Segoe UI", 11);
                lblTien.ForeColor = Color.DarkGreen;
                lblTien.AutoSize = true;
                lblTien.Location = new Point(15, 95);

                // Hạn + Trạng thái
                Label lblThoiHan = new Label();
                lblThoiHan.Text = $"📅 Hạn nộp: {t.ThoiHan:dd/MM/yyyy} | Trạng thái: {t.TrangThai}";
                lblThoiHan.Font = new Font("Segoe UI", 10, FontStyle.Italic);
                lblThoiHan.ForeColor = Color.DarkRed;
                lblThoiHan.AutoSize = true;
                lblThoiHan.Location = new Point(15, 125);

                // Thêm vào card
                card.Controls.Add(lblTen);
                card.Controls.Add(lblLoai);
                card.Controls.Add(lblTien);
                card.Controls.Add(lblThoiHan);

                // Thêm vào flow
                flow.Controls.Add(card);
            }
        }

    }
}
