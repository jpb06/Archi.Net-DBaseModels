using GenericStructure.Business.Exposed;
using GenericStructure.Business.InversionOfControl;
using NUnit.Framework;
using System.Threading.Tasks;

namespace GenericStructure.Business.Tests.UnitTests.Exposed
{
    [TestFixture]
    public class SalesManagerTest
    {
        public SalesManagerTest()
        {
            IoCConfiguration.Setup(true);
        }

        [Test]
        public async Task GetArticles()
        {
            var articles = await Sales.GetArticlesAsync(1);

            int a = 0;
        }
    }
}
