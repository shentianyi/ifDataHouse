using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace Brilliantech.Qmail.Framework.Util
{
    public class SQLUtil
    {
        public static SqlConnection GetConnection(string connStr)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[connStr].ToString());
            return conn;
        }
    }
}
