using System.Data.Entity;
using data.models.read;
using data.models.write;

namespace data.models.contexts
{
    public class DataContext : DbContext
    {
        //Write
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<BrokerageTransaction> BrokerageTransactions { get; set; }
        public virtual DbSet<Security> Securities { get; set; }
        public virtual DbSet<SecurityDescription> SecurityDescriptions { get; set; }
        public virtual DbSet<TransactionMatch> TransactionMatches { get; set; }

        //Read
        public virtual DbSet<FinancialOverview> FinancialOverviews { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<decimal>().Configure(prop => prop.HasPrecision(19, 6));

            modelBuilder.Entity<SecurityDescription>().HasRequired(x => x.Security);
            modelBuilder.Entity<BrokerageTransaction>().HasRequired(x => x.Account);
            modelBuilder.Entity<BrokerageTransaction>().HasOptional(x => x.Security);

            modelBuilder.Entity<Security>()
                .HasMany(x => x.BrokerageTransactions)
                .WithOptional(x => x.Security)
                .WillCascadeOnDelete(false);

        }
    }
}