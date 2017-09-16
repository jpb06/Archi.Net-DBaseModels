using GenericStructure.DataAccessLayer.Manipulation.Services;
using GenericStructure.DataAccessLayer.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.DataAccessLayer.Tests.Testing.Database
{
    [TestFixture]
    public class ArticlesTest
    {
        private Article article = new Article
        {
            IdCategory = 1,
            Title = "Article 7",
            Description = "Description 7",
            ImagesPath = Guid.NewGuid(),
            Price = 100m
        };

        [Test, Order(1)]
        public void Db_AddArticle()
        {
            using (SalesService ss = new SalesService())
            {
                ss.ArticleRepository.Insert(this.article);
                int res = ss.Save();

                Assert.AreEqual(1, res);
            }
        }

        [Test, Order(2)]
        public void Db_UpdateArticle() 
        {
            using (SalesService ss = new SalesService())
            {
                this.article.Title = "Article 7 updated";
                ss.ArticleRepository.Update(article);

                int res = ss.Save();

                Assert.AreEqual(1, res);
            }
        }

        [Test, Order(3)]
        public void Db_GetArticleById()
        {
            using (SalesService ss = new SalesService())
            {
                Article a = ss.ArticleRepository.GetByID(this.article.Id);

                Assert.IsNotNull(a);
                Assert.AreEqual(this.article.Title, a.Title);
                Assert.AreEqual(this.article.Description, a.Description);
                Assert.AreEqual(this.article.RowVersion, a.RowVersion);
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
                ss.ArticleRepository.Delete(this.article);

                int res = ss.Save();

                Assert.AreEqual(1, res);
            }
        }
    }
}
