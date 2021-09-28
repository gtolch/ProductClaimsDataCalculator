using ClaimsReserveCalculator.ClaimsDataDomainEntities;
using ClaimsReserveCalculator.ClaimsDataParserInterfaces;
using ClaimsReserveCalculator.CustomExceptions;
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
                throw new ArgumentException(
                    "Failed to create incremental claims parser - separator is alphanumeric char");
            }

            _dataItemSeparator = dataItemSeparator;
        }

        /// <summary>
        /// Parses the incremental claims data and updates the products claims data.
        /// </summary>
        /// <param name="dataLinesToParse">The data line entries to be parsed.</param>
        /// <param name="productsClaimsData">The products claims data.</param>
        /// <param name="claimsDataCategoryInfo">Contains category and index info.</param>
        /// <returns>The products claims data which may have been updated.</returns>
        public ProductsClaimsData ParseIncrementalClaimsData(IEnumerable<string> dataLinesToParse,
            ProductsClaimsData productsClaimsData, ClaimsDataCategoryInfo claimsDataCategoryInfo)
        {
            if (dataLinesToParse == null || productsClaimsData == null || (!dataLinesToParse.Any()))
            {
                throw new ArgumentException(
                    "Failed to parse incremental claims data - invalid parameter");
            }

            WarningCount = 0;

            ProductClaimsData claimsDataForProduct = null;

            foreach (var dataLine in dataLinesToParse)
            {
                WarningCount = 0;

                string[] dataItems = dataLine.Split(_dataItemSeparator);

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
                        claimsDataForProduct = SetupProductClaimsData(
                            productName, originYear, developmentYear, incrementalValue, productsClaimsData);
                    }
                    else
                    {
                        throw new ParseClaimsInputDataException("Aborting parse attempt. Exceeded warning limit");
                    }
                }

            }

            return productsClaimsData;
        }

        /// <summary>
        /// Setup and register product claims data using the supplied parameters.
        /// </summary>
        /// <param name="productName">The name of the product.</param>
        /// <param name="originYear">The origin year for the claims data.</param>
        /// <param name="developmentYear">The development year for the claims data</param>
        /// <param name="incrementalValue">The incremental value for the claims data</param>
        /// <returns></returns>
        private ProductClaimsData SetupProductClaimsData(
            string productName, int originYear, int developmentYear, double incrementalValue, ProductsClaimsData _productsClaimsData)
        {
            // Retrieve any existing product claims data for the current origin year
            ProductClaimsData productClaimsData =
                _productsClaimsData.GetProductClaimsForOriginYear(productName, originYear);

            if (productClaimsData == null)
            {
                productClaimsData = new ProductClaimsData(originYear);
            }

            double cumulativeValue = incrementalValue + productClaimsData.CalculateCumulativeClaimsValue();
            DevelopmentYearClaimsData devYearClaimsData = new DevelopmentYearClaimsData(
                developmentYear, incrementalValue, cumulativeValue);

            productClaimsData.UpdateDevelopmentYearClaimsData(devYearClaimsData);
            _productsClaimsData.UpdateProductClaimsData(productName, productClaimsData);

            return productClaimsData;
        }
    }
}
