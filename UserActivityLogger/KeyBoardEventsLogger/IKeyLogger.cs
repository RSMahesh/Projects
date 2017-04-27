namespace UserActivityLogger
{
    public interface IKeyLogger
    {
        void CleanBuffer();
        string GetKeys();
        void StartListening();
    }
}