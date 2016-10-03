using SQLite;
using Xamarin.Forms;
using CMS.Models;
using CMS.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace CMS.DataSource
{
    public class DSSkuHeader
    {
        SQLiteConnection dbConn;
        public DSSkuHeader()
        {
            dbConn = DependencyService.Get<ISQLite>().GetConnection();
        }
        public IEnumerable<SkuHeader> GetAll()
        {
            return dbConn.Query<SkuHeader>("Select * From [skuheader]");
        }
        public SkuHeader Get(int skuid)
        {
            return dbConn.Table<SkuHeader>().FirstOrDefault(x => x.skuid == skuid);
        }
        public IEnumerable<SkuList> GetList(string siteid, string brandid, int salesdate)
        {
            return dbConn.Query<SkuList>("Select skuh.skuid id, skuh.skusdesc name From [skuheader] skuh where skuh.skusdate <= " + salesdate + " and skuh.skuedate >= " + salesdate + " and exists(Select 1 From [skulink] link where skuh.skuid = link.skuid and link.brandid = '" + brandid +"' and link.siteid = '" + siteid + "' and link.linksdate <= " + salesdate + " and link.linkedate >= " + salesdate + ")");
        }
        public int Save(SkuHeader skuh)
        {
            SkuHeader sku = Get(skuh.skuid);
            if (sku == null)
                return dbConn.Insert(skuh);
            else
            {
                sku = null;
                return dbConn.Update(skuh);
            }
        }
        public int Delete(SkuHeader skuheader)
        {
            dbConn.Execute("delete from [skudetail] where skuid = ?", skuheader.skuid);
            dbConn.Execute("delete from [skulink] where skuid = ?", skuheader.skuid);
            return dbConn.Delete(skuheader);
        }
        public int Delete(string skuid)
        {
            dbConn.Execute("delete from [skudetail] where skuid = ?", skuid);
            dbConn.Execute("delete from [skulink] where skuid = ?", skuid);
            return dbConn.Delete<SkuHeader>(skuid);
        }
        public int Truncate()
        {
            return dbConn.DeleteAll<SkuHeader>();
        }
    }
}
