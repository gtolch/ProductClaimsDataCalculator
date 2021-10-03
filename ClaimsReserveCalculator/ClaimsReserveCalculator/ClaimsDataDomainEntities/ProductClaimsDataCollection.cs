using ClaimsReserveCalculator.Properties;
using System;
using System.Collections.Generic;

namespace ClaimsReserveCalculator.ClaimsDataDomainEntities
{
    /// <summary>
    /// Represents the collection of claims data for a product.
    /// </summary>
    public class ProductClaimsDataCollection
    {
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

            if (productClaimsData != null)
            {
                _claimsDataSortedByOriginYear.Add(productClaimsData.OriginYear, productClaimsData);
            }
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
        /// Gets the first claims data associated with a specified origin year.
        /// </summary>
        /// <param name="originYear">The origin year for which claims data is wanted.</param>
        /// <returns>Returns the product claims data associated with an origin year.</returns>
        public ProductClaimsData GetProductClaimsForOriginYear(int originYear)
        {
            if (!ClaimsDataValidator.IsClaimsYearValid(originYear))
            {
                throw new ArgumentException(Resources.DidntGetProductClaimsForOriginYear);
            }

            ProductClaimsData productClaimsData = null;

            if (_claimsDataSortedByOriginYear.ContainsKey(originYear))
            {
                productClaimsData = _claimsDataSortedByOriginYear[originYear];
            }

            return productClaimsData;
        }

        /// <summary>
        /// Adds entries for any missing years to the claims data. Placeholder product claims data 
        /// entries will be added for any years inferred to be missing from the recorded data.
        /// </summary>
        /// <param name="earliestOriginYear">The lowest origin year to consider.</param>
        /// <param name="maxDevelopmentYear">The highest origin year to consider.</param>
        public void AddMissingYearsData(int earliestOriginYear, int maxDevelopmentYear)
        {
            if (!ClaimsDataValidator.AreClaimsYearsValid(earliestOriginYear, maxDevelopmentYear))
            {
                throw new ArgumentException(Resources.FailedToAddMissingYearsData);
            }

            AddMissingOriginYearsData(earliestOriginYear, maxDevelopmentYear);
            AddMissingDevelopmentYearsData(earliestOriginYear, maxDevelopmentYear);
        }

        /// <summary>
        /// Adds any missing claims data entries for development years within a range.
        /// </summary>
        /// <param name="startYear">The minimum year for claims data.</param>
        /// <param name="endYear">The maximum year for claims data.</param>
        private void AddMissingDevelopmentYearsData(int startYear, int endYear)
        {
            if (!ClaimsDataValidator.AreClaimsYearsValid(startYear, endYear))
            {
                throw new ArgumentException(Resources.FailedToAddMissingDevYearsData);
            }

            foreach (var productClaimsData in _claimsDataSortedByOriginYear.Values)
            {
                productClaimsData.AddMissingDevelopmentYearsData(startYear, endYear);
            }
        }

        /// <summary>
        /// Adds any missing origin years to the claims data. Placeholder product claims data 
        /// entries will be added for origin years inferred to be missing from the recorded data.
        /// </summary>
        /// <param name="earliestOriginYear">the earliest origin year of claims data.</param>
        /// <param name="maxDevelopmentYear">The maximum development year of claims data.</param>
        private void AddMissingOriginYearsData(int earliestOriginYear, int maxDevelopmentYear)
        {
            if (!ClaimsDataValidator.AreClaimsYearsValid(earliestOriginYear, maxDevelopmentYear))
            {
                throw new ArgumentException(Resources.FailedToAddMissingOriginYearsData);
            }

            for (int originYear = earliestOriginYear; originYear <= maxDevelopmentYear; originYear++)
            {
                if (GetProductClaimsForOriginYear(originYear) == null)
                {
                    ProductClaimsData claimsData = new ProductClaimsData(originYear);
                    Add(claimsData);
                }
            }
        }
    }
}
