using Guna.UI2.WinForms;
using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DauThauApp
{
    public partial class DashboardControl : UserControl
    {
        private Guna2ProgressIndicator loading;
        private Panel pdfPanel;
        private PdfiumViewer.PdfViewer pdfViewer;
        private string currentPdfFileName;

        public DashboardControl()
        {
            InitializeComponent();
            LoadUI();
        }

        private void LoadUI()
        {
            Label title = new Label()
            {
                Text = "Tổng Quan Gói Thầu",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(30, 30)
            };
            this.Controls.Add(title);



            loading = new Guna2ProgressIndicator()
            {
                Location = new Point(500, 30),
                Size = new Size(40, 40),
                ProgressColor = Color.FromArgb(85, 26, 139),
                BackColor = Color.Transparent,
                Visible = false
            };
            this.Controls.Add(loading);




            FlowLayoutPanel flow = new FlowLayoutPanel()
            {
                Location = new Point(30, 80),
                Size = new Size(1100, 620),
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = Color.Transparent
            };
            this.Controls.Add(flow);

            pdfPanel = new Panel()
            {
                Location = new Point(30, 30),
                Size = new Size(1000, 650),
                Visible = false,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Padding = new Padding(20)
            };
            this.Controls.Add(pdfPanel);

            pdfViewer = new PdfiumViewer.PdfViewer()
            {
                Dock = DockStyle.Fill,
                ZoomMode = PdfViewerZoomMode.FitWidth
            };
            pdfPanel.Controls.Add(pdfViewer);

            Guna2Button btnClose = new Guna2Button()
            {
                Text = "Đóng",
                Size = new Size(100, 30),
                Location = new Point(800, 90),
                FillColor = Color.Gray,
                ForeColor = Color.White,
                BorderRadius = 6,
                // Nếu muốn tắt viền thì thêm:
                // BorderThickness = 0,
            };
            btnClose.Click += (s, e) =>
            {
                pdfPanel.Visible = false;
                pdfViewer.Document?.Dispose();
            };
            pdfPanel.Controls.Add(btnClose);
            btnClose.BringToFront();


            Guna2Button btnDownload = new Guna2Button()
            {
                Text = "Tải xuống",
                Size = new Size(100, 30),
                Location = new Point(680, 90),
                FillColor = Color.ForestGreen,
                ForeColor = Color.White,
                BorderRadius = 6
            };
            btnDownload.Click += BtnDownload_Click;
            pdfPanel.Controls.Add(btnDownload);
            btnDownload.BringToFront();

            // Danh sách gói thầu + file PDF
            var tenders = new List<(string Ma, string Ten, decimal SoTien, DateTime ThoiHan, string ChuDauTu, string Loai, string TrangThai, string FilePDF)>
            {
                ("G11-TXD-2025", "Gói thầu số 11: Tư vấn thiết kế Trường THCS Quảng Phú", 1200000000, DateTime.Now.AddDays(20), "Phòng GD&ĐT TP.Quảng Ngãi", "Tư vấn xây dựng", "Đang thực hiện", "G11-TXD-2025.pdf"),
                ("G12-TV-2025", "Gói thầu số 21: Tư vấn xây dựng showroom Hoàn Phước", 9083000099, DateTime.Now.AddDays(10), "Hoàn Phước", "Tư vấn", "Đang xét thầu", "G12-TV-2025.pdf"),
                ("G13-MSHH-2025", "Gói thầu số 3: Thiết kế trung tâm y tế dự phòng", 21300350034, DateTime.Now.AddDays(30), "Sở Y Tế Tỉnh", "Tư vấn", "Đã đóng", "G13-MSHH-2025.pdf"),
                ("G14-TXD-2025", "Gói thầu 46: Cải tạo Trường THPT Chuyên Lê Khiết", 100000000000, DateTime.Now.AddDays(15), "Sở GD&ĐT Tỉnh", "Xây lắp", "Đang mời thầu", "G14-TXD-2025.pdf")
            };

            int cardHeight = 170;
            int cardWidth = 950;

            foreach (var t in tenders)
            {
                Guna2Panel card = new Guna2Panel()
                {
                    Size = new Size(950, 180),
                    BorderRadius = 15,
                    FillColor = Color.White,
                    BorderColor = Color.LightGray,
                    BorderThickness = 1,
                    Margin = new Padding(0, 0, 0, 15),
                    ShadowDecoration = { Enabled = true, BorderRadius = 15, Color = Color.Gray, Shadow = new Padding(3) }
                };

                Label lblTen = new Label()
                {
                    Text = $"📌 [{t.Ma}] {t.Ten}",
                    Font = new Font("Segoe UI", 14, FontStyle.Bold),
                    Location = new Point(20, 20),
                    AutoSize = true
                };

                Label lblLoai = new Label()
                {
                    Text = $"📁 Loại: {t.Loai} | Chủ đầu tư: {t.ChuDauTu}",
                    Font = new Font("Segoe UI", 11),
                    Location = new Point(20, 55),
                    AutoSize = true
                };

                Label lblTien = new Label()
                {
                    Text = $"💰 Số tiền: {t.SoTien:N0} VND",
                    Font = new Font("Segoe UI", 11),
                    ForeColor = Color.DarkGreen,
                    Location = new Point(20, 85),
                    AutoSize = true
                };

                Label lblThoiHan = new Label()
                {
                    Text = $"📅 Hạn nộp: {t.ThoiHan:dd/MM/yyyy}",
                    Font = new Font("Segoe UI", 10, FontStyle.Italic),
                    ForeColor = Color.DimGray,
                    Location = new Point(20, 115),
                    AutoSize = true
                };

                Label lblTrangThai = new Label()
                {
                    Text = $"📌 Trạng thái: {t.TrangThai}",
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    ForeColor = GetTrangThaiColor(t.TrangThai),
                    Location = new Point(20, 140),
                    AutoSize = true
                };


                Guna2Button btnChiTiet = new Guna2Button()
                {
                    Text = "Xem chi tiết",
                    Size = new Size(130, 36),
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    Location = new Point(790, 120),
                    FillColor = Color.MediumSlateBlue,
                    ForeColor = Color.White,
                    BorderRadius = 10,
                    Tag = t.FilePDF
                };
                btnChiTiet.HoverState.FillColor = Color.MediumPurple;

                btnChiTiet.Click += async (s, e) =>
                {
                    loading.Visible = true;
                    await Task.Delay(500);
                    loading.Visible = false;

                    try
                    {
                        string fileName = (string)((Guna2Button)s).Tag;
                        string filePath = Path.Combine(Application.StartupPath, "Documents", fileName);

                        if (!File.Exists(filePath))
                        {
                            MessageBox.Show("Không tìm thấy file PDF!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        pdfViewer.Document?.Dispose();
                        pdfViewer.Document = PdfiumViewer.PdfDocument.Load(filePath);
                        pdfPanel.Visible = true;
                        pdfPanel.BringToFront();
                        currentPdfFileName = fileName;

                        // Gán Tag cho btnDownload (đã thêm ở trên) để tải đúng file
                        var downloadBtn = pdfPanel.Controls.OfType<Guna2Button>().FirstOrDefault(b => b.Text == "Tải xuống");
                        if (btnDownload != null)
                        {
                            btnDownload.Tag = fileName;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi mở PDF: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };

                card.Controls.Add(lblTen);
                card.Controls.Add(lblLoai);
                card.Controls.Add(lblTien);
                card.Controls.Add(lblThoiHan);
                card.Controls.Add(btnChiTiet);
                flow.Controls.Add(card);
            }
        }


        private void BtnDownload_Click(object sender, EventArgs e)
        {
            string baseFolder = Path.Combine(Application.StartupPath, "Documents");
            string pdfFileName = ((Guna2Button)sender).Tag as string;
            string pdfPath = Path.Combine(baseFolder, pdfFileName);

            if (File.Exists(pdfPath))
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.FileName = pdfFileName;
                    sfd.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            File.Copy(pdfPath, sfd.FileName, overwrite: true);
                            MessageBox.Show("Tải xuống thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi khi tải xuống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy file PDF để tải xuống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private Color GetTrangThaiColor(string status)
        {
            switch (status)
            {
                case "Đang thực hiện":
                    return Color.OrangeRed;
                case "Đang mời thầu":
                    return Color.DodgerBlue;
                case "Đang xét thầu":
                    return Color.MediumSlateBlue;
                case "Đã đóng":
                    return Color.Gray;
                default:
                    return Color.Black;
            }
        }


        private void DashboardControl_Load(object sender, EventArgs e)
        {
        }
    }
}
