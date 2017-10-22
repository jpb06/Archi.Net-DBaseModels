using System.Collections.Generic;
using System.Threading.Tasks;
using GenericStructure.Business.Internal.Contracts;
using GenericStructure.Dal.Manipulation.Services.CoreBusiness.Contracts;
using System.Linq;
using GenericStructure.Models.CoreBusiness;

namespace GenericStructure.Business.Internal
{
    internal class SalesManager : ISalesManager
    {
        private ISalesService salesService;

        public SalesManager(ISalesService salesService) => this.salesService = salesService;

        public async Task<List<Article>> GetArticlesAsync(int idCategory)
        {
            var articles = await this.salesService.GetAsync<Article>(
                filter: art => art.IdCategory == idCategory,
                orderBy: q => q.OrderByDescending(e => e.Price),
                includeProperties: "Category,OrderDetails"
            );

            return articles;
        }
    }
}
