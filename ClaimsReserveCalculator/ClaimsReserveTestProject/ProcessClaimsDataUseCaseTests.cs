using ClaimsReserveCalculator.ClaimsDataDomainEntities;
using ClaimsReserveCalculator.ClaimsDataInputAndOutputInterfaces;
using ClaimsReserveCalculator.ClaimsDataParserInterfaces;
using ClaimsReserveCalculator.ClaimsDataProcessing;
using ClaimsReserveCalculator.CustomExceptions;
using ClaimsReserveCalculator.UseCaseInteractors;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ClaimsReserveTestProject
{
    public class ProcessClaimsDataUseCaseTests
    {
        [Fact]
        public void SaveCumulativeClaimsData_InvalidSourceFilePathString_ThrowsInvalidInputSourceFileException()
        {
            var mockDataReader = new Mock<IProductsClaimsDataReader>();
            var mockDataWriter = new Mock<IProductsClaimsDataWriter>();
            var mockDataParser = new Mock<IProductsClaimsDataParser>();
            var mockDataProcessor = new Mock<IClaimsDataManager>();
            var mockDevYearSet = new Mock<IDevelopmentYearSet>();
            string sourceFilePath = "dfggfgddggdg.txt";
            string outputFilePath = "save1.txt";
            mockDataReader.Setup(
                reader => reader.IsInputSourceValid(sourceFilePath)).Returns(false);

            ProcessClaimsDataUseCase productsClaimsDataUseCase = new ProcessClaimsDataUseCase(
                mockDataReader.Object, mockDataWriter.Object, mockDataParser.Object, 
                mockDataProcessor.Object, mockDevYearSet.Object);

            Assert.Throws<InvalidInputSourceFileException>(
                () => productsClaimsDataUseCase.SaveCumulativeClaimsData(sourceFilePath, outputFilePath));
        }

        [Fact]
        public void SaveCumulativeClaimsData_ValidSourceFilePathString_WritesProductsClaimsOutputData()
        {
            var mockDataReader = new Mock<IProductsClaimsDataReader>();
            var mockDataWriter = new Mock<IProductsClaimsDataWriter>();
            var mockDataParser = new Mock<IProductsClaimsDataParser>();
            var mockDataManager = new Mock<IClaimsDataManager>();
            var mockDevYearSet = new Mock<IDevelopmentYearSet>();
            IEnumerable<ParsedProductClaimsData> mockParsedProductClaimsData = new List<ParsedProductClaimsData>();
            ProcessClaimsDataUseCase productsClaimsDataUseCase = new ProcessClaimsDataUseCase(
                mockDataReader.Object, mockDataWriter.Object, mockDataParser.Object, 
                mockDataManager.Object, mockDevYearSet.Object);
            string sourceFilePath = "input1.txt";
            string outputFilePath = "save1.txt";
            string[] rawInputData = { "2000, 2001, 2002" };
            mockDataReader.Setup(
                reader => reader.IsInputSourceValid(sourceFilePath)).Returns(true);
            mockDataReader.Setup(
                reader => reader.ReadRawInputData(sourceFilePath)).Returns(rawInputData); 
            mockDataParser.Setup(
                parser => parser.ParseProductsClaimsData(
                    rawInputData)).Returns(mockParsedProductClaimsData);

            productsClaimsDataUseCase.SaveCumulativeClaimsData(sourceFilePath, outputFilePath);

            mockDataWriter.Verify(mock => mock.WriteProductClaimsOutputData(
                outputFilePath, It.IsAny<int>()), Times.Once);
        }
    }
}
