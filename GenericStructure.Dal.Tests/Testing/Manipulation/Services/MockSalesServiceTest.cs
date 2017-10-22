using GenericStructure.Dal.Context.Contracts;
using GenericStructure.Dal.Manipulation.Repositories.Contracts;
using GenericStructure.Dal.Manipulation.Services.CoreBusiness;
using GenericStructure.Dal.Manipulation.Services.CoreBusiness.Contracts;
using GenericStructure.Models.CoreBusiness;
using GenericStructure.Shared.Tests.Data.Mocked;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
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

        #region async
        [Test]
        public async Task AddArticleAsync()
        {
            VolatileCoreBusinessDataset store = new VolatileCoreBusinessDataset();
            Mock<ISalesService> mockSalesService = new Mock<ISalesService>();

            mockSalesService.Setup(s => s.CreateAsync(It.Is<Article>(a => a.Title == "Added title")))
                            .ReturnsAsync(8);
            this.salesService = mockSalesService.Object;

            int id = await this.salesService.CreateAsync(this.articleToAdd);

            Assert.AreEqual(8, id);
        }

        [Test]
        public async Task UpdateArticleAsync()
        {
            VolatileCoreBusinessDataset store = new VolatileCoreBusinessDataset();
            Mock<ISalesService> mockSalesService = new Mock<ISalesService>();

            mockSalesService.Setup(r => r.ModifyAsync(It.IsAny<Article>()))
                            .Returns(Task.FromResult<object>(null))
                            .Callback((Article a) =>
                            {
                                var index = store.Articles.FindIndex(el => el.Id == a.Id);
                                store.Articles[index] = a;
                            }).Verifiable();
            this.salesService = mockSalesService.Object;

            Article article = store.Articles.ElementAt(4);
            string newTitle = article.Title = "a5";
            string newDescription = article.Description = "a5d";

            await this.salesService.ModifyAsync(article);

            mockSalesService.Verify(r => r.ModifyAsync(It.IsAny<Article>()), Times.Once());
            Assert.AreEqual(newTitle, store.Articles.ElementAt(4).Title);
            Assert.AreEqual(newDescription, store.Articles.ElementAt(4).Description);

        }

        [Test]
        public async Task DeleteArticleAsync()
        {
            VolatileCoreBusinessDataset store = new VolatileCoreBusinessDataset();
            Mock<ISalesService> mockSalesService = new Mock<ISalesService>();

            mockSalesService.Setup(r => r.DeleteAsync(It.IsAny<Article>()))
                            .Returns(Task.FromResult<object>(null))
                            .Callback((Article a) =>
                            {
                                store.Articles.Remove(a);
                            }).Verifiable();
            this.salesService = mockSalesService.Object;

            Article article = store.Articles.ElementAt(5);

            await this.salesService.DeleteAsync(article);

            mockSalesService.Verify(r => r.DeleteAsync(It.IsAny<Article>()), Times.Once());
            Assert.AreEqual(6, store.Articles.Count);
        }
        #endregion
    }
}
