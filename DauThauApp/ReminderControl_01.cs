using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace DauThauApp
{
    public partial class ReminderControl_01 : UserControl
    {
        private List<Reminder> reminders;
        private string dataFile = "reminders.json";

        public ReminderControl_01()
        {
            InitializeComponent();
            LoadData();
            LoadUI();
        }

        public class Reminder
        {
            public string NoiDung { get; set; }
            public string PhongBan { get; set; }
            public string TrangThai { get; set; }
        }

        private void LoadData()
        {
            if (File.Exists(dataFile))
            {
                string json = File.ReadAllText(dataFile);
                reminders = JsonSerializer.Deserialize<List<Reminder>>(json);
            }
            else
            {
                reminders = new List<Reminder>
                {
                    new Reminder { NoiDung = "Kiểm tra hồ sơ dự thầu", PhongBan = "Phòng đấu thầu", TrangThai = "Đang lập" },
                    new Reminder { NoiDung = "Phê duyệt yêu cầu mua hồ sơ mời thầu", PhongBan = "Phòng đấu thầu", TrangThai = "Đã duyệt" },
                    new Reminder { NoiDung = "Xác minh thông tin nhà thầu", PhongBan = "Phòng pháp lý", TrangThai = "Chờ kiểm tra" },
                    new Reminder { NoiDung = "Cập nhật nhân sự dự án", PhongBan = "Phòng nhân sự", TrangThai = "Chờ duyệt" },
                    new Reminder { NoiDung = "Phê duyệt dự toán tài chính", PhongBan = "Phòng kế toán", TrangThai = "Đã gửi" },
                    new Reminder { NoiDung = "Bổ sung tài liệu dự thầu", PhongBan = "Phòng đấu thầu", TrangThai = "Cần chỉnh sửa" },
                    new Reminder { NoiDung = "Rà soát hợp đồng", PhongBan = "Phòng pháp lý", TrangThai = "Đã duyệt" },
                    new Reminder { NoiDung = "Duyệt chi phí tư vấn", PhongBan = "Phòng kế toán", TrangThai = "Chờ duyệt" },
                    new Reminder { NoiDung = "Kiểm tra bản vẽ kỹ thuật", PhongBan = "Phòng kỹ thuật", TrangThai = "Chờ kiểm tra" },
                    new Reminder { NoiDung = "Phản hồi đơn khiếu nại", PhongBan = "Phòng pháp lý", TrangThai = "Đang lập" }
                };
                SaveData();
            }
        }

        private void SaveData()
        {
            string json = JsonSerializer.Serialize(reminders, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(dataFile, json);
        }

        private void LoadUI()
        {
            Controls.Clear();

            Label title = new Label()
            {
                Text = "Yêu cầu cần xử lý",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(30, 30)
            };
            Controls.Add(title);

            FlowLayoutPanel flowPanel = new FlowLayoutPanel()
            {
                Location = new Point(30, 80),
                Size = new Size(1100, 620),
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = Color.Transparent
            };
            Controls.Add(flowPanel);

            int cardWidth = 750;
            int cardHeight = 100;

            for (int i = 0; i < reminders.Count; i++)
            {
                var item = reminders[i];

                var card = new Guna2Panel()
                {
                    Size = new Size(cardWidth, cardHeight),
                    BorderRadius = 15,
                    FillColor = Color.White,
                    BorderColor = Color.LightGray,
                    BorderThickness = 1,
                    Margin = new Padding(0, 0, 0, 15),
                    ShadowDecoration = { Enabled = true, BorderRadius = 15, Color = Color.Gray, Shadow = new Padding(3) }
                
                };

                Label lblNoiDung = new Label()
                {
                    Text = item.NoiDung,
                    Font = new Font("Segoe UI Semibold", 14F),
                    AutoSize = true,
                    Location = new Point(10, 10)
                };

                Label lblPhongBan = new Label()
                {
                    Text = $"📌 {item.PhongBan}",
                    Font = new Font("Segoe UI", 11.5F, FontStyle.Italic),
                    AutoSize = true,
                    Location = new Point(10, 40),
                    ForeColor = Color.DimGray
                };

                Label lblTrangThai = new Label()
                {
                    Text = $"Trạng thái: {item.TrangThai}",
                    Font = new Font("Segoe UI", 11.5F),
                    AutoSize = true,
                    Location = new Point(10, 65),
                    ForeColor = GetColorByTrangThai(item.TrangThai)
                };

                Guna2ComboBox comboTrangThai = new Guna2ComboBox()
                {
                    Location = new Point(card.Width - 180, 20),
                    Width = 150,
                    Anchor = AnchorStyles.Top | AnchorStyles.Right,
                    BorderRadius = 8,
                    FillColor = Color.MediumSlateBlue,
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 11F),
                    ItemHeight = 30,
                    DropDownStyle = ComboBoxStyle.DropDownList,
                };
                Guna2Button btnNhanXet = new Guna2Button()
                {
                    Text = "Nhận xét",
                    Location = new Point(card.Width - 180, 60),
                    Size = new Size(150, 30),
                    BorderRadius = 10,
                    FillColor = Color.FromArgb(255, 179, 71),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    Anchor = AnchorStyles.Top | AnchorStyles.Right
                };

                btnNhanXet.Click += (s, e) =>
                {
                    using (Form nhanXetForm = new Form())
                    {
                        nhanXetForm.Text = "Nhập nhận xét";
                        nhanXetForm.Size = new Size(400, 300);
                        nhanXetForm.StartPosition = FormStartPosition.CenterParent;
                        nhanXetForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                        nhanXetForm.MaximizeBox = false;
                        nhanXetForm.MinimizeBox = false;

                        Label lbl = new Label() { Text = "Nhận xét:", Location = new Point(20, 20), AutoSize = true };
                        TextBox txtNhanXet = new TextBox()
                        {
                            Multiline = true,
                            Size = new Size(340, 150),
                            Location = new Point(20, 50),
                            ScrollBars = ScrollBars.Vertical
                        };

                        Guna2Button btnGui = new Guna2Button()
                        {
                            Text = "Gửi",
                            Location = new Point(270, 210),
                            Size = new Size(90, 30),
                            BorderRadius = 8,
                            FillColor = Color.SeaGreen,
                            ForeColor = Color.White
                        };

                        btnGui.Click += (s2, e2) =>
                        {
                            string noiDungNhanXet = txtNhanXet.Text.Trim();
                            if (!string.IsNullOrEmpty(noiDungNhanXet))
                            {
                                MessageBox.Show("Đã gửi nhận xét", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                nhanXetForm.Close();
                            }
                            else
                            {
                                MessageBox.Show("Vui lòng nhập nội dung nhận xét!", "Thiếu nội dung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        };

                        nhanXetForm.Controls.Add(lbl);
                        nhanXetForm.Controls.Add(txtNhanXet);
                        nhanXetForm.Controls.Add(btnGui);
                        nhanXetForm.ShowDialog();
                    }
                };

                card.Controls.Add(btnNhanXet);



                comboTrangThai.Items.AddRange(new object[]
                {
                    "Đang lập",
                    "Chờ kiểm tra",
                    "Cần chỉnh sửa",
                    "Chờ duyệt",
                    "Đã duyệt",
                    "Đã gửi"
                });

                int selectedIndex = comboTrangThai.FindStringExact(item.TrangThai);
                comboTrangThai.SelectedIndex = selectedIndex != -1 ? selectedIndex : 0;

                int reminderIndex = i; // capture for closure
                comboTrangThai.SelectedIndexChanged += (s, e) =>
                {
                    string selected = comboTrangThai.SelectedItem.ToString();
                    reminders[reminderIndex].TrangThai = selected;
                    lblTrangThai.Text = $"Trạng thái: {selected}";
                    lblTrangThai.ForeColor = GetColorByTrangThai(selected);
                    SaveData();
                };

                card.Controls.Add(lblNoiDung);
                card.Controls.Add(lblPhongBan);
                card.Controls.Add(lblTrangThai);
                card.Controls.Add(comboTrangThai);
                flowPanel.Controls.Add(card);
            }
        }

        private Color GetColorByTrangThai(string trangThai)
        {
            if (trangThai == "Đã duyệt") return Color.SeaGreen;
            if (trangThai == "Đã gửi") return Color.SteelBlue;
            if (trangThai == "Chờ duyệt") return Color.Orange;
            if (trangThai == "Đang lập" || trangThai == "Chờ kiểm tra" || trangThai == "Cần chỉnh sửa") return Color.IndianRed;
            return Color.Black;
        }

        private void ReminderControl_Load(object sender, EventArgs e)
        {

        }

        private void ReminderControl_01_Load(object sender, EventArgs e)
        {

        }
    }
}
