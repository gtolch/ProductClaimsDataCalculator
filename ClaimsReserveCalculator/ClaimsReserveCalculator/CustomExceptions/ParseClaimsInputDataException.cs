using System;

namespace ClaimsReserveCalculator.CustomExceptions
{
    /// <summary>
    /// An exception that indicates an error parsing products claims input data.
    /// </summary>
    public class ParseClaimsInputDataException : Exception
    {
        /// <summary>
        /// Constructor that has an error message.
        /// </summary>
        /// <param name="message">Description of the error that has occurred.</param>
        public ParseClaimsInputDataException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructor that has an error message and keeps hold of the inner exception.
        /// </summary>
        /// <param name="message">Description of the error that has occurred.</param>
        /// <param name="innerException">The inner exception that has occurred.</param>
        public ParseClaimsInputDataException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
