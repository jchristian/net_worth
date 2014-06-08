namespace data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial_create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SpecId = c.Int(),
                        Number = c.String(),
                        Name = c.String(),
                        AccountType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BrokerageTransactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        TradeDate = c.DateTime(nullable: false),
                        ProcessDate = c.DateTime(nullable: false),
                        TransactionType = c.Int(nullable: false),
                        TransactionDescription = c.String(),
                        SecurityId = c.Int(),
                        SecurityDescription = c.String(),
                        SharePrice = c.Decimal(nullable: false, precision: 19, scale: 6),
                        Shares = c.Decimal(nullable: false, precision: 19, scale: 6),
                        GrossAmount = c.Decimal(nullable: false, precision: 19, scale: 6),
                        NetAmount = c.Decimal(nullable: false, precision: 19, scale: 6),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.Securities", t => t.SecurityId)
                .Index(t => t.AccountId)
                .Index(t => t.SecurityId);
            
            CreateTable(
                "dbo.Securities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SpecId = c.Int(),
                        Ticker = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SecurityDescriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SecurityId = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Securities", t => t.SecurityId, cascadeDelete: true)
                .Index(t => t.SecurityId);
            
            CreateTable(
                "dbo.FinancialOverviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Income = c.Decimal(precision: 19, scale: 6),
                        Spending = c.Decimal(precision: 19, scale: 6),
                        Savings = c.Decimal(precision: 19, scale: 6),
                        Dividends = c.Decimal(nullable: false, precision: 19, scale: 6),
                        Appreciation = c.Decimal(nullable: false, precision: 19, scale: 6),
                        ChangeInNetWorth = c.Decimal(nullable: false, precision: 19, scale: 6),
                        NetWorth = c.Decimal(nullable: false, precision: 19, scale: 6),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TransactionMatches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransactionType = c.Int(nullable: false),
                        Description = c.String(),
                        TransactionMatchType = c.Int(nullable: false),
                        ContainsMatchString = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SecurityDescriptions", "SecurityId", "dbo.Securities");
            DropForeignKey("dbo.BrokerageTransactions", "SecurityId", "dbo.Securities");
            DropForeignKey("dbo.BrokerageTransactions", "AccountId", "dbo.Accounts");
            DropIndex("dbo.SecurityDescriptions", new[] { "SecurityId" });
            DropIndex("dbo.BrokerageTransactions", new[] { "SecurityId" });
            DropIndex("dbo.BrokerageTransactions", new[] { "AccountId" });
            DropTable("dbo.TransactionMatches");
            DropTable("dbo.FinancialOverviews");
            DropTable("dbo.SecurityDescriptions");
            DropTable("dbo.Securities");
            DropTable("dbo.BrokerageTransactions");
            DropTable("dbo.Accounts");
        }
    }
}
