using ClaimsReserveCalculator.ClaimsDataDomainEntities;
using System.Collections.Generic;

namespace ClaimsReserveCalculator.ClaimsDataParserInterfaces
{
    /// <summary>
    /// Interface to a parser of incremental claims data for products.
    /// </summary>
    public interface IIncrementalClaimsDataParser
    {
        /// <summary>
        /// Parses the incremental claims data and updates the products claims data.
        /// </summary>
        /// <param name="dataLinesToParse">The data line entries to be parsed.</param>
        /// <param name="productsClaimsData">The products claims data.</param>
        /// <param name="claimsDataCategoryInfo">Contains category and index info.</param>
        /// <returns>The products claims data which may have been updated.</returns>
        ProductsClaimsData ParseIncrementalClaimsData(IEnumerable<string> dataLinesToParse,
            ProductsClaimsData productsClaimsData, ClaimsDataCategoryInfo claimsDataCategoryInfo);
    }
}
