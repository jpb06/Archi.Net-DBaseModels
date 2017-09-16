using GenericStructure.DataAccessLayer.Context.Contracts;
using GenericStructure.DataAccessLayer.Manipulation.Repositories.Base;
using GenericStructure.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.DataAccessLayer.Manipulation.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>
    {
        public CustomerRepository() : base() { }
        public CustomerRepository(IDBContext context) : base(context) { }
    }
}
