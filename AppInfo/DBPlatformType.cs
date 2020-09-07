using SQLite.Net.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppInfo
{
    public enum DBPlatformType : byte
    {
        SQLite = 1, SQLServer, MSAccess, Oracle
    }
    public class DBPlatformInfo : BaseInfo
    {
        public DBPlatformType PlatformType;


        public ISQLitePlatform SQLitePlatform;


        public string SQLDBPath;

    }
}
