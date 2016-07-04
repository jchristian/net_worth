using System.Data.Entity;
using data.models.write;

namespace data.models.contexts
{
    public class DataContext : DbContext
    {
        //Write
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<BrokerageTransaction> BrokerageTransactions { get; set; }
        public virtual DbSet<Lot> Lots { get; set; }
        public virtual DbSet<Trade> Trades { get; set; }
        public virtual DbSet<Security> Securities { get; set; }
        public virtual DbSet<SecurityDescription> SecurityDescriptions { get; set; }
        public virtual DbSet<TransactionMatch> TransactionMatches { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<decimal>().Configure(prop => prop.HasPrecision(19, 6));

            modelBuilder.Entity<Trade>().HasRequired(x => x.Position);
            modelBuilder.Entity<Trade>().HasRequired(x => x.ClosingTransaction).WithMany().HasForeignKey(x => x.ClosingTransactionId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Lot>().HasRequired(x => x.BrokerageTransaction);
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