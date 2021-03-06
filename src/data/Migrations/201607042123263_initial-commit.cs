namespace data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcommit : DbMigration
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
                        Ticker = c.String(),
                        Name = c.String(),
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
                "dbo.Trades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AquireDate = c.DateTime(nullable: false),
                        ClosingDate = c.DateTime(nullable: false),
                        PositionId = c.Int(nullable: false),
                        ClosingTransactionId = c.Int(nullable: false),
                        Quantity = c.Decimal(nullable: false, precision: 19, scale: 6),
                        SellPrice = c.Decimal(nullable: false, precision: 19, scale: 6),
                        ProfileAndLoss = c.Decimal(nullable: false, precision: 19, scale: 6),
                        BrokerageTransaction_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BrokerageTransactions", t => t.ClosingTransactionId)
                .ForeignKey("dbo.Lots", t => t.PositionId, cascadeDelete: true)
                .ForeignKey("dbo.BrokerageTransactions", t => t.BrokerageTransaction_Id)
                .Index(t => t.PositionId)
                .Index(t => t.ClosingTransactionId)
                .Index(t => t.BrokerageTransaction_Id);
            
            CreateTable(
                "dbo.Lots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BrokerageTransactionId = c.Int(nullable: false),
                        IsOpen = c.Boolean(nullable: false),
                        RemainingShares = c.Decimal(nullable: false, precision: 19, scale: 6),
                        RemainingAmount = c.Decimal(nullable: false, precision: 19, scale: 6),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BrokerageTransactions", t => t.BrokerageTransactionId, cascadeDelete: true)
                .Index(t => t.BrokerageTransactionId);
            
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
            DropForeignKey("dbo.Trades", "BrokerageTransaction_Id", "dbo.BrokerageTransactions");
            DropForeignKey("dbo.Trades", "PositionId", "dbo.Lots");
            DropForeignKey("dbo.Lots", "BrokerageTransactionId", "dbo.BrokerageTransactions");
            DropForeignKey("dbo.Trades", "ClosingTransactionId", "dbo.BrokerageTransactions");
            DropForeignKey("dbo.SecurityDescriptions", "SecurityId", "dbo.Securities");
            DropForeignKey("dbo.BrokerageTransactions", "SecurityId", "dbo.Securities");
            DropForeignKey("dbo.BrokerageTransactions", "AccountId", "dbo.Accounts");
            DropIndex("dbo.Lots", new[] { "BrokerageTransactionId" });
            DropIndex("dbo.Trades", new[] { "BrokerageTransaction_Id" });
            DropIndex("dbo.Trades", new[] { "ClosingTransactionId" });
            DropIndex("dbo.Trades", new[] { "PositionId" });
            DropIndex("dbo.SecurityDescriptions", new[] { "SecurityId" });
            DropIndex("dbo.BrokerageTransactions", new[] { "SecurityId" });
            DropIndex("dbo.BrokerageTransactions", new[] { "AccountId" });
            DropTable("dbo.TransactionMatches");
            DropTable("dbo.Lots");
            DropTable("dbo.Trades");
            DropTable("dbo.SecurityDescriptions");
            DropTable("dbo.Securities");
            DropTable("dbo.BrokerageTransactions");
            DropTable("dbo.Accounts");
        }
    }
}
