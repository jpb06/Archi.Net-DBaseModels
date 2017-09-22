using GenericStructure.Dal.Context.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Manipulation.Services.Configuration
{
    public enum OptimisticConcurrencyPolicy { DatabaseWins, ClientWins, AskClient, NoPolicy }

    public static class OptimisticConcurrency
    {
        public static OptimisticConcurrencyValues ApplyPolicy(OptimisticConcurrencyPolicy policy, DbUpdateConcurrencyException exception) 
        {
            DbEntityEntry entry = exception.Entries.Single();

            switch (policy) 
            {
                case OptimisticConcurrencyPolicy.DatabaseWins :
                    // Update the values of the entity that failed to save from the store 
                    entry.Reload();

                    return null;
                case OptimisticConcurrencyPolicy.ClientWins :
                    // Update original values from the database 
                    entry.OriginalValues.SetValues(entry.GetDatabaseValues());

                    return null;
                case OptimisticConcurrencyPolicy.AskClient:
                    // Get the current entity values and the values in the database 
                    var currentValues = entry.CurrentValues;
                    var databaseValues = entry.GetDatabaseValues().Clone();

                    return new OptimisticConcurrencyValues 
                    {
                        CurrentValues = currentValues, 
                        DatabaseValues = databaseValues
                    };
                default :
                    return null;
            }
        }
    }
}
