using GenericStructure.Dal.Models.Base;
using GenericStructure.Dal.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Manipulation.Services.Contracts
{
    public interface ISalesService
    {
        int Create<TModel>(TModel model) where TModel : BaseModel, ISalesModel;
        void Modify<TModel>(TModel model) where TModel : BaseModel, ISalesModel;
        void Delete<TModel>(TModel model) where TModel : BaseModel, ISalesModel;
        TModel GetById<TModel>(int id) where TModel : BaseModel, ISalesModel;
    }
}
