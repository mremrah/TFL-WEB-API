using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TflApiClient.Interface
{
    public interface ILogger
    {
        string Log(string logMessage);
        string Log(string logMessage, string errorTitle);
        string Log(string logMessage, string errorTitle, Exception ex);

    }
}
