using BL_Films;
using BO_Films;
using ProfilesConfig;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.Table;
using TablesLibrary.Interpreter.TableCell;
using TL_Objects;
using TL_Tables;

namespace WpfApp.Models
{
    public class MainWindowModel : IUserModel
    {
        public bool InfoUnsaved { get; set; }
        public TableCollection TableCollection { get; private set; }
        public ProgramSettings Settings { get; private set; }
        public TLTables Tables { get; private set; }

        public event EventHandler UserChanged;
        private UserBO userBO;

        public bool IsLoggedIn
        {
            get
            {
                return LoggedInUser != null;
            }
        }
        public UserBO LoggedInUser
        {
            get { return userBO; }
            set
            {
                userBO = value;
                EventHandler handler = UserChanged;
                if (null != handler) handler(userBO, EventArgs.Empty);
            }
        }

        public MainWindowModel()
        {
            InfoUnsaved = false;

            TableCollection = new TableCollection();
            Tables = new TLTables(TableCollection);
            TableCollection.TableSave += boolSaved;
            TableCollection.CellInTablesChanged += TableCollection_CellInTablesChanged;

            string localPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            Settings = ProgramSettings.Initialize(TableCollection, localPath, Path.Combine(localPath, "ProgramSetting.xml"));
            if (Settings.StartUser.LoggedIn)
            {
                LoggedInUser = UserBL.LogIn(Settings.StartUser.Email, Settings.StartUser.Email);
            }

            TableCollection.FileEncoding = Encoding.UTF8;
            InfoUnsaved = false;
        }

        private void TableCollection_CellInTablesChanged(object sender, EventArgs e)
        {
            InfoUnsaved = true;
        }

        private void boolSaved(object sender, EventArgs e)
        {
            InfoUnsaved = false;
        }
    }
}
