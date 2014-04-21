﻿using System.Data.Entity;
using data.models.read;
using data.models.write;

namespace data.models.contexts
{
    public class DataContext : DbContext
    {
        //Write
        public DbSet<Account> Accounts { get; set; }
        public DbSet<BrokerageTransaction> BrokerageTransactions { get; set; }
        public DbSet<Security> Securities { get; set; }
        public DbSet<TransactionDescription> TransactionDescriptions { get; set; }

        //Read
        public DbSet<FinancialOverview> FinancialOverviews { get; set; }
    }
}