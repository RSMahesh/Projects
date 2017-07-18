using System;
using System.Collections.Generic;

namespace UserActivityLogger
{
    public interface IActivityReader: IEnumerable<Activity>, IDisposable
    {
        void ChangePostion(int positionNumber);
        void Dispose();
        int FileCount();
    }
}