using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Exceptions
{
    public class DalErrorType
    {
        // --------------------------------------------------------------------------
        //                                                                    Generic 

        // --------------------------------------------------------------------------
        //                                                                 Exceptions 
        public static readonly string SqlError = "Dal.SqlError";
        public static readonly string SqlUniqueConstraintViolation = "Dal.SqlUniqueConstraintViolation";
        public static readonly string SqlConstraintCheckViolation = "Dal.SqlConstraintCheckViolation";
        // --------------------------------------------------------------------------
        //                                                         Repositories stack 
        #region RepositoriesSet
        public static readonly string RepositoriesSetMissingMapping = "Dal.RepositoriesSetMissingMapping";
        #endregion
        // --------------------------------------------------------------------------
        //                                                             Services stack
        #region BaseService
        public static readonly string BaseServiceDataConflictWithNoPolicy = "Dal.BaseServiceDataConflictWithNoPolicy";
        public static readonly string BaseServiceDataConflictWithAskClientPolicy = "Dal.BaseServiceDataConflictWithAskClientPolicy";
        #endregion

        #region SaveResult
        public static readonly string SaveResultPersistenceValidationFailure = "Dal.SaveResultPersistenceValidationFailure";
        #endregion

        #region SalesService

        #endregion
        // --------------------------------------------------------------------------
    }
}
