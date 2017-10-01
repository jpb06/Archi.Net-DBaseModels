using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Exceptions
{
    public class DalException : Exception
    {
        public DalErrorType errorType;

        public DalException(DalErrorType errorType, string message) : base(message) 
        {
            this.errorType = errorType;
        }
    }
}
