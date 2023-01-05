using System;
using TL_Objects;
using TL_Tables;
using WpfApp.Services;

namespace WpfApp.Models
{
    public class BooksModel
    {
        private readonly TablesService _tablesService;

        public event Action TablesLoaded;

        public BooksModel(TablesService tablesService)
        {
            _tablesService = tablesService;
            _tablesService.TablesCollection.TablesLoaded += OnTableLoad;
        }

        private void OnTableLoad()
        {
            TablesLoaded?.Invoke();
        }

        public BooksTable BooksTable => _tablesService.BooksTable;
        public BookCategoriesTable BookCategoriesTable => _tablesService.BookCategoriesTable;
        public BookGenresTable BookGenresTable => _tablesService.BookGenresTable;
        public PriorityBooksTable PriorityBooksTable => _tablesService.PriorityBooksTable;

        public void AddCategory()
        {
            BookCategoriesTable.Add(new BookCategory());
        }

        public void AddBook()
        {
            Book book = new Book();
            book.BookGenre = BookGenresTable[0];
            BooksTable.Add(book);
        }

        public void SaveTables() { }//=> _tablesService.TablesCollection.SaveTables();
    }
}
