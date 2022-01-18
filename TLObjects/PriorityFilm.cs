using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.Attributes;

namespace TL_Objects
{
	[TableCell("PriorityFilm")]
	public class PriorityFilm : Cell
	{
		private Film film;
		private int filmId = 0;

		public PriorityFilm() : base() { }
		public PriorityFilm(int id) : base(id) { }

		protected override void loadBody(Comand comand)
		{
			switch (comand.Paramert)
			{
				case "film":
					filmId = Convert.ToInt32(comand.Value);
					break;

				default:
					break;
			}
		}

		protected override void saveBody(StreamWriter streamWriter)
		{
			streamWriter.Write(FormatParam("film", filmId, 0, 2));
		}

		protected override void updateThisBody(Cell cell)
		{
			PriorityFilm priorityFilm = (PriorityFilm)cell;

			Film = priorityFilm.Film;
		}

		public Film Film
		{
			get { return film; }
			set
			{
				film = value;
				filmId = film.ID;
				OnPropertyChanged(nameof(Film));
			}
		}

		public int FilmId
		{
			get { return filmId; }
		}
	}
}
