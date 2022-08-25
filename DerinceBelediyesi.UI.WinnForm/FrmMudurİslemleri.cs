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
using System.Data.OleDb;
using Tulpep.NotificationWindow;//form geçiş ekranı için

namespace DerinceBelediyesi.UI.WinnForm
{
    public partial class FrmMudurİslemleri : Form
    {
        public FrmMudurİslemleri()
        {
            InitializeComponent();
        }

        private void txtTcNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //sadece sayi girişi
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtTcNo_TextChanged(object sender, EventArgs e)
        {
            if (txtTcNo.Text.Trim() == "")//eğer TextBox1 boş ise
            {
                errorProvider1.SetError(txtTcNo, "Bu alan boş geçilmez");
            } // ErrorProvider açılacak ve
            //üstteki satırda belirtilen msj çıkacak
            else
            {
                errorProvider1.SetError(txtTcNo, "");
            }// ErrorProvider kapanacak
            if ((txtTcNo.Text.Length < 11) || (txtTcNo.Text.Length > 11))
            {
                errorProvider2.SetError(txtTcNo, "TC Kimlik numarısı 11 karakterli olmalıdır.");
            }
            else
            {
                errorProvider2.Clear();
            }
        }

        private void txtTcNo_VisibleChanged(object sender, EventArgs e)
        {
            if (txtTcNo.Text.Trim() == "")//eğer TextBox1 boş ise
            {
                errorProvider1.SetError(txtTcNo, "Bu alan boş geçilmez");
            } // ErrorProvider açılacak ve
            //üstteki satırda belirtilen msj çıkacak
            else
            {
                errorProvider1.SetError(txtTcNo, "");
            }// ErrorProvider kapanacak
        }

       

        private void FrmMudurİslemleri_Load(object sender, EventArgs e)
        {
            this.Location = new Point(400, 100);//Form ekranın açılınca yernini belirleme Point(x,y)
            errorProvider1.BlinkRate = 100000;
            errorProvider1.BlinkStyle = ErrorBlinkStyle.BlinkIfDifferentError;

            errorProvider2.BlinkRate = 400;
            errorProvider2.BlinkStyle = ErrorBlinkStyle.BlinkIfDifferentError;


            BilgiIslemKullaniciGirisBLL bll = new BilgiIslemKullaniciGirisBLL();
            dataGridView1.DataSource = bll.PersonelListele();
            dataGridView2.DataSource = bll.PersonelListele();
            dataGridView3.DataSource = bll.PersonelListele();
            dataGridView4.DataSource = bll.PersonelListele();
            dataGridView5.DataSource = bll.PersonelListele();

            /*
                        //Personellerin tc Kimlik numaralarını getiriyor
                        OleDbConnection baglanti = new OleDbConnection();
                        baglanti.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Staj.accdb";
                        OleDbCommand komut = new OleDbCommand();
                        komut.CommandText = "SELECT *FROM Personel";
                        komut.Connection = baglanti;
                        komut.CommandType = CommandType.Text;

                        OleDbDataReader dr;
                        baglanti.Open();
                        dr = komut.ExecuteReader();
                        while (dr.Read())
                        {
                            cmbtcno.Items.Add(dr["TC"]);
                        }
                        baglanti.Close();
            */


        }

        private void txtSifre_KeyPress(object sender, KeyPressEventArgs e)
        {
            //boşluk girişini engeller
            e.Handled = Char.IsWhiteSpace(e.KeyChar);
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            BilgiIslemKullaniciGiris bilgiIslemKullaniciGiris = new BilgiIslemKullaniciGiris();
            //önce textboxları değişkenlere atıyorum herhangi bir değişiklik yaptığımda kolay olsun 
            //tablodaki değişkenlerle aynı olacak dikkat et
            bilgiIslemKullaniciGiris.TC = txtTcNo.Text;
            bilgiIslemKullaniciGiris.Sifre = txtSifre.Text;
            bilgiIslemKullaniciGiris.Adi = txtAdi.Text;
            bilgiIslemKullaniciGiris.Soyadi = txtSoyadi.Text;
            bilgiIslemKullaniciGiris.Departman = "Bilgi İşlem";
            bilgiIslemKullaniciGiris.Yetki = cmbYetki.Text;
            bilgiIslemKullaniciGiris.Mesai = 0;//yeni kullanıcı olduğu için

            BilgiIslemKullaniciGirisBLL bll = new BilgiIslemKullaniciGirisBLL();
            try
            {
                string kullaniciadisoyadi = bll.PersonelEkle(bilgiIslemKullaniciGiris);
                if (kullaniciadisoyadi != "")
                {
                    MessageBox.Show("Kaydınız başarılya oluşturuldu.", "ÜYE KAYIT");
                    PopupNotifier popup = new PopupNotifier();
                    popup.Image = Properties.Resources.userana;
                    popup.ImagePadding = new Padding(5);
                    popup.TitleText = "yeni üye kaydı.".ToUpper().PadLeft(1);
                    popup.TitleColor = Color.Green;//başlık yazı rengi


                    popup.HeaderHeight = 025;
                    popup.HeaderColor = Color.Green;//başlık  rengi
                    popup.ContentHoverColor = Color.Yellow;//
                    popup.TitleFont = new Font("Segoe Script", 12, FontStyle.Bold);// Font("Yazı tipi",boyutu,şekli); 
                    popup.ContentFont = new Font("Cooper Black", 14, FontStyle.Regular);// Font("Yazı tipi",boyutu,şekli); 

                    popup.ContentText = kullaniciadisoyadi.PadLeft(1);
                    popup.BorderColor = Color.Yellow;//mesajın dış çepesi çercevesi
                    popup.BodyColor = Color.Black;//mesajın içi
                    popup.ContentColor = Color.Green;//text yazı rengi

                    popup.ContentHoverColor = Color.Black;
                    popup.TitlePadding = new Padding(3, 12, 5, 3);//sol , yukarı, aşağı sağ
                    popup.ContentPadding = new Padding(5, 0, 0, 0);
                    //  popup.BodyColor = Color.Red;
                    popup.Popup();
                    Form BilgiIslemMudurlugu = new FrmMudurİslemleri();
                    BilgiIslemMudurlugu.Show();
                    this.Hide();
                    BilgiIslemKullaniciGirisBLL bll2 = new BilgiIslemKullaniciGirisBLL();
                    dataGridView1.DataSource = bll2.PersonelListele();
                    dataGridView2.DataSource = bll2.PersonelListele();
                    dataGridView3.DataSource = bll2.PersonelListele();
                    dataGridView4.DataSource = bll2.PersonelListele();
                    dataGridView5.DataSource = bll2.PersonelListele();


                }
            }
            catch (OleDbException ex)
            {//aynı kullanıcı ekleniyorsa
                if (ex.ErrorCode == -2147467259)
                {
                    MessageBox.Show("Girmiş olduğunuz kullanıcı adı kullanılmaktadır.", "DİKKAT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void btncikis_Click(object sender, EventArgs e)
        {
            // Çıkış butonuna tıklanırsa uye ol formunu kapatıyorum kullanıcıgiriş formunu açıyorum.
            Form kullanicigirisekrani = new FrmMudurGiris();
            kullanicigirisekrani.Show();
            this.Hide();
        }

        private void btnmudurgiris_Click(object sender, EventArgs e)
        {
            Form kullanicigirisekrani = new FrmMudurGiris();
            kullanicigirisekrani.Show();
            this.Hide();
        }

        private void FrmMudurİslemleri_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form kullanicigirisekrani = new FrmMudurGiris();
            kullanicigirisekrani.Show();
            this.Hide();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
          
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {   //tıklanan personeli atxtbox'a ata
            txtsilinecekpersoneltc.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

        }

        private void btnpersonelsil_Click(object sender, EventArgs e)
        {
            BilgiIslemKullaniciGiris bilgiIslemKullaniciGirisiii = new BilgiIslemKullaniciGiris();
            bilgiIslemKullaniciGirisiii.TC = txtsilinecekpersoneltc.Text;
            BilgiIslemKullaniciGirisBLL bll = new BilgiIslemKullaniciGirisBLL();
            try
            {//eger burada hata alırsan cathdeki blogta yazılan mesaj bana hata vermesini göstercek
                //Silmek istiyormusunuz diye soru sorulduktan sonra evet seçeneğini seçer ise personel silinecek
                DialogResult dialogResult = MessageBox.Show(bilgiIslemKullaniciGirisiii.TC+" kimlik numaralı personeli silmek istediğinize emin misiniz ?", "Silme İşlemi", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    int sayi;
                    sayi = bll.PersonelSil(bilgiIslemKullaniciGirisiii);
                    if (sayi == 1)
                    {
                        MessageBox.Show(bilgiIslemKullaniciGirisiii.TC + " TC Kimlik Numaralı Personel Silinmiştir.", "Silme işlemi işlemi", MessageBoxButtons.OK);
                        BilgiIslemKullaniciGirisBLL bll3 = new BilgiIslemKullaniciGirisBLL();
                        dataGridView1.DataSource = bll3.PersonelListele();
                    }
                    txtsilinecekpersoneltc.Clear();
                    BilgiIslemKullaniciGirisBLL bll2 = new BilgiIslemKullaniciGirisBLL();
                    dataGridView1.DataSource = bll2.PersonelListele();
                    dataGridView2.DataSource = bll2.PersonelListele();
                    dataGridView3.DataSource = bll2.PersonelListele();
                    dataGridView4.DataSource = bll2.PersonelListele();
                    dataGridView5.DataSource = bll2.PersonelListele();

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

        private void btnmesaiekle_Click(object sender, EventArgs e)
        {
            BilgiIslemKullaniciGiris bilgiIslemKullaniciGirisiii = new BilgiIslemKullaniciGiris();
            bilgiIslemKullaniciGirisiii.TC = txtmesaieklenenpersonetc.Text;
            bilgiIslemKullaniciGirisiii.Mesai = Convert.ToInt32(txtmesaiekle.Text);
            BilgiIslemKullaniciGirisBLL bll = new BilgiIslemKullaniciGirisBLL();
            try
            {//eger burada hata alırsan cathdeki blogta yazılan mesaj bana hata vermesini göstercek

                int sayi;
                sayi = bll.PersonelMesaiEkle(bilgiIslemKullaniciGirisiii);
                if (sayi == 1)
                {
                    MessageBox.Show(bilgiIslemKullaniciGirisiii.TC + " TC Kimlik Numaralı Personelin Mesaisi Eklenmiştir.", "Mesai ekleme işlemi", MessageBoxButtons.OK);
                    BilgiIslemKullaniciGirisBLL bll2 = new BilgiIslemKullaniciGirisBLL();
                    dataGridView2.DataSource = bll2.PersonelListele();
                }
                txtmesaieklenenpersonetc.Clear();
                txtmesaiekle.Clear();

                BilgiIslemKullaniciGirisBLL bll3 = new BilgiIslemKullaniciGirisBLL();
                dataGridView1.DataSource = bll3.PersonelListele();
                dataGridView2.DataSource = bll3.PersonelListele();
                dataGridView3.DataSource = bll3.PersonelListele();
                dataGridView4.DataSource = bll3.PersonelListele();
                dataGridView5.DataSource = bll3.PersonelListele();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //tıklanan personeli atxtbox'a ata
            txtmesaieklenenpersonetc.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //tıklanan personeli atxtbox'a ata
            txtmesaisilTCNO.Text = dataGridView3.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void btnMesaiSil_Click(object sender, EventArgs e)
        {
            BilgiIslemKullaniciGiris bilgiIslemKullaniciGirisiii = new BilgiIslemKullaniciGiris();
            bilgiIslemKullaniciGirisiii.TC = txtmesaisilTCNO.Text;
            bilgiIslemKullaniciGirisiii.Mesai = Convert.ToInt32(txtMesaiSil.Text);
            BilgiIslemKullaniciGirisBLL bll = new BilgiIslemKullaniciGirisBLL();
            try
            {//eger burada hata alırsan cathdeki blogta yazılan mesaj bana hata vermesini göstercek

                int sayi;
                sayi = bll.PersonelMesaiSil(bilgiIslemKullaniciGirisiii);
                if (sayi == 1)
                {
                    MessageBox.Show(bilgiIslemKullaniciGirisiii.TC + " TC Kimlik Numaralı Personelin Mesaisi Silinmiştir.", "Mesai silme işlemi", MessageBoxButtons.OK);
                    BilgiIslemKullaniciGirisBLL bll2 = new BilgiIslemKullaniciGirisBLL();
                    dataGridView3.DataSource = bll2.PersonelListele();
                }
                txtmesaisilTCNO.Clear();
                txtMesaiSil.Clear();
                BilgiIslemKullaniciGirisBLL bll3 = new BilgiIslemKullaniciGirisBLL();
                dataGridView1.DataSource = bll3.PersonelListele();
                dataGridView2.DataSource = bll3.PersonelListele();
                dataGridView3.DataSource = bll3.PersonelListele();
                dataGridView4.DataSource = bll3.PersonelListele();
                dataGridView5.DataSource = bll3.PersonelListele();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //tıklanan personeli atxtbox'a ata
            txtmesaisifirlaTCNO.Text = dataGridView4.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void btnmesaisifirla_Click(object sender, EventArgs e)
        {

            BilgiIslemKullaniciGiris bilgiIslemKullaniciGirisiii = new BilgiIslemKullaniciGiris();
            bilgiIslemKullaniciGirisiii.TC = txtmesaisifirlaTCNO.Text;
            BilgiIslemKullaniciGirisBLL bll = new BilgiIslemKullaniciGirisBLL();
            try
            {//eger burada hata alırsan cathdeki blogta yazılan mesaj bana hata vermesini göstercek

                int sayi;
                sayi = bll.PersonelMesaiSifirla(bilgiIslemKullaniciGirisiii);
                if (sayi == 1)
                {
                    MessageBox.Show(bilgiIslemKullaniciGirisiii.TC + " TC Kimlik Numaralı Personelin Mesaisi Sıfırlanmıştır.", "Mesai sıfırlama işlemi", MessageBoxButtons.OK);
                    BilgiIslemKullaniciGirisBLL bll3 = new BilgiIslemKullaniciGirisBLL();
                    dataGridView1.DataSource = bll3.PersonelListele();
                    dataGridView2.DataSource = bll3.PersonelListele();
                    dataGridView3.DataSource = bll3.PersonelListele();
                    dataGridView4.DataSource = bll3.PersonelListele();
                    dataGridView5.DataSource = bll3.PersonelListele();
                }
                txtmesaisifirlaTCNO.Clear();
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {


            BilgiIslemKullaniciGiris bilgiIslemKullaniciGirisiii = new BilgiIslemKullaniciGiris();
            bilgiIslemKullaniciGirisiii.TC = txtguncelletcno.Text;
            bilgiIslemKullaniciGirisiii.Sifre = txtguncellesifre.Text;
            bilgiIslemKullaniciGirisiii.Adi = txtguncelleadi.Text;
            bilgiIslemKullaniciGirisiii.Soyadi = txtguncellesoyadi.Text;
            bilgiIslemKullaniciGirisiii.Departman = "Bilgi İşlem";
            bilgiIslemKullaniciGirisiii.Yetki = cmbguncellemeyetki.Text;
            bilgiIslemKullaniciGirisiii.Mesai = Convert.ToInt32(txtguncellemesai.Text);
            BilgiIslemKullaniciGirisBLL bll = new BilgiIslemKullaniciGirisBLL();
            try
            {//eger burada hata alırsan cathdeki blogta yazılan mesaj bana hata vermesini göstercek

                int sayi;
                sayi = bll.PersonelBilgilerigüncelle(bilgiIslemKullaniciGirisiii);
                if (sayi == 1)
                {
                    MessageBox.Show(bilgiIslemKullaniciGirisiii.TC + " TC Kimlik Numaralı Personelin Bilgileri güncellenmiştir.", "Personel bilgileri güncelleme işlemi", MessageBoxButtons.OK);
                    BilgiIslemKullaniciGirisBLL bll3 = new BilgiIslemKullaniciGirisBLL();
                    dataGridView1.DataSource = bll3.PersonelListele();
                    dataGridView2.DataSource = bll3.PersonelListele();
                    dataGridView3.DataSource = bll3.PersonelListele();
                    dataGridView4.DataSource = bll3.PersonelListele();
                    dataGridView5.DataSource = bll3.PersonelListele();
                }
                txtguncelleadi.Clear();
                txtguncellemesai.Clear();
                txtguncellesifre.Clear();
                txtguncellesoyadi.Clear();
                txtguncelletcno.Clear();
                cmbguncellemeyetki.Items[0].ToString();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //tıklanan personeli txtbox'a ata
            txtguncelletcno.Text = dataGridView5.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtguncellesifre.Text = dataGridView5.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtguncelleadi.Text = dataGridView5.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtguncellesoyadi.Text = dataGridView5.Rows[e.RowIndex].Cells[3].Value.ToString();
            cmbguncellemeyetki.Text = dataGridView5.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtguncellemesai.Text = dataGridView5.Rows[e.RowIndex].Cells[6].Value.ToString();
            

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {// tab controlde alan değiştikçe tablolara ekleme yapıldıysa tekrar güncellenecek
            BilgiIslemKullaniciGirisBLL bll2 = new BilgiIslemKullaniciGirisBLL();
            dataGridView1.DataSource = bll2.PersonelListele();
            dataGridView2.DataSource = bll2.PersonelListele();
            dataGridView3.DataSource = bll2.PersonelListele();
            dataGridView4.DataSource = bll2.PersonelListele();
            dataGridView5.DataSource = bll2.PersonelListele();
        }
    }
}
