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
			objParam = command.Parameters.Add("@submit_date", SqlDbType.DateTime);
			objParam.Value = program.SubmitDate;
			objParam = command.Parameters.Add("@version", SqlDbType.VarChar);
			objParam.Value = program.Version;
			objParam = command.Parameters.Add("@zipfile", SqlDbType.VarBinary);
			objParam.Value = (object)program.ZipFile ?? DBNull.Value;

		}

		private ProgramBO getProfile(SqlDataReader objReader)
		{
			ProgramBO program = new ProgramBO();

			program.ID = objReader.GetInt64(0);
			program.UpdateInfo = objReader.GetString(1);
			program.ProgramFile = (byte[])objReader.GetValue(2);
			program.SubmitDate = objReader.GetDateTime(3);
			program.Version = objReader.GetString(4);
			object value = objReader.GetValue(5);
			if (value != DBNull.Value)
				program.ZipFile = (byte[])value;

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

		public List<ProgramBO> GetPatchNotes()
        {
            List<ProgramBO> programs = new List<ProgramBO>();
            command.CommandText = "get_last_update_info";
            SqlDataReader objReader = command.ExecuteReader();
            ProgramBO program;
            while (objReader.Read())
			{
				program = new ProgramBO
				{
					UpdateInfo = objReader.GetString(0),
					Version = objReader.GetString(1),
                    ID = objReader.GetInt64(2)
                };
				programs.Add(program);
			}
			connection.Close();

			return programs;
		}

		public List<ProgramBO> GetNextPatchNotes(long last_id)
		{
			List<ProgramBO> programs = new List<ProgramBO>();
            command.CommandText = "get_next_patch_notes";

            SqlParameter objParam = command.Parameters.Add("@start_index", SqlDbType.BigInt);
            objParam.Value = last_id;

            SqlDataReader objReader = command.ExecuteReader();
            ProgramBO program;
            while (objReader.Read())
            {
                program = new ProgramBO
                {
                    UpdateInfo = objReader.GetString(0),
                    Version = objReader.GetString(1),
					ID = objReader.GetInt64(2)
                };
                programs.Add(program);
            }
            connection.Close();

            return programs;
        }


        public string GetLastVersion()
        {
			command.CommandText = "get_last_prog_version";
			SqlDataReader objReader = command.ExecuteReader();
			string version = String.Empty;
			if (objReader.Read())
			{
				version = objReader.GetString(0);
			}
			connection.Close();
			return version;
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
