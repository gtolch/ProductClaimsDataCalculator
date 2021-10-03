
namespace ClaimsReserveCalculator.ClaimsDataInputAndOutputInterfaces
{
    /// <summary>
    /// The interface to a file writer for saving products claims 
    /// reserve data, including cumulative claims data.
    /// </summary>
    public interface IProductsClaimsDataWriter
    {
        /// <summary>
        /// Saves the claims data for a group of products.
        /// Any existing destination resource (with the same name) should be overwritten.
        /// </summary>
        /// <param name="outputDestination">Fully qualified filename with path / URL.</param>
        /// <param name="totalDevelopmentYears">Total development years in the claims data.</param>
        void WriteProductClaimsOutputData(string outputDestination, int totalDevelopmentYears);
    }
}
