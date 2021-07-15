namespace SharpBrainfuck
{
    public struct LoggerInfo
    {
        private int _instructionIndex;
        private byte[] _memoryDump;
        private bool _crashed;

        public int InstructionIndex { get => _instructionIndex; }
        public byte[] MemoryDump { get => _memoryDump; }
        public bool Crashed { get => _crashed; }

        public LoggerInfo(int instructionIndex, byte[] memory, bool crashed)
        {
            _instructionIndex = instructionIndex;
            _memoryDump = memory;
            _crashed = crashed;
        }
    }
}