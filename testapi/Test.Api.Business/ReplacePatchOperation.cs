
using Newtonsoft.Json.Linq;

using Test.Api.Business;

namespace Test.Api.Business
{
    public class ReplacePatchOperation : PatchOperationWithValueBase
    {
        public ReplacePatchOperation(string path, JToken value)
            : base(path, value)
        {

        }
    }
}