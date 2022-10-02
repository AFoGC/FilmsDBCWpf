using FilmsUCWpf.Command;
using FilmsUCWpf.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL_Objects;
using TL_Objects.CellDataClasses;
using TL_Objects.Interfaces;

namespace FilmsUCWpf.ViewModel
{
    public class BookCategoryViewModel : BaseViewModel<BookCategory>, IHasGenre, IHasCheckedProperty
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
            //BooksVMs.Clear();
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

        public bool HasCheckedProperty(bool isReaded)
        {
            foreach (BookInCategoryViewModel vm in BooksVMs)
            {
                if (vm.HasCheckedProperty(isReaded))
                {
                    return true;
                }
            }

            return false;
        }

        public bool SetFinded(string search)
        {
            bool export = false;
            if (Model.Name.ToLowerInvariant().Contains(search))
            {
                SetVisualFinded();
            }

            foreach (BookInCategoryViewModel vm in BooksVMs)
            {
                vm.SetFinded(search);
            }

            return export;
        }

        private RelayCommand openUpdateCommand;
        public RelayCommand OpenUpdateCommand
        {
            get
            {
                return openUpdateCommand ??
                (openUpdateCommand = new RelayCommand(obj =>
                {
                    throw new NotImplementedException();
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

                    //menu.RemoveElement(book);
                    //Jira FDBC-59

                    throw new NotImplementedException();
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
                    vm = null;
                    foreach (BookInCategoryViewModel item in BooksVMs)
                    {
                        if (item.Model == e.OldItems[0])
                        {
                            vm = item;
                            break;
                        }
                    }
                    BooksVMs.Remove(vm);
                    BooksVMs.Insert(e.NewStartingIndex, vm);
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
