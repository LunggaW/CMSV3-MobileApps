using SQLite;
using Xamarin.Forms;
using CMS.Models;
using System.Collections.Generic;
using System.Linq;

namespace CMS.DataSource
{
    public class DSProfileSite
    {
        SQLiteConnection dbConn;
        public DSProfileSite()
        {
            dbConn = DependencyService.Get<ISQLite>().GetConnection();
        }
        public IEnumerable<ProfileSite> GetAll()
        {
            return dbConn.Query<ProfileSite>("Select * From [profilesite]");
        }
        public ProfileSite Get(string profsiteid)
        {
            return dbConn.Table<ProfileSite>().FirstOrDefault(x => x.profsiteid == profsiteid);
        }
        public int Save(ProfileSite p_profile)
        {
            ProfileSite profilesite = Get(p_profile.profsiteid);
            if (profilesite == null)
                return dbConn.Insert(p_profile);
            else
            {
                profilesite = null;
                return dbConn.Update(p_profile);
            }
        }
        public int Delete(ProfileSite profsite)
        {
            return dbConn.Delete(profsite);
        }
        public int Delete(string profsiteid)
        {
            return dbConn.Delete<ProfileSite>(profsiteid);
        }
        public int Truncate()
        {
            return dbConn.DeleteAll<ProfileSite>();
        }
    }
}
