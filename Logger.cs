using System;
using System.IO;

namespace SharpBrainfuck
{
    internal class Logger
    {
        internal void Log(string logFile, byte[] memory, int instrutionIndex)
        {
            if (logFile != null)
            {
                using (StreamWriter sw = new StreamWriter(logFile))
                {
                    sw.WriteLine(DateTime.Now);
                    sw.WriteLine("I was at instruction {0} (including comments)", instrutionIndex);
                    sw.WriteLine("\nMemory:\n");

                    for (int j = 0; j < memory.Length; j++)
                    {
                        if (memory[j] != 0)
                            sw.WriteLine("[{0}]\t==\t{1}", j, memory[j]);
                    }
                }
            }
        }
    }
}
