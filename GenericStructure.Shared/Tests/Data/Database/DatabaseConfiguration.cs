using System.Configuration;

namespace GenericStructure.Shared.Tests.Data.Database
{
    public static class DatabaseConfiguration
    {
        public static string CoreBusinessConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["CoreBusinessTestDB"].ConnectionString; }
        }
        public static string ErrorsReportingConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["ErrorsReportingContext"].ConnectionString; }
        }
    }
}
