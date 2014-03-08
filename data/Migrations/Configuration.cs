using System.Data.Entity.Migrations;
using data.models.contexts;

namespace data.Migrations
{
    sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "data.models.contexts.DataContext";
        }

        protected override void Seed(DataContext context) {}
    }
}