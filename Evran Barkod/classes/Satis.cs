using Evran_Barkod.derived;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evran_Barkod.classes
{
    class Satis
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public List<SatisUrun> SatilanUrunler { get; set; } = new List<SatisUrun>();
        private decimal _toplamUcret => SatilanUrunler.Sum((x) =>
                 {
                     return x.Fiyat;
                 });

        public decimal ToplamUcret { get; set; }

        public Satis() { }
        public Satis(TrulyObservableCollection<SatisUrun> c)
        {
            foreach (SatisUrun x in c)
            {
                SatilanUrunler.Add(x);
            }
        }

        public Satis(string json)
        {
            SatilanUrunler = JsonConvert.DeserializeObject<List<SatisUrun>>(json);
        }

        public DateTime Tarih { get; set; } = DateTime.Now;
        public string Saat { get
            {

                var t = Tarih.TimeOfDay;
                return t.Hours+":"+t.Minutes;
            }
        }

        public string TarihT
        {
            get
            {
                return Tarih.Day+"/"+Tarih.Month+" - "+Tarih.Hour+":"+Tarih.Minute;
            }
        }

        public int SatilanUrunSayisi => SatilanUrunler.Sum((x) =>
        {
            int t;
            if (x.Urun.KGFiyat == -1)
            {
                return x.Adet;
            } 
            return 1;
        });
    }
}
