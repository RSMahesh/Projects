namespace UserActivityLogger
{
    public interface ICurrentActivityProvider 
    {
        Activity GetActivity();
        Activity GetActivity(string keyPressedData);
    }
}