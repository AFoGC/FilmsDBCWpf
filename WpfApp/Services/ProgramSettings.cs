﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TablesLibrary.Interpreter;
using WpfApp.Models;

namespace WpfApp.Services
{
	[Serializable]
	public class ProgramSettings
	{
		public TableCollection TableCollection { get; private set; }
		public ProfileCollectionModel Profiles { get; private set; }
		public int MarkSystem { get => settingsFields.MarkSystem; set => settingsFields.MarkSystem = value; }
		public StartUserInfo StartUser { get => settingsFields.StartUser; set => settingsFields.StartUser = value; }


		private SettingsFields settingsFields;
		public ProfileModel UsedProfile
		{
			get => Profiles.UsedProfile;
		}

		public void SetUsedProfile(ProfileModel profile)
		{
			Profiles.SetUsedProfile(profile);
			if (TableCollection != null)
			{
				TableCollection.TableFilePath = UsedProfile.MainFilePath;
				TableCollection.LoadTables();
			}
			settingsFields.UsedProfile = UsedProfile.Name;
		}

		public void SetUsedProfile(String profileName)
		{
			Profiles.SetUsedProfile(profileName);
			if (TableCollection != null)
			{
				TableCollection.TableFilePath = UsedProfile.MainFilePath;
				TableCollection.LoadTables();
			}
			settingsFields.UsedProfile = UsedProfile.Name;
		}

		static ProgramSettings()
		{
			formatter = new XmlSerializer(typeof(SettingsFields));
			instance = null;
		}

		private ProgramSettings(TableCollection collection, String profilesDirectoryPath)
		{
			TableCollection = collection;
			this.Profiles = new ProfileCollectionModel(profilesDirectoryPath);
			this.settingsFields = new SettingsFields();
		}

		private static ProgramSettings instance;

		public static ProgramSettings Initialize(TableCollection collection, String profilesDirectoryPath, String settingsPath)
		{
			if (instance == null)
			{
				settingPath = settingsPath;
				instance = new ProgramSettings(collection, profilesDirectoryPath);
				if (!File.Exists(settingPath))
					instance.settingsFields = new SettingsFields();
				else instance.settingsFields = LoadSettings();
			}

			instance.SetUsedProfile(instance.settingsFields.UsedProfile);

			return instance;
		}

		public void SaveSettings()
		{
			using (StreamWriter fs = new StreamWriter(settingPath, false, Encoding.UTF8))
			{
				formatter.Serialize(fs, settingsFields);
			}
		}

		public static SettingsFields LoadSettings()
		{
			SettingsFields settings = new SettingsFields();
			using (StreamReader fs = new StreamReader(settingPath, Encoding.UTF8))
			{
				settings = (SettingsFields)formatter.Deserialize(fs);
			}

			return settings;
		}

		private static String settingPath;
		private static readonly XmlSerializer formatter;
	}
}
