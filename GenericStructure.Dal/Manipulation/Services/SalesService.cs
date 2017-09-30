using GenericStructure.Dal.Manipulation.Repositories;
using GenericStructure.Dal.Manipulation.Services.Base;
using GenericStructure.Dal.Manipulation.Services.Configuration;
using GenericStructure.Dal.Models.Base;
using GenericStructure.Dal.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Manipulation.Services
{
    public class SalesService : BaseService
    {
        public SalesService() : base() { }
        public SalesService(DataConflictPolicy policy) : base(policy) { }

        #region Alteration
        public int Create<TModel>(TModel model) where TModel : BaseModel, ISalesModel
        {
            SaveResult result = this.CreateFor(model);

            result.Validate(1);

            return result.AlteredIds[0];
        }

        public void Modify<TModel>(TModel model) where TModel : BaseModel, ISalesModel
        {
            SaveResult result = this.ModifyFor(model);

            result.Validate(1);
        }

        public void Delete<TModel>(TModel model) where TModel : BaseModel, ISalesModel
        {
            SaveResult result = this.DeleteFor(model);

            result.Validate(1);
        }
        #endregion

        #region Data
        public TModel GetById<TModel>(int id) where TModel : BaseModel, ISalesModel
        {
            return (TModel)this.GetByIdFor<TModel>(id);
        }
        #endregion
    }
}
