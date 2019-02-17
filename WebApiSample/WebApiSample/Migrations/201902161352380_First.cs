namespace WebApiSample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    /// <summary>
    /// 
    /// </summary>
    public partial class First : DbMigration
    {
        /// <summary>
        /// 
        /// </summary>
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        UserName2 = c.String(maxLength: 10, fixedLength: true),
                        パスワード = c.String(maxLength: 50),
                        Comments = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        /// <summary>
        /// 
        /// </summary>
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
