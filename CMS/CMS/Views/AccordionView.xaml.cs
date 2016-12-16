using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CMS.Views
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Xamarin.Forms;

    public partial class AccordionView
    {
        private ObservableCollection<AccordionItem> _items;
        public ICollection<AccordionItem> Items
        {
            get { return _items; }
        }

        public AccordionView()
        {
            _items = new ObservableCollection<AccordionItem>();
            InitializeComponent();
            ItemsSource = _items;
        }

        public void Toggle(AccordionCell selectedItem)
        {
            foreach (var item in _items)
            {
                if (item.Cell != null && item.Cell != selectedItem)
                {
                    item.IsSelected = false;
                }
            }
            selectedItem.IsSelected = true;
        }
    }

    public class AccordionItem : BindableObject
    {
        public string Title { get; set; }
        public string Barcode { get; set; }
        public string Item { get; set; }
        public string Variant { get; set; }
        public string Description { get; set; }
        public string SKU { get; set; }
        public string Qty{ get; set; }
        public string Gross { get; set; }
        public string Net { get; set; }

        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public AccordionCell Cell { get; set; }
    }
}
