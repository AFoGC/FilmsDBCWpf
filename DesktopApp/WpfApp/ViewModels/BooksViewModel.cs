using FilmsUCWpf.View;
using FilmsUCWpf.ViewModel;
using FilmsUCWpf.ViewModel.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        private void FilterTable(IEnumerable table, IGenre[] genres)
        {
            foreach (IFilter vm in table)
            {
                vm.Filter(genres, IsReadedChecked, IsUnReadedChecked);
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

        public void OpenInfoMenu(Cell model)
        {
            
        }

        public void OpenUpdateMenu(Cell model)
        {
            
        }

        public void OpenSourcesMenu(ObservableCollection<Source> sources)
        {
            
        }
    }
}
