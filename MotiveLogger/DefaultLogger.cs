namespace MotiveLogger
{
    /// <summary>
    /// Generic API for logging services
    /// </summary>
    public abstract class DefaultLogger : Logger
    {
        protected DefaultLogger(string name)
        {
            Name = name;
            LogLevel = LogManager.DefaultLogLevel;
        }

        ~DefaultLogger()
        {

        }

        /// <summary>
        /// Log a message at the specified log level
        /// </summary>
        /// <param name="level">The log level</param>
        /// <param name="message">The text of the message</param>
        protected abstract void Log(LogLevel level, string message);

        /// <summary>
        /// Log a formatted message at the specified log level
        /// </summary>
        /// <param name="level">The log level</param>
        /// <param name="format">the message format</param>
        /// <param name="args">the values to insert into the format string</param>
        protected abstract void Log(LogLevel level, string format, params object[] args);

        /// <summary>
        /// Log an error message
        /// </summary>
        /// <param name="message">The text of the message</param>
        virtual public void Error(string message)
        {
            if( LogLevel.ERROR <= LogLevel )
            {
                Log(LogLevel.ERROR, message);
            }
        }

        /// <summary>
        /// Log an error message with a formatted string and values.
        /// <para>If the last of <code>args</code> is a <code>System.Exception</code> then the logger may elect to record
        /// the exception's stacktrace.</para>
        /// </summary>
        /// <param name="format">the message format</param>
        /// <param name="args">the values to insert into the format string</param>
        virtual public void Error(string format, params object[] args)
        {
            if (LogLevel.ERROR <= LogLevel)
            {
                Log(LogLevel.ERROR, format, args);
            }
        }

        /// <summary>
        /// Log an warning message
        /// </summary>
        /// <param name="message">The text of the message</param>
        virtual public void Warning(string message)
        {
            if (LogLevel.WARN <= LogLevel)
            {
                Log(LogLevel.WARN, message);
            }
        }

        /// <summary>
        /// Log a warning message with a formatted string and values.
        /// <para>If the last of <code>args</code> is a <code>System.Exception</code> then the logger may elect to record
        /// the exception's stacktrace.</para>
        /// </summary>
        /// <param name="format">the message format</param>
        /// <param name="args">the values to insert into the format string</param>
        virtual public void Warning(string format, params object[] args)
        {
            if (LogLevel.WARN <= LogLevel)
            {
                Log(LogLevel.WARN, format, args);
            }
        }

        /// <summary>
        /// Log an information message
        /// </summary>
        /// <param name="message">The text of the message</param>
        virtual public void Info(string message)
        {
            if (LogLevel.INFO <= LogLevel)
            {
                Log(LogLevel.INFO, message);
            }
        }

        /// <summary>
        /// Log an information message with a formatted string and values.
        /// <para>If the last of <code>args</code> is a <code>System.Exception</code> then the logger may elect to record
        /// the exception's stacktrace.</para>
        /// </summary>
        /// <param name="format">the message format</param>
        /// <param name="args">the values to insert into the format string</param>
        virtual public void Info(string format, params object[] args)
        {
            if (LogLevel.INFO <= LogLevel)
            {
                Log(LogLevel.INFO, format, args);
            }
        }

        /// <summary>
        /// Log a debug message
        /// </summary>
        /// <param name="message">The text of the message</param>
        virtual public void Debug(string message)
        {
            if (LogLevel.DEBUG <= LogLevel)
            {
                Log(LogLevel.DEBUG, message);
            }
        }

        /// <summary>
        /// Log a debug message with a formatted string and values.
        /// <para>If the last of <code>args</code> is a <code>System.Exception</code> then the logger may elect to record
        /// the exception's stacktrace.</para>
        /// </summary>
        /// <param name="format">the message format</param>
        /// <param name="args">the values to insert into the format string</param>
        virtual public void Debug(string format, params object[] args)
        {
            if (LogLevel.DEBUG <= LogLevel)
            {
                Log(LogLevel.DEBUG, format, args);
            }
        }

        /// <summary>
        /// The name of the logger
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// The current log level
        /// </summary>
        public LogLevel LogLevel
        {
            get;
            set;
        }
    }
}
