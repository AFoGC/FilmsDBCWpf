﻿using System;
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
using WpfApp.Models;
using WpfApp.TableViewModels;
using WpfApp.TableViewModels.Interfaces;

namespace WpfApp.ViewModels
{
    public class BooksViewModel : BaseViewModel, IMenuViewModel<Book>
    {
        private readonly BooksModel _model;

        private BaseViewModel<Book> _selectedElement;

        private Visibility _categoryVisibility = Visibility.Collapsed;
        private Visibility _booksVisibility = Visibility.Collapsed;
        private Visibility _priorityVisibility = Visibility.Collapsed;

        private bool _isReadedChecked = true;
        private bool _isUnReadedChecked = true;
        private string _searchText = string.Empty;

        private RelayCommand showCategoriesCommand;
        private RelayCommand showBooksCommand;
        private RelayCommand showPriorityCommand;
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
        private Object _infoMenuDataContext;

        public ObservableCollection<GenreButtonViewModel> GenresTable { get; private set; }
        public ObservableCollection<BookCategoryViewModel> CategoriesMenu { get; private set; }
        public ObservableCollection<BookViewModel> SimpleBooksMenu { get; private set; }
        public ObservableCollection<BookViewModel> BooksMenu { get; private set; }
        public ObservableCollection<BookViewModel> PriorityBooksMenu { get; private set; }
        public CollectionViewSource SourcesCVS { get; private set; }
        public CollectionViewSource CategoryCVS { get; private set; }
        public CollectionViewSource SimpleBooksCVS { get; private set; }
        public CollectionViewSource BooksCVS { get; private set; }
        public CollectionViewSource PriorityBooksCVS { get; private set; }

        public BooksViewModel(BooksModel model)
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

            CategoryVisibility = Visibility.Visible;
            TableLoad();

            SimpleBooksCVS = new CollectionViewSource();
            CategoryCVS = new CollectionViewSource();
            BooksCVS = new CollectionViewSource();
            PriorityBooksCVS = new CollectionViewSource();

            SimpleBooksCVS.Source = SimpleBooksMenu;
            CategoryCVS.Source = CategoriesMenu;
            BooksCVS.Source = BooksMenu;
            PriorityBooksCVS.Source = PriorityBooksMenu;

            CVSChangeSort(CategoryCVS, "Model.ID", ListSortDirection.Ascending);
            CVSChangeSort(SimpleBooksCVS, "Model.ID", ListSortDirection.Ascending);
            CVSChangeSort(BooksCVS, "Model.ID", ListSortDirection.Ascending);
            CVSChangeSort(PriorityBooksCVS, "Model.ID", ListSortDirection.Ascending);

            SourcesCVS = new CollectionViewSource();
        }
        
        public Visibility CategoryVisibility
        {
            get => _categoryVisibility;
            set
            {
                _categoryVisibility = value;
                OnPropertyChanged();
            }
        }
        
        public Visibility BooksVisibility
        {
            get => _booksVisibility;
            set
            {
                _booksVisibility = value;
                OnPropertyChanged();
            }
        }
        
        public Visibility PriorityVisibility
        {
            get => _priorityVisibility;
            set
            {
                _priorityVisibility = value;
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
        
        public RelayCommand ShowCategoriesCommand
        {
            get
            {
                return showCategoriesCommand ??
                (showCategoriesCommand = new RelayCommand(obj =>
                {
                    CategoryVisibility = Visibility.Visible;
                    BooksVisibility = Visibility.Collapsed;
                    PriorityVisibility = Visibility.Collapsed;
                }));
            }
        }
        
        public RelayCommand ShowBooksCommand
        {
            get
            {
                return showBooksCommand ??
                (showBooksCommand = new RelayCommand(obj =>
                {
                    CategoryVisibility = Visibility.Collapsed;
                    BooksVisibility = Visibility.Visible;
                    PriorityVisibility = Visibility.Collapsed;
                }));
            }
        }
        
        public RelayCommand ShowPriorityCommand
        {
            get
            {
                return showPriorityCommand ??
                (showPriorityCommand = new RelayCommand(obj =>
                {
                    CategoryVisibility = Visibility.Collapsed;
                    BooksVisibility = Visibility.Collapsed;
                    PriorityVisibility = Visibility.Visible;
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
                    SourcesCVS.Source = null;
                }
                    
                OnPropertyChanged();
            }
        }
        
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
        
        public void OpenSourcesMenu(ObservableCollection<Source> sources)
        {
            SourcesCVS.Source = sources;
        }
        
        public RelayCommand RemoveSourceCommand =>
        removeSourceCommand ?? (removeSourceCommand = new RelayCommand(obj =>
        {
            if (SourcesCVS.Source != null)
            {
                Source source = obj as Source;
                ObservableCollection<Source> sources = SourcesCVS.Source as ObservableCollection<Source>;
                sources.Remove(source);
            }
        }));
        
        public RelayCommand AddSourceCommand =>
        addSourceCommand ?? (addSourceCommand = new RelayCommand(obj =>
        {
            if (SourcesCVS.Source != null)
            {
                ObservableCollection<Source> sources = SourcesCVS.Source as ObservableCollection<Source>;
                sources.Add(new Source());
            }
        }));
        
        public RelayCommand MoveUpSourceCommand =>
        moveUpSourceCommand ?? (moveUpSourceCommand = new RelayCommand(obj =>
        {
            if (SourcesCVS.Source != null)
            {
                Source source = obj as Source;
                ObservableCollection<Source> sources = SourcesCVS.Source as ObservableCollection<Source>;
                sources.Move(sources.IndexOf(source), 0);
            }
        }));
        
        public RelayCommand CloseInfoCommand =>
        closeInfoCommand ?? (closeInfoCommand = new RelayCommand(obj =>
        {
            InfoMenuCondition = BookInfoMenuCondition.Closed;
        }));
    }
}
