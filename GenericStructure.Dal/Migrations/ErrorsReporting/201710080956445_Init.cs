namespace GenericStructure.Dal.Migrations.ErrorsReporting
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Applications", new[] { "Name" });
            DropIndex("dbo.Applications", new[] { "Version" });
            CreateIndex("dbo.Applications", new[] { "Name", "Version" }, unique: true, name: "IX_NameAndVersion");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Applications", "IX_NameAndVersion");
            CreateIndex("dbo.Applications", "Version", unique: true);
            CreateIndex("dbo.Applications", "Name", unique: true);
        }
    }
}
