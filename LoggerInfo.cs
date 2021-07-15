namespace SharpBrainfuck
{
    public class LoggerInfo
    {
        public int instructionIndex;
        public byte[] memoryDump;
        public bool crashed;
        public LoggerInfo(int instructionIndex, byte[] memory, bool crashed)
        {
            this.instructionIndex = instructionIndex;
            this.memoryDump = memory;
            this.crashed = crashed;
        }
    }
}