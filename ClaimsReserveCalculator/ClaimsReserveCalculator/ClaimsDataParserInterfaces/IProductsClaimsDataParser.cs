using System.Collections.Generic;

namespace ClaimsReserveCalculator.ClaimsDataParserInterfaces
{
    /// <summary>
    /// Interface for a parser that converts raw product claims input data 
    /// into domain entity objects than can be more easily processed & manipulated.
    /// </summary>
    public interface IProductsClaimsDataParser
    {
        /// <summary>
        /// Converts raw input data into a collection of parsed product claims data.
        /// </summary>
        /// <param name="inputDataLines">the raw input data lines to be parsed.</param>
        /// <returns>the product claims data parsed from the input data.</returns>
        IEnumerable<ParsedProductClaimsData> ParseProductsClaimsData(IEnumerable<string> inputDataLines);
    }
}
