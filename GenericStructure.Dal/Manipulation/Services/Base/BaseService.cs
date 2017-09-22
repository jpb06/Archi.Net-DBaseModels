using GenericStructure.Dal.Context;
using GenericStructure.Dal.Context.Contracts;
using GenericStructure.Dal.Manipulation.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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

        public SaveResult Save(OptimisticConcurrencyPolicy policy = OptimisticConcurrencyPolicy.ClientWins)
        {   
            bool saveFailed;
            do
            {
                saveFailed = false;

                try
                {
                    int result = this.context.SaveChanges();

                    return new SaveResult { AffectedObjectsCount = result };
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (policy == OptimisticConcurrencyPolicy.NoPolicy) throw;

                    saveFailed = true;

                    OptimisticConcurrencyValues feedback = OptimisticConcurrency.ApplyPolicy(policy, ex);
                    if (feedback != null) 
                        return new SaveResult { Feedback = feedback };
                }

            } while (saveFailed);

            return null;
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
