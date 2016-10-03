using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.ViewModels
{
    public class SiteViewModel : ModelObject
    {
        string _siteid;
        int _class;
        string _name;
        public SiteViewModel(string _siteid, int _class, string _name)
        {
            this._siteid = _siteid;
            this._class = _class;
            this._name = _name;
        }
        public string Site {
            get { return _siteid; }
            set { _siteid = value; }
        }
        public int Class {
            get { return _class; }
            set { _class = value; }
        }
        public string Name {
            get { return _name; }
            set { _name = value; }
        }
    }
}
