using SQLite;

namespace NoteTakingApp.Core.Interfaces
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();

        SQLiteAsyncConnection GetAsyncConnection();

        string GetPath();
    }
}
