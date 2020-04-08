using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evran_Barkod.classes
{
    static class SatisListesi
    {
        public static ObservableCollection<Satis> Satislar { get; set; }

        static SatisListesi()
        {
            string json;

            if (!File.Exists("satisveri.json"))
            {
                Satislar = new ObservableCollection<Satis>();
                Satislar.CollectionChanged += Satislar_CollectionChanged;
                return;
            }

            using (StreamReader sr = new StreamReader("satisveri.json", Encoding.UTF8))
            {
                json = sr.ReadToEnd();
            }

            Satislar = JsonConvert.DeserializeObject<ObservableCollection<Satis>>(json);
            Satislar.CollectionChanged += Satislar_CollectionChanged;
        }

        public static void Guncelle()
        {
            string q = JsonConvert.SerializeObject(Satislar);

            using (StreamWriter sw = new StreamWriter("satisveri.json", false, Encoding.UTF8))
            {
                sw.Write(q);
                sw.Flush();
            }
        }

        private static void Satislar_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Guncelle();
        }
    }
}
