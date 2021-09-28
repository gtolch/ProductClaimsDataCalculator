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

        [Fact]
        public void CalculateCumulativeClaimsValue_DevYearClaimsData_UpdatesDevYearClaimsData()
        {
            int originYear = 2000;
            int developmentYear = 2001;
            double incrementalValue = 1.2;
            double cumulativeValue = 0.5;
            DevelopmentYearClaimsData devYearClaimsData1 = new DevelopmentYearClaimsData(
                developmentYear, incrementalValue, cumulativeValue);
            DevelopmentYearClaimsData devYearClaimsData2 = new DevelopmentYearClaimsData(
                developmentYear + 1, incrementalValue + 1, cumulativeValue + 1);
            ProductClaimsData productClaimsData = new ProductClaimsData(originYear);

            productClaimsData.UpdateDevelopmentYearClaimsData(devYearClaimsData1);
            productClaimsData.UpdateDevelopmentYearClaimsData(devYearClaimsData2);
            double latestCumulativeValue = productClaimsData.CalculateCumulativeClaimsValue();

            int precision = 2;
            Assert.Equal(3.4, latestCumulativeValue, precision); // 3.4 == 1.2 + 2.2
        }
    }
}
