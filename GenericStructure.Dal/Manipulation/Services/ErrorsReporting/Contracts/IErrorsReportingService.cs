using GenericStructure.Dal.Models.ErrorsReporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Manipulation.Services.ErrorsReporting.Contracts
{
    public interface IErrorsReportingService
    {
        ErrorReportApplication GetApplication(string name, string version);
        ErrorReportApplication CreateApplication(string name, string version);
        int? LogException(int versionId, Exception exception, string errorCode);
    }
}
