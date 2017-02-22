namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class code : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.T_User",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        Age = c.String(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.T_UserRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserAndUserRoleMap",
                c => new
                    {
                        UserRoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserRoleId, t.UserId })
                .ForeignKey("dbo.T_User", t => t.UserRoleId, cascadeDelete: true)
                .ForeignKey("dbo.T_UserRole", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserRoleId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAndUserRoleMap", "UserId", "dbo.T_UserRole");
            DropForeignKey("dbo.UserAndUserRoleMap", "UserRoleId", "dbo.T_User");
            DropIndex("dbo.UserAndUserRoleMap", new[] { "UserId" });
            DropIndex("dbo.UserAndUserRoleMap", new[] { "UserRoleId" });
            DropTable("dbo.UserAndUserRoleMap");
            DropTable("dbo.T_UserRole");
            DropTable("dbo.T_User");
        }
    }
}
