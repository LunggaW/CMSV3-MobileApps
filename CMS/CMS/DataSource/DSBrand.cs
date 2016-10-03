using SQLite;
using Xamarin.Forms;
using CMS.Models;
using System.Collections.Generic;
using System.Linq;

namespace CMS.DataSource
{
    public class DSBrand
    {
        SQLiteConnection dbConn;
        public DSBrand()
        {
            dbConn = DependencyService.Get<ISQLite>().GetConnection();
        }
        public IEnumerable<Brand> GetAll()
        {
            return dbConn.Query<Brand>("Select * From [brand]");
        }
        public IEnumerable<Brand> GetList(string siteid, int salesdate)
        {
            return dbConn.Query<Brand>("Select brandid, branddesc From [brand] brnd where exists(select 1 from [skulink] link where brnd.brandid = link.brandid and link.siteid = '" + siteid + "' and link.linksdate <= " + salesdate + " and link.linkedate >= " + salesdate +")");
        }
        public Brand Get(string brandid)
        {
            return dbConn.Table<Brand>().FirstOrDefault(x => x.brandid == brandid);
        }
        public int Save(Brand aBrand)
        {
            Brand brand = Get(aBrand.brandid);
            if (brand == null)
                return dbConn.Insert(aBrand);
            else
            {
                brand = null;
                return dbConn.Update(aBrand);
            }
        }
        public int Delete(Brand aBrand)
        {
            return dbConn.Delete(aBrand);
        }
        public int Delete(string brandid)
        {
            return dbConn.Delete<Brand>(brandid);
        }
        public int Truncate()
        {
            return dbConn.DeleteAll<Brand>();
        }
    }
}
