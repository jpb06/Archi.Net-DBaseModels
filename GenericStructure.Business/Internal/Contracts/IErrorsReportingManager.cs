using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Business.Internal.Contracts
{
    internal interface IErrorsReportingManager
    {
        void LogError(Exception exception, AssemblyName assemblyName, string errorCode);
        Task LogErrorAsync(Exception exception, AssemblyName assemblyName, string errorCode);
    }
}
