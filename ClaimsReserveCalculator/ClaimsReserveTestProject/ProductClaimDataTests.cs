using ClaimsReserveCalculator.ClaimsDataDomainEntities;
using System;
using System.Linq;
using Xunit;

namespace ClaimsReserveTestProject
{
    public class ProductClaimDataTests
    {
        [Fact]
        public void Constructor_ValidOriginYearInteger_SetsOriginYearPropertyToValue()
        {
            int originYear = 2010;
            ProductClaimsData devYearClaimsData = new ProductClaimsData(originYear);

            Assert.Equal(originYear, devYearClaimsData.OriginYear);
        }

        [Fact]
        public void Constructor_InvalidOriginYearInteger_ThrowsArgumentException()
        {
            int originYear = 100;

            Assert.Throws<ArgumentException>(() => new ProductClaimsData(originYear));
        }

        [Fact]
        public void UpdateDevelopmentYearClaimsData_DevYearClaimsData_UpdatesDevYearClaimsData()
        {
            int originYear = 2000;
            int developmentYear = 2001;
            double incrementalValue = 1.2;
            double cumulativeValue = 3.2;
            
            DevelopmentYearClaimsData devYearClaimsData = new DevelopmentYearClaimsData(developmentYear, incrementalValue, cumulativeValue);
            ProductClaimsData productClaimsData = new ProductClaimsData(originYear);

            productClaimsData.UpdateDevelopmentYearClaimsData(devYearClaimsData);
            var devYearsData = productClaimsData.DevelopmentYearsClaimsData;

            Assert.Equal(devYearClaimsData, devYearsData.First());
        }

        [Theory]
        [InlineData(1980, 1981, 1.2, 0.5, 1983, 2.2, 1.5)]
        [InlineData(2000, 2001, 0.3, 0, 2005, 3.5, 0)]
        [InlineData(2001, 2001, 1.2, 0.5, 2007, 2.2, 1.5)]
        [InlineData(2001, 2010, 1.2, 0.5, 2003, 3.7, 0)]
        public void CalculateCumulativeClaimsValue_DevYearClaimsDataValues_UpdatesCalculatedCumulativeValueInDevYearClaimsData(
            int originYear, int developmentYear1, double incrementalValue1, double cumulativeValue1, 
            int developmentYear2, double incrementalValue2, double cumulativeValue2)
        {
            DevelopmentYearClaimsData devYearClaimsData1 = new DevelopmentYearClaimsData(
                developmentYear1, incrementalValue1, cumulativeValue1);
            DevelopmentYearClaimsData devYearClaimsData2 = new DevelopmentYearClaimsData(
                developmentYear2, incrementalValue2, cumulativeValue2);
            ProductClaimsData productClaimsData = new ProductClaimsData(originYear);

            productClaimsData.UpdateDevelopmentYearClaimsData(devYearClaimsData1);
            productClaimsData.UpdateDevelopmentYearClaimsData(devYearClaimsData2);
            double calculatedCumulativeValue = productClaimsData.CalculateCumulativeClaimsValue();

            int precision = 2;
            double expectedCumulativeValue = incrementalValue1 + incrementalValue2; 
            Assert.Equal(expectedCumulativeValue, calculatedCumulativeValue, precision);
        }
    }
}
