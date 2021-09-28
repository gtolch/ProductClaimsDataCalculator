using System;

namespace ClaimsReserveCalculator.FrameworksAndDrivers.DataParserUserInterface
{
    /// <summary>
    /// The interface to the data parser view model. 
    /// May be used for dependency injection purposes.
    /// </summary>
    public interface IDataParserViewModel
    {
        string InputSourceFileName { get; set; }
    }
}
