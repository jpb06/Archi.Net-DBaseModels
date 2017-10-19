using GenericStructure.Dal.Context.Contracts;
using GenericStructure.Dal.Manipulation.Repositories.Contracts;
using GenericStructure.Dal.Manipulation.Repositories.Implementation.Base;
using GenericStructure.Dal.Models.CoreBusiness;

namespace GenericStructure.Dal.Manipulation.Repositories.Implementation.Specific
{
    public class ArticlesRepository : GenericRepository<Article>, IArticlesRepository
    {
        public ArticlesRepository(ICoreBusinessContext context) : base(context) { }
    }
}
