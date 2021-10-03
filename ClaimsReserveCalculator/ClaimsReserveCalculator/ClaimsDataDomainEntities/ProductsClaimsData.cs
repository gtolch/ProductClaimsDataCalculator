using ClaimsReserveCalculator.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClaimsReserveCalculator.ClaimsDataDomainEntities
{
    /// <summary>
    /// Represents the claims data associated with a collection of products.
    /// </summary>
    public class ProductsClaimsData : IProductsClaimsData
    {
        /// <summary>
        /// Allows claims data to be looked up for a particular product name (specified by the key).
        /// </summary>
        private readonly IDictionary<string, ProductClaimsDataCollection> _productClaimsDataLookup;

        /// <summary>
        /// All of the product names associated with the claims data.
        /// </summary>
        public IEnumerable<string> ProductNames => _productClaimsDataLookup.Keys;

        /// <summary>
        /// Stores the earliest origin year or ORIGIN_YEAR_NOT_SET if it hasn't been setup.
        /// </summary>
        public int EarliestOriginYear { get; private set; }

        /// <summary>
        /// Constructor to initialise the product claims data. 
        /// </summary>
        public ProductsClaimsData()
        {
            _productClaimsDataLookup = new Dictionary<string, ProductClaimsDataCollection>();
            EarliestOriginYear = ClaimsDataConstants.YEAR_NOT_SET_VALUE;
        }

        /// <summary>
        /// Clears any old claims data that might exist.
        /// </summary>
        public void EraseData()
        {
            _productClaimsDataLookup.Clear();
            EarliestOriginYear = ClaimsDataConstants.YEAR_NOT_SET_VALUE;
        }

        /// <summary>
        /// Get all the claims data associated with the specified product name parameter.
        /// </summary>
        /// <param name="productName">The name of the product to retrieve claims data for.</param>
        /// <returns>The collection of product claims data for the specified product or null if not found.</returns>
        public ProductClaimsDataCollection GetAllClaimsDataForProduct(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
            {
                throw new ArgumentException(Resources.CannotGetProductClaimsForOriginYear);
            }

            ProductClaimsDataCollection claimsData = null;

            if (_productClaimsDataLookup.ContainsKey(productName))
            {
                claimsData = _productClaimsDataLookup[productName];
            }

            return claimsData;
        }

        /// <summary>
        /// Updates the recorded claims data for a particular product.
        /// </summary>
        /// <param name="productName">The name of the associated product.</param>
        /// <param name="claimsData">claims data associated with the product.</param>
        public void UpdateProductClaimsData(string productName, ProductClaimsData claimsData)
        {
            if (_productClaimsDataLookup.ContainsKey(productName))
            {
                _productClaimsDataLookup[productName].Add(claimsData);
            }
            else
            {
                _productClaimsDataLookup.Add(productName, new ProductClaimsDataCollection(claimsData));
            }

            if (EarliestOriginYear > claimsData.OriginYear)
            {
                EarliestOriginYear = claimsData.OriginYear;
            }
        }

        /// <summary>
        /// Adds entries for any missing years to the claims data. Default/placeholder product claims 
        /// data entries will be added for any years inferred to be missing from the recorded data.
        /// </summary>
        /// <param name="maxDevelopmentYear">The maximum development year to insert entries for</param>
        public void AddMissingYearsData(int maxDevelopmentYear)
        {
            if (_productClaimsDataLookup.Any() &&
                ClaimsDataValidator.AreClaimsYearsValid(EarliestOriginYear, maxDevelopmentYear))
            {
                _productClaimsDataLookup.Values.AsParallel().ForAll(
                    p => p.AddMissingYearsData(EarliestOriginYear, maxDevelopmentYear));
            }
        }
    }
}
