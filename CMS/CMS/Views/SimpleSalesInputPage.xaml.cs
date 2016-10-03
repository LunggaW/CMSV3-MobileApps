﻿using CMS.DataSource;
using CMS.Models;
using CMS.ViewModels;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace CMS.Views
{
    public partial class SimpleSalesInputPage : ContentPage
    {
        public SimpleSalesInputPage()
        {
            InitializeComponent();


            DSBrand dsbrand = new DSBrand();
            int salesdate = Convert.ToInt32(App.salesdate.ToString("yyyyMMdd"));

            IEnumerable<Brand> BrandLists = dsbrand.GetList(App.salessite, salesdate);
            IEnumerable<SkuList> SKULists;
            if (BrandLists.Count() > 0)
            {
                BrandSelection.ItemsSource = BrandLists;
                BrandSelection.SelectedIndex = 0;

                var selectedValue = BrandSelection.SelectedValue.ToString();
                DSSkuHeader dsskuh = new DSSkuHeader();

                SKULists = dsskuh.GetList(App.salessite, selectedValue, salesdate);
                if (SKULists.Count() > 0)
                {
                    SKUSelection.ItemsSource = SKULists;
                    SKUSelection.SelectedIndex = 0;
                }
            }
            else
            {
                BrandLists = new List<Brand>();
                BrandSelection.ItemsSource = BrandLists;
                SKULists = new List<SkuList>();
                SKUSelection.ItemsSource = SKULists;
            }
        }

        public void BrandSelectionSelectedIndexChanged(object sender, EventArgs e)
        {
            if (BrandSelection.SelectedIndex != -1)
            {
                int salesdate = Convert.ToInt32(App.salesdate.ToString("yyyyMMdd"));
                var selectedValue = BrandSelection.SelectedValue.ToString();
                DSSkuHeader dsskuh = new DSSkuHeader();

                IEnumerable<SkuList> SKULists = dsskuh.GetList(App.salessite, selectedValue, salesdate);
                if(SKULists.Count() > 0)
                {
                    SKUSelection.ItemsSource = SKULists;
                    SKUSelection.SelectedIndex = 0;
                }
            }
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
            scanPage.OnScanResult += (result) => {
                scanPage.IsScanning = false;

                Device.BeginInvokeOnMainThread(async () => {
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

            if (string.IsNullOrWhiteSpace(price.Text))
            {
                errorcount++;
                price.PlaceholderColor = Color.Red;
                price.Focus();
            }
            if (string.IsNullOrWhiteSpace(Qty.Text))
            {
                errorcount++;
                Qty.PlaceholderColor = Color.Red;
                Qty.Focus();
            }
            if (string.IsNullOrEmpty(SKUSelection.SelectedValue.ToString()))
            {
                errorcount++;
                SKUSelection.BackgroundColor = Color.Red;
                SKUSelection.Focus();
            }
            if (string.IsNullOrWhiteSpace(BrandSelection.SelectedValue.ToString()))
            {
                errorcount++;
                BrandSelection.BackgroundColor = Color.Red;
                BrandSelection.Focus();
            }
            if (string.IsNullOrWhiteSpace(barcode.Text))
            {
                errorcount++;
                barcode.PlaceholderColor = Color.Red;
                barcode.Focus();
            }
            if (string.IsNullOrWhiteSpace(Nota.Text))
            {
                errorcount++;
                Nota.PlaceholderColor = Color.Red;
                Nota.Focus();
            }

            if (errorcount == 0)
            {
                string transsite = App.salessite;
                int transdate = Convert.ToInt32(App.salesdate.ToString("yyyyMMdd"));
                string transnota = Nota.Text;
                string transbrcd = barcode.Text;
                string transbrand = BrandSelection.SelectedValue.ToString();
                int transsku = Convert.ToInt32(SKUSelection.SelectedValue.ToString());
                int transqty = Convert.ToInt32(Qty.Text);
                decimal transprice = Convert.ToDecimal(price.Text);
                decimal transamt = transqty * transprice;
                short transstat = 1;
                short transtype = 2;
                short transflag = 0;
                long transdcre = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddhhmmss"));
                string transcreby = App.userLogged.userid;

                Transaction sales = new Transaction();
                sales.transnota = transnota;
                sales.transsite = transsite;
                sales.transdate = transdate;
                sales.transbrcd = transbrcd;
                sales.transsku = transsku;
                sales.transqty = transqty;
                sales.transprice = transprice;
                sales.transamt = transamt;
                sales.transstat = transstat;
                sales.transtype = transtype;
                sales.transflag = transflag;
                sales.transdcre = transdcre;
                sales.transcreby = transcreby;

                DSTransaction dstrans = new DSTransaction();
                dstrans.Save(sales);

                bool isconnected = await CrossConnectivity.Current.IsRemoteReachable(App.hostname, App.port, 1000);
                if (isconnected)
                {
                    ServiceWrapper serviceWrapper = new ServiceWrapper();
                    List<Transaction> tobesync = new List<Transaction>();
                    tobesync.Add(sales);
                    bool syncstat = await serviceWrapper.UploadSales(App.userLogged, tobesync);
                }

                    barcode.Text = "";
                BrandSelection.SelectedIndex = -1;
                SKUSelection.SelectedIndex = -1;
                Qty.Text = "";
                price.Text = "";
            }
        }
    }
}