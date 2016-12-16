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
    public partial class MainViewSalesPage : ContentPage
    {

        private bool isconnected;
        private ServiceWrapper serviceWrapper;

        public MainViewSalesPage()
        {
            if (App.IsUserLoggedIn)
            {
                // lblWelcome.Text = lblWelcome.Text + " " + App.userLogged.username + "!";
                InitializeComponent();

                DSSite dssite = new DSSite();
                JSalesHeader salesHeader = new JSalesHeader();

                List<SiteList> SalesTypeLists = salesHeader.EnumToSiteList(typeof(JSalesHeader.SalesStatusEnum));

                if (SalesTypeLists.Any())
                {
                    SalesTypeSelection.ItemsSource = SalesTypeLists;
                }

                List<SiteList> SalesStatusLists = salesHeader.EnumToSiteList(typeof(JSalesHeader.SalesTypeEnum));

                if (SalesStatusLists.Any())
                {
                    SalesStatusSelection.ItemsSource = SalesStatusLists;
                }

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
        
        async void ViewSalesHeaderButtonClicked(object sender, EventArgs e)
        {
            JSalesHeader salesHeaderTemp = new JSalesHeader();

            
            


            try
            {
                //bar.IsVisible = true;
                //bar.Progress = .2;
                bool isconnected = await CrossConnectivity.Current.IsRemoteReachable(App.hostname, App.port, 5000);
                if (isconnected)
                {

                    User user = App.userLogged;
                    serviceWrapper = new ServiceWrapper();

                    //bar.Progress = .4;

                    try
                    {
                        IEnumerable<JSalesHeader> SalesHeaderList;
                        //get the data
                        JSalesHeaderPrimary salesHeaderPrimary = new JSalesHeaderPrimary();

                        salesHeaderPrimary.nota = Nota.Text;
                        salesHeaderPrimary.site = App.salessite;
                        salesHeaderPrimary.date = App.salesdate;
                        salesHeaderPrimary.user = App.userLogged.userid;
                        salesHeaderPrimary.SalesStatus = (JSalesHeaderPrimary.SalesStatusEnum)Int32.Parse(SalesStatusSelection.SelectedValue.ToString());
                        salesHeaderPrimary.SalesType = (JSalesHeaderPrimary.SalesTypeEnum)Int32.Parse(SalesTypeSelection.SelectedValue.ToString());

                        IEnumerable<JSalesHeader> SalesHeaderTemp = await serviceWrapper.GetSalesHeaderByStatusTypeSales(salesHeaderPrimary, user.access_token);

                        if (SalesHeaderTemp.Any())
                        {
                            await Navigation.PushAsync(new ViewSalesHeaderPage(SalesHeaderTemp));
                        }
                        else
                        {
                            await DisplayAlert("Error", "List Empty, please revise the search criteria", "OK");
                        }
                        
                        //SalesDetailList = await serviceWrapper.GetSalesDetail(salesHeaderPrimary, user.access_token);
                        //bar.Progress = .6;


                    }
                    catch (ArgumentNullException ex)
                    {
                        await DisplayAlert("Error", "SKU or Price is empty, please check the barcode", "OK");

                    }
                }
                else
                {
                    await DisplayAlert("Error", "Cannot sync data. Please check your internet connection.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message + " : " + ex.InnerException, "OK");

            }


           

            

        }

        async void ViewSalesDetailButtonClicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new ViewSalesDetailPage());

        }
        
    }
}
