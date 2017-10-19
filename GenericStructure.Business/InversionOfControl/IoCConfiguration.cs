using GenericStructure.Business.Internal;
using GenericStructure.Business.Internal.Contracts;
using GenericStructure.Dal.Context.Contracts;
using GenericStructure.Dal.Context.EndObjects;
using GenericStructure.Dal.Manipulation.Repositories.Contracts;
using GenericStructure.Dal.Manipulation.Repositories.Implementation.Base;
using GenericStructure.Dal.Manipulation.Repositories.Implementation.Specific;
using GenericStructure.Dal.Manipulation.Services.CoreBusiness;
using GenericStructure.Dal.Manipulation.Services.CoreBusiness.Contracts;
using GenericStructure.Dal.Manipulation.Services.ErrorsReporting;
using GenericStructure.Dal.Manipulation.Services.ErrorsReporting.Contracts;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace GenericStructure.Business.InversionOfControl
{
    public static class IoCConfiguration
    {
        public static Container Container;

        public static void Setup(bool isTestRelated = false)
        {
            Container = new Container();

            Container.Options.DefaultScopedLifestyle = new ThreadScopedLifestyle();

            if (isTestRelated)
                TestRegistration();
            else
                ProductionRegistration();
            
            Container.Verify();
        }

        private static void ProductionRegistration() 
        {
            // Errors reporting
            Container.Register<IDbContext, ErrorsReportingContext>(Lifestyle.Scoped);
            Container.Register(typeof(IGenericRepository<>), typeof(GenericRepository<>), Lifestyle.Scoped);
            Container.Register<IErrorsReportingService, ErrorsReportingService>(Lifestyle.Scoped);
            Container.Register<IErrorsReportingManager, ErrorsReportingManager>(Lifestyle.Scoped);

            // Core business
            Container.Register<ICoreBusinessContext, CoreBusinessContext>(Lifestyle.Scoped);
            Container.Register<IArticlesRepository, ArticlesRepository>(Lifestyle.Scoped);
            Container.Register<ICategoriesRepository, CategoriesRepository>(Lifestyle.Scoped);
            Container.Register<ISalesService, SalesService>(Lifestyle.Scoped);
        }

        private static void TestRegistration() 
        {
            // Errors reporting
            Container.Register<IDbContext, ErrorsReportingContext>(Lifestyle.Scoped);
            Container.Register(typeof(IGenericRepository<>), typeof(GenericRepository<>), Lifestyle.Scoped);
            Container.Register<IErrorsReportingService, ErrorsReportingService>(Lifestyle.Scoped);
            Container.Register<IErrorsReportingManager, ErrorsReportingManager>(Lifestyle.Scoped);

            // Core business
            Container.Register<ICoreBusinessContext, CoreBusinessTestContext>(Lifestyle.Scoped);
            Container.Register<IArticlesRepository, ArticlesRepository>(Lifestyle.Scoped);
            Container.Register<ICategoriesRepository, CategoriesRepository>(Lifestyle.Scoped);
            Container.Register<ISalesService, SalesService>(Lifestyle.Scoped);
        }
    }
}
