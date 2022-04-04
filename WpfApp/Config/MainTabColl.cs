using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter;

namespace WpfApp.Config
{
    public class MainTabColl
    {
        private static MainTabColl instance;
        public TableCollection TableCollection { get; private set; }

        private MainTabColl()
        {
            TableCollection = new TableCollection();
        }

        public static MainTabColl GetInstance()
        {
            if (instance == null)
                instance = new MainTabColl();
            return instance;
        }
    }
}
