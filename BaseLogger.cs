using System;
using System.IO;

namespace SharpBrainfuck
{
    /// <summary>
    /// A default brainfuck logger.
    /// </summary>
    public sealed class BaseLogger : ILogger
    {
        /// <summary>
        /// The file log will be written to.
        /// </summary>
        public string LogFile { get; set; }

        /// <summary>
        /// Creates a log in LogFile.
        /// Note that LogFile will be cleared.
        /// </summary>
        /// <param name="loggerInfo">The LoggerInfo that contains information about the program ending.</param>
        public void Log(LoggerInfo loggerInfo)
        {
            if (LogFile == null)
                throw new NullReferenceException(nameof(LogFile));

            using var sw = new StreamWriter(LogFile);
            sw.Write(DateTime.Now);
            sw.WriteLine(" " + ((loggerInfo.Crashed) ? "CRASH" : "Execution completed"));
            sw.WriteLine("I was at instruction {0} (including comments)", loggerInfo.InstructionIndex);
            sw.WriteLine("\nMemory:\n");

            for (int j = 0; j < loggerInfo.MemoryDump.Length; j++)
            {
                if (loggerInfo.MemoryDump[j] != 0)
                    sw.WriteLine("[{0}]\t==\t{1}", j, loggerInfo.MemoryDump[j]);
            }
        }

        /// <summary>
        /// Creates a new instance of <see cref="BaseLogger"/>.
        /// </summary>
        /// <param name="logFile">The file log will be written to.</param>
        public BaseLogger(string logFile)
        {
            LogFile = logFile;
        }
        /// <summary>
        /// Creates a new instance of <see cref="BaseLogger"/>, and sets LogFile to "log.txt".
        /// </summary>
        public BaseLogger() : this("log.txt") { }
    }
}
