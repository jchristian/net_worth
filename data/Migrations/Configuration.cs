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
            context.TransactionDescriptions.AddOrUpdate(x => x.TransactionTypeId,
                Enum.GetValues(typeof(TransactionType))
                .Cast<TransactionType>()
                .Select(x => new TransactionDescription { TransactionTypeId = (int)x, Description = x.ToString() }).ToArray());

            context.Securities.AddIfDoesNotExist(s => s.SpecId, Security.Missing);
            context.Accounts.AddIfDoesNotExist(s => s.SpecId, Account.Missing);

            context.SaveChanges();
        }
    }
}