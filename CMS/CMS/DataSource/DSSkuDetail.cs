using SQLite;
using Xamarin.Forms;
using CMS.Models;
using System.Collections.Generic;
using System.Linq;

namespace CMS.DataSource
{
    public class DSSkuDetail
    {
        SQLiteConnection dbConn;
        public DSSkuDetail()
        {
            dbConn = DependencyService.Get<ISQLite>().GetConnection();
        }
        public IEnumerable<SkuDetail> GetAll(int skuid)
        {
            return dbConn.Table<SkuDetail>().Where(d => d.skuid == skuid);
        }
        public SkuDetail Get(int skuid, int skudetid)
        {
            return dbConn.Table<SkuDetail>().FirstOrDefault(x => x.skuid == skuid && x.skudetid == skudetid);
        }
        public int Save(SkuDetail skud)
        {
            SkuDetail skudetail = Get(skud.skuid, skud.skudetid);
            if (skudetail == null)
                return dbConn.Insert(skud);
            else
            {
                skudetail = null;
                return dbConn.Update(skud);
            }
        }
        public int Delete(SkuDetail skudetail)
        {
            return dbConn.Delete(skudetail);
        }
        public int Truncate()
        {
            return dbConn.DeleteAll<SkuDetail>();
        }
    }
}
