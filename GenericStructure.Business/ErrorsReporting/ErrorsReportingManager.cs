using GenericStructure.Dal.Manipulation.Services.ErrorsReporting;
using GenericStructure.Dal.Manipulation.Services.ErrorsReporting.Contracts;
using GenericStructure.Dal.Models.ErrorsReporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Business.ErrorsReporting
{
    public class ErrorsReportingManager
    {
        private IErrorsReportingService service;

        public ErrorsReportingManager(IErrorsReportingService service) 
        {
            this.service = service;
        }

        public void LogError(Exception exception, AssemblyName AssemblyInfos, string errorCodeFullyQualifiedName) 
        {
            string applicationName = AssemblyInfos.Name;
            string applicationVersion = AssemblyInfos.Version.ToString();

            ErrorReportApplication application = this.service.GetApplication(applicationName, applicationVersion);
            if(application == null)
                application = this.service.CreateApplication(applicationName, applicationVersion);

            this.service.LogException(application.Id, exception);
        }
    }
}
