namespace UserActivityLogger
{
    public interface IKeyProcessor
    {
        ProcessedKeyData ProcessKeys(string keyBuffer);
    }

    public class ProcessedKeyData
    {
        public ProcessedKeyData(string processedData, string unProcessedData)
        {
            ProcessedData = processedData;
            UnProcessedData = unProcessedData;
        }
        public string ProcessedData { get; private set; }

        public string UnProcessedData { get; private set; }
    }
}