using GenericStructure.Models.CoreBusiness;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GenericStructure.Business.Internal.Contracts
{
    internal interface ISalesManager
    {
        Task<List<Article>> GetArticlesAsync(int idCategory);

    }
}
