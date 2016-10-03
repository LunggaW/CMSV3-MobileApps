using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using CMS.Models;
using CMS.DataSource;
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
        public App()
        {
            InitializeComponent();
            hostname = CMS.Properties.Resources.HOSTNAME;
            port = Convert.ToInt32(CMS.Properties.Resources.PORT);

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
