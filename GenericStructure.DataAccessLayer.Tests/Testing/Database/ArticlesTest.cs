using GenericStructure.Dal.Context;
using GenericStructure.Dal.Manipulation.Services;
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
    public class ArticlesTest
    {
        private ConsolidatedDataSet dataSet;
        private Article addArticle;

        public ArticlesTest() 
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

            using (SalesService ss = new SalesService())
            {
                ss.ArticleRepository.Insert(this.addArticle);
                int result = ss.Save();

                Assert.AreEqual(1, result);
            }
        }

        [Test, Order(2)]
        public void Db_UpdateArticle() 
        {
            using (SalesService ss = new SalesService())
            {
                this.addArticle.Title = "Article 4 updated";
                ss.ArticleRepository.Update(this.addArticle);

                int result = ss.Save();

                Assert.AreEqual(1, result);
            }
        }

        [Test, Order(3)]
        public void Db_GetArticleById()
        {
            using (SalesService ss = new SalesService())
            {
                Article a = ss.ArticleRepository.GetByID(this.addArticle.Id);

                Assert.IsNotNull(a);
                Assert.AreEqual(this.addArticle.Title, a.Title);
                Assert.AreEqual(this.addArticle.Description, a.Description);
                Assert.AreEqual(this.addArticle.RowVersion, a.RowVersion);
            }
        }

        [Test]
        public void Db_GetArticleById_DoesntExist()
        {
            using (SalesService ss = new SalesService())
            {
                Article article = ss.ArticleRepository.GetByID(0);

                Assert.AreEqual(null, article);
            }
        }

        [Test, Order(4)]
        public void Db_DeleteArticle() 
        {
            using (SalesService ss = new SalesService()) 
            {
                ss.ArticleRepository.Delete(this.addArticle);

                int result = ss.Save();

                Assert.AreEqual(1, result);
            }
        }

        [Test]
        public void Db_Concurrency()
        {
            using (SalesService ss = new SalesService())
            {
                var article = ss.ArticleRepository.GetByID(this.dataSet.ArticlesIds.First());
                article.Description = "a";

                using (SqlConnection connection = new SqlConnection(DatabaseConfiguration.ConnectionString))
                {
                    connection.Open();
                    Articles a = new Articles(connection);
                    a.ModifyTitle(this.dataSet.ArticlesIds.First(), "b");
                }

                int result = 0;

                //Assert.That(() => result = ss.Save(),
                //            Throws.Exception.TypeOf<DbUpdateConcurrencyException>());

                Assert.Throws<DbUpdateConcurrencyException>(() => { result = ss.Save(); });
                Assert.AreEqual(0, result);
            }
        }
    }
}
