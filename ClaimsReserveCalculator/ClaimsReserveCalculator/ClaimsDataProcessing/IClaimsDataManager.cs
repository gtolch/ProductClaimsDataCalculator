using ClaimsReserveCalculator.ClaimsDataDomainEntities;
using System.Collections.Generic;

namespace ClaimsReserveCalculator.ClaimsDataProcessing
{
    /// <summary>
    /// Interface to the claims data processor/manager.
    /// </summary>
    public interface IClaimsDataManager
    {
        /// <summary>
        /// Names of all products that may have associated claims data.
        /// </summary>
        IEnumerable<string> ProductNames { get; }

        /// <summary>
        /// Stores the earliest origin year or ORIGIN_YEAR_NOT_SET if it hasn't been setup.
        /// </summary>
        int EarliestOriginYear { get; }

        /// <summary>
        /// Erase any old claims data that might exist.
        /// </summary>
        void EraseClaimsData();

        /// <summary>
        /// Setup and store a product claims data entry using the supplied parameters.
        /// </summary>
        /// <param name="productName">The name of the product.</param>
        /// <param name="originYear">The origin year for the claims data.</param>
        /// <param name="developmentYear">The development year for the claims data</param>
        /// <param name="incrementalValue">The incremental value for the claims data</param>
        /// <returns>The initialised claims data for the current product.</returns>
        ProductClaimsData SetupProductClaimsData(
            string productName, int originYear, int developmentYear, double incrementalValue);

        /// <summary>
        /// Gets the claims data for a product for all of the recorded development years.
        /// </summary>
        /// <param name="productName">The name of the product that we want claims data for.</param>
        /// <returns>Returns all development years claims data associated with the product.</returns>
        IEnumerable<DevelopmentYearClaimsData> GetAllDevelopmentYearsClaimsData(string productName);

        /// <summary>
        /// Adds entries for any missing years to the claims data. Default/placeholder product claims 
        /// data entries will be added for any years inferred to be missing from the recorded data.
        /// </summary>
        /// <param name="maxDevelopmentYear">The maximum development year to insert entries for</param>
        void AddMissingYearsData(int maxDevelopmentYear);
    }
}
