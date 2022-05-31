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
        /// <param name="loggerInfo"><see cref="LoggerInfo"/> containing information for logger.</param>
        void Log(LoggerInfo loggerInfo);
    }
}