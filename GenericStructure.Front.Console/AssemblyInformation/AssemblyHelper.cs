using System.Reflection;

namespace GenericStructure.Front.Console.AssemblyInformation
{
    public static class AssemblyHelper
    {
        public static AssemblyName AssemblyName
        {
            get { return Assembly.GetExecutingAssembly().GetName(); }
        }
    }
}
