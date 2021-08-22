using System;
using System.IO;
using System.Text;

namespace SharpBrainfuck
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: brainfuck.exe {path to .bf} [-i] [-l [path]]");
                Console.WriteLine("Arguments:");
                Console.WriteLine("\t-i - ignore code check");
                Console.WriteLine("\t-l - path to the file where the log will be written if something goes wrong. If path is not specified, log.txt will be used.");
                Console.WriteLine("\t-nc - do not use crash points (`)");
                return;
            }
            
            Interpreter interpreter = new();
            string path = WorkOnArgs(args, ref interpreter, out bool ignoreChecks, out bool useCrashPoint);
            string code;

            try
            {
                using (StreamReader sr = new(path))
                {
                    code = sr.ReadToEnd();
                }
            }
            catch
            {
                Console.WriteLine("File {0} doesn't exist!", path);
                return;
            }

            interpreter.UseCrashPoint = useCrashPoint;
            interpreter.SetCode(code, ignoreChecks);
            interpreter.Run(true);

            Console.WriteLine();
        }

        // ! argument parsing should be reworked
        // ! this is shit
        static string WorkOnArgs(string[] args, ref Interpreter interpreter, out bool ignoreChecks, out bool useCrashPoint)
        {
            bool makingLogPath = false;
            bool workingOnArguments = false;
            ignoreChecks = false;
            useCrashPoint = true;
            
            StringBuilder sb = new();
            StringBuilder logSb = new();
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
                    workingOnArguments = true;
                }
                else if (word == "-i")
                {
                    ignoreChecks = true;
                    workingOnArguments = true;
                }
                else if (word == "-nc")
                {
                    useCrashPoint = true;
                    workingOnArguments = true;
                }
                else if (!workingOnArguments)
                {
                    sb.Append(word);
                    sb.Append(' ');
                }
            }

            if (makingLogPath) ((BaseLogger)interpreter.Logger).LogFile = (logSb.Length > 0) ? logSb.ToString() : ((BaseLogger)interpreter.Logger).LogFile;
            return sb.ToString();
        }
    }
}
