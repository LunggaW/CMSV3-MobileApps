using SQLite;
using Xamarin.Forms;
using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMS.DataSource
{
    public class DSUser
    {
        SQLiteConnection dbConn;
        public DSUser()
        {
            dbConn = DependencyService.Get<ISQLite>().GetConnection();
        }
        public IEnumerable<User> GetAll()
        {
            return dbConn.Query<User>("Select * From [user]");
        }
        public User Get(string userid)
        {
            return dbConn.Table<User>().FirstOrDefault(x => x.userid == userid);
        }
        public User Get(string userid, string password)
        {
            int datenow = Convert.ToInt32(DateTime.Today.ToString("yyyyMMdd"));

            return dbConn.Table<User>().FirstOrDefault(d =>
                d.userid == userid &&
                d.password == password &&
                d.userstatus == 1 &&
                d.usertype == 1 &&
                //d.logged == 0 &&
                d.usersdate <= datenow &&
                d.useredate >= datenow
            );
        }
        public User getLoggedUser()
        {
            int datenow = Convert.ToInt32(DateTime.Today.ToString("yyyyMMdd"));
            return dbConn.Table<User>().FirstOrDefault(d => 
                d.userstatus == 1 && 
                d.usertype == 1 && 
                d.logged == 1 && 
                d.usersdate <= datenow &&
                d.useredate >= datenow
            );
        }
        public int Save(User p_user)
        {
            User user = Get(p_user.userid);
            if (user == null)
                return dbConn.Insert(p_user);
            else
            {
                user = null;
                return dbConn.Update(p_user);
            }
        }
        public int Delete(User user)
        {
            return dbConn.Delete(user);
        }
        public int Delete(string userid)
        {
            return dbConn.Delete<User>(userid);
        }
        public int Truncate()
        {
            return dbConn.DeleteAll<User>();
        }
    }
}
