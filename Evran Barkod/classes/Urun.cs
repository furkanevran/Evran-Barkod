using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evran_Barkod.classes
{
    class Urun
    {
        public string Barkod { get; set; }
        public string Ad { get; set; }
        public decimal Fiyat { get; set; } = -1;
        public decimal KGFiyat { get; set; } = -1;
        public bool HizliEkle { get; set; } = false;
    }
}
