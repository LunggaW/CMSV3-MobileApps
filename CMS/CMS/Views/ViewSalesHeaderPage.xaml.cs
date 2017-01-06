using CMS.DataSource;
using CMS.Models;
using CMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mobile.DataGrid;
using DevExpress.Mobile.DataGrid.Theme;

using Xamarin.Forms;
using Plugin.Connectivity;

namespace CMS.Views
{
    public partial class ViewSalesHeaderPage : ContentPage
    {
        private bool isconnected;
        private ServiceWrapper serviceWrapper;
        public ViewSalesHeaderPage(IEnumerable<JSalesHeader> SalesHeaderTemp)
        {

            

            InitializeComponent();
            ThemeManager.ThemeName = Themes.Light;
            SalesComplexRepository model;

            //DSTransaction dstrans = new DSTransaction();
            //btnSync.Text = "Sync Data (" + dstrans.HasSales(App.userLogged.userid).ToString()+ ")";

            //DSSite dssite = new DSSite();

            //IEnumerable<SiteList> SiteLists = dssite.getList(App.userLogged.userprofsiteid);
            //if (SiteLists.Count() > 0)
            //{
            //    SiteSelection.ItemsSource = SiteLists;
            //    SiteSelection.SelectedIndex = 0;
            //    model = new SalesRepository(SiteSelection.SelectedValue.ToString());
            //}
            //else
            //{
            //    model = new SalesComplexRepository();
            //}

            

            model = new SalesComplexRepository(SalesHeaderTemp);

            BindingContext = model;

            label1.Text = "Site : " + App.salessite + "  Sales Date : " + App.salesdate.ToString("dd-MMM-yyyy");
        }
        void SiteSelectionIndexChanged(object sender, EventArgs e)
        {
            //if (SiteSelection.SelectedIndex != -1)
            //{
            //    SalesRepository model = new SalesRepository(SiteSelection.SelectedValue.ToString());
            //    BindingContext = model;
            //    int totalnotsync = model.SalesData.Where(d => d.Synced == false).Count();
            //    btnSync.Text = "Sync Data (" + totalnotsync.ToString() + ")";
            //}
        }
        

        async void SalesDetailButtonClicked(object sender, EventArgs e)
        {
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


                        salesHeaderPrimary.nota = grid.GetRow(grid.SelectedRowHandle).GetFieldValue("Nota").ToString();


                        salesHeaderPrimary.site = App.salessite;
                        salesHeaderPrimary.date = App.salesdate;
                        salesHeaderPrimary.user = App.userLogged.userid;
                        salesHeaderPrimary.SalesStatus = (JSalesHeaderPrimary.SalesStatusEnum)Enum.Parse(typeof(JSalesHeaderPrimary.SalesStatusEnum), grid.GetRow(grid.SelectedRowHandle).GetFieldValue("Status").ToString());
                        salesHeaderPrimary.SalesType = (JSalesHeaderPrimary.SalesTypeEnum)Enum.Parse(typeof(JSalesHeaderPrimary.SalesTypeEnum), grid.GetRow(grid.SelectedRowHandle).GetFieldValue("Type").ToString());

                        IEnumerable<JSalesDetail> SalesDetTemp = await serviceWrapper.GetSalesDetail(salesHeaderPrimary, user.access_token);

                        if (SalesDetTemp.Any())
                        {
                            await Navigation.PushAsync(new ViewSalesDetailPage(SalesDetTemp, salesHeaderPrimary.nota));
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
        

        async void Grid_OnSelectionChanged(object sender, RowEventArgs e)
        {
            await DisplayAlert("Alert", "asd", "OK");
        }
    }
}
