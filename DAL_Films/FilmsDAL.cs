using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO_Films;
namespace DAL_Films
{
    public class FilmsDAL : BaseConnection
    {
        public int Add(FilmsBO ob)
        {
            command.CommandText = "User_Add";

            setParameters(ob);
            var insertedRows = command.ExecuteNonQuery();
            //var o = command.Parameters["RETURN VALUE"].Value;

            //if (o != null)
            //{
            //    insertedRows = Convert.ToInt32(o);
            //}

            connection.Close();
            return insertedRows;
        }
        private void setParameters(FilmsBO ob)
        {
            SqlParameter objParam1 = command.Parameters.Add("@id", SqlDbType.Int);
            objParam1.Value = ob.id;
            SqlParameter objParam2 = command.Parameters.Add("@username", SqlDbType.NVarChar);
            objParam2.Value = ob.username;
            SqlParameter objParam3 = command.Parameters.Add("@password", SqlDbType.NVarChar);
            objParam3.Value = ob.password;
        }
    }
}
