using System;

namespace ClaimsReserveCalculator.CustomExceptions
{
    /// <summary>
    /// An exception that can be raised to indicate failed to read claims data file.
    /// </summary>
    public class ClaimsDataFileReadException : Exception
    {
        /// <summary>
        /// Constructor that has an error message and keeps hold of the inner exception.
        /// </summary>
        /// <param name="message">Description of the error that has occurred.</param>
        /// <param name="innerException">The inner exception that has occurred.</param>
        public ClaimsDataFileReadException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}
