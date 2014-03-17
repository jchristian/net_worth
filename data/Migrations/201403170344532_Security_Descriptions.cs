namespace data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Security_Descriptions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransactionDescriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransactionTypeId = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.BrokerageTransactions", "SecurityDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BrokerageTransactions", "SecurityDescription");
            DropTable("dbo.TransactionDescriptions");
        }
    }
}
