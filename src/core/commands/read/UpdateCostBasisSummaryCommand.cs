using core.work;
using data.models.contexts;
using YSQ.core;

namespace core.commands.read
{
    public class UpdateCostBasisSummaryCommand
    {
        DataContext context;
        CostBasisSummaryCalculator calculator;
        QuoteService quote_service;

        public UpdateCostBasisSummaryCommand(DataContext context, CostBasisSummaryCalculator calculator, QuoteService quote_service)
        {
            this.context = context;
            this.calculator = calculator;
            this.quote_service = quote_service;
        }

        public void Execute()
        {
            
        }
    }
}