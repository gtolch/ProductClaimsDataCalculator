using System.Collections.Generic;

namespace ClaimsReserveCalculator.ClaimsDataDomainEntities
{
    /// <summary>
    /// Represents the claims data associated with a collection of products.
    /// </summary>
    public interface IProductsClaimsData
    {
        /// <summary>
        /// All of the product names associated with the claims data.
        /// </summary>
        IEnumerable<string> ProductNames { get; }

        /// <summary>
        /// Stores the earliest origin year or ORIGIN_YEAR_NOT_SET if it hasn't been setup.
        /// </summary>
        int EarliestOriginYear { get; }

        /// <summary>
        /// Clears any old claims data that might exist.
        /// </summary>
        void EraseData();

        /// <summary>
        /// Get all the claims data associated with the specified product name parameter.
        /// </summary>
        /// <param name="productName">The name of the product to retrieve claims data for.</param>
        /// <returns>The collection of product claims data for the specified product or null if not found.</returns>
        ProductClaimsDataCollection GetAllClaimsDataForProduct(string productName);

        /// <summary>
        /// Updates the recorded claims data for a particular product.
        /// </summary>
        /// <param name="productName">The name of the associated product.</param>
        /// <param name="claimsData">claims data associated with the product.</param>
        void UpdateProductClaimsData(string productName, ProductClaimsData claimsData);

        /// <summary>
        /// Adds entries for any missing years to the claims data. Default/placeholder product claims 
        /// data entries will be added for any years inferred to be missing from the recorded data.
        /// </summary>
        /// <param name="maxDevelopmentYear">The maximum development year to insert entries for</param>
        void AddMissingYearsData(int maxDevelopmentYear);
    }
}
