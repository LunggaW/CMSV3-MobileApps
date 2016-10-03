using CMS.DataSource;
using CMS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.ViewModels
{
    public class SalesViewModel: ModelObject
    {
        string _transnota;
        SiteViewModel _transsite;
        DateTime _transdate;
        string _transbrcd;
        SkuViewModel _sku;
        decimal _transprice;
        int _transqty;
        decimal _transamt;
        bool _transflag = true;

        public SalesViewModel(string _transnota, SiteViewModel _transsite, int _transdate, string _transbrcd, SkuViewModel _sku, decimal _transprice, int _transqty, decimal _transamt, int _transflag)
        {
            this._transnota = _transnota;
            this._transsite = _transsite;
            this._transdate = Convert.ToDateTime(_transdate.ToString().Substring(0, 4) + "-" + _transdate.ToString().Substring(4, 2) + "-" + _transdate.ToString().Substring(6, 2));
            this._transbrcd = _transbrcd;
            this._sku = _sku;
            this._transprice = _transprice;
            this._transqty = _transqty;
            this._transamt = _transamt;
            if(_transflag == 0)
            {
                this._transflag = false;
            }
        }

        public string Nota
        {
            get { return _transnota; }
            set
            {
                if (_transnota != value)
                {
                    _transnota = value;
                    RaisePropertyChanged("Nota");
                }
            }
        }

        public SiteViewModel Site
        {
            get { return _transsite; }
            set
            {
                if (_transsite != value)
                {
                    _transsite = value;
                    RaisePropertyChanged("Site");
                }
            }
        }

        public DateTime Date
        {
            get { return _transdate; }
            set
            {
                if (_transdate != value)
                {
                    _transdate = value;
                    RaisePropertyChanged("Date");
                }
            }
        }


        public string Barcode
        {
            get { return _transbrcd; }
            set
            {
                if (_transbrcd != value)
                {
                    _transbrcd = value;
                    RaisePropertyChanged("Barcode");
                }
            }
        }
        public SkuViewModel SKU
        {
            get { return _sku; }
            set
            {
                if (_sku != value)
                {
                    _sku = value;
                    RaisePropertyChanged("SKU");
                }
            }
        }
        public int QTY
        {
            get { return _transqty; }
            set
            {
                if (_transqty != value)
                {
                    _transqty = value;
                    RaisePropertyChanged("QTY");
                }
            }
        }
        public decimal Price
        {
            get { return _transprice; }
            set
            {
                if (_transprice != value)
                {
                    _transprice = value;
                    RaisePropertyChanged("Price");
                }
            }
        }
        public decimal Amount
        {
            get { return _transamt; }
            set
            {
                if (_transamt != value)
                {
                    _transamt = value;
                    RaisePropertyChanged("Amount");
                }
            }
        }
        public bool Synced
        {
            get { return _transflag; }
            set
            {
                if (_transflag != value)
                {
                    _transflag = value;
                    RaisePropertyChanged("Synced");
                }
            }
        }
    }
}
