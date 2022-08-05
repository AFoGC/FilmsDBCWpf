﻿using FilmsUCWpf.Presenter;
using FilmsUCWpf.Presenter.Interfaces;
using FilmsUCWpf.View;
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
using WpfApp.Presenters;

namespace WpfApp.Models
{
    public class BooksMenuModel : IMenuModel<Book>
    {
        public enum MenuCondition
        {
            Category = 1,
            Book = 2,
            PriorityBook = 3
        }

        public IMenuPresenter<Book> Presenter { get; set; }
        public MenuCondition ControlsCondition { get; set; }
        public TLTables Tables => mainModel.Tables;
        public TableCollection TableCollection => mainModel.TableCollection;
        BasePresenter<Book> IMenuModel<Book>.SelectedElement { get => SelectedElement; set => SelectedElement = (BookPresenter)value; }

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
        public ObservableCollection<GenrePressButton> GenreButtons { get; private set; }

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
            GenreButtons = new ObservableCollection<GenrePressButton>();

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
                    GenreButtons.Add(new GenrePressButton(genre));
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
                GenreButtons.Add(new GenrePressButton(genre));
            }

            foreach (BookCategory category in Tables.BookCategoriesTable)
            {
                CategoryPresenters.Add(new BookCategoryPresenter(category, new BookCategoryControl(), Presenter, TableCollection));
            }

            foreach (Book book in Tables.BooksTable)
            {
                BookPresenters.Add(new BookPresenter(book, new BookControl(), Presenter, TableCollection));
                if (book.FranshiseId == 0)
                {
                    CategoryPresenters.Add(new BookPresenter(book, new BookSimpleControl(), Presenter, TableCollection));
                }
            }

            foreach (PriorityBook priorityBook in Tables.PriorityBooksTable)
            {
                PriorityPresenters.Add(new BookPriorityPresenter(priorityBook, new BookPriorityControl(), Presenter, TableCollection));
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
                    PriorityPresenters.Add(new BookPriorityPresenter(priorityBook, new BookPriorityControl(), Presenter, TableCollection));
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
                    CategoryPresenters.Insert(Tables.BookCategoriesTable.Count - 1, new BookCategoryPresenter(category, new BookCategoryControl(), Presenter, TableCollection));
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
                    BookPresenters.Add(new BookPresenter(book, new BookControl(), Presenter, TableCollection));
                    CategoryPresenters.Add(new BookPresenter(book, new BookSimpleControl(), Presenter, TableCollection));
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

        public IEnumerable<BookCategoryPresenter> GetBookCategoryPresenters()
        {
            List<BookCategoryPresenter> export = new List<BookCategoryPresenter>();
            foreach (var item in CategoryPresenters)
            {
                if (item.GetType() == typeof(BookCategoryPresenter))
                {
                    export.Add((BookCategoryPresenter)item);
                }
            }
            return export;
        }

        public IEnumerable<BookPresenter> GetBookSimplePresenters()
        {
            List<BookPresenter> export = new List<BookPresenter>();
            foreach (var item in CategoryPresenters)
            {
                if (item.GetType() == typeof(BookPresenter))
                {
                    export.Add((BookPresenter)item);
                }
            }
            return export;
        }

        public bool AddElement(Book book)
        {
            int i = 0;
            Type type = typeof(BookPresenter);
            foreach (IBasePresenter item in CategoryPresenters)
            {
                if (item.GetType() == type)
                {
                    Book bookInPresenter = (Book)item.ModelCell;
                    if (bookInPresenter.ID > book.ID) break;
                }
                i++;
            }
            BookPresenter bookPresenter = new BookPresenter(book, new BookSimpleControl(), Presenter, TableCollection);
            CategoryPresenters.Insert(i, bookPresenter);
            return true;
        }

        public bool RemoveElement(Book book)
        {
            Type type = typeof(BookPresenter);
            BookPresenter presenterInCategory;
            foreach (IBasePresenter presenter in CategoryPresenters)
            {
                if (presenter.GetType() == type)
                {
                    presenterInCategory = (BookPresenter)presenter;
                    if (presenterInCategory.Model == book)
                    {
                        CategoryPresenters.Remove(presenterInCategory);
                        break;
                    }
                }
            }
            return false;
        }
    }
}
