namespace SharpBrainfuck
{
    public interface ILogger
    {
        string LogFile { get; set; }
        void Log (LoggerInfo loggerInfo);
    }
}