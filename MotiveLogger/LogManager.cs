using System;
using System.Collections.Concurrent;

namespace MotiveLogger
{
    public delegate Logger LogFactory(string name);

    /// <summary>
    /// Manages Loggers. Each Logger will have a name, there will only be one Logger with that name.
    /// </summary>
    class LogManager
    {
        private static readonly ConcurrentDictionary<string, Logger> loggers = new ConcurrentDictionary<string, Logger>();

        public static LogFactory DefaultLogFactory;

        public static LogLevel DefaultLogLevel = LogLevel.WARN;

        static LogManager()
        {
            DefaultLogFactory = ConsoleLogger.Factory;
        }

        /// <summary>
        /// Create or return a Logger with the provided name
        /// </summary>
        /// <param name="name">the logger name</param>
        /// <returns>A logger with the provided name</returns>
        public static Logger GetLogger(string name)
        {
            return GetLogger(name, DefaultLogFactory);
        }

        /// <summary>
        /// Create or return a Logger with the name of the provided type
        /// </summary>
        /// <param name="type">a type on which to base the logger</param>
        /// <returns>A logger with the provided type name</returns>
        public static Logger GetLogger(Type type)
        {
            return GetLogger(type.FullName, DefaultLogFactory);
        }

        /// <summary>
        /// Create or return a Logger with the provided name
        /// </summary>
        /// <param name="name">the logger name</param>
        /// <param name="logFactory">the factory for the logger</param>
        /// <returns>A logger with the provided name</returns>
        public static Logger GetLogger(string name, LogFactory logFactory)
        {
            return loggers.GetOrAdd(name, (loggerName) => logFactory(loggerName));
        }

        /// <summary>
        /// Create or return a Logger with the name of the provided type
        /// </summary>
        /// <param name="type">a type on which to base the logger</param>
        /// <param name="logFactory">the factory for the logger</param>
        /// <returns>A logger with the provided type name</returns>
        public static Logger GetLogger(Type type, LogFactory logFactory)
        {
            return GetLogger(type.FullName, logFactory);
        }
    }
}
