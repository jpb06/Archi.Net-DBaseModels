using GenericStructure.Dal.Context;
using GenericStructure.Dal.Context.Contracts;
using GenericStructure.Dal.Exceptions;
using GenericStructure.Dal.Exceptions.Custom;
using GenericStructure.Dal.Manipulation.Repositories;
using GenericStructure.Dal.Manipulation.Repositories.Contracts;
using GenericStructure.Dal.Manipulation.Services.Configuration;
using GenericStructure.Dal.Models.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Manipulation.Services.Base
{
    public class BaseService
    {
        protected IDBContext context;
        internal RepositoriesSet repositoriesSet;
        public DataConflictPolicy policy;

        public BaseService(IDBContext context)
        {
            this.context = context;
            this.repositoriesSet = new RepositoriesSet();
            this.policy = DataConflictPolicy.ClientWins;
        }

        #region Generic alteration
        protected SaveResult CreateFor<TDBaseModel>(TDBaseModel model)
            where TDBaseModel : BaseModel
        {
            var repository = this.repositoriesSet.GetGeneric<TDBaseModel>();
            repository.Insert(model);

            SaveResult result = this.Save(policy);
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

        #region Data retrieval
        protected TDBaseModel GetByIdFor<TDBaseModel>(int id)
            where TDBaseModel : BaseModel
        {
            var repository = this.repositoriesSet.GetGeneric<TDBaseModel>();
            TDBaseModel model = repository.GetByID(id);

            return model;
        }
        #endregion

        protected SaveResult Save(DataConflictPolicy policy)
        {
            bool saveFailed;
            do
            {
                saveFailed = false;

                try
                {
                    int result = this.context.SaveChanges();

                    return new SaveResult { AlteredObjectsCount = result };
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (policy == DataConflictPolicy.NoPolicy) 
                        throw new DalException(DalErrorType.BaseServiceDataConflictWithNoPolicy, 
                            "Data conflict (Optimistic concurrency)");

                    saveFailed = true;

                    DataConflictInfo info = OptimisticConcurrency.ApplyPolicy(policy, ex);
                    if (info != null) 
                        throw new DataConflictException(DalErrorType.BaseServiceDataConflictWithAskClientPolicy, info);
                }

            } while (saveFailed);

            return null;
        }
    }
}
