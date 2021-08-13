using System;
using System.Runtime.Serialization;

#pragma warning disable CS1591
namespace SharpBrainfuck
{
    [Serializable]
    public sealed class BrainfuckException : Exception
    {
        public BrainfuckException() : base() { }
        public BrainfuckException(string message) : base(message) { }
        public BrainfuckException(string message, Exception inner) : base(message, inner) { }
    }
}