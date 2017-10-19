using GenericStructure.Dal.Context.Contracts;
using GenericStructure.Dal.Manipulation.Repositories.Contracts;
using GenericStructure.Dal.Manipulation.Services.Base;
using GenericStructure.Dal.Manipulation.Services.CoreBusiness.Configuration;
using GenericStructure.Dal.Manipulation.Services.ErrorsReporting.Contracts;
using GenericStructure.Dal.Models.ErrorsReporting;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
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

        

        public ErrorReportApplication CreateApplication(string name, string version)
        {
            ErrorReportApplication application = new ErrorReportApplication
            {
                Name = name, 
                Version = version, 
                FirstRunDate = DateTime.Now
            };

            this.applicationsRepository.Insert(application);
            SaveResult result = base.Save(base.policy);

            return result.AlteredObjectsCount == 1 ? application : null;
        }

        public ErrorReportApplication GetApplication(string name, string version)
        {
            ErrorReportApplication application = this.applicationsRepository
                                                     .Get(el => el.Name == name && el.Version == version)
                                                     .SingleOrDefault();
            return application;
        }

        public int? LogException(int idApplication, Exception exception, string errorCode)
        {
            if (exception == null) return null;

            var exceptionModel = new ErrorReportException();
            exceptionModel.IdApplication = idApplication;
            exceptionModel.Type = exception.GetType().ToString();
            exceptionModel.Message = exception.Message;
            exceptionModel.Source = exception.Source;
            if (exception.TargetSite != null && exception.TargetSite.Module != null)
                exceptionModel.SiteModule = exception.TargetSite.Module.Name;
            exceptionModel.SiteName = exception.TargetSite.Name;
            exceptionModel.StackTrace = exception.StackTrace;
            exceptionModel.HelpLink = exception.HelpLink;
            exceptionModel.Date = DateTime.Now;
            exceptionModel.IdInnerException = this.LogException(idApplication, exception.InnerException, errorCode);

            exceptionModel.CustomErrorType = errorCode;

            this.exceptionRepository.Insert(exceptionModel);

            SaveResult result = base.Save(base.policy);

            return result.AlteredObjectsCount == 1 ? exceptionModel.Id : (int?)null;
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

            var exceptionModel = new ErrorReportException();
            exceptionModel.IdApplication = idApplication;
            exceptionModel.Type = exception.GetType().ToString();
            exceptionModel.Message = exception.Message;
            exceptionModel.Source = exception.Source;
            if (exception.TargetSite != null && exception.TargetSite.Module != null)
                exceptionModel.SiteModule = exception.TargetSite.Module.Name;
            exceptionModel.SiteName = exception.TargetSite.Name;
            exceptionModel.StackTrace = exception.StackTrace;
            exceptionModel.HelpLink = exception.HelpLink;
            exceptionModel.Date = DateTime.Now;
            exceptionModel.IdInnerException = await this.LogExceptionAsync(idApplication, exception.InnerException, errorCode);

            exceptionModel.CustomErrorType = errorCode;

            this.exceptionRepository.Insert(exceptionModel);

            SaveResult result = await base.SaveAsync(base.policy);

            return result.AlteredObjectsCount == 1 ? exceptionModel.Id : (int?)null;
        }
        #endregion
    }
}
