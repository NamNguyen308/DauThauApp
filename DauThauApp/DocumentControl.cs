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
    public partial class DocumentsControl : UserControl
    {
        private Guna2ProgressIndicator loading;
        private Guna2MessageDialog messageDialog;
        private Panel detailPanel;
        private Label detailTitleLabel;
        private Label detailDescLabel;
        private Label detailDateLabel;
        private Guna2Button btnCloseDetail;
        private FlowLayoutPanel flowPanel;
        private PdfViewer pdfViewer;
        private string currentPdfPath = null;
        private Guna2Button btnDownload;
        private User currentUser;
        private List<(string TieuDe, string MoTa, DateTime NgayTao)> allDocuments;


        public DocumentsControl(User user)
        {
            InitializeComponent();
            this.currentUser = user;

            this.AutoScroll = true;
            LoadUI();
            DisplayDocuments(allDocuments);
            this.Load += DocumentsControl_Load;
            this.currentUser = user;
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

           

            // Flow panel
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

            // Panel chi tiết (ẩn ban đầu)
            detailPanel = new Panel()
            {
                Size = new Size(950, 650),
                Location = new Point(30, 30),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Visible = false,
                Padding = new Padding(20)
            };

            PictureBox iconDoc = new PictureBox()
            {
                //Image = Properties.Resources.file_icon, // Bạn cần thêm ảnh file_icon.png vào Resources
                Size = new Size(80, 80),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(20, 20)
            };
            detailPanel.Controls.Add(iconDoc);

            detailTitleLabel = new Label()
            {
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                Location = new Point(120, 20),
                AutoSize = true,
                MaximumSize = new Size(700, 0)
            };

            detailDescLabel = new Label()
            {
                Font = new Font("Segoe UI", 12F),
                Location = new Point(120, 55),
                AutoSize = true,
                MaximumSize = new Size(700, 0)
            };

            detailDateLabel = new Label()
            {
                Font = new Font("Segoe UI", 11F, FontStyle.Italic),
                Location = new Point(120, 85),
                AutoSize = true
            };

            pdfViewer = new PdfViewer()
            {
                Dock = DockStyle.Bottom,
                Height = 500, // Chiều cao bạn mong muốn, phần còn lại của panel dành cho Label + nút
                ZoomMode = PdfViewerZoomMode.FitWidth,
                Visible = false
            };
            detailPanel.Controls.Add(pdfViewer);


            btnCloseDetail = new Guna2Button()
            {
                Text = "Đóng",
                Size = new Size(100, 30),
                Location = new Point(800, 90),
                FillColor = Color.Gray,
                ForeColor = Color.White,
                BorderRadius = 6
            };

            btnCloseDetail.Click += (s, e) =>
            {
                detailPanel.Visible = false;
                pdfViewer.Document?.Dispose();
                pdfViewer.Visible = false;
                detailPanel.Visible = false;
            };
            btnDownload = new Guna2Button()
            {
                Text = "Tải xuống",
                Size = new Size(100, 30),
                Location = new Point(680, 90),
                FillColor = Color.ForestGreen,
                ForeColor = Color.White,
                BorderRadius = 6
            };
            btnDownload.Click += BtnDownload_Click; // Sử dụng 1 event handler dùng chung
            detailPanel.Controls.Add(btnDownload);



            detailPanel.Controls.Add(detailTitleLabel);
            detailPanel.Controls.Add(detailDescLabel);
            detailPanel.Controls.Add(detailDateLabel);
            detailPanel.Controls.Add(btnCloseDetail);

            this.Controls.Add(detailPanel);
            var heightNeeded = flowPanel.GetPreferredSize(flowPanel.Size).Height;
            this.Controls.SetChildIndex(detailPanel, this.Controls.Count - 1); // Cho lên trên cùng
            detailPanel.BringToFront(); // Đảm bảo nằm trên




            allDocuments = new List<(string TieuDe, string MoTa, DateTime NgayTao)>
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


            int cardWidth = 900;
            int cardHeight = 110;

            foreach (var doc in allDocuments)
            {
                var currentDoc = doc;
                Guna2Panel card = new Guna2Panel()
                {
                    Size = new Size(900, 120),
                    BorderRadius = 20,
                    FillColor = Color.White,
                    BorderColor = Color.Gainsboro,
                    BorderThickness = 1,
                    Margin = new Padding(10, 0, 10, 20),
                    Padding = new Padding(20),
                    ShadowDecoration = { Enabled = true, BorderRadius = 20, Color = Color.DarkGray, Shadow = new Padding(5) }
                };

                Label lblTieuDe = new Label()
                {
                    Text = doc.TieuDe,
                    Font = new Font("Segoe UI", 16, FontStyle.Bold),
                    AutoSize = true,
                    Location = new Point(15, 10)
                };

                Label lblMoTa = new Label()
                {
                    Text = $"📝 Mô tả: {doc.MoTa}",
                    Font = new Font("Segoe UI", 12),
                    ForeColor = Color.DarkSlateGray, // màu mô tả
                    AutoSize = true,
                    Location = new Point(15, 40)
                };

                Label lblNgay = new Label()
                {
                    Text = $"📅 Ngày tạo: {doc.NgayTao:dd/MM/yyyy}",
                    Font = new Font("Segoe UI", 11, FontStyle.Italic),
                    ForeColor = Color.DarkRed, // màu nổi bật
                    AutoSize = true,
                    Location = new Point(15, 65)
                };

                // Nút Sửa
               

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

                btnXemChiTiet.Tag = $"{doc.TieuDe}.pdf";


                 btnXemChiTiet.Click += async (s, e) =>
                    {
                        try
                        {
                            loading.Visible = true;
                            await Task.Delay(500); // Giả lập loading
                            loading.Visible = false;

                      
                            // Gán dữ liệu chi tiết
                            detailTitleLabel.Text = $"📄 {currentDoc.TieuDe}";
                            detailDescLabel.Text = $"📝 Mô tả: {currentDoc.MoTa}";
                            detailDateLabel.Text = $"📅 Ngày tạo: {currentDoc.NgayTao:dd/MM/yyyy}";

                            string baseFolder = Path.Combine(Application.StartupPath, "Documents");
                            string pdfFileName = ((Guna2Button)s).Tag as string;
                            string pdfPath = Path.Combine(baseFolder, pdfFileName);


                            if (File.Exists(pdfPath))
                            {
                                pdfViewer.Visible = true;
                                pdfViewer.Document?.Dispose();
                                pdfViewer.Document = PdfiumViewer.PdfDocument.Load(pdfPath);
                            }
                            else
                            {
                                pdfViewer.Visible = false;
                                MessageBox.Show("Không tìm thấy file PDF tương ứng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            btnDownload.Tag = pdfFileName;
                            // Hiện panel chi tiết
                            detailPanel.BackColor = Color.LightYellow;
                            detailPanel.Visible = true;
                            detailPanel.BringToFront();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    };


                   

                            card.Controls.Add(lblTieuDe);
                            card.Controls.Add(lblMoTa);
                            card.Controls.Add(lblNgay);
                            card.Controls.Add(btnXemChiTiet);
                            flowPanel.Controls.Add(card);
                
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


        private string ShowInputDialog(string title, string promptText, string defaultValue)
        {
            Form prompt = new Form()
            {
                Width = 400,
                Height = 180,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = title,
                StartPosition = FormStartPosition.CenterParent
            };

            Label textLabel = new Label() { Left = 20, Top = 20, Text = promptText, Width = 340 };
            TextBox inputBox = new TextBox() { Left = 20, Top = 50, Width = 340, Text = defaultValue };
            Button confirmation = new Button() { Text = "OK", Left = 270, Width = 90, Top = 90, DialogResult = DialogResult.OK };

            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(inputBox);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? inputBox.Text : null;
        }



        private void DisplayDocuments(List<(string TieuDe, string MoTa, DateTime NgayTao)> documents)
        {
            flowPanel.Controls.Clear();
            int cardWidth = 800;
            int cardHeight = 110;
            foreach (var doc in documents)
            {
                var currentDoc = doc;
                Guna2Panel card = new Guna2Panel()
                {
                    Size = new Size(900, 120),
                    BorderRadius = 20,
                    FillColor = Color.White,
                    BorderColor = Color.Gainsboro,
                    BorderThickness = 1,
                    Margin = new Padding(10, 0, 10, 20),
                    Padding = new Padding(20),
                    
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
                    ForeColor = Color.DarkSlateGray,
                    AutoSize = true,
                    Location = new Point(15, 40)
                };

                Label lblNgay = new Label()
                {
                    Text = $"📅 Ngày tạo: {doc.NgayTao:dd/MM/yyyy}",
                    Font = new Font("Segoe UI", 9, FontStyle.Italic),
                    ForeColor = Color.DarkRed,
                    AutoSize = true,
                    Location = new Point(15, 65)
                };

                // Nút Xem chi tiết
                Guna2Button btnXemChiTiet = new Guna2Button()
                {
                    Text = "Xem chi tiết",
                    Font = new Font("Segoe UI", 9F),
                    Size = new Size(110, 30),
                    Location = new Point(cardWidth - 130, cardHeight - 45),
                    BorderRadius = 6,
                    FillColor = Color.MediumSlateBlue,
                    ForeColor = Color.White,
                    Tag = $"{doc.TieuDe}.pdf"
                };
                btnXemChiTiet.HoverState.FillColor = Color.MediumPurple;

                btnXemChiTiet.Click += async (s, e) =>
                {
                    try
                    {
                        loading.Visible = true;
                        await Task.Delay(500);
                        loading.Visible = false;

                        detailTitleLabel.Text = $"📄 {currentDoc.TieuDe}";
                        detailDescLabel.Text = $"📝 Mô tả: {currentDoc.MoTa}";
                        detailDateLabel.Text = $"📅 Ngày tạo: {currentDoc.NgayTao:dd/MM/yyyy}";

                        string baseFolder = Path.Combine(Application.StartupPath, "Documents");
                        string pdfFileName = ((Guna2Button)s).Tag as string;
                        string pdfPath = Path.Combine(baseFolder, pdfFileName);

                        if (File.Exists(pdfPath))
                        {
                            pdfViewer.Visible = true;
                            pdfViewer.Document?.Dispose();
                            pdfViewer.Document = PdfiumViewer.PdfDocument.Load(pdfPath);
                        }
                        else
                        {
                            pdfViewer.Visible = false;
                            MessageBox.Show("Không tìm thấy file PDF tương ứng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        btnDownload.Tag = pdfFileName;
                        detailPanel.BackColor = Color.LightYellow;
                        detailPanel.Visible = true;
                        detailPanel.BringToFront();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };

                card.Controls.Add(lblTieuDe);
                card.Controls.Add(lblMoTa);
                card.Controls.Add(lblNgay);
                card.Controls.Add(btnXemChiTiet);


                

                if (currentUser.Role == "Chuyên viên")
                {
                    Guna2Button btnSua = new Guna2Button()
                    {
                        Text = "Sửa",
                        Font = new Font("Segoe UI", 9F),
                        Size = new Size(80, 30),
                        Location = new Point(cardWidth - 250, cardHeight - 45),
                        BorderRadius = 6,
                        FillColor = Color.DarkOrange,
                        ForeColor = Color.White,
                        Tag = currentDoc
                    };

                    Guna2Button btnXoa = new Guna2Button()
                    {
                        Text = "Xoá",
                        Font = new Font("Segoe UI", 9F),
                        Size = new Size(80, 30),
                        Location = new Point(cardWidth - 340, cardHeight - 45),
                        BorderRadius = 6,
                        FillColor = Color.Crimson,
                        ForeColor = Color.White,
                        Tag = currentDoc
                    };



                    btnSua.Click += (s, e) =>
                    {
                        var button = (Guna2Button)s;
                        var clickedDoc = ((string TieuDe, string MoTa, DateTime NgayTao))button.Tag;

                        string newTitle = ShowInputDialog("Sửa tiêu đề", "Nhập tiêu đề mới:", clickedDoc.TieuDe);

                        if (!string.IsNullOrWhiteSpace(newTitle))
                        {
                            allDocuments.Remove(clickedDoc); 
                            var updatedDoc = (newTitle, clickedDoc.MoTa, clickedDoc.NgayTao);
                            allDocuments.Add(updatedDoc);
                            DisplayDocuments(allDocuments);
                        }
                    };

                    

                    btnXoa.Click += (s, e) =>
                    {
                        var clickedDoc = ((string TieuDe, string MoTa, DateTime NgayTao))((Guna2Button)s).Tag;
                        var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xoá \"{clickedDoc.TieuDe}\"?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (confirm == DialogResult.Yes)
                        {
                            allDocuments.Remove(clickedDoc);
                            

                            string filePath = Path.Combine(Application.StartupPath, "Documents", $"{clickedDoc.TieuDe}.pdf");
                            if (File.Exists(filePath)) File.Delete(filePath);
                        }
                    };

                    card.Controls.Add(btnSua);
                    card.Controls.Add(btnXoa);
                }

                flowPanel.Controls.Add(card);
            }
        }


        private void FilterDocuments(string keyword)
        {
            var filtered = allDocuments.FindAll(doc => doc.TieuDe.ToLower().Contains(keyword.ToLower()));
            DisplayDocuments(filtered);
        }

        private void DocumentsControl_Load(object sender, EventArgs e)
        {
            detailPanel.Visible = false;
        }
    }
}
