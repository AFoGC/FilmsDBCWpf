using BL_Films;
using BO_Films;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.Table;
using TablesLibrary.Interpreter.TableCell;
using TL_Objects;
using TL_Tables;

namespace WpfApp.Models
{
    public class MainWindowModel
    {
        public TableCollection TableCollection { get; private set; }
        public DispatcherTimer SaveTimer { get; private set; }

        public Action SaveSettings { get; private set; }
        public MainWindowModel()
        {
            SettingsModel settings = SettingsModel.Initialize();
            SaveTimer = settings.SaveTimer;
            TableCollection = settings.TableCollection;

            SaveSettings = settings.SaveSettings;
        }
    }
}
