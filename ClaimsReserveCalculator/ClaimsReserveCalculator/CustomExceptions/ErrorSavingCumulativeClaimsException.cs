using System;

namespace ClaimsReserveCalculator.CustomExceptions
{
    /// <summary>
    /// An exception that is raised when an error occurs 
    /// in saving cumulative claims data.
    /// </summary>
    public class ErrorSavingCumulativeClaimsException : Exception
    {
        /// <summary>
        /// Constructor that has an error message and keeps hold of the inner exception.
        /// </summary>
        /// <param name="message">Description of the error that has occurred.</param>
        /// <param name="innerException">The inner exception that has occurred.</param>
        public ErrorSavingCumulativeClaimsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
