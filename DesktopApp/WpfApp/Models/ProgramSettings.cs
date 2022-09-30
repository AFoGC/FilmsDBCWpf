using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Xml.Serialization;
using TablesLibrary.Interpreter;

namespace WpfApp.Models
{
    public class ProgramSettings
    {
        public TableCollection TableCollection { get; private set; }
        public ProfileCollectionModel Profiles { get; private set; }
        public StartUserInfo StartUser { get; set; }
        public DispatcherTimer SaveTimer { get; private set; }
        public bool IsSaveTimerEnabled { get; set; }
        public String Lang 
        { 
            get => Settings.Lang; 
            set => Settings.Lang = value; 
        }
        public int Scale
        {
            get => Settings.Scale;
            set => Settings.Scale = value;
        }
        private SettingsFields Settings { get; set; }

        static ProgramSettings()
        {
            formatter = new XmlSerializer(typeof(SettingsFields));
            instance = null;
        }

        private ProgramSettings(TableCollection collection, string profilesDirectoryPath)
        {
            TableCollection = collection;
            StartUser = new StartUserInfo();
            Profiles = new ProfileCollectionModel(profilesDirectoryPath);
            Settings = new SettingsFields();
            SaveTimer = new DispatcherTimer();
        }

        private static ProgramSettings instance;

        public static ProgramSettings Initialize(TableCollection collection, string profilesDirectoryPath, string settingsPath)
        {
            if (instance == null)
            {
                //Getting start information about program base settings from xml file
                settingPath = settingsPath;
                instance = new ProgramSettings(collection, profilesDirectoryPath);
                if (!File.Exists(settingPath))
                    instance.Settings = new SettingsFields();
                else instance.Settings = LoadSettings();

                //Setting start profile after getting information from xml file
                instance.Profiles.SetUsedProfile(instance.Settings.UsedProfile);
                if (instance.TableCollection != null)
                {
                    instance.TableCollection.TableFilePath = instance.Profiles.UsedProfile.MainFilePath;
                    instance.TableCollection.LoadTables();
                }

                //Setting save timer interval
                instance.IsSaveTimerEnabled = instance.Settings.IsSaveTimerEnabled;
                instance.SaveTimer.Interval = TimeSpan.FromSeconds(instance.Settings.SaveTimerSeconds);
            }

            return instance;
        }

        public void SaveSettings()
        {
            Settings.UsedProfile = Profiles.UsedProfile.Name;
            Settings.Lang = Thread.CurrentThread.CurrentUICulture.Name;
            Settings.SaveTimerSeconds = SaveTimer.Interval.TotalSeconds;
            Settings.IsSaveTimerEnabled = IsSaveTimerEnabled;

            using (StreamWriter fs = new StreamWriter(settingPath, false, Encoding.UTF8))
            {
                formatter.Serialize(fs, Settings);
            }
        }

        private static SettingsFields LoadSettings()
        {
            SettingsFields settings = new SettingsFields();
            using (StreamReader fs = new StreamReader(settingPath, Encoding.UTF8))
            {
                settings = (SettingsFields)formatter.Deserialize(fs);
            }

            return settings;
        }

        private static string settingPath;
        private static readonly XmlSerializer formatter;

        public class SettingsFields
        {
            public string UsedProfile { get; set; }
            public int MarkSystem { get; set; }
            public String Lang { get; set; }
            public StartUserInfo StartUser { get; set; }
            public double SaveTimerSeconds { get; set; }
            public bool IsSaveTimerEnabled { get; set; }
            public int Scale { get; set; }

            public SettingsFields()
            {
                UsedProfile = string.Empty;
                MarkSystem = 0;
                Lang = "en";
                StartUser = new StartUserInfo();
                SaveTimerSeconds = 30;
                IsSaveTimerEnabled = true;
                Scale = 1;
            }
        }

        [Serializable]
        public class StartUserInfo
        {
            public StartUserInfo()
            {
                LoggedIn = false;
                Email = string.Empty;
                Password = string.Empty;
            }
            public bool LoggedIn { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}
