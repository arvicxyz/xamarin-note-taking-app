using NoteTakingApp.Constants;
using NoteTakingApp.Core.Interfaces;
using NoteTakingApp.Droid.Dependencies;
using SQLite;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteAndroid))]
namespace NoteTakingApp.Droid.Dependencies
{
    public class SQLiteAndroid : ISQLite
    {
        public SQLiteAndroid()
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
            return Path.Combine(documentsPath, fileName);
        }
    }
}
