using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DerinceBelediyesi.Entity
{
    public class BilgiIslemKullaniciGiris
    {
        //butün tablo alanlarını burada oluşturcam class halinde olucak
        //artık tablo ile işim olmaması gerekiyor
        //prop tab tab yaparak uzun hali gelir 
        //kolonlarını hepsini tipleriyle beraber yazdım
        public string TC { get; set; }
        public string Sifre { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string Departman { get; set; }
        public string Yetki { get; set; }
        public int Mesai { get; set; }

    }
}
