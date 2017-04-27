using System;

namespace DeploymentWebUI
{
    /// <summary>
    /// Interface for logger 
    /// </summary>
    public interface ILogger
    {
        void LogException(Exception exception);
        void LogException(string message);
        void LogCriticalException(Exception exception);
        void LogCriticalException(string message);
        void LogWarning(string message);
        void LogInformation(string message);
        void LogVerbose(string message);
     }
}
