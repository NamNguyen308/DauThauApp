using Guna.UI2.WinForms;
using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DauThauApp
{
    public partial class DepartmentControl : UserControl
    {
        private FlowLayoutPanel flowPanel;
        private Panel detailPanel;
        private PdfViewer pdfViewer;
        private Guna2Button btnCloseDetail;
        private Guna2Button btnDownload;
        private Label detailTitleLabel;
        private Label detailDescLabel;
        private Label detailDateLabel;
        private Guna2MessageDialog messageDialog;

        // Dữ liệu phòng ban kèm thông tin PDF (giả định đường dẫn file pdf)
        private List<(string Ten, int SoNhanVien, string QuyenHan, string MoTa, DateTime NgayTao, string PdfPath)> departments;

        public DepartmentControl()
        {
            InitializeComponent();
            InitializeCustomUI();
            LoadDepartments();
            DisplayDepartments();
        }

        private void InitializeCustomUI()
        {
            Label title = new Label()
            {
                Text = "Phòng Ban",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(30, 30)
            };
            this.Controls.Add(title);

            Guna2TextBox txtSearch = new Guna2TextBox()
            {
                PlaceholderText = "🔍 Tìm kiếm",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Size = new Size(230, 30),
                Location = new Point(600, 30),
                FillColor = Color.White,                     // Trắng ngà nhẹ giúp viền nổi bật hơn
                ForeColor = Color.Black,
                PlaceholderForeColor = Color.DarkGray,
                BorderColor = Color.FromArgb(100, 100, 100),       // màu viền
                BorderRadius = 8,               // bo góc mượt
                BorderThickness = 1,
                Cursor = Cursors.IBeam,
            };

            txtSearch.TextChanged += (s, e) => { FilterDocuments(txtSearch.Text); };
            this.Controls.Add(txtSearch);
            txtSearch.BringToFront();


            messageDialog = new Guna2MessageDialog()
            {
                Caption = "Thông báo",
                Buttons = MessageDialogButtons.OK,
                Icon = MessageDialogIcon.Information,
                Style = MessageDialogStyle.Light
            };

            flowPanel = new FlowLayoutPanel()
            {
                Location = new Point(30, 80),
                Size = new Size(1100, 620),
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = Color.Transparent
            };
            this.Controls.Add(flowPanel);

            // detailPanel ẩn, dùng để hiện thông tin + pdf
            detailPanel = new Panel()
            {
                Size = new Size(900, 650),
                Location = new Point(30, 30),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Visible = false,
                Padding = new Padding(20)
            };
            this.Controls.Add(detailPanel);

            // Label hiển thị tiêu đề
            detailTitleLabel = new Label()
            {
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };
            detailPanel.Controls.Add(detailTitleLabel);

            // Label mô tả
            detailDescLabel = new Label()
            {
                Font = new Font("Segoe UI", 11),
                Location = new Point(20, 60),
                Size = new Size(600, 30),
                ForeColor = Color.DarkSlateGray
            };
            detailPanel.Controls.Add(detailDescLabel);

            // Label ngày tạo
            detailDateLabel = new Label()
            {
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                Location = new Point(20, 95),
                AutoSize = true,
                ForeColor = Color.DarkRed
            };
            detailPanel.Controls.Add(detailDateLabel);

            pdfViewer = new PdfViewer()
            {
                Dock = DockStyle.Bottom,
                Height = 450,
                ZoomMode = PdfViewerZoomMode.FitWidth,
                Visible = false
            };
            detailPanel.Controls.Add(pdfViewer);

            btnCloseDetail = new Guna2Button()
            {
                Text = "Đóng",
                Size = new Size(100, 30),
                Location = new Point(detailPanel.Width - 120, 30),
                FillColor = Color.Gray,
                ForeColor = Color.White,
                BorderRadius = 6
            };
            btnCloseDetail.Click += (s, e) =>
            {
                pdfViewer.Document?.Dispose();
                pdfViewer.Visible = false;
                detailPanel.Visible = false;
            };
            detailPanel.Controls.Add(btnCloseDetail);

            btnDownload = new Guna2Button()
            {
                Text = "Tải xuống",
                Size = new Size(100, 30),
                Location = new Point(detailPanel.Width - 240, 30),
                FillColor = Color.ForestGreen,
                ForeColor = Color.White,
                BorderRadius = 6
            };
            btnDownload.Click += BtnDownload_Click;
            detailPanel.Controls.Add(btnDownload);
        }

        private void LoadDepartments()
        {
            // Giả sử mỗi phòng ban có thêm mô tả, ngày tạo và đường dẫn PDF
            departments = new List<(string, int, string, string, DateTime, string)>
{
    ("Ban giám đốc", 5, "Toàn bộ quyền", "Phòng ban quản lý cao nhất", new DateTime(2023, 1, 15), @"Documents\bgd.pdf"),
    ("Phòng đấu thầu", 8, "Chỉnh sửa HS thầu, Xem HS tài chính", "Phòng chuyên trách đấu thầu", new DateTime(2023, 3, 10), @"Documents\dauthau.pdf"),
    ("Nhân sự", 6, "Chỉnh sửa HS nhân sự", "Phòng chuyên về nhân sự", new DateTime(2023, 5, 20), @"Documents\nhansu.pdf"),
    ("Kế toán", 4, "Chỉnh sửa HS tài chính, Xem HS thầu", "Phòng quản lý tài chính", new DateTime(2023, 2, 5), @"Documents\ketoan.pdf"),
    ("Pháp lý", 3, "Xem HS thầu", "Phòng phụ trách pháp lý", new DateTime(2023, 4, 25), @"Documents\phaply.pdf")
};
        }

        private void DisplayDepartments(List<(string Ten, int SoNhanVien, string QuyenHan, string MoTa, DateTime NgayTao, string PdfPath)> list = null)
        {
            flowPanel.Controls.Clear();
            var source = list ?? departments;
            int cardWidth = 800;
            int cardHeight = 110;

            foreach (var dept in source)
            {
                var currentDept = dept;

                Guna2Panel card = new Guna2Panel()
                {
                    Size = new Size(850, 100),
                    BorderRadius = 15,
                    FillColor = Color.White,
                    BorderColor = Color.LightGray,
                    BorderThickness = 1,
                    Margin = new Padding(0, 0, 0, 15),
                    ShadowDecoration = { Enabled = true, BorderRadius = 15, Color = Color.Gray, Shadow = new Padding(3) }
                };

                Label lblTen = new Label()
                {
                    Text = currentDept.Ten,
                    Font = new Font("Segoe UI", 16, FontStyle.Bold),
                    AutoSize = true,
                    Location = new Point(15, 10)
                };

                Label lblSoNV = new Label()
                {
                    Text = $"Số nhân viên: {currentDept.SoNhanVien}",
                    Font = new Font("Segoe UI", 13),
                    ForeColor = Color.DarkGreen,
                    AutoSize = true,
                    Location = new Point(15, 40)
                };

                Label lblQuyen = new Label()
                {
                    Text = $"Quyền hạn: {currentDept.QuyenHan}",
                    Font = new Font("Segoe UI", 12),
                    ForeColor = Color.DarkSlateBlue,
                    AutoSize = true,
                    Location = new Point(15, 65)
                };

                Guna2Button btnChiTiet = new Guna2Button()
                {
                    Text = "Xem chi tiết",
                    Font = new Font("Segoe UI", 9F),
                    Size = new Size(110, 30),
                    Location = new Point(cardWidth - 100, cardHeight - 68),
                    BorderRadius = 6,
                    FillColor = Color.MediumSlateBlue,
                    ForeColor = Color.White
                };

                btnChiTiet.Click += (s, e) =>
                {
                    detailTitleLabel.Text = currentDept.Ten;
                    detailDescLabel.Text = $"📌 Mô tả: {currentDept.MoTa}";
                    detailDateLabel.Text = $"📅 Ngày tạo: {currentDept.NgayTao:dd/MM/yyyy}";

                    string pdfPath = Path.Combine(Application.StartupPath, currentDept.PdfPath);
                    if (File.Exists(pdfPath))
                    {
                        pdfViewer.Document?.Dispose();
                        pdfViewer.Document = PdfiumViewer.PdfDocument.Load(pdfPath);
                        pdfViewer.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show($"Không tìm thấy file PDF:\n{pdfPath}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        pdfViewer.Visible = false;
                    }

                    detailPanel.Visible = true;
                    detailPanel.BringToFront();
                };

                card.Controls.Add(lblTen);
                card.Controls.Add(lblSoNV);
                card.Controls.Add(lblQuyen);
                card.Controls.Add(btnChiTiet);
                flowPanel.Controls.Add(card);
            }
        }


        private void ShowDetailPanel((string Ten, int SoNhanVien, string QuyenHan, string MoTa, DateTime NgayTao, string PdfPath) dept)
        {
            detailTitleLabel.Text = dept.Ten;
            detailDescLabel.Text = $"Mô tả: {dept.MoTa}";
            detailDateLabel.Text = $"Ngày thành lập: {dept.NgayTao:dd/MM/yyyy}";

            if (System.IO.File.Exists(dept.PdfPath))
            {
                pdfViewer.Document?.Dispose();
                pdfViewer.Document = PdfDocument.Load(dept.PdfPath);
                pdfViewer.Visible = true;
            }
            else
            {
                pdfViewer.Visible = false;
                messageDialog.Text = "Không tìm thấy file PDF của phòng ban này.";
                messageDialog.Show();
            }

            detailPanel.Visible = true;
            detailPanel.BringToFront();
        }


        private void FilterDocuments(string keyword)
        {
            var filtered = departments.FindAll(d =>
                d.Ten.ToLower().Contains(keyword.ToLower()) ||
                d.QuyenHan.ToLower().Contains(keyword.ToLower()) ||
                d.MoTa.ToLower().Contains(keyword.ToLower())
            );
            DisplayDepartments(filtered);
        }

        private void BtnDownload_Click(object sender, EventArgs e)
        {
            if (pdfViewer.Document == null) return;

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PDF files (*.pdf)|*.pdf";
                sfd.FileName = detailTitleLabel.Text + ".pdf";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pdfViewer.Document.Save(sfd.FileName);
                        messageDialog.Text = "Tải xuống thành công.";
                        messageDialog.Show();
                    }
                    catch (Exception ex)
                    {
                        messageDialog.Text = "Lỗi khi tải xuống: " + ex.Message;
                        messageDialog.Show();
                    }
                }
            }
        }

        private void DepartmentControl_Load(object sender, EventArgs e)
        {

        }
    }
}
