namespace UserActivityLogger
{
    public interface IActivityProvider 
    {
        Activity GetActivity();
        Activity GetActivity(string keyPressedData);
    }
}