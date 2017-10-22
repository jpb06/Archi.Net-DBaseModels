using GenericStructure.Models.ErrorsReporting;
using System.Data.Entity;

namespace GenericStructure.Dal.Context.Contracts
{
    public interface IErrorsReportingContext : IDbContext
    {
        IDbSet<ErrorReportApplication> Applications { get; set; }
        IDbSet<ErrorReportException> Exceptions { get; set; }
    }
}
