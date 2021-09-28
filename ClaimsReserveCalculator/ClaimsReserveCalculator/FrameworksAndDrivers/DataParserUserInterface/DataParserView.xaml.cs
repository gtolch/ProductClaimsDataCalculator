using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ClaimsReserveCalculator.Properties;
using Unity;

namespace ClaimsReserveCalculator.FrameworksAndDrivers.DataParserUserInterface
{
    /// <summary>
    /// Interaction logic for DataParserView.xaml
    /// </summary>
    public partial class DataParserView : UserControl
    {
        private IDataParserViewModel _viewModel;

        public DataParserView()
        {
            InitializeComponent();
            _viewModel = DependencyInjector.Instance.Resolve<IDataParserViewModel>();
            this.DataContext = _viewModel;
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            try
            {
                if (openFileDialog.ShowDialog() == true)
                {
                    _viewModel.InputSourceFileName = openFileDialog.FileName;
                    TextEditor.Text = File.ReadAllText(openFileDialog.FileName);
                }
            }
            catch
            {
            }         
        }
    }
}
