using ClaimsReserveCalculator.ClaimsDataDomainEntities;
using ClaimsReserveCalculator.ClaimsDataIO;
using System;
using Xunit;
using Moq;
using System.IO;
using ClaimsReserveCalculator.ClaimsDataProcessing;

namespace ClaimsDataTestProject
{
    public class ProductClaimsDataFileWriterTests
    {
        [Fact]
        public void WriteProductClaimsOutputData_NullOutputDestination_ThrowsArgumentException()
        {
            var mockDataProcessor = new Mock<IClaimsDataManager>();
            ProductsClaimsDataFileWriter dataWriter = new ProductsClaimsDataFileWriter(mockDataProcessor.Object);
            var mockClaimsData = new Mock<ProductsClaimsData>();
            string outputDestination = null;
            int totalDevelopmentYears = 1;

            Assert.Throws<ArgumentException>(
                () => dataWriter.WriteProductClaimsOutputData(outputDestination, totalDevelopmentYears));
        }

        [Fact]
        public void WriteProductClaimsOutputData_TestStringOutputDestination_WritesToFile()
        {
            var mockDataProcessor = new Mock<IClaimsDataManager>();
            ProductsClaimsDataFileWriter dataWriter = new ProductsClaimsDataFileWriter(mockDataProcessor.Object);
            var mockClaimsData = new Mock<ProductsClaimsData>();
            string outputDestination = "test.txt";
            int totalDevelopmentYears = 1;

            dataWriter.WriteProductClaimsOutputData(outputDestination, totalDevelopmentYears);

            Assert.True(File.Exists(outputDestination));
        }
    }
}
