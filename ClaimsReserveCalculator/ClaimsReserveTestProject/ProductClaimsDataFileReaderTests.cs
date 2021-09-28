using ClaimsReserveCalculator.ClaimsDataIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace ClaimsReserveTestProject
{
    public class ProductClaimsDataFileReaderTests
    {
        [Fact]
        public void IsInputSourceValid_EmptyStringInputFile_ReturnsFalse()
        {
            ProductsClaimsDataFileReader dataReader = new ProductsClaimsDataFileReader();
            string filePath = string.Empty;

            bool isFileNameValid = dataReader.IsInputSourceValid(filePath);

            Assert.False(isFileNameValid);
        }

        [Fact]
        public void IsInputSourceValid_NonExistentFilePathString_ReturnsFalse()
        {
            ProductsClaimsDataFileReader dataReader = new ProductsClaimsDataFileReader();
            string filePath = "ZZZZZZZ99999999.txt";

            bool isFileNameValid = dataReader.IsInputSourceValid(filePath);

            Assert.False(isFileNameValid);
        }

        [Fact]
        public void IsInputSourceValid_ValidFilePathString_ReturnsTrue()
        {
            ProductsClaimsDataFileReader dataReader = new ProductsClaimsDataFileReader();
            string filePath = "test_inputsource.txt";
            File.WriteAllText(filePath, "123");

            bool isFileNameValid = dataReader.IsInputSourceValid(filePath);

            Assert.True(isFileNameValid);
        }

        [Fact]
        public void WriteProductClaimsOutputData_EmptyStringInput_ThrowsArgumentException()
        {
            ProductsClaimsDataFileReader dataReader = new ProductsClaimsDataFileReader();
            string inputSource = string.Empty;

            Assert.Throws<ArgumentException>(
                () => dataReader.ReadRawInputData(inputSource));
        }

        [Fact]
        public void WriteProductClaimsOutputData_MultipleStringsInput_ReadsInputDataFromFile()
        {
            ProductsClaimsDataFileReader dataReader = new ProductsClaimsDataFileReader();
            string filePath = "test_write.txt";
            string[] contents = { "Product1, ", "Product2, " };
            File.WriteAllLines(filePath, contents);

            var inputData = dataReader.ReadRawInputData(filePath);

            Assert.True(inputData != null && inputData.Any());
        }
    }
}
