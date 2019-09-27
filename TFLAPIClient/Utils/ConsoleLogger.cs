using System;
using System.Threading.Tasks;

namespace TflApiClient.Utils
{
    public class ConsoleLogger : StringLogger
    {
        bool _exceptionWithStackTrace = true;
        public ConsoleLogger() : base()
        {
            SetLogFunction(LogFunction);
        }

        public ConsoleLogger(bool exceptionWithStackTrace = true) : this()
        {
            _exceptionWithStackTrace = exceptionWithStackTrace;
        }

        protected override string LogFunction(string logPrefix, string logMessage, string errorTitle, Exception exception)
        {
            var logString = base.LogFunction(logPrefix, logMessage, errorTitle, exception);
            Console.Write(logString);

            return logString;
        }
    }
}
