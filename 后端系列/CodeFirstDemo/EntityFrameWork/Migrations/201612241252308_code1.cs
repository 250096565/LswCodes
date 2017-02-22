namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class code1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.UserAndUserRoleMap", new[] { "UserRoleId" });
            DropIndex("dbo.UserAndUserRoleMap", new[] { "UserId" });
            RenameColumn(table: "dbo.UserAndUserRoleMap", name: "UserRoleId", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.UserAndUserRoleMap", name: "UserId", newName: "UserRoleId");
            RenameColumn(table: "dbo.UserAndUserRoleMap", name: "__mig_tmp__0", newName: "UserId");
            DropPrimaryKey("dbo.UserAndUserRoleMap");
            AlterColumn("dbo.UserAndUserRoleMap", "UserRoleId", c => c.Int(nullable: false));
            AlterColumn("dbo.UserAndUserRoleMap", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.UserAndUserRoleMap", new[] { "UserId", "UserRoleId" });
            CreateIndex("dbo.UserAndUserRoleMap", "UserId");
            CreateIndex("dbo.UserAndUserRoleMap", "UserRoleId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserAndUserRoleMap", new[] { "UserRoleId" });
            DropIndex("dbo.UserAndUserRoleMap", new[] { "UserId" });
            DropPrimaryKey("dbo.UserAndUserRoleMap");
            AlterColumn("dbo.UserAndUserRoleMap", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.UserAndUserRoleMap", "UserRoleId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.UserAndUserRoleMap", new[] { "UserRoleId", "UserId" });
            RenameColumn(table: "dbo.UserAndUserRoleMap", name: "UserId", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.UserAndUserRoleMap", name: "UserRoleId", newName: "UserId");
            RenameColumn(table: "dbo.UserAndUserRoleMap", name: "__mig_tmp__0", newName: "UserRoleId");
            CreateIndex("dbo.UserAndUserRoleMap", "UserId");
            CreateIndex("dbo.UserAndUserRoleMap", "UserRoleId");
        }
    }
}
