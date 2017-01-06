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
    public class SalesComplexDetViewModel : ModelObject
    {
        string _nota;
        string _site;
        string _userApps;
        string _barcode;
        string _itemID;
        string _variant;
        string _description;
        string _sku;
        int _qty;
        DateTime _salesDate;
        decimal _net;
        decimal _gross;

        JSalesDetail.SalesTypeEnum _Type;
        JSalesDetail.SalesStatusEnum _Status;

        public SalesComplexDetViewModel(JSalesDetail salesDetail)
        {
            this._nota = salesDetail.nota;
            this._site = salesDetail.site;
            this._userApps = salesDetail.userApps;
            this._salesDate = salesDetail.salesdate;
            this._barcode = salesDetail.barcode;
            this._qty = salesDetail.qty;
            this._net = salesDetail.totalamount;
            this._itemID = salesDetail.itemid;
            this._variant = salesDetail.variant;
            this._description = salesDetail.description;
            this._sku = salesDetail.sku;
            this._salesDate = salesDetail.salesdate;
            this._gross = salesDetail.gross;
            this._Type = salesDetail.SalesType;
            this._Status = salesDetail.SalesStatus;
            

        }

        //public SalesViewModel(string _transnota, SiteViewModel _transsite, int _transdate, string _transbrcd, SkuViewModel _sku, decimal _transprice, int _transqty, decimal _transamt, int _transflag)
        //{
        //    this._transnota = _transnota;
        //    this._transsite = _transsite;
        //    this._transdate = Convert.ToDateTime(_transdate.ToString().Substring(0, 4) + "-" + _transdate.ToString().Substring(4, 2) + "-" + _transdate.ToString().Substring(6, 2));
        //    this._transbrcd = _transbrcd;
        //    this._sku = _sku;
        //    this._transprice = _transprice;
        //    this._transqty = _transqty;
        //    this._transamt = _transamt;
        //    if (_transflag == 0)
        //    {
        //        this._transflag = false;
        //    }
        //}

        //public SalesViewModel(SiteViewModel _transsite, int _transdate, string _transbrcd, decimal _transprice, int _transqty, decimal _transamt, int _transflag, decimal _transfinalprice, int _transdiscount)
        //{
        //    //this._transnota = _transnota;
        //    this._transsite = _transsite;
        //    this._transdate = Convert.ToDateTime(_transdate.ToString().Substring(0, 4) + "-" + _transdate.ToString().Substring(4, 2) + "-" + _transdate.ToString().Substring(6, 2));
        //    this._transbrcd = _transbrcd;
        //    this._transprice = _transprice;
        //    this._transqty = _transqty;
        //    this._transamt = _transamt;
        //    if (_transflag == 0)
        //    {
        //        this._transflag = false;
        //    }


        //    //update GAGAN
        //    this._transfinalprice = _transfinalprice;
        //    this._transdiscount = _transdiscount;
        //}

        public string Nota
        {
            get { return _nota; }
            set
            {
                if (_nota != value)
                {
                    _nota = value;
                    RaisePropertyChanged("Nota");
                }
            }
        }
        public int Qty
        {
            get { return _qty; }
            set
            {
                if (_qty != value)
                {
                    _qty = value;
                    RaisePropertyChanged("Qty");
                }
            }
        }

        public string Site
        {
            get { return _site; }
            set
            {
                if (_site != value)
                {
                    _site = value;
                    RaisePropertyChanged("Site");
                }
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    RaisePropertyChanged("Description");
                }
            }
        }

        public string ItemID
        {
            get { return _itemID; }
            set
            {
                if (_itemID != value)
                {
                    _itemID = value;
                    RaisePropertyChanged("ItemID");
                }
            }
        }

        public string SKU
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

        public string Variant
        {
            get { return _variant; }
            set
            {
                if (_variant != value)
                {
                    _variant = value;
                    RaisePropertyChanged("Variant");
                }
            }
        }

        public string User
        {
            get { return _userApps; }
            set
            {
                if (_userApps != value)
                {
                    _userApps = value;
                    RaisePropertyChanged("User");
                }
            }
        }



        public string Barcode
        {
            get { return _barcode; }
            set
            {
                if (_barcode != value)
                {
                    _barcode = value;
                    RaisePropertyChanged("Barcode");
                }
            }
        }


        public DateTime Date
        {
            get { return _salesDate; }
            set
            {
                if (_salesDate != value)
                {
                    _salesDate = value;
                    RaisePropertyChanged("Date");
                }
            }
        }

        public Decimal Net
        {
            get { return _net; }
            set
            {
                if (_net != value)
                {
                    _net = value;
                    RaisePropertyChanged("Net");
                }
            }
        }

        public Decimal Gross
        {
            get { return _gross; }
            set
            {
                if (_gross != value)
                {
                    _gross = value;
                    RaisePropertyChanged("Gross");
                }
            }
        }

        public string Type
        {
            get { return _Type.ToString(); }
            set
            {
                if (_Type.ToString() != value)
                {
                    _Type = (JSalesDetail.SalesTypeEnum)Enum.Parse(typeof(JSalesDetail.SalesTypeEnum), value);
                    RaisePropertyChanged("Type");
                }
            }
        }

        public string Status
        {
            get { return _Status.ToString(); }
            set
            {
                if (_Status.ToString() != value)
                {
                    _Status = (JSalesDetail.SalesStatusEnum)Enum.Parse(typeof(JSalesDetail.SalesStatusEnum), value);
                    RaisePropertyChanged("Status");
                }
            }
        }


    }
}
