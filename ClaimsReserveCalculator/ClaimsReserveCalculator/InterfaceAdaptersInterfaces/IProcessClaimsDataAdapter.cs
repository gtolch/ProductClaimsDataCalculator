using System;
using System.Collections.Generic;

namespace ClaimsReserveCalculator.InterfaceAdaptersInterfaces
{
    /// <summary>
    /// The interface to the adapter for claims data processing.
    /// It decouples the 'frameworks and drivers' from 'use case interactors'
    /// </summary>
    public interface IProcessClaimsDataAdapter
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
