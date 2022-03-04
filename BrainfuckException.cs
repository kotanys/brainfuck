using System;

namespace SharpBrainfuck
{
    /// <summary>
    /// Represents errors that occur during brainfuck program exectuion.
    /// </summary>
    [Serializable]
    public sealed class BrainfuckException : Exception
    {
        /// <summary>
        /// Creates a new instance of <see cref="BrainfuckException"/>
        /// </summary>
        public BrainfuckException() : base() { }
        /// <summary>
        /// Creates a new instance of <see cref="BrainfuckException"/>
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public BrainfuckException(string message) : base(message) { }
        /// <summary>
        /// Creates a new instance of <see cref="BrainfuckException"/>
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference
        ///    (Nothing in Visual Basic) if no inner exception is specified.</param>
        public BrainfuckException(string message, Exception inner) : base(message, inner) { }
    }
}