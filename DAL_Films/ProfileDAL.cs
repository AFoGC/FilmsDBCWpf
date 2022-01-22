using BO_Films;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Films
{
	public class ProfileDAL : BaseConnection
	{
		private ProfileBO getProfile(SqlDataReader objReader)
		{
			ProfileBO profile = new ProfileBO();
			object ob = null;

			profile.Id = objReader.GetInt32(0);
			profile.Name = objReader.GetString(1);
			if ((ob = objReader.GetValue(2)) != DBNull.Value)
				profile.Lastsave = (byte[])ob;
			profile.UserId = objReader.GetInt32(3);

			return profile;
		}

		private void setParameters(ProfileBO profile)
		{
			SqlParameter objParam;
			objParam = command.Parameters.Add("@name", SqlDbType.NVarChar);
			objParam.Value = profile.Name;
			objParam = command.Parameters.Add("@lastsave", SqlDbType.NVarChar);
			objParam.Value = profile.Lastsave;
			objParam = command.Parameters.Add("@user_id", SqlDbType.NVarChar);
			objParam.Value = profile.UserId;
		}

		public ProfileBO[] GetAllUserProfiles(long user_id)
        {
			List<ProfileBO> profiles = new List<ProfileBO>();

			command.CommandText = "Profiles_GetAllUserProfiles";
			SqlParameter objParam;
			objParam = command.Parameters.Add("@user_id", SqlDbType.BigInt);
			objParam.Value = user_id;

			SqlDataReader objReader = command.ExecuteReader();

			while (objReader.Read())
			{
				profiles.Add(getProfile(objReader));
			}
			connection.Close();
			return profiles.ToArray();
		}

		public int Add(ProfileBO profile)
        {
			command.CommandText = "Profiles_Add";

			setParameters(profile);
			var insertedRows = command.ExecuteNonQuery();

			connection.Close();
			return insertedRows;
		}

		public int DeleteByID(long id)
        {
			command.CommandText = "Profiles_Delete";
			SqlParameter objParam = command.Parameters.Add("@id", SqlDbType.BigInt);
			objParam.Value = id;

			int result = command.ExecuteNonQuery();
			connection.Close();
			return result;
		}

		public int Update(ProfileBO profile)
        {
			command.CommandText = "Profiles_Update";
			setParameters(profile);
			var insertedRows = command.ExecuteNonQuery();
			connection.Close();
			return insertedRows;
		}
	}
}
