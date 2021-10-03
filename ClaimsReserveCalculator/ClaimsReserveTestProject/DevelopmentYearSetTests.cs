using ClaimsReserveCalculator.ClaimsDataDomainEntities;
using System;
using Xunit;

namespace ClaimsReserveTestProject
{
    public class DevelopmentYearSetTests
    {
        [Fact]
        public void Max_NoParams_ReturnsDefaultUnsetValueWhenNoDevYearClaimsDataAdded()
        {
            DevelopmentYearSet developmentYearSet = new DevelopmentYearSet();

            int maxDevelopmentYear = developmentYearSet.Max();

            Assert.Equal(-1, maxDevelopmentYear);
        }

        [Fact]
        public void Max_NoParams_ReturnsMaxDevelopmentYearValueWhenDevYearClaimsAdded()
        {
            DevelopmentYearSet developmentYearSet = new DevelopmentYearSet();
            int devYear = 1990;

            developmentYearSet.AddDevelopmentYear(devYear);
            int maxDevelopmentYear = developmentYearSet.Max();

            Assert.Equal(devYear, maxDevelopmentYear);
        }

        [Fact]
        public void AddDevelopmentYear_ValidDevelopmentYearParam_UpdatesDevelopmentYearSet()
        {
            DevelopmentYearSet developmentYearSet = new DevelopmentYearSet();
            int developmentYear = 1990;

            bool added = developmentYearSet.AddDevelopmentYear(developmentYear);

            Assert.True(added && (1 == developmentYearSet.Count));
        }

        [Fact]
        public void AddDevelopmentYear_DuplicateDevelopmentYearParam_FailsToUpdateDevelopmentYearSet()
        {
            DevelopmentYearSet developmentYearSet = new DevelopmentYearSet();
            int developmentYear = 1990;
            
            bool added1st = developmentYearSet.AddDevelopmentYear(developmentYear);
            bool added2nd = developmentYearSet.AddDevelopmentYear(developmentYear);

            Assert.True(added1st && !added2nd && (1 == developmentYearSet.Count));
        }

        [Fact]
        public void Clear_NoParams_DeletesAnyDevelopmentYearsData()
        {
            DevelopmentYearSet developmentYearSet = new DevelopmentYearSet();
            int developmentYear = 1990;

            developmentYearSet.AddDevelopmentYear(developmentYear);
            developmentYearSet.Clear();
            
            Assert.Equal(0, developmentYearSet.Count);
        }
    }
}
