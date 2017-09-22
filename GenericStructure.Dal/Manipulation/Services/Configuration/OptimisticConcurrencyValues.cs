using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Manipulation.Services.Configuration
{
    public class OptimisticConcurrencyValues
    {
        public DbPropertyValues DatabaseValues { get; set; }
        public DbPropertyValues CurrentValues { get; set; }
    }
}
