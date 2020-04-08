using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Evran_Barkod.commands
{
    class HotbardanEkleCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action<string> mfunc;
        public HotbardanEkleCommand(Action<string> ekle)
        {
            mfunc = ekle;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                mfunc.Invoke((string)parameter);
            }
        }
    }
}
