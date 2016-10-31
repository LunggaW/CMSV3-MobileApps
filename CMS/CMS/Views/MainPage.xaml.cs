using CMS.DataSource;
using CMS.Models;
using CMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Connectivity;
using Android.Content;

namespace CMS.Views
{
    public partial class MainPage : ContentPage
    {
        
        public MainPage()
        {
            InitializeComponent();

            if (App.IsUserLoggedIn)
            {
                lblWelcome.Text = lblWelcome.Text + " " + App.userLogged.username + "!";
            }
            else
            {
                Navigation.PushAsync(new LoginPage());
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

                    bool isconnected = await CrossConnectivity.Current.IsRemoteReachable(App.hostname, App.port, 5000);
                    if (isconnected)
                    {
                        User user = App.userLogged;
                        dstrans = new DSTransaction();

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
                                //int totalnotsync = model.SalesData.Where(d => d.Synced == false).Count();
                                //btnSync.Text = "Sync Data (" + totalnotsync.ToString() + ")";
                                await DisplayAlert("Success", "Transaction data succesfully synced. Total = " + tobesync.Count().ToString(), "OK");
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
        async void OnLogoutClicked(object sender, EventArgs e)
        {
            bool hastologout = false;

            DSTransaction dstrans = new DSTransaction();

            if (dstrans.HasSales(App.userLogged.userid) > 0)
            {
                var result = await DisplayAlert("Alert", "You still have sales data not synchronize yet. Do you still want to logout?", "Yes", "No");
                if (result == true)
                {
                    hastologout = true;
                }
                else
                {
                    await DisplayAlert("Alert", "You will be redirected to Synchronization Page", "OK");
                    await Navigation.PushAsync(new SynchronizeDataPage());


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
        async void OnSimpleSalesInputButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SiteSelectionPage());
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
    }
}
