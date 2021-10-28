using System;
using System.Collections.Generic;
using System.Text;
using TablesLibrary.Interpreter;
using TL_Objects;

namespace TL_Tables
{
	public class BookGenresTable : Table<BookGenre>
	{
		public BookGenresTable() : base() { }
		public BookGenresTable(int id) : base(id) { }
		public BookGenresTable(int id, string name) : base(id, name) { }

		public override void ConnectionsSubload(TableCollection tablesCollection)
		{

		}
	}
}
