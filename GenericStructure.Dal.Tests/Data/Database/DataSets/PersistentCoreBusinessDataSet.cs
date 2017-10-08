using GenericStructure.Dal.Tests.Data.Database.Primitives;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Tests.Data.Database.DataSets
{
    internal class PersistentCoreBusinessDataSet : IDisposable
    {
        private SqlConnection connection;
        private CategoriesSqlHelper categories;
        private ArticlesSqlHelper articles;

        public List<int> ArticlesIds { get; private set; }
        public List<int> CategoriesIds { get; private set; }

        public PersistentCoreBusinessDataSet() 
        {
            this.ArticlesIds = new List<int>();
            this.CategoriesIds = new List<int>();

            this.connection = new SqlConnection(DatabaseConfiguration.CoreBusinessConnectionString);
            this.connection.Open();

            this.categories = new CategoriesSqlHelper(this.connection);
            this.articles = new ArticlesSqlHelper(this.connection);
        }

        public void Initialize() 
        {
            this.CategoriesIds.Add(this.categories.Create("Test Category 1"));

            this.ArticlesIds.Add(this.articles.Create(this.CategoriesIds.ElementAt(0), "Test Article 1", "Description 1", Guid.NewGuid(), 100.0m));
            this.ArticlesIds.Add(this.articles.Create(this.CategoriesIds.ElementAt(0), "Test Article 2", "Description 2", Guid.NewGuid(), 500.0m));
            this.ArticlesIds.Add(this.articles.Create(this.CategoriesIds.ElementAt(0), "Test Article 3", "Description 3", Guid.NewGuid(), 1000.0m));
            this.ArticlesIds.Add(this.articles.Create(this.CategoriesIds.ElementAt(0), "Test Article 4", "Description 4", Guid.NewGuid(), 1500.0m));
        }

        public void Destroy() 
        {
            foreach (var id in this.ArticlesIds)
                this.articles.Delete(id);

            foreach (var id in this.CategoriesIds)
                this.categories.Delete(id);
        }

        public void Dispose()
        {
            this.connection.Close();
            this.connection.Dispose();
        }
    }
}
