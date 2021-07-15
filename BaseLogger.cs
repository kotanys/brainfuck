using System;
using System.IO;

namespace SharpBrainfuck
{
    public sealed class BaseLogger : ILogger
    {
        public string LogFile { get; set; }

        public void Log(LoggerInfo loggerInfo)
        {
            if (LogFile == null)
                throw new NullReferenceException(nameof(LogFile));
            
            using (StreamWriter sw = new StreamWriter(LogFile))
            {
                sw.WriteLine((loggerInfo.Crashed) ? "CRASH" : "Execution completed");
                sw.WriteLine(DateTime.Now);
                sw.WriteLine("I was at instruction {0} (including comments)", loggerInfo.InstructionIndex);
                sw.WriteLine("\nMemory:\n");

                for (int j = 0; j < loggerInfo.MemoryDump.Length; j++)
                {
                    if (loggerInfo.MemoryDump[j] != 0)
                        sw.WriteLine("[{0}]\t==\t{1}", j, loggerInfo.MemoryDump[j]);
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
