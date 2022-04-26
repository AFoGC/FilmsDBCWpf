using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;

namespace DAL_Launcher
{
    public class BaseConnection
    {
        private string connectionString;
        public SqlConnection connection;
        public SqlCommand command;
        public BaseConnection()
        {
            connectionString = ConfigurationManager.
                ConnectionStrings["ConnectionString"].ConnectionString;

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

        public static bool IsDatabaseOnline()
        {
            bool isConnected = false;
            string constr = ConfigurationManager.
                ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connetion = null;
            try
            {
                connetion = new SqlConnection(constr);
                connetion.Open();
                isConnected = true;
            }
            catch (SqlException ex)
            {
                isConnected = false;
            }
            finally
            {
                if (connetion.State == ConnectionState.Open)
                {
                    connetion.Close();
                }
            }


            return isConnected;
        }
    }
}
