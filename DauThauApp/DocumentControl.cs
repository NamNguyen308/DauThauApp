using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace DauThauApp
{
    public partial class DocumentsControl : UserControl
    {
        private Guna2ProgressIndicator loading;
        private Guna2MessageDialog messageDialog;

        public DocumentsControl()
        {
            InitializeComponent();
            LoadUI();
        }

        private void LoadUI()
        {
            // Tiêu đề
            Label title = new Label()
            {
                Text = "Tài Liệu Lưu Trữ",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(30, 30)
            };
            this.Controls.Add(title);

            // Loading
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
                Caption = "Chi tiết tài liệu",
                Buttons = MessageDialogButtons.OK,
                Icon = MessageDialogIcon.Information,
                Style = MessageDialogStyle.Light
            };

            // Flow panel
            FlowLayoutPanel flowPanel = new FlowLayoutPanel()
            {
                Location = new Point(30, 80),
                Size = new Size(850, 600),
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = Color.Transparent
            };
            this.Controls.Add(flowPanel);

            var documents = new List<(string TieuDe, string MoTa, DateTime NgayTao)>
            {
                ("Hồ sơ dự thầu công trình số 15", "Dự án xây dựng trường tiểu học Quảng Phú", new DateTime(2024, 2, 10)),
                ("Hồ sơ dự thầu công trình số 7", "Dự án nâng cấp hệ thống cấp nước phường Nghĩa Lộ", new DateTime(2024, 2, 15)),
                ("Hồ sơ dự thầu công trình số 23", "Gói thầu xây dựng showroom xe máy Hoàn Phước", new DateTime(2024, 2, 20)),
                ("Hồ sơ dự thầu công trình số 101", "Gói thầu cải tạo trung tâm y tế huyện Sơn Tịnh", new DateTime(2024, 2, 25)),
                ("Hồ sơ dự thầu công trình số 37", "Gói thầu xây dựng khu tái định cư 577", new DateTime(2024, 4, 5)),
                ("Hồ sơ dự thầu công trình số 6", "Gói thầu sửa chữa Trường Chuyên Lê Khiết", new DateTime(2024, 4, 6)),
                ("Hồ sơ chấm thầu", "Chấm thầu Xây dựng số 301-27", new DateTime(2024, 4, 7)),
                ("Biên bản họp thẩm định", "Thẩm định hồ sơ tài chính công trình số 09", new DateTime(2024, 4, 8)),
                ("Báo cáo tiến độ công trình Thành phố", "Tình trạng thi công đến 30/04/2024", new DateTime(2024, 4, 30)),
                ("Phụ lục tài liệu mời thầu", "Bổ sung bảng tiên lượng và bản vẽ kỹ thuật", new DateTime(2024, 3, 20))
            };

            int cardWidth = 800;
            int cardHeight = 110;

            foreach (var doc in documents)
            {
                Panel card = new Panel()
                {
                    Size = new Size(cardWidth, cardHeight),
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(0, 0, 0, 15)
                };

                Label lblTieuDe = new Label()
                {
                    Text = doc.TieuDe,
                    Font = new Font("Segoe UI", 13, FontStyle.Bold),
                    AutoSize = true,
                    Location = new Point(15, 10)
                };

                Label lblMoTa = new Label()
                {
                    Text = $"📝 Mô tả: {doc.MoTa}",
                    Font = new Font("Segoe UI", 10),
                    ForeColor = Color.DarkSlateGray, // màu mô tả
                    AutoSize = true,
                    Location = new Point(15, 40)
                };

                Label lblNgay = new Label()
                {
                    Text = $"📅 Ngày tạo: {doc.NgayTao:dd/MM/yyyy}",
                    Font = new Font("Segoe UI", 9, FontStyle.Italic),
                    ForeColor = Color.DarkRed, // màu nổi bật
                    AutoSize = true,
                    Location = new Point(15, 65)
                };

                Guna2Button btnXemChiTiet = new Guna2Button()
                {
                    Text = "Xem chi tiết",
                    Font = new Font("Segoe UI", 9F),
                    Size = new Size(110, 30),
                    Location = new Point(cardWidth - 130, cardHeight - 45),
                    BorderRadius = 6,
                    FillColor = Color.MediumSlateBlue,
                    ForeColor = Color.White
                };
                btnXemChiTiet.HoverState.FillColor = Color.MediumPurple;

                btnXemChiTiet.Click += async (s, e) =>
                {
                    loading.Visible = true;
                    await Task.Delay(1000);
                    loading.Visible = false;
                    messageDialog.Text = $"📄 {doc.TieuDe}\n\nMô tả: {doc.MoTa}\nNgày tạo: {doc.NgayTao:dd/MM/yyyy}";
                    messageDialog.Show();
                };

                card.Controls.Add(lblTieuDe);
                card.Controls.Add(lblMoTa);
                card.Controls.Add(lblNgay);
                card.Controls.Add(btnXemChiTiet);
                flowPanel.Controls.Add(card);
            }
        }

        private void DocumentsControl_Load(object sender, EventArgs e)
        {

        }
    }
}
