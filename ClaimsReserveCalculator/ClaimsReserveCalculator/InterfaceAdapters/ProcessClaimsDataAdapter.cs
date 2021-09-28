using ClaimsReserveCalculator.CustomExceptions;
using ClaimsReserveCalculator.InterfaceAdaptersInterfaces;
using ClaimsReserveCalculator.UseCaseInterfaces;
using System;

namespace ClaimsReserveCalculator.InterfaceAdaptors
{
    /// <summary>
    /// The adapter for claims data processing.
    /// </summary>
    public class ProcessClaimsDataAdapter : IProcessClaimsDataAdapter
    {
        private IProcessClaimsDataUseCase _processClaimsDataUseCase;

        public ProcessClaimsDataAdapter(IProcessClaimsDataUseCase processClaimsDataUseCase)
        {
            _processClaimsDataUseCase = processClaimsDataUseCase;
        }

        /// <summary>
        /// Saves the cumulative claims data that is derived from 
        /// incremental claims data retrieved from an input source.
        /// </summary>
        /// <param name="sourceClaimsDataFileName">Input source file name and path.</param>
        /// <param name="outputFilePath">The output destination file name and path.</param>
        public void SaveCumulativeClaimsData(string sourceClaimsDataFileName, string outputFilePath)
        {
            _processClaimsDataUseCase.SaveCumulativeClaimsData(sourceClaimsDataFileName, outputFilePath);
        }
    }
}
