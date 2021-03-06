﻿using GenericStructure.Business.Internal.Contracts;
using GenericStructure.Dal.Manipulation.Services.ErrorsReporting.Contracts;
using GenericStructure.Models.ErrorsReporting;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace GenericStructure.Business.Internal
{
    internal class ErrorsReportingManager : IErrorsReportingManager
    {
        private IErrorsReportingService reportingService;

        public ErrorsReportingManager(IErrorsReportingService reportingService) => this.reportingService = reportingService;

        public async Task LogErrorAsync(Exception exception, AssemblyName assemblyName, string errorCode)
        {
            string applicationName = assemblyName.Name;
            string applicationVersion = assemblyName.Version.ToString();

            ErrorReportApplication application = await this.reportingService.GetApplicationAsync(applicationName, applicationVersion);
            if (application == null)
                application = await this.reportingService.CreateApplicationAsync(applicationName, applicationVersion);

            await this.reportingService.LogExceptionAsync(application.Id, exception, errorCode);
        }

    }
}
