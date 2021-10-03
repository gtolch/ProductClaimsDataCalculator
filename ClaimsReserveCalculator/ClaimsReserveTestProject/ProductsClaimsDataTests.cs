using ClaimsReserveCalculator.ClaimsDataDomainEntities;
using Xunit;

namespace ClaimsReserveTestProject
{
    public class ProductsClaimsDataTests
    {
        [Fact]
        public void Constructor_NoParameters_SetsOriginYearPropertyToDefaultValue()
        {
            ProductsClaimsData devYearClaimsData = new ProductsClaimsData();

            Assert.Equal(ClaimsDataConstants.YEAR_NOT_SET_VALUE, devYearClaimsData.EarliestOriginYear);
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
        public void EraseData_ExistingProductName_DeletesAllClaimsData()
        {
            ProductsClaimsData productsClaimsData = new ProductsClaimsData();
            int originYear = 1990;
            ProductClaimsData claimsData = new ProductClaimsData(originYear);
            string productName = "Comp1";

            productsClaimsData.UpdateProductClaimsData(productName, claimsData);
            productsClaimsData.EraseData();
            var claimsDataCollection = productsClaimsData.GetAllClaimsDataForProduct(productName);

            Assert.Null(claimsDataCollection);
        }
    }
}
