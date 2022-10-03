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
        public SettingsModel Settings { get; private set; }
        public TLTables Tables { get; private set; }

        public MainWindowModel()
        {
            
        }
    }
}
