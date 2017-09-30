using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Framework.Exceptions
{
    public class BaseException : Exception
    {
        public string Name { get; set; }

        public BaseException(string exceptionName) : base()
        {
            this.Name = exceptionName;
        }

    }
}
