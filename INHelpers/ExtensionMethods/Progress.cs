using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INHelpers.ExtensionMethods
{
    /// <summary>
    /// Represents the progress through a finite task with a known bound
    /// </summary>
    public struct Progress
    {
        public int Index { get; }

        public long Total { get; }

        public Progress(int index, long total)
        {
            Index = index;
            Total = total;
        }

        public static implicit operator Progress((int index, long total) tuple) => new Progress(tuple.index, tuple.total);

    }

}