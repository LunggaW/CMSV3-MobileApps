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
    public class SalesComplexRepository
    {
        public ObservableCollection<SalesComplexHeaderViewModel> SalesData { get; }
        public ObservableCollection<SalesComplexDetViewModel> SalesDetailData { get; }
        public SalesComplexRepository()
        {

            try
            {
                ObservableCollection<SalesViewModel> listSalesData = new ObservableCollection<SalesViewModel>();
                DSTransaction dstrans = new DSTransaction();
                //DSSkuHeader dssku = new DSSkuHeader();
                DSSite dssite = new DSSite();

                IEnumerable<Transaction> trans = dstrans.GetAll(App.userLogged.userid);
                foreach (Transaction ts in trans)
                {
                    //SkuHeader skuh = dssku.Get(ts.transsku);
                    //SkuViewModel skuview = new SkuViewModel(skuh.skuid, skuh.skuidx, skuh.skusdesc, skuh.skuldesc, skuh.skusdate, skuh.skuedate);
                    Site site = dssite.Get(ts.transsite);
                    SiteViewModel siteview = new SiteViewModel(site.siteid, site.siteclass, site.sitename);

                    SalesViewModel sales = new SalesViewModel(siteview, ts.transdate, ts.transbrcd, ts.transprice, ts.transqty, ts.transamt, ts.transflag, ts.transfinalprice, ts.transdiscount);
                    listSalesData.Add(sales);
                }

                //this.SalesData = listSalesData;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public SalesComplexRepository(string siteid)
        {
            try
            {
                ObservableCollection<SalesViewModel> listSalesData = new ObservableCollection<SalesViewModel>();
                DSTransaction dstrans = new DSTransaction();
                //DSSkuHeader dssku = new DSSkuHeader();
                DSSite dssite = new DSSite();

                IEnumerable<Transaction> trans = dstrans.GetAllBySite(App.userLogged.userid, siteid);
                foreach (Transaction ts in trans)
                {
                    //SkuHeader skuh = dssku.Get(ts.transsku);
                    //SkuViewModel skuview = new SkuViewModel(skuh.skuid, skuh.skuidx, skuh.skusdesc, skuh.skuldesc, skuh.skusdate, skuh.skuedate);
                    Site site = dssite.Get(ts.transsite);
                    SiteViewModel siteview = new SiteViewModel(site.siteid, site.siteclass, site.sitename);

                    SalesViewModel sales = new SalesViewModel(siteview, ts.transdate, ts.transbrcd, ts.transprice, ts.transqty, ts.transamt, ts.transflag, ts.transfinalprice, ts.transdiscount);
                    listSalesData.Add(sales);
                }

                //this.SalesData = listSalesData;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        
        
        
        //for Sales Header 
        public SalesComplexRepository(IEnumerable<JSalesHeader> salesHeaderLists)
        {
            try
            {
                ObservableCollection<SalesComplexHeaderViewModel> listSalesData = new ObservableCollection<SalesComplexHeaderViewModel>();

                foreach (JSalesHeader SalesHeader in salesHeaderLists)
                {
                    SalesComplexHeaderViewModel sales = new SalesComplexHeaderViewModel(SalesHeader);
                    listSalesData.Add(sales);
                }

                //ObservableCollection<SalesViewModel> listSalesData = new ObservableCollection<SalesViewModel>();
                //DSTransaction dstrans = new DSTransaction();
                ////DSSkuHeader dssku = new DSSkuHeader();
                //DSSite dssite = new DSSite();

                //IEnumerable<Transaction> trans = dstrans.GetAllBySite(App.userLogged.userid, siteid);
                //foreach (Transaction ts in trans)
                //{
                //    //SkuHeader skuh = dssku.Get(ts.transsku);
                //    //SkuViewModel skuview = new SkuViewModel(skuh.skuid, skuh.skuidx, skuh.skusdesc, skuh.skuldesc, skuh.skusdate, skuh.skuedate);
                //    Site site = dssite.Get(ts.transsite);
                //    SiteViewModel siteview = new SiteViewModel(site.siteid, site.siteclass, site.sitename);

                //    SalesViewModel sales = new SalesViewModel(siteview, ts.transdate, ts.transbrcd, ts.transprice, ts.transqty, ts.transamt, ts.transflag, ts.transfinalprice, ts.transdiscount);
                //    listSalesData.Add(sales);
                //}

                this.SalesData = listSalesData;
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        //for Sales Detail
        public SalesComplexRepository(IEnumerable<JSalesDetail> salesDetLists)
        {
            try
            {
                ObservableCollection<SalesComplexDetViewModel> listSalesData = new ObservableCollection<SalesComplexDetViewModel>();

                foreach (JSalesDetail SalesDetail in salesDetLists)
                {
                    SalesComplexDetViewModel sales = new SalesComplexDetViewModel(SalesDetail);
                    listSalesData.Add(sales);
                }

                //ObservableCollection<SalesViewModel> listSalesData = new ObservableCollection<SalesViewModel>();
                //DSTransaction dstrans = new DSTransaction();
                ////DSSkuHeader dssku = new DSSkuHeader();
                //DSSite dssite = new DSSite();

                //IEnumerable<Transaction> trans = dstrans.GetAllBySite(App.userLogged.userid, siteid);
                //foreach (Transaction ts in trans)
                //{
                //    //SkuHeader skuh = dssku.Get(ts.transsku);
                //    //SkuViewModel skuview = new SkuViewModel(skuh.skuid, skuh.skuidx, skuh.skusdesc, skuh.skuldesc, skuh.skusdate, skuh.skuedate);
                //    Site site = dssite.Get(ts.transsite);
                //    SiteViewModel siteview = new SiteViewModel(site.siteid, site.siteclass, site.sitename);

                //    SalesViewModel sales = new SalesViewModel(siteview, ts.transdate, ts.transbrcd, ts.transprice, ts.transqty, ts.transamt, ts.transflag, ts.transfinalprice, ts.transdiscount);
                //    listSalesData.Add(sales);
                //}

                this.SalesDetailData = listSalesData;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
