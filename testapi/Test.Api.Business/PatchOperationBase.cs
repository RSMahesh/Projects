namespace Test.Api.Business
{
    public abstract class PatchOperationBase
    {
        public PatchOperationBase(string path)
        {
            this.Path = path;
        }

        public string Path { get; private set; } 
    }
}