using ProfilesConfig;
using System;
using System.Collections.Generic;
using System.Text;
using TablesLibrary.Interpreter;

namespace MobileApp.Models
{
    public class MainModel
    {
        public TableCollection TableCollection { get; private set; }
        public TLTables Tables { get; private set; }
        //public 

        public MainModel()
        {
            TableCollection = new TableCollection();
            TableCollection.FileEncoding = Encoding.UTF8;
            Tables = new TLTables(TableCollection);
            
        }
    }
}
