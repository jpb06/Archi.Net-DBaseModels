using GenericStructure.Models.ErrorsReporting;
using System.Data.SqlClient;

namespace GenericStructure.Shared.Tests.Data.Database.Primitives
{
    public class ExceptionsSqlHelper
    {        
        private SqlConnection connection;

        public ExceptionsSqlHelper(SqlConnection connection) 
        {
            this.connection = connection;
        }

        public void Delete(int id)
        {
            SqlCommand command = new SqlCommand("DELETE FROM [dbo].[Exceptions] WHERE [Id] = @id;", connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }

        public ErrorReportException GetBy(int id) 
        {
            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Exceptions] WHERE [Id] = @id;", connection);
            command.Parameters.AddWithValue("@id", id);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                reader.Read();
                return new ErrorReportException 
                {
                    Id = reader.GetInt32(0),
                    IdInnerException = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1), 
                    Type = reader.GetString(2),
                    Message = reader.GetString(3),
                    Source = reader.GetString(4),
                    SiteModule = reader.GetString(5),
                    SiteName = reader.GetString(6),
                    StackTrace = reader.GetString(7),
                    CustomErrorType = reader.IsDBNull(8) ? null : reader.GetString(8),
                    HelpLink = reader.IsDBNull(9) ? null : reader.GetString(9),
                    Date = reader.GetDateTime(10),
                    RowVersion = reader.GetValue(11) as byte[],
                    IdApplication = reader.GetInt32(12)
                };
            }
        }

        public ErrorReportException GetBy(string application, string version) 
        {
            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Exceptions] "+
                                                "JOIN [dbo].[Applications] on [dbo].[Exceptions].[IdApplication] = [dbo].[Applications].[Id] "+
                                                "WHERE [dbo].[Applications].[Name] = @name AND [dbo].[Applications].[Version] = @version "+
                                                "ORDER BY [dbo].[Exceptions].[Date] desc;", connection);
            command.Parameters.AddWithValue("@name", application);
            command.Parameters.AddWithValue("@version", version);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                reader.Read();
                return new ErrorReportException
                {
                    Id = reader.GetInt32(0),
                    IdInnerException = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1),
                    Type = reader.GetString(2),
                    Message = reader.GetString(3),
                    Source = reader.GetString(4),
                    SiteModule = reader.GetString(5),
                    SiteName = reader.GetString(6),
                    StackTrace = reader.GetString(7),
                    CustomErrorType = reader.IsDBNull(8) ? null : reader.GetString(8),
                    HelpLink = reader.IsDBNull(9) ? null : reader.GetString(9),
                    Date = reader.GetDateTime(10),
                    RowVersion = reader.GetValue(11) as byte[],
                    IdApplication = reader.GetInt32(12)
                };
            }
        }
    }
}
