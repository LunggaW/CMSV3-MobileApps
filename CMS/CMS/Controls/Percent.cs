using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Controls
{
    public class Percent : INotifyPropertyChanged
    {
        private int _amountPercent;

        public int AmountPercent
        {
            get { return _amountPercent; }

            set
            {
                if (value <= 100)
                {
                    _amountPercent = value;
                }
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
