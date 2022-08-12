using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.Table;
using TablesLibrary.Interpreter.TableCell;
using TL_Objects;
using TL_Tables.Interfaces;

namespace TL_Tables
{
	public class FilmsTable : Table<Film>, IHasMarkSystem
	{
		private int markSystem;
		public bool NewMarkSystem { get; private set; }
		public int MarkSystem 
		{ 
			get => markSystem;
            set
            {
				markSystem = value;
                foreach (Film film in this)
                {
					film.FormatedMark.MarkSystem = markSystem;
                }
			}
		}

		public FilmsTable()
        {
			NewMarkSystem = false;
			MarkSystem = 6;
            this.CollectionChanged += FilmsTable_CollectionChanged;
        }

        private void FilmsTable_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
				Film film = (Film)e.NewItems[0];
				film.FormatedMark.MarkSystem = MarkSystem;
            }
        }

        protected override void saveBody(StreamWriter streamWriter)
        {
			streamWriter.Write(Cell.FormatParam("newMarkSystem", NewMarkSystem, false, 1));
			streamWriter.Write(Cell.FormatParam("markSystem", MarkSystem, 6, 1));
        }

        protected override void loadBody(Comand comand)
        {
			switch (comand.Paramert)
			{
				case "newMarkSystem":
					NewMarkSystem = Convert.ToBoolean(comand.Value);
					break;
				case "markSystem":
					MarkSystem = Convert.ToInt32(comand.Value);
					break;

				default:
					break;
			}
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

			if (!this.NewMarkSystem)
			{
				changeToNewMarkSystem();
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

		private void changeToNewMarkSystem()
        {
			foreach (Film film in this)
			{
				film.Mark *= 50;
			}

			this.NewMarkSystem = true;
		}
	}
}
