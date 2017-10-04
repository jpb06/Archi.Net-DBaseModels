using GenericStructure.Dal.Context.Contracts;
using GenericStructure.Dal.Manipulation.Repositories;
using GenericStructure.Dal.Manipulation.Services.Base;
using GenericStructure.Dal.Manipulation.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Manipulation.Services
{
    public class OrdersService : BaseService
    {
        public OrdersService(IDBContext context) : base(context) { }
    }
}
