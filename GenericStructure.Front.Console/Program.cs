using GenericStructure.Business.Exposed;
using GenericStructure.Business.InversionOfControl;
using GenericStructure.Front.Console.AssemblyInformation;
using GenericStructure.Front.Console.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Front.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            IoCConfiguration.Setup();

            int a = 2, b = 0;

            try
            {
                int c = a / b;
            }
            catch (Exception exception)
            {
                Logging.Save(exception, AssemblyHelper.AssemblyName, ConsoleErrorType.DivideByZero);
            }
        }
    }
}
