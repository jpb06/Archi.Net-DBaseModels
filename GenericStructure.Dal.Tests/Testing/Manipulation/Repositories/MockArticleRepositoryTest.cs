using GenericStructure.Dal.Context.Contracts;
using GenericStructure.Dal.Manipulation.Repositories.Implementation.Specific;
using GenericStructure.Models.CoreBusiness;
using GenericStructure.Shared.Tests.Data.Mocked;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Tests.Testing.Manipulation.Repositories
{
    [TestFixture]
    public class MockArticleRepositoryTest
    {
        private ArticlesRepository articlesRepository;

        public MockArticleRepositoryTest() { }

        [Test]
        public void Insert()
        {
            VolatileCoreBusinessDataset store = new VolatileCoreBusinessDataset();
            Mock<ICoreBusinessContext> context = new Mock<ICoreBusinessContext>();
            Mock<ArticlesRepository> mockArticleRepository = new Mock<ArticlesRepository>(context.Object);

            mockArticleRepository.Setup(r => r.Insert(It.IsAny<Article>()))
                                 .Callback((Article a) =>
                                 {
                                     a.Id = 8;
                                     a.Category = new Category
                                     {
                                         Id = 4,
                                         Title = "Category 4",
                                         RowVersion = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 }
                                     };
                                     a.RowVersion = new byte[] { 0, 0, 0, 0, 0, 0, 0, 1 };
                                     store.Articles.Add(a);
                                 }).Verifiable();
            this.articlesRepository = mockArticleRepository.Object;

            Article article = new Article
            {
                IdCategory = 4,
                Title = "Article 8",
                Description = "Description 8",
                ImagesPath = Guid.NewGuid(),
                Price = 100m
            };

            this.articlesRepository.Insert(article);

            mockArticleRepository.Verify(r => r.Insert(It.IsAny<Article>()), Times.Once());
            Assert.AreEqual(8, store.Articles.Count);
            Assert.AreEqual(8, store.Articles.Last().Id);
        }

        [Test]
        public void Update()
        {
            VolatileCoreBusinessDataset store = new VolatileCoreBusinessDataset();
            Mock<ICoreBusinessContext> context = new Mock<ICoreBusinessContext>();
            Mock<ArticlesRepository> mockArticleRepository = new Mock<ArticlesRepository>(context.Object);

            mockArticleRepository.Setup(r => r.Update(It.IsAny<Article>()))
                                 .Callback((Article a) =>
                                 {
                                     var index = store.Articles.FindIndex(el => el.Id == a.Id);
                                     store.Articles[index] = a;
                                 }).Verifiable();
            this.articlesRepository = mockArticleRepository.Object;

            Article article = store.Articles.ElementAt(4);
            string newTitle = article.Title = "a5";
            string newDescription = article.Description = "a5d";

            this.articlesRepository.Update(article);

            mockArticleRepository.Verify(r => r.Update(It.IsAny<Article>()), Times.Once());
            Assert.AreEqual(newTitle, store.Articles.ElementAt(4).Title);
            Assert.AreEqual(newDescription, store.Articles.ElementAt(4).Description);

        }

        [Test]
        public void Delete()
        {
            VolatileCoreBusinessDataset store = new VolatileCoreBusinessDataset();
            Mock<ICoreBusinessContext> context = new Mock<ICoreBusinessContext>();
            Mock<ArticlesRepository> mockArticleRepository = new Mock<ArticlesRepository>(context.Object);

            mockArticleRepository.Setup(r => r.Delete(It.IsAny<Article>()))
                                 .Callback((Article a) =>
                                 {
                                     store.Articles.Remove(a);
                                 }).Verifiable();
            this.articlesRepository = mockArticleRepository.Object;

            Article article = store.Articles.ElementAt(5);

            this.articlesRepository.Delete(article);

            mockArticleRepository.Verify(r => r.Delete(It.IsAny<Article>()), Times.Once());
            Assert.AreEqual(6, store.Articles.Count);
        }

        [Test]
        public void DeleteById()
        {
            VolatileCoreBusinessDataset store = new VolatileCoreBusinessDataset();
            Mock<ICoreBusinessContext> context = new Mock<ICoreBusinessContext>();
            Mock<ArticlesRepository> mockArticleRepository = new Mock<ArticlesRepository>(context.Object);

            mockArticleRepository.Setup(r => r.Delete(It.IsAny<int>()))
                                 .Callback((object a) =>
                                 {
                                     var index = store.Articles.FindIndex(el => el.Id == (int)a);
                                     store.Articles.RemoveAt(index);
                                 }).Verifiable();
            this.articlesRepository = mockArticleRepository.Object;

            this.articlesRepository.Delete(1);

            mockArticleRepository.Verify(r => r.Delete(It.IsAny<int>()), Times.Once());
            Assert.AreEqual(6, store.Articles.Count);
        }

        [Test]
        public void GetById()
        {
            VolatileCoreBusinessDataset store = new VolatileCoreBusinessDataset();
            Mock<ICoreBusinessContext> context = new Mock<ICoreBusinessContext>();
            Mock<ArticlesRepository> mockArticleRepository = new Mock<ArticlesRepository>(context.Object);

            mockArticleRepository.Setup(r => r.GetById(It.IsInRange<int>(1, 6, Range.Inclusive)))
                                 .Returns<int>(id => store.Articles.Find(el => el.Id == id));
            this.articlesRepository = mockArticleRepository.Object;

            Article article = this.articlesRepository.GetById(1);
            Article storedArticle = store.Articles.Single(el => el.Id == 1);

            Assert.AreEqual(article.Title, storedArticle.Title);
            Assert.AreEqual(article.Description, storedArticle.Description);
            Assert.AreEqual(article.IdCategory, storedArticle.IdCategory);
        }

        [Test]
        public void GetById_DoesntExist()
        {
            VolatileCoreBusinessDataset store = new VolatileCoreBusinessDataset();
            Mock<ICoreBusinessContext> context = new Mock<ICoreBusinessContext>();
            Mock<ArticlesRepository> mockArticleRepository = new Mock<ArticlesRepository>(context.Object);

            mockArticleRepository.Setup(r => r.GetById(It.IsNotIn<int>(1, 2, 3, 4, 5, 6)))
                                 .Returns<Article>(null);
            this.articlesRepository = mockArticleRepository.Object;

            Article article = this.articlesRepository.GetById(10);

            Assert.AreEqual(null, article);
        }

        [Test]
        public void Get_Filtered()
        {
            VolatileCoreBusinessDataset store = new VolatileCoreBusinessDataset();
            Mock<ICoreBusinessContext> context = new Mock<ICoreBusinessContext>();
            Mock<ArticlesRepository> mockArticleRepository = new Mock<ArticlesRepository>(context.Object);

            mockArticleRepository.Setup(r => r.Get(It.IsAny<Expression<Func<Article, bool>>>(), null, string.Empty))
                                 .Returns((Expression<Func<Article, bool>> filter,
                                           Func<IQueryable<Article>, IOrderedQueryable<Article>> orderBy,
                                           string includeProperties) => store.Articles.Where(filter.Compile()));
            this.articlesRepository = mockArticleRepository.Object;

            var result = this.articlesRepository.Get(a => a.IdCategory == 1);

            Assert.AreEqual(3, result.Count());
        }

        [Test]
        public void Get_MinimumPriceFiltered()
        {
            VolatileCoreBusinessDataset store = new VolatileCoreBusinessDataset();
            Mock<ICoreBusinessContext> context = new Mock<ICoreBusinessContext>();
            Mock<ArticlesRepository> mockArticleRepository = new Mock<ArticlesRepository>(context.Object);

            mockArticleRepository.Setup(r => r.Get(It.IsAny<Expression<Func<Article, bool>>>(), null, string.Empty))
                                 .Returns((Expression<Func<Article, bool>> filter,
                                           Func<IQueryable<Article>, IOrderedQueryable<Article>> orderBy,
                                           string includeProperties) => store.Articles.Where(filter.Compile()));
            this.articlesRepository = mockArticleRepository.Object;

            var result = this.articlesRepository.Get(filter: a => a.Price > 100m);

            Assert.AreEqual(4, result.Count());
        }

        [Test]
        public void Get_Ordered()
        {
            VolatileCoreBusinessDataset store = new VolatileCoreBusinessDataset();
            Mock<ICoreBusinessContext> context = new Mock<ICoreBusinessContext>();
            Mock<ArticlesRepository> mockArticleRepository = new Mock<ArticlesRepository>(context.Object);

            mockArticleRepository.Setup(r => r.Get(null, It.IsAny<Func<IQueryable<Article>, IOrderedQueryable<Article>>>(), string.Empty))
                                 .Returns((Expression<Func<Article, bool>> filter,
                                           Func<IQueryable<Article>, IOrderedQueryable<Article>> orderBy,
                                           string includeProperties) => orderBy.Invoke(store.Articles.AsQueryable()));
            this.articlesRepository = mockArticleRepository.Object;

            var result = this.articlesRepository.Get(orderBy: q => q.OrderByDescending(a => a.Id));

            Assert.AreEqual(7, result.Count());
            Assert.AreEqual(1, result.Last().Id);
            Assert.AreEqual(7, result.First().Id);
        }

        [Test]
        public void Get_PriceFilteredAndOrdered()
        {
            VolatileCoreBusinessDataset store = new VolatileCoreBusinessDataset();
            Mock<ICoreBusinessContext> context = new Mock<ICoreBusinessContext>();
            Mock<ArticlesRepository> mockArticleRepository = new Mock<ArticlesRepository>(context.Object);

            mockArticleRepository.Setup(r => r.Get(It.IsAny<Expression<Func<Article, bool>>>(),
                                                   It.IsAny<Func<IQueryable<Article>, IOrderedQueryable<Article>>>(),
                                                   string.Empty))
                                 .Returns((Expression<Func<Article, bool>> filter,
                                           Func<IQueryable<Article>, IOrderedQueryable<Article>> orderBy,
                                           string includeProperties) => orderBy.Invoke(store.Articles.Where(filter.Compile()).AsQueryable()));
            this.articlesRepository = mockArticleRepository.Object;

            var result = this.articlesRepository.Get(filter: a => a.Price > 100m,
                                                    orderBy: q => q.OrderByDescending(a => a.Price));

            Assert.AreEqual(4, result.Count());
            Assert.AreEqual(150m, result.Last().Price);
            Assert.AreEqual(1000.0m, result.First().Price);
        }

        #region async
        [Test]
        public async Task GetByIdAsync()
        {
            VolatileCoreBusinessDataset store = new VolatileCoreBusinessDataset();
            Mock<ICoreBusinessContext> context = new Mock<ICoreBusinessContext>();
            Mock<ArticlesRepository> mockArticleRepository = new Mock<ArticlesRepository>(context.Object);

            mockArticleRepository.Setup(r => r.GetByIdAsync(It.IsInRange<int>(1, 6, Range.Inclusive)))
                                 .Returns<int>(id => Task.FromResult(store.Articles.Find(el => el.Id == id)));
            this.articlesRepository = mockArticleRepository.Object;

            Article article = await this.articlesRepository.GetByIdAsync(1);
            Article storedArticle = store.Articles.Single(el => el.Id == 1);

            Assert.AreEqual(article.Title, storedArticle.Title);
            Assert.AreEqual(article.Description, storedArticle.Description);
            Assert.AreEqual(article.IdCategory, storedArticle.IdCategory);
        }

        [Test]
        public async Task GetByIdAsync_DoesntExist()
        {
            VolatileCoreBusinessDataset store = new VolatileCoreBusinessDataset();
            Mock<ICoreBusinessContext> context = new Mock<ICoreBusinessContext>();
            Mock<ArticlesRepository> mockArticleRepository = new Mock<ArticlesRepository>(context.Object);

            mockArticleRepository.Setup(r => r.GetByIdAsync(It.IsNotIn<int>(1, 2, 3, 4, 5, 6)))
                                 .Returns<int>(id => Task.FromResult<Article>(null));
            this.articlesRepository = mockArticleRepository.Object;

            Article article = await this.articlesRepository.GetByIdAsync(10);

            Assert.AreEqual(null, article);
        }

        [Test]
        public async Task GetAsync_Filtered()
        {
            VolatileCoreBusinessDataset store = new VolatileCoreBusinessDataset();
            Mock<ICoreBusinessContext> context = new Mock<ICoreBusinessContext>();
            Mock<ArticlesRepository> mockArticleRepository = new Mock<ArticlesRepository>(context.Object);

            mockArticleRepository.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Article, bool>>>(), null, string.Empty))
                                 .Returns((Expression<Func<Article, bool>> filter,
                                           Func<IQueryable<Article>, IOrderedQueryable<Article>> orderBy,
                                           string includeProperties) => Task.FromResult(store.Articles.Where(filter.Compile())));
            this.articlesRepository = mockArticleRepository.Object;

            var result = await this.articlesRepository.GetAsync(a => a.IdCategory == 1);

            Assert.AreEqual(3, result.Count());
        }

        [Test]
        public async Task GetAsync_MinimumPriceFiltered()
        {
            VolatileCoreBusinessDataset store = new VolatileCoreBusinessDataset();
            Mock<ICoreBusinessContext> context = new Mock<ICoreBusinessContext>();
            Mock<ArticlesRepository> mockArticleRepository = new Mock<ArticlesRepository>(context.Object);

            mockArticleRepository.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Article, bool>>>(), null, string.Empty))
                                 .Returns((Expression<Func<Article, bool>> filter,
                                           Func<IQueryable<Article>, IOrderedQueryable<Article>> orderBy,
                                           string includeProperties) => Task.FromResult(store.Articles.Where(filter.Compile())));
            this.articlesRepository = mockArticleRepository.Object;

            var result = await this.articlesRepository.GetAsync(filter: a => a.Price > 100m);

            Assert.AreEqual(4, result.Count());
        }

        [Test]
        public async Task GetAsync_Ordered()
        {
            VolatileCoreBusinessDataset store = new VolatileCoreBusinessDataset();
            Mock<ICoreBusinessContext> context = new Mock<ICoreBusinessContext>();
            Mock<ArticlesRepository> mockArticleRepository = new Mock<ArticlesRepository>(context.Object);

            mockArticleRepository.Setup(r => r.GetAsync(null, It.IsAny<Func<IQueryable<Article>, IOrderedQueryable<Article>>>(), string.Empty))
                                 .Returns((Expression<Func<Article, bool>> filter,
                                           Func<IQueryable<Article>, IOrderedQueryable<Article>> orderBy,
                                           string includeProperties) =>
                                 {
                                     var ordered = orderBy.Invoke(store.Articles.AsQueryable());
                                     return Task.FromResult(ordered.AsEnumerable());
                                 });
            this.articlesRepository = mockArticleRepository.Object;

            var result = await this.articlesRepository.GetAsync(orderBy: q => q.OrderByDescending(a => a.Id));

            Assert.AreEqual(7, result.Count());
            Assert.AreEqual(1, result.Last().Id);
            Assert.AreEqual(7, result.First().Id);
        }

        [Test]
        public async Task GetAsync_PriceFilteredAndOrdered()
        {
            VolatileCoreBusinessDataset store = new VolatileCoreBusinessDataset();
            Mock<ICoreBusinessContext> context = new Mock<ICoreBusinessContext>();
            Mock<ArticlesRepository> mockArticleRepository = new Mock<ArticlesRepository>(context.Object);

            mockArticleRepository.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Article, bool>>>(),
                                                        It.IsAny<Func<IQueryable<Article>, IOrderedQueryable<Article>>>(),
                                                        string.Empty))
                                 .Returns((Expression<Func<Article, bool>> filter,
                                           Func<IQueryable<Article>, IOrderedQueryable<Article>> orderBy,
                                           string includeProperties) =>
                                 {
                                     var filtered = store.Articles.Where(filter.Compile()).AsQueryable();
                                     var ordered = orderBy.Invoke(filtered);
                                     return Task.FromResult(ordered.AsEnumerable());
                                 });
            this.articlesRepository = mockArticleRepository.Object;

            var result = await this.articlesRepository.GetAsync(filter: a => a.Price > 100m,
                                                          orderBy: q => q.OrderByDescending(a => a.Price));

            Assert.AreEqual(4, result.Count());
            Assert.AreEqual(150m, result.Last().Price);
            Assert.AreEqual(1000.0m, result.First().Price);
        }
        #endregion
    }
}
