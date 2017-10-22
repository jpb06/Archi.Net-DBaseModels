using GenericStructure.Dal.Context.Contracts;
using GenericStructure.Dal.Manipulation.Repositories.Contracts;
using GenericStructure.Dal.Manipulation.Repositories.Implementation.Base;
using GenericStructure.Models.CoreBusiness;

namespace GenericStructure.Dal.Manipulation.Repositories.Implementation.Specific
{
    public class CategoriesRepository : GenericRepository<Category>, ICategoriesRepository
    {
        public CategoriesRepository(ICoreBusinessContext context) : base(context) { }
    }
}
