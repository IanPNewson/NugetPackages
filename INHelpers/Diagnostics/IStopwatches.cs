using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace INHelpers.Diagnostics
{

    /// <summary>
    /// A collection of stop watches, each measuring a different set of time
    /// </summary>
    public interface IStopwatches
    {
        /// <summary>
        /// Number of stopwatches being monitored
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Resets all stopwatches in this collection, setting them to zero
        /// </summary>
        IStopwatches ResetAll();

        /// <summary>
        /// Starts the time ticking for the specified name
        /// </summary>
        IDisposable Start(string name);

        /// <summary>
        /// Starts the time ticking for the specified name
        /// </summary>
        IStopwatches Stop(string name);
        string Summary(int? iterations = null);
    }
}