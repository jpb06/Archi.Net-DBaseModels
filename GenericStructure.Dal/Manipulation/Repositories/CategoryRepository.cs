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
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository() : base() { }
        public CategoryRepository(IDBContext context) : base(context) { }
    }
}
