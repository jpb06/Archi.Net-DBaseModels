using GenericStructure.Business.Internal.Contracts;
using GenericStructure.Business.InversionOfControl;
using GenericStructure.Models.CoreBusiness;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Business.Exposed
{
    public static class Sales
    {
        public static async Task<List<Article>> GetArticlesAsync(int idCategory)
        {
            using (ThreadScopedLifestyle.BeginScope(IoCConfiguration.Container))
            {
                ISalesManager salesManager = IoCConfiguration.Container.GetInstance<ISalesManager>();
                return await salesManager.GetArticlesAsync(idCategory);
            }
        }
    }
}
