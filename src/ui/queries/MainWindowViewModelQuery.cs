using System.Collections.ObjectModel;
using data.models.contexts;
using data.models.read;
using ui.models;

namespace ui.queries
{
    public class MainWindowViewModelQuery
    {
        public MainWindowViewModel Get()
        {
            using (var financial_overview_context = new DataContext())
            {
                return new MainWindowViewModel
                       {
                          FinancialOverview = new FinancialOverviewViewModel
                                              {
                                                  FinancialData = new ObservableCollection<FinancialOverview>(financial_overview_context.FinancialOverviews)
                                              }
                       }; 
            }
        }
    }
}