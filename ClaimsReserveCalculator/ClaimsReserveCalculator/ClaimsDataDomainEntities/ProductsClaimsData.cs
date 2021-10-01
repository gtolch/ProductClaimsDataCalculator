using System;
using System.Collections.Generic;
using System.Linq;

namespace ClaimsReserveCalculator.ClaimsDataDomainEntities
{
    /// <summary>
    /// Represents the claims data associated with a collection of products.
    /// </summary>
    public class ProductsClaimsData
    {
        /// <summary>
        /// Represents the default value when the origin year has not been set.
        /// </summary>
        public const int ORIGIN_YEAR_NOT_SET = int.MaxValue;

        /// <summary>
        /// Represents the minimum valid year that may be recorded in claims data.
        /// </summary>
        public const int MINIMUM_VALID_YEAR = 1500;

        /// <summary>
        /// Allows claim data to be looked up for a particular product name (specified by the key).
        /// </summary>
        private readonly IDictionary<string, ProductClaimsDataCollection> _productClaimsDataLookup;

        /// <summary>
        /// Stores each of the distinct development years within the current claims data.
        /// </summary>
        private readonly HashSet<int> _developmentYears;

        /// <summary>
        /// Returns all of the product names associated with the claims data.
        /// </summary>
        public IEnumerable<string> ProductNames => _productClaimsDataLookup.Keys;

        /// <summary>
        /// Stores the earliest origin year or ORIGIN_YEAR_NOT_SET if it hasn't been setup.
        /// </summary>
        public int EarliestOriginYear { get; private set; }

        /// <summary>
        /// Represents the total number of distinct development years (across all products).
        /// </summary>
        public int TotalDevelopmentYears => _developmentYears.Count;

        /// <summary>
        /// Constructor to initialise the product claims data. 
        /// </summary>
        public ProductsClaimsData()
        {
            _productClaimsDataLookup = new Dictionary<string, ProductClaimsDataCollection>();
            _developmentYears = new HashSet<int>();
            EarliestOriginYear = ORIGIN_YEAR_NOT_SET;
        }

        /// <summary>
        /// Gets the collection of claims data associated with the specified product name parameter.
        /// </summary>
        /// <param name="productName">The name of the product that we want claims data for.</param>
        /// <returns></returns>
        public IEnumerable<ProductClaimsData> GetProductClaimsDataCollection(string productName)
        {
            IEnumerable<ProductClaimsData> claimsDataCollection = null;

            if (_productClaimsDataLookup.ContainsKey(productName))
            {
                claimsDataCollection = _productClaimsDataLookup[productName].ProductClaimsDataItems;
            }

            return claimsDataCollection;
        }

        /// <summary>
        /// Gets the claims data for a product for all of the recorded development years.
        /// </summary>
        /// <param name="productName">The name of the product that we want claims data for.</param>
        /// <returns>Returns all development years claims data associated with the product.</returns>
        public IEnumerable<DevelopmentYearClaimsData> GetAllDevelopmentYearsClaimsData(string productName)
        {
            List<DevelopmentYearClaimsData> devYearsClaimsData = new List<DevelopmentYearClaimsData>();

            foreach (var claimsDataEntry in GetProductClaimsDataCollection(productName))
            {
                devYearsClaimsData.AddRange(claimsDataEntry.DevelopmentYearsClaimsData);
            }

            return devYearsClaimsData;
        }

        /// <summary>
        /// Get the first claims data entry associated with the 
        /// specified product name and origin year parameters.
        /// </summary>
        /// <param name="productName">The name of the product that we want claims data for.</param>
        /// <param name="originYear">The origin year.</param>
        /// <returns>The product claims data for the specified origin year or null if not found.</returns>
        public ProductClaimsData GetProductClaimsForOriginYear(string productName, int originYear)
        {
            if (string.IsNullOrWhiteSpace(productName) || originYear < MINIMUM_VALID_YEAR)
            {
                throw new ArgumentException(
                    "Failed to get product claims for origin year - invalid argument values");
            }

            ProductClaimsData claimsData = null;

            if (_productClaimsDataLookup.ContainsKey(productName))
            {
                claimsData = _productClaimsDataLookup[productName].GetProductClaimsForOriginYear(originYear);
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

            UpdateDevelopmentYearsRecord(claimsData.DevelopmentYearsClaimsData);

            if (EarliestOriginYear > claimsData.OriginYear)
            {
                EarliestOriginYear = claimsData.OriginYear;
            }
        }

        /// <summary>
        /// Adds entries for any missing years to the claims data. Placeholder product claims data 
        /// entries will be added for any years inferred to be missing from the recorded data.
        /// </summary>
        public void AddMissingYearsData()
        {
            if (_productClaimsDataLookup.Any() && (EarliestOriginYear > MINIMUM_VALID_YEAR) &&
                (EarliestOriginYear != ORIGIN_YEAR_NOT_SET))
            {
                foreach (var productName in _productClaimsDataLookup.Keys)
                {
                    // Add entries for any origin years that may be missing from the product claims data.
                    AddMissingOriginYearsData(productName);
                    _productClaimsDataLookup[productName].AddMissingDevelopmentYearsData(
                        EarliestOriginYear, _developmentYears.Max());
                }
            }
        }

        /// <summary>
        /// Adds any missing origin years to the claims data. Placeholder product claims data 
        /// entries will be added for origin years inferred to be missing from the recorded data.
        /// </summary>
        /// <param name="productName">the name of the product associated with the data</param>
        private void AddMissingOriginYearsData(string productName)
        {
            for (int originYear = EarliestOriginYear; originYear <= _developmentYears.Max(); originYear++)
            {
                if (_productClaimsDataLookup[productName].GetProductClaimsForOriginYear(originYear) == null)
                {
                    ProductClaimsData claimsData = new ProductClaimsData(originYear);
                    UpdateProductClaimsData(productName, claimsData);
                }
            }
        }

        /// <summary>
        /// Updates the set containing development years associated with all the products,
        /// if the stored claims data contains a development year which hasn't already been recorded.
        /// </summary>
        /// <param name="devYearsData">The development years claims data that should be recorded.</param>
        private void UpdateDevelopmentYearsRecord(IEnumerable<DevelopmentYearClaimsData> devYearsData)
        {
            foreach (var claimsDataForYear in devYearsData)
            {
                if (!_developmentYears.Contains(claimsDataForYear.DevelopmentYear))
                {
                    _developmentYears.Add(claimsDataForYear.DevelopmentYear);
                }
            }
        }
    }
}
