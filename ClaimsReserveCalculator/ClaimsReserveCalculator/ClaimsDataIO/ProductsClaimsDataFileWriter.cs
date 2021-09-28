using ClaimsReserveCalculator.ClaimsDataDomainEntities;
using ClaimsReserveCalculator.ClaimsDataInputAndOutputInterfaces;
using ClaimsReserveCalculator.CustomExceptions;
using System;
using System.IO;

namespace ClaimsReserveCalculator.ClaimsDataIO
{
    /// <summary>
    /// Represents a file writer for saving products claims reserve data
    /// including cumulative claims data.
    /// </summary>
    public class ProductsClaimsDataFileWriter : IProductsClaimsDataWriter
    {
        private readonly string _separatorWithExtraSpace;

        /// <summary>
        /// Constructor which sets up the writer and the data field separator.
        /// </summary>
        /// <param name="outputItemSeparator">The delimiter used to separate data values.</param>
        public ProductsClaimsDataFileWriter(char outputItemSeparator = ',')
        {
            if (char.IsLetterOrDigit(outputItemSeparator))
            {
                throw new ArgumentException(
                    "Can't create data file writer - item separator is alphanumeric value.");
            }

            _separatorWithExtraSpace = $"{outputItemSeparator} ";
        }

        /// <summary>
        /// Saves the supplied claims data for a group of products.
        /// Any existing destination resource (with the same name) should be overwritten.
        /// </summary>
        /// <param name="productsClaimsData">The claims data for a collection of products.</param>
        /// <param name="outputDestination">Fully qualified filename with path / URL.</param>
        public void WriteProductClaimsOutputData(ProductsClaimsData productsClaimsData, string outputDestination)
        {
            if (productsClaimsData == null || string.IsNullOrWhiteSpace(outputDestination))
            {
                throw new ArgumentException("Didn't write product claims data - parameters are invalid");
            }

            try
            {
                int originYear = productsClaimsData.EarliestOriginYear;

                using (StreamWriter streamWriter = new StreamWriter(outputDestination))
                {
                    streamWriter.Write(originYear);
                    streamWriter.Write(_separatorWithExtraSpace);
                    streamWriter.Write(productsClaimsData.TotalDevelopmentYears);
                    streamWriter.Write(Environment.NewLine);

                    // Output cumulative claims data to file for each of the products.
                    foreach (var productName in productsClaimsData.ProductNames)
                    {
                        streamWriter.Write(productName);

                        foreach (var claimsData in productsClaimsData.GetAllDevelopmentYearsClaimsData(productName))
                        {
                            streamWriter.Write(_separatorWithExtraSpace);
                            streamWriter.Write(claimsData.CumulativeValue);
                        }

                        streamWriter.Write(Environment.NewLine);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new WriteProductsClaimsDataException(
                    $"Error occurred in writing claims data to file {outputDestination}: {ex}", ex);
            }
        }
    }
}
