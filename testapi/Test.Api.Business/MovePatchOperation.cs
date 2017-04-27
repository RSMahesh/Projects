namespace Test.Api.Business
{
    /// <summary>
    /// The move patch operation.
    /// </summary>
    public class MovePatchOperation : MovementPatchOperation
    {
        public MovePatchOperation(string path, string @from) : base(path, @from)
        {
        }
    }
}