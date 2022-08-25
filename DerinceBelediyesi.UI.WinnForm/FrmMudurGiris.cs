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
using Tulpep.NotificationWindow;//form geçiş ekranı için

namespace DerinceBelediyesi.UI.WinnForm
{
    public partial class FrmMudurGiris : Form
    {
        public FrmMudurGiris()
        {
            InitializeComponent();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {

            BilgiIslemKullaniciGiris bilgiIslemKullaniciGiris = new BilgiIslemKullaniciGiris();
            //önce textboxları değişkenlere atıyorum herhangi bir değişiklik yaptığımda kolay olsun 
            //tablodaki değişkenlerle aynı olacak dikkat
            bilgiIslemKullaniciGiris.TC = txtTc.Text;
            bilgiIslemKullaniciGiris.Sifre = txtSifre.Text;
            bilgiIslemKullaniciGiris.Yetki = "Müdür";//otomatik atanacak müdür girişi olduğu için
            bilgiIslemKullaniciGiris.Departman = "Bilgi İslem";//otomatik atanacak departman bilgiişlem  olduğu için

            BilgiIslemKullaniciGirisBLL bll = new BilgiIslemKullaniciGirisBLL();
            try
            {//eger burada hata alırsan cathdeki blogta yazılan mesaj bana hata vermesini göstercek
                string kullaniciadisoyadi = bll.MüdürGirisi(bilgiIslemKullaniciGiris);
                if (kullaniciadisoyadi == "")
                {
                    MessageBox.Show("TC Kimlik numaranız veya şifreniz yanlış. Ya da yetkiniz müdür değildir.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    PopupNotifier popup = new PopupNotifier();
                    popup.Image = Properties.Resources.userana;//buton image userana isimli resmi aktar
                    popup.ImagePadding = new Padding(5);
                    popup.TitleText = "Merhaba".ToUpper().PadLeft(1);
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
                    Form formekranı = new FrmMudurİslemleri();
                    formekranı.Text = "Müdür : " + kullaniciadisoyadi;
                    formekranı.Show();//bilgi işlem müdürlüğü açılsın 
                    this.Hide();//bu form ekranı gizlensin
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTc_VisibleChanged(object sender, EventArgs e)
        {
            if (txtTc.Text.Trim() == "")//eğer TextBox1 boş ise
            {
                errorProvider1.SetError(txtTc, "Bu alan boş geçilmez");
            } // ErrorProvider açılacak ve
            //üstteki satırda belirtilen msj çıkacak
            else
            {
                errorProvider1.SetError(txtTc, "");
            }// ErrorProvider kapanacak
        }

        private void txtTc_TextChanged(object sender, EventArgs e)
        {
            if (txtTc.Text.Trim() == "")//eğer TextBox1 boş ise
            {
                errorProvider1.SetError(txtTc, "Bu alan boş geçilmez");
            } // ErrorProvider açılacak ve
            //üstteki satırda belirtilen msj çıkacak
            else
            {
                errorProvider1.SetError(txtTc, "");
            }// ErrorProvider kapanacak
            if ((txtTc.Text.Length < 11) || (txtTc.Text.Length > 11))
            {
                errorProvider2.SetError(txtTc, "TC Kimlik numarısı 11 karakterli olmalıdır.");
            }
            else
            {
                errorProvider2.Clear();
            }
        }

        private void txtTc_KeyPress(object sender, KeyPressEventArgs e)
        {
            //sadece sayi girişi
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void FrmMudurGiris_Load(object sender, EventArgs e)
        {
            this.Location = new Point(400, 100);//Form ekranın açılınca yernini belirleme Point(x,y)
            errorProvider1.BlinkRate = 100000;
            errorProvider1.BlinkStyle = ErrorBlinkStyle.BlinkIfDifferentError;

            errorProvider2.BlinkRate = 400;
            errorProvider2.BlinkStyle = ErrorBlinkStyle.BlinkIfDifferentError;
        }

        private void FrmMudurGiris_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form anagiris = new FrmAnaGiris();
            anagiris.Show();
            this.Hide();
        }
    }
}
