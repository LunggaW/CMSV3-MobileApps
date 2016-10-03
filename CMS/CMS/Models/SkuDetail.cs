using SQLite;
using System;

namespace CMS.Models
{
    [Table("skudetail")]
    public class SkuDetail
    {
        public int skuid { set; get; }
        public int skudetid { set; get; }
        public string skudetidx { set; get; }
        public string skudetname { set; get; }
        public int skudetlvl { set; get; }
        public decimal skudetval { set; get; }
        public int skudetvaltype { set; get; }
        public decimal skudetpart { set; get; }
        public int skudetbasedon { set; get; }
    }
}
