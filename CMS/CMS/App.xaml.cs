using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Content;
using Android.Telephony;
using Xamarin.Forms;
using CMS.Models;
using CMS.DataSource;
using CMS.ViewModels;
using CMS.Views;

namespace CMS
{
    public partial class App : Application
    {
        public static bool IsUserLoggedIn;
        public static User userLogged;
        public static string salessite;
        public static DateTime salesdate;
        public static string hostname;
        public static int port;
        public static string localIMEI;
        public static string brand;

        Context mContext;
        public App(Context mContext)
        {
            InitializeComponent();

            Connection conn = new Connection();

            DSConnection dsconn = new DSConnection();

            conn = dsconn.Get();

            hostname = conn.hostname;
            port = conn.port;

            //hostname = CMS.Properties.Resources.HOSTNAME;
            //port = Convert.ToInt32(CMS.Properties.Resources.PORT);

            this.mContext = mContext;

            

            var telephonyManager = (TelephonyManager)mContext.GetSystemService(Context.TelephonyService);

            //IMEI number  
            App.localIMEI = telephonyManager.DeviceId;

            /*
            DSUser dsuser = new DSUser();
            userLogged = dsuser.getLoggedUser();

            if (userLogged != null)
                IsUserLoggedIn = true;
            */

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
