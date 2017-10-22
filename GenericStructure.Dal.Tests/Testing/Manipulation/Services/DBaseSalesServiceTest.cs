using GenericStructure.Dal.Context.Contracts;
using GenericStructure.Dal.Context.EndObjects;
using GenericStructure.Dal.Exceptions;
using GenericStructure.Dal.Exceptions.Custom.Specific;
using GenericStructure.Dal.Exceptions.CustomTypes;
using GenericStructure.Dal.Manipulation.Repositories.Contracts;
using GenericStructure.Dal.Manipulation.Repositories.Implementation.Specific;
using GenericStructure.Dal.Manipulation.Services.CoreBusiness;
using GenericStructure.Dal.Manipulation.Services.CoreBusiness.Configuration;
using GenericStructure.Models.CoreBusiness;
using GenericStructure.Shared.Tests.Data.Database;
using GenericStructure.Shared.Tests.Data.Database.DataSets;
using GenericStructure.Shared.Tests.Data.Database.Primitives;
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
    public class DBaseSalesServiceTest
    {
        private PersistentCoreBusinessDataSet dataSet;
        private SqlConnection connection;
        private ArticlesSqlHelper articlesSqlHelper;
        private Article article;

        private static readonly Container container = new Container();

        public DBaseSalesServiceTest()
        {
            this.dataSet = new PersistentCoreBusinessDataSet();

            this.connection = new SqlConnection(DatabaseConfiguration.CoreBusinessConnectionString);
            this.articlesSqlHelper = new ArticlesSqlHelper(this.connection);

            this.article = new Article
            {
                IdCategory = 1,
                Title = "Title",
                Description = "Description",
                ImagesPath = Guid.NewGuid(),
                Price = 100m
            };

            container.Options.DefaultScopedLifestyle = new ThreadScopedLifestyle();
            container.Register<ICoreBusinessContext, CoreBusinessTestContext>(Lifestyle.Scoped);
            container.Register<IArticlesRepository, ArticlesRepository>(Lifestyle.Scoped);
            container.Register<ICategoriesRepository, CategoriesRepository>(Lifestyle.Scoped);
            container.Register<SalesService>(Lifestyle.Scoped);
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
        public async Task Db_SalesService_Concurrency_NoPolicy()
        {
            using (ThreadScopedLifestyle.BeginScope(container))
            {
                SalesService service = container.GetInstance<SalesService>();
                service.SetPolicy(DataConflictPolicy.NoPolicy);

                var article = await service.GetByIdAsync<Article>(this.dataSet.ArticlesIds.First());
                article.Title = "User1 Title 1";

                this.articlesSqlHelper.ModifyTitle(article.Id, "User2 Title 1");

                DalException ex = Assert.ThrowsAsync<DalException>(async () =>
                {
                    await service.ModifyAsync(article);
                });
                Assert.That(ex.errorType, Is.EqualTo(DalErrorType.BaseServiceDataConflictWithNoPolicy));
            }
        }

        [Test]
        public async Task Db_SalesService_Concurrency_ClientWins()
        {
            using (ThreadScopedLifestyle.BeginScope(container))
            {
                SalesService service = container.GetInstance<SalesService>();
                service.SetPolicy(DataConflictPolicy.ClientWins);

                var article = await service.GetByIdAsync<Article>(this.dataSet.ArticlesIds.ElementAt(1));
                article.Description = "User1 Title 2";

                this.articlesSqlHelper.ModifyTitle(article.Id, "User2 Title 2");

                Assert.That(async () =>
                {
                    await service.ModifyAsync(article);
                }, Throws.Nothing);

                var updatedArticle = await service.GetByIdAsync<Article>(this.dataSet.ArticlesIds.ElementAt(1));

                Assert.AreEqual("User1 Title 2", updatedArticle.Description);
            }
        }

        [Test]
        public async Task Db_SalesService_Concurrency_DatabaseWins()
        {
            using (ThreadScopedLifestyle.BeginScope(container))
            {
                SalesService service = container.GetInstance<SalesService>();
                service.SetPolicy(DataConflictPolicy.DatabaseWins);

                var article = await service.GetByIdAsync<Article>(this.dataSet.ArticlesIds.ElementAt(2));
                article.Description = "User1 Title 3";

                this.articlesSqlHelper.ModifyTitle(article.Id, "User2 Title 3");

                Assert.That(async () =>
                {
                    await service.ModifyAsync(article);
                }, Throws.Nothing);

                var updatedArticle = await service.GetByIdAsync<Article>(this.dataSet.ArticlesIds.ElementAt(2));

                Assert.AreEqual("User2 Title 3", updatedArticle.Title);
            }
        }

        [Test]
        public async Task Db_SalesService_Concurrency_AskClient()
        {
            using (ThreadScopedLifestyle.BeginScope(container))
            {
                SalesService service = container.GetInstance<SalesService>();
                service.SetPolicy(DataConflictPolicy.AskClient);

                var article = await service.GetByIdAsync<Article>(this.dataSet.ArticlesIds.ElementAt(3));
                article.Title = "User1 Title 4";

                this.articlesSqlHelper.ModifyTitle(article.Id, "User2 Title 4");

                DataConflictException dce = Assert.ThrowsAsync<DataConflictException>(async () =>
                {
                    await service.ModifyAsync(article);
                });
                Assert.AreEqual(DalErrorType.BaseServiceDataConflictWithAskClientPolicy, dce.errorType);

                Assert.IsInstanceOf(typeof(Article), dce.CurrentValues);
                Assert.IsInstanceOf(typeof(Article), dce.DatabaseValues);

                Article currentValues = (Article)dce.CurrentValues;
                Article databaseValues = (Article)dce.DatabaseValues;

                Assert.AreEqual(article.Id, databaseValues.Id);
                Assert.AreEqual("User2 Title 4", databaseValues.Title);

                Assert.AreEqual(article.Id, currentValues.Id);
                Assert.AreEqual("User1 Title 4", currentValues.Title);
            }
        }

        #region async
        [Test, Order(5)]
        public async Task Db_SalesService_CreateArticleAsync()
        {
            using (ThreadScopedLifestyle.BeginScope(container))
            {
                SalesService service = container.GetInstance<SalesService>();

                this.article.Id = 0;
                int result = await service.CreateAsync(this.article);

                Assert.Greater(this.article.Id, 0);
            }
        }

        [Test, Order(6)]
        public void Db_SalesService_UpdateArticleAsync()
        {
            string newTitle = "New Title";
            this.article.Title = newTitle;

            using (ThreadScopedLifestyle.BeginScope(container))
            {
                SalesService service = container.GetInstance<SalesService>();

                Assert.That(async () =>
                {
                    await service.ModifyAsync(this.article);
                }, Throws.Nothing);
            }
        }

        [Test, Order(7)]
        public void Db_SalesService_DeleteArticleAsync()
        {
            using (ThreadScopedLifestyle.BeginScope(container))
            {
                SalesService service = container.GetInstance<SalesService>();

                Assert.That(async () =>
                {
                    await service.DeleteAsync(this.article);
                }, Throws.Nothing);
            }
        }
        #endregion
    }
}
