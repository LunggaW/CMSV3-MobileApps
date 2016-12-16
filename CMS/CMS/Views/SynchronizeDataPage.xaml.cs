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
            bar.Progress = .1;
            bool isconnected = await CrossConnectivity.Current.IsRemoteReachable(App.hostname, App.port, 5000);
            if (isconnected)
            {
                bar.Progress = .3;
                User user = App.userLogged;
                DSTransaction dstrans = new DSTransaction();

                //Simple
                List<Transaction> tobesync = dstrans.GetTobeSyncSimple(user.userid, 0);
                bar.Progress = .4;
                if (tobesync.Count() > 0)
                {
                    ServiceWrapper serviceWrapper = new ServiceWrapper();
                    bool syncstat = await serviceWrapper.UploadSales(user, tobesync);
                    bar.Progress = .7;
                    if (syncstat == false)
                    {
                        await DisplayAlert("Error", "Cannot sync data!", "OK");
                        bar.Progress = .8;
                    }
                    else
                    {
                        bar.Progress = .9;
                        SalesRepository model = new SalesRepository();
                        BindingContext = model;
                        int totalnotsync = model.SalesData.Where(d => d.Synced == false).Count();
                        btnSync.Text = "Sync Data (" + totalnotsync.ToString() + ")";
                        await DisplayAlert("Success", "Simple sales transaction data succesfully synced. Total = " + tobesync.Count().ToString(), "OK");
                        bar.Progress = 1;
                        bar.IsVisible = false;
                    }
                }

                //Complex
                 tobesync = dstrans.GetTobeSyncComplex(user.userid, 0);
                bar.Progress = .4;
                if (tobesync.Count() > 0)
                {
                    ServiceWrapper serviceWrapper = new ServiceWrapper();
                    bool syncstat = await serviceWrapper.UploadComplexSales(user, tobesync);
                    bar.Progress = .7;
                    if (syncstat == false)
                    {
                        await DisplayAlert("Error", "Cannot sync data!", "OK");
                        bar.Progress = .8;
                    }
                    else
                    {
                        bar.Progress = .9;
                        SalesRepository model = new SalesRepository();
                        BindingContext = model;
                        int totalnotsync = model.SalesData.Where(d => d.Synced == false).Count();
                        btnSync.Text = "Sync Data (" + totalnotsync.ToString() + ")";
                        await DisplayAlert("Success", "Sales transaction data succesfully synced. Total = " + tobesync.Count().ToString(), "OK");
                        bar.Progress = 1;
                        bar.IsVisible = false;
                    }
                }
            }
            else
            {
                bar.Progress = .9;
                await DisplayAlert("Error", "Cannot sync data. Please check your internet connection.", "OK");
                bar.Progress = 1;
                bar.IsVisible = false;
            }
        }
    }
}
