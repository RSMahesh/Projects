using System;
using System.Collections.Generic;
using System.Data;

namespace StatusMaker.Business.Sections
{
    public interface ISectionHelper
    {
        string CreateSection(string heading, IEnumerable<DataRow> rows, bool validateAganistJira);
    }
}