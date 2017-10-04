using GenericStructure.Dal.Context;
using GenericStructure.Dal.Context.Contracts;
using GenericStructure.Dal.Exceptions;
using GenericStructure.Dal.Manipulation.Repositories;
using GenericStructure.Dal.Manipulation.Repositories.Contracts;
using GenericStructure.Dal.Manipulation.Repositories.Implementation.Base;
using GenericStructure.Dal.Manipulation.Repositories.Implementation.Specific;
using GenericStructure.Dal.Models;
using GenericStructure.Dal.Models.Base;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Tests.Testing.Manipulation.Repositories
{
    [TestFixture]
    public class RepositoriesSetTest
    {
        [Test]
        public void GetGeneric_ForArticle()
        {
            RepositoriesSet repositoriesSet = new RepositoriesSet();

            repositoriesSet.Register<Article, IArticlesRepository>(new ArticlesRepository(new Mock<IDBContext>().Object));

            var repository = repositoriesSet.GetGeneric<Article>();

            Assert.IsInstanceOf<GenericRepository<Article>>(repository);
        }

        [Test]
        public void GetGeneric_ForArticle_NotRegistered()
        {
            RepositoriesSet repositoriesSet = new RepositoriesSet();

            DalException ex = Assert.Throws<DalException>(() =>
            {
                repositoriesSet.GetGeneric<Article>();
            });
            Assert.That(ex.errorType, Is.EqualTo(DalErrorType.RepositoriesSetMissingMapping));
            Assert.That(ex.Message, Is.EqualTo("Instance is missing for GenericStructure.Dal.Models.Article"));
        }

        [Test]
        public void GetSpecific_ForArticle()
        {
            RepositoriesSet repositoriesSet = new RepositoriesSet();

            repositoriesSet.Register<Article, IArticlesRepository>(new ArticlesRepository(new Mock<IDBContext>().Object));

            var repository = repositoriesSet.GetSpecific<Article, IArticlesRepository>();

            Assert.IsInstanceOf<IArticlesRepository>(repository);
        }

        [Test]
        public void GetSpecific_ForArticle_NotRegistered()
        {
            RepositoriesSet repositoriesSet = new RepositoriesSet();

            DalException ex = Assert.Throws<DalException>(() =>
            {
                repositoriesSet.GetGeneric<Article>();
            });
            Assert.That(ex.errorType, Is.EqualTo(DalErrorType.RepositoriesSetMissingMapping));
            Assert.That(ex.Message, Is.EqualTo("Instance is missing for GenericStructure.Dal.Models.Article"));
        }
    }
}
