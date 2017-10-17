﻿using GenericStructure.Business.Internal.Contracts;
using GenericStructure.Dal.Manipulation.Services.ErrorsReporting.Contracts;
using GenericStructure.Dal.Models.ErrorsReporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Business.Internal
{
    internal class ErrorsReportingManager : IErrorsReportingManager
    {
        private IErrorsReportingService reportingService;

        public ErrorsReportingManager(IErrorsReportingService reportingService)
        {
            this.reportingService = reportingService;
        }

        public void LogError(Exception exception, AssemblyName assemblyName, string errorCode)
        {
            string applicationName = assemblyName.Name;
            string applicationVersion = assemblyName.Version.ToString();

            ErrorReportApplication application = this.reportingService.GetApplication(applicationName, applicationVersion);
            if (application == null)
                application = this.reportingService.CreateApplication(applicationName, applicationVersion);

            this.reportingService.LogException(application.Id, exception, errorCode);
        }
    }
}
