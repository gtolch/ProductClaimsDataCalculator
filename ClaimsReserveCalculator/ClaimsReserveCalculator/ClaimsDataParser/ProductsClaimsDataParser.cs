using ClaimsReserveCalculator.ClaimsDataDomainEntities;
using ClaimsReserveCalculator.ClaimsDataParserInterfaces;
using ClaimsReserveCalculator.CustomExceptions;
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
        IClaimsDataCategoryParser _claimsDataCategoryParser;

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
                throw new ArgumentNullException(
                    "Failed to create product claims data parser - invalid parameters");
            }

            _claimsDataCategoryParser = claimsDataCategoryParser;
            _incrementalClaimsDataParser = incrementalClaimsParser;
        }

        /// <summary>
        /// Converts raw input data into a structured object representing 
        /// the product claims data.
        /// </summary>
        /// <param name="inputDataLines">the raw input data lines to be parsed.</param>
        /// <returns>the product claims data parsed from the input data.</returns>
        public ProductsClaimsData ParseProductsClaimsInputData(IEnumerable<string> inputDataLines)
        {
            ProductsClaimsData productsClaimsData = new ProductsClaimsData();

            if (inputDataLines != null && inputDataLines.Count() > 0)
            {
                try
                {
                    ClaimsDataCategoryInfo claimsDataCategoryInfo = 
                        _claimsDataCategoryParser.ParseClaimsDataCategoryInfo(inputDataLines.ElementAt(0));

                    if (claimsDataCategoryInfo != null)
                    {
                        var inputDataMainBody = inputDataLines.ToList();

                        inputDataMainBody.RemoveRange(0, 1);
                        productsClaimsData = _incrementalClaimsDataParser.ParseIncrementalClaimsData(
                            inputDataMainBody, productsClaimsData, claimsDataCategoryInfo);
                    }
                }
                catch (Exception ex)
                {
                    throw new ParseClaimsInputDataException(
                        $"Error parsing claims reserve input data: {ex}", ex);
                }
            }
            else
            {
                throw new ArgumentException(
                    "Cannot parse input data in data parser - input parameter is null or empty");
            }

            return productsClaimsData;
        }
    }
}