using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Exceptions
{
    public enum DalErrorType
    {
        // --------------------------------------------------------------------------
        //                                                                    Generic 

        // --------------------------------------------------------------------------
        //                                                         Repositories stack 
        #region RepositoryLocator
        RepositoryLocatorMissingMapping,
        RepositoryLocatorInvalidCast,
        #endregion
        // --------------------------------------------------------------------------
        //                                                             Services stack
        #region BaseService
        BaseServiceDataConflictWithNoPolicy,
        BaseServiceDataConflictWithAskClientPolicy,
        #endregion

        #region SaveResult
        SaveResultPersistenceValidationFailure,
        #endregion

        #region SalesService

        #endregion
        // --------------------------------------------------------------------------
    }
}
