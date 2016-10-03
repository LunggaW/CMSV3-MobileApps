using SQLite;
using Xamarin.Forms;
using CMS.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace CMS.DataSource
{
    public class DSTransaction
    {
        SQLiteConnection dbConn;
        public DSTransaction()
        {
            dbConn = DependencyService.Get<ISQLite>().GetConnection();
        }
        public IEnumerable<Transaction> GetAll(string userid)
        {
            return dbConn.Table<Transaction>().Where(x =>
                x.transcreby == userid
            );
        }
        public IEnumerable<Transaction> GetAllBySite(string userid, string siteid)
        {
            return dbConn.Table<Transaction>().Where(x =>
                x.transcreby == userid &&
                x.transsite == siteid
            );
        }
        public IEnumerable<Transaction> GetAllByDate(string userid, string siteid, int transdate)
        {
            return dbConn.Table<Transaction>().Where(x =>
                x.transcreby == userid &&
                x.transsite == siteid &&
                x.transdate == transdate
            );
        }
        public List<Transaction> GetTobeSync(string userid, int counter)
        {
            List<Transaction> all = dbConn.Table<Transaction>().Where(x => x.transcreby == userid && x.transflag == 0).ToList();

            if (counter == 0 || all.Count() == 0)
            {
                return all;
            }
            else
            {
                List<Transaction> retval = new List<Transaction>();
                int i = 0;
                foreach (Transaction ts in all)
                {
                    if (i < counter)
                    {
                        retval.Add(ts);
                    }
                }

                return retval;
            }
        }

        public Transaction Get(int transid)
        {
            return dbConn.Table<Transaction>().FirstOrDefault(x => x.transid == transid);
        }
        public int Save(Transaction p_trans)
        {
            Transaction trans = Get(p_trans.transid);
            if (trans == null)
                return dbConn.Insert(p_trans);
            else
            {
                trans = null;
                return dbConn.Update(p_trans);
            }
        }
        public int HasSales(string userid)
        {
            return dbConn.Table<Transaction>().Where(x => x.transcreby == userid && x.transflag == 0).Count();
        }

        public int Delete(Transaction trans)
        {
            return dbConn.Delete(trans);
        }
        public int Delete(string transid)
        {
            return dbConn.Delete<Transaction>(transid);
        }
        public int Truncate()
        {
            return dbConn.DeleteAll<Transaction>();
        }
    }
}
