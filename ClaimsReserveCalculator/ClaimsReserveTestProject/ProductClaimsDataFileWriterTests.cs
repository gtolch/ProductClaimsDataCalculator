using ClaimsReserveCalculator.ClaimsDataDomainEntities;
using ClaimsReserveCalculator.ClaimsDataIO;
using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using System.IO;

namespace ClaimsDataTestProject
{
    public class ProductClaimsDataFileWriterTests
    {
        [Fact]
        public void WriteProductClaimsOutputData_NullProductsClaimsData_ThrowsArgumentException()
        {
            ProductsClaimsDataFileWriter dataWriter = new ProductsClaimsDataFileWriter();
            string outputDestination = "test.txt";

            Assert.Throws<ArgumentException>(
                () => dataWriter.WriteProductClaimsOutputData(null, outputDestination));
        }

        [Fact]
        public void WriteProductClaimsOutputData_NullOutputDestination_ThrowsArgumentException()
        {
            ProductsClaimsDataFileWriter dataWriter = new ProductsClaimsDataFileWriter();
            var mockClaimsData = new Mock<ProductsClaimsData>();
            string outputDestination = null;

            Assert.Throws<ArgumentException>(
                () => dataWriter.WriteProductClaimsOutputData(mockClaimsData.Object, outputDestination));
        }

        [Fact]
        public void WriteProductClaimsOutputData_TestStringOutputDestination_WritesToFile()
        {
            ProductsClaimsDataFileWriter dataWriter = new ProductsClaimsDataFileWriter();
            var mockClaimsData = new Mock<ProductsClaimsData>();
            string outputDestination = "test.txt";

            dataWriter.WriteProductClaimsOutputData(mockClaimsData.Object, outputDestination);

            Assert.True(File.Exists(outputDestination));
        }
    }
}
