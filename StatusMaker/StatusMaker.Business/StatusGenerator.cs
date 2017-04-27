using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Caching;
using System.Threading.Tasks;

using StatusMaker.Business.Sections;
using StatusMaker.Data;

namespace StatusMaker.Business
{
    public class StatusGenerator : IStatusGenerator
    {
        private readonly IEnumerable<ISection> _items;
        private readonly IOutPutGenerator _outPutGenerator;

        public StatusGenerator(IEnumerable<ISection> items, IOutPutGenerator outPutGenerator)
        {
            _items = items;
            _outPutGenerator = outPutGenerator;
        }

        public string GenerateStatusForSingleDay(DateTime statusDate, string forMember, bool validateAganistJira)
        {
            ObjectCache cache = MemoryCache.Default;

            cache.Remove("JiraIsuueInformation");

            var token = new ConcurrentDictionary<string, string>();

            Parallel.ForEach(
                _items,
                item =>
                {
                    string htmlString = string.Empty;

                    try
                    {
                        htmlString = item.GetItmesAsHtml(statusDate, forMember, validateAganistJira);
                    }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                    catch(NoDataFoundException ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
                    {

                    }
                    token[item.TemplatePlaceHolder] = htmlString;
                }
            );

            return _outPutGenerator.GenerateMail(token);
        }
    }
}
