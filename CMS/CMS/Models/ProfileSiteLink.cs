using SQLite;
using System;

namespace CMS.Models
{
    [Table("profsitelink")]
    public class ProfileSiteLink
    {
        public string profsiteid { set; get; }
        public string siteid { set; get; }

        public string userid { set; get; }
        public int linksdate { set; get; }
        public int linkedate { set; get; }
    }
}
