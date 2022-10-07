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

		public bool SetFinded(string search)
		{
			return IsFinded = Model.Name.ToLowerInvariant().Contains(search);
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
					menu.SelectedElement = this;
				}));
			}
		}

		private RelayCommand copyUrlCommand;
		public RelayCommand CopyUrlCommand
		{
			get
			{
				return copyUrlCommand ??
				(copyUrlCommand = new RelayCommand(obj =>
				{
					Helper.CopyFirstSource(Model.Sources);
				}));
			}
		}

		private RelayCommand openUpdateCommand;
		public RelayCommand OpenUpdateCommand
		{
			get
			{
				return openUpdateCommand ??
				(openUpdateCommand = new RelayCommand(obj =>
				{
					menu.OpenUpdateMenu(Model);
				}));
			}
		}

		private RelayCommand openInfoCommand;
		public RelayCommand OpenInfoCommand
		{
			get
			{
				return openInfoCommand ??
				(openInfoCommand = new RelayCommand(obj =>
				{
					menu.OpenInfoMenu(Model);
				}));
			}
		}

		private RelayCommand openSourceCommand;
		public RelayCommand OpenSourceCommand
        {
            get
            {
                return openSourceCommand ??
                (openSourceCommand = new RelayCommand(obj =>
                {
					menu.OpenSourcesMenu(Model.Sources);
                }));
            }
        }

		public RelayCommand addToPriorityCommand;
		public RelayCommand AddToPriorityCommand
		{
            get
            {
                return addToPriorityCommand ??
                (addToPriorityCommand = new RelayCommand(obj =>
                {
                    PriorityBooksTable priorityBooks = (PriorityBooksTable)TableCollection.GetTable<PriorityBook>();
                    if (!priorityBooks.ContainBook(Model))
                    {
                        PriorityBook priority = new PriorityBook();
                        priority.Book = Model;
                        priorityBooks.AddElement(priority);
                    }
                }));
            }
        }

		private RelayCommand upInCategoryIDCommand;
		public RelayCommand UpInCategoryIDCommand
		{
			get
			{
				return upInCategoryIDCommand ??
				(upInCategoryIDCommand = new RelayCommand(obj =>
				{
					BookCategoriesTable categories = (BookCategoriesTable)TableCollection.GetTable<BookCategory>();
					BookCategory category = categories.GetCategoryByBook(Model);
					category.ChangeBookPositionBy(Model, -1);
				}));
			}
		}

		private RelayCommand downInCategoryIDCommand;
		public RelayCommand DownInCategoryIDCommand
		{
			get
			{
				return downInCategoryIDCommand ??
				(downInCategoryIDCommand = new RelayCommand(obj =>
				{
					BookCategoriesTable categories = (BookCategoriesTable)TableCollection.GetTable<BookCategory>();
					BookCategory category = categories.GetCategoryByBook(Model);
					category.ChangeBookPositionBy(Model, 1);
				}));
			}
		}

		private RelayCommand removeFromCategoryCommand;
		public RelayCommand RemoveFromCategoryCommand
		{
			get
			{
				return removeFromCategoryCommand ??
				(removeFromCategoryCommand = new RelayCommand(obj =>
				{
					BookCategoriesTable categories = (BookCategoriesTable)TableCollection.GetTable<BookCategory>();
					BookCategory category = categories.GetCategoryByBook(Model);
					if (category != null)
					{
						category.Books.Remove(Model);
					}
				}));
			}
		}

        private RelayCommand deleteBookCommand;
        public RelayCommand DeleteBookCommand
        {
            get
            {
                return deleteBookCommand ??
                (deleteBookCommand = new RelayCommand(obj =>
                {
					BooksTable booksTable = (BooksTable)TableCollection.GetTable<Book>();
                    booksTable.Remove(Model);
                }));
            }
        }

        private RelayCommand openCMCommand;
        public RelayCommand OpenCMCommand
        {
            get
            {
                return openCMCommand ??
                (openCMCommand = new RelayCommand(obj =>
                {
					IsCMOpen = true;
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
