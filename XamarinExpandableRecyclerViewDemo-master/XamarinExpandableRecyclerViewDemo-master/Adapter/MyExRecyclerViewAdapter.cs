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
using ExpandableRecyclerViewDemo.ViewHolder;
using ExpandableRecyclerViewDemo.Models;

namespace ExpandableRecyclerViewDemo.Adapter
{
    public class MyExRecyclerViewAdapter : ExpandableRecyclerAdapter<TitleParentViewHolder, TitleChildViewHolder>
    {
        LayoutInflater _inflater;

        public MyExRecyclerViewAdapter(Context context,List<IParentObject> itemList):base(context,itemList)
        {
            _inflater = LayoutInflater.From(context);
        }
        public override void OnBindChildViewHolder(TitleChildViewHolder childViewHolder, int position, object childObject)
        {
            var title = (TitleChild)childObject;
            childViewHolder.option1.Text = title.Option1;
            childViewHolder.option2.Text = title.Option2;
        }

        public override void OnBindParentViewHolder(TitleParentViewHolder parentViewHolder, int position, object parentObject)
        {
            var title = (TitleParent)parentObject;
            parentViewHolder._textView.Text = title.Title;
        }

        public override TitleChildViewHolder OnCreateChildViewHolder(ViewGroup childViewGroup)
        {
            var view = _inflater.Inflate(Resource.Layout.List_Child, childViewGroup, false);
            return new TitleChildViewHolder(view);
        }

        public override TitleParentViewHolder OnCreateParentViewHolder(ViewGroup parentViewGroup)
        {
            var view = _inflater.Inflate(Resource.Layout.List_Parent, parentViewGroup, false);
            return new TitleParentViewHolder(view);
        }
    }
}