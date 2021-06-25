using INHelpers.ExtensionMethods;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INHelpers.Diagnostics
{
    /// <summary>
    /// A collection of stop watches, each measuring a different set of time
    /// </summary>
    public class Stopwatches : IReadOnlyDictionary<string, Stopwatch>, IStopwatches
    {
        /// <summary>
        /// Starts the time ticking for the specified name
        /// </summary>
        public IDisposable Start(string name)
        {
            var watch = this[name];
            watch.Start();
            return new StopwatchSegment(this, name);
        }

        /// <summary>
        /// Starts the time ticking for the specified name
        /// </summary>
        public IStopwatches Stop(string name)
        {
            var watch = this[name];
            watch.Stop();
            return this;
        }

        /// <summary>
        /// Resets all stopwatches in this collection, setting them to zero
        /// </summary>
        public IStopwatches ResetAll()
        {
            foreach (var watch in Values)
                watch.Reset();
            return this;
        }

        /// <summary>
        /// Gets a human readable summary of the elapsed time for all stopwatches
        /// </summary>
        /// <param name="iterations">If provided, this is the number of iterations the stopwatch applies and therefore the total time will be divided by this to produce the average</param>
        public string Summary(int? iterations = null)
        {

            return string.Join("\r\n",
                this.Select(kvp => $"{kvp.Key}: {TimeSpan.FromMilliseconds(kvp.Value.ElapsedMilliseconds / (iterations ?? kvp.Value.ElapsedMilliseconds)).ToDisplayFormat()}"));
        }

        /// <summary>
        /// Used so regions to be measured can be enclosed within a using
        /// </summary>
        private class StopwatchSegment : IDisposable
        {
            private readonly Stopwatches watches;
            private readonly string name;

            public StopwatchSegment(Stopwatches watches, string name)
            {
                this.watches = watches;
                this.name = name;
            }

            public void Dispose()
            {
                watches.Stop(name);
            }
        }

        #region IDictionary

        private Dictionary<string, Stopwatch> _Watches = new Dictionary<string, Stopwatch>();

        public Stopwatch this[string key]
        {
            get
            {
                Stopwatch watch;
                if (!_Watches.TryGetValue(key, out watch))
                {
                    watch = new Stopwatch();
                    _Watches.Add(key, watch);
                }
                return watch;
            }
        }

        public IEnumerable<string> Keys => _Watches.Keys;

        public IEnumerable<Stopwatch> Values => _Watches.Values;

        public int Count => _Watches.Count;

        public bool ContainsKey(string key) => _Watches.ContainsKey(key);

        public IEnumerator<KeyValuePair<string, Stopwatch>> GetEnumerator() => _Watches.GetEnumerator();

        public bool TryGetValue(string key, out Stopwatch value)
            => _Watches.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() => _Watches.GetEnumerator();

        #endregion

    }
}
