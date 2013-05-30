using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brilliantech.DatahouseService.Util
{
    public class TimeUtil
    {
        public static long GetMilliseconds(DateTime date) {
            return (long)(date-new DateTime(1970, 1, 1)).TotalMilliseconds;
        }
    }
}
