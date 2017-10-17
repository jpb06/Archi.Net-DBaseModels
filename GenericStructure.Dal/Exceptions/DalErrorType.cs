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
        //                                                                 Exceptions 
        SqlError,
        SqlUniqueConstraintViolation,
        SqlConstraintCheckViolation,
        // --------------------------------------------------------------------------
        //                                                         Repositories stack 
        #region RepositoriesSet
        RepositoriesSetMissingMapping,
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
