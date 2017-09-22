using GenericStructure.Dal.Manipulation.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Manipulation.Services.Base
{
    public class SaveResult
    {
        public int AffectedObjectsCount { get; set; }
        public OptimisticConcurrencyValues Feedback { get; set; }
    }
}
