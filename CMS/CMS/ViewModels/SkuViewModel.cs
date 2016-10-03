using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.ViewModels
{
    public class SkuViewModel : ModelObject
    {
        int _skuid;
        string _skuidx;
        string _skusdesc;
        string _skuldesc;
        DateTime _skusdate;
        DateTime _skuedate;
        public SkuViewModel(int _skuid,string _skuidx, string _skusdesc, string _skuldesc, int _skusdate, int _skuedate)
        {
            this._skuid = _skuid;
            this._skuidx = _skuidx;
            this._skusdesc = _skusdesc;
            this._skuldesc = _skuldesc;
            this._skusdate = Convert.ToDateTime(_skusdate.ToString().Substring(0, 4) + "-" + _skusdate.ToString().Substring(4, 2) + "-" + _skusdate.ToString().Substring(6, 2));
            this._skuedate = Convert.ToDateTime(_skuedate.ToString().Substring(0, 4) + "-" + _skuedate.ToString().Substring(4, 2) + "-" + _skuedate.ToString().Substring(6, 2));
        }
        public int skuid {
            get { return _skuid; }
            set { _skuid = value; }
        }
        public string skuidx {
            get { return _skuidx; }
            set { _skuidx = value; }
        }
        public string skusdesc {
            get { return _skusdesc; }
            set { _skusdesc = value; }
        }
        public string skuldesc {
            get { return _skuldesc; }
            set { _skuldesc = value; }
        }
        public DateTime skusdate {
            get { return _skusdate; }
            set { _skusdate = value; }
        }
        public DateTime skuedate {
            get { return _skuedate; }
            set { _skuedate = value; }
        }
    }
}
