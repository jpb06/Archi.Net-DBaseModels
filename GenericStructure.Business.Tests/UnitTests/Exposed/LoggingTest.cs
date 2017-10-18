using GenericStructure.Business.Exposed;
using GenericStructure.Business.Tests.AssemblyInformation;
using GenericStructure.Business.Tests.Exceptions;
using GenericStructure.Dal.Models.ErrorsReporting;
using GenericStructure.Shared.Tests.Data.Database;
using GenericStructure.Shared.Tests.Data.Database.Primitives;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
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
        public void Save() 
        {
            int a = 2, b = 0;

            try
            {
                int c = a / b;
            }
            catch (Exception exception)
            {
                AssemblyName assemblyName = AssemblyHelper.AssemblyName;
                Logging.Save(exception, assemblyName, TestErrorType.DivideByZero);

                ErrorReportException savedException = this.exceptionsSqlHelper.GetBy(assemblyName.Name, assemblyName.Version.ToString());

                Assert.IsNotNull(savedException);

                this.exceptionsSqlHelper.Delete(savedException.Id);
            }
        }
    }
}
