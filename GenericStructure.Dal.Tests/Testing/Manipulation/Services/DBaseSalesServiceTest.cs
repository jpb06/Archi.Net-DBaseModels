using GenericStructure.Dal.Exceptions.Custom;
using GenericStructure.Dal.Manipulation.Services;
using GenericStructure.Dal.Manipulation.Services.Configuration;
using GenericStructure.Dal.Models;
using GenericStructure.Dal.Tests.Data.Database;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Tests.Testing.Manipulation.Services
{
    [TestFixture]
    public class DBaseSalesServiceTest
    {
        private ConsolidatedDataSet dataSet;
        private Article article;

        public DBaseSalesServiceTest()
        {
            this.dataSet = new ConsolidatedDataSet();
            this.article = new Article
            {
                IdCategory = 1,
                Title = "Title",
                Description = "Description",
                ImagesPath = Guid.NewGuid(),
                Price = 100m
            };
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
        public void Db_Service_CreateArticle()
        {
            using (SalesService service = new SalesService())
            {
                int result = service.Create(this.article);
                this.article.Id = result;

                Assert.Greater(this.article.Id, 0);
            }
        }

        [Test, Order(2)]
        public void Db_Service_GetArticleById()
        {
            using (SalesService service = new SalesService())
            {
                Article article = service.GetById<Article>(this.article.Id);

                Assert.IsNotNull(article);
                Assert.AreEqual(this.article.Title, article.Title);
                Assert.AreEqual(this.article.Description, article.Description);
            }
        }

        [Test, Order(3)]
        public void Db_Service_UpdateArticle()
        {
            string newTitle = "New Title";
            this.article.Title = newTitle;

            using (SalesService service = new SalesService())
            {
                Assert.That(() =>
                {
                    service.Modify(this.article);
                }, Throws.Nothing);
            }
        }

        [Test, Order(4)]
        public void Db_Service_DeleteArticle()
        {
            using (SalesService service = new SalesService())
            {
                Assert.That(() =>
                {
                    service.Delete(this.article);
                }, Throws.Nothing);
            }
        }

        [Test]
        public void Db_Concurrency_NoPolicy()
        {
            using (SalesService service1 = new SalesService())
            using (SalesService service2 = new SalesService(DataConflictPolicy.NoPolicy))
            {
                var article1 = service1.GetById<Article>(this.dataSet.ArticlesIds.First());
                article1.Description = "User1 Description 1";
                var article2 = service2.GetById<Article>(this.dataSet.ArticlesIds.First());
                article2.Description = "User2 Description 1";

                service1.Modify(article1);

                //Assert.That(() => 
                //{
                //    service2.Modify(article2);
                //}, Throws.Exception.TypeOf<DbUpdateConcurrencyException>());

                Assert.Throws<DbUpdateConcurrencyException>(() =>
                {
                    service2.Modify(article2);
                });
            }
        }

        [Test]
        public void Db_Concurrency_ClientWins()
        {
            using (SalesService service1 = new SalesService())
            using (SalesService service2 = new SalesService(DataConflictPolicy.ClientWins))
            {
                var article1 = service1.GetById<Article>(this.dataSet.ArticlesIds.ElementAt(1));
                article1.Description = "User1 Description 2";

                service1.Modify(article1);

                var article2 = service2.GetById<Article>(this.dataSet.ArticlesIds.ElementAt(1));
                article2.Description = "User2 Description 2";

                Assert.That(() =>
                {
                    service2.Modify(article2);
                }, Throws.Nothing);

                var updatedArticle = service2.GetById<Article>(this.dataSet.ArticlesIds.ElementAt(1));

                Assert.AreEqual("User2 Description 2", updatedArticle.Description);
            }
        }

        [Test]
        public void Db_Concurrency_DatabaseWins()
        {
            using (SalesService service1 = new SalesService())
            using (SalesService service2 = new SalesService(DataConflictPolicy.DatabaseWins))
            {
                var article1 = service1.GetById<Article>(this.dataSet.ArticlesIds.ElementAt(2));
                article1.Description = "User1 Description 3";
                var article2 = service2.GetById<Article>(this.dataSet.ArticlesIds.ElementAt(2));
                article2.Description = "User2 Description 3";

                service1.Modify(article1);

                Assert.That(() =>
                {
                    service2.Modify(article2);
                }, Throws.Nothing);

                var updatedArticle = service2.GetById<Article>(this.dataSet.ArticlesIds.ElementAt(2));

                Assert.AreEqual("User1 Description 3", updatedArticle.Description);
            }
        }

        [Test]
        public void Db_Concurrency_AskClient()
        {
            using (SalesService service1 = new SalesService())
            using (SalesService service2 = new SalesService(DataConflictPolicy.AskClient))
            {
                var article1 = service1.GetById<Article>(this.dataSet.ArticlesIds.ElementAt(3));
                article1.Description = "User1 Description 4";
                var article2 = service2.GetById<Article>(this.dataSet.ArticlesIds.ElementAt(3));
                article2.Description = "User2 Description 4";

                service1.Modify(article1);

                DataConflictException dce = Assert.Throws<DataConflictException>(() =>
                {
                    service2.Modify(article2);
                });

                Assert.IsInstanceOf(typeof(Article), dce.CurrentValues);
                Assert.IsInstanceOf(typeof(Article), dce.DatabaseValues);

                Article currentValues = (Article)dce.CurrentValues;
                Article databaseValues = (Article)dce.DatabaseValues;

                Assert.AreEqual(databaseValues.Id, article1.Id);
                Assert.AreEqual(databaseValues.Description, article1.Description);

                Assert.AreEqual(currentValues.Id, article2.Id);
                Assert.AreEqual(currentValues.Description, article2.Description);
            }
        }
    }
}
