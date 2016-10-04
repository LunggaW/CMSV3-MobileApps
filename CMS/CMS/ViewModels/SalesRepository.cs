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
    public class SalesRepository
    {
        public ObservableCollection<SalesViewModel> SalesData { get; }
        public SalesRepository()
        {
            //ObservableCollection<SalesViewModel> listSalesData = new ObservableCollection<SalesViewModel>();
            //DSTransaction dstrans = new DSTransaction();
            //DSSkuHeader dssku = new DSSkuHeader();
            //DSSite dssite = new DSSite();

            //IEnumerable<Transaction> trans = dstrans.GetAll(App.userLogged.userid);
            //foreach (Transaction ts in trans)
            //{
            //    SkuHeader skuh = dssku.Get(ts.transsku);
            //    SkuViewModel skuview = new SkuViewModel(skuh.skuid, skuh.skuidx, skuh.skusdesc, skuh.skuldesc, skuh.skusdate, skuh.skuedate);
            //    Site site = dssite.Get(ts.transsite);
            //    SiteViewModel siteview = new SiteViewModel(site.siteid, site.siteclass, site.sitename);

            //    SalesViewModel sales = new SalesViewModel(ts.transnota, siteview, ts.transdate, ts.transbrcd, skuview, ts.transprice, ts.transqty, ts.transamt, ts.transflag);
            //    listSalesData.Add(sales);
            //}

            //this.SalesData = listSalesData;

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

                this.SalesData = listSalesData;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public SalesRepository(string siteid)
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

                this.SalesData = listSalesData;
            }
            catch (Exception ex)
            {
                    
                throw;
            }
           
        }
    }
}
