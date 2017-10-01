using GenericStructure.Dal.Context;
using GenericStructure.Dal.Context.Contracts;
using GenericStructure.Dal.Exceptions;
using GenericStructure.Dal.Manipulation.Repositories;
using GenericStructure.Dal.Manipulation.Repositories.Contracts;
using GenericStructure.Dal.Manipulation.Repositories.Implementation.Base;
using GenericStructure.Dal.Manipulation.Repositories.Implementation.Specific;
using GenericStructure.Dal.Models;
using GenericStructure.Dal.Models.Base;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Tests.Testing.Manipulation.Repositories
{
    [TestFixture]
    public class RepositoryLocatorTest
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

        [Test]
        public void FindGenericRepository()
        {
            var repository = this.repositoriesMapping.FindGenericRepository<Article>();

            Assert.IsInstanceOf<GenericRepository<Article>>(repository);
        }

        [Test]
        public void FindRepository_Generic()
        {
            var repository = this.repositoriesMapping.FindRepository<IGenericRepository<Article>, Article>();

            Assert.IsInstanceOf<GenericRepository<Article>>(repository);
        }

        [Test]
        public void FindRepository_Specific()
        {
            var repository = this.repositoriesMapping.FindRepository<IArticlesRepository, Article>();

            Assert.IsInstanceOf<ArticlesRepository>(repository);
        }

        [Test]
        public void FindGenericRepository_UnmappedModel()
        {
            DalException ex = Assert.Throws<DalException>(() =>
            {
                this.repositoriesMapping.FindGenericRepository<BaseModel>();
            });
            Assert.That(ex.errorType, Is.EqualTo(DalErrorType.RepositoryLocatorMissingMapping));
            Assert.That(ex.Message, Is.EqualTo("Mapping is missing for GenericStructure.Dal.Models.Base.BaseModel"));
        }

        [Test]
        public void FindRepository_UnmappedModel()
        {
            DalException ex = Assert.Throws<DalException>(() =>
            {
                this.repositoriesMapping.FindRepository<IGenericRepository<BaseModel>, BaseModel>();
            });
            Assert.That(ex.errorType, Is.EqualTo(DalErrorType.RepositoryLocatorMissingMapping));
            Assert.That(ex.Message, Is.EqualTo("Mapping is missing for GenericStructure.Dal.Models.Base.BaseModel"));
        }
    }
}
