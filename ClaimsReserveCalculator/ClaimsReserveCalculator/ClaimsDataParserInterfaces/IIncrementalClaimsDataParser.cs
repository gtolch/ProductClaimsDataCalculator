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
        /// <param name="claimsDataCategoryInfo">Contains category and index info.</param>
        /// <returns>The parsed collection of product data entries.</returns>
        IEnumerable<ParsedProductClaimsData> ParseIncrementalClaimsData(IEnumerable<string> dataLinesToParse,
            ClaimsDataCategoryInfo claimsDataCategoryInfo);
    }
}
