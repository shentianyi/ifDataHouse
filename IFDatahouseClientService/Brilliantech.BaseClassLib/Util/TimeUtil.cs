using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brilliantech.BaseClassLib.Util
{
    public class TimeUtil
    {
        /// <summary>
        /// get milliseconds by date time
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static long GetMilliseconds(DateTime date) {
            return (long)(date-new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        public static string GetDateTimeInMil() {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }
    }
}
