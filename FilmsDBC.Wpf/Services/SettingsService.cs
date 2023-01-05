using System;
using System.IO;
using System.Xml;
using WpfApp.Helper;

namespace WpfApp.Services
{
    public class SettingsService
    {
        private const string mainNodeName = "SettingsFields";
        private const string profileNodeName = "UsedProfile";
        private const string langNodeName = "Lang";
        private const string scaleNodeName = "Scale";
        private const string autosaveNodeName = "IsSaveTimerEnabled";
        private const string autosaveSecondsNodeName = "SaveTimerSeconds";

        private readonly XmlDocument _settingsXml;
        private TablesService _tablesService;
        private ProfilesService _profilesService;
        private LanguageService _languageService;
        private ScaleService _scaleService;

        public SettingsService(TablesService tablesService,
                                LanguageService languageService,
                                ProfilesService profilesService,
                                ScaleService scaleService)
        {
            _tablesService = tablesService;
            _profilesService = profilesService;
            _languageService = languageService;
            _scaleService = scaleService;

            _settingsXml = new XmlDocument();

            _profilesService.UsedProfileChanged += OnProfileChanged;
            _tablesService.AutosaveIsEnableChanged += OnAutosaveEnableChanged;
            _tablesService.AutosaveIntervalChanged += OnAutosaveIntervalChanged;

            _languageService.LanguageChanged += (culture) => OnLanguageChanged();
            _scaleService.ScaleChanged += (scale) => OnScaleChanged();
        }

        public TablesService TablesService => _tablesService;
        public ProfilesService ProfilesService => _profilesService;
        public LanguageService LanguageService => _languageService;
        public ScaleService ScaleService => _scaleService;

        private void OnScaleChanged()
        {
            XmlNode node = GetXmlNode(scaleNodeName);
            int scaleCode = (int)_scaleService.CurrentScale;
            node.InnerText = scaleCode.ToString();
        }

        private void OnLanguageChanged()
        {
            XmlNode node = GetXmlNode(langNodeName);
            node.InnerText = _languageService.CurrentLanguage.Name;
        }

        private void OnProfileChanged()
        {
            XmlNode node = GetXmlNode(profileNodeName);
            node.InnerText = _profilesService.UsedProfile.Name;
        }

        private void OnAutosaveEnableChanged()
        {
            XmlNode node = GetXmlNode(autosaveNodeName);
            node.InnerText = _tablesService.IsAutosaveEnable.ToString();
        }

        private void OnAutosaveIntervalChanged()
        {
            XmlNode node = GetXmlNode(autosaveSecondsNodeName);
            node.InnerText = _tablesService.SaveTimerInterval.ToString();
        }

        public void SaveSettings()
        {
            _settingsXml.Save(PathHelper.SettingsPath);
        }

        public void LoadSettings()
        {
            if (LoadDocument() == false)
            {
                OnScaleChanged();
                OnLanguageChanged();
                OnProfileChanged();
                OnAutosaveEnableChanged();
                OnAutosaveIntervalChanged();
            }

            GetXmlProfile();
            GetXmlLang();
            GetXmlScale();
            GetXmlAutosaveEnabled();
            GetXmlAutosaveSeconds();
        }

        private bool LoadDocument()
        {
            if (File.Exists(PathHelper.SettingsPath))
            {
                _settingsXml.Load(PathHelper.SettingsPath);
                return true;
            }

            return false;
        }

        private void GetXmlProfile()
        {
            XmlNode node = GetXmlNode(profileNodeName);
            _profilesService.SetUsedProfile(node.InnerText);
        }

        private void GetXmlLang()
        {
            XmlNode node = GetXmlNode(langNodeName);
            _languageService.SetLanguage(node.InnerText);
        }

        private void GetXmlScale()
        {
            XmlNode node = GetXmlNode(scaleNodeName);
            int scaleCode = Int32.Parse(node.InnerText);
            _scaleService.SetScale(scaleCode);
        }

        private void GetXmlAutosaveEnabled()
        {
            XmlNode node = GetXmlNode(autosaveNodeName);
            _tablesService.IsAutosaveEnable = Boolean.Parse(node.InnerText);
        }

        private void GetXmlAutosaveSeconds()
        {
            XmlNode node = GetXmlNode(autosaveSecondsNodeName);
            _tablesService.SaveTimerInterval = Double.Parse(node.InnerText);
        }

        private XmlNode GetXmlNode(string name)
        {
            XmlNode mainNode = GetMainNode();
            XmlNode node = mainNode.SelectSingleNode(name);

            if (node == null)
            {
                node = _settingsXml.CreateElement(name);
                node = mainNode.AppendChild(node);
            }

            return node;
        }

        private XmlNode GetMainNode()
        {
            XmlNode node = _settingsXml.SelectSingleNode(mainNodeName);

            if (node == null || node.Name != mainNodeName)
            {
                node = _settingsXml.CreateElement(mainNodeName);
                node = _settingsXml.AppendChild(node);
            }

            return node;
        }
    }
}
