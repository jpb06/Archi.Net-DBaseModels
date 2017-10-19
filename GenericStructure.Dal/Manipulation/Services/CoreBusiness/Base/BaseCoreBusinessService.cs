﻿using GenericStructure.Dal.Context.Contracts;
using GenericStructure.Dal.Manipulation.Repositories;
using GenericStructure.Dal.Manipulation.Services.Base;
using GenericStructure.Dal.Manipulation.Services.CoreBusiness.Configuration;
using GenericStructure.Dal.Models.Base;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Manipulation.Services.CoreBusiness.Base
{
    public class BaseCoreBusinessService : BaseService
    {
        internal RepositoriesSet repositoriesSet;

        public BaseCoreBusinessService(ICoreBusinessContext context) : base(context)
        {
            base.context = context;
            base.policy = DataConflictPolicy.ClientWins;
            this.repositoriesSet = new RepositoriesSet();
        }

        #region Generic alteration
        protected SaveResult CreateFor<TDBaseModel>(TDBaseModel model)
            where TDBaseModel : BaseModel
        {
            var repository = this.repositoriesSet.GetGeneric<TDBaseModel>();
            repository.Insert(model);

            SaveResult result = base.Save(policy);
            result.AlteredIds = new int[] { model.Id };

            return result;
        }

        protected SaveResult ModifyFor<TDBaseModel>(TDBaseModel model)
            where TDBaseModel : BaseModel
        {
            var repository = this.repositoriesSet.GetGeneric<TDBaseModel>();
            repository.Update(model);

            SaveResult result = this.Save(policy);
            result.AlteredIds = new int[] { model.Id };

            return result;
        }

        protected SaveResult DeleteFor<TDBaseModel>(TDBaseModel model)
            where TDBaseModel : BaseModel
        {
            var repository = this.repositoriesSet.GetGeneric<TDBaseModel>();
            repository.Delete(model);

            SaveResult result = this.Save(policy);
            result.AlteredIds = new int[] { model.Id };

            return result;
        }
        #endregion

        #region Generic alteration async
        protected async Task<SaveResult> CreateForAsync<TDBaseModel>(TDBaseModel model)
            where TDBaseModel : BaseModel
        {
            var repository = this.repositoriesSet.GetGeneric<TDBaseModel>();
            repository.Insert(model);

            SaveResult result = await base.SaveAsync(policy);
            result.AlteredIds = new int[] { model.Id };

            return result;
        }

        protected async Task<SaveResult> ModifyForAsync<TDBaseModel>(TDBaseModel model)
            where TDBaseModel : BaseModel
        {
            var repository = this.repositoriesSet.GetGeneric<TDBaseModel>();
            repository.Update(model);

            SaveResult result = await this.SaveAsync(policy);
            result.AlteredIds = new int[] { model.Id };

            return result;
        }

        protected async Task<SaveResult> DeleteForAsync<TDBaseModel>(TDBaseModel model)
            where TDBaseModel : BaseModel
        {
            var repository = this.repositoriesSet.GetGeneric<TDBaseModel>();
            repository.Delete(model);

            SaveResult result = await this.SaveAsync(policy);
            result.AlteredIds = new int[] { model.Id };

            return result;
        }
        #endregion

        #region Data retrieval
        protected TDBaseModel GetByIdFor<TDBaseModel>(int id)
            where TDBaseModel : BaseModel
        {
            var repository = this.repositoriesSet.GetGeneric<TDBaseModel>();
            TDBaseModel model = repository.GetByID(id);

            return model;
        }
        #endregion

       
    }
}
