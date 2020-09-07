using AppInfo;
using SQLite.Net;
using SQLite.Net.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteDAL
{
    internal class CommonDAL : IDisposable
    {
        private ISQLitePlatform sqlitePlatform = null;
        private string sqliteDBPath = null;


        internal CommonDAL(DBPlatformInfo dbPlatform)
        {
            sqliteDBPath = dbPlatform.SQLDBPath;
            sqlitePlatform = dbPlatform.SQLitePlatform;
        }

        public SQLiteConnection GetSQLiteConnection()
        {
            SQLiteConnection dbConn = null;
            try
            {
                dbConn = new SQLiteConnection(sqlitePlatform, sqliteDBPath, false);
            }
            catch
            {
                throw;
            }
            finally
            {

            }
            return dbConn;
        }

        public void Dispose()
        {
            sqlitePlatform = null;
        }

        internal static string GetDate(string dateToConvert)
        {
            string resultDateStr = string.Empty;
            DateTime resultDate = DateTime.MinValue;
            string[] expectedDateFormat = new string[] { "dd-MM-yyyy", "d-MM-yyyy", "dd-M-yyyy", "d-M-yyyy", "dd-MM-yy", "d-MM-yy", "dd-M-yy", "d-M-yy", "dd/MM/yyyy HH:mm:ss" };

            if (DateTime.TryParseExact(dateToConvert, expectedDateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out resultDate))
            {
                resultDateStr = resultDate.ToString("dd/MMM/yyyy HH:mm:ss");
            }

            else
            {
                resultDateStr = Convert.ToDateTime(resultDate).ToString("dd/MMM/yyyy HH:mm:ss");
            }

            return resultDateStr;
        }
    }
}
