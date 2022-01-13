using BO_Films;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DAL_Films
{
    public class UserDAL : BaseConnection
    {
        public int Add(UserBO ob)
        {
            command.CommandText = "eaq";

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
        private void setParameters(UserBO ob)
        {
            SqlParameter objParam2 = command.Parameters.Add("@email", SqlDbType.NVarChar);
            objParam2.Value = ob.Email;
            SqlParameter objParam3 = command.Parameters.Add("@password", SqlDbType.NVarChar);
            objParam3.Value = ob.Password;
        }
    }
}
