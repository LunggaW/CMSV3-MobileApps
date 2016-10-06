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
    public partial class SiteSelectionPage : ContentPage
    {
        public SiteSelectionPage()
        {
            InitializeComponent();

            DSSite dssite = new DSSite();

            IEnumerable<SiteList> SiteLists = dssite.getList(App.userLogged.userprofsiteid);
            if(SiteLists.Count() > 0)
            {
                SiteSelection.ItemsSource = SiteLists;
                SiteSelection.SelectedIndex = 0;
            }
            SalesDate.MinimumDate = DateTime.Today.AddDays(-30);
        }
        async void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(SiteSelection.SelectedValue.ToString()) && !string.IsNullOrWhiteSpace(SalesDate.Date.ToString()))
            {
                App.salessite = SiteSelection.SelectedValue.ToString();
                App.salesdate = SalesDate.Date;
                await Navigation.PushAsync(new SimpleSalesInputPage());
            }
            else
            {
                await DisplayAlert("Alert", "Please select the site and sales date!", "OK");
            }
        }

        async void OnSalesReturnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(SiteSelection.SelectedValue.ToString()) && !string.IsNullOrWhiteSpace(SalesDate.Date.ToString()))
            {
                App.salessite = SiteSelection.SelectedValue.ToString();
                App.salesdate = SalesDate.Date;
                await Navigation.PushAsync(new SalesReturnPage());
            }
            else
            {
                await DisplayAlert("Alert", "Please select the site and sales date!", "OK");
            }
        }
    }
}
