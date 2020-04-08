using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Evran_Barkod.models
{
    class SatisModel
    {
        private int _adet;
        public int Adet
        {
            get
            {
                return _adet;
            }
            set
            {
                _adet = value;
            }
        }

        private string _barkod;
        public string Barkod {
            get {
                return _barkod;
            }
            set
            {
                _barkod = value;
            }
        }

        private bool _musteriBeklet;
        public bool MusteriBeklet
        {
            get
            {
                return _musteriBeklet;
            }
            set
            {
                _musteriBeklet = value;
            }
        }

        public SatisModel()
        {
            Barkod = "";
            _adet = 1;
            _musteriBeklet = false;
        }

        public bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        public bool CanDo()
        {
            return true;
        }

        public void Nothing()
        {
            MessageBox.Show("o");
            return;
        }


        public void Ekle()
        {
            MessageBox.Show("i");
            return;
        }
    }
}
