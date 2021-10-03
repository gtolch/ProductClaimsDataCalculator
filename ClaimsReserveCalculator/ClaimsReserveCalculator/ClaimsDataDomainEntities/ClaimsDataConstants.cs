using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ClaimsReserveTestProject")]
namespace ClaimsReserveCalculator.ClaimsDataDomainEntities
{
    /// <summary>
    /// Contains constants relating to the claims data.
    /// Could use resource file or store in app.config instead.
    /// </summary>
    static class ClaimsDataConstants
    {
        /// <summary>
        /// Represents the default value when the origin year has not been set.
        /// </summary>
        internal const int YEAR_NOT_SET_VALUE = int.MaxValue;

        /// <summary>
        /// Represents the minimum valid year that may be recorded in claims data.
        /// </summary>
        internal const int MIN_VALID_YEAR = 1500;

        /// <summary>
        /// Represents the default value when no development years have been set.
        /// </summary>
        internal const int NO_DEVELOPMENT_YEARS_VALUE = -1;
    }
}
