using NewTablesLibrary;
using System.Collections.ObjectModel;
using TL_Objects.CellDataClasses;

namespace TL_Objects
{
	[Cell("Book")]
	public class Book : Cell
	{
		[SaveField("name")]
		private String _name;

        [SaveField("author")]
        private String _author;

        [SaveField("publicationYear")]
        private int _publicationYear;

        [SaveField("readed")]
        private bool _readed;

        [SaveField("fullReadDate")]
        private DateTime _fullReadDate;

        [SaveField("mark")]
        private Mark _mark;

        [SaveField("sourceUrl")]
        private ObservableCollection<Source> _sources;

        [SaveField("countOfReadings")]
        private int _countOfReadings;

        [SaveField("bookmark")]
        private String _bookmark;

        [SaveField("franshiseListIndex")]
        private sbyte _franshiseListIndex;

        [SaveField("bookGenreId")]
        private readonly ManyToOne<Book, BookGenre> _genre;

        [SaveField("franshiseId")]
		private readonly ManyToOne<Book, BookCategory> _category;

		private readonly OneToOne<Book, PriorityBook> _prioryty;

        

		public Book()
		{
			_name = String.Empty;
			_author = String.Empty;
			_publicationYear = 0;
			_readed = false;
			_fullReadDate = new DateTime();
			_mark = new Mark();
			_sources = new ObservableCollection<Source>();
			_countOfReadings = 0;
			_bookmark = String.Empty;
			_franshiseListIndex = -1;

			_genre = new ManyToOne<Book, BookGenre>(this);
			_category = new ManyToOne<Book, BookCategory>(this);
			_prioryty = new OneToOne<Book, PriorityBook>(this);

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

		public String Name
		{
			get { return _name; }
			set { _name = value; OnPropertyChanged(); }
		}
		public String Author
		{
			get { return _author; }
			set { _author = value; OnPropertyChanged(); }
		}
		public BookGenre BookGenre
		{
			get => _genre.Value;
			set
			{
				_genre.SetValue(value);
				OnPropertyChanged();
			}
		}
		public int PublicationYear
		{
			get { return _publicationYear; }
			set { _publicationYear = value; OnPropertyChanged(); }
		}
		public bool Readed
		{
			get { return _readed; }
			set { _readed = value; OnPropertyChanged(); }
		}
		public DateTime FullReadDate
		{
			get { return _fullReadDate; }
			set { _fullReadDate = value; OnPropertyChanged(); }
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
			set { _sources = value; OnPropertyChanged(); }
		}

		public int CountOfReadings
		{
			get { return _countOfReadings; }
			set { _countOfReadings = value; OnPropertyChanged(); }
		}

		public sbyte FranshiseListIndex
		{
			get { return _franshiseListIndex; }
			set { _franshiseListIndex = value; OnPropertyChanged(); }
		}

		public String Bookmark
		{
			get { return _bookmark; }
			set { _bookmark = value; OnPropertyChanged(); }
		}
	}
}
