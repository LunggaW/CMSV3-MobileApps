using CMS.DataSource;
using CMS.Models;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Telephony;
using CMS.ViewModels;
using Xamarin.Forms;

namespace CMS.Views
{
    public partial class LoginPage : ContentPage
    {
        private bool isconnected;
        private ServiceWrapper serviceWrapper;


        public LoginPage()
        {
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
            bar.IsVisible = false;

        }

        //public LoginPage()
        //{
        //    NavigationPage.SetHasBackButton(this, false);
        //    InitializeComponent();
        //    NavigationPage.SetHasBackButton(this, false);
        //    NavigationPage.SetHasNavigationBar(this, false);
        //    bar.IsVisible = false;

        //}
        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            try
            {


                //await DisplayAlert("IMEI", App.IMEI, "OK");
                bar.IsVisible = true;


                string username = usernameEntry.Text.Trim();
                string password = passwordEntry.Text.Trim();
                DSUser dsuser = new DSUser();

                if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
                {
                    bar.Progress = .2;
                    isconnected = await CrossConnectivity.Current.IsRemoteReachable(App.hostname, App.port, 5000);
                    User userloged = dsuser.Get(username, password);
                    bool locallogin = true;

                    if (userloged != null)
                    {

                        if (string.Compare(DateTime.Now.ToString("yyyyMMdd"), userloged.lastdown.ToString().Substring(0, 8)) != 0)
                        {
                            if (isconnected)
                                locallogin = false;
                        }
                    }
                    else
                    {
                        locallogin = false;
                    }

                    bar.Progress = .3;

                    if (locallogin == false && isconnected == true)
                    {
                        serviceWrapper = new ServiceWrapper();
                        try
                        {
                            TokenModel tokenModel = await serviceWrapper.GetAuthorizationTokenData(username, password);

                            bar.Progress = .4;

                            string token = tokenModel.access_token;

                            if (!string.IsNullOrWhiteSpace(token))
                            {
                                //string ServerImei = await serviceWrapper.GetImei(username, token);

                                //if (ServerImei == App.localIMEI)
                                //{
                                userloged = await serviceWrapper.GetUserData(username, password, token);
                                bar.Progress = .8;
                                if (userloged != null)
                                {
                                    App.userLogged = userloged;
                                    App.IsUserLoggedIn = true;

                                    await DisplayAlert("Success", "Welcome back " + userloged.username + "!", "OK");

                                    bar.Progress = .9;

                                    UploadSales();
                                    bar.Progress = 1;
                                    await Navigation.PushAsync(new MainPage2());


                                }
                                else
                                {
                                    await DisplayAlert("Alert", "Download data failed! Please check your connection and login again.", "OK");
                                    usernameEntry.Text = string.Empty;
                                    passwordEntry.Text = string.Empty;
                                    usernameEntry.Focus();
                                }
                                //}
                                //else
                                //{
                                //    await DisplayAlert("Alert", "IMEI is not the same.", "OK");
                                //}


                            }
                            else
                            {
                                await DisplayAlert("Alert", "Login failed! Please check again you username and password.", "OK");
                                usernameEntry.Text = string.Empty;
                                passwordEntry.Text = string.Empty;
                                usernameEntry.Focus();
                            }
                        }
                        catch
                        {
                            await DisplayAlert("Alert", "Login failed! Please check again you username and password.", "OK");
                            usernameEntry.Text = string.Empty;
                            passwordEntry.Text = string.Empty;
                            usernameEntry.Focus();
                        }
                    }
                    //else if (locallogin && isconnected)
                    //{

                    //}
                    else
                    {
                        if (userloged != null)
                        {
                            bar.Progress = .5;
                            userloged.lastlogin = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddhhmmss"));
                            userloged.logged = 1;
                            dsuser.Save(userloged);

                            App.userLogged = userloged;
                            App.IsUserLoggedIn = true;
                            if (string.Compare(DateTime.Now.ToString("yyyyMMdd"), userloged.lastdown.ToString().Substring(0, 8)) != 0)
                                await DisplayAlert("Information", "Welcome back " + userloged.username + "! " + "You are not connected to the server. You're working on offile mode.", "OK");
                            else
                                await DisplayAlert("Sucess", "Welcome back " + userloged.username + "!", "OK");
                            bar.Progress = .6;
                            UploadSales();
                            bar.Progress = .9;
                            await Navigation.PushAsync(new MainPage2());
                            //await Navigation.PushAsync(new MainFPage());
                            bar.Progress = 1;
                        }
                        else
                        {
                            await DisplayAlert("Alert", "Login failed! Please check again you username and password.", "OK");
                            usernameEntry.Text = string.Empty;
                            passwordEntry.Text = string.Empty;
                            usernameEntry.Focus();
                        }
                    }
                }
                else
                {
                    await DisplayAlert("Alert", "Login failed! Please check again you username and password.", "OK");
                    usernameEntry.Text = string.Empty;
                    passwordEntry.Text = string.Empty;
                    usernameEntry.Focus();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");

                throw;
            }
        }

        private void UsernameEntry_OnCompleted(object sender, EventArgs e)
        {
            passwordEntry.Focus();
        }

        private void PasswordEntry_OnCompleted(object sender, EventArgs e)
        {
            btnLogin.Focus();
        }
        protected override bool OnBackButtonPressed()
        {
            if (App.IsUserLoggedIn)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        async void UploadSales()
        {
            bar.Progress = .6;
            isconnected = await CrossConnectivity.Current.IsRemoteReachable(App.hostname, App.port, 5000);
            if (isconnected)
            {
                User user = App.userLogged;
                DSTransaction dstrans = new DSTransaction();

                List<Transaction> tobesync = dstrans.GetTobeSync(user.userid, 0);
                if (tobesync.Count() > 0)
                {
                    serviceWrapper = new ServiceWrapper();
                    bar.Progress = .7;
                    bool syncstat = await serviceWrapper.UploadSales(user, tobesync);
                    if (syncstat)
                    {
                        SalesRepository model = new SalesRepository();
                        BindingContext = model;
                        await DisplayAlert("Success", "Transaction data succesfully synced. Total = " + tobesync.Count().ToString(), "OK");
                        bar.Progress = .8;
                    }
                }
            }
            else
            {
                await DisplayAlert("Error", "Cannot sync data. Please check your internet connection.", "OK");
            }
        }

        private string getLocalImei()
        {
            return null;
        }
    }
}
