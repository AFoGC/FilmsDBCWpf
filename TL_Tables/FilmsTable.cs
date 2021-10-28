using System;
using System.Collections.Generic;
using System.Text;
using TablesLibrary.Interpreter;
using TL_Objects;

namespace TL_Tables
{
	public class FilmsTable : Table<Film>
	{
		public FilmsTable() : base() { }
		public FilmsTable(int id) : base(id) { }
		public FilmsTable(int id, string name) : base(id, name) { }

		public override void ConnectionsSubload(TableCollection tablesCollection)
		{
			Table<Genre> genresTable = tablesCollection.GetTable<Genre>();
			foreach (Film film in this)
			{
				if (film.GenreId != 0)
				{
					foreach (Genre genre in genresTable)
					{
						if (film.GenreId == genre.ID)
						{
							film.Genre = genre;
							break;
						}
					}
				}
				else
				{
					film.Genre = genresTable.DefaultCell;
				}
			}
		}
	}
}
