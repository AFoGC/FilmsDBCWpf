using CustomButtons;
using FilmsUCWpf.Presenter;
using FilmsUCWpf.PresenterInterfaces;
using FilmsUCWpf.View;
using InfoMenusWpf.MoreInfo;
using InfoMenusWpf.UpdateInfo;
using ProfilesConfig;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter;
using TL_Objects;

namespace WpfApp.MVP.Models
{
	public class BooksMenuModel : IMenu<Book>
	{
		public enum MenuCondition
		{
			Category = 1,
			Book = 2,
			PriorityBook = 3
		}

		public MenuCondition ControlsCondition { get; set; }
		public MoreInfoFormVisualizer MoreInfoFormVisualizer { get; set; }
		public UpdateFormVisualizer UpdateFormVisualizer { get; set; }
		public TLTables Tables => mainModel.Tables;
		public TableCollection TableCollection => mainModel.TableCollection;
		BasePresenter<Book> IMenu<Book>.SelectedElement { get => SelectedElement; set => SelectedElement = (BookPresenter)value; }

		private BookPresenter selectedElement = null;
		public BookPresenter SelectedElement
		{
			get { return selectedElement; }
			set
			{
				if (selectedElement != null) selectedElement.SetVisualDefault();
				selectedElement = value;
			}
		}

		public ObservableCollection<IBasePresenter> CategoryPresenters { get; private set; }
		public ObservableCollection<BookPresenter> BookPresenters { get; private set; }
		public ObservableCollection<BookPriorityPresenter> PriorityPresenters { get; private set; }
		public ObservableCollection<GenrePressButtonControl> GenreButtons { get; private set; }

		public IEnumerable GetCurrentPresenters()
		{
			switch (ControlsCondition)
			{
				case MenuCondition.Category:
					return CategoryPresenters;
				case MenuCondition.Book:
					return BookPresenters;
				case MenuCondition.PriorityBook:
					return PriorityPresenters;
				default:
					return null;
			}
		}

		private readonly MainWindowModel mainModel;
		public BooksMenuModel(MainWindowModel mainWindowModel)
		{
			mainModel = mainWindowModel;

			CategoryPresenters = new ObservableCollection<IBasePresenter>();
			BookPresenters = new ObservableCollection<BookPresenter>();
			PriorityPresenters = new ObservableCollection<BookPriorityPresenter>();
			GenreButtons = new ObservableCollection<GenrePressButtonControl>();

			mainModel.TableCollection.TableLoad += TableCollection_TableLoad;

			mainModel.Tables.BooksTable.CollectionChanged += BooksTable_CollectionChanged;
			mainModel.Tables.BookCategoriesTable.CollectionChanged += BookCategoriesTable_CollectionChanged;
			mainModel.Tables.PriorityBooksTable.CollectionChanged += PriorityBooksTable_CollectionChanged;
			mainModel.Tables.BookGenresTable.CollectionChanged += GenresTable_CollectionChanged;

			ControlsCondition = MenuCondition.Category;
		}

		private void GenresTable_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			BookGenre genre;
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					genre = (BookGenre)e.NewItems[0];
					GenreButtons.Add(new GenrePressButtonControl(genre));
					break;
				case NotifyCollectionChangedAction.Remove:
					genre = (BookGenre)e.OldItems[0];
					GenreButtons.Remove(GenreButtons.Where(x => x.Genre == genre).FirstOrDefault());
					break;
				case NotifyCollectionChangedAction.Reset:
					GenreButtons.Clear();
					break;
				default:
					break;
			}
		}

		public void TableLoad()
		{
			PriorityPresenters.Clear();
			BookPresenters.Clear();
			CategoryPresenters.Clear();
			GenreButtons.Clear();

			foreach (BookGenre genre in Tables.BookGenresTable)
			{
				GenreButtons.Add(new GenrePressButtonControl(genre));
			}

			foreach (BookCategory category in Tables.BookCategoriesTable)
			{
				CategoryPresenters.Add(new BookCategoryPresenter(category, new BookCategoryControl(), this, TableCollection));
			}

			foreach (Book book in Tables.BooksTable)
			{
				BookPresenters.Add(new BookPresenter(book, new BookControl(), this, TableCollection));
				if (book.FranshiseId == 0)
				{
					CategoryPresenters.Add(new BookPresenter(book, new BookSimpleControl(), this, TableCollection));
				}
			}

			foreach (PriorityBook priorityBook in Tables.PriorityBooksTable)
			{
				PriorityPresenters.Add(new BookPriorityPresenter(priorityBook, new BookPriorityControl(), this, TableCollection));
			}
		}

		private void TableCollection_TableLoad(object sender, EventArgs e) => TableLoad();

		private void PriorityBooksTable_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			PriorityBook priorityBook;
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					priorityBook = (PriorityBook)e.NewItems[0];
					PriorityPresenters.Add(new BookPriorityPresenter(priorityBook, new BookPriorityControl(), this, TableCollection));
					break;
				case NotifyCollectionChangedAction.Remove:
					priorityBook = (PriorityBook)e.OldItems[0];
					PriorityPresenters.Remove(PriorityPresenters.Where(x => x.PriorityModel == priorityBook).FirstOrDefault());
					break;
				case NotifyCollectionChangedAction.Reset:
					PriorityPresenters.Clear();
					break;
				default:
					break;
			}
		}

		private void BookCategoriesTable_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			BookCategory category;
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					category = (BookCategory)e.NewItems[0];
					CategoryPresenters.Insert(Tables.BookCategoriesTable.Count - 1, new BookCategoryPresenter(category, new BookCategoryControl(), this, TableCollection));
					break;
				case NotifyCollectionChangedAction.Remove:
					CategoryPresenters.Remove(CategoryPresenters.Where(x =>
					{
						category = (BookCategory)e.OldItems[0];
						if (x.GetType() == typeof(BookCategoryPresenter))
							return ((BookCategoryPresenter)x).Model == category;
						else
							return false;

					}).FirstOrDefault());
					break;
				case NotifyCollectionChangedAction.Reset:
					CategoryPresenters.Clear();
					break;
				default:
					break;
			}
		}

		private void BooksTable_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			Book book;
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					book = (Book)e.NewItems[0];
					BookPresenters.Add(new BookPresenter(book, new BookControl(), this, TableCollection));
					CategoryPresenters.Add(new BookPresenter(book, new BookSimpleControl(), this, TableCollection));
					break;
				case NotifyCollectionChangedAction.Remove:
					book = (Book)e.OldItems[0];
					BookPresenters.Remove(BookPresenters.Where(x => x.Model == book).FirstOrDefault());
					PriorityPresenters.Remove(PriorityPresenters.Where(x => x.Model == book).FirstOrDefault());
					CategoryPresenters.Remove(CategoryPresenters.Where(x =>
						{
							if (x.GetType() == typeof(BookPresenter))
								return ((BookPresenter)x).Model == book;
							else 
								return false;

						}).FirstOrDefault());
					break;
				case NotifyCollectionChangedAction.Reset:
					BookPresenters.Clear();
					CategoryPresenters.Clear();
					break;
				default:
					break;
			}
		}

		public bool AddPresenter(BasePresenter<Book> presenter)
		{
			int i = 0;
			Type type = presenter.GetType();
			foreach (IBasePresenter item in CategoryPresenters)
			{
				if (item.GetType().IsSubclassOf(type))
				{
					BasePresenter<Book> basePresenter = (BasePresenter<Book>)item;
					Book book = basePresenter.Model;
					if (book.ID > SelectedElement.Model.ID) break;
				}
				++i;
			}
			BookPresenter bookPresenter = new BookPresenter(SelectedElement.Model, new BookSimpleControl(), this, TableCollection);
			CategoryPresenters.Insert(i, bookPresenter);
			return true;
		}

		public bool RemovePresenter(BasePresenter<Book> presenter)
		{
			return CategoryPresenters.Remove(presenter);
		}
	}
}
