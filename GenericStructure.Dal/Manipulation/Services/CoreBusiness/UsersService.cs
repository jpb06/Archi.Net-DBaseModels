using GenericStructure.Dal.Context.Contracts;
using GenericStructure.Dal.Manipulation.Services.CoreBusiness.Base;

namespace GenericStructure.Dal.Manipulation.Services.CoreBusiness
{
    public class UsersService : BaseCoreBusinessService
    {
        public UsersService(ICoreBusinessContext context) : base(context) { }
    }
}
