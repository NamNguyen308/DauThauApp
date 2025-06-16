using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace DauThauApp
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            int width = 1280;
            int height = (int)(width * 9.0 / 16);
            this.Size = new Size(width, height);
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

   
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private User Authenticate(string username, string password)
        {
            var users = new List<(string Username, string PasswordHash, string Role)>
            {
                ("chuyenvien", "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3", "Chuyên viên"), 
                ("giamdoc", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Giám đốc"),     
                ("phaply", "15e2b0d3c33891ebb0f1ef609ec419420c20e320ce94c65fbc8c3312448eb225", "Pháp lý")        
            };

            string hashedInput = HashPassword(password);

            var matched = users.FirstOrDefault(u => u.Username == username && u.PasswordHash == hashedInput);
            if (matched != default)
            {
                return new User
                {
                    Username = matched.Username,
                    Role = matched.Role
                };
            }

            return null;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            var user = Authenticate(username, password);

            if (user != null)
            {
                this.Hide(); // Ẩn LoginForm

                Form nextForm;

      
                switch (user.Role)
                {
                    case "Chuyên viên":
                        nextForm = new AdminForm(user);
                        break;
                    case "Giám đốc":
                        nextForm = new TenderForm(user);
                        break;
                    case "Pháp lý":
                        nextForm = new ContractorForm(user);
                        break;
                    default:
                        MessageBox.Show("Không có quyền truy cập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }

                nextForm.ShowDialog(); 
                this.Hide();          // Đóng login sau khi xong
            }
            else
            {
                lblMessage.Text = "Sai tài khoản hoặc mật khẩu!";
                lblMessage.Visible = true;
            }
        }

        private void LoginForm_Load(object sender, EventArgs e) { }
        private void txtUsername_TextChanged(object sender, EventArgs e) { }
        private void txtUsername_TextChanged_1(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
    }
}
