using SQLite;
using Xamarin.Forms;
using CMS.ViewModels;
using CMS.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace CMS.DataSource
{
    public class DSSite
    {
        SQLiteConnection dbConn;
        public DSSite()
        {
            dbConn = DependencyService.Get<ISQLite>().GetConnection();
        }
        public IEnumerable<Site> GetAll()
        {
            return dbConn.Query<Site>("Select * From [site]");
        }
        public Site Get(string siteid)
        {
            return dbConn.Table<Site>().FirstOrDefault(x => x.siteid == siteid);
        }
        public IEnumerable<SiteList> getList(string userid)
        {
            string datenow = DateTime.Today.ToString("yyyyMMdd");
            return dbConn.Query<SiteList>("Select st.siteid id, st.sitename name From [site] st where st.siteflag = 1 and st.sitestatus = 1 and exists(select 1 from [profsitelink] link where link.siteid = st.siteid and link.userid = '"+userid+"' and link.linksdate <= "+datenow+ " and link.linkedate >= "+datenow+")");
        }
        public int Save(Site p_site)
        {
            Site site = Get(p_site.siteid);
            if (site == null)
                return dbConn.Insert(p_site);
            else
            {
                site = null;
                return dbConn.Update(p_site);
            }
        }
        public int Delete(Site site)
        {
            return dbConn.Delete(site);
        }
        public int Delete(string siteid)
        {
            return dbConn.Delete<Site>(siteid);
        }
        public int Truncate()
        {
            return dbConn.DeleteAll<Site>();
        }
    }
}
