using System.Data.Entity.Infrastructure;

namespace GenericStructure.Dal.Manipulation.Services.CoreBusiness.Configuration
{
    public class DataConflictInfo
    {
        public DbPropertyValues DatabaseValues { get; set; }
        public DbPropertyValues CurrentValues { get; set; }
    }
}
