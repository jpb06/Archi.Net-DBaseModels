using GenericStructure.Dal.Context.Base;

namespace GenericStructure.Dal.Context.EndObjects
{
    // https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/powershell
    // Get-Help EntityFramework

    // Enable-Migrations
    // Add-Migration <name>
    // Update-Database

    // -------------------------------------------------

    // Enable-Migrations -MigrationsDirectory "Migrations\Tests" -ContextTypeName GenericStructure.Dal.Context.EndObjects.CoreBusinessTestContext

    // Add-Migration -ConfigurationTypeName TestsConfiguration -Name <something>

    // Update-Database -ConfigurationTypeName TestsConfiguration

    public class CoreBusinessTestContext : CoreBusinessBaseContext
    {
        public CoreBusinessTestContext() : base("name=CoreBusinessTestContext") { }
    }
}
