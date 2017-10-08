using GenericStructure.Dal.Models.ErrorsReporting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Tests.Data.Database.Primitives
{
    internal class ExceptionsSqlHelper
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

        public ErrorReportException Get(int id) 
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
    }
}
