using System;
using System.Collections.Generic;

namespace ClaimsReserveCalculator.ClaimsDataDomainEntities
{
    /// <summary>
    /// Represents the claims data for a product associated with an origin year.
    /// Each origin year can have multiple development years entries associated with it.
    /// </summary>
    public class ProductClaimsData
    {
        /// <summary>
        /// The default minimum permitted origin or development year.
        /// </summary>
        private const int MIN_VALID_YEAR = 1800;

        /// <summary>
        /// Incremental development years claims data - sorted according to origin year key.
        /// </summary>
        private readonly SortedList<int, DevelopmentYearClaimsData> _developmentYearsClaimsData;

        /// <summary>
        /// The origin year associated with the product claims data.
        /// </summary>
        public int OriginYear { get; private set; }

        /// <summary>
        /// Gets incremental development years claims data entries.
        /// An empty collection may be returned if no development years have been set.
        /// </summary>
        public IEnumerable<DevelopmentYearClaimsData> DevelopmentYearsClaimsData => 
            _developmentYearsClaimsData.Values;

        /// <summary>
        /// Constructor which performs initialisation and allows the origin year to be set.
        /// </summary>
        /// <param name="originYear">The origin year associated with the claims data.</param>
        public ProductClaimsData(int originYear)
        {
            if (originYear < MIN_VALID_YEAR)
            {
                throw new ArgumentException(
                    "Failed to create product claims data - origin year parameter is invalid.");
            }

            OriginYear = originYear;
            _developmentYearsClaimsData = new SortedList<int, DevelopmentYearClaimsData>();
        }

        /// <summary>
        /// Calculates the cumulative value of the claims data, by adding the 
        /// incremental values in the claims data for each of the development years.
        /// </summary>
        /// <returns>Returns the cumulative claims amount - can be zero if no claims are found.</returns>
        public double CalculateCumulativeClaimsValue()
        {
            double cumulativeClaimsValue = 0;

            foreach (var yearlyClaimsData in _developmentYearsClaimsData.Values)
            {
                cumulativeClaimsValue += yearlyClaimsData.IncrementalValue;
            }

            return cumulativeClaimsValue;
        }

        /// <summary>
        /// Update the recorded claims data for a development year.
        /// </summary>
        /// <param name="developmentYearClaimsData">The claims data for a particular development year.</param>
        public void UpdateDevelopmentYearClaimsData(DevelopmentYearClaimsData claimsDataForYear)
        {
            if (_developmentYearsClaimsData.ContainsKey(claimsDataForYear.DevelopmentYear))
            {
                _developmentYearsClaimsData[claimsDataForYear.DevelopmentYear] = claimsDataForYear;
            }
            else
            {
                _developmentYearsClaimsData.Add(claimsDataForYear.DevelopmentYear, claimsDataForYear);
            }
        }

        /// <summary>
        /// Adds data entries for any development years data that is missing from the collection.
        /// The incremental claims value for any year that is inserted is assumed to be zero.
        /// </summary>
        /// <param name="startYear">The start of the permitted year range</param>
        /// <param name="endYear">The end of the permitted year range</param>
        public void AddMissingDevelopmentYearsData(int startYear, int endYear)
        {
            if (startYear < MIN_VALID_YEAR || endYear < MIN_VALID_YEAR)
            {
                throw new ArgumentNullException("Can't add missing development year - parameters are invalid");
            }

            // prevent adding any development years before the origin year
            if (startYear < OriginYear)
            {
                startYear = OriginYear;
            }

            for (int year = startYear; year <= endYear; year++)
            {
                if (!_developmentYearsClaimsData.ContainsKey(year))
                {
                    double cumulativeValue = 0;
                    int previousYear = year - 1;

                    if (_developmentYearsClaimsData.ContainsKey(previousYear))
                    {
                        cumulativeValue = _developmentYearsClaimsData[previousYear].CumulativeValue;
                    }
                    
                    UpdateDevelopmentYearClaimsData(new DevelopmentYearClaimsData(year, 0, cumulativeValue));
                }
            }
        }
    }
}
