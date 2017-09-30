using GenericStructure.Dal.Manipulation.Services.Configuration;
using GenericStructure.Dal.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Exceptions.Custom
{
    public class DataConflictException : DalException
    {
        public DataConflictException(DataConflictInfo info)
            : base()
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
