using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.Table;
using TablesLibrary.Interpreter.TableCell;
using TL_Objects;

namespace TL_Tables
{
	public class FilmsTable : Table<Film>
	{
		public FilmsTable()
        {
			NewMarkSystem = false;
			MarkSystem = 6;
        }

		public bool NewMarkSystem { get; private set; }
		public int MarkSystem { get; private set; }

        protected override void saveBody(StreamWriter streamWriter)
        {
			//streamWriter.Write(Cell.FormatParam("newMarkSystem", NewMarkSystem, false, 1));
        }

        protected override void loadBody(Comand comand)
        {
			/*
			switch (comand.Paramert)
			{
				case "newMarkSystem":
					NewMarkSystem = Convert.ToBoolean(comand.Value);
					break;

				default:
					break;
			}
			*/
		}

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

		public bool GenreHasFilm(Genre genre)
        {
			bool hasGenre = false;

            foreach (Film film in this)
            {
                if (film.Genre == genre)
                {
					return true;
                }
            }

			return hasGenre;
        }
	}
}
