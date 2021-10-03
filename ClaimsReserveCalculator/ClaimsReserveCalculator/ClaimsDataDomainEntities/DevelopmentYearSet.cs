using System;
using System.Collections.Generic;
using System.Linq;

namespace ClaimsReserveCalculator.ClaimsDataDomainEntities
{
    /// <summary>
    /// Encapsulates a set of distinct claims data development years.
    /// </summary>
    public class DevelopmentYearSet : IDevelopmentYearSet
    {
        /// <summary>
        /// Stores each of the distinct development years within the current claims data.
        /// </summary>
        private readonly HashSet<int> _developmentYears;

        /// <summary>
        /// Represents the total number of distinct development years.
        /// </summary>
        public int Count => _developmentYears.Count;

        /// <summary>
        /// Constructor to initialise the underlying development years collection data. 
        /// </summary>
        public DevelopmentYearSet()
        {
            _developmentYears = new HashSet<int>();
        }

        /// <summary>
        /// Erases the development year set data.
        /// </summary>
        public void Clear()
        {
            _developmentYears.Clear();
        }

        /// <summary>
        /// Gets the max development year within the set of development year claims data.
        /// </summary>
        /// <returns>Returns the max development year value or -1 if no development years found</returns>
        public int Max()
        {
            int max = ClaimsDataConstants.NO_DEVELOPMENT_YEARS_VALUE;

            if (_developmentYears != null && _developmentYears.Any())
            {
                max = _developmentYears.Max();
            }

            return max;
        }

        /// <summary>
        /// Updates the set containing development years associated with all the products,
        /// if the supplied development year hasn't previously been recorded.
        /// </summary>
        /// <returns>True if the specified year was added, false otherwise e.g. if year already exists</returns>
        public bool AddDevelopmentYear(int developmentYear) => _developmentYears.Add(developmentYear);
    }
}
