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
	[TableCell("Book")]
	public class Book : Cell
	{
		private String _name;
		private String _author;
		private BookGenre _bookGenre;
		private int _bookGenreId;
		private int _publicationYear;
		private bool _readed;
		private DateTime _fullReadDate;
		private Mark _mark;
		private ObservableCollection<Source> _sources;

		private int _countOfReadings;
		private String _bookmark;

		private int _franshiseId;
		private sbyte _franshiseListIndex;

		public Book()
		{
			_name = String.Empty;
			_author = String.Empty;
			_bookGenre = null;
			_bookGenreId = 0;
			_publicationYear = 0;
			_readed = false;
			_fullReadDate = new DateTime();
			_mark = new Mark();
			_sources = new ObservableCollection<Source>();
			_countOfReadings = 0;
			_bookmark = String.Empty;
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

		protected override void loadBody(Comand comand)
		{
			switch (comand.Paramert)
			{
				case "name":
					this._name = comand.Value;
					break;
				case "author":
					this._author = comand.Value;
					break;
				case "bookGenreId":
					this._bookGenreId = Convert.ToInt32(comand.Value);
					break;
				case "publicationYear":
					this._publicationYear = Convert.ToInt32(comand.Value);
					break;
				case "readed":
					this._readed = Convert.ToBoolean(comand.Value);
					break;
				case "fullReadDate":
					this._fullReadDate = Convert.ToDateTime(comand.Value);
					break;
				case "mark":
					this._mark.RawMark = Convert.ToInt32(comand.Value);
					break;
				case "sourceUrl":
					this._sources.Add(Source.ToSource(comand.Value));
					break;
				case "bookmark":
					this._bookmark = comand.Value;
					break;
				case "franshiseId":
					this._franshiseId = Convert.ToInt32(comand.Value);
					break;
				case "franshiseListIndex":
					this._franshiseListIndex = Convert.ToSByte(comand.Value);
					break;
			}
		}

		private static Source defSource = new Source();
		protected override void saveBody(StreamWriter streamWriter, Cell defaultCell)
		{
			Book dbook = (Book)defaultCell;

			streamWriter.Write(FormatParam("name", _name, String.Empty, 2));
			streamWriter.Write(FormatParam("author", _author, String.Empty, 2));
			streamWriter.Write(FormatParam("bookGenreId", _bookGenreId, 0, 2));
			streamWriter.Write(FormatParam("publicationYear", _publicationYear, 0, 2));
			streamWriter.Write(FormatParam("readed", _readed, false, 2));
			streamWriter.Write(FormatParam("fullReadDate", _fullReadDate, new DateTime(), 2));
			streamWriter.Write(FormatParam("mark", _mark.RawMark, 0, 2));
			streamWriter.Write(FormatParam("bookmark", _bookmark, String.Empty, 2));
			streamWriter.Write(FormatParam("franshiseId", _franshiseId, 0, 2));
			streamWriter.Write(FormatParam("franshiseListIndex", _franshiseListIndex, -1, 2));

			foreach (Source source in _sources)
			{
				streamWriter.Write(FormatParam("sourceUrl", source, defSource, 2));
			}
		}

		protected override void updateThisBody(Cell cell)
		{
			Book book = (Book)cell;

			this._name = book._name;
			this._author = book._author;
			this.BookGenre = book.BookGenre;
			this._publicationYear = book._publicationYear;
			this._readed = book._readed;
			this._fullReadDate = book._fullReadDate;
			this._mark = book._mark;
			this._sources = book._sources;
			this._bookmark = book._bookmark;
		}


		public String Name
		{
			get { return _name; }
			set { _name = value; OnPropertyChanged(nameof(Name)); }
		}
		public String Author
		{
			get { return _author; }
			set { _author = value; OnPropertyChanged(nameof(Author)); }
		}
		public BookGenre BookGenre
		{
			get
			{
				if (_bookGenre != null)
				{
					return _bookGenre;
				}
				else
				{
					return new BookGenre();
				}
			}
			set
			{
				_bookGenre = value;
				_bookGenreId = _bookGenre.ID;
				OnPropertyChanged(nameof(BookGenre));
			}
		}
		public int BookGenreId
		{
			get { return _bookGenreId; }
		}
		public int PublicationYear
		{
			get { return _publicationYear; }
			set { _publicationYear = value; OnPropertyChanged(nameof(PublicationYear)); }
		}
		public bool Readed
		{
			get { return _readed; }
			set { _readed = value; OnPropertyChanged(nameof(Readed)); }
		}
		public DateTime FullReadDate
		{
			get { return _fullReadDate; }
			set { _fullReadDate = value; OnPropertyChanged(nameof(FullReadDate)); }
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
		public ObservableCollection<Source> Sources
		{
			get { return _sources; }
			set { _sources = value; OnPropertyChanged(nameof(Sources)); }
		}

		public int CountOfReadings
		{
			get { return _countOfReadings; }
			set { _countOfReadings = value; OnPropertyChanged(nameof(CountOfReadings)); }
		}

		public int FranshiseId
		{
			get { return _franshiseId; }
			set { _franshiseId = value; OnPropertyChanged(nameof(FranshiseId)); }
		}

		public sbyte FranshiseListIndex
		{
			get { return _franshiseListIndex; }
			set { _franshiseListIndex = value; OnPropertyChanged(nameof(FranshiseListIndex)); }
		}

		public String Bookmark
		{
			get { return _bookmark; }
			set { _bookmark = value; OnPropertyChanged(nameof(Bookmark)); }
		}
	}
}
