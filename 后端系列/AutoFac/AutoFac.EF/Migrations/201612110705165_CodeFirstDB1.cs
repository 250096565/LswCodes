namespace AutoFac.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CodeFirstDB1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_User", "Address_DynamicAddress", c => c.String());
            AddColumn("dbo.T_User", "Address_City", c => c.String());
            DropColumn("dbo.T_User", "UserAddress_DynamicAddress");
            DropColumn("dbo.T_User", "UserAddress_City");
        }
        
        public override void Down()
        {
            AddColumn("dbo.T_User", "UserAddress_City", c => c.String());
            AddColumn("dbo.T_User", "UserAddress_DynamicAddress", c => c.String());
            DropColumn("dbo.T_User", "Address_City");
            DropColumn("dbo.T_User", "Address_DynamicAddress");
        }
    }
}
