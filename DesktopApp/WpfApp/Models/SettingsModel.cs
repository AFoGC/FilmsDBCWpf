using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Xml.Serialization;
using TablesLibrary.Interpreter;
using WpfApp.Properties;

namespace WpfApp.Models
{
    public class SettingsModel
    {
        public TableCollection TableCollection { get; private set; }
        public ProfileCollection Profiles { get; private set; }
        public StartUserInfo StartUser { get; set; }
        public DispatcherTimer SaveTimer { get; private set; }
        private SettingsFields Settings { get; set; }
        public List<CultureInfo> Cultures { get; private set; }
        public CultureInfo Language
        {
            get
            {
                return Thread.CurrentThread.CurrentUICulture;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                if (value == Thread.CurrentThread.CurrentUICulture) return;

                //1. Change Application Language:
                Thread.CurrentThread.CurrentUICulture = value;

                //2. Creating ResourceDictionary for new culture
                ResourceDictionary dict = new ResourceDictionary();
                if (Cultures.Contains(value) && value.Name != "en")
                {
                    dict.Source = new Uri(String.Format("Resources/Localizations/lang.{0}.xaml", value.Name), UriKind.Relative);
                }
                else
                {
                    dict.Source = new Uri("Resources/Localizations/lang.xaml", UriKind.Relative);
                }

                //3. Find old ResourceDictionary delete it and add new ResourceDictionary
                ResourceDictionary oldDict = (from d in Application.Current.Resources.MergedDictionaries
                                              where d.Source != null && d.Source.OriginalString.StartsWith("Resources/Localizations/lang.")
                                              select d).First();
                if (oldDict != null)
                {
                    int ind = Application.Current.Resources.MergedDictionaries.IndexOf(oldDict);
                    Application.Current.Resources.MergedDictionaries.Remove(oldDict);
                    Application.Current.Resources.MergedDictionaries.Insert(ind, dict);
                }
                else
                {
                    Application.Current.Resources.MergedDictionaries.Add(dict);
                }

                SaveSettings();
            }
        }
        public ScaleEnum Scale
        {
            get => (ScaleEnum)Settings.Scale;
            set
            {
                if (Enum.IsDefined(typeof(ScaleEnum), value))
                {
                    Settings.Scale = (int)value;
                    ResourceDictionary dict = new ResourceDictionary();
                    dict.Source = new Uri(String.Format("Resources/Dictionaries/TableControls/Scale.{0}.xaml", value), UriKind.Relative);

                    ResourceDictionary oldDict =
                        (from d in Application.Current.Resources.MergedDictionaries
                         where d.Source != null && d.Source.OriginalString.StartsWith("Resources/Dictionaries/TableControls/Scale.")
                         select d).First();

                    if (oldDict != null)
                    {
                        int ind = Application.Current.Resources.MergedDictionaries.IndexOf(oldDict);
                        Application.Current.Resources.MergedDictionaries.Remove(oldDict);
                        Application.Current.Resources.MergedDictionaries.Insert(ind, dict);
                    }
                    else
                    {
                        Application.Current.Resources.MergedDictionaries.Add(dict);
                    }
                }
            }
        }


        static SettingsModel()
        {
            formatter = new XmlSerializer(typeof(SettingsFields));
            instance = null;
        }

        private SettingsModel()
        {
            //Initializing tables collection and settings file path
            TableCollection collection = TLTables.GetDefaultTableCollectionData();
            string profilesDirectoryPath =
                Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            settingPath = Path.Combine(profilesDirectoryPath, "ProgramSetting.xml");

            TableCollection = collection;
            StartUser = new StartUserInfo();
            Profiles = new ProfileCollection(profilesDirectoryPath);
            Settings = new SettingsFields();
            SaveTimer = new DispatcherTimer();

            //Initializing Languages
            Cultures = new List<CultureInfo>();
            Cultures.Add(new CultureInfo("en"));
            Cultures.Add(new CultureInfo("ru"));
            Cultures.Add(new CultureInfo("uk-UA"));
        }

        private static SettingsModel instance;

        public static SettingsModel Initialize()
        {
            if (instance == null)
            {
                //Getting start information about program base settings from xml file
                instance = new SettingsModel();
                if (!File.Exists(settingPath))
                    instance.Settings = new SettingsFields();
                else instance.Settings = LoadSettings();

                //Setting start profile after getting information from xml file
                instance.Profiles.SetUsedProfile(instance.Settings.UsedProfile);
                instance.TableCollection.TableFilePath = instance.Profiles.UsedProfile.MainFilePath;
                instance.TableCollection.LoadTables();

                //Setting save timer interval
                instance.SaveTimer.IsEnabled = instance.Settings.IsSaveTimerEnabled;
                instance.SaveTimer.Interval = TimeSpan.FromSeconds(instance.Settings.SaveTimerSeconds);

                //Set Launguage
                instance.Language = new CultureInfo(instance.Settings.Lang);
            }

            return instance;
        }

        public void SaveSettings()
        {
            Settings.UsedProfile = Profiles.UsedProfile.Name;
            Settings.Lang = Language.Name;
            Settings.SaveTimerSeconds = SaveTimer.Interval.TotalSeconds;
            Settings.IsSaveTimerEnabled = SaveTimer.IsEnabled;

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

    public enum ScaleEnum
    {
        Small = 0,
        Medium = 1
    }
}
