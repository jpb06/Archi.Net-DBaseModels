using GenericStructure.Business.Internal.Contracts;
using GenericStructure.Business.InversionOfControl;
using SimpleInjector.Lifestyles;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace GenericStructure.Business.Exposed
{
    public static class Logging
    {
        public static async Task SaveAsync(Exception exception, AssemblyName assemblyName, string errorCode)
        {
            using (ThreadScopedLifestyle.BeginScope(IoCConfiguration.Container))
            {
                IErrorsReportingManager manager = IoCConfiguration.Container.GetInstance<IErrorsReportingManager>();
                await manager.LogErrorAsync(exception, assemblyName, errorCode);
            }
        }
    }
}
