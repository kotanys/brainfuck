namespace SharpBrainfuck
{
    /// <summary>
    /// Interface for brainfuck loggers.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// This method will be called if brainfuck program crashes.
        /// </summary>
        void Log(LoggerInfo loggerInfo);
    }
}