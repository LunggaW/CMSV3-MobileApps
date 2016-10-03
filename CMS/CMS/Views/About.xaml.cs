using CMS.DataSource;
using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CMS.Views
{
    public partial class About : ContentPage
    {
        public About()
        {
      //      InitializeComponent();            
        }
        async void OnLogoutButtonClicked(object sender, EventArgs e)
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
    }
}
