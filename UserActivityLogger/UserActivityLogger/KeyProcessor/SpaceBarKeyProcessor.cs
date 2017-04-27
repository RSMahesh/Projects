
namespace UserActivityLogger
{
    public class SpaceBarKeyProcessor : SpecificKeysProcessor
    {
        public override bool CanProcess(string loggedKey)
        {
            return "Space" == loggedKey;
        }

        public override string ProcessKey(string loggedKey)
        {
            return ' '.ToString();
        }
    }
}
