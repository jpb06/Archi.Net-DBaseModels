using GenericStructure.Business.Internal.Contracts;
using GenericStructure.Business.InversionOfControl;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Business.Exposed
{
    public static class Logging
    {
        public static void Save(Exception exception, AssemblyName assemblyName, string errorCode)
        {
            using (ThreadScopedLifestyle.BeginScope(IoCConfiguration.Container))
            {
                IErrorsReportingManager manager = IoCConfiguration.Container.GetInstance<IErrorsReportingManager>();
                manager.LogError(exception, assemblyName, errorCode);
            }
        }
    }
}
