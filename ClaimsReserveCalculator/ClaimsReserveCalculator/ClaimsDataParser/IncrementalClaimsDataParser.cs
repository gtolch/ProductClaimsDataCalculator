using ClaimsReserveCalculator.ClaimsDataParserInterfaces;
using ClaimsReserveCalculator.CustomExceptions;
using ClaimsReserveCalculator.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClaimsReserveCalculator.ClaimsDataParser
{
    /// <summary>
    /// A parser of incremental claims data for products.
    /// </summary>
    public class IncrementalClaimsDataParser : IIncrementalClaimsDataParser
    {
        /// <summary>
        /// The maximum tolerated number of parser warnings.
        /// It is an arbitrary figure that could be adjusted.
        /// </summary>
        private const int MAX_PARSE_WARNINGS_TOLERANCE = 2;

        /// <summary>
        /// The total number of parser warnings. Can be optionally used to 
        /// report number of issues in log or produce reports that can be sent to the UI. 
        /// </summary>
        public int WarningCount { get; private set; }

        /// <summary>
        /// The separator char that delineates data items on a line.
        /// </summary>
        readonly private char _dataItemSeparator;

        /// <summary>
        /// Constructor to initialise the parser and set data separator.
        /// </summary>
        /// <param name="dataItemSeparator">Separator that delineates data items.</param>
        public IncrementalClaimsDataParser(char dataItemSeparator = ',')
        {
            if (char.IsLetterOrDigit(dataItemSeparator))
            {
                throw new ArgumentException(Resources.SeparatorIsAlphanumericValue);
            }

            _dataItemSeparator = dataItemSeparator;
        }

        /// <summary>
        /// Parses the incremental claims data and returns the parsed products claims data.
        /// </summary>
        /// <param name="dataLinesToParse">The data line entries to be parsed.</param>
        /// <param name="productsClaimsData">The products claims data.</param>
        /// <param name="claimsDataCategoryInfo">Contains category and index info.</param>
        /// <returns>The parsed products claims data.</returns>
        public IEnumerable<ParsedProductClaimsData> /*ProductsClaimsData*/ ParseIncrementalClaimsData(IEnumerable<string> dataLinesToParse,
            /*ProductsClaimsData productsClaimsData,*/ ClaimsDataCategoryInfo claimsDataCategoryInfo)
        {
            if (dataLinesToParse == null || /*productsClaimsData == null ||*/ (!dataLinesToParse.Any()))
            {
                throw new ArgumentException(Resources.IncrementalClaimsDataInvalidParameter);
            }

            WarningCount = 0;
            List<ParsedProductClaimsData> parsedProductsData = new List<ParsedProductClaimsData>();
            ParsedProductClaimsData parsedProductDataEntry = null; ;

            foreach (var dataLine in dataLinesToParse)
            {
                parsedProductDataEntry = ParseInputDataLine(dataLine, /*productsClaimsData,*/ claimsDataCategoryInfo);

                if (parsedProductDataEntry != null)
                {
                    parsedProductsData.Add(parsedProductDataEntry);
                }
            }

            //return productsClaimsData;
            return parsedProductsData;
        }

        /// <summary>
        /// Parses an input date line of incremental claims data and updates the products claims data.
        /// </summary>
        /// <param name="dataLineToParse">The data line entries to be parsed.</param>
        /// <param name="productsClaimsData">The products claims data.</param>
        /// <param name="claimsDataCategoryInfo">Contains category and index info.</param>
        /// <returns>The products claims data which may have been updated.</returns>
        public ParsedProductClaimsData ParseInputDataLine(string dataLineToParse, 
            /*ProductsClaimsData productsClaimsData,*/ ClaimsDataCategoryInfo claimsDataCategoryInfo)
        {
            WarningCount = 0;

            string[] dataItems = dataLineToParse.Split(_dataItemSeparator);
            ParsedProductClaimsData productDataEntry = null;

            if (dataItems != null && dataItems.Any())
            {
                string productName = string.Empty;
                if (claimsDataCategoryInfo.ProductIndex < dataItems.Length)
                {
                    productName = dataItems[claimsDataCategoryInfo.ProductIndex].Trim();
                }
                else
                {
                    WarningCount++;
                }

                int originYear = 0;
                if ((claimsDataCategoryInfo.OriginYearIndex >= dataItems.Length) ||
                    (!int.TryParse(dataItems[claimsDataCategoryInfo.OriginYearIndex], out originYear)))
                {
                    WarningCount++;
                }

                int developmentYear = 0;
                if ((claimsDataCategoryInfo.DevelopmentYearIndex >= dataItems.Length) ||
                    (!int.TryParse(
                        dataItems[claimsDataCategoryInfo.DevelopmentYearIndex], out developmentYear)))
                {
                    WarningCount++;
                }

                double incrementalValue = 0;
                if ((claimsDataCategoryInfo.IncrementalValueIndex >= dataItems.Length) ||
                    (!double.TryParse(
                        dataItems[claimsDataCategoryInfo.IncrementalValueIndex], out incrementalValue)))
                {
                    WarningCount++;
                }

                if (WarningCount <= MAX_PARSE_WARNINGS_TOLERANCE)
                {
                    productDataEntry = new ParsedProductClaimsData(productName, originYear, developmentYear, incrementalValue, WarningCount);

                    /*SetupProductClaimsData(
                        productName, originYear, developmentYear, incrementalValue, productsClaimsData);*/
                }
                else
                {
                    throw new ParseClaimsInputDataException(Resources.ExceededWarningLimit);
                }
            }

            return productDataEntry;
        }
    }
}
