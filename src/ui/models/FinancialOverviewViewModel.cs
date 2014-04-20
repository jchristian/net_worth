using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using data.models.read;

namespace ui.models
{
    public class FinancialOverviewViewModel
    {
        public ImportTransactionsCommand ImportTransactionsCommand { get { return new ImportTransactionsCommand(); } }
        public ObservableCollection<FinancialOverview> FinancialData { get; set; }
    }

    public class ImportTransactionsCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {

        }
    }
}