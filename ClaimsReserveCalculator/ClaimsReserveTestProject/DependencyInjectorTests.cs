using ClaimsReserveCalculator.ClaimsDataParser;
using ClaimsReserveCalculator.FrameworksAndDrivers;
using ClaimsReserveCalculator.FrameworksAndDriversInterfaces;
using Xunit;

namespace ClaimsReserveTestProject
{
    public class DependencyInjectorTests
    { 

        [Fact]
        public void ResolveType_ClaimsParserTypeWithNoParams_ReturnsObjectAssociatedWithType()
        {
            IDependencyInjector dependencyInjector = DependencyInjector.Instance;

            dependencyInjector.RegisterInterfaces();
            var claimsParser = dependencyInjector.Resolve<ClaimsDataCategoryParser>();
            
            Assert.NotNull(claimsParser);
        }


        [Fact]
        public void Resolve_ClaimsParserTypeWithNoParams_ReturnsObjectAssociatedWithType()
        {
            IDependencyInjector dependencyInjector = DependencyInjector.Instance;
            ClaimsDataCategoryParser claimsParser = new ClaimsDataCategoryParser();

            dependencyInjector.RegisterInterfaces();
            var resolvedClaimsParser = dependencyInjector.Resolve(claimsParser.GetType());

            Assert.NotNull(resolvedClaimsParser);
        }
    }
}
