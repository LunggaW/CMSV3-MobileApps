using CMS.DataSource;
using CMS.Models;
using CMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mobile.DataGrid.Theme;

using Xamarin.Forms;
using Plugin.Connectivity;

namespace CMS.Views
{
    public partial class ViewSalesDetailPage : ContentPage
    {
        public ViewSalesDetailPage(IEnumerable<JSalesDetail> SalesDetailTemp, string Nota)
        {
            


            InitializeComponent();
            ThemeManager.ThemeName = Themes.Light;
            SalesComplexRepository model;

            //DSTransaction dstrans = new DSTransaction();
            //btnSync.Text = "Sync Data (" + dstrans.HasSales(App.userLogged.userid).ToString()+ ")";

            //DSSite dssite = new DSSite();

            //IEnumerable<SiteList> SiteLists = dssite.getList(App.userLogged.userprofsiteid);
            //if (SiteLists.Count() > 0)
            //{
            //    SiteSelection.ItemsSource = SiteLists;
            //    SiteSelection.SelectedIndex = 0;
            //    model = new SalesRepository(SiteSelection.SelectedValue.ToString());
            //}
            //else
            //{
            //    model = new SalesComplexRepository();
            //}

            model = new SalesComplexRepository(SalesDetailTemp);

            BindingContext = model;

            label1.Text = "Site : " + App.salessite + "  Sales Date : " + App.salesdate.ToString("dd-MMM-yyyy")+"  Nota : " + Nota;
        }
        void SiteSelectionIndexChanged(object sender, EventArgs e)
        {
            //if (SiteSelection.SelectedIndex != -1)
            //{
            //    SalesRepository model = new SalesRepository(SiteSelection.SelectedValue.ToString());
            //    BindingContext = model;
            //    int totalnotsync = model.SalesData.Where(d => d.Synced == false).Count();
            //    btnSync.Text = "Sync Data (" + totalnotsync.ToString() + ")";
            //}
        }

        async void OnBtnSyncClicked(object sender, EventArgs e)
        {
            
        }
    }
}
