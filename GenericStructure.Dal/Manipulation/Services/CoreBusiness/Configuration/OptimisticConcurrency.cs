using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Manipulation.Services.CoreBusiness.Configuration
{
    public enum DataConflictPolicy { DatabaseWins, ClientWins, AskClient, NoPolicy }

    public static class OptimisticConcurrency
    {
        public static DataConflictInfo ApplyPolicy(DataConflictPolicy policy, DbUpdateConcurrencyException exception) 
        {
            DbEntityEntry entry = exception.Entries.Single();

            switch (policy) 
            {
                case DataConflictPolicy.DatabaseWins:
                    // Update the values of the entity that failed to save from the store 
                    entry.Reload();

                    return null;
                case DataConflictPolicy.ClientWins:
                    // Update original values from the database 
                    entry.OriginalValues.SetValues(entry.GetDatabaseValues());

                    return null;
                case DataConflictPolicy.AskClient:
                    // Get the current entity values and the values in the database 
                    var currentValues = entry.CurrentValues;
                    var databaseValues = entry.GetDatabaseValues().Clone();

                    return new DataConflictInfo 
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
