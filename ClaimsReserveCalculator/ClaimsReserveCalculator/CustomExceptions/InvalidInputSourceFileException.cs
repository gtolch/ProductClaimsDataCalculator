using System;

namespace ClaimsReserveCalculator.CustomExceptions
{
    /// <summary>
    /// An exception that is raised to indicate 
    /// that an input source file is invalid
    /// </summary>
    public class InvalidInputSourceFileException : Exception
    {
        /// <summary>
        /// Constructor that has an error message parameter.
        /// </summary>
        /// <param name="message">Description of the error that has occurred.</param>
        public InvalidInputSourceFileException(string message)
            : base(message)
        {
        }
    }
}
