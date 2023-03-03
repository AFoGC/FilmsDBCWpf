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
    public class BookCategoryViewModel : BaseViewModel<BookCategory>
    {
        private readonly IMenuViewModel<Book> menu;

        private bool _isCollectionVisible = true;
        private bool _isFiltered = true;

        private RelayCommand openUpdateCommand;
        private RelayCommand createBookCommand;
        private RelayCommand addSelectedCommand;
        private RelayCommand removeSelectedCommand;
        private RelayCommand deleteCategoryCommand;
        private RelayCommand collapseCommand;
        private RelayCommand contextMenuOpenedCommand;
        private RelayCommand contextMenuClosedCommand;

        public BookCategoryViewModel(BookCategory model, IMenuViewModel<Book> menu) : base(model)
        {
            this.menu = menu;
            BooksVMs = new ObservableCollection<BookInCategoryViewModel>();

            model.PropertyChanged += ModelPropertyChanged;
            model.FormatedMark.PropertyChanged += MarkPropertyChanged;
            model.CategoryElements.CollectionChanged += BooksCollectionChanged;

            FillCategoryBooks();
        }

        private void FillCategoryBooks()
        {
            foreach (Book book in Model.CategoryElements)
            {
                BookInCategoryViewModel vm = new BookInCategoryViewModel(book, menu);
                BooksVMs.Add(vm);
            }
        }

        public override bool HasSelectedGenre(IGenre[] selectedGenres)
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

        public override bool Filter(IGenre[] selectedGenres, bool isReadedChecked, bool isUnReadedChecked)
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

        public override bool SetFinded(string search)
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
        
        public RelayCommand CreateBookCommand
        {
            get
            {
                return createBookCommand ??
                (createBookCommand = new RelayCommand(obj =>
                {
                    Book book = new Book();
                    book.Name = GetDefaulBookName();
                    book.BookGenre = TableCollection.GetTable<BookGenre>()[0];
                    TableCollection.GetTable<Book>().AddElement(book);
                    Model.CategoryElements.Add(book);
                }));
            }
        }

        private string GetDefaulBookName()
        {
            if (Model.HideName == string.Empty)
                return Model.Name;
            else
                return Model.HideName;
        }
        
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
                            Model.CategoryElements.Add(book);
                            menu.SelectedElement = null;
                        }
                    }
                }));
            }
        }
        
        public RelayCommand RemoveSelectedCommand
        {
            get
            {
                return removeSelectedCommand ??
                (removeSelectedCommand = new RelayCommand(obj =>
                {
                    Book book = menu.SelectedElement.Model;
                    if (Model.CategoryElements.Remove(book))
                    {
                        menu.SelectedElement = null;
                    }
                }));
            }
        }
        
        public RelayCommand DeleteCategoryCommand
        {
            get
            {
                return deleteCategoryCommand ??
                (deleteCategoryCommand = new RelayCommand(obj =>
                {
                    if (Model.CategoryElements.Count == 0)
                    {
                        BookCategoriesTable categories = (BookCategoriesTable)TableCollection.GetTable<BookCategory>();
                        categories.Remove(Model);
                    }
                }));
            }
        }
        
        public RelayCommand CollapseCommand
        {
            get
            {
                return collapseCommand ??
                (collapseCommand = new RelayCommand(obj =>
                {
                    IsCollectionVisible = !IsCollectionVisible;
                }));
            }
        }
        
        public RelayCommand CMOpenedCommand
        {
            get
            {
                return contextMenuOpenedCommand ??
                (contextMenuOpenedCommand = new RelayCommand(obj =>
                {
                    IsSelected = true;
                }));
            }
        }
        
        public RelayCommand CMClosedCommand
        {
            get
            {
                return contextMenuClosedCommand ??
                (contextMenuClosedCommand = new RelayCommand(obj =>
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
        
        public bool IsCollectionVisible
        {
            get => _isCollectionVisible;
            set
            {
                _isCollectionVisible = value;
                OnPropertyChanged();
            }
        }

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
