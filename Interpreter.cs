using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBrainfuck
{
    /// <summary>
    /// A brainfuck interpreter.
    /// </summary>
    public class Interpreter
    {
        #region Fields
        private string _code;
        private char _crashPointChar = '`';
        #endregion

        #region Properties
        /// <summary>
        /// A brainfuck code that will be executed.
        /// Use SetCode() with ignoreChecks parameter set to true to set it without checks.
        /// </summary>
        public string Code { get => _code; set => SetCode(value); }

        /// <summary>
        /// An amount of 8-bit memory cells that will be created by interpreter.
        /// By default, it's 30000.
        /// </summary>
        public int CellsCount { get; set; } = 30000;

        /// <summary> 
        /// Character that will be used as a crash point.
        /// If UseCrashPoint is false, it will be ignored.
        /// Trying to set to one of the brainfuck operators will throw ArgumentException.
        /// By default, it's '`'.
        /// </summary>
        public char CrashPointChar
        {
            get => _crashPointChar;
            set
            {
                char[] bfOps = new[] { '+', '-', '<', '>', '.', ',', '[', ']' };
                if (bfOps.Contains(value))
                    throw new ArgumentException("Character that you are trying to set as a crash point is a brainfuck operator.");

                _crashPointChar = value;
            }
        }
        /// <summary> 
        /// If true, program will crash if interpreter
        /// reaches a CrashPointChar in code.
        /// By default, it's false.
        /// </summary>
        public bool UseCrashPoint { get; set; } = false;

        /// <summary>
        /// Logger that will be used, if program crashes.
        /// By default, it's BaseLogger.
        /// </summary>
        public ILogger Logger { get; set; } = new BaseLogger();
        #endregion
        
        #region Constructors
        /// <summary>
        /// Creates a new instance of Interpreter.
        /// </summary>
        public Interpreter() { }
        /// <summary>
        /// Creates a new instance of Interpreter and sets the specified code.
        /// </summary>
        /// <param name="code">Code that will be set.</param>
        /// <param name="ignoreChecks">If true, no code checks will be made.</param>
        public Interpreter(string code, bool ignoreChecks)
        {
            SetCode(code, ignoreChecks);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets code.
        /// </summary>
        /// <param name="newCode">Code that will be set.</param>
        /// <param name="ignoreChecks">If true, no code checks will be made.</param>
        public void SetCode(string newCode, bool ignoreChecks = false)
        {
            if (ignoreChecks)
            {
                _code = newCode;
                return;
            }

            if (newCode == null)
            {
                throw new ArgumentNullException(nameof(newCode));
            }

            try
            {
                LoopCheck(newCode);
            }
            catch (BrainfuckException bfEx)
            {
                throw new ArgumentException("Code check was failed: " + bfEx.Message);
            }

            _code = newCode;
        }

        /// <summary>
        /// Executes the brainfuck code.
        /// </summary>
        /// <param name="outputToConsole">If true, program output will be printed to console.</param>
        /// <param name="loggerOutput">A LoggerInfo, that contains information about program execution completion.</param>
        /// <returns>A string that contains program output.</returns>
        public string Run(bool outputToConsole, out LoggerInfo loggerOutput)
        {
            if (Code == null)
                throw new BrainfuckException("No code set!");

            byte[] memory = new byte[CellsCount];
            int pointer = 0;
            List<int> loops = new List<int>();

            uint loopCount = 0; // used, when skipping [] loop

            StringBuilder output = new StringBuilder();

            int i = 0;
            try
            {
                for (; i < Code.Length; i++)
                {
                    if (loopCount == 0)
                    {
                        switch (Code[i])
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
                                if (UseCrashPoint && Code[i] == _crashPointChar)
                                    throw new BrainfuckException("Crash point reached!");
                                break;
                        }
                    }
                    else
                    {
                        switch (Code[i])
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

                loggerOutput = new(i, memory, false);
                return output.ToString();
            }
            catch (Exception e)
            {
                loggerOutput = new(i, memory, true);
                Logger.Log(loggerOutput);
                throw new BrainfuckException("Something went wrong!", e);
            }
        }

        /// <summary>
        /// Executes the brainfuck code.
        /// </summary>
        /// <param name="outputToConsole">If true, program output will be printed to console.</param>
        /// <returns>A string that contains program output.</returns>
        public string Run(bool outputToConsole)
        {
            return Run(outputToConsole, out LoggerInfo _);
        }
        #endregion

        #region Private Methods
        private static void LoopCheck(string code)
        {
            uint openLoopSigns = 0;
            uint closeLoopSigns = 0;

            foreach (char instruction in code) switch (instruction)
                {
                    case '[':
                        openLoopSigns++;
                        break;
                    case ']':
                        closeLoopSigns++;
                        break;
                    default:
                        break;
                }

            if (openLoopSigns != closeLoopSigns)
                throw new BrainfuckException(String.Format("Code is incorrect -- there are {0} \'[\' and {1} \']\'", openLoopSigns, closeLoopSigns));
        }
            
        #endregion
    }
}
