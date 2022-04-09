using BL_Films;
using BO_Films;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.Table;
using TablesLibrary.Interpreter.TableCell;
using TL_Objects;
using TL_Tables;
using WpfApp.Config;

namespace WpfApp.MVP.Models
{
    public class MainWindowModel
    {
        public Boolean InfoUnsaved { get; set; }
        public TableCollection TableCollection { get; private set; }
        public ProgramSettings Settings { get; private set; }
		public TLTables Tables { get; private set; }

        public event EventHandler UserChanged;
        private UserBO userBO;

        public Boolean IsLoggedIn
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
                if (null != handler) handler(null, EventArgs.Empty);
            }
        }

        public MainWindowModel()
        {
            InfoUnsaved = false;

            TableCollection = MainTabColl.GetInstance().TableCollection;
			Tables = new TLTables(TableCollection);
			TableCollection.TableSave += boolSaved;
            TableCollection.CellInTablesChanged += TableCollection_CellInTablesChanged;

			Settings = ProgramSettings.Initialize();
			if (Settings.StartUser.LoggedIn)
			{
				LoggedInUser = UserBL.LogIn(Settings.StartUser.Email, Settings.StartUser.Email);
			}

			TableCollection.FileEncoding = Encoding.UTF8;
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
