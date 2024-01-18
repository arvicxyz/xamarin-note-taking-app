using NoteTakingApp.Constants;
using NoteTakingApp.Core.Interfaces;
using NoteTakingApp.iOS.Dependencies;
using SQLite;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteiOS))]
namespace NoteTakingApp.iOS.Dependencies
{
    public class SQLiteiOS : ISQLite
    {
        public SQLiteiOS()
        {
        }

        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(GetPath());
        }

        public SQLiteAsyncConnection GetAsyncConnection()
        {
            return new SQLiteAsyncConnection(GetPath());
        }

        public string GetPath()
        {
            var fileName = AppConstants.LocalDbFile;
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libraryPath = Path.Combine(documentsPath, "..", "Library");
            return Path.Combine(libraryPath, fileName);
        }
    }
}
