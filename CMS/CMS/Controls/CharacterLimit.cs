using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Controls
{
    public class CharacterLimit : INotifyPropertyChanged
    {
        private int _amountPercent;
        private decimal _amountNormalPrice;
        private decimal _amountFinalPrice;
        private int _amountQty;
        private string _lengthBarcode;

        public int AmountPercent
        {
            get { return _amountPercent; }

            set
            {
                if (value < 100)
                {
                    _amountPercent = value;
                }
                OnPropertyChanged();
            }
        }

        public decimal AmountNormalPrice
        {
            get { return _amountNormalPrice; }

            set
            {
                if (value < 1000000000)
                {
                    _amountNormalPrice = value;
                }
                OnPropertyChanged();
            }
        }

        public decimal AmountFinalPrice
        {
            get { return _amountFinalPrice; }

            set
            {
                if (value < 1000000000)
                {
                    _amountFinalPrice = value;
                }
                OnPropertyChanged();
            }
        }

        public int AmountQty
        {
            get { return _amountQty; }

            set
            {
                if (value < 10000)
                {
                    _amountQty = value;
                }
                OnPropertyChanged();
            }
        }

        public string BarcodeLength
        {
            get { return _lengthBarcode; }

            set
            {
                if (value.Length < 21)
                {
                    _lengthBarcode = value;
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
