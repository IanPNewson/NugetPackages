﻿using System;
using System.Collections.Generic;
using System.Text;

namespace INHelpers.ExtensionMethods
{
    public static class NumberExtensionMethods
    {

        /// <summary>
        /// Returns text summary of the progress to the total
        /// </summary>
        public static string ToProgressSummary(this int i, int total)
        {
            return $"{i}/{total} ({i.ToPercent(total)*100:0.#}%)";
        }

        /// <summary>
        /// Returns the percentage this number is of the total. 0 <= x <= 1
        /// </summary>
        public static double ToPercent(this int i, int total)
        {
            if (i < 0) throw new ArgumentException("i must be >= 0", nameof(i));
            if (total < 0) throw new ArgumentException("total must be >= 0", nameof(i));
            if (i > total) throw new ArgumentException("i must be <= total", nameof(i));
            return i / (double)total;
        }

    }
}
