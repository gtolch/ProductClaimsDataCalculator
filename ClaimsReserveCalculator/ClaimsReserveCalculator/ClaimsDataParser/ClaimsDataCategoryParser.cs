using ClaimsReserveCalculator.ClaimsDataParserInterfaces;
using ClaimsReserveCalculator.Properties;
using System;

namespace ClaimsReserveCalculator.ClaimsDataParser
{
    /// <summary>
    /// A parser of products claims data categories.
    /// </summary>
    public class ClaimsDataCategoryParser : IClaimsDataCategoryParser
    {
        /// <summary>
        /// The separator char that delineates data items on a line.
        /// </summary>
        private readonly char _dataItemSeparator;

        /// <summary>
        /// Constructor that initialises the parser and sets the data separator.
        /// </summary>
        /// <param name="dataItemSeparator">The data item delimiter.</param>
        public ClaimsDataCategoryParser(char dataItemSeparator = ',')
        {
            if (char.IsLetterOrDigit(dataItemSeparator))
            {
                throw new ArgumentException(
                    "Failed to create data category parser - separator is alphanumeric char");
            }

            _dataItemSeparator = dataItemSeparator;
        }

        /// <summary>This method extracts claims category info from raw input data.</summary>
        /// <param name="inputData">raw input data which may contain multiple category titles.</param>
        /// <returns>the claims data info if successfully parsed.</returns>    
        public ClaimsDataCategoryInfo ParseClaimsDataCategoryInfo(string inputData)
        {
            if (string.IsNullOrEmpty(inputData))
            {
                throw new ArgumentException(
                    "Cannot parse category titles in data parser - input data is null or empty");
            }

            ClaimsDataCategoryInfo claimsDataCategoryInfo = new ClaimsDataCategoryInfo();
            string[] titleItems = inputData.Split(_dataItemSeparator);

            if (titleItems != null)
            {
                for (int index = 0; index < titleItems.Length; index++)
                {
                    claimsDataCategoryInfo = SetupDataCategoryPositionInfo(
                        titleItems[index], index, claimsDataCategoryInfo);
                }
            }

            return claimsDataCategoryInfo;
        }

        /// <summary>
        /// Tries to match the data category title item to known values and store the 
        /// position index of the data category, to cater for different orders of category titles.
        /// </summary>
        /// <param name="inputDataItem">The input data item to try to match.</param>
        /// <param name="inputDataItemIndex">The index of the data item.</param>
        /// <param name="claimsDataCategoryInfo">The claims data category info.</param>
        /// <returns>The claims data category info that may have been updated.</returns>
        private ClaimsDataCategoryInfo SetupDataCategoryPositionInfo(string inputDataItem, int inputDataItemIndex, 
            ClaimsDataCategoryInfo claimsDataCategoryInfo)
        {
            // Do case insensitive string comparison and set the item index if we get a match.
            string trimmedlowerCaseData = inputDataItem.Trim().ToLower();
            if (trimmedlowerCaseData == Resources.LowerCaseProductTitle)
            {
                claimsDataCategoryInfo.ProductIndex = inputDataItemIndex;
            }
            else if (trimmedlowerCaseData == Resources.LowerCaseOriginYearTitle)
            {
                claimsDataCategoryInfo.OriginYearIndex = inputDataItemIndex;
            }
            else if (trimmedlowerCaseData == Resources.LowerCaseDevelopmentYearTitle)
            {
                claimsDataCategoryInfo.DevelopmentYearIndex = inputDataItemIndex;
            }
            else if (trimmedlowerCaseData == Resources.LowerCaseIncrementalValueTitle)
            {
                claimsDataCategoryInfo.IncrementalValueIndex = inputDataItemIndex;
            }

            return claimsDataCategoryInfo;
        }
    }
}
