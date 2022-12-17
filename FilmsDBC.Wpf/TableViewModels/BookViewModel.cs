﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
using TL_Objects;
using TL_Objects.Interfaces;
using TL_Tables;
using WpfApp.Commands;
using WpfApp.TableViewModels.Interfaces;

namespace WpfApp.TableViewModels
{
	public class BookViewModel : BaseViewModel<Book>, IHasGenre, IFilter, IFinded
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
			IsFinded = Model.Name.ToLowerInvariant().Contains(search);
            return IsFinded ;
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

		public bool Filter(IGenre[] selectedGenres, bool isReadedChecked, bool isUnReadedChecked)
		{
			bool passedFilter = false;

			if (HasSelectedGenre(selectedGenres))
			{
				passedFilter = Model.Readed == isReadedChecked || Model.Readed != isUnReadedChecked;
			}

			IsFiltered = passedFilter;
			return passedFilter;
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

		public RelayCommand removeFromPriorityCommand;
		public RelayCommand RemoveFromPriorityCommand
		{
			get
			{
				return removeFromPriorityCommand ??
				(removeFromPriorityCommand = new RelayCommand(obj =>
				{
					PriorityBooksTable priorityBooks = (PriorityBooksTable)TableCollection.GetTable<PriorityBook>();
					IEnumerable<PriorityBook> enumerable = priorityBooks as IEnumerable<PriorityBook>;
					priorityBooks?.Remove(enumerable.Where(x => x.Book == Model).FirstOrDefault());
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

		private static readonly Regex regex = new Regex(@"\D");
		private RelayCommand textIsNumberCommand;
		public RelayCommand TextIsNumberCommand
		{
			get
			{
				return textIsNumberCommand ??
				(textIsNumberCommand = new RelayCommand(obj =>
				{
					TextCompositionEventArgs e = obj as TextCompositionEventArgs;
					e.Handled = regex.IsMatch(e.Text);
				}));
			}
		}

		private RelayCommand baseAutoFill;
		public RelayCommand BaseAutoFill
		{
			get
			{
				return baseAutoFill ??
				(baseAutoFill = new RelayCommand(obj =>
				{
					if (Model.Readed == false)
					{
						if (Model.CountOfReadings == 0)
						{
							Model.CountOfReadings = 1;
						}
					}
				}));
			}
		}

		private RelayCommand fullAutoFill;
		public RelayCommand FullAutoFill
		{
			get
			{
				return fullAutoFill ??
				(fullAutoFill = new RelayCommand(obj =>
				{
					BaseAutoFill.Execute(obj);
					if (Model.Readed == false)
					{
						if (Model.FullReadDate == defaultDate)
						{
							Model.FullReadDate = DateTime.Today;
						}

						Model.Readed = true;
					}
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

		private bool _isFiltered = true;
		public bool IsFiltered
		{
			get => _isFiltered;
			set
			{
				_isFiltered = value;
				OnPropertyChanged();
			}
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
