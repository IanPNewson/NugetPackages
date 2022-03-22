using INHelpers.ExtensionMethods;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace INHelpers.ExtensionMethods
{
    public class ProgressUpdater<T> : IEnumerable<T>
    {

        public static readonly Action<ProgressUpdate<T>> DefaultUpdateHandler = t => Console.WriteLine(t.Index.ToProgressSummary(t.Total));

        public IEnumerable<T> Items { get; }
        public int ProgressEvery { get; }
        public int Total { get; private set; }
        public Action<ProgressUpdate<T>> OnUpdate { get; }
        public int Index { get; private set; }

        public ProgressUpdater(IEnumerable<T> items, int progressEvery = 10, int? total = null, Action<ProgressUpdate<T>>? onUpdate = null)
        {
            Items = items;
            ProgressEvery = progressEvery;
            Total = total ?? items.Count();
            OnUpdate = onUpdate ?? DefaultUpdateHandler;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var sw = new Stopwatch();
            sw.Start();

            var firstSet = false;
            T? first = default(T);

            foreach (var item in Items)
            {
                if (!firstSet)
                {
                    first = item;
                    firstSet = true;
                }

                yield return item;
                ++Index;
                if ((Index % ProgressEvery) == 0 || (Index + 1) == Total)
                {
                    var update = new ProgressUpdate<T>(first, item, Index, Total, sw.Elapsed);
                    this?.OnUpdate(update);

                    sw.Restart();
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

    }

    public struct ProgressUpdate<T>
    {
        public T First { get; }
        public T Last { get; }

        public int Index { get; }

        public int Total { get; }

        public TimeSpan Elapsed { get; }

        public ProgressUpdate(T first, T last, int index, int total, TimeSpan elapsed)
        {
            First = first;
            Last = last;
            Index = index;
            Total = total;
            Elapsed = elapsed;
        }

        public static explicit operator Progress(ProgressUpdate<T> progress) => new Progress(progress.Index, progress.Total);
    }

    public static class EnumerableExtensionMethods
    {

        public static ProgressUpdater<T> GetProgressUpdates<T>(
            this IEnumerable<T> items, 
            int progressEvery = 10,
            Action<ProgressUpdate<T>>? onUpdate = null,
            int? total = null)
        {
            return new ProgressUpdater<T>(items, progressEvery, total, onUpdate);
        }

    }
}
