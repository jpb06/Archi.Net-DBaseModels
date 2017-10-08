using GenericStructure.Dal.Context.Contracts;
using GenericStructure.Dal.Manipulation.Services.CoreBusiness.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Manipulation.Services.CoreBusiness
{
    public class UsersService : BaseCoreBusinessService
    {
        public UsersService(ICoreBusinessContext context) : base(context) { }
    }
}
