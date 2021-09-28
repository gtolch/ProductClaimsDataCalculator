using System;
using System.Collections.Generic;
using ClaimsReserveCalculator.ClaimsDataDomainEntities;
using ClaimsReserveCalculator.ClaimsDataInputAndOutputInterfaces;
using ClaimsReserveCalculator.ClaimsDataParserInterfaces;
using ClaimsReserveCalculator.CustomExceptions;
using ClaimsReserveCalculator.UseCaseInteractors;
using Moq;
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
            string sourceFilePath = "dfggfgddggdg.txt";
            string outputFilePath = "save1.txt";
            mockDataReader.Setup(
                reader => reader.IsInputSourceValid(sourceFilePath)).Returns(false);

            ProcessClaimsDataUseCase productsClaimsDataUseCase = new ProcessClaimsDataUseCase(
                mockDataReader.Object, mockDataWriter.Object, mockDataParser.Object);

            Assert.Throws<InvalidInputSourceFileException>(
                () => productsClaimsDataUseCase.SaveCumulativeClaimsData(sourceFilePath, outputFilePath));
        }

        [Fact]
        public void SaveCumulativeClaimsData_ValidSourceFilePathString_WritesProductsClaimsOutputData()
        {
            var mockDataReader = new Mock<IProductsClaimsDataReader>();
            var mockDataWriter = new Mock<IProductsClaimsDataWriter>();
            var mockDataParser = new Mock<IProductsClaimsDataParser>();
            var mockClaimsData = new Mock<ProductsClaimsData>();
            ProcessClaimsDataUseCase productsClaimsDataUseCase = new ProcessClaimsDataUseCase(
                mockDataReader.Object, mockDataWriter.Object, mockDataParser.Object);
            string sourceFilePath = "input1.txt";
            string outputFilePath = "save1.txt";
            string[] rawInputData = { "2000, 2001, 2002" };
            mockDataReader.Setup(
                reader => reader.IsInputSourceValid(sourceFilePath)).Returns(true);
            mockDataReader.Setup(
                reader => reader.ReadRawInputData(sourceFilePath)).Returns(rawInputData); 
            mockDataParser.Setup(
                parser => parser.ParseProductsClaimsInputData(
                    rawInputData)).Returns(mockClaimsData.Object);

            productsClaimsDataUseCase.SaveCumulativeClaimsData(sourceFilePath, outputFilePath);

            mockDataWriter.Verify(mock => mock.WriteProductClaimsOutputData(
                It.IsAny<ProductsClaimsData>(), outputFilePath), Times.Once);
        }
    }
}
