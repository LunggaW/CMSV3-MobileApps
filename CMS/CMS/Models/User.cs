using SQLite;
using System;

namespace CMS.Models
{
    [Table("user")]
    public class User
    {
        [PrimaryKey]
        public string userid { set; get; }
        public string password { set; get; }
        public string username { set; get; }
        public string userdesc { set; get; }
        public int userstatus { set; get; }
        public int usertype { set; get; }
        public int usersdate { set; get; }
        public int useredate { set; get; }
        public string userprofsiteid { set; get; }
        public string userprofaccid { set; get; }
        public string userprofmenuid { set; get; }
        public string access_token { set; get; }
        public int logged { set; get; }
        public long lastlogin { set; get; }
        public long lastdown { set; get; }

        public string imei { set; get; }
    }
}
