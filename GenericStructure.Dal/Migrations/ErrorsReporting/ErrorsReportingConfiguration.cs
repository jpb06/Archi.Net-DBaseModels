namespace GenericStructure.Dal.Migrations.ErrorsReporting
{
    using System.Data.Entity.Migrations;

    internal sealed class ErrorsReportingConfiguration : DbMigrationsConfiguration<GenericStructure.Dal.Context.EndObjects.ErrorsReportingContext>
    {
        public ErrorsReportingConfiguration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\ErrorsReporting";
        }

        protected override void Seed(GenericStructure.Dal.Context.EndObjects.ErrorsReportingContext context)
        {
            //  This method will be called after migrating to the latest version.
        }
    }
}
