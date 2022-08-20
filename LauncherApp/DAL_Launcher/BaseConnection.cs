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
        private static readonly string connectstr = "Server=tcp:filmsdbc.database.windows.net;Database=fdbc_up;User ID=fuhgc@filmsdbc;Password=D32wsxzaq1;Trusted_Connection=False;Encrypt=True;";
        public BaseConnection()
        {
            connectionString = connectstr;

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
            SqlConnection connetion = null;
            try
            {
                connetion = new SqlConnection(connectstr);
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
