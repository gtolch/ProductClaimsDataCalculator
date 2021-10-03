using ClaimsReserveCalculator.FrameworksAndDrivers.DataParserUserInterface;
using ClaimsReserveCalculator.InterfaceAdaptersInterfaces;
using Moq;
using Xunit;

namespace ClaimsReserveTestProject
{
    public class DataParserViewModelTests
    {
        [Fact]
        public void SaveCumulativeClaimsData_ValidFormatFilePathStrings_CallsSaveCumulativeClaimsData()
        {
            var mockAdapter = new Mock<IProcessClaimsDataAdapter>();
            DataParserViewModel viewModel = new DataParserViewModel(
                mockAdapter.Object);

            viewModel.ParseData(this);

            mockAdapter.Verify(mock => mock.SaveCumulativeClaimsData(
                It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
