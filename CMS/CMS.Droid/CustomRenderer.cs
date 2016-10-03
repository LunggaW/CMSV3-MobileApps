using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CMS.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using CMS.Droid;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomRenderer))]
namespace CMS.Droid
{
    public class CustomRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            var native = Control as EditText;

            native.InputType = Android.Text.InputTypes.ClassNumber | Android.Text.InputTypes.NumberFlagSigned | Android.Text.InputTypes.NumberFlagDecimal;
        }
    }
}