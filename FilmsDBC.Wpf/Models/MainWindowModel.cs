using WpfApp.Services;

namespace WpfApp.Models
{
    public class MainWindowModel
    {
        private readonly SettingsService _settingsService;
        private readonly TablesService _tablesService;

        public MainWindowModel(SettingsService settingsService, TablesService tablesService)
        {
            _settingsService = settingsService;
            _tablesService = tablesService;
        }

        public bool IsInfoUnsaved => false;

        public void SaveSettings()
        {
            _settingsService.SaveSettings();
        }

        public void SaveTables()
        {
            _tablesService.SaveTables();
        }
    }
}
