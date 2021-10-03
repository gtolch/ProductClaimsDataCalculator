using ClaimsReserveCalculator.ClaimsDataInputAndOutputInterfaces;
using ClaimsReserveCalculator.ClaimsDataProcessing;
using ClaimsReserveCalculator.CustomExceptions;
using ClaimsReserveCalculator.Properties;
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
        private readonly IClaimsDataManager _claimsDataManager;
        private readonly string _separatorWithExtraSpace;

        /// <summary>
        /// Constructor which sets up the writer and the data field separator.
        /// </summary>
        /// <param name="claimsManager">The processor/manager of claims data</param>
        /// <param name="outputItemSeparator">The delimiter used to separate data values.</param>
        public ProductsClaimsDataFileWriter(IClaimsDataManager claimsManager, char outputItemSeparator = ',')
        {
            if (char.IsLetterOrDigit(outputItemSeparator))
            {
                throw new ArgumentException(
                    Resources.CantCreateWriterAsSeparatorIsAlphanumeric);
            }

            _claimsDataManager = claimsManager;
            _separatorWithExtraSpace = $"{outputItemSeparator} ";
        }

        /// <summary>
        /// Saves the claims data for a group of products.
        /// Any existing destination resource (with the same name) should be overwritten.
        /// </summary>
        /// <param name="outputDestination">Fully qualified filename with path / URL.</param>
        /// <param name="totalDevelopmentYears">Total development years in the claims data.</param>
        public void WriteProductClaimsOutputData(string outputDestination, int totalDevelopmentYears)
        {
            if (string.IsNullOrWhiteSpace(outputDestination))
            {
                throw new ArgumentException(Resources.DidntWriteProductClaimsDataToFile);
            }

            try
            {
                using (StreamWriter streamWriter = new StreamWriter(outputDestination))
                {
                    streamWriter.Write(_claimsDataManager.EarliestOriginYear);
                    streamWriter.Write(_separatorWithExtraSpace);
                    streamWriter.Write(totalDevelopmentYears);
                    streamWriter.Write(Environment.NewLine);

                    // Output cumulative claims data to file for each of the products.
                    foreach (var productName in _claimsDataManager.ProductNames)
                    {
                        streamWriter.Write(productName);

                        foreach (var claimsData in 
                            _claimsDataManager.GetAllDevelopmentYearsClaimsData(productName))
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
                throw new WriteProductsClaimsDataException(Resources.ErrorWritingClaimsDataToFile, ex);
            }
        }
    }
}
