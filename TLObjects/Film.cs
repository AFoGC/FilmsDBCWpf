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
		private string _name;
		private Genre _genre;
		private int _genreId;
		private int _realiseYear;
		private bool _watched;
		private Mark _mark;
		private DateTime _dateOfWatch;

		private string _comment;
		private ObservableCollection<Source> _sources;

		private int _countOfviews;
		private int _franshiseId;
		private sbyte _franshiseListIndex;

		public Serie Serie { get; internal set; }

		public Film()
		{
			_name = String.Empty;
			_genre = null;
			_genreId = 0;
			_realiseYear = 0;
			_watched = false;
			_mark = new Mark();
			_dateOfWatch = new DateTime();
			_comment = String.Empty;
			_sources = new ObservableCollection<Source>();
			_countOfviews = 0;
			_franshiseId = 0;
			_franshiseListIndex = -1;

			_sources.CollectionChanged += Sources_CollectionChanged;
            _mark.PropertyChanged += Mark_PropertyChanged;
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

			_name = film._name;
			Genre = film._genre;
			_realiseYear = film._realiseYear;
			_watched = film._watched;
			_mark = film._mark;
			_dateOfWatch = film._dateOfWatch;
			_comment = film._comment;
			_sources = film._sources;
			_countOfviews = film._countOfviews;
			_franshiseId = film._franshiseId;
			_franshiseListIndex = film._franshiseListIndex;
		}

		private Source defSource = new Source();
		protected override void saveBody(StreamWriter streamWriter, Cell defaultCell)
		{
			streamWriter.Write(FormatParam("name", _name, String.Empty, 2));
			streamWriter.Write(FormatParam("genre", _genre.ID, 0, 2));
			streamWriter.Write(FormatParam("realiseYear", _realiseYear, 0, 2));
			streamWriter.Write(FormatParam("watched", _watched, false, 2));
			streamWriter.Write(FormatParam("mark", _mark.RawMark, 0, 2));
			streamWriter.Write(FormatParam("dateOfWatch", _dateOfWatch, new DateTime(), 2));
			streamWriter.Write(FormatParam("comment", _comment, String.Empty, 2));

			foreach (Source source in _sources)
			{
				streamWriter.Write(FormatParam("sourceUrl", source, defSource, 2));
			}

			streamWriter.Write(FormatParam("countOfviews", _countOfviews, 0, 2));
			streamWriter.Write(FormatParam("franshiseId", _franshiseId, 0, 2));
			streamWriter.Write(FormatParam("franshiseListIndex", _franshiseListIndex, -1, 2));
		}

		protected override void loadBody(Comand comand)
		{
			switch (comand.Paramert)
			{
				case "name":
					_name = comand.Value;
					break;
				case "genre":
					_genreId = Convert.ToInt32(comand.Value);
					break;
				case "realiseYear":
					_realiseYear = Convert.ToInt32(comand.Value);
					break;
				case "watched":
					_watched = Convert.ToBoolean(comand.Value);
					break;
				case "mark":
					_mark.RawMark = Convert.ToInt32(comand.Value);
					break;
				case "dateOfWatch":
					_dateOfWatch = Convert.ToDateTime(comand.Value);
					break;
				case "comment":
					_comment = comand.Value;
					break;
				case "sourceUrl":
					_sources.Add(Source.ToSource(comand.Value));
					break;
				case "countOfviews":
					_countOfviews = Convert.ToInt32(comand.Value);
					break;
				case "franshiseId":
					_franshiseId = Convert.ToInt32(comand.Value);
					break;
				case "franshiseListIndex":
					_franshiseListIndex = Convert.ToSByte(comand.Value);
					break;

				default:
					break;
			}
		}

		public string Name
		{
			get { return _name; }
			set { _name = value; OnPropertyChanged(nameof(Name)); }
		}

		public Genre Genre
		{
			get
			{
				return _genre;
			}
			set
			{
				_genre = value;
				_genreId = _genre.ID;
				OnPropertyChanged(nameof(Genre));
			}
		}

		public int RealiseYear
		{
			get { return _realiseYear; }
			set { _realiseYear = value; OnPropertyChanged(nameof(RealiseYear)); }
		}

		public bool Watched
		{
			get { return _watched; }
			set { _watched = value; OnPropertyChanged(nameof(Watched)); }
		}

		public int Mark
		{
			get { return _mark.RawMark; }
			set { _mark.RawMark = value; }
		}

		public Mark FormatedMark
        {
			get => _mark;
        }

		public DateTime DateOfWatch
		{
			get { return _dateOfWatch; }
			set { _dateOfWatch = value; OnPropertyChanged(nameof(DateOfWatch)); }
		}

		public string Comment
		{
			get { return _comment; }
			set { _comment = value; OnPropertyChanged(nameof(Comment)); }
		}

		public ObservableCollection<Source> Sources
		{
			get { return _sources; }
			set { _sources = value; OnPropertyChanged(nameof(Sources)); }
		}

		public int CountOfViews
		{
			get { return _countOfviews; }
			set { _countOfviews = value; OnPropertyChanged(nameof(CountOfViews)); }
		}

		public int FranshiseId
		{
			get { return _franshiseId; }
			set
			{
				_franshiseId = value;
				OnPropertyChanged(nameof(FranshiseId));
			}
		}

		public sbyte FranshiseListIndex
		{
			get { return _franshiseListIndex; }
			set { _franshiseListIndex = value; OnPropertyChanged(nameof(FranshiseListIndex)); }
		}

		public int GenreId
		{
			get { return _genreId; }
		}
	}
}
