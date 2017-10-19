using System.Reflection;

namespace GenericStructure.Business.Tests.AssemblyInformation
{
    public static class AssemblyHelper
    {
        public static AssemblyName AssemblyName
        {
            get { return Assembly.GetExecutingAssembly().GetName(); }
        }
    }
}
