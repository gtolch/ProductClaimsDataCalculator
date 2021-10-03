using System.Collections.Generic;

namespace ClaimsReserveCalculator.ClaimsDataDomainEntities
{
    /// <summary>
    /// Contains common validation methods for claims data.
    /// </summary>
    public class ClaimsDataValidator
    {
        /// <summary>
        /// Determines whether a particular year 
        /// relating to claims data is considered to be valid or not.
        /// </summary>
        /// <param name="year">The year value to check (associated with claims data).</param>
        /// <returns>Returns true if the year is valid, false otherwise.</returns>
        internal static bool IsClaimsYearValid(int year)
        {
            return year >= ClaimsDataConstants.MIN_VALID_YEAR &&
                year != ClaimsDataConstants.YEAR_NOT_SET_VALUE;
        }

        /// <summary>
        /// Determines whether a couple of years
        /// relating to claims data are both considered to be valid or not.
        /// </summary>
        /// <param name="year1">The 1st year value to check (associated with claims data).</param>
        /// <param name="year2">The 2nd year value to check (associated with claims data).</param>
        /// <returns>Returns true if both of the years are valid, false otherwise.</returns>
        internal static bool AreClaimsYearsValid(int year1, int year2)
            => IsClaimsYearValid(year1) && IsClaimsYearValid(year2);
    }
}
