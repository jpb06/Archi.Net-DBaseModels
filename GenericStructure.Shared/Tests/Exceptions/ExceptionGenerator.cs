using System;

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
