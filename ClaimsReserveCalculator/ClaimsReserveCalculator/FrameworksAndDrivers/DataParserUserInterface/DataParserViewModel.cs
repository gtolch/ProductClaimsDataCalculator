using ClaimsReserveCalculator.CustomExceptions;
using ClaimsReserveCalculator.InterfaceAdaptersInterfaces;
using ClaimsReserveCalculator.Properties;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;

namespace ClaimsReserveCalculator.FrameworksAndDrivers.DataParserUserInterface
{
    /// <summary>
    /// The view model for the data parser. Can be bound to by a corresponding view.
    /// </summary>
    public class DataParserViewModel : IDataParserViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private ICommand _parseButtonCommand;
        private IProcessClaimsDataAdapter _processClaimsDataAdapter;
        private bool _canExecute = true;
        private string _inputSourceFileName = string.Empty;

        public string InputSourceFileName
        {
            get
            {
                return _inputSourceFileName;
            }
            set
            {
                if (_inputSourceFileName != value)
                {
                    _inputSourceFileName = value;
                }

                CanEnableConvertDataOption = !string.IsNullOrWhiteSpace(_inputSourceFileName);

                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("InputSourceFileName"));
                }
            }
        }

        private string _statusText = string.Empty;

        public string StatusText
        {
            get
            {
                return _statusText;
            }
            set
            {
                if (_statusText != value)
                {
                    _statusText = value;
                }

                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("StatusText"));
                }
            }
        }

        private bool _canEnableConvertDataOption = false;

        public bool CanEnableConvertDataOption
        {
            get
            {
                return _canEnableConvertDataOption;
            }
            set
            {
                if (_canEnableConvertDataOption != value)
                {
                    _canEnableConvertDataOption = value;
                }

                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("CanEnableConvertDataOption"));
                }
            }
        }

        public bool CanExecute
        {
            get
            {
                return _canExecute;
            }

            set
            {
                if (_canExecute != value)
                {
                    _canExecute = value;
                }   
            }
        }

        public ICommand ParseButtonCommand
        {
            get
            {
                return _parseButtonCommand;
            }
            set
            {
                _parseButtonCommand = value;
            }
        }

        public DataParserViewModel(IProcessClaimsDataAdapter processClaimsDataAdapter)
        {
            _processClaimsDataAdapter = processClaimsDataAdapter;
            ParseButtonCommand = new RelayCommand(ParseData, param => _canExecute);
        }

        public void ParseData(object obj)
        {
            SaveClaimsData();
        }

        public void SaveClaimsData()
        {
            try
            {
                // The output filepath is derived from the input filepath with '_out.txt' on the end.
                // The user should have the option to choose the destination file path rather than setting it here.
                string filePathWithoutExtension = Path.ChangeExtension(_inputSourceFileName, null);
                string outputDestination = filePathWithoutExtension + "_out.txt";

                _processClaimsDataAdapter.SaveCumulativeClaimsData(
                    _inputSourceFileName, outputDestination);

                StatusText = $"{Resources.UpdatedClaimsOutputFile}:{Environment.NewLine}{outputDestination}";
            }
            catch (InvalidInputSourceFileException)
            {
                StatusText = Resources.InvalidInputSourceError;
            }
            catch (ErrorSavingCumulativeClaimsException)
            {
                StatusText = Resources.SaveCumulativeClaimsDataError;
            }
            catch (ClaimsDataFileReadException)
            {
                StatusText = Resources.ClaimsDataFileReadError;
            }
            catch (ParseClaimsInputDataException)
            {
                StatusText = Resources.ParseClaimsInputDataError;
            }
            catch (WriteProductsClaimsDataException)
            {
                StatusText = Resources.WriteProductsClaimsDataException;
            }
            catch (Exception)
            {
                // Update the status display with general error message
                StatusText = Resources.GeneralIssueSavingClaimsData;
            }
        }
    }
}
