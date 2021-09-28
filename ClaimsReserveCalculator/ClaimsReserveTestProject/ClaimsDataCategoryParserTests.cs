using ClaimsReserveCalculator.ClaimsDataParser;
using ClaimsReserveCalculator.ClaimsDataParserInterfaces;
using System;
using Xunit;

namespace ClaimsReserveTestProject
{
    public class ClaimsDataCategoryParserTests
    {
        [Fact]
        public void Constructor_AlphanumericSeparatorChar_ThrowsArgumentException()
        {
            char separator = '4';

            Assert.Throws<ArgumentException>(
                () => new ClaimsDataCategoryParser(separator));
        }

        [Fact]
        public void ParseClaimsDataCategoryInfo_InputDataStringWithNoSeparators_CategoryIndexPropertiesAreUnchanged()
        {
            ClaimsDataCategoryParser parser = new ClaimsDataCategoryParser();
            string inputData = "Origin YearDevelopment YearIncremental Value Product";

            var categoryInfo = parser.ParseClaimsDataCategoryInfo(inputData);

            Assert.Equal(ClaimsDataCategoryInfo.DEFAULT_PRODUCT_POSITION_INDEX, categoryInfo.ProductIndex);
            Assert.Equal(ClaimsDataCategoryInfo.DEFAULT_ORIGIN_YEAR_INDEX, categoryInfo.OriginYearIndex);
            Assert.Equal(ClaimsDataCategoryInfo.DEFAULT_DEVELOPMENT_YEAR_INDEX, categoryInfo.DevelopmentYearIndex);
            Assert.Equal(ClaimsDataCategoryInfo.DEFAULT_INCREMENTAL_VALUE_INDEX, categoryInfo.IncrementalValueIndex);
        }

        [Fact]
        public void ParseClaimsDataCategoryInfo_CommaSeparatedInputDataString_UpdatesCategoryIndexProperties()
        {
            ClaimsDataCategoryParser parser = new ClaimsDataCategoryParser();
            string inputData = "Origin Year, Development Year, Incremental Value, Product";

            var categoryInfo = parser.ParseClaimsDataCategoryInfo(inputData);

            Assert.Equal(0, categoryInfo.OriginYearIndex);
            Assert.Equal(1, categoryInfo.DevelopmentYearIndex);
            Assert.Equal(2, categoryInfo.IncrementalValueIndex);
            Assert.Equal(3, categoryInfo.ProductIndex);
        }
    }
}
