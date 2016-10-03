using SQLite;
using Xamarin.Forms;
using CMS.Models;
using System.Collections.Generic;
using System.Linq;

namespace CMS.DataSource
{
    public class DSSkuLink
    {
        SQLiteConnection dbConn;
        public DSSkuLink()
        {
            dbConn = DependencyService.Get<ISQLite>().GetConnection();
        }
        public IEnumerable<SkuLink> GetAll()
        {
            return dbConn.Query<SkuLink>("Select * From [skulink]");
        }
        public SkuLink Get(string siteid, string brandid, int skuid, int sdate)
        {
            return dbConn.Table<SkuLink>().FirstOrDefault(d =>
                d.siteid == siteid &&
                d.brandid == brandid &&
                d.skuid == skuid &&
                d.linksdate == sdate
            );
        }
        public int Save(SkuLink skulink)
        {
            SkuLink skudetail = Get(skulink.siteid, skulink.brandid, skulink.skuid, skulink.linksdate);
            if (skudetail == null)
                return dbConn.Insert(skulink);
            else
            {
                skudetail = null;
                return dbConn.Update("update [skulink] set linkedate = " + skulink.linkedate + " where siteid = '" + skulink.siteid + "' and brandid = '" + skulink.brandid + "' and skuid = " + skulink.skuid + " and linksdate = " + skulink.linksdate);
            }
        }
        public int Delete(SkuLink skulink)
        {
            return dbConn.Delete(skulink);
        }
        public int Truncate()
        {
            return dbConn.DeleteAll<SkuLink>();
        }
    }
}
