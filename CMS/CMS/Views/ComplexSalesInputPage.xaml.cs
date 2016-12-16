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
    public partial class ComplexSalesInputPage : ContentPage
    {
        private bool isconnected;
        private ServiceWrapper serviceWrapper;

        public ComplexSalesInputPage()
        {
            InitializeComponent();



            CharLimitTextbox();

            SKUSelection.IsVisible = true;
            SKULabel.IsVisible = true;

            int salesdate = Convert.ToInt32(App.salesdate.ToString("yyyyMMdd"));
            var selectedValue = App.brand;
            DSSkuHeader dsskuh = new DSSkuHeader();

            //IEnumerable<SkuList> SKULists = dsskuh.GetList(App.salessite, selectedValue, salesdate);

            


            //DSBrand dsbrand = new DSBrand();
            //int salesdate = Convert.ToInt32(App.salesdate.ToString("yyyyMMdd"));

            //IEnumerable<Brand> BrandLists = dsbrand.GetList(App.salessite, salesdate);
            //IEnumerable<SkuList> SKULists;

            //if (BrandLists.Count() > 0)
            //{
            //    BrandSelection.ItemsSource = BrandLists;
            //    BrandSelection.SelectedIndex = 0;

            //    var selectedValue = BrandSelection.SelectedValue.ToString();
            //    DSSkuHeader dsskuh = new DSSkuHeader();

            //    SKULists = dsskuh.GetList(App.salessite, selectedValue, salesdate);
            //    if (SKULists.Count() > 0)
            //    {
            //        SKUSelection.ItemsSource = SKULists;
            //        SKUSelection.SelectedIndex = 0;
            //    }
            //}
            //else
            //{
            //    BrandLists = new List<Brand>();
            //    BrandSelection.ItemsSource = BrandLists;
            //    SKULists = new List<SkuList>();
            //    SKUSelection.ItemsSource = SKULists;
            //}
        }

        //public void BrandSelectionSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (BrandSelection.SelectedIndex != -1)
        //    {
        //        int salesdate = Convert.ToInt32(App.salesdate.ToString("yyyyMMdd"));
        //        var selectedValue = BrandSelection.SelectedValue.ToString();
        //        DSSkuHeader dsskuh = new DSSkuHeader();

        //        IEnumerable<SkuList> SKULists = dsskuh.GetList(App.salessite, selectedValue, salesdate);
        //        if(SKULists.Count() > 0)
        //        {
        //            SKUSelection.ItemsSource = SKULists;
        //            SKUSelection.SelectedIndex = 0;
        //        }
        //    }
        //}

        private void CharLimitTextbox()
        {
            var charLimit = new CharacterLimit(); //this will hold page(view) data
            BindingContext = charLimit;

            //discount.SetBinding(Entry.TextProperty, "AmountPercent");

            barcode.SetBinding(Entry.TextProperty, "BarcodeLength");

            Price.SetBinding(Entry.TextProperty, "AmountNormalPrice");            


            Qty.SetBinding(Entry.TextProperty, "AmountQty");
        }





        async void OnbtnScanClicked(object sender, EventArgs e)
        {
            var options = new ZXing.Mobile.MobileBarcodeScanningOptions();
            options.PossibleFormats = new List<ZXing.BarcodeFormat>() {
                ZXing.BarcodeFormat.EAN_13,
                ZXing.BarcodeFormat.EAN_8,
                ZXing.BarcodeFormat.UPC_A,
                ZXing.BarcodeFormat.UPC_E,
                ZXing.BarcodeFormat.UPC_EAN_EXTENSION,
                ZXing.BarcodeFormat.CODE_128
            };

            ZXingScannerPage scanPage = new ZXingScannerPage(options);
            scanPage.OnScanResult += (result) =>
            {
                scanPage.IsScanning = false;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    scanPage.AutoFocus();
                    barcode.Text = result.Text;
                    await Navigation.PopAsync();
                });
            };

            await Navigation.PushAsync(scanPage);
        }
        //async 
        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            int errorcount = 0;

            try
            {
                if (string.IsNullOrWhiteSpace(Nota.Text))
                {
                    errorcount++;
                    Nota.PlaceholderColor = Color.Red;
                    Nota.Focus();
                }

                if (string.IsNullOrWhiteSpace(Price.Text))
                {
                    errorcount++;
                    Price.PlaceholderColor = Color.Red;
                    Price.Focus();
                }
                if (string.IsNullOrWhiteSpace(Qty.Text))
                {
                    errorcount++;
                    Qty.PlaceholderColor = Color.Red;
                    Qty.Focus();
                }
                if (string.IsNullOrWhiteSpace(barcode.Text))
                {
                    errorcount++;
                    barcode.PlaceholderColor = Color.Red;
                    barcode.Focus();
                }
                if (string.IsNullOrWhiteSpace(barcode.Text))
                {
                    errorcount++;
                    barcode.PlaceholderColor = Color.Red;
                    barcode.Focus();
                }
                if (SKUSelection.SelectedIndex == -1)
                {
                    errorcount++;
                    await DisplayAlert("Alert", "Please choose SKU", "OK");
                    SKUSelection.Focus();
                }

                if (errorcount == 0)
                {
                    if (Int32.Parse(Price.Text) <= 0)
                    {
                        errorcount++;
                        await DisplayAlert("Alert", "Price cannot be smaller than 0", "OK");
                        Price.Text = "";
                        Price.Focus();
                    }
                    else if (Int32.Parse(Qty.Text) <= 0)
                    {
                        errorcount++;
                        await DisplayAlert("Alert", "Quantity cannot be 0", "OK");
                        Qty.Text = "";
                        Qty.Focus();
                    }
                    else
                    {
                        string transsite = App.salessite;
                        int transdate = Convert.ToInt32(App.salesdate.ToString("yyyyMMdd"));
                        string transnota = Nota.Text;
                        string transbrcd = barcode.Text;
                        int transsku = Convert.ToInt32(SKUSelection.SelectedValue.ToString());
                        int transqty = Convert.ToInt32(Qty.Text);
                        decimal transamt = Convert.ToDecimal(Price.Text);

                        string transcomm = Comment.Text;

                        //Sales
                        short transstat = 1;

                        short transtype = 2;
                        short transflag = 0;
                        short transiscomplex = 1;

                        long transdcre = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddhhmmss"));
                        string transcreby = App.userLogged.userid;

                        Transaction sales = new Transaction();
                        sales.transnota = transnota;
                        sales.transsite = transsite;
                        sales.transdate = transdate;
                        sales.transbrcd = transbrcd;
                        sales.transsku = transsku;
                        sales.transqty = transqty;
                        sales.transamt = transamt;
                        sales.transstat = transstat;
                        sales.transtype = transtype;
                        sales.transflag = transflag;
                        sales.transdcre = transdcre;
                        sales.transcreby = transcreby;
                        sales.transcomm = transcomm;
                        sales.transiscomplex = transiscomplex;


                        DSTransaction dstrans = new DSTransaction();

                        //dstrans.Save(sales);

                        bool isconnected =
                            await CrossConnectivity.Current.IsRemoteReachable(App.hostname, App.port, 1000);
                        if (isconnected)
                        {
                            ServiceWrapper serviceWrapper = new ServiceWrapper();
                            List<Transaction> tobesync = new List<Transaction>();
                            tobesync.Add(sales);
                            bool syncstat = await serviceWrapper.UploadComplexSales(App.userLogged, tobesync);


                            barcode.Text = "";
                            //BrandSelection.SelectedIndex = -1;
                            //SKUSelection.SelectedIndex = -1;
                            SKUSelection.ItemsSource = null;
                            Qty.Text = "";
                            Price.Text = "";
                            Comment.Text = "";
                            Nota.Focus();
                            //discount.Text = "";
                        }
                        else
                        {
                            await DisplayAlert("Error", "No Connection", "OK");
                        }

                    
                    }

                }
            }
            catch (Exception ex)
            {

                await DisplayAlert("Error", ex.Message, "OK");
            }
        }




        private void Barcode_OnUnfocused(object sender, FocusEventArgs e)
        {
            processBarcode();
            Qty.Focus();
        }

        async void processBarcode()
        {
            try
            {
                bar.IsVisible = true;
                bar.Progress = .2;
                bool isconnected = await CrossConnectivity.Current.IsRemoteReachable(App.hostname, App.port, 5000);
                if (isconnected)
                {

                    User user = App.userLogged;
                    serviceWrapper = new ServiceWrapper();

                    bar.Progress = .4;

                    try
                    {
                        IEnumerable<SkuList> SKULists = await serviceWrapper.GetSkuLists(barcode.Text, App.salessite, user.access_token);
                        bar.Progress = .6;
                        bar.Progress = .7;
                        serviceWrapper = new ServiceWrapper();
                        Price.Text = await serviceWrapper.GetPrice(barcode.Text, App.salessite, user.access_token);

                        bar.Progress = .9;

                        bar.Progress = 1;
                        bar.IsVisible = false;

                        Qty.Text = "1";
                        

                        if (SKULists.Any())
                        {
                            SKULabel.IsVisible = true;
                            SKUSelection.IsVisible = true;
                            SKUSelection.ItemsSource = SKULists;

                            if (SKULists.Count() == 1)
                            {
                                SKUSelection.SelectedIndex = 0;
                                Comment.Focus();
                            }
                            else
                            {
                                SKUSelection.Focus();
                            }

                        }
                    }
                    catch (ArgumentNullException ex)
                    {
                        await DisplayAlert("Error", "SKU or Price is empty, please check the barcode", "OK");
                        barcode.Focus();
                    }
                }
                else
                {
                    await DisplayAlert("Error", "Cannot sync data. Please check your internet connection.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message +" : "+ex.InnerException, "OK");
                
            }

            
        }

        
    }
}
