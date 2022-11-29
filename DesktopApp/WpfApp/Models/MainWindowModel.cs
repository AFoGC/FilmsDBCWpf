using System.Windows.Threading;
using TablesLibrary.Interpreter;
using WpfApp.Services;

namespace WpfApp.Models
{
    public class MainWindowModel
    {
        private readonly SettingsService _settingsService;

        public MainWindowModel(SettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public TableCollection TableCollection => 
               _settingsService.TablesService.TablesCollection;

        public void SaveSettings() => _settingsService.SaveSettings();
    }
}
