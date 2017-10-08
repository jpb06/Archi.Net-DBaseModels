using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Manipulation.Services.CoreBusiness.Configuration
{
    public class DataConflictInfo
    {
        public DbPropertyValues DatabaseValues { get; set; }
        public DbPropertyValues CurrentValues { get; set; }
    }
}
