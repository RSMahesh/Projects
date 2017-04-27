using Newtonsoft.Json.Linq;

using Test.Api.Business;

namespace Test.Api.Business
{
    /// <summary>
    /// The add patch operation.
    /// </summary>
    public class AddPatchOperation : PatchOperationWithValueBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="value"></param>
        public AddPatchOperation(string path, JToken value)
            : base(path, value)
        {
        }
    }
}