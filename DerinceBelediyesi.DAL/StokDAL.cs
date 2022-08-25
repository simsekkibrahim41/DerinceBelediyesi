using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;//eklenecek
using DerinceBelediyesi.Entity;//eklenecek

namespace DerinceBelediyesi.DAL
{
    public class StokDAL // public yapmayı unutmaaaaa
    {
        private accessconnection Accessconnection;
        //new acceesscone.... yapma yapıcı metod altında yapılırmış
        public StokDAL()
        {
            Accessconnection = new accessconnection();
        }
        public List<Stok> StokListesi()//Listeeme işlemi
        {// datatable üzerinde veriileri çekiyorum
            OleDbCommand cmd = Accessconnection.GetOleDbCommand();
            cmd.CommandText = "select * from Stok";
            List<Stok> stoks = new List<Stok>();//entity de oluşturduğum class ismi
            OleDbDataReader rdr = cmd.ExecuteReader();
            //cmd nesnesini çalıştırıp daha sonra dönüp tekeer teker okuyup bu kullanicigirisi listesini doldurmamız gerekiyor

            while (rdr.Read())
            {//bütün tablodaki kolonları(alanları) kullaniicgirisi classımın alanlarına koydum
                Stok sstoks = new Stok();
                sstoks.StokID = rdr["StokID"].ToString();
                sstoks.StokAdi = rdr["StokAdi"].ToString();
                sstoks.Tarih = Convert.ToDateTime(rdr["Tarih"]);
                sstoks.Kategori = rdr["Kategori"].ToString();
                sstoks.Miktar =Convert.ToInt32( rdr["Miktar"]);
                sstoks.Islem = rdr["Islem"].ToString();
                sstoks.DolumMiktari = Convert.ToInt32(rdr["DolumMiktari"]);

                //satır satır herkesi incele sonrasında ilk oluğturduğum listeye at liste tekrar sıfırlansın sonra tekrar aynı işlem kullanıcı bitene kadar
                stoks.Add(sstoks);
            }
            return stoks;
        }
        public List<Stok> KategoriListele(Stok stok)//Toner Listeeme işlemi
        {// datatable üzerinde veriileri çekiyorum
            OleDbCommand cmd = Accessconnection.GetOleDbCommand();
            cmd.CommandText = "select * from Stok";
            List<Stok> stoks = new List<Stok>();//entity de oluşturduğum class ismi
            OleDbDataReader rdr = cmd.ExecuteReader();
            //cmd nesnesini çalıştırıp daha sonra dönüp tekeer teker okuyup bu kullanicigirisi listesini doldurmamız gerekiyor

            while (rdr.Read())
            {//bütün tablodaki kolonları(alanları) kullaniicgirisi classımın alanlarına koydum
                Stok sstoks = new Stok();

                if (rdr["Kategori"].ToString() ==stok.Kategori )
                { //Stoklardaki Tonerleri listele

                    sstoks.StokID = rdr["StokID"].ToString();
                    sstoks.StokAdi = rdr["StokAdi"].ToString();
                    sstoks.Tarih = Convert.ToDateTime(rdr["Tarih"]);
                    sstoks.Kategori = rdr["Kategori"].ToString();
                    sstoks.Miktar = Convert.ToInt32(rdr["Miktar"]);
                    sstoks.Islem = rdr["Islem"].ToString();
                    sstoks.DolumMiktari = Convert.ToInt32(rdr["DolumMiktari"]);

                }
                //satır satır herkesi incele sonrasında ilk oluğturduğum listeye at liste tekrar sıfırlansın sonra tekrar aynı işlem kullanıcı bitene kadar
                stoks.Add(sstoks);
            }
            return stoks;
        }
        public List<Stok> TonerListele()//Toner Listeeme işlemi
        {// datatable üzerinde veriileri çekiyorum
            OleDbCommand cmd = Accessconnection.GetOleDbCommand();
            cmd.CommandText = "select * from Stok";
            List<Stok> stoks = new List<Stok>();//entity de oluşturduğum class ismi
            OleDbDataReader rdr = cmd.ExecuteReader();
            //cmd nesnesini çalıştırıp daha sonra dönüp tekeer teker okuyup bu kullanicigirisi listesini doldurmamız gerekiyor

            while (rdr.Read())
            {//bütün tablodaki kolonları(alanları) kullaniicgirisi classımın alanlarına koydum
                Stok sstoks = new Stok();

                if (rdr["Kategori"].ToString() == "Toner")
                { //Stoklardaki Tonerleri listele

                    sstoks.StokID = rdr["StokID"].ToString();
                    sstoks.StokAdi = rdr["StokAdi"].ToString();
                    sstoks.Tarih = Convert.ToDateTime(rdr["Tarih"]);
                    sstoks.Kategori = rdr["Kategori"].ToString();
                    sstoks.Miktar = Convert.ToInt32(rdr["Miktar"]);
                    sstoks.Islem = rdr["Islem"].ToString();
                    sstoks.DolumMiktari = Convert.ToInt32(rdr["DolumMiktari"]);

                }
                //satır satır herkesi incele sonrasında ilk oluğturduğum listeye at liste tekrar sıfırlansın sonra tekrar aynı işlem kullanıcı bitene kadar
                stoks.Add(sstoks);
            }
            return stoks;
        }
        public int StokEkle(Stok stok)//müdür veya personel stok ekleme işlemleri
        {//YENİ Stok EKLEME İŞLEMİ
            string cmdText = "INSERT INTO [Stok] ([StokID],[StokAdi],[Tarih],[Kategori],[Miktar],[Islem],[DolumMiktari])";
            cmdText += String.Format(" Values('{0}','{1}','{2}','{3}',{4},'{5}', {6})",
                stok.StokID, stok.StokAdi,
                stok.Tarih, stok.Kategori,
                stok.Miktar , stok.Islem,
                stok.DolumMiktari = 0);// ilk defa eklendiği için dolum yapılmamış olucak tonerler

            //tabloda hangi dergerimin geleceği indexi aynı olamak zorumda
            //string veya datetime tırnak içinde .Gerisi normal biçimde yani tırnak olmıcak 
            OleDbCommand cmd = Accessconnection.GetOleDbCommand();
            cmd.CommandText = cmdText;
            return cmd.ExecuteNonQuery();
        }
        public int StokMiktariAttir(Stok stok) //Yeni stok geldiğinde miktar attirma işlemi
        {
            OleDbCommand cmd = Accessconnection.GetOleDbCommand();
            cmd.CommandText = string.Format("Update Stok set  Miktar=Miktar+" + stok.Miktar
                 + " where StokID='" + stok.StokID + "'");

            return cmd.ExecuteNonQuery();
        }
        public int StokMiktariAzalt(Stok stok) // Yanlış girilen stok geldiğinde miktar azaltma işlemi
        {
            OleDbCommand cmd = Accessconnection.GetOleDbCommand();
            cmd.CommandText = string.Format("Update Stok set  Miktar=Miktar-" + stok.Miktar
                 + " where StokID='" + stok.StokID + "' and Miktar >= " +stok.Miktar+ "");
            //seçilen stok numarasından elimizze o kadar var mı diye bakıyorum
            return cmd.ExecuteNonQuery();
        }
        public int DolumMiktariArttir(Stok stok) // Eğer kartuş bittiyse doluma yollanıyorsa kaç kere gittiğini öğrenmek için dolum miktarını bir attırmak için
        {
            OleDbCommand cmd = Accessconnection.GetOleDbCommand();
            cmd.CommandText = string.Format("Update Stok set  DolumMiktari = DolumMiktari + 1 where StokID='" + stok.StokID + "'");

            return cmd.ExecuteNonQuery();
        }
        public int DolumMiktariSifirla(Stok stok) // Eğer kartuş 3 kereden fazla doluma yollandı ise bunu tamire görndermek gerekir tamire gönderince dolum miktari sıfırlanması gerekir
        {
            OleDbCommand cmd = Accessconnection.GetOleDbCommand();
            cmd.CommandText = string.Format("Update Stok set  DolumMiktari = 0 where StokID='" + stok.StokID + "'");

            return cmd.ExecuteNonQuery();
        }
        public int StokSil(Stok stok)
        {// stok silme işlemi Girilen stok numarasına göre silme işlemi
            OleDbCommand cmd = Accessconnection.GetOleDbCommand();
            cmd.CommandText = string.Format("Delete From Stok where StokID='" + stok.StokID + "'");
            return cmd.ExecuteNonQuery();
        }
        public int StokBilgileriniGuncelle(Stok stok ) //Stok  güncelleme işlemi
        { 
            OleDbCommand cmd = Accessconnection.GetOleDbCommand();
            cmd.CommandText = string.Format("Update Stok set StokAdi='" + stok.StokAdi
                + "',Tarih='" + stok.Tarih + "',Kategori='" + stok.Kategori
                + "',Miktar=" + stok.Miktar + ",Islem='" + stok.Islem
                + "',DolumMiktari=" + stok.DolumMiktari + " where StokID='" + stok.StokID + "'");

            return cmd.ExecuteNonQuery();
        }
    }
}
