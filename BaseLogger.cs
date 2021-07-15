using System;
using System.IO;

namespace SharpBrainfuck
{
    public sealed class BaseLogger : ILogger
    {
        public string LogFile { get; set; }

        public void Log(LoggerInfo loggerInfo)
        {
            if (loggerInfo == null) 
                throw new NullReferenceException(nameof(loggerInfo));
            if (LogFile == null)
                throw new NullReferenceException(nameof(LogFile));
            
            using (StreamWriter sw = new StreamWriter(LogFile))
            {
                sw.WriteLine((loggerInfo.crashed) ? "CRASH" : "Execution completed");
                sw.WriteLine(DateTime.Now);
                sw.WriteLine("I was at instruction {0} (including comments)", loggerInfo.instructionIndex);
                sw.WriteLine("\nMemory:\n");

                for (int j = 0; j < loggerInfo.memoryDump.Length; j++)
                {
                    if (loggerInfo.memoryDump[j] != 0)
                        sw.WriteLine("[{0}]\t==\t{1}", j, loggerInfo.memoryDump[j]);
                }
            }
        }
        
        public BaseLogger(string logFile)
        {
            LogFile = logFile;
        }
        public BaseLogger() : this("log.txt") { }
    }
}
