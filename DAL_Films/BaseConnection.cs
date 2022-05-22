using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL_Films
{
    public class BaseConnection
    {
        private string connectionString;
        public SqlConnection connection;
        public SqlCommand command;
        public BaseConnection()
        {
            /*connectionString = ConfigurationManager.
                ConnectionStrings["ConnectionString"].ConnectionString;*/
            connectionString = string.Empty;

            connection = new SqlConnection(connectionString);

            command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;

            openConnection();
        }
        private SqlConnection openConnection()
        {
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection.Open();
            }
            return connection;
        }
    }
}
