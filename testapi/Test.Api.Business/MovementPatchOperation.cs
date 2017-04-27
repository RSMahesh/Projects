namespace Test.Api.Business
{
    public abstract class MovementPatchOperation : PatchOperationBase
    {
        public MovementPatchOperation(string path, string from) 
            : base(path)
        {
            this.From = from;
        }

        public string From { get; private set; }
    }
}