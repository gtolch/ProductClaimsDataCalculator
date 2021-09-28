using ClaimsReserveCalculator.ClaimsDataInputAndOutputInterfaces;
using ClaimsReserveCalculator.ClaimsDataParserInterfaces;
using ClaimsReserveCalculator.CustomExceptions;
using ClaimsReserveCalculator.UseCaseInterfaces;
using System;

namespace ClaimsReserveCalculator.UseCaseInteractors
{
    /// <summary>
    /// This 'use-case interactor' undertakes processing of reserve claims 
    /// at a relatively abstract level (e.g. business/control logic). 
    /// </summary>
    public class ProcessClaimsDataUseCase : IProcessClaimsDataUseCase
    {
        private readonly IProductsClaimsDataReader _inputReader;
        private readonly IProductsClaimsDataWriter _dataWriter;
        private readonly IProductsClaimsDataParser _dataParser;

        public ProcessClaimsDataUseCase(IProductsClaimsDataReader inputReader, 
            IProductsClaimsDataWriter dataWriter, IProductsClaimsDataParser dataParser)
        {
            _inputReader = inputReader;
            _dataWriter = dataWriter;
            _dataParser = dataParser;
        }

        /// <summary>
        /// Saves the cumulative claims data that is derived from 
        /// incremental claims data retrieved from an input source.
        /// </summary>
        /// <param name="sourceClaimsDataFileName">Input source file name and path.</param>
        /// <param name="outputFilePath">The output destination file name and path.</param>
        public void SaveCumulativeClaimsData(string sourceClaimsDataFileName, string outputFilePath)
        {
            if (_inputReader.IsInputSourceValid(sourceClaimsDataFileName))
            {
                try
                {
                    // Read the source data from file and parse it into product claims data.
                    var rawInputData = _inputReader.ReadRawInputData(sourceClaimsDataFileName);
                    var productsClaimsData = _dataParser.ParseProductsClaimsInputData(rawInputData);

                    if (productsClaimsData != null)
                    {
                        // Fill in any gaps in the claims data in terms of entries for different years across products.
                        // It helps ensure that cumulative data output will be in a consistent format between products.
                        productsClaimsData.AddMissingYearsData();

                        // Save the cumulative product claims data to file.
                        _dataWriter.WriteProductClaimsOutputData(productsClaimsData, outputFilePath);
                    }
                }
                catch (ClaimsDataFileReadException)
                {
                    // Rethrow for other layers to handle. Could extend this section eg attempt to retry failed operation
                    throw;
                }
                catch (ParseClaimsInputDataException)
                {
                    // Rethrow for other layers to handle. Could extend this section eg attempt to retry failed operation.
                    throw;
                }
                catch (WriteProductsClaimsDataException)
                {
                    // Rethrow for other layers to handle. Could extend this section eg attempt to retry failed operation.
                    throw;
                }
                catch (InvalidInputSourceFileException)
                {
                    // Rethrow for other layers to handle. Could extend this section eg attempt to retry failed operation.
                    throw;
                }
                catch (Exception e)
                {
                    throw new ErrorSavingCumulativeClaimsException(
                        $"Problem in saving cumulative claims data: {e}", e);
                }
            }
            else
            {
                throw new InvalidInputSourceFileException(
                    $"Didn't save cumulative claims data - invalid input source: {sourceClaimsDataFileName}");
            }
        }
    }
}
