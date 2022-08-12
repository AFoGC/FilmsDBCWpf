using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.TableCell;
using TL_Objects.CellDataClasses;

namespace TL_Objects
{
	[TableCell("Film")]
	public class Film : Cell
	{
		private string name	= "";
		private Genre genre	= null;
		private int genreId	= 0;
		private int realiseYear	= 0;
		private bool watched = false;
		private Mark mark = new Mark();
		private DateTime dateOfWatch = new DateTime();

		private string comment = "";
		private ObservableCollection<Source> sources = new ObservableCollection<Source>();

		private int countOfviews = 0;
		private int franshiseId = 0;
		private sbyte franshiseListIndex = -1;

		public Serie Serie { get; internal set; }

		public Film() : base()
		{ 
			sources.CollectionChanged += Sources_CollectionChanged;
            mark.PropertyChanged += Mark_PropertyChanged;
		}

        private void Mark_PropertyChanged(object sender, EventArgs e)
        {
			OnPropertyChanged(nameof(Mark));
			OnPropertyChanged(nameof(FormatedMark));
		}

        private void Sources_CollectionChanged(object sender, EventArgs e)
		{
			this.OnPropertyChanged(nameof(Sources));
		}

		protected override void updateThisBody(Cell cell)
		{
			Film film = (Film)cell;

			name = film.name;
			Genre = film.genre;
			realiseYear = film.realiseYear;
			watched = film.watched;
			mark = film.mark;
			dateOfWatch = film.dateOfWatch;
			comment = film.comment;
			sources = film.sources;
			countOfviews = film.countOfviews;
			franshiseId = film.franshiseId;
			franshiseListIndex = film.franshiseListIndex;
		}

		private Source defSource = new Source();
		protected override void saveBody(StreamWriter streamWriter, Cell defaultCell)
		{
			streamWriter.Write(FormatParam("name", name, "", 2));
			streamWriter.Write(FormatParam("genre", genre.ID, 0, 2));
			streamWriter.Write(FormatParam("realiseYear", realiseYear, 0, 2));
			streamWriter.Write(FormatParam("watched", watched, false, 2));
			streamWriter.Write(FormatParam("mark", mark.RawMark, 0, 2));
			streamWriter.Write(FormatParam("dateOfWatch", dateOfWatch, new DateTime(), 2));
			streamWriter.Write(FormatParam("comment", comment, "", 2));

			foreach (Source source in sources)
			{
				streamWriter.Write(FormatParam("sourceUrl", source, defSource, 2));
			}

			streamWriter.Write(FormatParam("countOfviews", countOfviews, 0, 2));
			streamWriter.Write(FormatParam("franshiseId", franshiseId, 0, 2));
			streamWriter.Write(FormatParam("franshiseListIndex", franshiseListIndex, -1, 2));
		}

		protected override void loadBody(Comand comand)
		{
			switch (comand.Paramert)
			{
				case "name":
					name = comand.Value;
					break;
				case "genre":
					genreId = Convert.ToInt32(comand.Value);
					break;
				case "realiseYear":
					realiseYear = Convert.ToInt32(comand.Value);
					break;
				case "watched":
					watched = Convert.ToBoolean(comand.Value);
					break;
				case "mark":
					mark.RawMark = Convert.ToInt32(comand.Value);
					break;
				case "dateOfWatch":
					dateOfWatch = Convert.ToDateTime(comand.Value);
					break;
				case "comment":
					comment = comand.Value;
					break;
				case "sourceUrl":
					sources.Add(Source.ToSource(comand.Value));
					break;
				case "countOfviews":
					countOfviews = Convert.ToInt32(comand.Value);
					break;
				case "franshiseId":
					franshiseId = Convert.ToInt32(comand.Value);
					break;
				case "franshiseListIndex":
					franshiseListIndex = Convert.ToSByte(comand.Value);
					break;

				default:
					break;
			}
		}

		public string Name
		{
			get { return name; }
			set { name = value; OnPropertyChanged(nameof(Name)); }
		}

		public Genre Genre
		{
			get
			{
				return genre;
			}
			set
			{
				genre = value;
				genreId = genre.ID;
				OnPropertyChanged(nameof(Genre));
			}
		}

		public int RealiseYear
		{
			get { return realiseYear; }
			set { realiseYear = value; OnPropertyChanged(nameof(RealiseYear)); }
		}

		public bool Watched
		{
			get { return watched; }
			set { watched = value; OnPropertyChanged(nameof(Watched)); }
		}

		public int Mark
		{
			get { return mark.RawMark; }
			set { mark.RawMark = value; OnPropertyChanged(nameof(Mark)); OnPropertyChanged(nameof(FormatedMark)); }
		}

		public Mark FormatedMark
        {
			get => mark;
        }

		public DateTime DateOfWatch
		{
			get { return dateOfWatch; }
			set { dateOfWatch = value; OnPropertyChanged(nameof(DateOfWatch)); }
		}

		public string Comment
		{
			get { return comment; }
			set { comment = value; OnPropertyChanged(nameof(Comment)); }
		}

		public ObservableCollection<Source> Sources
		{
			get { return sources; }
			set { sources = value; OnPropertyChanged(nameof(Sources)); }
		}

		public int CountOfViews
		{
			get { return countOfviews; }
			set { countOfviews = value; OnPropertyChanged(nameof(CountOfViews)); }
		}

		public int FranshiseId
		{
			get { return franshiseId; }
			set
			{
				franshiseId = value;
				OnPropertyChanged(nameof(FranshiseId));
			}
		}

		public sbyte FranshiseListIndex
		{
			get { return franshiseListIndex; }
			set { franshiseListIndex = value; OnPropertyChanged(nameof(FranshiseListIndex)); }
		}

		public int GenreId
		{
			get { return genreId; }
		}
	}
}
