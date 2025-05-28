using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DauThauApp
{
    public partial class ReminderControl : UserControl
    {
        public ReminderControl()
        {
            InitializeComponent();
            LoadUI();
        }

        private void LoadUI()
        {
            Label title = new Label()
            {
                Text = "Yêu Cầu Từ Các Bộ Phận",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(30, 30)
            };
            this.Controls.Add(title);

            FlowLayoutPanel flowPanel = new FlowLayoutPanel()
            {
                Location = new Point(30, 80),
                Size = new Size(800, 450),
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = Color.Transparent
            };
            this.Controls.Add(flowPanel);

            var reminders = new List<(string NoiDung, string PhongBan, string TrangThai)>
            {
                ("Kiểm tra hồ sơ dự thầu", "Phòng đấu thầu", "Đang chờ xử lí"),
                ("Phê duyệt yêu cầu mua hồ sơ mời thầu", "Phòng đấu thầu", "Đã phê duyệt"),
                ("Xác minh thông tin nhà thầu", "Phòng pháp lý", "Đang xử lí"),
                ("Cập nhật nhân sự dự án", "Phòng nhân sự", "Chờ duyệt"),
                ("Phê duyệt dự toán tài chính", "Phòng kế toán", "Đã gửi"),
                ("Bổ sung tài liệu dự thầu", "Phòng đấu thầu", "Đang xử lí"),
                ("Rà soát hợp đồng", "Phòng pháp lý", "Đã duyệt"),
                ("Duyệt chi phí tư vấn", "Phòng kế toán", "Chờ duyệt"),
                ("Kiểm tra bản vẽ kỹ thuật", "Phòng kỹ thuật", "Đang chờ xử lí"),
                ("Phản hồi đơn khiếu nại", "Phòng pháp lý", "Đang xử lí")
            };

            int cardWidth = 750;
            int cardHeight = 90;

            foreach (var item in reminders)
            {
                Panel card = new Panel()
                {
                    Size = new Size(cardWidth, cardHeight),
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(0, 0, 0, 12)
                };

                Label lblNoiDung = new Label()
                {
                    Text = item.NoiDung,
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                    AutoSize = true,
                    Location = new Point(15, 10)
                };

                Label lblPhongBan = new Label()
                {
                    Text = $"Phòng ban: {item.PhongBan}",
                    Font = new Font("Segoe UI", 10F),
                    AutoSize = true,
                    Location = new Point(15, 35)
                };

                Color trangThaiColor;

                if (item.TrangThai.Contains("Đã phê duyệt"))
                    trangThaiColor = Color.Green;
                else if (item.TrangThai.Contains("Đã duyệt"))
                    trangThaiColor = Color.SeaGreen;
                else if (item.TrangThai.Contains("Đã gửi"))
                    trangThaiColor = Color.SteelBlue;
                else if (item.TrangThai.Contains("Chờ duyệt"))
                    trangThaiColor = Color.Orange;
                else if (item.TrangThai.Contains("Đang"))
                    trangThaiColor = Color.IndianRed;
                else
                    trangThaiColor = Color.Black;

                Label lblTrangThai = new Label()
                {
                    Text = $"Trạng thái: {item.TrangThai}",
                    Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                    ForeColor = trangThaiColor,
                    AutoSize = true,
                    Location = new Point(15, 55)
                };


                card.Controls.Add(lblNoiDung);
                card.Controls.Add(lblPhongBan);
                card.Controls.Add(lblTrangThai);
                flowPanel.Controls.Add(card);
            }
        }

        private void ReminderControl_Load(object sender, EventArgs e)
        {
        }
    }
}
