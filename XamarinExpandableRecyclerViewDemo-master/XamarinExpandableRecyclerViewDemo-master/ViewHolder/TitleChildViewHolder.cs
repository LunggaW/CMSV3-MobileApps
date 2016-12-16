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
using XamDroid.ExpandableRecyclerView;

namespace ExpandableRecyclerViewDemo.ViewHolder
{
    public class TitleChildViewHolder : ChildViewHolder
    {
        public TextView option1, option2;
        public TitleChildViewHolder(View itemView) : base(itemView)
        {
            option1 = itemView.FindViewById<TextView>(Resource.Id.childOption1);
            option2 = itemView.FindViewById<TextView>(Resource.Id.childOption2);
        }
    }
}