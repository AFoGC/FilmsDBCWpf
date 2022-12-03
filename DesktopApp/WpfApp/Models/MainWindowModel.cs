using TablesLibrary.Interpreter;
using WpfApp.Services;

namespace WpfApp.Models
{
    public class MainWindowModel
    {
        private readonly SettingsService _settingsService;
        private readonly TablesFileService _tablesService;

        public MainWindowModel(SettingsService settingsService, TablesFileService tablesService)
        {
            _settingsService = settingsService;
            _tablesService = tablesService;
        }

        public bool IsInfoUnsaved => _tablesService.TablesCollection.IsInfoUnsaved;

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
