using GenericStructure.Dal.Context;
using GenericStructure.Dal.Context.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Manipulation.Services.Base
{
    public class BaseService : IDisposable
    {
        protected IDBContext context;

        public BaseService()
        {
            this.context = new GenericStructureContext();
        }

        public int Save()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
