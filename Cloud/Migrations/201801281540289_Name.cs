namespace BodyLifeSkillsPlatform.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Name : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FabicVideos", "Name", c => c.String());
            DropColumn("dbo.FabicVideos", "Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FabicVideos", "Title", c => c.String());
            DropColumn("dbo.FabicVideos", "Name");
        }
    }
}
