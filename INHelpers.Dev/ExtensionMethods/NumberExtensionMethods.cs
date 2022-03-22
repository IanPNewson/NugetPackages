using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INHelpers.ExtensionMethods
{
    public static class NumberExtensionMethods
    {

        public static string ToHumanByteSize(this int bytesLength) => ToHumanByteSize((long)bytesLength);

        //From https://stackoverflow.com/questions/281640/how-do-i-get-a-human-readable-file-size-in-bytes-abbreviation-using-net
        public static string ToHumanByteSize(this long bytesLength)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytesLength;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return String.Format("{0:0.##} {1}", len, sizes[order]);
        }

    }
}
