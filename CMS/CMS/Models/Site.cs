using SQLite;
using System;

namespace CMS.Models
{
    [Table("site")]
    public class Site
    {
        [PrimaryKey]
        public string siteid { set; get; }
        public int siteclass { set; get; }
        public string sitename { set; get; }
        public int siteflag { set; get; }
        public int sitestatus { set; get; }
    }
}
