using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBrainfuck
{
    class Interpreter : Logger
    {
        private string _code;
        public string Code { get => _code; private set => _code = value; } // use SetCode() method to set code

        public Interpreter(string code, bool ignoreChecks)
        {
            if (!ignoreChecks)
                SetCode(code);
            else 
                Code = code;
        }

        public void SetCode(string newCode)
        {
            if (newCode == null)
            {
                throw new ArgumentNullException(nameof(newCode));
            }

            uint openLoopSign = 0;
            uint closeLoopSign = 0;
            foreach (char instruction in newCode)
            {
                switch (instruction)
                {
                    case '[':
                        openLoopSign++;
                        break;
                    case ']':
                        closeLoopSign++;
                        break;
                    default:
                        break;
                }
            }

            if (openLoopSign != closeLoopSign)
                throw new BrainfuckException(String.Format("Code is incorrect -- there are {0} \'[\' and {1} \']\'", openLoopSign, closeLoopSign));

            Code = newCode;
        }

        public string Run(bool outputToConsole, string logFile)
        {
            byte[] memory = new byte[30000];
            int pointer = 0;
            List<int> loops = new List<int>();

            uint loopCount = 0; // used, when skipping [] loop

            StringBuilder output = new StringBuilder();

            int i = 0;
            try
            {
                for (; i < _code.Length; i++)
                {
                    if (loopCount == 0)
                    {
                        switch (_code[i])
                        {
                            case '+':
                                memory[pointer]++;
                                break;
                            case '-':
                                memory[pointer]--;
                                break;
                            case '<':
                                if (pointer == 0)
                                    pointer = memory.Length - 1;
                                else
                                    pointer--;
                                break;
                            case '>':
                                if (pointer == memory.Length - 1)
                                    pointer = 0;
                                else
                                    pointer++;
                                break;
                            case '.':
                                char outChar = char.ConvertFromUtf32(memory[pointer])[0];
                                if (outputToConsole)
                                    Console.Write(outChar);
                                output.Append(outChar);
                                break;
                            case ',':
                                memory[pointer] = (byte)Console.ReadKey().KeyChar;
                                break;
                            case '[':
                                if (memory[pointer] != 0)
                                    loops.Add(i - 1);
                                else
                                    loopCount++;
                                break;
                            case ']':
                                if (memory[pointer] != 0)
                                    i = loops[^1];
                                loops.RemoveAt(loops.Count - 1);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        switch (_code[i])
                        {
                            case '[':
                                loopCount++;
                                break;
                            case ']':
                                loopCount--;
                                break;
                            default:
                                break;
                        }
                    }
                }
                Console.WriteLine();
                return output.ToString();
            } 
            catch (Exception)
            {
                Log(logFile, memory, i);
                throw new BrainfuckException();
            }
        }
    }
}
