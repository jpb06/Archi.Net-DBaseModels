using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Shared.Tests.Exceptions
{
    public static class ExceptionGenerator
    {
        public static void ThrowsOne() 
        {
            throw new Exception("One");
        }

        public static void ThrowsTwo() 
        {
            try 
            {
                ThrowsOne();
            }
            catch (Exception ex) 
            {
                throw new Exception("Two", ex);
            }
        }
    }
}
