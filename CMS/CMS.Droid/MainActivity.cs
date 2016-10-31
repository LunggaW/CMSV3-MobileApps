using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using CMS.Views;

namespace CMS.Droid
{
    [Activity(Label = "CMS", Theme = "@style/MainTheme", Icon = "@drawable/icon", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;


           //LoginPage lfyl = new LoginPage(this); //Here the context is passing 


            base.OnCreate(bundle);
            global::DevExpress.Mobile.Forms.Init();
            global::ZXing.Net.Mobile.Forms.Android.Platform.Init();

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(this));
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            global::ZXing.Net.Mobile.Forms.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        //public override void OnBackPressed()
        //{
        //    // This prevents a user from being able to hit the back button and leave the login page.
        //    //if (!user.IsLoggedIn()) return;

        //    //base.OnBackPressed();
        //}

    }
}

