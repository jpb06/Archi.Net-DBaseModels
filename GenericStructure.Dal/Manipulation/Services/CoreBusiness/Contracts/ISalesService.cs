using GenericStructure.Models.Base;
using GenericStructure.Models.CoreBusiness.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Manipulation.Services.CoreBusiness.Contracts
{
    public interface ISalesService
    {
        #region Async
        Task<int> CreateAsync<TModel>(TModel model) where TModel : BaseModel, ISalesModel;
        Task ModifyAsync<TModel>(TModel model) where TModel : BaseModel, ISalesModel;
        Task DeleteAsync<TModel>(TModel model) where TModel : BaseModel, ISalesModel;

        Task<TModel> GetByIdAsync<TModel>(int id) where TModel : BaseModel, ISalesModel;
        Task<List<TModel>> GetAsync<TModel>(
            Expression<Func<TModel, bool>> filter = null,
            Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null,
            string includeProperties = "") where TModel : BaseModel, ISalesModel;
        #endregion
    }
}
