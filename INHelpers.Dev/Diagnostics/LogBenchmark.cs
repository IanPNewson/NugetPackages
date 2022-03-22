using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace INHelpers.Diagnostics
{
    public static class Benchmark
    {

        public static IBenchmarkSection Start(string logFormat = "Time taken: {0}")
        {
            return new BenchmarkSectionImpl(logFormat);
        }

        private class BenchmarkSectionImpl : IBenchmarkSection
        {
            private string logFormat;
            private Stopwatch sw;

            public BenchmarkSectionImpl(string logFormat)
            {
                this.logFormat = logFormat;
                sw = new Stopwatch();
                sw.Start();
            }

            public void Dispose()
            {
                var elapsed = sw.Elapsed;
                Console.WriteLine(String.Format(logFormat, elapsed));
            }
        }

    }

    public interface IBenchmarkSection : IDisposable
    {
    }
}
