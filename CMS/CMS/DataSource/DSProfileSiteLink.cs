using SQLite;
using Xamarin.Forms;
using CMS.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace CMS.DataSource
{
    public class DSProfileSiteLink
    {
        SQLiteConnection dbConn;
        public DSProfileSiteLink()
        {
            dbConn = DependencyService.Get<ISQLite>().GetConnection();
        }
        public IEnumerable<ProfileSiteLink> GetAll(string siteid, string userid)
        {
            int datenow = Convert.ToInt32(DateTime.Today.ToString("yyyyMMdd"));
            return dbConn.Table<ProfileSiteLink>().Where(d =>
                d.userid == userid &&
                d.siteid == siteid &&
                d.linksdate <= datenow &&
                d.linkedate >= datenow
            );
        }
        public ProfileSiteLink Get(string userid, string siteid, int sdate)
        {
            return dbConn.Table<ProfileSiteLink>().FirstOrDefault(d =>
                d.userid == userid &&
                d.siteid == siteid &&
                d.linksdate == sdate
            );
        }
        public int Save(ProfileSiteLink p_sitelink)
        {
            ProfileSiteLink profsitelink = Get(p_sitelink.userid, p_sitelink.siteid, p_sitelink.linksdate);
            if (profsitelink == null)
                return dbConn.Insert(p_sitelink);
            else
            {
                profsitelink = null;
                return dbConn.Execute("update profsitelink set linkedate = " + p_sitelink.linkedate + " where userid = '"+ p_sitelink.userid+"' and siteid = '"+ p_sitelink.siteid+"' and linksdate = " + p_sitelink.linksdate);
            }
        }
        public int Delete(ProfileSiteLink profsitelink)
        {
            return dbConn.Delete(profsitelink);
        }
        public int Truncate()
        {
            return dbConn.DeleteAll<ProfileSiteLink>();
        }
    }
}
