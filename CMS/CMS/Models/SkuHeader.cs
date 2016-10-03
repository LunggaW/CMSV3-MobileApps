using SQLite;
using System;

namespace CMS.Models
{
    [Table("skuheader")]
    public class SkuHeader
    {
        [PrimaryKey]
        public int skuid { set; get; }
        public string skuidx { set; get; }
        public string skusdesc { set; get; }
        public string skuldesc { set; get; }
        public int skusdate { set; get; }
        public int skuedate { set; get; }
    }
}
