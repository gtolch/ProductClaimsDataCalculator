using ClaimsReserveCalculator.ClaimsDataDomainEntities;
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
        /// Parses raw input lines of data for products claims.
        /// </summary>
        /// <param name="inputDataLines">raw input data lines containing claims data.</param>
        /// <returns>Returns the parsed product claims data.</returns>
        ProductsClaimsData ParseProductsClaimsInputData(IEnumerable<string> inputDataLines);
    }
}
