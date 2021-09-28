using ClaimsReserveCalculator.ClaimsDataDomainEntities;

namespace ClaimsReserveCalculator.ClaimsDataInputAndOutputInterfaces
{
    /// <summary>
    /// The interface to a file writer for saving products claims 
    /// reserve data, including cumulative claims data.
    /// </summary>
    public interface IProductsClaimsDataWriter
    {
        /// <summary>
        /// Saves the supplied claims data for a group of products.
        /// Any existing destination resource (with the same name) should be overwritten.
        /// </summary>
        /// <param name="productsClaimsData">The claims data for a collection of products.</param>
        /// <param name="outputDestination">Fully qualified filename with path / URL.</param>
        void WriteProductClaimsOutputData(ProductsClaimsData productsClaimsData, string outputDestination);
    }
}
