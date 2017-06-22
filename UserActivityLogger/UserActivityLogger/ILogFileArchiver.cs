using System;

namespace UserActivityLogger
{
    public interface ILogFileArchiver
    {
        void Start(string logFolder, TimeSpan pollingTimeInterval);
    }
}