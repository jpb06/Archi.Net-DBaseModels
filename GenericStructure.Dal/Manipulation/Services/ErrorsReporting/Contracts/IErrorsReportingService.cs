using GenericStructure.Models.ErrorsReporting;
using System;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Manipulation.Services.ErrorsReporting.Contracts
{
    public interface IErrorsReportingService
    {
        Task<ErrorReportApplication> GetApplicationAsync(string name, string version);
        Task<ErrorReportApplication> CreateApplicationAsync(string name, string version);
        Task<int?> LogExceptionAsync(int versionId, Exception exception, string errorCode);
    }
}
