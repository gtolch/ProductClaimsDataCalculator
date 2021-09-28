using System;
using System.Collections.Generic;

namespace ClaimsReserveCalculator.ClaimsDataDomainEntities
{
    /// <summary>
    /// Represents the collection of claims data for a product.
    /// </summary>
    public class ProductClaimsDataCollection
    {
        public const int MINIMUM_VALID_YEAR_VALUE = 1800;

        /// <summary>
        /// Claims data sorted in order of origin year. 
        /// </summary>
        private readonly SortedList<int, ProductClaimsData> _claimsDataSortedByOriginYear;

        /// <summary>
        /// Product claims data items sorted in order of ascending origin year.
        /// </summary>
        public ICollection<ProductClaimsData> ProductClaimsDataItems => _claimsDataSortedByOriginYear.Values;

        /// <summary>
        /// Default constructor to perform initialisation.
        /// </summary>
        public ProductClaimsDataCollection()
        {
            _claimsDataSortedByOriginYear = new SortedList<int, ProductClaimsData>();
        }

        /// <summary>
        /// Constructor to perform initialisation including setting initial claims data.
        /// </summary>
        /// <param name="productClaimsData">Claims data for a product.</param>
        public ProductClaimsDataCollection(ProductClaimsData productClaimsData)
        {
            _claimsDataSortedByOriginYear = new SortedList<int, ProductClaimsData>();
            _claimsDataSortedByOriginYear.Add(productClaimsData.OriginYear, productClaimsData);
        }

        /// <summary>
        /// Adds product claims data to collection or replace recorded claims data for origin year 
        /// if origin year in the parameter matches an existing entry in the collection.
        /// </summary>
        /// <param name="claimsDataForProduct">The claims data for a product.</param>
        public void Add(ProductClaimsData claimsDataForProduct)
        {
            if (_claimsDataSortedByOriginYear.ContainsKey(claimsDataForProduct.OriginYear))
            {
                _claimsDataSortedByOriginYear[claimsDataForProduct.OriginYear] = claimsDataForProduct;
            }
            else
            {
                _claimsDataSortedByOriginYear.Add(claimsDataForProduct.OriginYear, claimsDataForProduct);
            }
        }

        /// <summary>
        /// Get the first claims data associated with the specified origin year parameter.
        /// </summary>
        /// <param name="originYear">The origin year for which claims data is wanted.</param>
        /// <returns></returns>
        public ProductClaimsData GetProductClaimsForOriginYear(int originYear)
        {
            if (originYear < MINIMUM_VALID_YEAR_VALUE)
            {
                throw new ArgumentException(
                    $"Didn't get product claims for origin year, invalid parameter value: {originYear}");
            }

            ProductClaimsData productClaimsData = null;

            if (_claimsDataSortedByOriginYear.ContainsKey(originYear))
            {
                productClaimsData = _claimsDataSortedByOriginYear[originYear];
            }

            return productClaimsData;
        }

        /// <summary>
        /// Adds any missing claims data entries for development years within a range.
        /// </summary>
        /// <param name="startYear">The minimum year for claims data.</param>
        /// <param name="endYear">The maximum year for claims data.</param>
        public void AddMissingDevelopmentYearsData(int startYear, int endYear)
        {
            if (startYear < MINIMUM_VALID_YEAR_VALUE || endYear < MINIMUM_VALID_YEAR_VALUE)
            {
                throw new ArgumentException(
                    "Failed to add missing development years data - invalid argument values");
            }

            foreach (var productClaimsData in _claimsDataSortedByOriginYear.Values)
            {
                productClaimsData.AddMissingDevelopmentYearsData(startYear, endYear);
            }
        }
    }
}
