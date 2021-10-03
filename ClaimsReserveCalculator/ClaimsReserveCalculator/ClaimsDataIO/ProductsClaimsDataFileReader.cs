using ClaimsReserveCalculator.ClaimsDataInputAndOutputInterfaces;
using ClaimsReserveCalculator.CustomExceptions;
using ClaimsReserveCalculator.Properties;
using System;
using System.Collections.Generic;
using System.IO;

namespace ClaimsReserveCalculator.ClaimsDataIO
{
    /// <summary>
    /// Responsible for reading products claims reserve data from file sources.
    /// </summary>
    public class ProductsClaimsDataFileReader : IProductsClaimsDataReader
    {
        /// <summary>
        /// Checks whether the supplied input source is valid,
        /// e.g whether a file exists and whether the user has permission to access it.
        /// </summary>
        /// <param name="inputSource"></param>
        /// <returns>Returns true if input source is valid, false otherwise.</returns>
        public bool IsInputSourceValid(string inputSource) => File.Exists(inputSource);

        /// <summary>
        /// Attempts to read input data from the input source specified in the parameter. 
        /// </summary>
        /// <param name="inputSource">The fully qualified input source file</param>
        /// <returns>The claims data for the collection of products or null.</returns>
        public IEnumerable<string> ReadRawInputData(string inputSource)
        {
            string[] rawInputData = null;
            if (string.IsNullOrWhiteSpace(inputSource))
            {
                throw new ArgumentException(Resources.DidntReadFileInputData);
            }

            if (!IsInputSourceValid(inputSource))
            {
                throw new InvalidInputSourceFileException(Resources.CantAccessRawInputDataFile);
            }

            try
            {
                rawInputData = File.ReadAllLines(inputSource);
            }
            catch (Exception ex)
            {
                throw new ClaimsDataFileReadException(Resources.ErrorReadingClaimsDataFile, ex);
            }

            return rawInputData;
        }
    }
}
