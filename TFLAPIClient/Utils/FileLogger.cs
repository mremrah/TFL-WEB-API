using System;
using System.IO;

namespace TflApiClient.Utils
{
    public class FileLogger : StringLogger
    {
        bool _exceptionWithStackTrace = true;
        string _fileName = $"tflapi-{DateTime.Today.ToString("ddMMyyyy")}.log";
        public FileLogger() : base()
        {
            SetLogFunction(LogFunction);
        }

        public FileLogger(bool exceptionWithStackTrace = true) : this()
        {
            _exceptionWithStackTrace = exceptionWithStackTrace;
        }

        protected override string LogFunction(string logPrefix, string logMessage, string errorTitle, Exception exception)
        {
            try
            {
                var logText = base.LogFunction(logPrefix, logMessage, errorTitle, exception);
                File.AppendAllText(_fileName, logText);
                return logText;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Writing Log file failed: {ex.Message}");
            }
            return "";

        }
    }
}
