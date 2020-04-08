using Evran_Barkod.classes;
using Evran_Barkod.commands;
using Evran_Barkod.derived;
using Evran_Barkod.models;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Evran_Barkod.viewmodels
{
    class SatisViewModel : INotifyPropertyChanged
    {
        private SatisModel model;

        private ObservableCollection<Urun> _hizliEkle;
        
        public ObservableCollection<Urun> HizliEkle
        {
            get
            {
                return _hizliEkle;
            } set
            {
                _hizliEkle = value;
                InvokePropertyChanged(nameof(HizliEkle));
            }
        }
        
        private TrulyObservableCollection<SatisUrun> _satisListesi;
        private decimal _alinanNakitB;
        public TrulyObservableCollection<SatisUrun> SatisListesi
        {
            get { return _satisListesi; }
            set
            {
                _satisListesi = value;
                InvokePropertyChanged(nameof(SatisListesi));

                SatisListesi.CollectionChanged += SatisListesi_CollectionChanged;
            }
        }

        private void SatisListesi_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            InvokePropertyChanged(nameof(ToplamUcret));
            InvokePropertyChanged(nameof(ToplamUcretTL));
            InvokePropertyChanged(nameof(IkiYuzTLUstu));
            InvokePropertyChanged(nameof(YuzTLUstu));
            InvokePropertyChanged(nameof(ElliTLUstu));
            InvokePropertyChanged(nameof(YirmiTLUstu));
            InvokePropertyChanged(nameof(AlinanNakitUstu));
        }

        private TrulyObservableCollection<SatisUrun> _satisListesiB;

        private SatisUrun _seciliUrun;

        public SatisUrun SeciliUrun
        {
            get { return _seciliUrun; }
            set
            {
                _seciliUrun = value;
                InvokePropertyChanged(nameof(SeciliUrun));
            }
        }

        public bool CanExecuteUrunKaldir ()
        {
            if (SeciliUrun == null)
            {
                return false;
            } else
            {
                return true;
            }
        }

        private void urunKaldir()
        {
            SatisListesi.Remove(SeciliUrun);
        }

        private ICommand _urunKaldir;
        public ICommand UrunKaldir
        {
            get
            {
                if (_urunKaldir == null)
                {
                    _urunKaldir = new RelayCommand(
                        param => urunKaldir(),
                        param => CanExecuteUrunKaldir()
                    );
                }
                return _urunKaldir;
            }
        }



        private void urunAdet(int ii)
        {
            if (Adet + ii < 1)
            {
                return;
            }

            Adet += ii;
        }

        private ICommand _urunSayisiArttir;
        public ICommand UrunSayisiArttir
        {
            get
            {
                if (_urunSayisiArttir == null)
                {
                    _urunSayisiArttir = new RelayCommand(
                        param => urunAdet(1),
                        param => model.CanDo()
                    );
                }
                return _urunSayisiArttir;
            }
        }

        private ICommand _urunSayisiAzalt;
        public ICommand UrunSayisiAzalt
        {
            get
            {
                if (_urunSayisiAzalt == null)
                {
                    _urunSayisiAzalt = new RelayCommand(
                        param => urunAdet(-1),
                        param => model.CanDo()
                    );
                }
                return _urunSayisiAzalt;
            }
        }

        private IDialogCoordinator dialogCoordinator;
        private MetroTabControl tcontrol;
        private MainWindow mwindow;
        public SatisViewModel(IDialogCoordinator instance, MainWindow mw)
        {
            model = new SatisModel();
            SatisListesi = new TrulyObservableCollection<SatisUrun>();
            _satisListesiB = new TrulyObservableCollection<SatisUrun>();
            _alinanNakitB = 0m;
            dialogCoordinator = instance;
            tcontrol = mw.tcTabs;
            mwindow = mw;

            HizliEkle = new ObservableCollection<Urun>(UrunListesi.Urunler.Where((x) => x.HizliEkle == true));

            UrunListesi.Urunler.CollectionChanged += (s, e) => HizliEkle = new ObservableCollection<Urun>(UrunListesi.Urunler.Where((x) => x.HizliEkle == true));

        }

        private decimal _alinanNakit;

        public decimal AlinanNakit
        {
            get { return _alinanNakit; }
            set
            {
                _alinanNakit = value;
                InvokePropertyChanged(nameof(AlinanNakit));
                InvokePropertyChanged(nameof(AlinanNakitUstu));
            }
        }

        public string AlinanNakitUstu
        {
            get
            {
                if (ToplamUcret != 0)
                {
                    if (AlinanNakit - ToplamUcret > 0)
                    {
                        return Math.Round((AlinanNakit - ToplamUcret), 2) + " ₺";
                    }
                }

                return "- - -";
            }
        }


        public string Barkod
        {
            get { return model.Barkod; }
            set
            {
                if (model.Barkod != value)
                {
                    if (!model.IsDigitsOnly(value))
                    {
                        return;
                    }

                    model.Barkod = value;
                    InvokePropertyChanged("Barkod");
                }
            }
        }

        public int Adet
        {
            get { return model.Adet; }
            set
            {
                if (model.Adet != value)
                {
                    model.Adet = value;
                    InvokePropertyChanged(nameof(Adet));
                }
            }
        }

        public bool MusteriBekletB
        {
            get { return model.MusteriBeklet; }
            set
            {
                if (model.MusteriBeklet != value)
                {
                    model.MusteriBeklet = value;

                    InvokePropertyChanged(nameof(MusteriBekletB));
                    InvokePropertyChanged(nameof(MusteriBekletText));
                }
            }
        }

        public string MusteriBekletText
        {
            get
            {
                if (MusteriBekletB == true)
                {
                    return "Geri Dön";
                } else
                {
                    return "Müşteri Beklet";
                }
            }
        }

        public bool CanExecuteMusteriBeklet()
        {
            if (SatisListesi.Count == 0 && _satisListesiB.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public decimal ToplamUcret
        {
            get
            {
                if (_satisListesi.Count > 0)
                {
                    decimal d = _satisListesi.Sum((x) => x.Fiyat);
                    char lc = d.ToString().Last();

                    switch (lc)
                    {
                        case '9':
                            d += 0.01m;
                            break;
                        case '8':
                            d += 0.02m;
                            break;
                        case '7':
                            d -= 0.02m;
                            break;
                        case '6':
                            d -= 0.01m;
                            break;
                        case '4':
                            d += 0.01m;
                            break;
                        case '3':
                            d += 0.02m;
                            break;
                        case '2':
                            d -= 0.02m;
                            break;
                        case '1':
                            d -= 0.01m;
                            break;
                        default:
                            break;
                    }

                    return d;
                }

                return 0m;
            }
        }

        public string IkiYuzTLUstu
        {
            get
            {
                if (ToplamUcret != 0)
                {
                    if (200m - ToplamUcret > 0)
                    {
                        return 200m - ToplamUcret + " ₺";
                    }
                }

                return "- - -";
            }
        }


        public string YuzTLUstu
        {
            get
            {
                if (ToplamUcret != 0)
                {
                    if (100m - ToplamUcret > 0)
                    {
                        return 100m - ToplamUcret + " ₺";
                    }
                }

                return "- - -";
            }
        }

        public string ElliTLUstu
        {
            get
            {
                if (ToplamUcret != 0)
                {
                    if (50m - ToplamUcret > 0)
                    {
                        return 50m - ToplamUcret + " ₺";
                    }
                }

                return "- - -";
            }
        }

        public string YirmiTLUstu
        {
            get
            {
                if (ToplamUcret != 0)
                {
                    if (20 - ToplamUcret > 0)
                    {
                        return 20m - ToplamUcret + " ₺";
                    }
                }

                return "- - -";
            }
        }

        public string ToplamUcretTL {
            get
            {
                if (ToplamUcret != 0)
                {
                    return ToplamUcret + "₺";
                }

                return "- - - - - -";
            }
        }

        private void musteriBeklet()
        {
            MusteriBekletB = !MusteriBekletB;
            object o = SatisListesi;
            SatisListesi = _satisListesiB;
            _satisListesiB = (TrulyObservableCollection<SatisUrun>)o;

            object p = AlinanNakit;
            AlinanNakit = _alinanNakitB;
            _alinanNakitB = (decimal)p;

            InvokePropertyChanged(nameof(ToplamUcret));
            InvokePropertyChanged(nameof(ToplamUcretTL));
            InvokePropertyChanged(nameof(IkiYuzTLUstu));
            InvokePropertyChanged(nameof(YuzTLUstu));
            InvokePropertyChanged(nameof(ElliTLUstu));
            InvokePropertyChanged(nameof(YirmiTLUstu));
            InvokePropertyChanged(nameof(AlinanNakitUstu));
        }

        private ICommand _musteriBeklet;
        public ICommand MusteriBeklet
        {
            get
            {
                if (_musteriBeklet == null)
                {
                    _musteriBeklet = new RelayCommand(
                        param => musteriBeklet(),
                        param => CanExecuteMusteriBeklet()
                    );
                }
                return _musteriBeklet;
            }
        }



        private ICommand _nothingCommand;
        public ICommand NothingCommand
        {
            get
            {
                if (_nothingCommand == null)
                {
                    _nothingCommand = new RelayCommand(
                        param => model.Nothing(),
                        param => model.CanDo()
                    );
                }
                return _nothingCommand;
            }
        }

        public bool CanExecuteEkle()
        {
            if (Barkod.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool CanExecuteSat()
        {
            return SatisListesi.Count > 0;
        }

        private void sat()
        {
            Satis s = new Satis(JsonConvert.SerializeObject(SatisListesi)) { ToplamUcret = ToplamUcret};
            Evran_Barkod.classes.SatisListesi.Satislar.Add(s);
            AlinanNakit = 0m;
            SatisListesi.Clear();
            Barkod = "";
            Adet = 1;
        }

        private ICommand _sat;
        public ICommand SatCommand
        {
            get
            {
                if (_sat == null)
                {
                    _sat = new RelayCommand(
                        param => sat(),
                        param => CanExecuteSat()
                    );
                }
                return _sat;
            }
        }

        private async void ekleAsync(string Barkod = "")
        {

            if (Barkod == "")
            {
                Barkod = this.Barkod;
            }


            SatisUrun a = SatisListesi.SingleOrDefault((x) => x.Urun.Barkod == Barkod);


            if (a != null)
            {
                a.Adet+=Adet;
                this.Barkod = "";
                Adet = 1;
                InvokePropertyChanged(nameof(ToplamUcret));
                InvokePropertyChanged(nameof(ToplamUcretTL));
                InvokePropertyChanged(nameof(IkiYuzTLUstu));
                InvokePropertyChanged(nameof(YuzTLUstu));
                InvokePropertyChanged(nameof(ElliTLUstu));
                InvokePropertyChanged(nameof(YirmiTLUstu));
                InvokePropertyChanged(nameof(AlinanNakitUstu));
                return;
            }

            if (a == null && Barkod.Length == 12)
            {
                a = SatisListesi.SingleOrDefault((x) =>
                {
                    if (x.Urun.Barkod.Length != 7)
                    {
                        return false;
                    }

                    if (x.Urun.Barkod == Barkod.Substring(0, 6))
                    {
                        if (x.Urun.Fiyat == -1)
                        {
                            return true;
                        }
                    }

                    return false;
                });

                if (a != null)
                {
                    decimal f = decimal.Parse(Barkod.Substring(6, 5)) / 100m;
                    a.Adet += 1;
                    a.ChangeFiyat(a.Fiyat + f);
                    this.Barkod = "";
                    Adet = 1;
                    InvokePropertyChanged(nameof(ToplamUcret));
                    InvokePropertyChanged(nameof(ToplamUcretTL));
                    InvokePropertyChanged(nameof(IkiYuzTLUstu));
                    InvokePropertyChanged(nameof(YuzTLUstu));
                    InvokePropertyChanged(nameof(ElliTLUstu));
                    InvokePropertyChanged(nameof(YirmiTLUstu));
                    InvokePropertyChanged(nameof(AlinanNakitUstu));
                    return;
                }
            }

            Urun u = UrunListesi.Urunler.SingleOrDefault((x) => x.Barkod == Barkod);
            
            if (u == null && Barkod.Length == 12)
            {
                u = UrunListesi.Urunler.SingleOrDefault((x) =>
                {
                    if (x.Barkod.Length != 6)
                    {
                        return false;
                    }
                    if (x.Barkod == Barkod.Substring(0, 6))
                    {
                        if (x.Fiyat == -1)
                        {
                            return true;
                        }
                    }

                    return false;
                });
            }

            if (u == null)
            {
                MessageDialogResult mdr = await dialogCoordinator.ShowMessageAsync(this, "Hata!", "Ürün kayıtlı değil!\n Yeni ürün eklemek ister misiniz?", MessageDialogStyle.AffirmativeAndNegative);

                if (mdr == MessageDialogResult.Affirmative)
                {
                    mwindow.txUrunEkleBarkod.Text = Barkod;
                    tcontrol.SelectedIndex = 1;
                    mwindow.txUrunEkleAd.Focus();
                }

                this.Barkod = "";
                Adet = 1;
                return;
            }

            if (u.KGFiyat != -1 && Barkod.Length != 12)
            {
                MessageDialogResult mdr = await dialogCoordinator.ShowMessageAsync(this, "Hata!", "Barkodlu Terazi ürünü fakat fiyat bilgisi okunmadı!", MessageDialogStyle.Affirmative);
                this.Barkod = "";
                Adet = 1;
                return;
            }


            SatisUrun s;
            if (u.KGFiyat == -1)
            {
                s = new SatisUrun() { Urun = u, Adet = Adet };
            } else
            {
                decimal f = decimal.Parse(Barkod.Substring(6, 5)) / 100m;
                s = new SatisUrun(f, u);
            }


            SatisListesi.Add(s);
            this.Barkod = "";
            Adet = 1;
            InvokePropertyChanged(nameof(ToplamUcret));
            InvokePropertyChanged(nameof(ToplamUcretTL));
            InvokePropertyChanged(nameof(IkiYuzTLUstu));
            InvokePropertyChanged(nameof(YuzTLUstu));
            InvokePropertyChanged(nameof(ElliTLUstu));
            InvokePropertyChanged(nameof(YirmiTLUstu));
            InvokePropertyChanged(nameof(AlinanNakitUstu));
        }

        private ICommand _ekleCommand;
        public ICommand Ekle
        {
            get
            {
                if (_ekleCommand == null)
                {
                    _ekleCommand = new RelayCommand(
                        param => ekleAsync(),
                        param => CanExecuteEkle()
                    );
                }
                return _ekleCommand;
            }
        }


        private ICommand _hotbardan;
        public ICommand HotbardanEkle
        {
            get
            {
                if (_hotbardan == null)
                {
                    _hotbardan = new HotbardanEkleCommand((x) => ekleAsync(x));
                }
                return _hotbardan;
            }
        }

        public ICommand UrunKaldir1 { get => _urunKaldir; set => _urunKaldir = value; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void InvokePropertyChanged(string propertyName)
        {
            var e = new PropertyChangedEventArgs(propertyName);
            PropertyChanged?.Invoke(this, e);
        }
    }
    }
