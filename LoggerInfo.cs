namespace SharpBrainfuck
{
    /// <summary>
    /// A container of interpreter information at the moment the program ends.
    /// Once it's created, it cannot be changed.
    /// </summary>
    public struct LoggerInfo
    {
        private readonly int _instructionIndex;
        private readonly byte[] _memoryDump;
        private readonly bool _crashed;

#pragma warning disable CS1591
        public int InstructionIndex { get => _instructionIndex; }
        public byte[] MemoryDump { get => _memoryDump; }
        public bool Crashed { get => _crashed; }
#pragma warning restore CS1591

        /// <summary>
        /// Creates a new instance of LoggerInfo.
        /// </summary>
        public LoggerInfo(int instructionIndex, byte[] memory, bool crashed)
        {
            _instructionIndex = instructionIndex;
            _memoryDump = memory;
            _crashed = crashed;
        }
    }
}