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

namespace ExpandableRecyclerViewDemo.Models
{
    public class TitleParent : IParentObject
    {

        Guid _id;
        List<Object> _childrenList;


        public TitleParent()
        {
            _id = Guid.NewGuid();
        }

        public Guid Id
        {
            get
            {
                return _id;
            }
            private set
            {

            }
        }

        public string Title { get; set; }

        public List<object> ChildObjectList
        {
            get
            {
                return _childrenList;
            }

            set
            {
                _childrenList = value;
            }
        }
    }
}