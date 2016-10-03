using CMS.Windows;
using Windows.Storage;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(SqliteService))]
namespace CMS.Windows
{
    public class SqliteService : ISQLite
    {
        public SqliteService()
        {

        }
        public SQLite.SQLiteConnection GetConnection()
        {
            var sqliteFilename = "cms.db3";
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename);
            var conn = new SQLite.SQLiteConnection(path);
            return conn;
        }
    }
}
