using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace DauThauApp
{
    public partial class BackupRestoreControl : UserControl
    {
        // Kết nối đến database DauThauDB dùng để backup
        private string connectionString = @"Server=NAMNGUYEN;Database=DauThauDB;Trusted_Connection=True;";
        // Kết nối đến database master dùng để restore
        private string masterConnectionString = @"Server=NAMNGUYEN;Database=master;Trusted_Connection=True;";

        public BackupRestoreControl()
        {
            InitializeComponent();
            InitUI();
        }

        private void InitUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = System.Drawing.Color.White;

            Font buttonFont = new Font("Segoe UI", 12, FontStyle.Bold);

            var btnBackup = new Button()
            {
                Text = "📦 Sao lưu dữ liệu",
                Width = 280,
                Height = 60,
                Top = 70,
                Left = 100,
                Font = buttonFont,
                BackColor = System.Drawing.Color.FromArgb(0, 123, 255),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            var btnRestore = new Button()
            {
                Text = "♻️ Khôi phục dữ liệu",
                Width = 280,
                Height = 60,
                Top = 160,
                Left = 100,
                Font = buttonFont,
                BackColor = System.Drawing.Color.FromArgb(40, 167, 69),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            // Làm bo góc đẹp hơn
            btnBackup.FlatAppearance.BorderSize = 0;
            btnRestore.FlatAppearance.BorderSize = 0;

            btnBackup.MouseEnter += (s, e) => btnBackup.BackColor = System.Drawing.Color.FromArgb(0, 105, 217);
            btnBackup.MouseLeave += (s, e) => btnBackup.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);

            btnRestore.MouseEnter += (s, e) => btnRestore.BackColor = System.Drawing.Color.FromArgb(30, 150, 60);
            btnRestore.MouseLeave += (s, e) => btnRestore.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);

            btnBackup.Click += BtnBackup_Click;
            btnRestore.Click += BtnRestore_Click;

            this.Controls.Add(btnBackup);
            this.Controls.Add(btnRestore);
        }


        private void BtnBackup_Click(object sender, EventArgs e)
        {
            var saveDialog = new SaveFileDialog
            {
                Filter = "Backup files (*.bak)|*.bak",
                InitialDirectory = @"E:\BACKUP",
                FileName = $"DauThauDB_Backup_{DateTime.Now:yyyyMMdd_HHmmss}.bak"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = $"BACKUP DATABASE DauThauDB TO DISK = '{saveDialog.FileName}' WITH INIT";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Sao lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi sao lưu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnRestore_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "Backup files (*.bak)|*.bak";
                openDialog.Title = "Chọn tệp sao lưu để khôi phục";

                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    string backupFile = openDialog.FileName;

                    try
                    {
                        using (SqlConnection conn = new SqlConnection(masterConnectionString))  // Kết nối DB master
                        {
                            conn.Open();

                            // Đặt database ở SINGLE_USER mode để restore (đóng hết kết nối khác)
                            using (SqlCommand cmdSingleUser = new SqlCommand(
                            "ALTER DATABASE DauThauDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE", conn))
                            {
                                cmdSingleUser.ExecuteNonQuery();
                            }

                            using (SqlCommand cmdRestore = new SqlCommand(
                                $"RESTORE DATABASE DauThauDB FROM DISK = '{backupFile}' WITH REPLACE", conn))
                            {
                                cmdRestore.ExecuteNonQuery();
                            }

                            using (SqlCommand cmdMultiUser = new SqlCommand(
                                "ALTER DATABASE DauThauDB SET MULTI_USER", conn))
                            {
                                cmdMultiUser.ExecuteNonQuery();
                            }

                        }

                        MessageBox.Show("Khôi phục thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi khôi phục: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BackupRestoreControl_Load(object sender, EventArgs e)
        {

        }
    }
}
