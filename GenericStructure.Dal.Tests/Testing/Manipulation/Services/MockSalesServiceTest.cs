using GenericStructure.Dal.Manipulation.Services;
using GenericStructure.Dal.Manipulation.Services.Contracts;
using GenericStructure.Dal.Models;
using GenericStructure.Dal.Tests.Data.Mocked;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Tests.Testing.Manipulation.Services
{
    [TestFixture]
    public class MockSalesServiceTest
    {
        private ISalesService salesService;
        private Article articleToAdd;

        public MockSalesServiceTest() 
        {
            this.articleToAdd = new Article
            {
                IdCategory = 1, 
                Title = "Added title", 
                Description = "Added description", 
                Price = 100m, 
                ImagesPath = Guid.NewGuid()
            };
            this.salesService = new SalesService();
        }

        [Test]
        public void AddArticle()
        {
            VolatileDataset store = new VolatileDataset();
            Mock<ISalesService> mockSalesService = new Mock<ISalesService>();

            mockSalesService.Setup(s => s.Create(It.Is<Article>(a => a.Title == "Added title")))
                            .Returns(8);
            this.salesService = mockSalesService.Object;

            int id = this.salesService.Create(this.articleToAdd);

            Assert.AreEqual(8, id);
        }

        [Test]
        public void UpdateArticle()
        {
            VolatileDataset store = new VolatileDataset();
            Mock<ISalesService> mockSalesService = new Mock<ISalesService>();

            mockSalesService.Setup(r => r.Modify(It.IsAny<Article>()))
                            .Callback((Article a) =>
                            {
                                var index = store.Articles.FindIndex(el => el.Id == a.Id);
                                store.Articles[index] = a;
                            }).Verifiable();
            this.salesService = mockSalesService.Object;

            Article article = store.Articles.ElementAt(4);
            string newTitle = article.Title = "a5";
            string newDescription = article.Description = "a5d";

            this.salesService.Modify(article);

            mockSalesService.Verify(r => r.Modify(It.IsAny<Article>()), Times.Once());
            Assert.AreEqual(newTitle, store.Articles.ElementAt(4).Title);
            Assert.AreEqual(newDescription, store.Articles.ElementAt(4).Description);

        }

        [Test]
        public void DeleteArticle()
        {
            VolatileDataset store = new VolatileDataset();
            Mock<ISalesService> mockSalesService = new Mock<ISalesService>();

            mockSalesService.Setup(r => r.Delete(It.IsAny<Article>()))
                            .Callback((Article a) =>
                            {
                                store.Articles.Remove(a);
                            }).Verifiable();
            this.salesService = mockSalesService.Object;

            Article article = store.Articles.ElementAt(5);

            this.salesService.Delete(article);

            mockSalesService.Verify(r => r.Delete(It.IsAny<Article>()), Times.Once());
            Assert.AreEqual(6, store.Articles.Count);
        }

        [Test]
        public void GetArticle()
        {
            VolatileDataset store = new VolatileDataset(); 
            Mock<ISalesService> mockSalesService = new Mock<ISalesService>();

            mockSalesService.Setup(r => r.GetById<Article>(It.IsInRange<int>(1, 6, Range.Inclusive)))
                            .Returns<int>(id => store.Articles.Find(el => el.Id == id));
            this.salesService = mockSalesService.Object;

            Article article = this.salesService.GetById<Article>(1);

            Article expectedArticle = store.Articles.Single(a => a.Id == 1);
            Assert.AreEqual(expectedArticle.Title, article.Title);
            Assert.AreEqual(expectedArticle.Description, article.Description);
            Assert.AreEqual(expectedArticle.ImagesPath, article.ImagesPath);
        }


    }
}
