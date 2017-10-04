using GenericStructure.Dal.Context.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Context.Specific.Main
{
    internal class GenericStructureContext : BaseContext
    {
        public GenericStructureContext() : base("name=GenericStructureContextConnectionString") { }
    }
}
