using ClaimsReserveCalculator.ClaimsDataDomainEntities;
using ClaimsReserveCalculator.ClaimsDataParser;
using ClaimsReserveCalculator.ClaimsDataParserInterfaces;
using ClaimsReserveCalculator.CustomExceptions;
using Moq;
using System;
using Xunit;

namespace ClaimsReserveTestProject
{
    public class IncrementalClaimsDataParserTests
    {
        [Fact]
        public void Constructor_AlphanumericSeparatorChar_ThrowsArgumentException()
        {
            char separator = '4';

            Assert.Throws<ArgumentException>(
                () => new IncrementalClaimsDataParser(separator));
        }

        [Fact]
        public void ParseIncrementalClaimsData_NullDataLinesParameter_ThrowsArgumentException()
        {
            var mockClaimsData = new Mock<ProductsClaimsData>();
            var mockCategoryInfo = new Mock<ClaimsDataCategoryInfo>();

            var parser = new IncrementalClaimsDataParser();

            Assert.Throws<ArgumentException>(
                () => parser.ParseIncrementalClaimsData(null, mockClaimsData.Object, mockCategoryInfo.Object));
        }

        [Fact]
        public void ParseIncrementalClaimsData_NoSeparatorsInDataLines_ThrowsParseClaimsInputDataException()
        {
            string[] dataLinesToParse = { "Comp1 1990 1991 30", "Comp1 1991 1992 40" };
            var mockClaimsData = new Mock<ProductsClaimsData>();
            var mockCategoryInfo = new Mock<ClaimsDataCategoryInfo>();

            var parser = new IncrementalClaimsDataParser();

            Assert.Throws<ParseClaimsInputDataException>(
                () => parser.ParseIncrementalClaimsData(dataLinesToParse, mockClaimsData.Object, mockCategoryInfo.Object));
        }

        [Fact]
        public void ParseIncrementalClaimsData_ValidDataLinesParameter_ReturnsProductClaimsData()
        {
            string[] dataLinesToParse = { "Comp1,1990,1991,30", "Comp1,1991,1992,40" };
            var mockClaimsData = new Mock<ProductsClaimsData>();
            var mockCategoryInfo = new Mock<ClaimsDataCategoryInfo>();
            
            var parser = new IncrementalClaimsDataParser();
            var productClaimsData = parser.ParseIncrementalClaimsData(
                dataLinesToParse, mockClaimsData.Object, mockCategoryInfo.Object);

            Assert.NotNull(productClaimsData);
        }
    }
}
