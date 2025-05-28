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
    public partial class TenderForm : Form
    {
        private User currentUser;
        public TenderForm(User user)
        {
            InitializeComponent();
            this.Size = new Size(1280, 720);
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            currentUser = user;
        }

        private void TenderForm_Load(object sender, EventArgs e)
        {

        }
    }
}
