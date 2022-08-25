using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DerinceBelediyesi.BLL;//sadece BLL ile bağlanır unutma Form için geçerli
using DerinceBelediyesi.Entity;

namespace DerinceBelediyesi.UI.WinnForm
{
    public partial class FrmStokIslemleri : Form
    {
        public FrmStokIslemleri()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void s1txtstokid_VisibleChanged(object sender, EventArgs e)
        {
            if (s1txtstokid.Text.Trim() == "")//eğer TextBox1 boş ise
            {
                errorProvider1.SetError(s1txtstokid, "Bu alan boş geçilmez");
            } // ErrorProvider açılacak ve
            //üstteki satırda belirtilen msj çıkacak
            else
            {
                errorProvider1.SetError(s1txtstokid, "");
            }// ErrorProvider kapanacak
        }

        private void s1txtstokid_TextChanged(object sender, EventArgs e)
        {
            if (s1txtstokid.Text.Trim() == "")//eğer TextBox1 boş ise
            {
                errorProvider1.SetError(s1txtstokid, "Bu alan boş geçilmez");
            } // ErrorProvider açılacak ve
            //üstteki satırda belirtilen msj çıkacak
            else
            {
                errorProvider1.SetError(s1txtstokid, "");
            }// ErrorProvider kapanacak
            if ((s1txtstokid.Text.Length < 5) || (s1txtstokid.Text.Length > 5))
            {
                errorProvider2.SetError(s1txtstokid, "Stok numarısı 5 haneli olmalıdır.");
            }
            else
            {
                errorProvider2.Clear();
            }
        }

        private void s1txtstokid_KeyPress(object sender, KeyPressEventArgs e)
        {
            //sadece sayi girişi
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void FrmStokIslemleri_Load(object sender, EventArgs e)
        {
            this.Location = new Point(400, 100);//Form ekranın açılınca yernini belirleme Point(x,y)
            errorProvider1.BlinkRate = 100000;
            errorProvider1.BlinkStyle = ErrorBlinkStyle.BlinkIfDifferentError;

            errorProvider2.BlinkRate = 400;
            errorProvider2.BlinkStyle = ErrorBlinkStyle.BlinkIfDifferentError;

            StokBLL bll = new StokBLL();
            dataGridView1.DataSource = bll.StokListesi();
            s4dgwAdetArttir.DataSource = bll.StokListesi();
            dataGridView3.DataSource = bll.StokListesi();
            s6dgwtoner.DataSource = bll.StokListesi();
            dgwStokguncelle.DataSource = bll.StokListesi();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            Stok stok = new Stok();
            stok.StokID = s1txtstokid.Text;
            stok.StokAdi = s1txtstokadi.Text;
            stok.Tarih = s1dtpTarih.Value;
            stok.Kategori = s1cmbKategori.Text;
            stok.Miktar = Convert.ToInt32(s1txtmiktar.Text);
            stok.Islem = s1cmbIslem.Text;

            StokBLL stokBLL = new StokBLL();
            try
            {//eger burada hata alırsan cathdeki blogta yazılan mesaj bana hata vermesini göstercek
                int sayi;
                sayi = stokBLL.StokEkle(stok);
                if(sayi==1)
                {
                    MessageBox.Show(stok.StokID + " Stok Numaralı ürün  Eklenmiştir.", "Stok ekleme işlemi", MessageBoxButtons.OK);
                    s1txtstokid.Clear();
                    s1txtstokadi.Clear();
                    s1dtpTarih.CustomFormat = " ";
                    s1cmbKategori.Text = "Seçiniz";
                    s1txtmiktar.Text = "0";
                    s1cmbIslem.Text = "Seçiniz";

                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void s2txtSilinecekStokNo_VisibleChanged(object sender, EventArgs e)
        {
            if (s2txtSilinecekStokNo.Text.Trim() == "")//eğer TextBox1 boş ise
            {
                errorProvider1.SetError(s2txtSilinecekStokNo, "Bu alan boş geçilmez");
            } // ErrorProvider açılacak ve
            //üstteki satırda belirtilen msj çıkacak
            else
            {
                errorProvider1.SetError(s2txtSilinecekStokNo, "");
            }// ErrorProvider kapanacak
        }

        private void s2txtSilinecekStokNo_TextChanged(object sender, EventArgs e)
        {
            if (s2txtSilinecekStokNo.Text.Trim() == "")//eğer TextBox1 boş ise
            {
                errorProvider1.SetError(s2txtSilinecekStokNo, "Bu alan boş geçilmez");
            } // ErrorProvider açılacak ve
            //üstteki satırda belirtilen msj çıkacak
            else
            {
                errorProvider1.SetError(s2txtSilinecekStokNo, "");
            }// ErrorProvider kapanacak
            if ((s2txtSilinecekStokNo.Text.Length < 5) || (s2txtSilinecekStokNo.Text.Length > 5))
            {
                errorProvider2.SetError(s2txtSilinecekStokNo, "Stok numarısı 5 haneli olmalıdır.");
            }
            else
            {
                errorProvider2.Clear();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //tıklanan personeli atxtbox'a ata
            s2txtSilinecekStokNo.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void s2btnStokSil_Click(object sender, EventArgs e)
        {
            Stok stok = new Stok();
            stok.StokID = s2txtSilinecekStokNo.Text;
            StokBLL stokBLL = new StokBLL();
            try
            {//eger burada hata alırsan cathdeki blogta yazılan mesaj bana hata vermesini göstercek
                //Silmek istiyormusunuz diye soru sorulduktan sonra evet seçeneğini seçer ise personel silinecek
                DialogResult dialogResult = MessageBox.Show(stok.StokID + " stok numaralı ürünü silmek istediğinize emin misiniz ?", "Silme İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    int sayi;
                    sayi = stokBLL.StokSil(stok);
                    if (sayi == 1)
                    {
                        MessageBox.Show(stok.StokID + " Stok Numaralı ürün Silinmiştir.", "Silme işlemi işlemi", MessageBoxButtons.OK);
                        StokBLL bll3 = new StokBLL();
                        dataGridView1.DataSource = bll3.StokListesi();
                    }
                    s2txtSilinecekStokNo.Clear();
                    StokBLL bll = new StokBLL();
                    dataGridView1.DataSource = bll.StokListesi();
                    s4dgwAdetArttir.DataSource = bll.StokListesi();
                    dataGridView3.DataSource = bll.StokListesi();
                    s6dgwtoner.DataSource = bll.StokListesi();
                    dgwStokguncelle.DataSource = bll.StokListesi();

                }
                else if (dialogResult == DialogResult.No)
                {
                    MessageBox.Show("Sil işlemi iptal edildi");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            StokBLL bll = new StokBLL();
            dataGridView1.DataSource = bll.StokListesi();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {// tab controlde alan değiştikçe tablolara ekleme yapıldıysa tekrar güncellenecek
            StokBLL bll = new StokBLL();
            dataGridView1.DataSource = bll.StokListesi();
            s4dgwAdetArttir.DataSource = bll.StokListesi();
            dataGridView3.DataSource = bll.StokListesi();
            s6dgwtoner.DataSource = bll.StokListesi();
            dgwStokguncelle.DataSource = bll.StokListesi();
        }

        private void s3txtstokno_VisibleChanged(object sender, EventArgs e)
        {
            if (s3txtstokno.Text.Trim() == "")//eğer TextBox1 boş ise
            {
                errorProvider1.SetError(s3txtstokno, "Bu alan boş geçilmez");
            } // ErrorProvider açılacak ve
            //üstteki satırda belirtilen msj çıkacak
            else
            {
                errorProvider1.SetError(s3txtstokno, "");
            }// ErrorProvider kapanacak
        }

        private void s3txtstokno_TextChanged(object sender, EventArgs e)
        {
            if (s3txtstokno.Text.Trim() == "")//eğer TextBox1 boş ise
            {
                errorProvider1.SetError(s3txtstokno, "Bu alan boş geçilmez");
            } // ErrorProvider açılacak ve
            //üstteki satırda belirtilen msj çıkacak
            else
            {
                errorProvider1.SetError(s3txtstokno, "");
            }// ErrorProvider kapanacak
            if ((s3txtstokno.Text.Length < 5) || (s3txtstokno.Text.Length > 5))
            {
                errorProvider2.SetError(s3txtstokno, "Stok numarısı 5 haneli olmalıdır.");
            }
            else
            {
                errorProvider2.Clear();
            }
        }

        private void s3txtstokno_KeyPress(object sender, KeyPressEventArgs e)
        {
            //sadece sayi girişi
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void s2txtSilinecekStokNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //sadece sayi girişi
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void s3btnguncelle_Click(object sender, EventArgs e)
        {
            Stok stok = new Stok();
            stok.StokID = s3txtstokno.Text;
            stok.StokAdi = s3TxtStokAdi.Text;
            stok.Tarih = s3DtpTarih.Value;
            stok.Kategori = s3cmbKategori.Text;
            stok.Miktar = Convert.ToInt32(s3txtMiktar.Text);
            stok.Islem = s3cmbIslem.Text;
            StokBLL stokBLL = new StokBLL();
            try
            {//eger burada hata alırsan cathdeki blogta yazılan mesaj bana hata vermesini göstercek
                //Güncellmek istiyormusunuz diye soru sorulduktan sonra evet seçeneğini seçer ise personel güncellenecek
                DialogResult dialogResult = MessageBox.Show(stok.StokID + " stok numaralı ürünü güncellemek istediğinize emin misiniz ?", "Silme İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    int sayi;
                    sayi = stokBLL.StokBilgileriniGuncelle(stok);
                    if (sayi == 1)
                    {
                        MessageBox.Show(stok.StokID + " Stok Numaralı ürün  güncellenmiştir.", "Stok güncelleme işlemi", MessageBoxButtons.OK);
                        //işlemler tamamlandıktan sonra textboxtt combobox date leri temizle
                        s3txtstokno.Clear();
                        s3TxtStokAdi.Clear();
                        s3DtpTarih.CustomFormat = " ";
                        s3cmbKategori.Text = "Seçiniz";
                        s3txtMiktar.Text = "0";
                        s3cmbIslem.Text = "Seçiniz";
                        StokBLL bll = new StokBLL();
                        dataGridView1.DataSource = bll.StokListesi();
                        s4dgwAdetArttir.DataSource = bll.StokListesi();
                        dataGridView3.DataSource = bll.StokListesi();
                        s6dgwtoner.DataSource = bll.StokListesi();
                        dgwStokguncelle.DataSource = bll.StokListesi();
                    }
                }
                else if (dialogResult == DialogResult.No)
                {
                    MessageBox.Show("Güncelleme işlemi iptal edildi");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void dgwStokguncelle_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //tıklanan personeli txtbox'a ata
            s3txtstokno.Text = dgwStokguncelle.Rows[e.RowIndex].Cells[0].Value.ToString();
            s3TxtStokAdi.Text = dgwStokguncelle.Rows[e.RowIndex].Cells[1].Value.ToString();
            s3DtpTarih.Text = dgwStokguncelle.Rows[e.RowIndex].Cells[2].Value.ToString();
            s3cmbKategori.Text = dgwStokguncelle.Rows[e.RowIndex].Cells[3].Value.ToString();
            s3txtMiktar.Text = dgwStokguncelle.Rows[e.RowIndex].Cells[4].Value.ToString();
            s3cmbIslem.Text = dgwStokguncelle.Rows[e.RowIndex].Cells[5].Value.ToString();
        }

        private void s4txtStokNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //sadece sayi girişi
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void s4txtStokNo_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void s4txtStokNo_TextChanged(object sender, EventArgs e)
        {
            if (s4txtStokNo.Text.Trim() == "")//eğer TextBox1 boş ise
            {
                errorProvider1.SetError(s4txtStokNo, "Bu alan boş geçilmez");
            } // ErrorProvider açılacak ve
            //üstteki satırda belirtilen msj çıkacak
            else
            {
                errorProvider1.SetError(s4txtStokNo, "");
            }// ErrorProvider kapanacak
            if ((s4txtStokNo.Text.Length < 5) || (s4txtStokNo.Text.Length > 5))
            {
                errorProvider2.SetError(s4txtStokNo, "Stok numarısı 5 haneli olmalıdır.");
            }
            else
            {
                errorProvider2.Clear();
            }
        }

        private void s4dgwAdetArttir_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //tıklanan personeli txtbox'a ata
            s4txtStokNo.Text = dgwStokguncelle.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void s4btnStokArttir_Click(object sender, EventArgs e)
        {
            Stok stok = new Stok();
            stok.StokID = s4txtStokNo.Text;
            stok.Miktar = Convert.ToInt32(s4txtStokAdeti.Text);
            StokBLL stokBLL = new StokBLL();
            try
            {//eger burada hata alırsan cathdeki blogta yazılan mesaj bana hata vermesini göstercek

                int sayi;
                sayi = stokBLL.StokMiktariAttir(stok);
                if (sayi == 1)
                {
                    MessageBox.Show(stok.StokID + " Stok Numaralı ürün  adeti eklenmiştir.", "Stok adeti arttırma işlemi", MessageBoxButtons.OK);

                    //işlemler tamamlandıktan sonra textboxtt combobox date leri temizle
                    s4txtStokNo.Clear();
                    s4txtStokAdeti.Text = "0";

                    //işlem yapıldıkta sonra tabloların güncel halini getir
                    StokBLL bll = new StokBLL();
                    dataGridView1.DataSource = bll.StokListesi();
                    s4dgwAdetArttir.DataSource = bll.StokListesi();
                    dataGridView3.DataSource = bll.StokListesi();
                    s6dgwtoner.DataSource = bll.StokListesi();
                    dgwStokguncelle.DataSource = bll.StokListesi();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        int urunazalt = 0;
        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //tıklanan personeli txtbox'a ata
            s5txtStokNo.Text = dgwStokguncelle.Rows[e.RowIndex].Cells[0].Value.ToString();
            urunazalt = Convert.ToInt32(dgwStokguncelle.Rows[e.RowIndex].Cells[4].Value.ToString());
        }

        private void s5txtAdetAzalt_KeyPress(object sender, KeyPressEventArgs e)
        {
            //sadece sayi girişi
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void s4txtStokAdeti_KeyPress(object sender, KeyPressEventArgs e)
        {
            //sadece sayi girişi
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void s3txtMiktar_KeyPress(object sender, KeyPressEventArgs e)
        {
            //sadece sayi girişi
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        
        private void s1txtmiktar_KeyPress(object sender, KeyPressEventArgs e)
        {
            //sadece sayi girişi
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void s5btnAdetAzalt_Click(object sender, EventArgs e)
        {
            Stok stok = new Stok();
            stok.StokID = s5txtStokNo.Text;
            stok.Miktar = Convert.ToInt32(s5txtAdetAzalt.Text);
            StokBLL stokBLL = new StokBLL();
            try
            {//eger burada hata alırsan cathdeki blogta yazılan mesaj bana hata vermesini göstercek

                int sayi;
                sayi = stokBLL.StokMiktariAzalt(stok);
                if (sayi == 1)
                {
                    MessageBox.Show(stok.StokID + " Stok Numaralı ürün  adeti azaltılmıştır.", "Stok adeti azaltma işlemi", MessageBoxButtons.OK);

                    //işlemler tamamlandıktan sonra textboxtt combobox date leri temizle
                    s5txtStokNo.Clear();
                    s5txtAdetAzalt.Text = "0";

                    //işlem yapıldıkta sonra tabloların güncel halini getir
                    StokBLL bll = new StokBLL();
                    dataGridView1.DataSource = bll.StokListesi();
                    s4dgwAdetArttir.DataSource = bll.StokListesi();
                    dataGridView3.DataSource = bll.StokListesi();
                    s6dgwtoner.DataSource = bll.StokListesi();
                    dgwStokguncelle.DataSource = bll.StokListesi();
                }
                else
                {
                    MessageBox.Show(stok.StokID + " Stok Numaralı üründen "+s5txtAdetAzalt.Text+" adet bulunmamaktadır. Elimizde "+urunazalt+" adet ürün bulunmaktadır.", "Stok adeti azaltma işlemi", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void s6txtStokNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //sadece sayi girişi
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
       
        private void s6btnTonerDolum_Click(object sender, EventArgs e)
        {
            Stok stok = new Stok();
            stok.StokID = s6txtStokNo.Text;
            stok.Islem = "Toner Doldurma";
            StokBLL stokBLL = new StokBLL();
            try
            {//eger burada hata alırsan cathdeki blogta yazılan mesaj bana hata vermesini göstercek

                int sayi;
                sayi = stokBLL.DolumMiktariArttir(stok);
                if (sayi == 1)
                {
                    MessageBox.Show(stok.StokID + " Stok Numaralı Toner dolduruma gönderilmiştir..", "Toner Dolum işlemi", MessageBoxButtons.OK);

                    //işlemler tamamlandıktan sonra textboxtt combobox date leri temizle
                    s6txtStokNo.Clear();

                    //işlem yapıldıkta sonra tabloların güncel halini getir
                    StokBLL bll = new StokBLL();
                    dataGridView1.DataSource = bll.StokListesi();
                    s4dgwAdetArttir.DataSource = bll.StokListesi();
                    dataGridView3.DataSource = bll.StokListesi();
                    s6dgwtoner.DataSource = bll.StokListesi();
                    dgwStokguncelle.DataSource = bll.StokListesi();
                }
                else
                {
                    MessageBox.Show(stok.StokID + " Stok Numaralı üründen " + s5txtAdetAzalt.Text + " adet bulunmamaktadır. Elimizde " + urunazalt + " adet ürün bulunmaktadır.", "Stok adeti azaltma işlemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void s6dgwtoner_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //tıklanan personeli txtbox'a ata
            s6txtStokNo.Text = dgwStokguncelle.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

    }
}
