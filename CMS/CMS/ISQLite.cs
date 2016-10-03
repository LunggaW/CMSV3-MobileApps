using SQLite;

namespace CMS
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
