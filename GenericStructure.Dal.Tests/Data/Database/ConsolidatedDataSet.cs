using GenericStructure.Dal.Tests.Data.Database.Primitives;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Tests.Data.Database
{
    internal class ConsolidatedDataSet : IDisposable
    {
        private SqlConnection connection;
        private Categories categories;
        private Articles articles;

        public List<int> ArticlesIds { get; private set; }
        public List<int> CategoriesIds { get; private set; }

        public ConsolidatedDataSet() 
        {
            this.ArticlesIds = new List<int>();
            this.CategoriesIds = new List<int>();

            this.connection = new SqlConnection(DatabaseConfiguration.ConnectionString);
            this.connection.Open();

            this.categories = new Categories(this.connection);
            this.articles = new Articles(this.connection);
        }

        public void Initialize() 
        {
            this.CategoriesIds.Add(this.categories.Create("Test Category 1"));

            this.ArticlesIds.Add(this.articles.Create(this.CategoriesIds.ElementAt(0), "Test Article 1", "Description 1", Guid.NewGuid(), 100.0m));
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
