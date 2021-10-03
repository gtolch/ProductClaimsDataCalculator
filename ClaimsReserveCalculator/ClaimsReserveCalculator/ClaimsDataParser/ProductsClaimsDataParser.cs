using ClaimsReserveCalculator.ClaimsDataParserInterfaces;
using ClaimsReserveCalculator.CustomExceptions;
using ClaimsReserveCalculator.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClaimsReserveCalculator.ClaimsDataParser
{
    /// <summary>
    /// Represeents a parser that converts raw product claims input data 
    /// into domain entities that can be more easily processed & manipulated.
    /// </summary>
    public class ProductsClaimsDataParser : IProductsClaimsDataParser
    {
        /// <summary>
        /// The parser for decoding claims data categories.
        /// </summary>
        private readonly IClaimsDataCategoryParser _claimsDataCategoryParser;

        /// <summary>
        /// The parser for decoding incremental claims data.
        /// </summary>
        private readonly IIncrementalClaimsDataParser _incrementalClaimsDataParser;

        /// <summary>This constructor initialises parsers and sets input data separator.</summary>
        /// <param name="claimsDataCategoryParser">Parser to decode claims data categories.</param>
        /// <param name="incrementalClaimsParser">Parser to decode incremental claims data.</param>
        public ProductsClaimsDataParser(IClaimsDataCategoryParser claimsDataCategoryParser,
            IIncrementalClaimsDataParser incrementalClaimsParser)
        {
            if (claimsDataCategoryParser == null || incrementalClaimsParser == null)
            {
                throw new ArgumentNullException(Resources.FailedToCreateProductClaimsParser);
            }

            _claimsDataCategoryParser = claimsDataCategoryParser;
            _incrementalClaimsDataParser = incrementalClaimsParser;
        }

        /// <summary>
        /// Converts raw input data into a collection of parsed product claims data.
        /// </summary>
        /// <param name="inputDataLines">the raw input data lines to be parsed.</param>
        /// <returns>the product claims data parsed from the input data.</returns>
        public IEnumerable<ParsedProductClaimsData> ParseProductsClaimsData(IEnumerable<string> inputDataLines)
        {
            IEnumerable<ParsedProductClaimsData> productsClaimsData = new List<ParsedProductClaimsData>();

            if (inputDataLines != null && inputDataLines.Count() > 0)
            {
                try
                {
                    ClaimsDataCategoryInfo claimsDataCategoryInfo = 
                        _claimsDataCategoryParser.ParseClaimsDataCategoryInfo(inputDataLines.ElementAt(0));

                    if (claimsDataCategoryInfo != null)
                    {
                        var inputDataMainBody = inputDataLines.ToList();

                        //remove header data
                        inputDataMainBody.RemoveRange(0, 1);

                        productsClaimsData = _incrementalClaimsDataParser.ParseIncrementalClaimsData(
                            inputDataMainBody, claimsDataCategoryInfo);
                    }
                }
                catch (Exception ex)
                {
                    throw new ParseClaimsInputDataException(Resources.ParseClaimsInputDataError, ex);
                }
            }
            else
            {
                throw new ArgumentException(Resources.CannotParseInputAsParamIsNullOrEmpty);
            }

            return productsClaimsData;
        }
    }
}