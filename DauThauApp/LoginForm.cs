using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DauThauApp
{
    public partial class LoginForm : Form
    {
        private User Authenticate(string username, string password)
        {
            var users = new List<(string Username, string Password, string Role)>
    {
        ("admin", "123", "Admin"),
        ("investor1", "456", "Chủ đầu tư"),
        ("contractor1", "789", "Nhà thầu"),
    };

            var matched = users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (!string.IsNullOrEmpty(matched.Username))
            {
                return new User { Username = matched.Username, Role = matched.Role };
            }

            return null;
        }
        public LoginForm()
        {
            InitializeComponent();
            int width = 1280;
            int height = (int)(width * 9.0 / 16); // 720 nếu width là 1280

            this.Size = new Size(width, height);            // Kích thước khởi đầu
            this.MinimumSize = this.Size;                   // Không cho resize nhỏ hơn
            this.MaximumSize = this.Size;                   // Không cho resize lớn hơn
            this.StartPosition = FormStartPosition.CenterScreen;  // Hiển thị ở giữa màn hình
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            var user = Authenticate(username, password);

            if (user != null)
            {
                this.Hide(); // Ẩn login form

                Form nextForm;

                if (user.Role == "Admin")
                    nextForm = new AdminForm(user);
                else if (user.Role == "Chủ đầu tư")
                    nextForm = new InvestorForm(user);
                else
                    nextForm = new ContractorForm(user);

                nextForm.ShowDialog();
                this.Close(); // Đóng sau khi user xong việc
            }
            else
            {
                lblMessage.Text = "Sai tài khoản hoặc mật khẩu!";
                lblMessage.Visible = true;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
