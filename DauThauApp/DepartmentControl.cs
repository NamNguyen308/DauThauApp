// DepartmentControl.cs
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace DauThauApp
{
    public partial class DepartmentControl : UserControl
    {
        private Guna2ProgressIndicator loading;
        private Guna2MessageDialog messageDialog;

        public DepartmentControl()
        {
            InitializeComponent();
            LoadUI();
        }

        private void LoadUI()
        {
            Label title = new Label()
            {
                Text = "Phòng Ban Quản Lý",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(30, 30)
            };
            this.Controls.Add(title);

            // Loader
            loading = new Guna2ProgressIndicator()
            {
                Location = new Point(500, 30),
                Size = new Size(50, 50),
                ProgressColor = Color.FromArgb(102, 0, 234),
                Visible = false
            };
            this.Controls.Add(loading);

            // Dialog
            messageDialog = new Guna2MessageDialog()
            {
                Caption = "Thông báo",
                Buttons = MessageDialogButtons.OK,
                Icon = MessageDialogIcon.Information,
                Style = MessageDialogStyle.Light
            };

            FlowLayoutPanel flowPanel = new FlowLayoutPanel()
            {
                Location = new Point(30, 80),
                Size = new Size(600, 600),
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = Color.Transparent
            };
            this.Controls.Add(flowPanel);

            var departments = new List<(string Ten, int SoNhanVien, string QuyenHan)>
            {
                ("Ban giám đốc", 5, "Toàn bộ quyền"),
                ("Phòng đấu thầu", 8, "Chỉnh sửa HS thầu, Xem HS tài chính"),
                ("Nhân sự", 6, "Chỉnh sửa HS nhân sự"),
                ("Kế toán", 4, "Chỉnh sửa HS tài chính, Xem HS thầu"),
                ("Pháp lý", 3, "Xem HS thầu")
            };

            int cardHeight = 120;
            int cardWidth = 550;

            foreach (var dept in departments)
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
                    Text = dept.Ten,
                    Font = new Font("Segoe UI", 14, FontStyle.Bold),
                    AutoSize = true,
                    Location = new Point(15, 15)
                };

                Label lblSoNV = new Label()
                {
                    Text = $"Số nhân viên: {dept.SoNhanVien}",
                    Font = new Font("Segoe UI", 11),
                    ForeColor = Color.DarkGreen,
                    AutoSize = true,
                    Location = new Point(15, 45)
                };

                Label lblQuyen = new Label()
                {
                    Text = $"Quyền hạn: {dept.QuyenHan}",
                    Font = new Font("Segoe UI", 10, FontStyle.Italic),
                    ForeColor = Color.DarkSlateBlue,
                    AutoSize = true,
                    Location = new Point(15, 70)
                };

                Guna2Button btnAction = new Guna2Button()
                {
                    Text = "Xem chi tiết",
                    Font = new Font("Segoe UI", 9F),
                    Size = new Size(110, 30),
                    Location = new Point(cardWidth - 130, cardHeight - 45),
                    BorderRadius = 6,
                    FillColor = Color.MediumSlateBlue,
                    ForeColor = Color.White
                };
                btnAction.HoverState.FillColor = Color.MediumPurple;

                btnAction.Click += async (s, e) =>
                {
                    loading.Visible = true;
                    await Task.Delay(1500); // giả lập xử lý
                    loading.Visible = false;
                    messageDialog.Text = $"Thông tin chi tiết: {dept.Ten}";
                    messageDialog.Show();
                };

                card.Controls.Add(lblTen);
                card.Controls.Add(lblSoNV);
                card.Controls.Add(lblQuyen);
                card.Controls.Add(btnAction);
                flowPanel.Controls.Add(card);
            }
        }

        private void DepartmentControl_Load(object sender, EventArgs e)
        {
        }
    }
}