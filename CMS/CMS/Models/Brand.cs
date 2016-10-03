using SQLite;
using System;

namespace CMS.Models
{
    [Table("brand")]
    public class Brand
    {
        [PrimaryKey]
        public string brandid { set; get; }
        public string branddesc { set; get; }
    }
}
