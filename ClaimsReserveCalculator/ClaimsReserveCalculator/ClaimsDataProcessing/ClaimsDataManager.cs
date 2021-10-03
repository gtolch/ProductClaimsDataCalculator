using ClaimsReserveCalculator.ClaimsDataDomainEntities;
using ClaimsReserveCalculator.Properties;
using System;
using System.Collections.Generic;

namespace ClaimsReserveCalculator.ClaimsDataProcessing
{
    /// <summary>
    /// Represents a manager of claims data processing operations.
    /// </summary>
    public class ClaimsDataManager : IClaimsDataManager
    {
        private readonly IProductsClaimsData _productsClaimsData;

        /// <summary>
        /// Parameterized constructor to initialise the claims data processor.
        /// </summary>
        public ClaimsDataManager(IProductsClaimsData productsClaimsData)
        {
            _productsClaimsData = productsClaimsData;
        }

        /// <summary>
        /// Stores the earliest origin year or ORIGIN_YEAR_NOT_SET if it hasn't been setup.
        /// </summary>
        public int EarliestOriginYear => _productsClaimsData.EarliestOriginYear;

        /// <summary>
        /// Names of all products that may have associated claims data.
        /// </summary>
        public IEnumerable<string> ProductNames => _productsClaimsData.ProductNames;

        /// <summary>
        /// Setup and store a product claims data entry using the supplied parameters.
        /// </summary>
        /// <param name="productName">The name of the product.</param>
        /// <param name="originYear">The origin year for the claims data.</param>
        /// <param name="developmentYear">The development year for the claims data</param>
        /// <param name="incrementalValue">The incremental value for the claims data</param>
        /// <returns>The initialised claims data for the current product.</returns>
        public ProductClaimsData SetupProductClaimsData(
            string productName, int originYear, int developmentYear, double incrementalValue)
        {
            // Retrieve any existing product claims data for the current origin year
            ProductClaimsData productClaimsData = GetProductClaimsForOriginYear(productName, originYear);

            // Create a new product claims data entry if an existing one for the origin year cannot be found.
            if (productClaimsData == null)
            {
                productClaimsData = new ProductClaimsData(originYear);
            }

            double cumulativeValue = incrementalValue + productClaimsData.CalculateCumulativeClaimsValue();
            DevelopmentYearClaimsData devYearClaimsData = new DevelopmentYearClaimsData(
                developmentYear, incrementalValue, cumulativeValue);

            productClaimsData.UpdateDevelopmentYearClaimsData(devYearClaimsData);

            // Update the product claims data - store it for later use
            _productsClaimsData.UpdateProductClaimsData(productName, productClaimsData);

            return productClaimsData;
        }

        /// <summary>
        /// Clears any old claims data that might exist.
        /// </summary>
        public void EraseClaimsData() => _productsClaimsData.EraseData();

        /// <summary>
        /// Get the first claims data entry associated with the 
        /// specified product name and origin year parameters.
        /// </summary>
        /// <param name="productName">The name of the product to retrieve claims data for.</param>
        /// <param name="originYear">The origin year associated with claims data.</param>
        /// <returns>The product claims data for the specified origin year or null if not found.</returns>
        public ProductClaimsData GetProductClaimsForOriginYear(string productName, int originYear)
        {
            if (string.IsNullOrWhiteSpace(productName) || !ClaimsDataValidator.IsClaimsYearValid(originYear))
            {
                throw new ArgumentException(Resources.CannotGetProductClaimsForOriginYear);
            }

            ProductClaimsDataCollection allClaimsDataForProduct = _productsClaimsData.GetAllClaimsDataForProduct(productName);
            ProductClaimsData claimsData = null;

            if (allClaimsDataForProduct != null)
            {
                claimsData = allClaimsDataForProduct.GetProductClaimsForOriginYear(originYear);
            }

            return claimsData;
        }

        /// <summary>
        /// Gets the claims data for a product for all of the recorded development years.
        /// </summary>
        /// <param name="productName">The name of the product that we want claims data for.</param>
        /// <returns>Returns all development years claims data associated with the product.</returns>
        public IEnumerable<DevelopmentYearClaimsData> GetAllDevelopmentYearsClaimsData(string productName)
        {
            List<DevelopmentYearClaimsData> devYearsClaimsData = new List<DevelopmentYearClaimsData>();

            ProductClaimsDataCollection allClaimsDataForProduct = _productsClaimsData.GetAllClaimsDataForProduct(productName);

            if (allClaimsDataForProduct != null)
            {
                foreach (var claimsDataEntry in allClaimsDataForProduct.ProductClaimsDataItems)
                {
                    devYearsClaimsData.AddRange(claimsDataEntry.DevelopmentYearsClaimsData);
                }
            }

            return devYearsClaimsData;
        }

        /// <summary>
        /// Adds entries for any missing years to the claims data. Default/placeholder product claims 
        /// data entries will be added for any years inferred to be missing from the recorded data.
        /// </summary>
        /// <param name="maxDevelopmentYear">The maximum development year to insert entries for</param>
        public void AddMissingYearsData(int maxDevelopmentYear) => _productsClaimsData.AddMissingYearsData(maxDevelopmentYear);
    }
}
