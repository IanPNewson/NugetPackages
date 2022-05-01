using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INHelpers.ExtensionMethods
{
    public static class StringExtensionMethods
    {

        public static string ToPathName(this string str)
        {
            return new string(str.Except(Path.GetInvalidFileNameChars()).ToArray());
        }

    }
}
