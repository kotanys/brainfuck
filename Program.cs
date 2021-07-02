using System;
using System.IO;
using System.Text;

namespace SharpBrainfuck
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: brainfuck.exe {path to .bf} [-i] [-l [path]]");
                Console.WriteLine("Arguments:");
                Console.WriteLine("\t-i - ignore code check");
                Console.WriteLine("\t-l - path to the file where the log will be written if something goes wrong. If path is not specified, log.txt will be used.");
                return;
            }
            
            string path = WorkOnArgs(args, out string logFile, out bool ignoreChecks);
            string code;

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    code = sr.ReadToEnd();
                }
            }
            catch
            {
                Console.WriteLine("File {0} doesn't exist!", path);
                return;
            }

            Interpreter interpreter = new Interpreter(code, ignoreChecks);
            interpreter.Run(outputToConsole: true, logFile);
        }

        // argument parsing should be reworked
        // this is shit
        static string WorkOnArgs(string[] args, out string logFile, out bool ignoreChecks)
        {
            bool makingLogPath = false;
            ignoreChecks = false;
            
            StringBuilder sb = new StringBuilder();
            StringBuilder logSb = new StringBuilder();
            foreach (string word in args)
            {
                if (makingLogPath) 
                {
                    logSb.Append(word);
                    logSb.Append(' ');
                }
                else if (word == "-l")
                {
                    makingLogPath = true;
                }
                else if (word == "-i")
                {
                    ignoreChecks = true;
                }
                else
                {
                    sb.Append(word);
                    sb.Append(' ');
                }
            }

            logFile = (logSb.Length > 0) ? logSb.ToString() : "log.txt";
            return sb.ToString();
        }
    }
}
