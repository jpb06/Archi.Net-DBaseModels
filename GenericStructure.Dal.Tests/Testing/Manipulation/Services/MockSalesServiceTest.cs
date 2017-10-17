using GenericStructure.Dal.Context.Contracts;
using GenericStructure.Dal.Manipulation.Repositories.Contracts;
using GenericStructure.Dal.Manipulation.Services.CoreBusiness;
using GenericStructure.Dal.Manipulation.Services.CoreBusiness.Contracts;
using GenericStructure.Dal.Models.CoreBusiness;
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

            Mock<ICoreBusinessContext> context = new Mock<ICoreBusinessContext>();
            Mock<IArticlesRepository> articleRepo = new Mock<IArticlesRepository>();
            Mock<ICategoriesRepository> categoryRepo = new Mock<ICategoriesRepository>();
            this.salesService = new SalesService(context.Object, articleRepo.Object, categoryRepo.Object);
        }

        [Test]
        public void AddArticle()
        {
            VolatileCoreBusinessDataset store = new VolatileCoreBusinessDataset();
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
            VolatileCoreBusinessDataset store = new VolatileCoreBusinessDataset();
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
            VolatileCoreBusinessDataset store = new VolatileCoreBusinessDataset();
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
            VolatileCoreBusinessDataset store = new VolatileCoreBusinessDataset(); 
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
