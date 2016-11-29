using CMS.DataSource;
using CMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CMS.Views
{
    public partial class MainPage2 : ContentPage
    {
        public MainPage2()
        {
            if (App.IsUserLoggedIn)
            {
               // lblWelcome.Text = lblWelcome.Text + " " + App.userLogged.username + "!";
                InitializeComponent();

                DSSite dssite = new DSSite();

                IEnumerable<SiteList> SiteLists = dssite.getList(App.userLogged.userprofsiteid);
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
    }
}
