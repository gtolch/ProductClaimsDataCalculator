using System;

namespace ClaimsReserveCalculator.CustomExceptions
{
    /// <summary>
    /// An exception that can be raised to indicate that claims data
    /// categories could not be successfully identified.
    /// </summary>
    public class InvalidClaimsDataCategoriesException : Exception
    {
        /// <summary>
        /// Constructor that has an error message and keeps hold of the inner exception.
        /// </summary>
        /// <param name="message">Description of the error that has occurred.</param>
        public InvalidClaimsDataCategoriesException(string message)
            : base(message)
        {
        }
    }
}
