namespace MotiveLogger
{
    public interface Logger
    {
        /// <summary>
        /// Log an error message
        /// </summary>
        /// <param name="message">The text of the message</param>
        void Error(string message);

        /// <summary>
        /// Log an error message with a formatted string and values.
        /// <para>If the last of <code>args</code> is a <code>System.Exception</code> then the logger may elect to record
        /// the exception's stacktrace.</para>
        /// </summary>
        /// <param name="format">the message format</param>
        /// <param name="args">the values to insert into the format string</param>
        void Error(string format, params object[] args);

        /// <summary>
        /// Log an warning message
        /// </summary>
        /// <param name="message">The text of the message</param>
        void Warning(string message);

        /// <summary>
        /// Log a warning message with a formatted string and values.
        /// <para>If the last of <code>args</code> is a <code>System.Exception</code> then the logger may elect to record
        /// the exception's stacktrace.</para>
        /// </summary>
        /// <param name="format">the message format</param>
        /// <param name="args">the values to insert into the format string</param>
        void Warning(string format, params object[] args);

        /// <summary>
        /// Log an information message
        /// </summary>
        /// <param name="message">The text of the message</param>
        void Info(string message);

        /// <summary>
        /// Log an information message with a formatted string and values.
        /// <para>If the last of <code>args</code> is a <code>System.Exception</code> then the logger may elect to record
        /// the exception's stacktrace.</para>
        /// </summary>
        /// <param name="format">the message format</param>
        /// <param name="args">the values to insert into the format string</param>
        void Info(string format, params object[] args);

        /// <summary>
        /// Log a debug message
        /// </summary>
        /// <param name="message">The text of the message</param>
        void Debug(string message);

        /// <summary>
        /// Log a debug message with a formatted string and values.
        /// <para>If the last of <code>args</code> is a <code>System.Exception</code> then the logger may elect to record
        /// the exception's stacktrace.</para>
        /// </summary>
        /// <param name="format">the message format</param>
        /// <param name="args">the values to insert into the format string</param>
        void Debug(string format, params object[] args);

        /// <summary>
        /// The name of the logger
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// The current log level
        /// </summary>
        LogLevel LogLevel
        {
            get;
            set;
        }
    }
}
