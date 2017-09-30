using GenericStructure.Dal.Context;
using GenericStructure.Dal.Context.Contracts;
using GenericStructure.Dal.Manipulation.Repositories;
using GenericStructure.Dal.Manipulation.Repositories.Contracts;
using GenericStructure.Dal.Manipulation.Repositories.Implementation.Base;
using GenericStructure.Dal.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Tests.Testing.Manipulation.Repositories
{
    [TestFixture]
    public class RepositoriesMappingTest
    {
        private IDBContext context;
        private RepositoriesMapping repositoriesMapping;

        [OneTimeSetUp]
        public void Init()
        {
            this.context = new GenericStructureContext();
            this.repositoriesMapping = new RepositoriesMapping(this.context);
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            this.context.Dispose();
        }

        #region GetRepositoryType
        [Test]
        public void GetRepositoryType_FromArticle()
        {
            Type type = this.repositoriesMapping.GetRepositoryType(typeof(Article));
            Assert.AreEqual(typeof(IArticlesRepository), type);
        }

        [Test]
        public void GetRepositoryType_FromCategory()
        {
            Type type = this.repositoriesMapping.GetRepositoryType(typeof(Category));
            Assert.AreEqual(typeof(ICategoriesRepository), type);
        }

        [Test]
        public void GetRepositoryType_FromCustomer()
        {
            Type type = this.repositoriesMapping.GetRepositoryType(typeof(Customer));
            Assert.AreEqual(typeof(ICustomersRepository), type);
        }

        [Test]
        public void GetRepositoryType_FromOrderDetail()
        {
            Type type = this.repositoriesMapping.GetRepositoryType(typeof(OrderDetail));
            Assert.AreEqual(typeof(IOrderDetailsRepository), type);
        }

        [Test]
        public void GetRepositoryType_FromOrder()
        {
            Type type = this.repositoriesMapping.GetRepositoryType(typeof(Order));
            Assert.AreEqual(typeof(IOrdersRepository), type);
        }
        #endregion

        #region GetGenericFactory
        [Test]
        public void GetGenericFactory_ForArticle()
        {
            var func = this.repositoriesMapping.GetGenericFactory<Article>();
            var repo = func();

            Assert.AreEqual(typeof(GenericRepository<Article>), repo.GetType());
        }

        [Test]
        public void GetGenericFactory_ForCategory()
        {
            var func = this.repositoriesMapping.GetGenericFactory<Category>();
            var repo = func();

            Assert.AreEqual(typeof(GenericRepository<Category>), repo.GetType());
        }

        [Test]
        public void GetGenericFactory_ForCustomer()
        {
            var func = this.repositoriesMapping.GetGenericFactory<Customer>();
            var repo = func();

            Assert.AreEqual(typeof(GenericRepository<Customer>), repo.GetType());
        }

        [Test]
        public void GetGenericFactory_ForOrderDetail()
        {
            var func = this.repositoriesMapping.GetGenericFactory<OrderDetail>();
            var repo = func();

            Assert.AreEqual(typeof(GenericRepository<OrderDetail>), repo.GetType());
        }

        [Test]
        public void GetGenericFactory_ForOrder()
        {
            var func = this.repositoriesMapping.GetGenericFactory<Order>();
            var repo = func();

            Assert.AreEqual(typeof(GenericRepository<Order>), repo.GetType());
        }
        #endregion

        [Test]
        public void GetSpecificFactory()
        {

        }
    }
}
