using System.Data.SqlClient;

namespace GenericStructure.Shared.Tests.Data.Database.Primitives
{
    public class CategoriesSqlHelper
    {
        private SqlConnection connection;

        public CategoriesSqlHelper(SqlConnection connection) 
        {
            this.connection = connection;
        }

        public int Create(string title) 
        {
            SqlCommand command = new SqlCommand("INSERT INTO [dbo].[Categories] ([Title]) output inserted.Id VALUES (@title);", connection);
            command.Parameters.AddWithValue("@title", title);
            return (int)command.ExecuteScalar();
        }

        public void Modify(int id, string title) 
        {
            SqlCommand command = new SqlCommand("UPDATE [dbo].[Categories] SET [Title] = @title WHERE [Id] = @id;", connection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@title", title);
            command.ExecuteNonQuery();
        }

        public void Delete(int id) 
        {
            SqlCommand command = new SqlCommand("DELETE FROM [dbo].[Categories] WHERE [Id] = @id;", connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
    }
}
