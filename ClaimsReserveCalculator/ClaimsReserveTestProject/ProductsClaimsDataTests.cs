using ClaimsReserveCalculator.ClaimsDataDomainEntities;
using System;
using System.Linq;
using Xunit;

namespace ClaimsReserveTestProject
{
    public class ProductsClaimsDataTests
    {
        [Fact]
        public void Constructor_NoParameters_SetsOriginYearPropertyToDefaultValue()
        {
            ProductsClaimsData devYearClaimsData = new ProductsClaimsData();

            Assert.Equal(ProductsClaimsData.ORIGIN_YEAR_NOT_SET, devYearClaimsData.EarliestOriginYear);
        }

        [Fact]
        public void UpdateProductClaimsData_ValidParameters_UpdatesEarliestOriginYear()
        {
            ProductsClaimsData productsClaimsData = new ProductsClaimsData();
            string productName = "Comp1";
            int originYear = 1996;
            int developmentYear = 1998;
            double incrementalValue = 6.4;
            double cumulativeValue = 0;
            ProductClaimsData productClaimsData = new ProductClaimsData(originYear);
            productClaimsData.UpdateDevelopmentYearClaimsData(
                new DevelopmentYearClaimsData(developmentYear, incrementalValue, cumulativeValue));

            productsClaimsData.UpdateProductClaimsData(productName, productClaimsData);

            Assert.Equal(originYear, productsClaimsData.EarliestOriginYear);
        }

        [Fact]
        public void GetAllDevelopmentYearsClaimsData_ProductName_ReturnsDevelopmentYearsThatWereAdded()
        {
            ProductsClaimsData productsClaimsData = new ProductsClaimsData();
            string productName = "Comp1";
            int originYear = 1995;
            int developmentYear = 1997;
            double incrementalValue = 12.4;
            double cumulativeValue = 0;
            ProductClaimsData productClaimsData = new ProductClaimsData(originYear);
            productClaimsData.UpdateDevelopmentYearClaimsData(
                new DevelopmentYearClaimsData(developmentYear, incrementalValue, cumulativeValue));

            productsClaimsData.UpdateProductClaimsData(productName, productClaimsData);
            var devYearsData = productsClaimsData.GetAllDevelopmentYearsClaimsData(productName);

            Assert.Single(devYearsData);
        }

        
    }
}
