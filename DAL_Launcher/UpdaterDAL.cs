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
    public class UpdaterDAL : BaseConnection
    {
		private void setParameters(UpdaterBO updater)
		{
			SqlParameter objParam;
			objParam = command.Parameters.Add("@updaterfile", SqlDbType.VarBinary);
			objParam.Value = updater.UpdaterFile;
			objParam = command.Parameters.Add("@submit_date", SqlDbType.DateTime);
			objParam.Value = updater.SubmitDate;
			objParam = command.Parameters.Add("@version", SqlDbType.VarChar);
			objParam.Value = updater.Version;
		}

		private UpdaterBO getUpdater(SqlDataReader objReader)
		{
			UpdaterBO updater = new UpdaterBO();

			updater.ID = objReader.GetInt64(0);
			updater.UpdaterFile = (byte[])objReader.GetValue(1);
			updater.SubmitDate = objReader.GetDateTime(2);
			updater.Version = objReader.GetString(3);

			return updater;
		}

		public UpdaterBO GetLastUpdate()
		{
			UpdaterBO programBO = null;
			command.CommandText = "get_last_updater_update";
			SqlDataReader objReader = command.ExecuteReader();
			if (objReader.Read())
			{
				programBO = getUpdater(objReader);
			}
			connection.Close();

			return programBO;
		}

		public string GetLastVersion()
		{
			command.CommandText = "get_last_updater_version";
			SqlDataReader objReader = command.ExecuteReader();
			string version = String.Empty;
			if (objReader.Read())
			{
				version = objReader.GetString(0);
			}
			connection.Close();
			return version;
		}

		public int AddUpdate(UpdaterBO updater)
		{
			command.CommandText = "add_updater_update";

			setParameters(updater);
			var insertedRows = command.ExecuteNonQuery();

			connection.Close();
			return insertedRows;
		}
	}
}
