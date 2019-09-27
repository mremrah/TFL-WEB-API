using System;
using System.Text;

namespace TflApiClient.Utils
{
    public class StringLogger : Logger
    {
        bool _exceptionWithStackTrace = true;
        StringBuilder _stringLogs;
        public StringLogger() : base()
        {
            _stringLogs = new StringBuilder();
            SetLogFunction(LogFunction);
        }

        public StringLogger(bool exceptionWithStackTrace = true) : this()
        {
            _exceptionWithStackTrace = exceptionWithStackTrace;
        }
        public string GetLogStrings()
        {
            return _stringLogs.ToString();
        }
        protected virtual string LogFunction(string logPrefix, string logMessage, string errorTitle, Exception exception)
        {
            _stringLogs.Clear();
            _stringLogs.Append(Environment.NewLine);
            _stringLogs.Append(string.IsNullOrEmpty(logPrefix) ? "" : logPrefix);
            _stringLogs.Append(string.IsNullOrEmpty(errorTitle) ? "" : $"{errorTitle} :");
            _stringLogs.Append(string.IsNullOrEmpty(logMessage) ? "" : logMessage);
            if (exception != null)
            {
                string indent = "";
                do
                {
                    _stringLogs.Append(indent + exception.Message);
                    if (_exceptionWithStackTrace)
                    {
                        _stringLogs.Append($"{Environment.NewLine} {indent} {exception.StackTrace} {Environment.NewLine}");
                    }
                    exception = exception.InnerException;
                    indent += "\t";
                }
                while (exception != null);
            }
            _stringLogs.Append(Environment.NewLine);
            return _stringLogs.ToString();
        }
    }
}
