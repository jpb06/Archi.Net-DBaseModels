﻿using GenericStructure.Dal.Context.Contracts;
using GenericStructure.Dal.Manipulation.Repositories.Contracts;
using GenericStructure.Dal.Manipulation.Services.Base;
using GenericStructure.Dal.Manipulation.Services.CoreBusiness.Base;
using GenericStructure.Dal.Manipulation.Services.CoreBusiness.Configuration;
using GenericStructure.Dal.Manipulation.Services.CoreBusiness.Contracts;
using GenericStructure.Dal.Models.Base;
using GenericStructure.Dal.Models.CoreBusiness;
using GenericStructure.Dal.Models.CoreBusiness.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        #region Generic
        public int Create<TModel>(TModel model) where TModel : BaseModel, ISalesModel
        {
            SaveResult result = base.CreateFor(model);

            result.Validate(1, this.policy);

            return result.AlteredIds[0];
        }

        public void Modify<TModel>(TModel model) where TModel : BaseModel, ISalesModel
        {
            SaveResult result = base.ModifyFor(model);

            result.Validate(1, this.policy);
        }

        public void Delete<TModel>(TModel model) where TModel : BaseModel, ISalesModel
        {
            SaveResult result = base.DeleteFor(model);

            result.Validate(1, this.policy);
        }

        public TModel GetById<TModel>(int id) where TModel : BaseModel, ISalesModel
        {
            return (TModel) base.GetByIdFor<TModel>(id);
        }
        #endregion
    }
}