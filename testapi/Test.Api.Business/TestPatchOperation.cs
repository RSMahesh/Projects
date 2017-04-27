using Newtonsoft.Json.Linq;

namespace Test.Api.Business
{
    public class TestPatchOperation : PatchOperationWithValueBase
    {
        public TestPatchOperation(string path, JToken value)
            : base(path, value)
        {
        }
    }
}