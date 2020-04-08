using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evran_Barkod.classes
{
    class SatisUrun : INotifyPropertyChanged
    {
        public Urun Urun{ get; set; }
        private int _adet;

        public int Adet
        {
            get { return _adet; }
            set
            {
                if (Urun.KGFiyat == -1)
                {
 _adet = value;
                } else
                {
                    _fiyat = (Urun.KGFiyat / 1000m) * value;
                    _adet = value;
                }
               
                InvokePropertyChanged(nameof(Adet));
                InvokePropertyChanged(nameof(Fiyat));
            }
        }

 
        private decimal _fiyat;

        public event PropertyChangedEventHandler PropertyChanged;
        private void InvokePropertyChanged(string propertyName)
        {
            var e = new PropertyChangedEventArgs(propertyName);
            PropertyChanged?.Invoke(this, e);
        }

        public decimal Fiyat {
            get
            {
                if (Urun.KGFiyat == -1)
                {
                    return Urun.Fiyat * Adet;
                } else
                {
                    return Math.Round((Urun.KGFiyat / 1000m) * _adet, 2);
                }
            }
        }

        public SatisUrun()
        {
        }

        public SatisUrun(decimal fiyat, Urun u)
        {
            _fiyat = fiyat;
            Urun = u;
            Adet = Convert.ToInt32((1000m * fiyat) / Urun.KGFiyat);
            InvokePropertyChanged(nameof(Adet));
            InvokePropertyChanged(nameof(Fiyat));
        }

        public void ChangeFiyat(decimal fiyat)
        {
            _fiyat = fiyat;
            _adet = Convert.ToInt32((1000m * fiyat) / Urun.KGFiyat);// 1000 * 10      / 5
            InvokePropertyChanged(nameof(Adet));
            InvokePropertyChanged(nameof(Fiyat));
        }
    }
}
