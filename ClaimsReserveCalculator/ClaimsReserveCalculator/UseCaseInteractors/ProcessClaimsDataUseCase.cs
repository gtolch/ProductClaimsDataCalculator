using ClaimsReserveCalculator.ClaimsDataDomainEntities;
using ClaimsReserveCalculator.ClaimsDataInputAndOutputInterfaces;
using ClaimsReserveCalculator.ClaimsDataParserInterfaces;
using ClaimsReserveCalculator.ClaimsDataProcessing;
using ClaimsReserveCalculator.CustomExceptions;
using ClaimsReserveCalculator.Properties;
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
        private readonly IClaimsDataManager _claimsDataManager;
        private readonly IDevelopmentYearSet _developmentYearsSet;

        /// <summary>
        /// Parameterized constructor to initialise an instance.
        /// </summary>
        /// <param name="inputReader">The product claims data input reader.</param>
        /// <param name="dataWriter">The product claims data writer.</param>
        /// <param name="dataParser">The product claims data parser.</param>
        /// <param name="dataManager">The products claims data manager.</param>
        /// <param name="devYearsSet">The set of distinct development years.</param>
        public ProcessClaimsDataUseCase(IProductsClaimsDataReader inputReader, IProductsClaimsDataWriter dataWriter, 
            IProductsClaimsDataParser dataParser, IClaimsDataManager dataManager, IDevelopmentYearSet devYearsSet)
        {
            _inputReader = inputReader;
            _dataWriter = dataWriter;
            _dataParser = dataParser;
            _claimsDataManager = dataManager;
            _developmentYearsSet = devYearsSet;
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
                    var parsedProductsData = _dataParser.ParseProductsClaimsData(rawInputData);

                    if (parsedProductsData != null)
                    {
                        // delete any old data that may be stored from a previous run.
                        _claimsDataManager.EraseClaimsData();
                        _developmentYearsSet.Clear();

                        foreach (var productData in parsedProductsData)
                        {
                            var productClaimsData = _claimsDataManager.SetupProductClaimsData(productData.ProductName, 
                                productData.OriginYear, productData.DevelopmentYear, productData.IncrementalValue);

                            _developmentYearsSet.AddDevelopmentYear(productData.DevelopmentYear);
                        }

                        // Fill in any gaps in the claims data in terms of entries for different years across products.
                        // It helps ensure that cumulative data output will be in a consistent format between products.
                        _claimsDataManager.AddMissingYearsData(_developmentYearsSet.Max());

                        // Save the cumulative product claims data to file.
                        _dataWriter.WriteProductClaimsOutputData(outputFilePath, _developmentYearsSet.Count);
                    }
                }
                catch (ClaimsDataFileReadException)
                {
                    // Rethrow for other layers to handle. Placeholder section - could log or attempt to retry failed operation
                    throw;
                }
                catch (InvalidClaimsDataCategoriesException)
                {
                    // Rethrow for other layers to handle. Placeholder section - could log or attempt to retry failed operation
                    throw;
                }
                catch (ParseClaimsInputDataException)
                {
                    // Rethrow for other layers to handle. Placeholder section - could log or attempt to retry failed operation.
                    throw;
                }
                catch (WriteProductsClaimsDataException)
                {
                    // Rethrow for other layers to handle. Placeholder section - could log or attempt to retry failed operation.
                    throw;
                }
                catch (InvalidInputSourceFileException)
                {
                    // Rethrow for other layers to handle. Placeholder section - could log or attempt to retry failed operation.
                    throw;
                }
                catch (Exception e)
                {
                    throw new ErrorSavingCumulativeClaimsException(Resources.ErrorSavingCumulativeClaimsData, e);
                }
            }
            else
            {
                throw new InvalidInputSourceFileException(Resources.DidNotSaveDataDueToInvalidSource);
            }
        }
    }
}
