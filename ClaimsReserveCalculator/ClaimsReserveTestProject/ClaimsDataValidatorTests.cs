using ClaimsReserveCalculator.ClaimsDataDomainEntities;
using Xunit;

namespace ClaimsReserveTestProject
{
    public class ClaimsDataValidatorTests
    {
        [Theory]
        [InlineData(ClaimsDataConstants.YEAR_NOT_SET_VALUE, 1990)]
        [InlineData(ClaimsDataConstants.YEAR_NOT_SET_VALUE, ClaimsDataConstants.YEAR_NOT_SET_VALUE)]
        [InlineData(2000, ClaimsDataConstants.YEAR_NOT_SET_VALUE)]
        public void AreClaimsYearsValid_YearNotSetValue_ReturnsFalse(int year1, int year2)
        {
            bool isClaimsYearValid;
            
            isClaimsYearValid = ClaimsDataValidator.AreClaimsYearsValid(year1, year2);

            Assert.False(isClaimsYearValid);
        }

        [Fact]
        public void AreClaimsYearsValid_ValidClaimsYears_ReturnsTrue()
        {
            int year1 = 2000;
            int year2 = 1995;

            bool areClaimsYearsValid = ClaimsDataValidator.AreClaimsYearsValid(year1, year2);

            Assert.True(areClaimsYearsValid);
        }

        [Fact]
        public void IsClaimsYearValid_YearNotSetValue_ReturnsFalse()
        {
            int year = ClaimsDataConstants.YEAR_NOT_SET_VALUE;

            bool isClaimsYearValid = ClaimsDataValidator.IsClaimsYearValid(year);

            Assert.False(isClaimsYearValid);
        }

        [Fact]
        public void IsClaimsYearValid_ValidClaimsYear_ReturnsTrue()
        {
            int year = 2000;

            bool isClaimsYearValid = ClaimsDataValidator.IsClaimsYearValid(year);

            Assert.True(isClaimsYearValid);
        }
    }
}
