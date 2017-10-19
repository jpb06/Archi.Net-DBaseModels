using System;
using System.Reflection;
using System.Threading.Tasks;

namespace GenericStructure.Business.Internal.Contracts
{
    internal interface IErrorsReportingManager
    {
        void LogError(Exception exception, AssemblyName assemblyName, string errorCode);
        Task LogErrorAsync(Exception exception, AssemblyName assemblyName, string errorCode);
    }
}
