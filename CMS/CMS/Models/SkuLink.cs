using SQLite;
using System;

namespace CMS.Models
{
    [Table("skulink")]
    public class SkuLink
    {
        public int skuid { set; get; }
        public string siteid { set; get; }
        public string brandid { set; get; }
        public int linksdate { set; get; }
        public int linkedate { set; get; }
    }
}
