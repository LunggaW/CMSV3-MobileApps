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
    public class TitleCreator
    {
        static TitleCreator _titleCreator;
        Context context;
        List<TitleParent> _titleParents;

        public TitleCreator(Context context)
        {
            _titleParents = new List<TitleParent>();
            for(int i = 1; i<=10;i++)
            {
                var title = new TitleParent() {
                    Title = $"Caller #{i}"
                };
            _titleParents.Add(title);
            }
       
        }

        public static TitleCreator Get(Context context)
        {
            if (_titleCreator == null)
                _titleCreator = new TitleCreator(context);
            return _titleCreator;
        }

        public List<TitleParent> GetAll
        {
            get
            {
                return _titleParents;
            }
            private set
            {

            }
        }
       
    }
}