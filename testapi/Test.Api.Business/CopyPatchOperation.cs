namespace Test.Api.Business
{
    public class CopyPatchOperation : MovementPatchOperation
    {
        public CopyPatchOperation(string path, string @from) 
            : base(path, @from)
        {
        }
    }
}