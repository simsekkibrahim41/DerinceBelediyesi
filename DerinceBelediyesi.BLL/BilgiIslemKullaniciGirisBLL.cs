using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DerinceBelediyesi.DAL;//DAL katmanına ulaşmak için ekledim
using DerinceBelediyesi.Entity;//Entity katmanına ulaşmak için ekledim

namespace DerinceBelediyesi.BLL
{
    public class BilgiIslemKullaniciGirisBLL
    {//BLL DAL dan ve Entity den referans alıcak  
        BilgiIslemKullaniciGirisDAL bilgiIslemKullaniciGirisDAL;//ataması yapıcı metoda yağılır
        public BilgiIslemKullaniciGirisBLL()
        {//ctor tab tab yapıcı metod yapar
            bilgiIslemKullaniciGirisDAL = new BilgiIslemKullaniciGirisDAL();
        }
        public List<BilgiIslemKullaniciGiris> PersonelListele()
        {
            return bilgiIslemKullaniciGirisDAL.PersonelListele();
            //BLL gidip PersonelListele metodu tetiklendiğinde gidip kullanci......DAL a gidip getallaitem metodu çağırı geri dönderdi
        }
        public string PersonelGirisi(BilgiIslemKullaniciGiris bilgiIslemKullaniciGiris)
        {//Arama işlemi giriş yapma işlemi gibi
            string kullaniciadi = bilgiIslemKullaniciGirisDAL.PersonelGirisi(bilgiIslemKullaniciGiris);
            //işlemler bu katmanda yapılır.
            if ((bilgiIslemKullaniciGiris.TC == "") && (bilgiIslemKullaniciGiris.Sifre == ""))
            {
                throw new Exception("TC ve şifrenizi giriniz");
            }
            else if (bilgiIslemKullaniciGiris.TC == "")
            {
                throw new Exception("TC giriniz");
            }
            else if (bilgiIslemKullaniciGiris.Sifre == "")
            {
                throw new Exception("Şifrenizi giriniz");
            }
            //else if (kullaniciadi == "")
            //{
            //    throw new Exception("TC veya şifreniz yanlış");
            //}

            return kullaniciadi;
        }
        public string MüdürGirisi(BilgiIslemKullaniciGiris bilgiIslemKullaniciGiris)
        {//Arama işlemi giriş yapma işlemi gibi
            string kullaniciadi = bilgiIslemKullaniciGirisDAL.MüdürGirisi(bilgiIslemKullaniciGiris);
            //işlemler bu katmanda yapılır.
            if ((bilgiIslemKullaniciGiris.TC == "") && (bilgiIslemKullaniciGiris.Sifre == ""))
            {
                throw new Exception("TC ve şifrenizi giriniz");
            }
            else if (bilgiIslemKullaniciGiris.TC == "")
            {
                throw new Exception("TC giriniz");
            }
            else if (bilgiIslemKullaniciGiris.Sifre == "")
            {
                throw new Exception("Şifrenizi giriniz");
            }
            //else if (kullaniciadi == "")
            //{
            //    throw new Exception("TC veya şifreniz yanlış");
            //}

            return kullaniciadi;
        }
        public string PersonelEkle(BilgiIslemKullaniciGiris bilgiIslemKullaniciGiris)///dalın entity yi referans alması lazım
        {
            //kullanıcıadı =
            if ((bilgiIslemKullaniciGiris.TC == "") && (bilgiIslemKullaniciGiris.Departman == "") && (bilgiIslemKullaniciGiris.Sifre == "") && (bilgiIslemKullaniciGiris.Adi == "") && (bilgiIslemKullaniciGiris.Soyadi == "") && (bilgiIslemKullaniciGiris.Yetki == ""))
            {
                throw new Exception("Boş alanları doldurunuz.");
            }
            else if (bilgiIslemKullaniciGiris.TC == "")
            {
                throw new Exception("TC giriniz.");
            }
            else if (bilgiIslemKullaniciGiris.Departman == "")
            {
                throw new Exception("Departman giriniz.");
            }
            else if (bilgiIslemKullaniciGiris.Sifre.Length < 3)
            {
                throw new Exception("Sifreniz en az 4 karakter olmalıdır.");
            }
            else if (bilgiIslemKullaniciGiris.Adi == "")
            {
                throw new Exception("Lütfen adınızı giriniz.");
            }
            else if (bilgiIslemKullaniciGiris.Soyadi == "")
            {
                throw new Exception("Lütfen soyadınızı giriniz.");
            }
            else if (bilgiIslemKullaniciGiris.Yetki == "Seçiniz")
            {
                throw new Exception("Lütfen Yetki seçiniz.");
            }
            return bilgiIslemKullaniciGirisDAL.PersonelEkle(bilgiIslemKullaniciGiris);//sıralamaya dikkat e
        }
        public int PersonelSil(BilgiIslemKullaniciGiris bilgiIslemKullaniciGiris)
        {
            return bilgiIslemKullaniciGirisDAL.PersonelSil(bilgiIslemKullaniciGiris);
        }
        public int PersonelBilgilerigüncelle(BilgiIslemKullaniciGiris bilgiIslemKullaniciGiris)
        {
            //kullanıcıadı =
            if ((bilgiIslemKullaniciGiris.TC == "") && (bilgiIslemKullaniciGiris.Departman == "") && (bilgiIslemKullaniciGiris.Sifre == "") && (bilgiIslemKullaniciGiris.Adi == "") && (bilgiIslemKullaniciGiris.Soyadi == "") && (bilgiIslemKullaniciGiris.Yetki == ""))
            {
                throw new Exception("Boş alanları doldurunuz.");
            }
            else if (bilgiIslemKullaniciGiris.TC == "")
            {
                throw new Exception("TC giriniz.");
            }
            else if (bilgiIslemKullaniciGiris.Departman == "")
            {
                throw new Exception("Departman giriniz.");
            }
            else if (bilgiIslemKullaniciGiris.Sifre.Length < 3)
            {
                throw new Exception("Sifreniz en az 4 karakter olmalıdır.");
            }
            else if (bilgiIslemKullaniciGiris.Adi == "")
            {
                throw new Exception("Lütfen adınızı giriniz.");
            }
            else if (bilgiIslemKullaniciGiris.Soyadi == "")
            {
                throw new Exception("Lütfen soyadınızı giriniz.");
            }
            else if (bilgiIslemKullaniciGiris.Yetki == "Seçiniz")
            {
                throw new Exception("Lütfen Yetki seçiniz.");
            }
            return bilgiIslemKullaniciGirisDAL.PersonelBilgilerigüncelle(bilgiIslemKullaniciGiris);
        }
        public int PersonelMesaiEkle(BilgiIslemKullaniciGiris bilgiIslemKullaniciGiris)
        {
            return bilgiIslemKullaniciGirisDAL.PersonelMesaiEkle(bilgiIslemKullaniciGiris);
        }
        public int PersonelMesaiSil(BilgiIslemKullaniciGiris bilgiIslemKullaniciGiris)
        {
            return bilgiIslemKullaniciGirisDAL.PersonelMesaiSil(bilgiIslemKullaniciGiris);
        }
        public int PersonelMesaiSifirla(BilgiIslemKullaniciGiris bilgiIslemKullaniciGiris)
        {
            return bilgiIslemKullaniciGirisDAL.PersonelMesaiSifirla(bilgiIslemKullaniciGiris);
        }
    }
}
