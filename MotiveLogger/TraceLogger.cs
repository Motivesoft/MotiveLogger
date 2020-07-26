using System;
using System.Diagnostics;
using System.IO;

namespace MotiveLogger
{
    /// <summary>
    /// Concrete Logger that uses the .NET <code>TraceSource</code> functionality
    /// </summary>
    class TraceLogger : DefaultLogger
    {
        public static LogFactory Factory = (name) =>
        {
            return new TraceLogger(name);
        };

        private readonly TraceSource traceSource;

        public TraceLogger(string name) : base(name)
        {
            // Create the logger as a TraceSource and attach the singleton listener
            traceSource = new TraceSource(name);
            traceSource.Switch.Level = SourceLevels.All;

            // Create a threadsafe writer - but how can we parameterise the filename?
            TextWriter writer = FileManager.GetTextWriter(@"MotiveLogger.log");

            // Listener should be threadsafe here as we are using the same one for all TraceLoggers
            TraceListener listener = new FormattedTraceListener(writer);
            listener.TraceOutputOptions = TraceOptions.DateTime | TraceOptions.ProcessId | TraceOptions.ThreadId | TraceOptions.Timestamp | TraceOptions.LogicalOperationStack | TraceOptions.Callstack;

            // Define the contents scope - gather everything so that we can use whatever we want later
            traceSource.Listeners.Add(listener);
        }
        ~TraceLogger()
        {
            traceSource.Flush();
            traceSource.Listeners.Clear();
            traceSource.Close();
        }

        private void Log(TraceEventType eventType, string message)
        {
            traceSource.TraceEvent(eventType, NextId(eventType), message);
            traceSource.Flush();
        }
        private void Log(TraceEventType eventType, string format, params object[] args)
        {
            traceSource.TraceEvent(eventType, NextId(eventType, args), format, args);
            traceSource.Flush();
        }

        /// <summary>
        /// Create a unique ID, ever increasing from 0
        /// </summary>
        /// <param name="eventType">the event type of the log message</param>
        /// <param name="args">the args for the log message. May be empty</param>
        /// <returns>the next ID</returns>
        virtual protected int NextId(TraceEventType eventType, params object[] args)
        {
            return 0;
        }

        protected override void Log(LogLevel level, string message)
        {
            switch( level )
            {
                case LogLevel.ERROR:
                    Log(TraceEventType.Error, message);
                    break;
                case LogLevel.WARN:
                    Log(TraceEventType.Warning, message);
                    break;
                case LogLevel.INFO:
                    Log(TraceEventType.Information, message);
                    break;
                case LogLevel.DEBUG:
                    Log(TraceEventType.Verbose, message);
                    break;
            }
        }

        protected override void Log(LogLevel level, string format, params object[] args)
        {
            switch (level)
            {
                case LogLevel.ERROR:
                    Log(TraceEventType.Error, format, args);
                    break;
                case LogLevel.WARN:
                    Log(TraceEventType.Warning, format, args);
                    break;
                case LogLevel.INFO:
                    Log(TraceEventType.Information, format, args);
                    break;
                case LogLevel.DEBUG:
                    Log(TraceEventType.Verbose, format, args);
                    break;
            }
        }
    }

    /// <summary>
    /// A custom TraceListener that outputs log messages and context information with a familiar, one-line format
    /// </summary>
    class FormattedTraceListener : TextWriterTraceListener
    {
        // date, name, processId/threadId, level
        private const string STOCK_FORMAT = "{0:yyyy-MM-dd HH:mm:ss.fff} {1,-25} [{2,5}/{3,5}] {4,5}";
        public string MessageFormat
        {
            get;
            set;
        }

        public FormattedTraceListener(TextWriter writer) : base(writer)
        {
            MessageFormat = STOCK_FORMAT;
        }
        public FormattedTraceListener(TextWriter writer, string name) : base(writer, name)
        {
            MessageFormat = STOCK_FORMAT;
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
            // Don't dump id here - {5} - assume it is unused
            string fullFormat = GetFullFormat("{6}");
            Writer.WriteLine(string.Format(fullFormat,
                eventCache.DateTime.ToLocalTime(),
                source,
                eventCache.ProcessId,
                eventCache.ThreadId,
                GetEventType(eventType),
                id,
                message));
        }
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id)
        {
            // Do dump id here - assume it is used because there is no other purpose to this method
            string fullFormat = GetFullFormat("{5}");
            Writer.WriteLine(string.Format(fullFormat,
                eventCache.DateTime.ToLocalTime(),
                source,
                eventCache.ProcessId,
                eventCache.ThreadId,
                GetEventType(eventType),
                id));
        }
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
        {
            // Don't dump id here - {5} - assume it is unused
            string fullFormat = GetFullFormat("{6}");
            Writer.WriteLine(string.Format(fullFormat,
                eventCache.DateTime.ToLocalTime(),
                source,
                eventCache.ProcessId,
                eventCache.ThreadId,
                GetEventType(eventType),
                id,
                string.Format(format, args)));

            // If we're logging an exception, show the stack trace
            foreach (object arg in args)
            {
                if (arg is Exception)
                {
                    Exception ex = (Exception)arg;
                    //Writer.WriteLine(eventCache.Callstack);
                    //Writer.WriteLine("-> {0}", ex.Message);
                    Writer.WriteLine(ex.StackTrace);
                    break;
                }
            }
        }

        /// <summary>
        /// Take the standard format and append the specific parameters of each type of log output
        /// </summary>
        /// <param name="contentFormat">the bespoke format</param>
        /// <returns>a full format string</returns>
        private string GetFullFormat(string contentFormat)
        {
            return string.Format("{0} {1}", MessageFormat == null ? "" : MessageFormat, contentFormat == null ? "" : contentFormat);
        }

        /// <summary>
        /// Normalise the event type
        /// </summary>
        /// <param name="eventType">the TraceEventType</param>
        /// <returns>a stamdardised textual name</returns>
        private string GetEventType(TraceEventType eventType)
        {
            switch (eventType)
            {
                case TraceEventType.Error:
                    return LogLevel.ERROR.ToString();
                case TraceEventType.Warning:
                    return LogLevel.WARN.ToString();
                case TraceEventType.Information:
                    return LogLevel.INFO.ToString();
                case TraceEventType.Verbose:
                    return LogLevel.DEBUG.ToString();
                default:
                    return eventType.ToString();
            }
        }
    }
}
