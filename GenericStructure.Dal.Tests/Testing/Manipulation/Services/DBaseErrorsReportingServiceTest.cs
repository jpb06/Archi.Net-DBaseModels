using GenericStructure.Dal.Context.Contracts;
using GenericStructure.Dal.Context.EndObjects;
using GenericStructure.Dal.Exceptions;
using GenericStructure.Dal.Exceptions.CustomTypes;
using GenericStructure.Dal.Manipulation.Repositories.Contracts;
using GenericStructure.Dal.Manipulation.Repositories.Implementation.Base;
using GenericStructure.Dal.Manipulation.Services.ErrorsReporting;
using GenericStructure.Dal.Models.ErrorsReporting;
using GenericStructure.Shared.Tests.Data.Database;
using GenericStructure.Shared.Tests.Data.Database.DataSets;
using GenericStructure.Shared.Tests.Data.Database.Primitives;
using GenericStructure.Shared.Tests.Exceptions;
using NUnit.Framework;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Tests.Testing.Manipulation.Services
{
    [TestFixture]
    public class DBaseErrorsReportingServiceTest
    {
        private PersistentErrorsReportingDataSet dataSet;
        private SqlConnection connection;
        private ExceptionsSqlHelper exceptionsSqlHelper;

        private static readonly Container container = new Container();

        public DBaseErrorsReportingServiceTest()
        {
            this.dataSet = new PersistentErrorsReportingDataSet();

            this.connection = new SqlConnection(DatabaseConfiguration.ErrorsReportingConnectionString);
            this.exceptionsSqlHelper = new ExceptionsSqlHelper(this.connection);

            container.Options.DefaultScopedLifestyle = new ThreadScopedLifestyle();
            container.Register<IDbContext, ErrorsReportingContext>(Lifestyle.Scoped);
            container.Register<IGenericRepository<ErrorReportApplication>, GenericRepository<ErrorReportApplication>>(Lifestyle.Scoped);
            container.Register<IGenericRepository<ErrorReportException>, GenericRepository<ErrorReportException>>(Lifestyle.Scoped);
            container.Register<ErrorsReportingService>(Lifestyle.Scoped);
            container.Verify();

        }

        [OneTimeSetUp]
        public void Init()
        {
            this.dataSet.Initialize();
            this.connection.Open();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            this.connection.Close();
            this.dataSet.Destroy();
            this.dataSet.Dispose();
        }

        [Test]
        public void Db_ErrorsReportingService_CreateApplication()
        {
            using (ThreadScopedLifestyle.BeginScope(container))
            {
                ErrorsReportingService service = container.GetInstance<ErrorsReportingService>();

                ErrorReportApplication result = service.CreateApplication("TestApplication", "a.a.a.a");

                Assert.IsNotNull(result);
                Assert.Greater(result.Id, 0);

                this.dataSet.ApplicationsIds.Add(result.Id);
            }
        }

        [Test]
        public void Db_ErrorsReportingService_CreateApplication_AlreadyExists()
        {
            using (ThreadScopedLifestyle.BeginScope(container))
            {
                ErrorsReportingService service = container.GetInstance<ErrorsReportingService>();

                DalException ex = Assert.Throws<DalException>(() =>
                {
                    service.CreateApplication("TestApplicationAlreadyExisting", "a.a.a.a");
                });
                Assert.That(ex.errorType, Is.EqualTo(DalErrorType.SqlUniqueConstraintViolation));
            }
        }

        [Test]
        public void Db_ErrorsReportingService_GetApplication() 
        {
            using (ThreadScopedLifestyle.BeginScope(container))
            {
                ErrorsReportingService service = container.GetInstance<ErrorsReportingService>();

                ErrorReportApplication application = service.GetApplication("TestApplicationAlreadyExisting", "a.a.a.a");

                Assert.IsNotNull(application);
                Assert.Greater(application.Id, 0);
                Assert.AreEqual(new DateTime(2000, 1, 1), application.FirstRunDate);
            }
        }

        [Test]
        public void Db_ErrorsReportingService_GetApplication_NotExisting()
        {
            using (ThreadScopedLifestyle.BeginScope(container))
            {
                ErrorsReportingService service = container.GetInstance<ErrorsReportingService>();

                ErrorReportApplication application = null;
                Assert.That(() =>
                {
                    application = service.GetApplication("TestApplicationAlreadyExisting", "z.z.z.z");
                }, Throws.Nothing);

                Assert.IsNull(application);
            }
        }

        [Test]
        public void Db_ErrorsReportingService_LogException() 
        {
            using (ThreadScopedLifestyle.BeginScope(container))
            {
                ErrorsReportingService service = container.GetInstance<ErrorsReportingService>();

                try 
                {
                    ExceptionGenerator.ThrowsOne();
                }
                catch (Exception exception) 
                {
                    int? id = null;
                    Assert.That(() =>
                    {
                        id = service.LogException(this.dataSet.ApplicationsIds.ElementAt(0), exception, "ErrorType.Specific");
                    }, Throws.Nothing);

                    Assert.IsNotNull(id);

                    ErrorReportException ex = this.exceptionsSqlHelper.GetBy(id.Value);

                    Assert.AreEqual("One", ex.Message);
                }
            }
        }

        [Test]
        public void Db_ErrorsReportingService_LogException_WithInner()
        {
            using (ThreadScopedLifestyle.BeginScope(container))
            {
                ErrorsReportingService service = container.GetInstance<ErrorsReportingService>();

                try
                {
                    ExceptionGenerator.ThrowsTwo();
                }
                catch (Exception exception)
                {
                    int? id = null;
                    Assert.That(() =>
                    {
                        id = service.LogException(this.dataSet.ApplicationsIds.ElementAt(0), exception, "ErrorType.Specific");
                    }, Throws.Nothing);

                    Assert.IsNotNull(id);

                    ErrorReportException ex = this.exceptionsSqlHelper.GetBy(id.Value);
                    ErrorReportException innerEx = this.exceptionsSqlHelper.GetBy(ex.IdInnerException.Value);

                    Assert.AreEqual("Two", ex.Message);
                    Assert.AreEqual("One", innerEx.Message);
                }
            }
        }

        #region Async
        [Test]
        public async Task Db_ErrorsReportingService_CreateApplicationAsync()
        {
            using (ThreadScopedLifestyle.BeginScope(container))
            {
                ErrorsReportingService service = container.GetInstance<ErrorsReportingService>();

                ErrorReportApplication result = await service.CreateApplicationAsync("TestApplicationAsync", "a.a.a.a");

                Assert.IsNotNull(result);
                Assert.Greater(result.Id, 0);

                this.dataSet.ApplicationsIds.Add(result.Id);
            }
        }

        [Test]
        public void Db_ErrorsReportingService_CreateApplicationAsync_AlreadyExists()
        {
            using (ThreadScopedLifestyle.BeginScope(container))
            {
                ErrorsReportingService service = container.GetInstance<ErrorsReportingService>();

                DalException ex = Assert.ThrowsAsync<DalException>(async () =>
                {
                    await service.CreateApplicationAsync("TestApplicationAlreadyExisting", "a.a.a.a");
                });
                Assert.That(ex.errorType, Is.EqualTo(DalErrorType.SqlUniqueConstraintViolation));
            }
        }

        [Test]
        public async Task Db_ErrorsReportingService_GetApplicationAsync()
        {
            using (ThreadScopedLifestyle.BeginScope(container))
            {
                ErrorsReportingService service = container.GetInstance<ErrorsReportingService>();

                ErrorReportApplication application = await service.GetApplicationAsync("TestApplicationAlreadyExisting", "a.a.a.a");

                Assert.IsNotNull(application);
                Assert.Greater(application.Id, 0);
                Assert.AreEqual(new DateTime(2000, 1, 1), application.FirstRunDate);
            }
        }

        [Test]
        public void Db_ErrorsReportingService_GetApplicationAsync_NotExisting()
        {
            using (ThreadScopedLifestyle.BeginScope(container))
            {
                ErrorsReportingService service = container.GetInstance<ErrorsReportingService>();

                ErrorReportApplication application = null;
                Assert.That(async () =>
                {
                    application = await service.GetApplicationAsync("TestApplicationAlreadyExisting", "z.z.z.z");
                }, Throws.Nothing);

                Assert.IsNull(application);
            }
        }

        [Test]
        public void Db_ErrorsReportingService_LogExceptionAsync()
        {
            using (ThreadScopedLifestyle.BeginScope(container))
            {
                ErrorsReportingService service = container.GetInstance<ErrorsReportingService>();

                try
                {
                    ExceptionGenerator.ThrowsOne();
                }
                catch (Exception exception)
                {
                    int? id = null;
                    Assert.That(async () =>
                    {
                        id = await service.LogExceptionAsync(this.dataSet.ApplicationsIds.ElementAt(0), exception, "ErrorType.Specific");
                    }, Throws.Nothing);

                    Assert.IsNotNull(id);

                    ErrorReportException ex = this.exceptionsSqlHelper.GetBy(id.Value);

                    Assert.AreEqual("One", ex.Message);
                }
            }
        }

        [Test]
        public void Db_ErrorsReportingService_LogExceptionAsync_WithInner()
        {
            using (ThreadScopedLifestyle.BeginScope(container))
            {
                ErrorsReportingService service = container.GetInstance<ErrorsReportingService>();

                try
                {
                    ExceptionGenerator.ThrowsTwo();
                }
                catch (Exception exception)
                {
                    int? id = null;
                    Assert.That(async () =>
                    {
                        id = await service.LogExceptionAsync(this.dataSet.ApplicationsIds.ElementAt(0), exception, "ErrorType.Specific");
                    }, Throws.Nothing);

                    Assert.IsNotNull(id);

                    ErrorReportException ex = this.exceptionsSqlHelper.GetBy(id.Value);
                    ErrorReportException innerEx = this.exceptionsSqlHelper.GetBy(ex.IdInnerException.Value);

                    Assert.AreEqual("Two", ex.Message);
                    Assert.AreEqual("One", innerEx.Message);
                }
            }
        }

        #endregion
    }
}
