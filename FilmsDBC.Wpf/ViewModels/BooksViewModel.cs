using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using TablesLibrary.Interpreter.TableCell;
using TL_Objects;
using TL_Objects.CellDataClasses;
using TL_Objects.Interfaces;
using WpfApp.Commands;
using WpfApp.Factories;
using WpfApp.Models;
using WpfApp.TableViewModels;
using WpfApp.TableViewModels.Interfaces;

namespace WpfApp.ViewModels
{
    public class BooksViewModel : BaseViewModel, IMenuViewModel<Book>
    {
        private readonly BooksModel _model;

        private BaseViewModel<Book> _selectedElement;

        private BooksMenuMode _menuMode = BooksMenuMode.Categories;

        private bool _isReadedChecked = true;
        private bool _isUnReadedChecked = true;
        private string _searchText = string.Empty;

        private RelayCommand changeMenuModeCommand;
        private RelayCommand addCategoryCommand;
        private RelayCommand addBookCommand;
        private RelayCommand saveTablesCommand;
        private RelayCommand filterCommand;
        private RelayCommand searchCommand;
        private RelayCommand sortTable;
        private RelayCommand removeSourceCommand;
        private RelayCommand addSourceCommand;
        private RelayCommand moveUpSourceCommand;
        private RelayCommand closeInfoCommand;

        private BookInfoMenuCondition infoMenuCondition;
        private object _infoMenuDataContext;

        public ObservableCollection<Source> SelectedSources { get; private set; }
        public ObservableCollection<GenreButtonViewModel> GenresTable { get; }
        public ObservableCollection<BookCategoryViewModel> CategoriesMenu { get; }
        public ObservableCollection<BookViewModel> SimpleBooksMenu { get; }
        public ObservableCollection<BookViewModel> BooksMenu { get; }
        public ObservableCollection<BookViewModel> PriorityBooksMenu { get; }
        public IViewCollection CategoriesViewCollection { get; }
        public IViewCollection SimpleBooksViewCollection { get; }
        public IViewCollection BooksViewCollection { get; }
        public IViewCollection PriorityViewCollection { get; }

        public BooksViewModel(BooksModel model, IViewCollectionFactory viewCollectionFactory)
        {
            _model = model;

            GenresTable = new ObservableCollection<GenreButtonViewModel>();
            CategoriesMenu = new ObservableCollection<BookCategoryViewModel>();
            SimpleBooksMenu = new ObservableCollection<BookViewModel>();
            BooksMenu = new ObservableCollection<BookViewModel>();
            PriorityBooksMenu = new ObservableCollection<BookViewModel>();

            _model.TablesLoaded += TableLoad;

            _model.BookGenresTable.CollectionChanged += GenresChanged;
            _model.BooksTable.CollectionChanged += BooksChanged;
            _model.BookCategoriesTable.CollectionChanged += CategoriesChanged;
            _model.PriorityBooksTable.CollectionChanged += PriorityChanged;

            TableLoad();

            viewCollectionFactory.SetDescendingProperties(GetDescendingProperties());
            CategoriesViewCollection = viewCollectionFactory.CreateViewCollection(CategoriesMenu);
            SimpleBooksViewCollection = viewCollectionFactory.CreateViewCollection(SimpleBooksMenu);
            BooksViewCollection = viewCollectionFactory.CreateViewCollection(BooksMenu);
            PriorityViewCollection = viewCollectionFactory.CreateViewCollection(PriorityBooksMenu);

            CategoriesViewCollection.ChangeSortProperty("Model.ID");
            SimpleBooksViewCollection.ChangeSortProperty("Model.ID");
            BooksViewCollection.ChangeSortProperty("Model.ID");
            PriorityViewCollection.ChangeSortProperty("Model.ID");
        }

        private IEnumerable<string> GetDescendingProperties()
        {
            yield return "Model.Mark";
            yield return "Model.PublicationYear";
            yield return "Model.Author";
            yield return "Model.Bookmark";
            yield return "Model.FullReadDate";
            yield return "Model.CountOfReadings";
        }

        public BooksMenuMode MenuMode
        {
            get => _menuMode;
            set
            {
                _menuMode = value;
                OnPropertyChanged();
            }
        }
        
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
        
        public BaseViewModel<Book> SelectedElement 
        { 
            get => _selectedElement;
            set
            {
                if (_selectedElement != null) _selectedElement.IsSelected = false;
                _selectedElement = value;

                if (_selectedElement != null) _selectedElement.IsSelected = true;
            } 
        }
        
        public String SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                SearchInTable(CategoriesMenu);
                SearchInTable(SimpleBooksMenu);
                SearchInTable(BooksMenu);
                SearchInTable(PriorityBooksMenu);
                OnPropertyChanged();
            }
        }

        private void SearchInTable(IEnumerable table)
        {
            string search = SearchText.ToLower();
            foreach (IFinded vm in table)
            {
                vm.SetFinded(search);
            }
        }

        public RelayCommand ChangeMenuModeCommand
        {
            get
            {
                return changeMenuModeCommand ??
                (changeMenuModeCommand = new RelayCommand(obj =>
                {
                    MenuMode = (BooksMenuMode)obj;
                }));
            }
        }

        public RelayCommand AddCategoryCommand
        {
            get
            {
                return addCategoryCommand ??
                (addCategoryCommand = new RelayCommand(obj => _model.AddCategory()));
            }
        }
        
        public RelayCommand AddBookCommand
        {
            get
            {
                return addBookCommand ??
                (addBookCommand = new RelayCommand(obj => _model.AddBook()));
            }
        }
        
        public RelayCommand SaveTablesCommand
        {
            get
            {
                return saveTablesCommand ??
                (saveTablesCommand = new RelayCommand(obj => _model.SaveTables()));
            }
        }
        
        public RelayCommand FilterCommand
        {
            get
            {
                return filterCommand ??
                (filterCommand = new RelayCommand(obj =>
                {
                    IGenre[] genres = getSelectedGenres();
                    FilterTable(CategoriesMenu, genres);
                    FilterTable(SimpleBooksMenu, genres);
                    FilterTable(BooksMenu, genres);
                    FilterTable(PriorityBooksMenu, genres);

                }));
            }
        }
        
        public RelayCommand SearchCommand
        {
            get
            {
                return searchCommand ??
                (searchCommand = new RelayCommand(obj =>
                {
                    SearcherTable(CategoriesMenu);
                    SearcherTable(SimpleBooksMenu);
                    SearcherTable(BooksMenu);
                    SearcherTable(PriorityBooksMenu);
                }));
            }
        }
        
        public RelayCommand SortTable =>
        sortTable ?? (sortTable = new RelayCommand(obj =>
        {
            string str = obj as string;
            CategoriesViewCollection.ChangeSortProperty(str);
            SimpleBooksViewCollection.ChangeSortProperty(str);
            BooksViewCollection.ChangeSortProperty(str);
            PriorityViewCollection.ChangeSortProperty(str);
        }));

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
                    if (_model.BooksTable.Contains(book))
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

        private void TableLoad()
        {
            CategoriesMenu.Clear();
            SimpleBooksMenu.Clear();
            BooksMenu.Clear();
            PriorityBooksMenu.Clear();
            GenresTable.Clear();

            foreach (BookGenre genre in _model.BookGenresTable)
            {
                GenresTable.Add(new GenreButtonViewModel(genre));
            }

            foreach (BookCategory category in _model.BookCategoriesTable)
            {
                CategoriesMenu.Add(new BookCategoryViewModel(category, this));
                category.Books.CollectionChanged += CategoryChanged;
            }

            foreach (Book book in _model.BooksTable)
            {
                BooksMenu.Add(new BookViewModel(book, this));
                if (book.FranshiseId == 0)
                {
                    SimpleBooksMenu.Add(new BookViewModel(book, this));
                }
            }

            foreach (PriorityBook book in _model.PriorityBooksTable)
            {
                PriorityBooksMenu.Add(new BookViewModel(book.Book, this));
            }
        }
        
        public BookInfoMenuCondition InfoMenuCondition
        {
            get => infoMenuCondition;
            set
            {
                infoMenuCondition = value;
                if (infoMenuCondition == BookInfoMenuCondition.Closed)
                {
                    InfoMenuDataContext = null;
                    SelectedSources = null;
                }
                    
                OnPropertyChanged();
            }
        }
        
        public object InfoMenuDataContext
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
            SelectedSources = null;
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
            SelectedSources = null;
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
        
        public void OpenSourcesMenu(ObservableCollection<Source> sources)
        {
            SelectedSources = sources;
        }
        
        public RelayCommand RemoveSourceCommand =>
        removeSourceCommand ?? (removeSourceCommand = new RelayCommand(obj =>
        {
            if (SelectedSources != null)
            {
                Source source = obj as Source;
                SelectedSources.Remove(source);
            }
        }));
        
        public RelayCommand AddSourceCommand =>
        addSourceCommand ?? (addSourceCommand = new RelayCommand(obj =>
        {
            if (SelectedSources != null)
            {
                SelectedSources.Add(new Source());
            }
        }));
        
        public RelayCommand MoveUpSourceCommand =>
        moveUpSourceCommand ?? (moveUpSourceCommand = new RelayCommand(obj =>
        {
            if (SelectedSources != null)
            {
                Source source = obj as Source;
                SelectedSources.Move(SelectedSources.IndexOf(source), 0);
            }
        }));
        
        public RelayCommand CloseInfoCommand =>
        closeInfoCommand ?? (closeInfoCommand = new RelayCommand(obj =>
        {
            InfoMenuCondition = BookInfoMenuCondition.Closed;
        }));
    }
}
