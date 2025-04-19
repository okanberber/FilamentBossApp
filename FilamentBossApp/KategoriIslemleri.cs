using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccessLayer;

namespace FilamentBossApp
{
    public partial class KategoriIslemleri : Form
    {
        
        public KategoriIslemleri()
        {
            InitializeComponent();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {

            DataModel dm = new DataModel();
            if (dm.KategoriEkle(tb_categoryName.Text, tb_description.Text, cb_isActive.Checked))
            {
                MessageBox.Show("Kategori Başarıyla Eklendi","Başarılı",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void KategoriIslemleri_Load(object sender, EventArgs e)
        {
            DataModel dm = new DataModel();
            dm.KategoriListele(dataGridView1);
        }
    }
}
