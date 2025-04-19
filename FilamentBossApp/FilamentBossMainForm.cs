using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FilamentBossApp
{
    public partial class FilamentBossMainForm : Form
    {
        public FilamentBossMainForm()
        {
            InitializeComponent();
        }

        private void FilamentBossMainForm_Load(object sender, EventArgs e)
        {
            AdminLogin frm = new AdminLogin();
            frm.ShowDialog();
        }
    }
}
