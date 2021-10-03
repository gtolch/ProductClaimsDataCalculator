
namespace ClaimsReserveCalculator.ClaimsDataDomainEntities
{
    /// <summary>
    /// Interface to a set of distinct claims data development years.
    /// </summary>
    public interface IDevelopmentYearSet
    {
        /// <summary>
        /// Represents the total number of distinct development years.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Erases the development year set data.
        /// </summary>
        void Clear();

        /// <summary>
        /// Gets the max development year within the set of development year claims data.
        /// </summary>
        /// <returns>Returns the max development year value or -1 if no development years found</returns>
        int Max();

        /// <summary>
        /// Updates the set containing development years associated with all the products,
        /// if the supplied development year hasn't previously been recorded.
        /// </summary>
        /// <returns>True if the specified year was added, false otherwise e.g. if year already exists</returns>
        bool AddDevelopmentYear(int developmentYear);
    }
}
