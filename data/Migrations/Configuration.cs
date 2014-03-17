using System.Data.Entity.Migrations;
using data.models.contexts;
using data.models.write;

namespace data.Migrations
{
    sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "data.models.contexts.DataContext";
        }

        protected override void Seed(DataContext context)
        {
            context.TransactionDescriptions.AddRange(new[]
            {
                new TransactionDescription { Description = "Buy", TransactionTypeId = 1 },
                new TransactionDescription { Description = "Sell", TransactionTypeId = 2 },
                new TransactionDescription { Description = "Distribution_Dividend", TransactionTypeId = 3 },
                new TransactionDescription { Description = "Distribution_LongTermCapGain", TransactionTypeId = 4 },
                new TransactionDescription { Description = "Distribution_ShortTermCapGain", TransactionTypeId = 5 },
                new TransactionDescription { Description = "Exchange", TransactionTypeId = 6 },
                new TransactionDescription { Description = "Conversion", TransactionTypeId = 7 }
            });
        }
    }
}