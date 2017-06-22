using System;

namespace UserActivityLogger
{
    public interface IStartUp
    {
        void Start(TimeSpan screenCaptureTimeInterval);
    }
}