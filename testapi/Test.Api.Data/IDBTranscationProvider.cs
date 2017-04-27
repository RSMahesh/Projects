using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Api.Data
{
    interface IDBTranscationProvider
    {
        void BeginTranscation();

        void EndTranscation();
    }
}
