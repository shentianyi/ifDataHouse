using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Brilliantech.Tsk.Manage.WebApp.Util
{
    public class DbUtil
    {
        private static string connectionString;

        static DbUtil() {
            connectionString = ConfigurationManager.ConnectionStrings["Leoni_Tsk_JNConnectionString"].ConnectionString;
        }
        public static string ConnectionString
        {
            get { return DbUtil.connectionString; } 
        }
    }
}