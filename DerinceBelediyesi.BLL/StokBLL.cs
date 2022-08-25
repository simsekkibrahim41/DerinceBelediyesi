using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DerinceBelediyesi.DAL;//DAL katmanına ulaşmak için ekledim
using DerinceBelediyesi.Entity;//Entity katmanına ulaşmak için ekledim

namespace DerinceBelediyesi.BLL
{
    public class StokBLL
    {
        //BLL DAL dan ve Entity den referans alıcak  
        StokDAL stokDAL;//ataması yapıcı metoda yağılır
        public StokBLL()
        {//ctor tab tab yapıcı metod yapar
            stokDAL = new StokDAL();
        }
        public List<Stok> StokListesi()
        {
            return stokDAL.StokListesi();
            //BLL gidip StokListesi metodu tetiklendiğinde gidip kullanci......DAL a gidip StokListesi metodu çağırı geri dönderdi
        }
        public List<Stok> KategoriListele(Stok stok)
        {
            return stokDAL.KategoriListele(stok);
            //BLL gidip KategoriListele metodu tetiklendiğinde gidip kullanci......DAL a gidip KategoriListele metodu çağırı geri dönderdi
        }
        public List<Stok> TonerListele()
        {
            return stokDAL.TonerListele();
            //BLL gidip TonerListele metodu tetiklendiğinde gidip kullanci......DAL a gidip TonerListele metodu çağırı geri dönderdi
        }
        public int StokEkle(Stok stok)
        {// stok eklemesi yapılırken alanlar dolumu diye bakılıyor
            if ((stok.StokID == "") && (stok.StokAdi == "")  && (stok.Kategori == "") && (Convert.ToInt32(stok.Miktar) ==0) && (stok.Islem ==""))
            {
                throw new Exception("Lütfen Alanları doldurunuz");
            }
            else if (stok.StokID == "")
            {
                throw new Exception("Lütfen Stok numarasını giriniz!!");
            }
            else if ((stok.StokAdi == ""))
            {
                throw new Exception("Lütfen Stok Adını giriniz!!");
            }
            else if (stok.Kategori == "Seçiniz")
            {
                throw new Exception("Lütfen Kategori seçiniz!!");
            }
            else if (stok.Miktar <= 0)
            {
                throw new Exception("Lütfen Stok miktarını pozitif tam sayı giriniz!!");
            }
            else if (stok.Islem == "Seçiniz")
            {
                throw new Exception("Lütfen yapılan işlemi seçiniz!!");
            }
            return stokDAL.StokEkle(stok);
        }
        public int StokMiktariAttir(Stok stok)
        {
            if ((stok.StokID == "") && (Convert.ToInt32(stok.Miktar) == 0))
            {
                throw new Exception("Lütfen Alanları doldurunuz");
            }
            else if (stok.StokID == "")
            {
                throw new Exception("Lütfen Stok numarasını giriniz!!");
            }

            else if (stok.Miktar <= 0)
            {
                throw new Exception("Lütfen Stok miktarını pozitif tam sayı giriniz!!");
            }
            
            return stokDAL.StokMiktariAttir(stok);
        }
        public int StokMiktariAzalt(Stok stok)
        {
            return stokDAL.StokMiktariAzalt(stok);
        }
        public int DolumMiktariArttir(Stok stok)
        {
            return stokDAL.DolumMiktariArttir(stok);
        }
        public int DolumMiktariSifirla(Stok stok)
        {
            return stokDAL.DolumMiktariSifirla(stok);
        }
        public int StokSil(Stok stok)
        {
            return stokDAL.StokSil(stok);
        }
        public int StokBilgileriniGuncelle(Stok stok)
        {// stok güncellemesi yapılırken alanlar dolumu diye bakılıyor
            if ((stok.StokID == "") && (stok.StokAdi == "") && (stok.Kategori == "") && (Convert.ToInt32(stok.Miktar) == 0) && (stok.Islem == ""))
            {
                throw new Exception("Lütfen Alanları doldurunuz");
            }
            else if (stok.StokID == "")
            {
                throw new Exception("Lütfen Stok numarasını giriniz!!");
            }
            else if ((stok.StokAdi == ""))
            {
                throw new Exception("Lütfen Stok Adını giriniz!!");
            }
            else if (stok.Kategori == "Seçiniz")
            {
                throw new Exception("Lütfen Kategori seçiniz!!");
            }
            else if (stok.Miktar <= 0)
            {
                throw new Exception("Lütfen Stok miktarını pozitif tam sayı giriniz!!");
            }
            else if (stok.Islem == "Seçiniz")
            {
                throw new Exception("Lütfen yapılan işlemi seçiniz!!");
            }
            return stokDAL.StokBilgileriniGuncelle(stok);
        }
    }
}
