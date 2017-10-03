using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using StockLibrary.DataAccess;

namespace StockLibrary
{
    public class GlobalConfig
    {
        public static IDataConnection connection { get; private set; }

        public static void InitializeConnections()
        {
           SqlConnector sql = new SqlConnector();
           connection = sql;
        }

        public static string connecString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
