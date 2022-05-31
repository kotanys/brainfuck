using System;

namespace SharpBrainfuck
{
    /// <summary>
    /// A container of interpreter information at the moment the program ends. Immutable.
    /// </summary>
    [Serializable]
    public struct LoggerInfo
    {
        /// <summary>
        /// Represents position of the instruction in the code at the moment of execution completion.
        /// </summary>
        public int InstructionIndex { get; }
        /// <summary>
        /// An array containing program memory at the moment of execution completion.
        /// </summary>
        public byte[] MemoryDump { get; }
        /// <summary>
        /// Respresents whether the program crashed (<c>true</c>) or not (<c>false</c>).
        /// </summary>
        public bool Crashed { get; }

        /// <summary>
        /// Creates a new instance of <see cref="LoggerInfo"/>.
        /// </summary>
        public LoggerInfo(int instructionIndex, byte[] memory, bool crashed)
        {
            InstructionIndex = instructionIndex;
            MemoryDump = memory;
            Crashed = crashed;
        }
    }
}