using BO_Films;
using ProfilesConfig;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter;

namespace MobileApp.Models
{
    public class MainModel
    {
        public TableCollection TableCollection { get; private set; }
        public TLTables Tables { get; private set; }
        public ProgramSettings Settings { get; private set; }

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
                if (null != handler) handler(userBO, EventArgs.Empty);
            }
        }

        public MainModel()
        {
            TableCollection = new TableCollection();
            TableCollection.FileEncoding = Encoding.UTF8;
            Tables = new TLTables(TableCollection);

            String localPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal, Environment.SpecialFolderOption.Create);

            Settings = ProgramSettings.Initialize(TableCollection, localPath, Path.Combine(localPath, "ProgramSetting.xml"));
        }
    }
}
