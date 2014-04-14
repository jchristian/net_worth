namespace data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Security_Descriptions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "SpecId", c => c.Int());
            AddColumn("dbo.Securities", "SpecId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Securities", "SpecId");
            DropColumn("dbo.Accounts", "SpecId");
        }
    }
}
