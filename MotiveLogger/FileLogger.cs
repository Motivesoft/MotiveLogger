using System;
using System.Collections.Generic;
using System.Text;

namespace MotiveLogger
{
    public class FileLogger : DefaultLogger
    {
        public static LogFactory Factory = (name) =>
        {
            return new FileLogger(name);
        };

        public FileLogger(string name) : base(name)
        {
            // Nothing else to do
        }

        protected override void Log(LogLevel level, string message)
        {
            Console.WriteLine(string.Format("[{0}] {1}", level, message));
        }

        protected override void Log(LogLevel level, string format, params object[] args)
        {
            Console.WriteLine(string.Format("[{0}] {1}", level, string.Format(format, args)));
        }
    }
}
