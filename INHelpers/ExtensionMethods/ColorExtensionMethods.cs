using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace INHelpers.ExtensionMethods
{
    public static class ColorExtensionMethods
    {

        public static string ToHex(this Color clr)
        {
            return $"#{clr.R:X2}{clr.G:X2}{clr.B:X2}";
        }

    }
}
