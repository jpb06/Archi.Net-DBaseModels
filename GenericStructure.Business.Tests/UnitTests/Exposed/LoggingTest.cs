using GenericStructure.Business.Exposed;
using GenericStructure.Business.InversionOfControl;
using GenericStructure.Business.Tests.AssemblyInformation;
using GenericStructure.Business.Tests.Exceptions;
using GenericStructure.Models.ErrorsReporting;
using GenericStructure.Shared.Tests.Data.Database;
using GenericStructure.Shared.Tests.Data.Database.Primitives;
using NUnit.Framework;
using System;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading.Tasks;

namespace GenericStructure.Business.Tests.UnitTests.Exposed
{
    [TestFixture]
    public class LoggingTest
    {
        private SqlConnection connection;
        private ExceptionsSqlHelper exceptionsSqlHelper;

        public LoggingTest() 
        {
            IoCConfiguration.Setup(true);

            this.connection = new SqlConnection(DatabaseConfiguration.ErrorsReportingConnectionString);
            this.exceptionsSqlHelper = new ExceptionsSqlHelper(this.connection);
        }

        [OneTimeSetUp]
        public void Init()
        {
            this.connection.Open();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            this.connection.Close();
        }

        [Test]
        public async Task SaveAsync()
        {
            int a = 2, b = 0;

            try
            {
                int c = a / b;
            }
            catch (Exception exception)
            {
                AssemblyName assemblyName = AssemblyHelper.AssemblyName;
                await Logging.SaveAsync(exception, assemblyName, TestErrorType.DivideByZero);

                ErrorReportException savedException = this.exceptionsSqlHelper.GetBy(assemblyName.Name, assemblyName.Version.ToString());

                Assert.IsNotNull(savedException);

                this.exceptionsSqlHelper.Delete(savedException.Id);
            }
        }
    }
}
