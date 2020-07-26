using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLoggerCL
{
    class Program
    {
        static void Main(string[] args)
        {
            string myname = "Hello";

            MotiveLogger.ConsoleLogger.INTERNAL_LOGGER.Info("Setting loglevel");
            MotiveLogger.LogManager.DefaultLogLevel = MotiveLogger.LogLevel.DEBUG;
            MotiveLogger.ConsoleLogger.INTERNAL_LOGGER.Info("LogLevel set");

            MotiveLogger.ConsoleLogger.INTERNAL_LOGGER.Info("Setting factory");
            MotiveLogger.LogManager.DefaultLogFactory = MotiveLogger.TraceLogger.Factory;
            MotiveLogger.ConsoleLogger.INTERNAL_LOGGER.Info("Factory set");

            MotiveLogger.ConsoleLogger.INTERNAL_LOGGER.Info("Logging to trace");
            MotiveLogger.LogManager.GetLogger("Trace").Error("This is a trace");
            MotiveLogger.ConsoleLogger.INTERNAL_LOGGER.Info("Trace logged");

            MotiveLogger.LogManager.GetLogger(typeof(Program), (name) => new MotiveLogger.ConsoleLogger(myname) ).Info("Hello world");

            var logger = MotiveLogger.LogManager.GetLogger(typeof(Program), MotiveLogger.ConsoleLogger.Factory);
            logger.Info("Goodbye cruel world");
        }
    }
}
