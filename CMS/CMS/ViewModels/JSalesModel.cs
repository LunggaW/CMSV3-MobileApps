using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.ViewModels
{
    public class JSalesModel
    {
        public int transid { set; get; }
        public string transnota { set; get; }
        public string transsite { set; get; }
        public DateTime transdate { set; get; }
        public string transbrcd { set; get; }
        public int transsku { set; get; }
        public int transqty { set; get; }
        public decimal transprice { set; get; }
        public decimal transamt { set; get; }
        public int transstat { set; get; }
        public int transtype { set; get; }
        public int transflag { set; get; }
        public DateTime transdcre { set; get; }
        public string transcreby { set; get; }
    }
}
