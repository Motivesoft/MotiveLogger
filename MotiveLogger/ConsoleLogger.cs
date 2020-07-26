using System;
using System.Collections.Generic;
using System.Text;

namespace MotiveLogger
{
    public class ConsoleLogger : DefaultLogger
    {
        public static readonly Logger INTERNAL_LOGGER = new ConsoleLogger("INTERNAL");

        public static LogFactory Factory = (name) =>
        {
            return new ConsoleLogger(name);
        };

        public ConsoleLogger(string name) : base( name )
        {
            // Nothing else to do
        }

        protected override void Log(LogLevel level, string message)
        {
            Console.WriteLine(string.Format("[{0}] {1}",level,message));
        }

        protected override void Log(LogLevel level, string format, params object[] args)
        {
            Console.WriteLine(string.Format("[{0}] {1}", level, string.Format(format,args)));
        }
    }
}
