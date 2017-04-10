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
    public partial class ConnectionPage : ContentPage
    {
        public ConnectionPage()
        {
            InitializeComponent();


            Connection conn = new Connection();

            DSConnection dsconn = new DSConnection();

            conn = dsconn.Get();
            
            hostname.Text = conn.hostname;
            port.Text = conn.port.ToString();
        }
        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            int errorcount = 0;

            try
            {
                if (string.IsNullOrWhiteSpace(hostname.Text))
                {
                    errorcount++;
                    hostname.PlaceholderColor = Color.Red;
                    hostname.Focus();
                }
                if (string.IsNullOrWhiteSpace(port.Text))
                {
                    errorcount++;
                    port.PlaceholderColor = Color.Red;
                    port.Focus();
                }

                if (errorcount == 0)
                {
                    //if (Int32.Parse(finalPrice.Text) > Int32.Parse(normalPrice.Text))
                    //{
                    //    errorcount++;
                    //    await DisplayAlert("Alert", "Final Price cannot be larger than Normal Price", "OK");
                    //    finalPrice.Text = "";
                    //    finalPrice.Focus();
                    //}
                    //else if (Int32.Parse(finalPrice.Text) <= 0)
                    //{
                    //    errorcount++;
                    //    await DisplayAlert("Alert", "Final Price cannot be larger than 0", "OK");
                    //    finalPrice.Text = "";
                    //    finalPrice.Focus();
                    //}

                    string tempHostname = hostname.Text;
                    int tempPort = Int32.Parse(port.Text);

                    Connection conn = new Connection();

                    conn.id = 1;
                    conn.hostname = tempHostname;
                    conn.port = tempPort;

                    DSConnection dsconn = new DSConnection();


                    dsconn.Save(conn);

                    App.hostname = conn.hostname;
                    App.port = conn.port;

                    await DisplayAlert("Sucess", "Success Saved", "OK");

                }
            }
            catch (Exception ex)
            {

                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        
    }
}
