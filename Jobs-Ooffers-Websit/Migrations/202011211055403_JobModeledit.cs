namespace Jobs_Ooffers_Websit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JobModeledit : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Jobs", new[] { "UserId" });
            CreateIndex("dbo.Jobs", "UserID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Jobs", new[] { "UserID" });
            CreateIndex("dbo.Jobs", "UserId");
        }
    }
}
