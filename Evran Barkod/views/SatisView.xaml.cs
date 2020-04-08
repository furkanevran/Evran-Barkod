using Evran_Barkod.viewmodels;
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
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;

namespace Evran_Barkod.views
{
    /// <summary>
    /// Interaction logic for SatisView.xaml
    /// </summary>
    public partial class SatisView : UserControl
    {
        UIElement lastFocus;
        public SatisView(MainWindow mw)
        {
            InitializeComponent();
            SatisViewModel svm = new SatisViewModel(DialogCoordinator.Instance, mw);
            DataContext = svm;
        }
        /*
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
        */
        private void FocusBarcode(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).IsEnabled)
            {
                FocusBarcode();
            }
        }

        private void FocusBarcode()
        {
            txBarcode.Focusable = true;
            Keyboard.Focus(txBarcode);
        }

        private void StartBussiness(object sender, RoutedEventArgs e)
        {
            this.Focusable = true;
            Keyboard.Focus(this);
            FocusBarcode();
            lastFocus = txBarcode;
        }
        /*
        private void FocusOnAlinanNakit(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Q)
            {
                txAlinanNakit.Focusable = true;
                Keyboard.Focus(txAlinanNakit);
            }
        }

        private void FocusOnBarcodeOrSatis(object sender, KeyEventArgs e)
        {
            if (!(isKeyNumeric(e.Key) || e.Key == Key.OemComma || e.Key == Key.Delete || e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Back || e.Key == Key.Tab))
            {
                e.Handled = true;
            }

          

            if (e.Key == Key.Q)
            {
                txBarcode.Focusable = true;
                Keyboard.Focus(txBarcode);
            }

            if (e.Key == Key.Enter)
            {
                btnSatis.Focusable = true;
                Keyboard.Focus(btnSatis);
            }
        }*/

        private void FocusOnBarcodeAfterE(object sender, KeyEventArgs e)
        {
            if (((Button)sender).IsEnabled)
            {
                if (e.Key == Key.Enter)
                {
                    txBarcode.Focusable = true;
                    Keyboard.Focus(txBarcode);
                }
            }
        }

        private void LVFocusBarcode(object sender, RoutedEventArgs e)
        {
            lastFocus.Focusable = true;
            Keyboard.Focus(lastFocus);
        }

        
        private void UpdateLastFocused(object sender, RoutedEventArgs e)
        {
            lastFocus = (UIElement)sender;
        }

        private void FocusBarcodeAfterTab(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                FocusBarcode();
            }
        }
    }
}
