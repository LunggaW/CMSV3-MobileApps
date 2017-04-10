using SQLite;
using System;

namespace CMS.Models
{
    [Table("connection")]
    public class Connection
    {
        [PrimaryKey]
        public int id { set; get; }
        public string hostname { set; get; }
        public int port { set; get; }

    }
}
