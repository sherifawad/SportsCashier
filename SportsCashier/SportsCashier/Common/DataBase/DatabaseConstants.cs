using SportsCashier.Common.DataBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace SportsCashier.DataBase
{
    public static class DatabaseConstants
    {
        public const string DatabaseFilename = "SportCashierSQLite.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {

                var basePath = DependencyService.Get<IDownloadPath>().Get();
                //var basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                //var basePath = Xamarin.Essentials.FileSystem.AppDataDirectory;
                return Path.Combine(basePath, DatabaseFilename);

            }
        }
    }
}
