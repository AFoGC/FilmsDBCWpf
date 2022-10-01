using FilmsUCWpf.Command;
using FilmsUCWpf.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL_Objects;
using TL_Objects.Interfaces;
using TL_Tables;

namespace FilmsUCWpf.ViewModel
{
	public class BookViewModel : BaseViewModel<Book>, IHasGenre, IHasCheckedProperty
	{
		private readonly BookGenresTable genresTable;
		private readonly IMenuViewModel<Book> menu;
		public BookViewModel(Book model, IMenuViewModel<Book> menu) : base(model)
		{
			model.PropertyChanged += ModelPropertyChanged;
			this.menu = menu;
			genresTable = (BookGenresTable)model.BookGenre.ParentTable;
		}

		public void SetSelected()
		{
			SetVisualSelected();
			menu.SelectedElement = this;
		}

		public bool SetFinded(string search)
		{
			if (Model.Name.ToLowerInvariant().Contains(search))
			{
				SetVisualFinded();
				return true;
			}
			return false;
		}

		public bool HasSelectedGenre(IGenre[] selectedGenres)
		{
			foreach (IGenre genre in selectedGenres)
			{
				if (genre == Model.BookGenre)
				{
					return true;
				}
			}
			return false;
		}

		public bool HasCheckedProperty(bool isReaded)
		{
			return isReaded == Model.Readed;
		}

		private RelayCommand selectCommand;
		public RelayCommand SelectCommand
		{
			get
			{
				return selectCommand ??
				(selectCommand = new RelayCommand(obj =>
				{
					SetVisualSelected();
					menu.SelectedElement = this;
				}));
			}
		}

		private void ModelPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(Model.BookGenre))
			{
				OnPropertyChanged(nameof(BookGenreText));
				OnPropertyChanged(nameof(SelectedGenre));
				return;
			}
			if (e.PropertyName == nameof(Model.FullReadDate))
			{
				OnPropertyChanged(nameof(Date));
				return;
			}

			OnPropertyChanged(e);
		}

		public String ID
		{
			get => Model.ID.ToString();
			set { }
		}
		public virtual String Name
		{
			get => Model.Name;
			set => Model.Name = value;
		}

		public String BookGenreText
		{
			get => Model.BookGenre.ToString();
			set { }
		}
		public BookGenre SelectedGenre
		{
			get => Model.BookGenre;
			set => Model.BookGenre = value;
		}
		public INotifyCollectionChanged GenresCollection => genresTable;

		public String PublicationYear
		{
			get => formatZero(Model.PublicationYear);
			set => Model.PublicationYear = formatEmpty(value);
		}
		public Boolean Readed
		{
			get => Model.Readed;
			set => Model.Readed = value;
		}
		public String Author
		{
			get => Model.Author;
			set => Model.Author = value;
		}
		public String FullReadDate
		{
			get => FormateDate(Model.FullReadDate);
			set { }
		}
		public DateTime Date
		{
			get => Model.FullReadDate;
			set => Model.FullReadDate = value;
		}
		public String Mark
		{
			get => Model.FormatedMark.ToString();
			set => Model.FormatedMark.SetMarkFromString(value);
		}
		public List<String> Marks => Model.FormatedMark.GetComboItems();
		public String CountOfReadings
		{
			get => formatZero(Model.CountOfReadings);
			set => Model.CountOfReadings = formatEmpty(value);
		}
		public String Bookmark
		{
			get => Model.Bookmark;
			set => Model.Bookmark = value;
		}
		public String Sources
		{
			get => Helper.SourcesStateString(Model.Sources);
			set { }
		}
	}
}
