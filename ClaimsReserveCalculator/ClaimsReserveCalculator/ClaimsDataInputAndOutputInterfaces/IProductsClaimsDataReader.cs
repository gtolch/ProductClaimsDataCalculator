using System.Collections.Generic;

namespace ClaimsReserveCalculator.ClaimsDataInputAndOutputInterfaces
{
    /// <summary>
    /// The interface to an input reader for reading raw products claims data.
    /// </summary>
    public interface IProductsClaimsDataReader
    {
        /// <summary>
        /// Checks whether the supplied input source is valid,
        /// e.g whether a file exists and whether the user has permission to access it.
        /// </summary>
        /// <param name="inputSource"></param>
        /// <returns>Returns true if input source is valid, false otherwise.</returns>
        bool IsInputSourceValid(string inputSource);

        /// <summary>
        /// Reads raw input data from the specified input source.
        /// </summary>
        /// <param name="inputSource"></param>
        /// <returns>Input source that can be a fully qualified filename with path.</returns>
        IEnumerable<string> ReadRawInputData(string inputSource);
    }
}
