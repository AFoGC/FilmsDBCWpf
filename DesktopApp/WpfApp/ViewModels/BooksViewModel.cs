using FilmsUCWpf.View;
using FilmsUCWpf.ViewModel;
using FilmsUCWpf.ViewModel.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.TableCell;
using TL_Objects;
using TL_Objects.CellDataClasses;
using TL_Objects.Interfaces;
using TL_Tables;
using WpfApp.Commands;
using WpfApp.Models;

namespace WpfApp.ViewModels
{
    public class BooksViewModel : BaseViewModel, IMenuViewModel<Book>
    {
        public BooksModel Model { get; private set; }

        public ObservableCollection<GenreButtonViewModel> GenresTable { get; private set; }
        public ObservableCollection<BookCategoryViewModel> CategoriesMenu { get; private set; }
        public ObservableCollection<BookViewModel> SimpleBooksMenu { get; private set; }
        public ObservableCollection<BookViewModel> BooksMenu { get; private set; }
        public ObservableCollection<BookViewModel> PriorityBooksMenu { get; private set; }

        private Visibility _categoryVisibility = Visibility.Collapsed;
        public Visibility CategoryVisibility
        {
            get => _categoryVisibility;
            set
            {
                _categoryVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _booksVisibility = Visibility.Collapsed;
        public Visibility BooksVisibility
        {
            get => _booksVisibility;
            set
            {
                _booksVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _priorityVisibility = Visibility.Collapsed;
        public Visibility PriorityVisibility
        {
            get => _priorityVisibility;
            set
            {
                _priorityVisibility = value;
                OnPropertyChanged();
            }
        }

        private bool _isReadedChecked = true;
        public bool IsReadedChecked
        {
            get => _isReadedChecked;
            set
            {
                if (IsUnReadedChecked != false || value != false)
                {
                    _isReadedChecked = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isUnReadedChecked = true;
        public bool IsUnReadedChecked
        {
            get => _isUnReadedChecked;
            set
            {
                if (IsReadedChecked != false || value != false)
                {
                    _isUnReadedChecked = value;
                    OnPropertyChanged();
                }
            }
        }

        private BaseViewModel<Book> _selectedElement;
        public BaseViewModel<Book> SelectedElement 
        { 
            get => _selectedElement;
            set
            {
                if (_selectedElement != null) _selectedElement.IsSelected = false;
                _selectedElement = value;
                _selectedElement.IsSelected = true;
            } 
        }

        private String _searchText = String.Empty;
        public String SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
            }
        }

        private Command showCategoriesCommand;
        public Command ShowCategoriesCommand
        {
            get
            {
                return showCategoriesCommand ??
                (showCategoriesCommand = new Command(obj =>
                {
                    CategoryVisibility = Visibility.Visible;
                    BooksVisibility = Visibility.Collapsed;
                    PriorityVisibility = Visibility.Collapsed;
                }));
            }
        }

        private Command showBooksCommand;
        public Command ShowBooksCommand
        {
            get
            {
                return showBooksCommand ??
                (showBooksCommand = new Command(obj =>
                {
                    CategoryVisibility = Visibility.Collapsed;
                    BooksVisibility = Visibility.Visible;
                    PriorityVisibility = Visibility.Collapsed;
                }));
            }
        }

        private Command showPriorityCommand;
        public Command ShowPriorityCommand
        {
            get
            {
                return showPriorityCommand ??
                (showPriorityCommand = new Command(obj =>
                {
                    CategoryVisibility = Visibility.Collapsed;
                    BooksVisibility = Visibility.Collapsed;
                    PriorityVisibility = Visibility.Visible;
                }));
            }
        }

        private Command addCategoryCommand;
        public Command AddCategoryCommand
        {
            get
            {
                return addCategoryCommand ??
                (addCategoryCommand = new Command(obj => Model.AddCategory()));
            }
        }

        private Command addBookCommand;
        public Command AddBookCommand
        {
            get
            {
                return addBookCommand ??
                (addBookCommand = new Command(obj => Model.AddBook()));
            }
        }

        private Command saveTablesCommand;
        public Command SaveTablesCommand
        {
            get
            {
                return saveTablesCommand ??
                (saveTablesCommand = new Command(obj => Model.SaveTables()));
            }
        }

        private Command filterCommand;
        public Command FilterCommand
        {
            get
            {
                return filterCommand ??
                (filterCommand = new Command(obj =>
                {
                    IGenre[] genres = getSelectedGenres();
                    FilterTable(CategoriesMenu, genres);
                    FilterTable(SimpleBooksMenu, genres);
                    FilterTable(BooksMenu, genres);
                    FilterTable(PriorityBooksMenu, genres);

                }));
            }
        }

        private Command searchCommand;
        public Command SearchCommand
        {
            get
            {
                return searchCommand ??
                (searchCommand = new Command(obj =>
                {
                    SearcherTable(CategoriesMenu);
                    SearcherTable(SimpleBooksMenu);
                    SearcherTable(BooksMenu);
                    SearcherTable(PriorityBooksMenu);
                }));
            }
        }

        public CollectionViewSource CategoryCVS { get; private set; }
        public CollectionViewSource SimpleBooksCVS { get; private set; }
        public CollectionViewSource BooksCVS { get; private set; }
        public CollectionViewSource PriorityBooksCVS { get; private set; }

        private Command sortTable;
        public Command SortTable =>
        sortTable ?? (sortTable = new Command(obj =>
        {
            string str = obj as string;
            ListSortDirection direction = getSortDirection(str);
            CollectionViewSource[] sources = getVisibleCVS();

            foreach (CollectionViewSource item in sources)
            {
                CVSChangeSort(item, str, direction);
            }
        }));

        private ListSortDirection getSortDirection(string paramenter)
        {
            switch (paramenter)
            {
                case "Model.Mark":
                    return ListSortDirection.Descending;
                case "Model.PublicationYear":
                    return ListSortDirection.Descending;
                case "Model.Author":
                    return ListSortDirection.Descending;
                case "Model.Bookmark":
                    return ListSortDirection.Descending;
                case "Model.FullReadDate":
                    return ListSortDirection.Descending;
                case "Model.CountOfReadings":
                    return ListSortDirection.Descending;

                default:
                    return ListSortDirection.Ascending;
            }
        }

        private CollectionViewSource[] getVisibleCVS()
        {
            List<CollectionViewSource> sources = new List<CollectionViewSource>();

            if (CategoryVisibility == Visibility.Visible)
            {
                sources.Add(CategoryCVS);
                sources.Add(SimpleBooksCVS);
            }
            if (BooksVisibility == Visibility.Visible)
            {
                sources.Add(BooksCVS);
            }
            if (PriorityVisibility == Visibility.Visible)
            {
                sources.Add(PriorityBooksCVS);
            }

            return sources.ToArray();
        }

        private void CVSChangeSort(CollectionViewSource cvs, string property, ListSortDirection direction)
        {
            cvs.SortDescriptions.Clear();
            cvs.SortDescriptions.Add(new SortDescription(property, direction));
        }

        private void FilterTable(IEnumerable table, IGenre[] genres)
        {
            foreach (IFilter vm in table)
            {
                vm.Filter(genres, IsReadedChecked, IsUnReadedChecked);
            }
        }

        private void SearcherTable(IEnumerable table)
        {
            string search = SearchText.ToLower();
            foreach (IFinded vm in table)
            {
                vm.SetFinded(search);
            }
        }

        private IGenre[] getSelectedGenres()
        {
            List<IGenre> genres = new List<IGenre>();
            foreach (var genre in GenresTable)
            {
                if (genre.IsChecked)
                {
                    genres.Add(genre.Model);
                }
            }
            return genres.ToArray();
        }

        public BooksViewModel()
        {
            Model = new BooksModel();

            GenresTable = new ObservableCollection<GenreButtonViewModel>();
            CategoriesMenu = new ObservableCollection<BookCategoryViewModel>();
            SimpleBooksMenu = new ObservableCollection<BookViewModel>();
            BooksMenu = new ObservableCollection<BookViewModel>();
            PriorityBooksMenu = new ObservableCollection<BookViewModel>();

            Model.TableCollection.TableLoad += TableLoad;

            Model.BookGenresTable.CollectionChanged += GenresChanged;
            Model.BooksTable.CollectionChanged += BooksChanged;
            Model.BookCategoriesTable.CollectionChanged += CategoriesChanged;
            Model.PriorityBooksTable.CollectionChanged += PriorityChanged;

            CategoryVisibility = Visibility.Visible;
            TableLoad(this, null);

            SimpleBooksCVS = new CollectionViewSource();
            CategoryCVS = new CollectionViewSource();
            BooksCVS = new CollectionViewSource();
            PriorityBooksCVS = new CollectionViewSource();

            SimpleBooksCVS.Source = SimpleBooksMenu;
            CategoryCVS.Source = CategoriesMenu;
            BooksCVS.Source = BooksMenu;
            PriorityBooksCVS.Source = PriorityBooksMenu;

            SourcesCVS = new CollectionViewSource();
        }

        private void GenresChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            BookGenre genre;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    genre = (BookGenre)e.NewItems[0];
                    GenresTable.Add(new GenreButtonViewModel(genre));
                    break;
                case NotifyCollectionChangedAction.Remove:
                    genre = (BookGenre)e.OldItems[0];
                    GenresTable.Remove(GenresTable.Where(x => x.Model == genre).FirstOrDefault());
                    break;
                case NotifyCollectionChangedAction.Reset:
                    GenresTable.Clear();
                    break;
                default:
                    break;
            }
        }

        private void PriorityChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            PriorityBook priorityBook;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    priorityBook = (PriorityBook)e.NewItems[0];
                    PriorityBooksMenu.Add(new BookViewModel(priorityBook.Book, this));
                    break;
                case NotifyCollectionChangedAction.Remove:
                    priorityBook = (PriorityBook)e.OldItems[0];
                    PriorityBooksMenu.Remove(PriorityBooksMenu.Where(x => x.Model == priorityBook.Book).FirstOrDefault());
                    break;
                case NotifyCollectionChangedAction.Reset:
                    PriorityBooksMenu.Clear();
                    break;
                default:
                    break;
            }
        }

        private void CategoriesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            BookCategory category;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    category = (BookCategory)e.NewItems[0];
                    CategoriesMenu.Add(new BookCategoryViewModel(category, this));
                    category.Books.CollectionChanged += CategoryChanged;
                    break;
                case NotifyCollectionChangedAction.Remove:
                    category = (BookCategory)e.OldItems[0];
                    CategoriesMenu.Remove(CategoriesMenu.Where(x => x.Model == category).FirstOrDefault());
                    category.Books.CollectionChanged -= CategoryChanged;
                    break;
                case NotifyCollectionChangedAction.Reset:
                    CategoriesMenu.Clear();
                    break;
                default:
                    break;
            }
        }

        private void CategoryChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Book book;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    book = (Book)e.NewItems[0];
                    SimpleBooksMenu.Remove(SimpleBooksMenu.Where(x => x.Model == book).FirstOrDefault());
                    break;
                case NotifyCollectionChangedAction.Remove:
                    book = (Book)e.OldItems[0];
                    if (Model.BooksTable.Contains(book))
                    {
                        SimpleBooksMenu.Add(new BookViewModel(book, this));
                    }
                    break;
                default:
                    break;
            }
        }

        private void BooksChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Book book;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    book = (Book)e.NewItems[0];
                    BooksMenu.Add(new BookViewModel(book, this));
                    SimpleBooksMenu.Add(new BookViewModel(book, this));
                    break;
                case NotifyCollectionChangedAction.Remove:
                    book = (Book)e.OldItems[0];
                    BooksMenu.Remove(BooksMenu.Where(x => x.Model == book).FirstOrDefault());
                    SimpleBooksMenu.Remove(SimpleBooksMenu.Where(x => x.Model == book).FirstOrDefault());
                    PriorityBooksMenu.Remove(PriorityBooksMenu.Where(x => x.Model == book).FirstOrDefault());
                    break;
                case NotifyCollectionChangedAction.Reset:
                    BooksMenu.Clear();
                    SimpleBooksMenu.Clear();
                    break;
                default:
                    break;
            }
        }

        private void TableLoad(object sender, EventArgs e)
        {
            CategoriesMenu.Clear();
            SimpleBooksMenu.Clear();
            BooksMenu.Clear();
            PriorityBooksMenu.Clear();
            GenresTable.Clear();

            foreach (BookGenre genre in Model.BookGenresTable)
            {
                GenresTable.Add(new GenreButtonViewModel(genre));
            }

            foreach (BookCategory category in Model.BookCategoriesTable)
            {
                CategoriesMenu.Add(new BookCategoryViewModel(category, this));
                category.Books.CollectionChanged += CategoryChanged;
            }

            foreach (Book book in Model.BooksTable)
            {
                BooksMenu.Add(new BookViewModel(book, this));
                if (book.FranshiseId == 0)
                {
                    SimpleBooksMenu.Add(new BookViewModel(book, this));
                }
            }

            foreach (PriorityBook book in Model.PriorityBooksTable)
            {
                PriorityBooksMenu.Add(new BookViewModel(book.Book, this));
            }
        }

        private BookInfoMenuCondition infoMenuCondition;
        public BookInfoMenuCondition InfoMenuCondition
        {
            get => infoMenuCondition;
            set
            {
                infoMenuCondition = value;
                if (infoMenuCondition == BookInfoMenuCondition.Closed)
                {
                    InfoMenuDataContext = null;
                    SourcesCVS.Source = null;
                }
                    
                OnPropertyChanged();
            }
        }

        private Object _infoMenuDataContext;
        public Object InfoMenuDataContext
        {
            get => _infoMenuDataContext;
            set
            {
                _infoMenuDataContext = value;
                OnPropertyChanged();
            }
        }

        public void OpenInfoMenu(Cell model)
        {
            SourcesCVS.Source = null;
            InfoMenuCondition = BookInfoMenuCondition.Closed;
            if (model.GetType() == typeof(Book))
            {
                Book book = model as Book;
                InfoMenuDataContext = new BookViewModel(book, this);
                InfoMenuCondition = BookInfoMenuCondition.BookInfo;
            }
        }

        public void OpenUpdateMenu(Cell model)
        {
            SourcesCVS.Source = null;
            InfoMenuCondition = BookInfoMenuCondition.Closed;
            if (model.GetType() == typeof(Book))
            {
                Book book = model as Book;
                InfoMenuDataContext = new BookViewModel(book, this);
                InfoMenuCondition = BookInfoMenuCondition.BookUpdate;
            }
            if (model.GetType() == typeof(BookCategory))
            {
                BookCategory category = model as BookCategory;
                InfoMenuDataContext = new BookCategoryViewModel(category, this);
                InfoMenuCondition = BookInfoMenuCondition.CategoryUpdate;
            }
        }

        public CollectionViewSource SourcesCVS { get; private set; }
        public void OpenSourcesMenu(ObservableCollection<Source> sources)
        {
            SourcesCVS.Source = sources;
        }

        private Command removeSourceCommand;
        public Command RemoveSourceCommand =>
        removeSourceCommand ?? (removeSourceCommand = new Command(obj =>
        {
            if (SourcesCVS.Source != null)
            {
                Source source = obj as Source;
                ObservableCollection<Source> sources = SourcesCVS.Source as ObservableCollection<Source>;
                sources.Remove(source);
            }
        }));

        private Command addSourceCommand;
        public Command AddSourceCommand =>
        addSourceCommand ?? (addSourceCommand = new Command(obj =>
        {
            if (SourcesCVS.Source != null)
            {
                ObservableCollection<Source> sources = SourcesCVS.Source as ObservableCollection<Source>;
                sources.Add(new Source());
            }
        }));

        private Command moveUpSourceCommand;
        public Command MoveUpSourceCommand =>
        moveUpSourceCommand ?? (moveUpSourceCommand = new Command(obj =>
        {
            if (SourcesCVS.Source != null)
            {
                Source source = obj as Source;
                ObservableCollection<Source> sources = SourcesCVS.Source as ObservableCollection<Source>;
                sources.Move(sources.IndexOf(source), 0);
            }
        }));

        private Command closeInfoCommand;
        public Command CloseInfoCommand =>
        closeInfoCommand ?? (closeInfoCommand = new Command(obj =>
        {
            InfoMenuCondition = BookInfoMenuCondition.Closed;
        }));
    }

    public enum BookInfoMenuCondition
    {
        Closed,
        BookInfo,
        BookUpdate,
        CategoryUpdate
    }
}
