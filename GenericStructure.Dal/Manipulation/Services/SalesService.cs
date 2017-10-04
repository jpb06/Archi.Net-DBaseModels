using GenericStructure.Dal.Context.Contracts;
using GenericStructure.Dal.Manipulation.Repositories;
using GenericStructure.Dal.Manipulation.Repositories.Contracts;
using GenericStructure.Dal.Manipulation.Services.Base;
using GenericStructure.Dal.Manipulation.Services.Configuration;
using GenericStructure.Dal.Manipulation.Services.Contracts;
using GenericStructure.Dal.Models;
using GenericStructure.Dal.Models.Base;
using GenericStructure.Dal.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Manipulation.Services
{
    public class SalesService : BaseService, ISalesService
    {
        public SalesService(IDBContext context,
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

        #region Generic
        public int Create<TModel>(TModel model) where TModel : BaseModel, ISalesModel
        {
            SaveResult result = base.CreateFor(model);

            result.Validate(1);

            return result.AlteredIds[0];
        }

        public void Modify<TModel>(TModel model) where TModel : BaseModel, ISalesModel
        {
            SaveResult result = base.ModifyFor(model);

            result.Validate(1);
        }

        public void Delete<TModel>(TModel model) where TModel : BaseModel, ISalesModel
        {
            SaveResult result = base.DeleteFor(model);

            result.Validate(1);
        }

        public TModel GetById<TModel>(int id) where TModel : BaseModel, ISalesModel
        {
            return (TModel) base.GetByIdFor<TModel>(id);
        }
        #endregion
    }
}
