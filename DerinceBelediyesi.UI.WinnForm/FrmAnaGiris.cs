using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DerinceBelediyesi.UI.WinnForm
{
    public partial class FrmAnaGiris : Form
    {
        public FrmAnaGiris()
        {
            InitializeComponent();
        }

        private void btnpersonelislemleri_Click(object sender, EventArgs e)
        {//müdür giriş sayfası açılacak
            Form kullanicigirisekrani = new FrmMudurGiris();
            kullanicigirisekrani.Show();
            this.Hide();
        }

        private void FrmAnaGiris_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();//programı kapat
        }

        private void btnstok_Click(object sender, EventArgs e)
        {
            Form kullanicigirisekrani = new FrmPersonelGiris();
            kullanicigirisekrani.Show();
            this.Hide();
        }
    }
}
