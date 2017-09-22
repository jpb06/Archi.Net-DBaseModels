using GenericStructure.Dal.Context;
using GenericStructure.Dal.Manipulation.Services;
using GenericStructure.Dal.Manipulation.Services.Base;
using GenericStructure.Dal.Manipulation.Services.Configuration;
using GenericStructure.Dal.Models;
using GenericStructure.Dal.Tests.Data.Database;
using GenericStructure.Dal.Tests.Data.Database.Primitives;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Tests.Testing.Database
{
    [TestFixture]
    public class DBaseArticlesTest
    {
        private ConsolidatedDataSet dataSet;
        private Article addArticle;

        public DBaseArticlesTest() 
        {
            this.dataSet = new ConsolidatedDataSet();
        }

        [OneTimeSetUp]
        public void Init()
        {
            this.dataSet.Initialize();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            this.dataSet.Destroy();
            this.dataSet.Dispose();
        }

        [Test, Order(1)]
        public void Db_AddArticle()
        {
            this.addArticle = new Article
            {
                IdCategory = this.dataSet.CategoriesIds.ElementAt(0),
                Title = "Test Article 4",
                Description = "Description 4",
                ImagesPath = Guid.NewGuid(),
                Price = 1000.0m
            };

            using (SalesService service = new SalesService())
            {
                service.ArticleRepository.Insert(this.addArticle);
                var result = service.Save();

                Assert.AreEqual(1, result.AffectedObjectsCount);
            }
        }

        [Test, Order(2)]
        public void Db_UpdateArticle() 
        {
            using (SalesService service = new SalesService())
            {
                this.addArticle.Title = "Article 4 updated";
                service.ArticleRepository.Update(this.addArticle);

                var result = service.Save();

                Assert.AreEqual(1, result.AffectedObjectsCount);
            }
        }

        [Test, Order(3)]
        public void Db_GetArticleById()
        {
            using (SalesService service = new SalesService())
            {
                Article article = service.ArticleRepository.GetByID(this.addArticle.Id);

                Assert.IsNotNull(article);
                Assert.AreEqual(this.addArticle.Title, article.Title);
                Assert.AreEqual(this.addArticle.Description, article.Description);
                Assert.AreEqual(this.addArticle.RowVersion, article.RowVersion);
            }
        }

        [Test]
        public void Db_GetArticleById_DoesntExist()
        {
            using (SalesService service = new SalesService())
            {
                Article article = service.ArticleRepository.GetByID(0);

                Assert.AreEqual(null, article);
            }
        }

        [Test, Order(4)]
        public void Db_DeleteArticle() 
        {
            using (SalesService service = new SalesService()) 
            {
                service.ArticleRepository.Delete(this.addArticle);

                var result = service.Save();

                Assert.AreEqual(1, result.AffectedObjectsCount);
            }
        }

        [Test]
        public void Db_Concurrency_NoPolicy()
        {
            using(SalesService service1 = new SalesService())
            using (SalesService service2 = new SalesService()) 
            {
                var article1 = service1.ArticleRepository.GetByID(this.dataSet.ArticlesIds.First());
                article1.Description = "User1 Description 1";

                var article2 = service2.ArticleRepository.GetByID(this.dataSet.ArticlesIds.First());
                article2.Description = "User2 Description 1";


                service1.ArticleRepository.Update(article1);
                service1.Save();

                service2.ArticleRepository.Update(article2);

                SaveResult result = null;

                //Assert.That(() => result = ss.Save(),
                //            Throws.Exception.TypeOf<DbUpdateConcurrencyException>());

                Assert.Throws<DbUpdateConcurrencyException>(() => { result = service2.Save(OptimisticConcurrencyPolicy.NoPolicy); });
                Assert.AreEqual(null, result);
            }
        }

        [Test]
        public void Db_Concurrency_ClientWins()
        {
            using(SalesService service1 = new SalesService())
            using (SalesService service2 = new SalesService())
            {
                var article1 = service1.ArticleRepository.GetByID(this.dataSet.ArticlesIds.ElementAt(1));
                article1.Description = "User1 Description 2";

                var article2 = service2.ArticleRepository.GetByID(this.dataSet.ArticlesIds.ElementAt(1));
                article2.Description = "User2 Description 2";

                service1.ArticleRepository.Update(article1);
                service1.Save();

                service2.ArticleRepository.Update(article2);
                SaveResult result = service2.Save(OptimisticConcurrencyPolicy.ClientWins);

                Assert.AreEqual(1, result.AffectedObjectsCount);
                Assert.AreEqual(null, result.Feedback);
                Assert.AreEqual("User2 Description 2", article2.Description);
            }
        }

        [Test]
        public void Db_Concurrency_DatabaseWins()
        {
            using(SalesService service1 = new SalesService())
            using (SalesService service2 = new SalesService())
            {
                var article1 = service1.ArticleRepository.GetByID(this.dataSet.ArticlesIds.ElementAt(2));
                article1.Description = "User1 Description 3";

                var article2 = service2.ArticleRepository.GetByID(this.dataSet.ArticlesIds.ElementAt(2));
                article2.Description = "User2 Description 3";


                service1.ArticleRepository.Update(article1);
                service1.Save();

                service2.ArticleRepository.Update(article2);
                SaveResult result = service2.Save(OptimisticConcurrencyPolicy.DatabaseWins);

                Assert.AreEqual(0, result.AffectedObjectsCount);
                Assert.IsNull(result.Feedback);
                Assert.AreEqual("User1 Description 3", article2.Description);
            }
        }

        [Test]
        public void Db_Concurrency_AskClient()
        {
            using (SalesService service1 = new SalesService())
            using (SalesService service2 = new SalesService())
            {
                var article1 = service1.ArticleRepository.GetByID(this.dataSet.ArticlesIds.ElementAt(3));
                article1.Description = "User1 Description 4";

                var article2 = service2.ArticleRepository.GetByID(this.dataSet.ArticlesIds.ElementAt(3));
                article2.Description = "User2 Description 4";


                service1.ArticleRepository.Update(article1);
                service1.Save();

                service2.ArticleRepository.Update(article2);
                SaveResult result = service2.Save(OptimisticConcurrencyPolicy.AskClient);

                Assert.AreEqual(0, result.AffectedObjectsCount);
                Assert.IsNotNull(result.Feedback);
                Assert.AreEqual("User1 Description 4", result.Feedback.DatabaseValues.GetValue<string>("Description"));
                Assert.AreEqual("User2 Description 4", result.Feedback.CurrentValues.GetValue<string>("Description"));
            }
        }
    }
}
