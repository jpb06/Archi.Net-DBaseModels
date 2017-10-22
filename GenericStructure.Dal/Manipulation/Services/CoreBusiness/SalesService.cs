using GenericStructure.Dal.Context.Contracts;
using GenericStructure.Dal.Manipulation.Repositories.Contracts;
using GenericStructure.Dal.Manipulation.Services.Base;
using GenericStructure.Dal.Manipulation.Services.CoreBusiness.Base;
using GenericStructure.Dal.Manipulation.Services.CoreBusiness.Configuration;
using GenericStructure.Dal.Manipulation.Services.CoreBusiness.Contracts;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GenericStructure.Models.Base;
using GenericStructure.Models.CoreBusiness.Contracts;
using GenericStructure.Models.CoreBusiness;

namespace GenericStructure.Dal.Manipulation.Services.CoreBusiness
{
    public class SalesService : BaseCoreBusinessService, ISalesService
    {
        public SalesService(ICoreBusinessContext context,
                            IArticlesRepository articlesRespository,
                            ICategoriesRepository categoriesRespository)
            : base(context) 
        {
            base.repositoriesSet.Register<Article, IArticlesRepository>(articlesRespository);
            base.repositoriesSet.Register<Category, ICategoriesRepository>(categoriesRespository);
        }

        public void SetPolicy(DataConflictPolicy policy)
        {
            base.policy = policy;
        }

        #region Generic async
        public async Task<int> CreateAsync<TModel>(TModel model) where TModel : BaseModel, ISalesModel
        {
            SaveResult result = await base.CreateForAsync(model);

            result.Validate(1, this.policy);

            return result.AlteredIds[0];
        }

        public async Task ModifyAsync<TModel>(TModel model) where TModel : BaseModel, ISalesModel
        {
            SaveResult result = await base.ModifyForAsync(model);

            result.Validate(1, this.policy);
        }

        public async Task DeleteAsync<TModel>(TModel model) where TModel : BaseModel, ISalesModel
        {
            SaveResult result = await base.DeleteForAsync(model);

            result.Validate(1, this.policy);
        }

        public async Task<List<TModel>> GetAsync<TModel>(
            Expression<Func<TModel, bool>> filter = null, 
            Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null, 
            string includeProperties = ""
            ) where TModel : BaseModel, ISalesModel
        {
            var repository = this.repositoriesSet.GetGeneric<TModel>();

            var res = await repository.GetAsync(filter, orderBy, includeProperties);

            return res.ToList();
        }

        public async Task<TModel> GetByIdAsync<TModel>(int id) where TModel : BaseModel, ISalesModel
        {
            return (TModel) await base.GetByIdForAsync<TModel>(id);
        }
        #endregion
    }
}
