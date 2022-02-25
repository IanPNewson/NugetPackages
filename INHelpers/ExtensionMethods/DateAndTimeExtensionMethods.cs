using System.Text;
using System.Threading.Tasks;

namespace INHelpers.ExtensionMethods
{
    public static class DateAndTimeExtensionMethods
    {

        //From https://stackoverflow.com/questions/16689468/how-to-produce-human-readable-strings-to-represent-a-timespan/21649465
        public static string ToDisplayFormat(this TimeSpan t)
        {
            if (t.TotalSeconds <= 1)
            {
                return $@"{t:s\.ff} s";
            }
            if (t.TotalMinutes <= 1)
            {
                return $@"{t:%s} s";
            }
            if (t.TotalHours <= 1)
            {
                return $@"{t:%m} m";
            }
            if (t.TotalDays <= 1)
            {
                return $@"{t:%h} h";
            }

            return $@"{t:%d} d";
        }

    }
}
