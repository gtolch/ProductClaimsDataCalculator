using System;

namespace ClaimsReserveCalculator.ClaimsDataParserInterfaces
{
    /// <summary>
    /// Information about the claims data categories.
    /// </summary>
    public class ClaimsDataCategoryInfo
    {
        /// <summary>
        /// The start index of the product field in the input data.
        /// </summary>
        public int ProductIndex { get; set; }

        /// <summary>
        /// The start index of the origin year field in the input data.
        /// </summary>
        public int OriginYearIndex { get; set; }

        /// <summary>
        /// The start index of the development year field in the input data.
        /// </summary>
        public int DevelopmentYearIndex { get; set; }

        /// <summary>
        /// The start index of the incremental value field in the input data.
        /// </summary>
        public int IncrementalValueIndex { get; set; }

        public const int DEFAULT_PRODUCT_POSITION_INDEX = 0;
        public const int DEFAULT_ORIGIN_YEAR_INDEX = 1;
        public const int DEFAULT_DEVELOPMENT_YEAR_INDEX = 2;
        public const int DEFAULT_INCREMENTAL_VALUE_INDEX = 3;

        /// <summary>
        /// Constructor that sets category indexes to default values.
        /// </summary>
        public ClaimsDataCategoryInfo()
        {
            ProductIndex = DEFAULT_PRODUCT_POSITION_INDEX;
            OriginYearIndex = DEFAULT_ORIGIN_YEAR_INDEX;
            DevelopmentYearIndex = DEFAULT_DEVELOPMENT_YEAR_INDEX;
            IncrementalValueIndex = DEFAULT_INCREMENTAL_VALUE_INDEX;
        }
    }
}
