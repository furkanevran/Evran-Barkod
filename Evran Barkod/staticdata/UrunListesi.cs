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
    static class UrunListesi
    {
        public static ObservableCollection<Urun> Urunler { get; set; }
        
        static UrunListesi()
        {
            string json;

            if (!File.Exists("urunveri.json"))
            {
                Urunler = new ObservableCollection<Urun>();
                Urunler.CollectionChanged += Urunler_CollectionChanged;
                return;
            }

            using (StreamReader sr = new StreamReader("urunveri.json", Encoding.UTF8))
            {
                json = sr.ReadToEnd();
            }

            Urunler = JsonConvert.DeserializeObject<ObservableCollection<Urun>>(json);
            Urunler.CollectionChanged += Urunler_CollectionChanged;
        }

        public static void Guncelle()
        {
            string q = JsonConvert.SerializeObject(Urunler);

            using (StreamWriter sw = new StreamWriter("urunveri.json", false, Encoding.UTF8))
            {
                sw.Write(q);
                sw.Flush();
            }
        }

        private static void Urunler_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Guncelle();
        }
    }
}
