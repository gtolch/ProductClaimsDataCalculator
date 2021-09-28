
namespace ClaimsReserveCalculator.UseCaseInterfaces
{
    /// <summary>
    /// Interface to the use-case interactor for processing claims data.
    /// </summary>
    public interface IProcessClaimsDataUseCase
    {
        /// <summary>
        /// Saves the cumulative claims data that is derived from 
        /// incremental claims data retrieved from an input source.
        /// </summary>
        /// <param name="sourceClaimsDataFileName">Input source file name and path.</param>
        /// <param name="outputFilePath">The output destination file name and path.</param>
        void SaveCumulativeClaimsData(string sourceClaimsDataFileName, string outputFilePath);
    }
}
