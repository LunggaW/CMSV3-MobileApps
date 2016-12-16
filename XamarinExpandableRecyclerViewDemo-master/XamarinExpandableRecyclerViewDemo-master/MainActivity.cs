using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;
using ExpandableRecyclerViewDemo.Adapter;
using System;
using System.Collections.Generic;
using XamDroid.ExpandableRecyclerView;
using ExpandableRecyclerViewDemo.Models;

namespace ExpandableRecyclerViewDemo
{
    [Activity(Label = "ExpandableRecyclerViewDemo", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        RecyclerView myRecyclerView;


        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            ((MyExRecyclerViewAdapter)myRecyclerView.GetAdapter()).OnSaveInstanceState(outState);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.Main);

            myRecyclerView = FindViewById<RecyclerView>(Resource.Id.myRecyclerView);
            myRecyclerView.SetLayoutManager(new LinearLayoutManager(this));
            var adapter = new MyExRecyclerViewAdapter(this, InitData());
            //adapter.CustomParentAnimationViewId = Resource.Id.expandArrow;
            adapter.SetParentClickableViewAnimationDefaultDuration();
            adapter.ParentAndIconExpandOnClick = true;

            myRecyclerView.SetAdapter(adapter);
        }

        private List<IParentObject> InitData()
        {
            var titleCreator = TitleCreator.Get(this);
            var titles = titleCreator.GetAll;
            var parentObject = new List<IParentObject>();
            foreach(var title in titles)
            {
                var childList = new List<Object>();
                childList.Add(new TitleChild("Add to details", "Send Message"));
                title.ChildObjectList = childList;
                parentObject.Add(title);
            }
            return parentObject;
        }
    }
}

