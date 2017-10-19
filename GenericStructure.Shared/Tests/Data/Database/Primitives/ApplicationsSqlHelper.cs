using System;
using System.Data.SqlClient;

namespace GenericStructure.Shared.Tests.Data.Database.Primitives
{
    public class ApplicationsSqlHelper
    {
        private SqlConnection connection;

        public ApplicationsSqlHelper(SqlConnection connection) 
        {
            this.connection = connection;
        }

        public int Create(string name, string version, DateTime firstRunDate) 
        {
            SqlCommand command = new SqlCommand("INSERT INTO [dbo].[Applications] ([Name], [Version], [FirstRunDate]) " +
                                                "output inserted.Id VALUES (@name, @version, @firstrundate);", connection);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@version", version);
            command.Parameters.AddWithValue("@firstrundate", firstRunDate);
            return (int)command.ExecuteScalar();
        }

        public void Delete(int id)
        {
            SqlCommand command = new SqlCommand("DELETE FROM [dbo].[Applications] WHERE [Id] = @id;", connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
    }
}
