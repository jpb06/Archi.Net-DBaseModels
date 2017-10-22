using GenericStructure.Dal.Context.Contracts;
using GenericStructure.Dal.Manipulation.Repositories.Contracts;
using GenericStructure.Dal.Manipulation.Repositories.Implementation.Base;
using GenericStructure.Models.CoreBusiness;

namespace GenericStructure.Dal.Manipulation.Repositories.Implementation.Specific
{
    public class OrdersRepository : GenericRepository<Order>, IOrdersRepository
    {
        public OrdersRepository(ICoreBusinessContext context) : base(context) { }
    }
}
