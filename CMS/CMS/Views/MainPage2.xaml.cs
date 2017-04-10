using CMS.DataSource;
using CMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Models;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace CMS.Views
{
    public partial class MainPage2 : ContentPage
    {

        private bool isconnected;
        private ServiceWrapper serviceWrapper;

        public MainPage2()
        {
            if (App.IsUserLoggedIn)
            {
               // lblWelcome.Text = lblWelcome.Text + " " + App.userLogged.username + "!";
                InitializeComponent();

                DSSite dssite = new DSSite();

                IEnumerable<SiteList> SiteLists = dssite.getList(App.userLogged.userid);
                if (SiteLists.Count() > 0)
                {
                    SiteSelection.ItemsSource = SiteLists;
                    SiteSelection.SelectedIndex = 0;
                }
                SalesDate.MinimumDate = DateTime.Today.AddDays(-30);

            }
            else
            {
                Navigation.PushAsync(new LoginPage());
            }
            
        }
       

        //async void OnSalesReturnButtonClicked(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrWhiteSpace(SiteSelection.SelectedValue.ToString()) && !string.IsNullOrWhiteSpace(SalesDate.Date.ToString()))
        //    {
        //        App.salessite = SiteSelection.SelectedValue.ToString();
        //        App.salesdate = SalesDate.Date;
        //        await Navigation.PushAsync(new SalesReturnPage());
        //    }
        //    else
        //    {
        //        await DisplayAlert("Alert", "Please select the site and sales date!", "OK");
        //    }
        //}

        async void OnSimpleSalesButtonClicked(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(SiteSelection.SelectedValue.ToString()) && !string.IsNullOrWhiteSpace(SalesDate.Date.ToString()))
            {
                App.salessite = SiteSelection.SelectedValue.ToString();
                App.salesdate = SalesDate.Date;
                await Navigation.PushAsync(new MainSimpleSalesPage());
            }
            else
            {
                await DisplayAlert("Alert", "Please select the site and sales date!", "OK");
            }
        }
        async void OnSynchronizeButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SynchronizeDataPage());
        }
        async void OnAboutButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Abouts());
        }


        async void Button_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChangePasswordPage());
        }

        async void OnComplexSalesButtonClicked(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(SiteSelection.SelectedValue.ToString()) && !string.IsNullOrWhiteSpace(SalesDate.Date.ToString()))
            {
                App.salessite = SiteSelection.SelectedValue.ToString();
                App.salesdate = SalesDate.Date;
                await Navigation.PushAsync(new MainComplexSalesPage());
            }
            else
            {
                await DisplayAlert("Alert", "Please select the site and sales date!", "OK");
            }
        }

        async void OnViewSalesClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(SiteSelection.SelectedValue.ToString()) && !string.IsNullOrWhiteSpace(SalesDate.Date.ToString()))
            {
                App.salessite = SiteSelection.SelectedValue.ToString();
                App.salesdate = SalesDate.Date;
                await Navigation.PushAsync(new MainViewSalesPage());
            }
            else
            {
                await DisplayAlert("Alert", "Please select the site and sales date!", "OK");
            }
            
        }


        async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            bool hastologout = false;
            DSTransaction dstrans = new DSTransaction();

            if (dstrans.HasSales(App.userLogged.userid) > 0)
            {
                var result = await DisplayAlert("Alert", "You still have sales data not synchronize yet. Do you want to try synchronize?", "Yes", "No");
                if (result == false)
                {
                    hastologout = true;
                }
                else
                {
                    //await DisplayAlert("Alert", "You will be redirected to Synchronization Page", "OK");
                    //await Navigation.PushAsync(new SynchronizeDataPage());

                    //Simple
                    bool isconnected = await CrossConnectivity.Current.IsRemoteReachable(App.hostname, App.port, 5000);
                    if (isconnected)
                    {
                        User user = App.userLogged;
                        dstrans = new DSTransaction();

                        //Simple
                        List<Transaction> tobesync = dstrans.GetTobeSyncSimple(user.userid, 0);
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
                                //int totalnotsync = model.SalesData.Where(d => d.Synced == false).Count();
                                //btnSync.Text = "Sync Data (" + totalnotsync.ToString() + ")";
                                await DisplayAlert("Success", "Simple sales transaction data succesfully synced. Total = " + tobesync.Count().ToString(), "OK");
                                hastologout = true;
                            }
                        }

                        //Complex
                        tobesync = dstrans.GetTobeSyncComplex(user.userid, 0);
                        if (tobesync.Count() > 0)
                        {
                            ServiceWrapper serviceWrapper = new ServiceWrapper();
                            bool syncstat = await serviceWrapper.UploadComplexSales(user, tobesync);
                            if (syncstat == false)
                            {
                                await DisplayAlert("Error", "Cannot sync data!", "OK");
                            }
                            else
                            {
                                SalesRepository model = new SalesRepository();
                                BindingContext = model;
                                //int totalnotsync = model.SalesData.Where(d => d.Synced == false).Count();
                                //btnSync.Text = "Sync Data (" + totalnotsync.ToString() + ")";
                                await DisplayAlert("Success", "Sales transaction data succesfully synced. Total = " + tobesync.Count().ToString(), "OK");
                                hastologout = true;
                            }
                        }
                    }
                    else
                    {
                        await DisplayAlert("Error", "Cannot sync data. Please check your internet connection.", "OK");
                    }
                }
            }
            else
            {
                hastologout = true;
            }
            if (hastologout == true)
            {
                App.IsUserLoggedIn = false;

                DSUser dsuser = new DSUser();
                User loged = App.userLogged;
                loged.logged = 0;
                dsuser.Save(loged);

                await Navigation.PushAsync(new LoginPage());
            }
        }
    }
}
