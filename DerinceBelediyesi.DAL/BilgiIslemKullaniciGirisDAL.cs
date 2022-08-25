using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;//eklenecek
using System.Data;//eklenecek
using DerinceBelediyesi.Entity;//eklenecek

namespace DerinceBelediyesi.DAL
{
    public class BilgiIslemKullaniciGirisDAL
    {
        private accessconnection Accessconnection;
        //new acceesscone.... yapma yapıcı metod altında yapılırmış
        public BilgiIslemKullaniciGirisDAL()
        {
            Accessconnection = new accessconnection();
        }
        public List<BilgiIslemKullaniciGiris> PersonelListele()//BURAYI MÜDÜR GÖREBİLİR PERSONEL LİSTESİ
        {// datatable üzerinde veriileri çekiyorum
            OleDbCommand cmd = Accessconnection.GetOleDbCommand();//bağlantı hazır komut yazılmadı daha
            cmd.CommandText = "select * from Personel";
            List<BilgiIslemKullaniciGiris> bilgiIslemKullaniciGirises = new List<BilgiIslemKullaniciGiris>();//entity de oluşturduğum class ismi
            OleDbDataReader rdr = cmd.ExecuteReader();
            //cmd nesnesini çalıştırıp daha sonra dönüp tekeer teker okuyup bu kullanicigirisi listesini doldurmamız gerekiyor

            while (rdr.Read())
            {//bütün tablodaki kolonları(alanları) kullaniicgirisi classımın alanlarına koydum
                BilgiIslemKullaniciGiris bbilgiislemkullanicigirisi = new BilgiIslemKullaniciGiris();
                bbilgiislemkullanicigirisi.TC = rdr["TC"].ToString();
                bbilgiislemkullanicigirisi.Sifre = rdr["Sifre"].ToString();
                bbilgiislemkullanicigirisi.Adi = rdr["Adi"].ToString();
                bbilgiislemkullanicigirisi.Soyadi = rdr["Soyadi"].ToString();
                bbilgiislemkullanicigirisi.Departman = rdr["Departman"].ToString();
                bbilgiislemkullanicigirisi.Yetki = rdr["Yetki"].ToString();
                bbilgiislemkullanicigirisi.Mesai = Convert.ToInt32(rdr["Mesai"]);

                //satır satır herkesi incele sonrasında ilk oluğturduğum listeye at liste tekrar sıfırlansın sonra tekrar aynı işlem kullanıcı bitene kadar
                bilgiIslemKullaniciGirises.Add(bbilgiislemkullanicigirisi);
            }
            return bilgiIslemKullaniciGirises;
        }

        public string PersonelGirisi(BilgiIslemKullaniciGiris bilgiIslemKullaniciGiris)
        {//datatable üzerinde veriileri çekiyorum
            OleDbCommand cmd = Accessconnection.GetOleDbCommand();//bağlantı hazır komut yazılmadı daha
            cmd.CommandText = "select * from Personel ";
            OleDbDataReader rdr = cmd.ExecuteReader();
            //cmd nesnesini çalıştırıp daha sonra dönüp tekeer teker okuyup bu kullanicigirisi listesini doldurmamız gerekiyor
            string kullaniciadi = "";
            while (rdr.Read())
            {
                if ((rdr["TC"].ToString() == bilgiIslemKullaniciGiris.TC) 
                    &&(rdr["Sifre"].ToString() == bilgiIslemKullaniciGiris.Sifre)
                    &&(rdr["Departman"].ToString()=="Bilgi İşlem")
                    &&((rdr["Yetki"].ToString() == "Personel")||(rdr["Yetki"].ToString() == "Müdür")))
                {//Bilgi işlemde bu personel veya müdür  ise TC ve şifre doğru ise bana kullanıcı adını ve soyadını dönder.
                    kullaniciadi = rdr["Adi"].ToString() + " " + rdr["Soyadi"].ToString();//bunu giriş ekranında kim giriş yaptı diye kullamak istiyorum
                }
            }
            return kullaniciadi;
        }
        public string MüdürGirisi(BilgiIslemKullaniciGiris bilgiIslemKullaniciGiris)
        {//datatable üzerinde veriileri çekiyorum
            OleDbCommand cmd = Accessconnection.GetOleDbCommand();//bağlantı hazır komut yazılmadı daha
            cmd.CommandText = "select * from Personel ";
            OleDbDataReader rdr = cmd.ExecuteReader();
            //cmd nesnesini çalıştırıp daha sonra dönüp tekeer teker okuyup bu kullanicigirisi listesini doldurmamız gerekiyor
            string kullaniciadi = "";
            while (rdr.Read())
            {
                if ((rdr["TC"].ToString() == bilgiIslemKullaniciGiris.TC)
                    && (rdr["Sifre"].ToString() == bilgiIslemKullaniciGiris.Sifre)
                    && (rdr["Departman"].ToString() == "Bilgi İşlem")
                    && ((rdr["Yetki"].ToString() == "Müdür")))
                {//Bilgi işlemde bu  müdür  ise TC ve şifre doğru ise bana kullanıcı adını ve soyadını dönder.
                    kullaniciadi = rdr["Adi"].ToString() + " " + rdr["Soyadi"].ToString();//bunu giriş ekranında kim giriş yaptı diye kullamak istiyorum
                }
            }
            return kullaniciadi;
        }
        public string PersonelEkle(BilgiIslemKullaniciGiris bilgiIslemKullaniciGiris)//MÜDÜR PERSONEL EKLEYEBİLİR
        {//YENİ PERSONEL EKLEME İŞLEMİ
            string cmdText = "INSERT INTO [Personel] ([TC],[Sifre],[Adi],[Soyadi],[Departman],[Yetki],[Mesai])";
            cmdText += String.Format(" Values('{0}','{1}','{2}','{3}','{4}','{5}', {6})",
                bilgiIslemKullaniciGiris.TC,bilgiIslemKullaniciGiris.Sifre,
                bilgiIslemKullaniciGiris.Adi, bilgiIslemKullaniciGiris.Soyadi,
                bilgiIslemKullaniciGiris.Departman= "Bilgi İşlem", bilgiIslemKullaniciGiris.Yetki,
                bilgiIslemKullaniciGiris.Mesai=0);
            //tabloda hangi dergerimin geleceği indexi aynı olamak zorumda
            //string veya datetime tırnak içinde .Gerisi normal biçimde yani tırnak olmıcak 
            OleDbCommand cmd = Accessconnection.GetOleDbCommand();
            cmd.CommandText = cmdText;
            cmd.ExecuteNonQuery();
            return bilgiIslemKullaniciGiris.Adi + " " + bilgiIslemKullaniciGiris.Soyadi;//açılır pencere mesajı için dönderiyorum
        }
        public int PersonelSil(BilgiIslemKullaniciGiris bilgiIslemKullaniciGiris)//PERSONEL SİLME İŞLEMİ
        {
            OleDbCommand cmd = Accessconnection.GetOleDbCommand();
            cmd.CommandText = string.Format("Delete From Personel where TC='" + bilgiIslemKullaniciGiris.TC + "'");
            //personelin TC'sine göre sil

            return cmd.ExecuteNonQuery();
        }
        public int PersonelBilgilerigüncelle(BilgiIslemKullaniciGiris bilgiIslemKullaniciGiris) //Personel  güncelleme işlemi
        {
            OleDbCommand cmd = Accessconnection.GetOleDbCommand();
            cmd.CommandText = string.Format("Update Personel set Sifre='" + bilgiIslemKullaniciGiris.Sifre 
                + "',Adi='" + bilgiIslemKullaniciGiris.Adi  + "',Soyadi='" + bilgiIslemKullaniciGiris.Soyadi 
                + "',Departman='" + bilgiIslemKullaniciGiris.Departman  + "',Yetki='" + bilgiIslemKullaniciGiris.Yetki
                + "',Mesai=" + bilgiIslemKullaniciGiris.Mesai + " where TC='" + bilgiIslemKullaniciGiris.TC + "'");

            return cmd.ExecuteNonQuery();
        }
        public int PersonelMesaiEkle(BilgiIslemKullaniciGiris bilgiIslemKullaniciGiris) //Personelin mesai işlemi güncelleme işlemi
        {
            OleDbCommand cmd = Accessconnection.GetOleDbCommand();
            cmd.CommandText = string.Format("Update Personel set  Mesai=Mesai+" + bilgiIslemKullaniciGiris.Mesai
                 + " where TC='" + bilgiIslemKullaniciGiris.TC + "'");//Yaptığı mesaiye mesai ekleme işlemi

            return cmd.ExecuteNonQuery();
        }
        public int PersonelMesaiSil(BilgiIslemKullaniciGiris bilgiIslemKullaniciGiris) //Personelin mesai işlemi güncelleme işlemi
        {
            OleDbCommand cmd = Accessconnection.GetOleDbCommand();
            cmd.CommandText = string.Format("Update Personel set  Mesai=Mesai-" + bilgiIslemKullaniciGiris.Mesai
                 + " where TC='" + bilgiIslemKullaniciGiris.TC + "'");//Yanlış  mesaiye girdiğimizde  Silme işlemi

            return cmd.ExecuteNonQuery();
        }
        public int PersonelMesaiSifirla(BilgiIslemKullaniciGiris bilgiIslemKullaniciGiris) //Personelin mesai işlemi güncelleme işlemi
        {
            OleDbCommand cmd = Accessconnection.GetOleDbCommand();
            cmd.CommandText = string.Format("Update Personel set  Mesai=0 where TC='" + bilgiIslemKullaniciGiris.TC + "'");
            //Mesai sıfırlama

            return cmd.ExecuteNonQuery();
        }
    }
}
