using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.Attributes;
using TL_Objects.CellDataClasses;

namespace TL_Objects
{
	[TableCell("Film")]
	public class Film : Cell
	{
		public String Name { get; set; }			= "";
		public Genre Genre
		{
			get
			{
				if (genre != null)
				{
					return genre;
				}
				else
				{
					return new Genre(0);
				}
			}
            set
            {
				genre = value;
				GenreId = genre.ID;
			}
		}
		private Genre genre;
		public int GenreId { get; private set; }	= 0;
		public int RealiseYear { get; set; }		= 0;
		public bool Watched { get; set; }			= false;
		public int Mark { get; set; }				= -1;
		public DateTime DateOfWatch { get; set; }	= new DateTime();
		public String Comment { get; set; }			= "";
		public List<Source> Sources { get; set; }	= new List<Source>();
		public int CountOfViews { get; set; }		= 0;
		public int FranshiseId { get; set; }		= 0;
		public int FranshiseListIndex { get; set; } = -1;


		public Film() : base() { }
		public Film(int id) : base(id) { }


		protected override void updateThisBody(Cell cell)
		{
			Film film = (Film)cell;

			Name = film.Name;
			Genre = film.Genre;
			RealiseYear = film.RealiseYear;
			Watched = film.Watched;
			Mark = film.Mark;
			DateOfWatch = film.DateOfWatch;
			Comment = film.Comment;
			Sources = film.Sources;
			CountOfViews = film.CountOfViews;
			FranshiseId = film.FranshiseId;
			FranshiseListIndex = film.FranshiseListIndex;
		}

		protected override void loadBody(Comand comand)
		{
			switch (comand.Paramert)
			{
				case "name":
					Name = comand.Value;
					break;
				case "genre":
					GenreId = Convert.ToInt32(comand.Value);
					break;
				case "realiseYear":
					RealiseYear = Convert.ToInt32(comand.Value);
					break;
				case "watched":
					Watched = Convert.ToBoolean(comand.Value);
					break;
				case "mark":
					Mark = Convert.ToInt32(comand.Value);
					break;
				case "dateOfWatch":
					DateOfWatch = readDate(comand.Value);
					break;
				case "comment":
					Comment = comand.Value;
					break;
				case "sourceUrl":
					Sources.Add(Source.ToSource(comand.Value));
					break;
				case "countOfviews":
					CountOfViews = Convert.ToInt32(comand.Value);
					break;
				case "franshiseId":
					FranshiseId = Convert.ToInt32(comand.Value);
					break;
				case "franshiseListIndex":
					FranshiseListIndex = Convert.ToInt32(comand.Value);
					break;

				default:
					break;
			}
		}

		private Source defSource = new Source();
		protected override void saveBody(StreamWriter streamWriter)
		{
			streamWriter.Write(FormatParam("name", Name, "", 2));
			streamWriter.Write(FormatParam("genre", Genre.ID, 0, 2));
			streamWriter.Write(FormatParam("realiseYear", RealiseYear, 0, 2));
			streamWriter.Write(FormatParam("watched", Watched, false, 2));
			streamWriter.Write(FormatParam("mark", Mark, -1, 2));
			streamWriter.Write(FormatParam("dateOfWatch", DateOfWatch, new DateTime(), 2));
			streamWriter.Write(FormatParam("comment", Comment, "", 2));

			foreach (Source source in Sources)
			{
				streamWriter.Write(FormatParam("sourceUrl", source, defSource, 2));
			}

			streamWriter.Write(FormatParam("countOfviews", CountOfViews, 0, 2));
			streamWriter.Write(FormatParam("franshiseId", FranshiseId, 0, 2));
			streamWriter.Write(FormatParam("franshiseListIndex", FranshiseListIndex, -1, 2));
		}


	}
}
