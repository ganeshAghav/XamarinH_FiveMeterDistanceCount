using AppInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteDAL
{
    public class PCVCDAL
    {
        int Recount;
        string ReDate;
        private DBPlatformInfo dbPlatform = null;

        public PCVCDAL()
        {

        }
        
        public PCVCDAL(DBPlatformInfo dbPlatform)
        {
            this.dbPlatform = dbPlatform;
        }

       
    }
}
