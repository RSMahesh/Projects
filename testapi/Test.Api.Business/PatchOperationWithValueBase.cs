using Test.Api.Business;

using Newtonsoft.Json.Linq;

namespace Test.Api.Business
{
    public abstract class PatchOperationWithValueBase : PatchOperationBase
    {
        public PatchOperationWithValueBase(string path, JToken value)
            : base(path)
        {
            this.Value = value;
        }

        public JToken Value { get; private set; }
    }
}