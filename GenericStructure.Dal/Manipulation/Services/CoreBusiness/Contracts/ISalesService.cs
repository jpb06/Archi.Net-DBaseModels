using GenericStructure.Dal.Models.Base;
using GenericStructure.Dal.Models.CoreBusiness.Contracts;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Manipulation.Services.CoreBusiness.Contracts
{
    public interface ISalesService
    {
        int Create<TModel>(TModel model) where TModel : BaseModel, ISalesModel;
        Task<int> CreateAsync<TModel>(TModel model) where TModel : BaseModel, ISalesModel;
        void Modify<TModel>(TModel model) where TModel : BaseModel, ISalesModel;
        Task ModifyAsync<TModel>(TModel model) where TModel : BaseModel, ISalesModel;
        void Delete<TModel>(TModel model) where TModel : BaseModel, ISalesModel;
        Task DeleteAsync<TModel>(TModel model) where TModel : BaseModel, ISalesModel;
        TModel GetById<TModel>(int id) where TModel : BaseModel, ISalesModel;
    }
}
