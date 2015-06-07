using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus.BO.Extensions
{
    public static class UnixTimeHelper
    {
        public static DateTime GetDateTimeFromUnixTimestamp(this long timestamp)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(timestamp);
        }

        public static long ToUnixTimestamp(this DateTime dt)
        {
            TimeSpan period = dt - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64(period.TotalSeconds);
        }
    }
}
