using System;
using System.Collections.Generic;

namespace ClaimsReserveCalculator.ClaimsDataParserInterfaces
{
    /// <summary>
    /// Interface to a parser of products claims data categories..
    /// </summary>
    public interface IClaimsDataCategoryParser
    {
        /*int ProductIndex { get; }
        int OriginYearIndex { get; }
        int DevelopmentYearIndex { get; }
        int IncrementalValueIndex { get; }*/

        /// <summary>This method extracts claims category info from raw input data.</summary>
        /// <param name="inputData">raw input data which may contain multiple category titles.</param>
        /// <returns>the claims data info if successfully parsed.</returns>    
        ClaimsDataCategoryInfo ParseClaimsDataCategoryInfo(string inputData);
        //IEnumerable<string> ParseCategoryTitles(string inputData);
    }
}
