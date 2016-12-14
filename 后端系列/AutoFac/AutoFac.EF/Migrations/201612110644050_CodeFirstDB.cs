namespace AutoFac.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CodeFirstDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.T_User",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20, unicode: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        UserAddress_DynamicAddress = c.String(),
                        UserAddress_City = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.T_User_Card",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        IdCard = c.String(maxLength: 18),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.T_User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.T_UserRole",
                c => new
                    {
                        UserRoleId = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserRoleId);
            
            CreateTable(
                "dbo.T_User_UserRole_Config",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        UserRoleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.UserRoleId })
                .ForeignKey("dbo.T_UserRole", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.T_User", t => t.UserRoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.UserRoleId);
            
            CreateTable(
                "dbo.T_SuperUser",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        UserNum = c.String(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.T_User", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.T_SuperUser", "UserId", "dbo.T_User");
            DropForeignKey("dbo.T_User_UserRole_Config", "UserRoleId", "dbo.T_User");
            DropForeignKey("dbo.T_User_UserRole_Config", "UserId", "dbo.T_UserRole");
            DropForeignKey("dbo.T_User_Card", "UserId", "dbo.T_User");
            DropIndex("dbo.T_SuperUser", new[] { "UserId" });
            DropIndex("dbo.T_User_UserRole_Config", new[] { "UserRoleId" });
            DropIndex("dbo.T_User_UserRole_Config", new[] { "UserId" });
            DropIndex("dbo.T_User_Card", new[] { "UserId" });
            DropTable("dbo.T_SuperUser");
            DropTable("dbo.T_User_UserRole_Config");
            DropTable("dbo.T_UserRole");
            DropTable("dbo.T_User_Card");
            DropTable("dbo.T_User");
        }
    }
}
