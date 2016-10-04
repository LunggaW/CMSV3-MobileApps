using SQLite;
using System;

namespace CMS.Models
{
    [Table("transaction")]
    public class Transaction
    {
        private int _WId;

        [PrimaryKey]
        [AutoIncrement]
        public int transid
        {
            get { return _WId; }
            set
            {
                if (_WId != value)
                {
                    _WId = value;
                }
            }
        }
        public string transnota { set; get; }
        public string transsite { set; get; }
        public int transdate { set; get; }
        public string transbrcd { set; get; }
        public int transsku { set; get; }
        public int transqty { set; get; }
        public decimal transprice { set; get; }
        public decimal transamt { set; get; }
        public int transstat { set; get; }
        public int transtype { set; get; }
        public int transflag { set; get; }
        public long transdcre { set; get; }
        public string transcreby { set; get; }

        //update GAGAN
        public decimal transfinalprice { set; get; }
        public int transdiscount { set; get; }
    }
}
