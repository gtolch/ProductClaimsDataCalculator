using ClaimsReserveCalculator.ClaimsDataParser;
using ClaimsReserveCalculator.ClaimsDataParserInterfaces;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace ClaimsDataTestProject
{
    public class ProductClaimsDataParserTests
    {
        [Fact]
        public void ParseProductsClaimsInputData_NullInputData_ThrowsArgumentException()
        {
            var mockDataCategoryParser = new Mock<IClaimsDataCategoryParser>();
            var mockIncrementalDataParser = new Mock<IIncrementalClaimsDataParser>();
            var dataParser = new ProductsClaimsDataParser(
                mockDataCategoryParser.Object, mockIncrementalDataParser.Object);
            List<string> inputData = null;

            Assert.Throws<ArgumentException>(() => dataParser.ParseProductsClaimsInputData(inputData));
        }

        [Fact]
        public void ParseProductsClaimsInputData_EmptyListInputData_ThrowsArgumentException()
        {
            var mockDataCategoryParser = new Mock<IClaimsDataCategoryParser>();
            var mockIncrementalDataParser = new Mock<IIncrementalClaimsDataParser>();
            var dataParser = new ProductsClaimsDataParser(
                mockDataCategoryParser.Object, mockIncrementalDataParser.Object);
            List<string> inputData = new List<string>();

            Assert.Throws<ArgumentException>(() => dataParser.ParseProductsClaimsInputData(inputData));
        }

        [Theory]
        [InlineData("Comp", "Non-comp1")]
        [InlineData("Comp1", "Non-comp2")]
        public void ParseProductsClaimsInputData_MultipleStringsInput_ReturnsProductsClaimsData(string inputLine1, string inputLine2)
        {
            var mockCategoryParser = new Mock<IClaimsDataCategoryParser>();
            var mockIncrementalParser = new Mock<IIncrementalClaimsDataParser>();
            var dataParser = new ProductsClaimsDataParser(mockCategoryParser.Object, mockIncrementalParser.Object);
            List<string> inputData = new List<string>() { inputLine1, inputLine2 };

            var productsClaimsData = dataParser.ParseProductsClaimsInputData(inputData);

            Assert.NotNull(productsClaimsData);
        }
    }
}
