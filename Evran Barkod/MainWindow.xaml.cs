using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;
using Evran_Barkod.views;
using Evran_Barkod.classes;
using Newtonsoft.Json;
using System.IO;
using MahApps.Metro.Controls.Dialogs;
using System.Collections.ObjectModel;
using System.Data;

namespace Evran_Barkod
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            satisContentControl.Content = new SatisView(this);
            dgUrunDuzenle.ItemsSource = UrunListesi.Urunler;
            dgUrunKaldir.ItemsSource = UrunListesi.Urunler;
            dgRaporGun.ItemsSource = SatisListesi.Satislar.Where(x => x.Tarih.ToShortDateString() == DateTime.Now.ToShortDateString());
            dgRaporAy.ItemsSource = SatisListesi.Satislar.Where(x => x.Tarih.Month == DateTime.Now.Month && x.Tarih.Year == DateTime.Now.Year);
            dpRaporSecimSon.Text = DateTime.Now.ToShortDateString();

            
            Closing += (s, e) =>
            {
                DateTime bt = DateTime.Now;
                string fp = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"backup\\{bt.ToShortDateString().Replace("." , "") + "-" + bt.ToShortTimeString().Replace(":", "")}");
                Directory.CreateDirectory(fp);

                if (File.Exists("urunveri.json"))
                {
                    File.Copy("urunveri.json", fp + @"\urunveri.json");
                }

                if (File.Exists("satisveri.json"))
                {
                    File.Copy("satisveri.json", fp + @"\satisveri.json");
                }
            };
            /*
                        Urun t = new Urun()
                        {
                            Barkod = "80135876",
                            Ad = "Nutella 350gr.",
                            Fiyat = 13.5m
                        };

                        Urun l = new Urun()
                        {
                            Barkod = "8690120070056",
                            Ad = "Torku Fındık Kreması 400gr.",
                            Fiyat = 10.5m
                        };

                        List<Urun> u = new List<Urun>();
                        u.Add(t);
                        u.Add(l);
                        string q = JsonConvert.SerializeObject(u);

                        using (StreamWriter sw = new StreamWriter("veri.json", false, Encoding.UTF8))
                        {
                            sw.Write(q);
                            sw.Flush();
                        }*/
        }

        private void UrunEkle(object sender, RoutedEventArgs e)
        {
            tcTabs.SelectedIndex = 1;
            tcUrun.SelectedIndex = 0;
        }

        private string btu = "";
        private void Urun_Ekle_BarkodluTeraziUrunuStateChanged(object sender, RoutedEventArgs e)
        {
            bool? a = cbUrunEkleBarkodluTerazi.IsChecked;

            if (a == true)
            {
                txUrunEkleFiyat.SetValue(TextBoxHelper.WatermarkProperty, "KG Fiyat");
                if (txUrunEkleBarkod.Text.Length >= 7)
                {
                    btu = txUrunEkleBarkod.Text;
                    txUrunEkleBarkod.Text = btu.Substring(0, 6);
                    txUrunEkleBarkod.MaxLength = 7;
                }
            }
            else
            {
                txUrunEkleFiyat.SetValue(TextBoxHelper.WatermarkProperty, "Fiyat");
                if (txUrunEkleBarkod.Text.Length >= 7)
                {
                    txUrunEkleBarkod.Text = txUrunEkleBarkod.Text + btu.Substring(6);
                    txUrunEkleBarkod.MaxLength = 100;
                }
            }

        }

        private async void Urun_Ekle_UrunEkle(object sender, RoutedEventArgs e)
        {
            Urun s = UrunListesi.Urunler.SingleOrDefault((x) => x.Barkod == txUrunEkleBarkod.Text);

            if (s != null)
            {
                MessageDialogResult mdr = await this.ShowMessageAsync("Hata", $"{s.Ad} ürününü zaten kayıtlı düzenlemek ister misiniz?", MessageDialogStyle.AffirmativeAndNegative);

                if (mdr == MessageDialogResult.Affirmative)
                {
                    txUrunDuzenleAd.Text = s.Ad;
                    txUrunDuzenleBarkod.Text = s.Barkod;

                    bool bkt = s.KGFiyat == -1m ? false : true;
                    string f = s.KGFiyat == -1m ? s.Fiyat + "" : s.KGFiyat + "";

                    txUrunDuzenleFiyat.Text = f;
                    cbUrunDuzenleBarkodluTerazi.IsChecked = bkt;
                    dgUrunDuzenle.Visibility = Visibility.Hidden;
                    txUrunDuzenleAraBarkod.Visibility = Visibility.Hidden;
                    gDuzenle.Visibility = Visibility.Visible;
                    cbUrunDuzenleHizli.IsChecked = s.HizliEkle;

                    tcTabs.SelectedIndex = 1;
                    tcUrun.SelectedIndex = 1;
                }

                txUrunEkleBarkod.Text = "";
                txUrunEkleAd.Text = "";
                txUrunEkleFiyat.Text = "";
                cbUrunEkleBarkodluTerazi.IsChecked = false;
                txUrunEkleBarkod.MaxLength = 100;
                cbUrunEkleHizli.IsChecked = false;
                return;
            }

            Urun u = new Urun()
            {
                Barkod = txUrunEkleBarkod.Text,
                Ad = txUrunEkleAd.Text,
                HizliEkle = cbUrunEkleHizli.IsChecked == true
            };

            string fy = string.Format(txUrunEkleFiyat.Text, "N2");

            if (!fy.Contains(','))
            {
                fy += ",0";
            }

            if (cbUrunEkleBarkodluTerazi.IsChecked == true)
            {
                u.KGFiyat = decimal.Parse(fy);
            }
            else
            {
                u.Fiyat = decimal.Parse(fy);
            }

            UrunListesi.Urunler.Add(u);

            txUrunEkleBarkod.Text = "";
            txUrunEkleAd.Text = "";
            txUrunEkleFiyat.Text = "";
            cbUrunEkleBarkodluTerazi.IsChecked = false;
            cbUrunEkleHizli.IsChecked = false;
            txUrunEkleBarkod.MaxLength = 100;
            txUrunEkleBarkod.Focus();
        }

        private bool isKeyNumeric(Key key)
        {
            if (Keyboard.IsKeyDown(Key.LeftAlt) ||
    Keyboard.IsKeyDown(Key.RightAlt) ||
    Keyboard.IsKeyDown(Key.LeftCtrl) ||
    Keyboard.IsKeyDown(Key.RightCtrl) ||
    Keyboard.IsKeyDown(Key.RightShift) ||
    Keyboard.IsKeyDown(Key.LeftShift))
            {
                return false;
            }


            switch (key)
            {
                case Key.D0:
                case Key.D1:
                case Key.D2:
                case Key.D3:
                case Key.D4:
                case Key.D5:
                case Key.D6:
                case Key.D7:
                case Key.D8:
                case Key.D9:
                case Key.NumPad0:
                case Key.NumPad1:
                case Key.NumPad2:
                case Key.NumPad3:
                case Key.NumPad4:
                case Key.NumPad5:
                case Key.NumPad6:
                case Key.NumPad7:
                case Key.NumPad8:
                case Key.NumPad9: return true;

                default:
                    return false;
            }
        }

        private void TextBox_OnlyAcceptNumberic(object sender, KeyEventArgs e)
        {
            if (isKeyNumeric(e.Key) || e.Key == Key.Delete || e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Back || e.Key == Key.Tab)
            {
                return;
            }

            e.Handled = true;
        }

        private void TextBox_OnlyAcceptNumbericAndPeriod(object sender, KeyEventArgs e)
        {
            if (isKeyNumeric(e.Key) || e.Key == Key.OemComma || e.Key == Key.Delete || e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Back || e.Key == Key.Tab)
            {
                return;
            }

            e.Handled = true;
        }

        private int uid = -1;
        private void Urun_Duzenle_DuzenlemeEkrani(object sender, MouseButtonEventArgs e)
        {
            if (dgUrunDuzenle.SelectedItem == null)
            {
                e.Handled = true;
                return;
            }
            Urun s = (Urun)dgUrunDuzenle.SelectedItem;
            uid = UrunListesi.Urunler.IndexOf((Urun)dgUrunDuzenle.SelectedItem);
            txUrunDuzenleAd.Text = s.Ad;
            txUrunDuzenleBarkod.Text = s.Barkod;

            bool bkt = s.KGFiyat == -1m ? false : true;
            string f = s.KGFiyat == -1m ? s.Fiyat + "" : s.KGFiyat + "";

            txUrunDuzenleFiyat.Text = f;
            cbUrunDuzenleBarkodluTerazi.IsChecked = bkt;
            dgUrunDuzenle.Visibility = Visibility.Hidden;
            txUrunDuzenleAraBarkod.Visibility = Visibility.Hidden;
            gDuzenle.Visibility = Visibility.Visible;
            cbUrunDuzenleHizli.IsChecked = s.HizliEkle;
        }

        private void Urun_Duzenle_Duzenle(object sender, RoutedEventArgs e)
        {
            UrunListesi.Urunler[uid].Barkod = txUrunDuzenleBarkod.Text;
            UrunListesi.Urunler[uid].Ad = txUrunDuzenleAd.Text;
            UrunListesi.Urunler[uid].HizliEkle = cbUrunDuzenleHizli.IsChecked == true;
            decimal f;

            f = decimal.Parse(txUrunDuzenleFiyat.Text);

            if (cbUrunDuzenleBarkodluTerazi.IsChecked == true)
            {
                UrunListesi.Urunler[uid].KGFiyat = f;
            }
            else
            {
                
                UrunListesi.Urunler[uid].Fiyat = f;
            }

         



            txUrunDuzenleBarkod.Text = "";
            txUrunDuzenleAd.Text = "";
            txUrunEkleFiyat.Text = "";
            cbUrunDuzenleBarkodluTerazi.IsChecked = false;

            UrunListesi.Guncelle();

            dgUrunDuzenle.ItemsSource = null;
            dgUrunDuzenle.ItemsSource = UrunListesi.Urunler;
            dgUrunKaldir.ItemsSource = null;
            dgUrunKaldir.ItemsSource = UrunListesi.Urunler;

            gDuzenle.Visibility = Visibility.Hidden;
            txUrunDuzenleAraBarkod.Visibility = Visibility.Visible;
            dgUrunDuzenle.Visibility = Visibility.Visible;
        }

        private void Urun_Duzenle_Geri(object sender, RoutedEventArgs e)
        {
            gDuzenle.Visibility = Visibility.Hidden;
            txUrunDuzenleAraBarkod.Visibility = Visibility.Visible;
            dgUrunDuzenle.Visibility = Visibility.Visible;
        }

        private async void Urun_Kaldir_Kaldir(object sender, RoutedEventArgs e)
        {
            if (dgUrunKaldir.SelectedIndex == -1)
            {
                return;
            }

            Urun s = (Urun)dgUrunKaldir.SelectedItem;
            int id = UrunListesi.Urunler.IndexOf((Urun)dgUrunKaldir.SelectedItem);
            MessageDialogResult mdr = await this.ShowMessageAsync("Onay", $"{s.Ad} ürününü kaldırmak istediğinize emin misiniz?", MessageDialogStyle.AffirmativeAndNegative);

            if (mdr == MessageDialogResult.Affirmative)
            {
                UrunListesi.Urunler.RemoveAt(id);

                dgUrunDuzenle.ItemsSource = null;
                dgUrunDuzenle.ItemsSource = UrunListesi.Urunler;
                dgUrunKaldir.ItemsSource = null;
                dgUrunKaldir.ItemsSource = UrunListesi.Urunler;
            }

        }

        private int prev = -1;
        private void RefreshRaporTiles(object sender, SelectionChangedEventArgs e)
        {
            if (prev == tcTabs.SelectedIndex)
            {
                return;
            }

            prev = tcTabs.SelectedIndex;

            if (prev != 2)
            {
                return;
            }

            dgRaporGun.ItemsSource = null;
            var d = SatisListesi.Satislar.Where(x => x.Tarih.ToShortDateString() == DateTime.Now.ToShortDateString());
            dgRaporGun.ItemsSource = d;

            dgRaporAy.ItemsSource = null;
            var m = SatisListesi.Satislar.Where(x => x.Tarih.Month == DateTime.Now.Month && x.Tarih.Year == DateTime.Now.Year);
            dgRaporAy.ItemsSource = m;

            tRaporGunSatilan.Content = d.Sum(x => x.SatilanUrunSayisi);
            tRaporGunKazanc.Content = d.Sum(x => x.ToplamUcret);

            tRaporAySatilan.Content = m.Sum(x => x.SatilanUrunSayisi);
            tRaporAyKazanc.Content = m.Sum(x => x.ToplamUcret);

            if (dpRaporSecimBaslangic.Text != "" && dpRaporSecimSon.Text != "")
            {
                ShowCurrentSelectionSales();
            }

            if (SatisListesi.Satislar.Any((tr) => tr.Tarih.Day == DateTime.Now.Day && tr.Tarih.Month == DateTime.Now.Month && tr.Tarih.Year == DateTime.Now.Year))
            {
                var t = (from tr in SatisListesi.Satislar where (tr.Tarih.Day == DateTime.Now.Day && tr.Tarih.Month == DateTime.Now.Month && tr.Tarih.Year == DateTime.Now.Year) select tr).Max((x) => x.ToplamUcret);
                tRaporIstatistikGunRekor.Content = t;
            }
            else
            {
                tRaporIstatistikGunRekor.Content = ":(";

            }

            
            tRaporIstatistikKayitliUrun.Content = UrunListesi.Urunler.Count;
            tRaporIstatistikSatisSayisi.Content = SatisListesi.Satislar.Count;
            /*
            int[] ss = new int[UrunListesi.Urunler.Count];

            for (int i = 0; i < UrunListesi.Urunler.Count; i++)
            {
                ss[i] = 0;
            }

            for (int i = 0; i < SatisListesi.Satislar.Count; i++)
            {
                for (int j = 0; j < SatisListesi.Satislar[i].SatilanUrunler.Count; j++)
                {
                    int idx = -1;

                    for (int k = 0; k < UrunListesi.Urunler.Count; k++)
                    {
                        if (UrunListesi.Urunler[k].Barkod == SatisListesi.Satislar[i].SatilanUrunler[j].Urun.Barkod)
                        {
                            idx = k;
                            break;
                        }
                    }

                    ss[idx]++;
                }
            }

            MessageBox.Show("W");*/
        }

        private void Rapor_Gun_Ekstra(object sender, MouseButtonEventArgs e)
        {
            if (dgRaporGun.SelectedItem == null)
            {
                e.Handled = true;
                return;
            }

            Satis s = (Satis)dgRaporGun.SelectedItem;

            var q = from x in s.SatilanUrunler
                    select new { x.Urun.Barkod, x.Urun.Ad, x.Adet, x.Fiyat };
            dgRaporGunUrunler.ItemsSource = q;
            tRaporGunDetayUrun.Content = s.SatilanUrunSayisi;
            tRaporGunDetayToplam.Content = s.ToplamUcret;

            gRaporGun.Visibility = Visibility.Hidden;
            gRaporGunDetay.Visibility = Visibility.Visible;
        }

        private void Rapor_Gun_Detay_Geri(object sender, RoutedEventArgs e)
        {
            gRaporGunDetay.Visibility = Visibility.Hidden;
            gRaporGun.Visibility = Visibility.Visible;
        }

        private void Rapor_Ay_Ekstra(object sender, MouseButtonEventArgs e)
        {
            if (dgRaporAy.SelectedItem == null)
            {
                e.Handled = true;
                return;
            }

            Satis st = (Satis)dgRaporAy.SelectedItem;

            var q = from x in st.SatilanUrunler
                    select new { x.Urun.Barkod, x.Urun.Ad, x.Adet, x.Fiyat };

            dgRaporAyUrunler.ItemsSource = q;
            tRaporAyDetayUrun.Content = st.SatilanUrunSayisi;
            tRaporAyDetayToplam.Content = st.ToplamUcret;

            gRaporAyDetay.Visibility = Visibility.Visible;
            gRaporAy.Visibility = Visibility.Hidden;
        }

        private void Rapor_Ay_Geri(object sender, RoutedEventArgs e)
        {
            gRaporAy.Visibility = Visibility.Visible;
            gRaporAyDetay.Visibility = Visibility.Hidden;

        }

        private void Rapor_Secim_Ekstra(object sender, MouseButtonEventArgs e)
        {
            if (dgRaporSecim.SelectedItem == null)
            {
                e.Handled = true;
                return;
            }

            Satis s = (Satis)dgRaporSecim.SelectedItem;

            var q = from x in s.SatilanUrunler
                    select new { x.Urun.Barkod, x.Urun.Ad, x.Adet, x.Fiyat };
            dgRaporSecimUrunler.ItemsSource = q;
            tRaporSecimDetayUrun.Content = s.SatilanUrunSayisi;
            tRaporSecimDetayToplam.Content = s.ToplamUcret;

            gRaporSecim.Visibility = Visibility.Hidden;
            gRaporSecimDetay.Visibility = Visibility.Visible;
        }

        private void Rapor_Secim_Geri(object sender, RoutedEventArgs e)
        {
            gRaporSecim.Visibility = Visibility.Visible;
            gRaporSecimDetay.Visibility = Visibility.Hidden;
        }


        private void Rapor_Secim_Baslangic_Secildi(object sender, SelectionChangedEventArgs e)
        {
            if (dpRaporSecimBaslangic.Text != "" && dpRaporSecimSon.Text != "")
            {
                ShowCurrentSelectionSales();
            }
        }

        private void Rapor_Secim_Son_Secildi(object sender, SelectionChangedEventArgs e)
        {
            if (dpRaporSecimBaslangic.Text != "" && dpRaporSecimSon.Text != "")
            {
                ShowCurrentSelectionSales();
            }
        }

        private void ShowCurrentSelectionSales()
        {
            DateTime d1 = (DateTime)dpRaporSecimBaslangic.SelectedDate;
            DateTime d2 = (DateTime)dpRaporSecimSon.SelectedDate;

            if (d1 > d2)
            {
                var o = d1;
                d1 = d2;
                d2 = o;
            }

            d2 = d2.AddDays(1);
            ObservableCollection<Satis> s = new ObservableCollection<Satis>(SatisListesi.Satislar.Where((sa) =>
            {
                if (sa.Tarih >= d1 && sa.Tarih <= d2)
                {
                    return true;
                }

                return false;
            }));

            tRaporSecimSatilan.Content = s.Sum((x) => x.SatilanUrunSayisi);
            tRaporSecimKazanc.Content = s.Sum((x) => x.ToplamUcret);
            dgRaporSecim.ItemsSource = s;
        }

        private string btud = "";
        private void Urun_Duzenle_BarkodluTeraziUrunuStateChanged(object sender, RoutedEventArgs e)
        {
            bool? a = cbUrunDuzenleBarkodluTerazi.IsChecked;

            if (a == true)
            {
                txUrunDuzenleFiyat.SetValue(TextBoxHelper.WatermarkProperty, "KG Fiyat");
                if (txUrunDuzenleBarkod.Text.Length >= 7)
                {
                    btud = txUrunDuzenleBarkod.Text;
                    txUrunDuzenleBarkod.Text = btu.Substring(0, 6);
                    txUrunDuzenleBarkod.MaxLength = 7;
                }
            }
            else
            {
                txUrunDuzenleFiyat.SetValue(TextBoxHelper.WatermarkProperty, "Fiyat");
                if (txUrunDuzenleBarkod.Text.Length >= 7)
                {
                    txUrunDuzenleBarkod.Text = txUrunDuzenleBarkod.Text + btud.Substring(6);
                    txUrunDuzenleBarkod.MaxLength = 100;
                }
            }
        }

        private async void Urun_Duzenle_BarkodAra(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Urun s = UrunListesi.Urunler.SingleOrDefault((x) => x.Barkod == txUrunDuzenleAraBarkod.Text);

                if (s == null)
                {
                    s = UrunListesi.Urunler.SingleOrDefault((x) => x.Barkod == txUrunDuzenleAraBarkod.Text.Substring(0,6));
                    if (s == null)
                    {
                        /* MessageDialogResult mdr = await this.ShowMessageAsync("Hata", $"{txUrunDuzenleAraBarkod.Text} barkodlu ürün kayıtlı değil eklemek misiniz?", MessageDialogStyle.AffirmativeAndNegative);

                         if (mdr == MessageDialogResult.Affirmative)
                         {

                             txUrunEkleBarkod.Text = txUrunDuzenleAraBarkod.Text;
                             txUrunDuzenleAraBarkod.Text = "";

                             tcTabs.SelectedIndex = 1;
                             tcUrun.SelectedIndex = 0;
                         }

                         return;*/
                    }
                    }

                    uid = UrunListesi.Urunler.IndexOf(s);
                     txUrunDuzenleAd.Text = s.Ad;
                    txUrunDuzenleBarkod.Text = s.Barkod;

                    bool bkt = s.KGFiyat == -1m ? false : true;
                    string f = s.KGFiyat == -1m ? s.Fiyat + "" : s.KGFiyat + "";

                    txUrunDuzenleFiyat.Text = f;
                    cbUrunDuzenleBarkodluTerazi.IsChecked = bkt;

                txUrunDuzenleAraBarkod.Text = "";

                    dgUrunDuzenle.Visibility = Visibility.Hidden;
                    gDuzenle.Visibility = Visibility.Visible;
                    txUrunDuzenleAraBarkod.Visibility = Visibility.Hidden;
                    cbUrunDuzenleHizli.IsChecked = s.HizliEkle;
                
            }
        }
    }
}
