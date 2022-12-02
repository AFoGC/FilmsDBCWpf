using System;
using TL_Objects;
using TL_Tables;
using WpfApp.Services;

namespace WpfApp.Models
{
    public class BooksModel
    {
        private readonly TablesFileService _tablesService;

        public event Action TablesLoaded;

        public BooksModel(TablesFileService tablesService)
        {
            _tablesService = tablesService;
            _tablesService.TablesCollection.TableLoad += OnTableLoad;
        }

        private void OnTableLoad(object sender, EventArgs e)
        {
            TablesLoaded?.Invoke();
        }

        public BooksTable BooksTable => _tablesService.BooksTable;
        public BookCategoriesTable BookCategoriesTable => _tablesService.BookCategoriesTable;
        public BookGenresTable BookGenresTable => _tablesService.BookGenresTable;
        public PriorityBooksTable PriorityBooksTable => _tablesService.PriorityBooksTable;

        public void AddCategory()
        {
            BookCategoriesTable.AddElement(new BookCategory());
        }

        public void AddBook()
        {
            Book book = new Book();
            book.BookGenre = BookGenresTable[0];
            BooksTable.AddElement(book);
        }

        public void SaveTables() => _tablesService.TablesCollection.SaveTables();
    }
}
