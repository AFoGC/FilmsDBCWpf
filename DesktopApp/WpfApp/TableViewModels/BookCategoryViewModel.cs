using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using TL_Objects;
using TL_Objects.Interfaces;
using TL_Tables;
using WpfApp.Commands;
using WpfApp.TableViewModels.Interfaces;

namespace WpfApp.TableViewModels
{
    public class BookCategoryViewModel : BaseViewModel<BookCategory>, IHasGenre, IFilter, IFinded
    {
        private readonly IMenuViewModel<Book> menu;
        public BookCategoryViewModel(BookCategory model, IMenuViewModel<Book> menu) : base(model)
        {
            this.menu = menu;
            BooksVMs = new ObservableCollection<BookInCategoryViewModel>();

            model.PropertyChanged += ModelPropertyChanged;
            model.FormatedMark.PropertyChanged += MarkPropertyChanged;
            model.Books.CollectionChanged += BooksCollectionChanged;

            fillCategoryBooks();
        }

        private void fillCategoryBooks()
        {
            foreach (Book book in Model.Books)
            {
                BookInCategoryViewModel vm = new BookInCategoryViewModel(book, menu);
                BooksVMs.Add(vm);
            }
        }

        public bool HasSelectedGenre(IGenre[] selectedGenres)
        {
            foreach (BookInCategoryViewModel vm in BooksVMs)
            {
                if (vm.HasSelectedGenre(selectedGenres))
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
                foreach (var vm in BooksVMs)
                {
                    if (vm.Filter(selectedGenres, isReadedChecked, isUnReadedChecked))
                    {
                        passedFilter = true;
                    }
                }
            }

            IsFiltered = passedFilter;
            return passedFilter;
        }

        public bool SetFinded(string search)
        {
            IsFinded = Model.Name.ToLower().Contains(search);

            foreach (BookInCategoryViewModel vm in BooksVMs)
            {
                if (vm.SetFinded(search))
                {
                    IsFinded = true;
                }
            }

            return IsFinded;
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

        private RelayCommand createBookCommand;
        public RelayCommand CreateBookCommand
        {
            get
            {
                return createBookCommand ??
                (createBookCommand = new RelayCommand(obj =>
                {
                    Book book = new Book();
                    book.Name = getDefaulBookName();
                    book.BookGenre = TableCollection.GetTable<BookGenre>()[0];
                    TableCollection.GetTable<Book>().AddElement(book);
                    Model.Books.Add(book);
                }));
            }
        }

        private string getDefaulBookName()
        {
            if (Model.HideName == string.Empty)
                return Model.Name;
            else
                return Model.HideName;
        }

        private RelayCommand addSelectedCommand;
        public RelayCommand AddSelectedCommand
        {
            get
            {
                return addSelectedCommand ??
                (addSelectedCommand = new RelayCommand(obj =>
                {
                    if (menu.SelectedElement != null)
                    {
                        Book book = menu.SelectedElement.Model;
                        if (book.FranshiseId == 0)
                        {
                            Model.Books.Add(book);
                            menu.SelectedElement = null;
                        }
                    }
                }));
            }
        }

        private RelayCommand removeSelectedCommand;
        public RelayCommand RemoveSelectedCommand
        {
            get
            {
                return removeSelectedCommand ??
                (removeSelectedCommand = new RelayCommand(obj =>
                {
                    Book book = menu.SelectedElement.Model;
                    if (Model.Books.Remove(book))
                    {
                        menu.SelectedElement = null;
                    }
                }));
            }
        }

        private RelayCommand deleteCategoryCommand;
        public RelayCommand DeleteCategoryCommand
        {
            get
            {
                return deleteCategoryCommand ??
                (deleteCategoryCommand = new RelayCommand(obj =>
                {
                    if (Model.Books.Count == 0)
                    {
                        BookCategoriesTable categories = (BookCategoriesTable)TableCollection.GetTable<BookCategory>();
                        categories.Remove(Model);
                    }
                }));
            }
        }

        private RelayCommand collapseCommand;
        public RelayCommand CollapseCommand
        {
            get
            {
                return collapseCommand ??
                (collapseCommand = new RelayCommand(obj =>
                {
                    if (CollectionVisiblility == Visibility.Visible) 
                        CollectionVisiblility = Visibility.Collapsed;
                    else 
                        CollectionVisiblility = Visibility.Visible;
                }));
            }
        }

        private RelayCommand _CMOpenedCommand;
        public RelayCommand CMOpenedCommand
        {
            get
            {
                return _CMOpenedCommand ??
                (_CMOpenedCommand = new RelayCommand(obj =>
                {
                    IsSelected = true;
                }));
            }
        }

        private RelayCommand _CMClosedCommand;
        public RelayCommand CMClosedCommand
        {
            get
            {
                return _CMClosedCommand ??
                (_CMClosedCommand = new RelayCommand(obj =>
                {
                    IsSelected = false;
                }));
            }
        }

        private void BooksCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Book book;
            BookInCategoryViewModel vm;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    book = (Book)e.NewItems[0];
                    vm = new BookInCategoryViewModel(book, menu);
                    BooksVMs.Add(vm);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    book = (Book)e.OldItems[0];
                    vm = BooksVMs.Where(x => x.Model == book).FirstOrDefault();
                    BooksVMs.Remove(vm);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    BooksVMs.Clear();
                    break;
                case NotifyCollectionChangedAction.Move:
                    BooksVMs.Move(e.OldStartingIndex, e.NewStartingIndex);
                    break;
                default:
                    break;
            }
        }

        private void MarkPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Mark));
            if (e.PropertyName == nameof(Model.FormatedMark.MarkSystem))
            {
                OnPropertyChanged(nameof(Marks));
            }
        }

        private void ModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e);
        }

        private Visibility _collectionVisibility = Visibility.Visible;
        public Visibility CollectionVisiblility
        {
            get => _collectionVisibility;
            set { _collectionVisibility = value; OnPropertyChanged(); }
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
        public String Name
        {
            get => Model.Name;
            set => Model.Name = value;
        }
        public String HideName
        {
            get => Model.HideName;
            set => Model.HideName = value;
        }
        public String Mark
        {
            get => Model.FormatedMark.ToString();
            set => Model.FormatedMark.SetMarkFromString(value);
        }
        public List<String> Marks => Model.FormatedMark.GetComboItems();

        public ObservableCollection<BookInCategoryViewModel> BooksVMs { get; private set; }
    }
}
