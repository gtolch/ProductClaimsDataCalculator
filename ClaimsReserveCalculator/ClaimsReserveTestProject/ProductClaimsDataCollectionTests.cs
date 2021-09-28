using ClaimsReserveCalculator.ClaimsDataDomainEntities;
using System;
using Xunit;
using Moq;
using System.Linq;

namespace ClaimsReserveTestProject
{
    public class ProductClaimsDataCollectionTests
    {
        [Fact]
        public void Constructor_ValidProductClaimsData_AddsClaimsDatatoCollection()
        {
            int originYear = 2000;
            var productClaimsData = new ProductClaimsData(originYear);

            ProductClaimsDataCollection claimsDataCollection = new ProductClaimsDataCollection(productClaimsData);

            Assert.Equal(productClaimsData, claimsDataCollection.ProductClaimsDataItems.First());
        }

        [Fact]
        public void Add_MultipleProductClaimsData_AddsClaimsDatatoCollection()
        {
            int originYear1 = 2000;
            int originYear2 = 2002;
            var productClaimsData1 = new ProductClaimsData(originYear1);
            var productClaimsData2 = new ProductClaimsData(originYear2);
            ProductClaimsDataCollection claimsDataCollection = new ProductClaimsDataCollection();

            claimsDataCollection.Add(productClaimsData1);
            claimsDataCollection.Add(productClaimsData2);

            int claimsDataItemCount = claimsDataCollection.ProductClaimsDataItems.Count;
            Assert.Equal(2, claimsDataItemCount);
        }

        [Fact]
        public void GetProductClaimsForOriginYear_MultipleProductClaimsData_AddsClaimsDatatoCollection()
        {
            int originYear = 2000;
            ProductClaimsData productClaimsData = new ProductClaimsData(originYear);

            ProductClaimsDataCollection claimsDataCollection = new ProductClaimsDataCollection();
            claimsDataCollection.Add(productClaimsData);
            var claimsDataForOriginYear = claimsDataCollection.GetProductClaimsForOriginYear(originYear);

            Assert.Equal(claimsDataForOriginYear, productClaimsData);
        }
    }
}
