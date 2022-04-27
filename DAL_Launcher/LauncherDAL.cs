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
    public class LauncherDAL : BaseConnection
    {
		private void setParameters(LauncherBO launcher)
		{
			SqlParameter objParam;
			objParam = command.Parameters.Add("@launcherfile", SqlDbType.VarBinary);
			objParam.Value = launcher.LauncherFile;
		}

		private LauncherBO getLauncher(SqlDataReader objReader)
		{
			LauncherBO program = new LauncherBO();

			program.ID = objReader.GetInt64(0);
			program.LauncherFile = (byte[])objReader.GetValue(1);

			return program;
		}

		public LauncherBO GetLastUpdate()
		{
			LauncherBO launcherBO = null;
			command.CommandText = "get_last_launcher_update";
			SqlDataReader objReader = command.ExecuteReader();
			if (objReader.Read())
			{
				launcherBO = getLauncher(objReader);
			}
			connection.Close();

			return launcherBO;
		}

		public int AddUpdate(LauncherBO launcher)
		{
			command.CommandText = "add_launcher_update";

			setParameters(launcher);
			var insertedRows = command.ExecuteNonQuery();

			connection.Close();
			return insertedRows;
		}
	}
}
