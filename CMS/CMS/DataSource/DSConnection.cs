using SQLite;
using Xamarin.Forms;
using CMS.Models;
using System.Collections.Generic;
using System.Linq;

namespace CMS.DataSource
{
    public class DSConnection
    {
        SQLiteConnection dbConn;
        public DSConnection()
        {
            dbConn = DependencyService.Get<ISQLite>().GetConnection();
        }
        public IEnumerable<Connection> GetAll()
        {
            return dbConn.Query<Connection>("Select * From [connection]");
        }
        //public IEnumerable<Brand> GetList(string siteid, int salesdate)
        //{
        //    return dbConn.Query<Brand>("Select brandid, branddesc From [brand] brnd where exists(select 1 from [skulink] link where brnd.brandid = link.brandid and link.siteid = '" + siteid + "' and link.linksdate <= " + salesdate + " and link.linkedate >= " + salesdate +")");
        //}
        public Connection Get()
        {
            return dbConn.Table<Connection>().FirstOrDefault(x => x.id == 1);
        }
        public int Save(Connection aConnection)
        {
            return dbConn.Update(aConnection);
        }
        //public int Delete(Connection connection)
        //{
        //    return dbConn.Delete(aBrand);
        //}
        //public int Delete(string brandid)
        //{
        //    return dbConn.Delete<Brand>(brandid);
        //}
        //public int Truncate()
        //{
        //    return dbConn.DeleteAll<Brand>();
        //}
    }
}
