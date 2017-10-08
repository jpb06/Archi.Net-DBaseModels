using GenericStructure.Dal.Context.Contracts;
using GenericStructure.Dal.Manipulation.Services.CoreBusiness.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Manipulation.Services.CoreBusiness
{
    public class OrdersService : BaseCoreBusinessService
    {
        public OrdersService(ICoreBusinessContext context) : base(context) { }
    }
}
