using System;
using System.Data.SqlClient;

namespace GenericStructure.Shared.Tests.Data.Database.Primitives
{
    public class ArticlesSqlHelper
    {
        private SqlConnection connection;

        public ArticlesSqlHelper(SqlConnection connection) 
        {
            this.connection = connection;
        }

        public int Create(int idCategory,
                          string title, string description,
                          Guid imagePath, decimal price) 
        {
            SqlCommand command = new SqlCommand("INSERT INTO [dbo].[Articles] ([IdCategory], [Title], [Description], [ImagesPath], [Price]) "+ 
                                                "output inserted.Id VALUES (@idCategory, @title, @description, @imagesPath, @price);", connection);
            command.Parameters.AddWithValue("@idCategory", idCategory);
            command.Parameters.AddWithValue("@title", title);
            command.Parameters.AddWithValue("@description", description);
            command.Parameters.AddWithValue("@imagesPath", imagePath);
            command.Parameters.AddWithValue("@price", price);
            return (int)command.ExecuteScalar();
        }

        public void Modify(int id, int idCategory,
                           string title, string description,
                           Guid imagePath, decimal price)
        {
            SqlCommand command = new SqlCommand("UPDATE [dbo].[Articles] SET "+
                                                "[IdCategory] = @idCategory, [Title] = @title, [Description] = @description, [ImagesPath] = @imagesPath, [Price] = @price " +
                                                "WHERE [Id] = @id;", connection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@idCategory", idCategory);
            command.Parameters.AddWithValue("@title", title);
            command.Parameters.AddWithValue("@description", description);
            command.Parameters.AddWithValue("@imagesPath", imagePath);
            command.Parameters.AddWithValue("@price", price);
            command.ExecuteNonQuery();
        }

        public void ModifyTitle(int id, string title) 
        {
            SqlCommand command = new SqlCommand("UPDATE [dbo].[Articles] SET " +
                                                "[Title] = @title " +
                                                "WHERE [Id] = @id;", connection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@title", title);
            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            SqlCommand command = new SqlCommand("DELETE FROM [dbo].[Articles] WHERE [Id] = @id;", connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
    }
}
