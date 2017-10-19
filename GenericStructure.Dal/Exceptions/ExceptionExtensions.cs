using GenericStructure.Dal.Exceptions.CustomTypes;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace GenericStructure.Dal.Exceptions
{
    public static class ExceptionExtensions
    {
        public static void HandleException(this Exception exception)
        {
            DbUpdateException dbUpdateEx = exception as DbUpdateException;
            if (dbUpdateEx != null)
            {
                if (dbUpdateEx.InnerException != null && dbUpdateEx.InnerException.InnerException != null)
                {
                    SqlException sqlException = dbUpdateEx.InnerException.InnerException as SqlException;
                    if (sqlException != null)
                    {
                        switch (sqlException.Number)
                        {
                            case 2627:  // Primary key unique violation
                            case 2601:  // Duplicated key row error
                                throw new DalException(DalErrorType.SqlUniqueConstraintViolation, sqlException.Message);
                            case 547:   // Constraint check violation
                                throw new DalException(DalErrorType.SqlConstraintCheckViolation, sqlException.Message);
                            default:
                                throw new DalException(DalErrorType.SqlError, sqlException.Message);
                        }
                    }
                }
            }
        }
    }
}
