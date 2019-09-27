using System;
using TflApiClient.Interface;

namespace TflApiClient.Utils
{
    public class Logger : ILogger
    {
        Func<string, string, string, Exception, string> _logFunction;

        #region [Constructors]
        public Logger()
        {
        }
        public Logger(Func<string, string, string, Exception, string> logFunction)
        {
            _logFunction = logFunction;
        }
        #endregion

        string LogPrefix => $"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToLongTimeString()} : ";

        #region [public Methods]

        public void SetLogFunction(Func<string, string, string, Exception, string> logFunction)
        {
            _logFunction = logFunction;
        }

        public string Log(string logMessage)
        {
            return Log(logMessage, "");
        }

        public string Log(string logMessage, string errorTitle)
        {
            return Log(logMessage, errorTitle, null);
        }

        public string Log(string logMessage, string errorTitle, Exception ex)
        {
            return _logFunction?.Invoke(LogPrefix, logMessage, errorTitle, ex);
        }

        #endregion
    }
}
