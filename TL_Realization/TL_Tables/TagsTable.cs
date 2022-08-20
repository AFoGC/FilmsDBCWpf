using System;
using System.Collections.Generic;
using System.Text;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.Table;
using TL_Objects;

namespace TL_Tables
{
    public class TagsTable : Table<Tag>
    {
        public override void ConnectionsSubload(TableCollection tablesCollection)
        {
            throw new NotImplementedException();
        }
    }
}
