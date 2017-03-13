using CMS.DataSource;
using CMS.Models;
using CMS.ViewModels;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Controls;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace CMS.Views
{
    public partial class ViewSales : ContentPage
    {
        private bool isconnected;
        private ServiceWrapper serviceWrapper;

        
        private IEnumerable<JSalesDetail> SalesDetailList;
        private IEnumerable<JSalesDetail> SalesDetailTemp;

        public ViewSales(IEnumerable<JSalesDetail> SalesDetailTempLists, String Nota)
        {
            SalesDetailTemp = SalesDetailTempLists;
            processGridView();




        }

        async Task processGridView()
        {

            var accordion = new AccordionView();

            foreach (JSalesDetail data in SalesDetailTemp)
            {
                accordion.Items.Add(new AccordionItem
                {
                    Title =
                        "Barcode : " + data.barcode + "    Qty : " + data.qty + "    Net : " + data.totalamount + "    Type : " + data.SalesType,
                    Barcode = "Barcode : " + data.barcode,
                    Item = "Item : " + data.itemid,
                    Variant = "Variant : " + data.variant,
                    Description = "Description : " + data.description,
                    SKU = "SKU : " + data.sku,
                    Qty = "Qty : " + data.qty,
                    Gross = "Gross : " + data.gross,
                    Net = "Net : " + data.totalamount,

                });
            }



            //accordion.Items.Add(new AccordionItem { Title = "Item #1", Content = "Body content #1", IsSelected = true });
            //accordion.Items.Add(new AccordionItem { Title = "Item #2", Content = "Body content #2" });
            //accordion.Items.Add(new AccordionItem { Title = "Item #3", Content = "Body content #3" });

            var accordionItem = new AccordionItem();

            Content = accordion;

            //try
            //{
            //    //bar.IsVisible = true;
            //    //bar.Progress = .2;
            //    bool isconnected = await CrossConnectivity.Current.IsRemoteReachable(App.hostname, App.port, 5000);
            //    if (isconnected)
            //    {

            //        User user = App.userLogged;
            //        serviceWrapper = new ServiceWrapper();

            //        //bar.Progress = .4;

            //        try
            //        {
            //            //get the data
            //            JSalesHeaderPrimary salesHeaderPrimary = new JSalesHeaderPrimary();

            //            salesHeaderPrimary.nota = "1000";
            //            salesHeaderPrimary.barcode = "1000021001016";
            //            salesHeaderPrimary.site = App.salessite;
            //            salesHeaderPrimary.date = App.salesdate;
            //            salesHeaderPrimary.user = App.userLogged.userid;

            //            SalesHeaderList = await serviceWrapper.GetSalesHeader(salesHeaderPrimary, user.access_token);
            //            SalesDetailList = await serviceWrapper.GetSalesDetail(salesHeaderPrimary, user.access_token);
            //            //bar.Progress = .6;



            //            //append the data to Accordion

            //            var accordion = new AccordionView();
            //            foreach (JSalesDetail data in SalesDetailList)
            //            {
            //                accordion.Items.Add(new AccordionItem
            //                {
            //                    Title =
            //                        "Barcode : " + data.barcode + "    Qty : " + data.qty + "    Net : " + data.totalamount+"    Type : "+data.SalesType,
            //                    Barcode = "Barcode : " + data.barcode,
            //                    Item = "Item : " + data.itemid,
            //                    Variant = "Variant : " + data.variant,
            //                    Description = "Description : " + data.description,
            //                    SKU = "SKU : " + data.sku,
            //                    Qty = "Qty : " + data.qty,
            //                    Gross = "Gross : " + data.gross,
            //                    Net = "Net : " + data.totalamount,

            //                });
            //            }


                     
            //            //accordion.Items.Add(new AccordionItem { Title = "Item #1", Content = "Body content #1", IsSelected = true });
            //            //accordion.Items.Add(new AccordionItem { Title = "Item #2", Content = "Body content #2" });
            //            //accordion.Items.Add(new AccordionItem { Title = "Item #3", Content = "Body content #3" });

            //            var accordionItem = new AccordionItem();

            //            Content = accordion;

            //        }
            //        catch (ArgumentNullException ex)
            //        {
            //            await DisplayAlert("Error", "SKU or Price is empty, please check the barcode", "OK");

            //        }
            //    }
            //    else
            //    {
            //        await DisplayAlert("Error", "Cannot sync data. Please check your internet connection.", "OK");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    await DisplayAlert("Error", ex.Message + " : " + ex.InnerException, "OK");

            //}


        }

    }
}
