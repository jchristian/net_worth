using System;
using System.Data.Entity.Migrations;
using System.Linq;
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
            context.TransactionMatches.AddOrUpdate(x => x.TransactionType,
                Enum.GetValues(typeof(TransactionType))
                .Cast<TransactionType>()
                .Select(x => new TransactionMatch { TransactionType = x, Description = x.ToString(), TransactionMatchType = TransactionMatchType.ExactMatch }).ToArray());

            context.SaveChanges();

            new VanguardSeedData().Seed(context);
        }
    }
}