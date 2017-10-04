using GenericStructure.Dal.Context.Contracts;
using GenericStructure.Dal.Manipulation.Repositories.Contracts;
using GenericStructure.Dal.Manipulation.Repositories.Implementation.Base;
using GenericStructure.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Manipulation.Repositories.Implementation.Specific
{
    internal class CustomersRepository : GenericRepository<Customer>, ICustomersRepository
    {
        public CustomersRepository(IDBContext context) : base(context) { }
    }
}
