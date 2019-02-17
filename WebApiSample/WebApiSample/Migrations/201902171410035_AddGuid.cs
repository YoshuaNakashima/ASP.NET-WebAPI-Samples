namespace WebApiSample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGuid : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.categories",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 255),
                        nth_child = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.tree_paths",
                c => new
                    {
                        ancestor = c.Long(nullable: false),
                        descendant = c.Long(nullable: false),
                        path_length = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.ancestor, t.descendant })
                .ForeignKey("dbo.categories", t => t.ancestor)
                .ForeignKey("dbo.categories", t => t.descendant)
                .Index(t => t.ancestor)
                .Index(t => t.descendant);
            
            AddColumn("dbo.Users", "SessionId", c => c.Guid(nullable: false));
            AddColumn("dbo.Users", "UpdatedTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tree_paths", "descendant", "dbo.categories");
            DropForeignKey("dbo.tree_paths", "ancestor", "dbo.categories");
            DropIndex("dbo.tree_paths", new[] { "descendant" });
            DropIndex("dbo.tree_paths", new[] { "ancestor" });
            DropColumn("dbo.Users", "UpdatedTime");
            DropColumn("dbo.Users", "SessionId");
            DropTable("dbo.tree_paths");
            DropTable("dbo.categories");
        }
    }
}
