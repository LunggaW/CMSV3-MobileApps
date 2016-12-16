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
    public class SalesComplexHeaderViewModel : ModelObject
    {
        string _nota;
        string _site;
        string _user;
        string _barcode;
        int _qty;
        DateTime _date;
        decimal _net;

        JSalesHeader.SalesTypeEnum _Type;
        JSalesHeader.SalesStatusEnum _Status;

        public SalesComplexHeaderViewModel(JSalesHeader salesHeader)
        {
            this._nota = salesHeader.nota;
            this._site = salesHeader.site;
            this._date = salesHeader.date;
            this._net = salesHeader.totalamount;
            this._Type = salesHeader.SalesType;
            this._Status = salesHeader.SalesStatus;
            this._qty = salesHeader.qty;

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

        public string User
        {
            get { return _user; }
            set
            {
                if (_user != value)
                { 
                    _user = value;
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
            get { return _date; }
            set
            {
                if (_date != value)
                {
                    _date = value;
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

        public string Type
        {
            get { return _Type.ToString(); }
            set
            {
                if (_Type.ToString() != value)
                {
                    _Type = (JSalesHeader.SalesTypeEnum)Enum.Parse(typeof(JSalesHeader.SalesTypeEnum), value);
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
                    _Status = (JSalesHeader.SalesStatusEnum)Enum.Parse(typeof(JSalesHeader.SalesStatusEnum), value);
                    RaisePropertyChanged("Status");
                }
            }
        }


    }
}
