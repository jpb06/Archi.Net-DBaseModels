using GenericStructure.Dal.Models.ErrorsReporting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Context.Contracts
{
    public interface IErrorsReportingContext : IDbContext
    {
        IDbSet<ErrorReportApplication> Applications { get; set; }
        IDbSet<ErrorReportException> Exceptions { get; set; }
    }
}
