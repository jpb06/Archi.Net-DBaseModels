using GenericStructure.Dal.Context.Contracts;
using GenericStructure.Dal.Manipulation.Repositories.Contracts;
using GenericStructure.Dal.Manipulation.Services.Base;
using GenericStructure.Dal.Manipulation.Services.CoreBusiness.Configuration;
using GenericStructure.Dal.Manipulation.Services.ErrorsReporting.Contracts;
using GenericStructure.Models.ErrorsReporting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Manipulation.Services.ErrorsReporting
{
    public class ErrorsReportingService : BaseService, IErrorsReportingService
    {
        private IGenericRepository<ErrorReportApplication> applicationsRepository;
        private IGenericRepository<ErrorReportException> exceptionRepository;

        public ErrorsReportingService(IDbContext context,
                                      IGenericRepository<ErrorReportApplication> applicationsRespository,
                                      IGenericRepository<ErrorReportException> exceptionsRespository) 
            : base(context)
        {
            base.policy = DataConflictPolicy.DatabaseWins;
            base.context = context;

            this.applicationsRepository = applicationsRespository;
            this.exceptionRepository = exceptionsRespository;
        }

        #region Async
        public async Task<ErrorReportApplication> CreateApplicationAsync(string name, string version)
        {
            ErrorReportApplication application = new ErrorReportApplication
            {
                Name = name,
                Version = version,
                FirstRunDate = DateTime.Now
            };

            this.applicationsRepository.Insert(application);
            SaveResult result = await base.SaveAsync(base.policy);

            return result.AlteredObjectsCount == 1 ? application : null;
        }

        public async Task<ErrorReportApplication> GetApplicationAsync(string name, string version)
        {
            var result = await this.applicationsRepository
                                   .GetAsync(el => el.Name == name && el.Version == version);

            return result.SingleOrDefault();
        }

        public async Task<int?> LogExceptionAsync(int idApplication, Exception exception, string errorCode)
        {
            if (exception == null) return null;

            var exceptionModel = new ErrorReportException
            {
                IdApplication = idApplication,
                Type = exception.GetType().ToString(),
                Message = exception.Message,
                Source = exception.Source,
                SiteName = exception.TargetSite.Name,
                StackTrace = exception.StackTrace,
                HelpLink = exception.HelpLink,
                SiteModule = exception?.TargetSite?.Module.Name,
                Date = DateTime.Now,
                CustomErrorType = errorCode,
               
                IdInnerException = await this.LogExceptionAsync(idApplication, exception.InnerException, errorCode),
            };

            this.exceptionRepository.Insert(exceptionModel);

            SaveResult result = await base.SaveAsync(base.policy);

            return result.AlteredObjectsCount == 1 ? exceptionModel.Id : (int?)null;
        }
        #endregion
    }
}
