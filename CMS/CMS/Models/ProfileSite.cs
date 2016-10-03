using SQLite;
using System;

namespace CMS.Models
{
    [Table("profilesite")]
    public class ProfileSite
    {
        [PrimaryKey]
        public string profsiteid { set; get; }
        public string profsitedesc { set; get; }
    }
}
