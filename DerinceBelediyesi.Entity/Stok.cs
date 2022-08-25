using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DerinceBelediyesi.Entity
{
    public class Stok //public olmasına dikkat et
    {
        //butün tablo alanlarını burada oluşturcam class halinde olucak
        //artık tablo ile işim olmaması gerekiyor
        //prop tab tab yaparak uzun hali gelir 
        //kolonlarını hepsini tipleriyle beraber yazdım
        public string StokID { get; set; }
        public string StokAdi { get; set; }
        public DateTime Tarih { get; set; }
        public string Kategori { get; set; }
        public int Miktar { get; set; }
        public string Islem { get; set; }
        public int DolumMiktari { get; set; }
    }
}
