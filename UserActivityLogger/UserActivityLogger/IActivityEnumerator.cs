using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserActivityLogger;

namespace UserActivityLogger
{
    public interface IActivitesEnumerator<T> : IEnumerator<T>, IDisposable
    {
    }
}
