﻿using GenericStructure.Dal.Exceptions;
using GenericStructure.Dal.Exceptions.CustomTypes;
using GenericStructure.Dal.Manipulation.Services.CoreBusiness.Configuration;
using System.Linq;

namespace GenericStructure.Dal.Manipulation.Services.Base
{
    public class SaveResult
    {
        public int AlteredObjectsCount { get; set; }
        public int[] AlteredIds { get; set; }
        public DataConflictInfo DataConflictInfo { get; set; }

        public SaveResult()
        {
            this.AlteredObjectsCount = 0;
            this.AlteredIds = new int[] { };
            this.DataConflictInfo = null;
        }

        public void Validate(int expectedObjectsCount, DataConflictPolicy dataConflictPolicy)
        {
            if (dataConflictPolicy == DataConflictPolicy.DatabaseWins && 
                (this.AlteredObjectsCount != 0 || this.AlteredIds.Count() != expectedObjectsCount)) 
            {
                throw new DalException(DalErrorType.SaveResultPersistenceValidationFailure,
                                       "Persistence validation failure");
            }


            if (dataConflictPolicy == DataConflictPolicy.ClientWins && 
                (this.AlteredObjectsCount != expectedObjectsCount || this.AlteredIds.Count() != expectedObjectsCount))
            {
                throw new DalException(DalErrorType.SaveResultPersistenceValidationFailure, 
                                       "Persistence validation failure");
            }

            
        }
    }
}
