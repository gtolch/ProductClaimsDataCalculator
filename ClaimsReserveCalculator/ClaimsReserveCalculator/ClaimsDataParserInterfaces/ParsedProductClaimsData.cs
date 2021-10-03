
namespace ClaimsReserveCalculator.ClaimsDataParserInterfaces
{
    /// <summary>
    /// Represents the parsed claims data for a particular product.
    /// </summary>
    public class ParsedProductClaimsData
    {
        public string ProductName { get; private set; }
        public int OriginYear { get; private set; }
        public int DevelopmentYear { get; private set; }
        public double IncrementalValue { get; private set; }
        public int WarningCount { get; private set; }

        /// <summary>
        /// Parameterized constructor that initialises the parsed product claims data .
        /// </summary>
        /// <param name="productName">Name of the product.</param>
        /// <param name="originYear">The origin year associated with the data.</param>
        /// <param name="developmentYear">The development year associated with the data.</param>
        /// <param name="incrementalValue">The incremental claims value.</param>
        /// <param name="warningCount">The number of warnings that occurred during the parsing of this product.</param>
        public ParsedProductClaimsData(string productName, int originYear, int developmentYear, double incrementalValue, int warningCount)
        {
            ProductName = productName;
            OriginYear = originYear;
            DevelopmentYear = developmentYear;
            IncrementalValue = incrementalValue;
            WarningCount = warningCount;
        }
    }
}
