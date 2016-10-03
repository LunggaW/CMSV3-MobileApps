using CMS.DataSource;
using CMS.Models;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CMS.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
			NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
        }
        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string username = usernameEntry.Text.Trim();
            string password = passwordEntry.Text.Trim();
            DSUser dsuser = new DSUser();

            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            {
                bool isconnected = await CrossConnectivity.Current.IsRemoteReachable(App.hostname, App.port, 5000);
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

                if (locallogin == false && isconnected == true)
                {
                    ServiceWrapper serviceWrapper = new ServiceWrapper();
                    try
                    {
                        TokenModel tokenModel = await serviceWrapper.GetAuthorizationTokenData(username, password);

                        string token = tokenModel.access_token;

                        if (!string.IsNullOrWhiteSpace(token))
                        {
                            userloged = await serviceWrapper.GetUserData(username, password, token);
                            if (userloged != null)
                            {
                                App.userLogged = userloged;
                                App.IsUserLoggedIn = true;

                                await DisplayAlert("Sucess", "Welcome back " + userloged.username + "!", "OK");
                                await Navigation.PushAsync(new MainPage());
                            }
                            else
                            {
                                await DisplayAlert("Alert", "Download data failed! Please check your connection and login again.", "OK");
                                usernameEntry.Text = string.Empty;
                                passwordEntry.Text = string.Empty;
                                usernameEntry.Focus();
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
                    catch
                    {
                        await DisplayAlert("Alert", "Login failed! Please check again you username and password.", "OK");
                        usernameEntry.Text = string.Empty;
                        passwordEntry.Text = string.Empty;
                        usernameEntry.Focus();
                    }
                }
                else
                {
                    if (userloged != null)
                    {
                        userloged.lastlogin = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddhhmmss"));
                        userloged.logged = 1;
                        dsuser.Save(userloged);

                        App.userLogged = userloged;
                        App.IsUserLoggedIn = true;
                        if (string.Compare(DateTime.Now.ToString("yyyyMMdd"), userloged.lastdown.ToString().Substring(0, 8)) != 0)
                            await DisplayAlert("Information", "Welcome back " + userloged.username + "! " + "You are not connected to the server. You're working on offile mode.", "OK");
                        else
                            await DisplayAlert("Sucess", "Welcome back " + userloged.username + "!", "OK");
                        await Navigation.PushAsync(new MainPage());
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
            if (App.IsUserLoggedIn == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
