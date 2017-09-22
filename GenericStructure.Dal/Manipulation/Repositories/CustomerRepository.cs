using GenericStructure.Dal.Context.Contracts;
using GenericStructure.Dal.Manipulation.Repositories.Base;
using GenericStructure.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Manipulation.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>
    {
        public CustomerRepository() : base() { }
        public CustomerRepository(IDBContext context) : base(context) { }
    }
}
