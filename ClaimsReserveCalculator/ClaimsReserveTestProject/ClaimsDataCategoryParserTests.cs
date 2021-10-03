using ClaimsReserveCalculator.ClaimsDataParser;
using ClaimsReserveCalculator.ClaimsDataParserInterfaces;
using ClaimsReserveCalculator.CustomExceptions;
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
        public void ParseClaimsDataCategoryInfo_InputDataStringWithNoSeparators_ThrowsDataCategoryException()
        {
            ClaimsDataCategoryParser parser = new ClaimsDataCategoryParser();
            string inputData = "Origin YearDevelopment YearIncremental Value Product";

            Assert.Throws<InvalidClaimsDataCategoriesException>( () => parser.ParseClaimsDataCategoryInfo(inputData));
        }

        [Fact]
        public void ParseClaimsDataCategoryInfo_InputDataStringsContainSeparatorsButAreGarbled_DefaultCategoryIndexPositionPropertiesAreSet()
        {
            ClaimsDataCategoryParser parser = new ClaimsDataCategoryParser();
            string inputData = "Origin, YearDevelopme,nt YearIncremental, Value Product";

            var categoryInfo = parser.ParseClaimsDataCategoryInfo(inputData);

            Assert.Equal(ClaimsDataCategoryInfo.DEFAULT_PRODUCT_POSITION_INDEX, categoryInfo.ProductIndex);
            Assert.Equal(ClaimsDataCategoryInfo.DEFAULT_ORIGIN_YEAR_INDEX, categoryInfo.OriginYearIndex);
            Assert.Equal(ClaimsDataCategoryInfo.DEFAULT_DEVELOPMENT_YEAR_INDEX, categoryInfo.DevelopmentYearIndex);
            Assert.Equal(ClaimsDataCategoryInfo.DEFAULT_INCREMENTAL_VALUE_INDEX, categoryInfo.IncrementalValueIndex);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ParseClaimsDataCategoryInfo_NullOrEmptyInputDataStrings_UpdatesCategoryIndexProperties(
            string categoryInputDataToParse)
        {
            ClaimsDataCategoryParser parser = new ClaimsDataCategoryParser();

            Assert.Throws<ArgumentException>(
                () => parser.ParseClaimsDataCategoryInfo(categoryInputDataToParse));
        }

        [Theory]
        [InlineData("Origin Year,Development Year,Incremental Value,Product")]
        [InlineData("Origin Year, Development Year, Incremental Value, Product")]
        [InlineData("Origin Year,   Development Year, Incremental Value,    Product")]
        [InlineData("ORIGIN Year,     Development YEAr,IncrEMENtal Value,product")]
        public void ParseClaimsDataCategoryInfo_DifferentCommaSeparatedInputDataStrings_UpdatesCategoryIndexProperties(
            string categoryInputDataToParse)
        {
            ClaimsDataCategoryParser parser = new ClaimsDataCategoryParser();

            var categoryInfo = parser.ParseClaimsDataCategoryInfo(categoryInputDataToParse);

            Assert.Equal(0, categoryInfo.OriginYearIndex);
            Assert.Equal(1, categoryInfo.DevelopmentYearIndex);
            Assert.Equal(2, categoryInfo.IncrementalValueIndex);
            Assert.Equal(3, categoryInfo.ProductIndex);
        }
    }
}
