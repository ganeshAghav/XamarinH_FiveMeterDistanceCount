using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelViewUI
{
    public interface IPCVCUIService
    {
        bool UpdateScriptToDB(string qr);
        string GetDBVersion();
        bool CleanTable(string DbVersion);
        bool DeleteOldData(int days);

    }
}
