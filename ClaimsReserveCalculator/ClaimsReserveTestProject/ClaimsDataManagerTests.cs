using ClaimsReserveCalculator.ClaimsDataDomainEntities;
using ClaimsReserveCalculator.ClaimsDataProcessing;
using Moq;
using Xunit;

namespace ClaimsReserveTestProject
{
    public class ClaimsDataManagerTests
    {
        [Fact]
        public void EarliestOriginYearProperty_NoParams_CallsMethodToReturnEarliestOriginYear()
        {
            Mock<IProductsClaimsData> mockProductsClaimsData = new Mock<IProductsClaimsData>();
            var claimsDataManager = new ClaimsDataManager(mockProductsClaimsData.Object);

            int year = claimsDataManager.EarliestOriginYear;

            mockProductsClaimsData.Verify(mock => mock.EarliestOriginYear, Times.Once);
        }

        [Fact]
        public void ProductNamesProperty_NoParams_CallsMethodToReturnProductNames()
        {
            Mock<IProductsClaimsData> mockProductsClaimsData = new Mock<IProductsClaimsData>();
            var claimsDataManager = new ClaimsDataManager(mockProductsClaimsData.Object);

            var productNames = claimsDataManager.ProductNames;

            mockProductsClaimsData.Verify(mock => mock.ProductNames, Times.Once);
        }

        [Fact]
        public void EraseClaimsData_NoParams_CallsMethodToEraseAllClaimsData()
        {
            Mock<IProductsClaimsData> mockProductsClaimsData = new Mock<IProductsClaimsData>();
            var claimsDataManager = new ClaimsDataManager(mockProductsClaimsData.Object);

            claimsDataManager.EraseClaimsData();

            mockProductsClaimsData.Verify(mock => mock.EraseData(), Times.Once);
        }

        [Fact]
        public void SetupDevelopmentYearsClaimsData_ProductName_ReturnsDevelopmentYearsThatWereAdded()
        {
            Mock<IProductsClaimsData> mockProductsClaimsData = new Mock<IProductsClaimsData>();
            string productName = "Comp1";
            int originYear = 1995;
            int developmentYear = 1997;
            double incrementalValue = 12.4;
            var claimsDataManager = new ClaimsDataManager(mockProductsClaimsData.Object);

            var productClaimsData = claimsDataManager.SetupProductClaimsData(
                productName, originYear, developmentYear, incrementalValue);

            Assert.NotNull(productClaimsData);
        }

        [Fact]
        public void GetAllDevelopmentYearsClaimsData_ProductName_ReturnsDevelopmentYearsThatWereAdded()
        {
            Mock<IProductsClaimsData> mockProductsClaimsData = new Mock<IProductsClaimsData>();
            string productName = "Comp1";
            int originYear = 1995;
            var productClaimsData = new ProductClaimsData(originYear);
            var productClaimsCollection = new ProductClaimsDataCollection(productClaimsData);
            mockProductsClaimsData.Setup(
                mock => mock.GetAllClaimsDataForProduct(productName)).Returns(productClaimsCollection);
            
            int developmentYear = 1997;
            double incrementalValue = 12.4;
            double cumulativeValue = 0;
            productClaimsData.UpdateDevelopmentYearClaimsData(
                new DevelopmentYearClaimsData(developmentYear, incrementalValue, cumulativeValue)); 

            var claimsDataProcessor = new ClaimsDataManager(mockProductsClaimsData.Object);
            var devYearsData = claimsDataProcessor.GetAllDevelopmentYearsClaimsData(productName);

            Assert.Single(devYearsData);
        }

        [Fact]
        public void AddMissingYearsData_NoParams_CallsMethodToAddMissingYearsData()
        {
            Mock<IProductsClaimsData> mockProductsClaimsData = new Mock<IProductsClaimsData>();
            var claimsDataManager = new ClaimsDataManager(mockProductsClaimsData.Object);
            int maxDevelopmentYear = 1;

            claimsDataManager.AddMissingYearsData(maxDevelopmentYear);

            mockProductsClaimsData.Verify(mock => mock.AddMissingYearsData(maxDevelopmentYear), Times.Once);
        }
    }
}
