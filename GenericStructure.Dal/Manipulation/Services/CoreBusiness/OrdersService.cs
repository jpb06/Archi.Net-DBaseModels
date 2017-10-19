using GenericStructure.Dal.Context.Contracts;
using GenericStructure.Dal.Manipulation.Services.CoreBusiness.Base;

namespace GenericStructure.Dal.Manipulation.Services.CoreBusiness
{
    public class OrdersService : BaseCoreBusinessService
    {
        public OrdersService(ICoreBusinessContext context) : base(context) { }
    }
}
