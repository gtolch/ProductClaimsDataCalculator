
namespace ClaimsReserveCalculator.ClaimsDataDomainEntities
{
    /// <summary>
    /// A development year claims data entry.
    /// </summary>
    public class DevelopmentYearClaimsData
    {
        /// <summary>
        /// The development year of the claims data
        /// </summary>
        public int DevelopmentYear { get; }

        /// <summary>
        /// The incremental value of the claims data
        /// </summary>
        public double IncrementalValue { get; }

        /// <summary>
        /// The cumulative value of the claims data
        /// </summary>
        public double CumulativeValue { get; }

        /// <summary>
        /// Constructor that sets the development year, incremental year and cumulative year.
        /// </summary>
        /// <param name="developmentYear">The development year of the claims data.</param>
        /// <param name="incrementalValue">The incremental value of the claims data.</param>
        /// <param name="cumulativeValue">The cumulative value of the claims data.</param>
        public DevelopmentYearClaimsData(int developmentYear, double incrementalValue, double cumulativeValue)
        {
            DevelopmentYear = developmentYear;
            IncrementalValue = incrementalValue;
            CumulativeValue = cumulativeValue;
        }
    }
}
