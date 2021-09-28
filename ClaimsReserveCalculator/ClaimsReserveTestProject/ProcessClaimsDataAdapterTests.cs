using ClaimsReserveCalculator.InterfaceAdaptors;
using ClaimsReserveCalculator.UseCaseInterfaces;
using Moq;
using System;
using Xunit;

namespace ClaimsReserveTestProject
{
    public class ProcessClaimsDataAdapterTests
    {
        [Fact]
        public void SaveCumulativeClaimsData_ValidFormatFilePathStrings_CallsSaveCumulativeClaimsData()
        {
            string sourceFilePath = "input1.txt";
            string outputFilePath = "save1.txt";
            var mockProcessClaimsUseCase = new Mock<IProcessClaimsDataUseCase>();
            ProcessClaimsDataAdapter productsClaimsDataAdapter = new ProcessClaimsDataAdapter(
                mockProcessClaimsUseCase.Object);

            productsClaimsDataAdapter.SaveCumulativeClaimsData(sourceFilePath, outputFilePath);

            mockProcessClaimsUseCase.Verify(mock => mock.SaveCumulativeClaimsData(
                sourceFilePath, outputFilePath), Times.Once);
        }
    }
}
