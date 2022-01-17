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
            command.CommandText = "add_user";

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
            SqlParameter objParam;
            objParam = command.Parameters.Add("@email", SqlDbType.NVarChar);
            objParam.Value = ob.Email;
            objParam = command.Parameters.Add("@username", SqlDbType.NVarChar);
            objParam.Value = ob.Username;
            objParam = command.Parameters.Add("@password", SqlDbType.NVarChar);
            objParam.Value = ob.Password;
        }
        private UserBO selectUser(SqlDataReader objReader)
        {
            var userBO = new UserBO();
            object obj;

            userBO.Id = objReader.GetInt64(0);
            if ((obj = objReader.GetValue(1)) != DBNull.Value)
                userBO.Email = (String)obj;
            if ((obj = objReader.GetValue(2)) != DBNull.Value)
                userBO.Username = (String)obj;
            if ((obj = objReader.GetValue(3)) != DBNull.Value)
                userBO.Password = (String)obj;

            return userBO;
        }
        public UserBO LogIn(String email, String password)
        {
            UserBO userBO = null;

            command.CommandText = "login_user";

            SqlParameter objParam;
            objParam = command.Parameters.Add("@email", SqlDbType.NVarChar);
            objParam.Value = email;
            objParam = command.Parameters.Add("@password", SqlDbType.NVarChar);
            objParam.Value = password;

            SqlDataReader objReader = command.ExecuteReader();
            if (objReader.Read())
            {
                userBO = selectUser(objReader);
            }
            connection.Close();

            return userBO;
        }
    }
}
