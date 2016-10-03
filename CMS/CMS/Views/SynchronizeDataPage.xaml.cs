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
    public partial class SynchronizeDataPage : ContentPage
    {
        public SynchronizeDataPage()
        {
            InitializeComponent();
            ThemeManager.ThemeName = Themes.Light;
            SalesRepository model;

            DSTransaction dstrans = new DSTransaction();
            btnSync.Text = "Sync Data (" + dstrans.HasSales(App.userLogged.userid).ToString()+ ")";

            DSSite dssite = new DSSite();

            IEnumerable<SiteList> SiteLists = dssite.getList(App.userLogged.userprofsiteid);
            if (SiteLists.Count() > 0)
            {
                SiteSelection.ItemsSource = SiteLists;
                SiteSelection.SelectedIndex = 0;
                model = new SalesRepository(SiteSelection.SelectedValue.ToString());
            }
            else
            {
                model = new SalesRepository();
            }
            BindingContext = model;
        }
        void SiteSelectionIndexChanged(object sender, EventArgs e)
        {
            if (SiteSelection.SelectedIndex != -1)
            {
                SalesRepository model = new SalesRepository(SiteSelection.SelectedValue.ToString());
                BindingContext = model;
                int totalnotsync = model.SalesData.Where(d => d.Synced == false).Count();
                btnSync.Text = "Sync Data (" + totalnotsync.ToString() + ")";
            }
        }

        async void OnBtnSyncClicked(object sender, EventArgs e)
        {
            bool isconnected = await CrossConnectivity.Current.IsRemoteReachable(App.hostname, App.port, 5000);
            if (isconnected)
            {
                User user = App.userLogged;
                DSTransaction dstrans = new DSTransaction();

                List<Transaction> tobesync = dstrans.GetTobeSync(user.userid, 0);
                if (tobesync.Count() > 0)
                {
                    ServiceWrapper serviceWrapper = new ServiceWrapper();
                    bool syncstat = await serviceWrapper.UploadSales(user, tobesync);
                    if (syncstat == false)
                    {
                        await DisplayAlert("Error", "Cannot sync data!", "OK");
                    }
                    else
                    {
                        SalesRepository model = new SalesRepository();
                        BindingContext = model;
                        int totalnotsync = model.SalesData.Where(d => d.Synced == false).Count();
                        btnSync.Text = "Sync Data (" + totalnotsync.ToString() + ")";
                        await DisplayAlert("Success", "Transaction data succesfully synced. Total = " + tobesync.Count().ToString(), "OK");
                    }
                }
            }else
            {
                await DisplayAlert("Error", "Cannot sync data. Please check your internet connection.", "OK");
            }
        }
    }
}
