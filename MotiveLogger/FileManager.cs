using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MotiveLogger
{
    /// <summary>
    /// <para>TL;DR Manages files, perhaps unsurprisingly</para>
    /// </summary>
    class FileManager
    {
        private static readonly ConcurrentDictionary<string, TextWriter> writers = new ConcurrentDictionary<string, TextWriter>();

        public static TextWriter GetTextWriter(string filename, bool append = false)
        {
            string fullPath = Path.GetFullPath(filename);
            return writers.GetOrAdd(fullPath,  (name) => 
            {
                ConsoleLogger.INTERNAL_LOGGER.Info("Creating: {0}", name);
                // Create a threadsafe writer - but how can we parameterise the filename?
                return TextWriter.Synchronized(File.CreateText(name));
            });
        }
    }
}
