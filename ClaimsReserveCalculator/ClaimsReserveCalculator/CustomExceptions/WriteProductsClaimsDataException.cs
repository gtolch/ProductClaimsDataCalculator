using System;

namespace ClaimsReserveCalculator.CustomExceptions
{
    /// <summary>
    /// An exception indicating an error occurred whilst writing product claims data.
    /// </summary>
    public class WriteProductsClaimsDataException : Exception
    {
        /// <summary>
        /// Constructor that has an error message and keeps hold of the inner exception.
        /// </summary>
        /// <param name="message">Description of the error that has occurred.</param>
        /// <param name="innerException">The inner exception that has occurred.</param>
        public WriteProductsClaimsDataException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
