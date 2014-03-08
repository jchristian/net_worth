using System.Collections.ObjectModel;
using data.models;
using data.models.read;

namespace ui.models
{
    public class FinancialOverviewViewModel
    {
        public ObservableCollection<FinancialOverview> FinancialData { get; set; }
    }
}