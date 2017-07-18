using System;
using System.Collections.Generic;

namespace UserActivityLogger
{
    public interface IActivitesEnumerator:  IDisposable
    {
        int FileCount { get; }
        void ChangePostion(int positionNumber);
        
    }
}