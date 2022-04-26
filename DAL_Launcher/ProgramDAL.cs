using BO_Launcher;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Launcher
{
    public class ProgramDAL : BaseConnection
    {
		private void setParameters(ProgramBO program)
		{
			SqlParameter objParam;
			objParam = command.Parameters.Add("@updateinfo", SqlDbType.NVarChar);
			objParam.Value = program.UpdateInfo;
			objParam = command.Parameters.Add("@programfile", SqlDbType.VarBinary);
			objParam.Value = program.ProgramFile;
		}

		private ProgramBO getProfile(SqlDataReader objReader)
		{
			ProgramBO program = new ProgramBO();

			program.ID = objReader.GetInt64(0);
			program.UpdateInfo = objReader.GetString(1);
			program.ProgramFile = (byte[])objReader.GetValue(2);
			/*
			if ((ob = objReader.GetValue(2)) != DBNull.Value)
				program.ProgramFile = (byte[])ob;
			*/

			return program;
		}

		public ProgramBO GetLastUpdate()
        {
			ProgramBO programBO = null;
			command.CommandText = "get_last_update";
			SqlDataReader objReader = command.ExecuteReader();
			if (objReader.Read())
			{
				programBO = getProfile(objReader);
			}
			connection.Close();

			return programBO;
        }

		public int AddUpdate(ProgramBO program)
        {
			command.CommandText = "add_program_update";

			setParameters(program);
			var insertedRows = command.ExecuteNonQuery();

			connection.Close();
			return insertedRows;
		}
	}
}
