using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomDB
{
    public static class AtomManager
    {
        /// <summary>
        /// Checks if there will be a performance increase in using multi threaded Atom.
        /// </summary>
        public static bool UseMultiThreaded
        {
            get;
            private set;
        }

        internal static bool Initialized = false;

        public static void Initialize()
        {
            Initialized = true;
            return; // todo: fix
            Atom a = Atom.Create();
            AtomMT b = AtomMT.Create();
            Stopwatch sw = new();
            sw.Start();
            a.SetBytes(new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 });
            var aCheckBytes = a.GetBytes(new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 });
            sw.Stop();
            var aMS = sw.ElapsedMilliseconds;
            sw.Reset();
            sw.Start();
            b.SetBytes(new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 });
            var bCheckBytes = b.GetBytes(new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 });
            sw.Stop();
            var bMS = sw.ElapsedMilliseconds;
            UseMultiThreaded = bMS < aMS;
        }
    }
}
