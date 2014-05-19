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
                        TradeDate = c.DateTime(nullable: false),
                        ProcessDate = c.DateTime(nullable: false),
                        TransactionType = c.Int(nullable: false),
                        TransactionDescription = c.String(),
                        SecurityDescription = c.String(),
                        SharePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Shares = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GrossAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NetAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Account_Id = c.Int(),
                        Security_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .ForeignKey("dbo.Securities", t => t.Security_Id)
                .Index(t => t.Account_Id)
                .Index(t => t.Security_Id);
            
            CreateTable(
                "dbo.Securities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SpecId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FinancialOverviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Income = c.Decimal(precision: 18, scale: 2),
                        Spending = c.Decimal(precision: 18, scale: 2),
                        Savings = c.Decimal(precision: 18, scale: 2),
                        Dividends = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Appreciation = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ChangeInNetWorth = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NetWorth = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SecurityDescriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Security_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Securities", t => t.Security_Id, cascadeDelete: true)
                .Index(t => t.Security_Id);
            
            CreateTable(
                "dbo.TransactionDescriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransactionTypeId = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SecurityDescriptions", "Security_Id", "dbo.Securities");
            DropForeignKey("dbo.BrokerageTransactions", "Security_Id", "dbo.Securities");
            DropForeignKey("dbo.BrokerageTransactions", "Account_Id", "dbo.Accounts");
            DropIndex("dbo.SecurityDescriptions", new[] { "Security_Id" });
            DropIndex("dbo.BrokerageTransactions", new[] { "Security_Id" });
            DropIndex("dbo.BrokerageTransactions", new[] { "Account_Id" });
            DropTable("dbo.TransactionDescriptions");
            DropTable("dbo.SecurityDescriptions");
            DropTable("dbo.FinancialOverviews");
            DropTable("dbo.Securities");
            DropTable("dbo.BrokerageTransactions");
            DropTable("dbo.Accounts");
        }
    }
}
