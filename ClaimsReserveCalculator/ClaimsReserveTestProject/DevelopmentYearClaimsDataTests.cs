using ClaimsReserveCalculator.ClaimsDataDomainEntities;
using System;
using System.Collections.Generic;
using Xunit;

namespace ClaimsReserveTestProject
{
    public class DevelopmentYearClaimsDataTests
    {
        [Fact]
        public void Constructor_DevYearInteger_UpdatesDevelopmentYearProperty()
        {
            int devYear = 1990;
            double incrementalValue = 0;
            double cumulativeValue = 0;
            
            DevelopmentYearClaimsData devYearClaimsData = new DevelopmentYearClaimsData(devYear, incrementalValue, cumulativeValue);

            Assert.Equal(devYear, devYearClaimsData.DevelopmentYear);
        }

        [Fact]
        public void Constructor_IncrementalYearFloat_UpdatesIncrementalValueProperty()
        {
            int devYear = 1990;
            double incrementalValue = 10.1;
            double cumulativeValue = 20.2;

            DevelopmentYearClaimsData devYearClaimsData = new DevelopmentYearClaimsData(devYear, incrementalValue, cumulativeValue);

            int precision = 2;
            Assert.Equal(incrementalValue, devYearClaimsData.IncrementalValue, precision);
        }

        [Fact]
        public void Constructor_CumulativeValueFloat_UpdatesCumulativeValueProperty()
        {
            int devYear = 1990;
            double incrementalValue = 10.1;
            double cumulativeValue = 20.2;

            DevelopmentYearClaimsData devYearClaimsData = new DevelopmentYearClaimsData(devYear, incrementalValue, cumulativeValue);

            int precision = 2;
            Assert.Equal(cumulativeValue, devYearClaimsData.CumulativeValue, precision);
        }
    }
}
