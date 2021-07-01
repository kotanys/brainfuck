using System;
using System.Runtime.Serialization;

namespace SharpBrainfuck
{
    [Serializable]
    public class BrainfuckException : Exception
    {
        public BrainfuckException() : base() { }
        public BrainfuckException(string message) : base(message) { }
        public BrainfuckException(string message, Exception inner) : base(message, inner) { }
        protected BrainfuckException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}