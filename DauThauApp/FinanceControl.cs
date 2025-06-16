using LiveChartsCore;
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.WinForms;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DauThauApp
{
    public partial class FinanceControl : UserControl
    {
        private List<ThongTinNhanVien> nhanViens;

        public FinanceControl(List<ThongTinNhanVien> dsNhanVien)
        {
            InitializeComponent();
            nhanViens = dsNhanVien;
            InitUI();
        }

        private void InitUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.FromArgb(245, 245, 250); // Màu nền nhẹ nhàng

            double tongChiPhi = nhanViens.Sum(nv => (double)nv.MucLuong) / 1_000_000;

            var tongLuongTheoPhongBan = nhanViens
                .GroupBy(nv => nv.PhongBan)
                .Select(g => new
                {
                    PhongBan = g.Key,
                    TongLuong = g.Sum(nv => (double)nv.MucLuong) / 1_000_000
                })
                .OrderByDescending(x => x.TongLuong)
                .ToList();

            var phongBanLabels = tongLuongTheoPhongBan.Select(x => x.PhongBan).ToArray();
            var chiPhiValues = tongLuongTheoPhongBan.Select(x => x.TongLuong).ToArray();

            var dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                Font = new Font("Segoe UI", 10)
            };
            dgv.Columns.Add("PhongBan", "Ph\u00f2ng Ban");
            dgv.Columns.Add("TongLuong", "T\u1ed5ng L\u01b0\u01a1ng (tri\u1ec7u VND)");

            foreach (var item in tongLuongTheoPhongBan)
            {
                dgv.Rows.Add(item.PhongBan, item.TongLuong.ToString("N2"));
            }

            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 3,
                Padding = new System.Windows.Forms.Padding(15),
                BackColor = Color.White
            };
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 30));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 30));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40));

            var colChartThang = new CartesianChart
            {
                Series = new ISeries[]
                {
                    new ColumnSeries<double>
                    {
                        Name = "Chi ph\u00ed",
                        Values = new double[] {
                            tongChiPhi * 1.0,
                            tongChiPhi * 1.2,
                            tongChiPhi * 0.9,
                            tongChiPhi * 1.1
                        },
                        Stroke = null
                    }
                },
                XAxes = new Axis[] { new Axis { Labels = new[] { "T1", "T2", "T3", "T4" }, Name = "Th\u00e1ng" } },
                YAxes = new Axis[] { new Axis { Name = "Tri\u1ec7u VND" } },
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            mainLayout.Controls.Add(WrapChart("Chi ph\u00ed nh\u00e2n s\u1ef1 theo th\u00e1ng", colChartThang), 0, 0);

            var colChartPhongBan = new CartesianChart
            {
                Series = new ISeries[]
                {
                    new ColumnSeries<double>
                    {
                        Name = "Chi ph\u00ed ph\u00f2ng ban",
                        Values = chiPhiValues,
                        Stroke = null,
                        DataLabelsPosition = LiveChartsCore.Measure.DataLabelsPosition.Top,
                        DataLabelsPaint = new SolidColorPaint(SKColors.DarkBlue),
                        DataLabelsSize = 14
                    }
                },
                XAxes = new Axis[]
                {
                    new Axis
                    {
                        Name = "Ph\u00f2ng Ban",
                        Labels = phongBanLabels,
                        LabelsRotation = 20,
                        Padding = new LiveChartsCore.Drawing.Padding(5, 0, 5, 20)
                    }
                },
                YAxes = new Axis[]
                {
                    new Axis
                    {
                        Name = "Tri\u1ec7u VND",
                        Labeler = value => value.ToString("N2")
                    }
                },
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            mainLayout.Controls.Add(WrapChart("Chi ph\u00ed nh\u00e2n s\u1ef1 theo ph\u00f2ng ban", colChartPhongBan), 1, 0);

            var lineChart = new CartesianChart
            {
                Series = new ISeries[]
                {
                    new LineSeries<double>
                    {
                        Values = new double[] {
                            tongChiPhi * 2.5,
                            tongChiPhi * 3,
                            tongChiPhi * 2.8,
                            tongChiPhi * 3.2
                        },
                        Name = "Doanh thu",
                        Fill = null,
                        GeometrySize = 10
                    }
                },
                XAxes = new Axis[] { new Axis { Labels = new[] { "Q1", "Q2", "Q3", "Q4" }, Name = "Qu\u00fd" } },
                YAxes = new Axis[] { new Axis { Name = "Tri\u1ec7u VND" } },
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            mainLayout.Controls.Add(WrapChart("Doanh thu theo qu\u00fd", lineChart), 0, 1);

            var noteLabel = new Label
            {
                Text = $"T\u1ed5ng chi ph\u00ed nh\u00e2n s\u1ef1: {tongChiPhi:N2} tri\u1ec7u VND",
                Font = new Font("Segoe UI", 12, FontStyle.Italic),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            mainLayout.Controls.Add(noteLabel, 1, 1);

            var groupTable = new GroupBox
            {
                Text = "T\u1ed5ng L\u01b0\u01a1ng Theo Ph\u00f2ng Ban",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.White
            };
            groupTable.Controls.Add(dgv);
            mainLayout.Controls.Add(groupTable, 0, 2);
            mainLayout.SetColumnSpan(groupTable, 2);

            this.Controls.Add(mainLayout);
        }

        private Control WrapChart(string title, Control chart)
        {
            var group = new GroupBox
            {
                Text = title,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.White
            };
            group.Controls.Add(chart);
            return group;
        }

        private void FinanceControl_Load(object sender, EventArgs e) { }
    }
}
