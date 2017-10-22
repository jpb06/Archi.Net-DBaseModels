using GenericStructure.Dal.Exceptions.CustomTypes;
using GenericStructure.Dal.Manipulation.Services.CoreBusiness.Configuration;
using GenericStructure.Models.Base;

namespace GenericStructure.Dal.Exceptions.Custom.Specific
{
    public class DataConflictException : DalException
    {
        public DataConflictException(string errorType, DataConflictInfo info)
            : base(errorType, "Data conflict (Optimistic concurrency)")
        {
            BaseModel dbValues = (BaseModel)info.DatabaseValues.ToObject();
            BaseModel cValues = (BaseModel)info.CurrentValues.ToObject();

            this.DatabaseValues = dbValues;
            this.CurrentValues = cValues;
        }

        public BaseModel DatabaseValues { get; set; }
        public BaseModel CurrentValues { get; set; }
    }
}
