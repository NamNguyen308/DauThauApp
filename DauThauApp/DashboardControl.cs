using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace DauThauApp
{
    public partial class DashboardControl : UserControl
    {
        private Guna2ProgressIndicator loading;
        private Guna2MessageDialog messageDialog;

        public DashboardControl()
        {
            InitializeComponent();
            LoadUI();
        }

        private void LoadUI()
        {
            // Tiêu đề
            Label title = new Label()
            {
                Text = "Tổng Quan Gói Thầu",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(30, 30)
            };
            this.Controls.Add(title);

            // Loader đậm màu
            loading = new Guna2ProgressIndicator()
            {
                Location = new Point(500, 30),
                Size = new Size(40, 40),
                ProgressColor = Color.FromArgb(85, 26, 139),
                BackColor = Color.Transparent,
                Visible = false
            };
            this.Controls.Add(loading);

            // MessageDialog
            messageDialog = new Guna2MessageDialog()
            {
                Caption = "Chi tiết gói thầu",
                Buttons = MessageDialogButtons.OK,
                Icon = MessageDialogIcon.Information,
                Style = MessageDialogStyle.Light
            };

            // FlowLayoutPanel chứa các card
            FlowLayoutPanel flow = new FlowLayoutPanel()
            {
                Location = new Point(30, 80),
                Size = new Size(960, 600),
                AutoScroll = true,
                WrapContents = false,
                FlowDirection = FlowDirection.TopDown,
                BackColor = Color.Transparent
            };
            this.Controls.Add(flow);

            // Danh sách gói thầu
            var tenders = new List<(string Ma, string Ten, decimal SoTien, DateTime ThoiHan, string ChuDauTu, string Loai, string TrangThai)>
            {
                ("G11-TXD-2025", "Gói thầu số 11: Tư vấn thiết kế và Trường THCS Quảng Phú", 1200000000, DateTime.Now.AddDays(20), "Phòng GD&ĐT TP.Quảng Ngãi", "Tư vấn xây dựng", "Đang thực hiện"),
                ("G12-TV-2025", "Gói thầu số 21: Tư vấn xây dựng tòa nhà showroom Hoàn Phước", 9083000099, DateTime.Now.AddDays(10), "Hoàn Phước", "Tư vấn", "Đang xét thầu"),
                ("G13-MSHH-2025", "Gói thầu số 3: Tư vấn thiết kế trung tâm y tế dự phòng ", 21300350034, DateTime.Now.AddDays(30), "Sở Y Tế Tỉnh", "Tư vấn", "Đã đóng"),
                ("G14-TXD-2025", "Gói thầu 46: Cải tạo Trường THPT Chuyên Lê Khiết", 100000000000, DateTime.Now.AddDays(15), "Sở GD&ĐT Tỉnh", "Xây lắp", "Đang mời thầu")
            };

            int cardHeight = 170;
            int cardWidth = 950;

            foreach (var t in tenders)
            {
                Panel card = new Panel()
                {
                    Size = new Size(cardWidth, cardHeight),
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(0, 0, 0, 15)
                };

                Label lblTen = new Label()
                {
                    Text = $"[{t.Ma}] {t.Ten}",
                    Font = new Font("Segoe UI", 14, FontStyle.Bold),
                    AutoSize = true,
                    Location = new Point(15, 15),
                    MaximumSize = new Size(cardWidth - 150, 0)
                };

                Label lblLoai = new Label()
                {
                    Text = $"🔹 Loại: {t.Loai} | Chủ đầu tư: {t.ChuDauTu}",
                    Font = new Font("Segoe UI", 11),
                    ForeColor = Color.DarkSlateGray,
                    AutoSize = true,
                    Location = new Point(15, 55)
                };

                Label lblTien = new Label()
                {
                    Text = $"💰 Số tiền: {t.SoTien:N0} VND",
                    Font = new Font("Segoe UI", 11),
                    ForeColor = Color.DarkGreen,
                    AutoSize = true,
                    Location = new Point(15, 85)
                };

                Label lblThoiHan = new Label()
                {
                    Text = $"📅 Hạn nộp: {t.ThoiHan:dd/MM/yyyy} | Trạng thái: {t.TrangThai}",
                    Font = new Font("Segoe UI", 10, FontStyle.Italic),
                    ForeColor = Color.DarkRed,
                    AutoSize = true,
                    Location = new Point(15, 115)
                };

                Guna2Button btnChiTiet = new Guna2Button()
                {
                    Text = "Xem chi tiết",
                    Font = new Font("Segoe UI", 9F),
                    Size = new Size(110, 30),
                    Location = new Point(cardWidth - 130, cardHeight - 45),
                    BorderRadius = 6,
                    FillColor = Color.MediumSlateBlue,
                    ForeColor = Color.White
                };
                btnChiTiet.HoverState.FillColor = Color.MediumPurple;

                btnChiTiet.Click += async (s, e) =>
                {
                    loading.Visible = true;
                    await Task.Delay(1500);
                    loading.Visible = false;
                    messageDialog.Text = $"Chi tiết gói thầu:\n\n{t.Ten}\nChủ đầu tư: {t.ChuDauTu}\nSố tiền: {t.SoTien:N0} VND";
                    messageDialog.Show();
                };

                card.Controls.Add(lblTen);
                card.Controls.Add(lblLoai);
                card.Controls.Add(lblTien);
                card.Controls.Add(lblThoiHan);
                card.Controls.Add(btnChiTiet);
                flow.Controls.Add(card);
            }
        }

        private void DashboardControl_Load(object sender, EventArgs e)
        {

        }
    }
}
