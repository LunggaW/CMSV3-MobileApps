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

namespace ExpandableRecyclerViewDemo.Models
{
    public class TitleChild
    {
        public string Option1 { get; set; }
        public string Option2 { get; set; }

        public TitleChild(string option1,string option2)
        {
            Option1 = option1;
            Option2 = option2;
        }


    }
}