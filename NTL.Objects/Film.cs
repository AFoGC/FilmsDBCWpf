using NewTablesLibrary;
using System.Collections.ObjectModel;
using TL_Objects.CellDataClasses;

namespace TL_Objects
{
	[Cell("Film")]
	public class Film : Cell
	{
		[SaveField("name")]
		private string _name;

        [SaveField("realiseYear")]
        private int _realiseYear;

        [SaveField("watched")]
        private bool _watched;

        [SaveField("dateOfWatch")]
        private DateTime _dateOfWatch;

        [SaveField("comment")]
        private string _comment;

        [SaveField("countOfviews")]
        private int _countOfviews;

        [SaveField("franshiseListIndex")]
        private sbyte _franshiseListIndex;

        [SaveField("mark")]
        private readonly Mark _mark;

        [SaveField("sourceUrl")]
        private ObservableCollection<Source> _sources;

        [SaveField("franshiseId")]
        private readonly ManyToOne<Film, Category> _category;

        [SaveField("genre")]
        private readonly ManyToOne<Film, Genre> _genre;

		public readonly OneToOne<Film, Serie> _serie;

		public Film()
		{
			_name = String.Empty;
			_realiseYear = 0;
			_watched = false;
			_dateOfWatch = new DateTime();
			_comment = String.Empty;
			_countOfviews = 0;
			_franshiseListIndex = -1;

            _mark = new Mark();
            _sources = new ObservableCollection<Source>();

            _category = new ManyToOne<Film, Category>(this);
			_genre = new ManyToOne<Film, Genre>(this);
			_serie = new OneToOne<Film, Serie>(this);

			_sources.CollectionChanged += Sources_CollectionChanged;
            _mark.PropertyChanged += Mark_PropertyChanged;
		}

        protected override void OnRemoving()
        {
			_category.SetValue(null);
			_genre.SetValue(null);
			_serie.SetValue(null);

            base.OnRemoving();
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

		public string Name
		{
			get { return _name; }
			set { _name = value; OnPropertyChanged(nameof(Name)); }
		}

		public Genre Genre
		{
			get => _genre.Value;
			set
			{
				_genre.SetValue(value);
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

		public sbyte FranshiseListIndex
		{
			get { return _franshiseListIndex; }
			set { _franshiseListIndex = value; OnPropertyChanged(nameof(FranshiseListIndex)); }
		}
	}
}
