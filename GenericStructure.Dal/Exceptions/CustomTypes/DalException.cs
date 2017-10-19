using System;

namespace GenericStructure.Dal.Exceptions.CustomTypes
{
    public class DalException : Exception
    {
        public string errorType;

        public DalException(string errorType, string message) : base(message) 
        {
            this.errorType = errorType;
        }
    }
}
