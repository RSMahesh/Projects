namespace UserActivityLogger
{
    public interface IActivityRepositary
    {
        void Add(Activity activity);
        ActivityReader GetReader();
    }
}