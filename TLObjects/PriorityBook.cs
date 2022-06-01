using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.TableCell;

namespace TL_Objects
{
    [TableCell("PriorityBook")]
    public class PriorityBook : Cell
    {
        private Book book;
        private int bookId = 0;

        public PriorityBook() : base() { }

        protected override void loadBody(Comand comand)
        {
            switch (comand.Paramert)
            {
                case "book":
                    bookId = Convert.ToInt32(comand.Value);
                    break;

                default:
                    break;
            }
        }

        protected override void saveBody(StreamWriter streamWriter, Cell defaultCell)
        {
            streamWriter.Write(FormatParam("book", bookId, 0, 2));
        }

        protected override void updateThisBody(Cell cell)
        {
            PriorityBook priorityBook = (PriorityBook)cell;

            Book = priorityBook.Book;
        }

        public Book Book
        {
            get { return book; }
            set
            {
                if (book != null)
                    book.CellRemoved -= Book_CellRemoved;

                book = value;
                bookId = book.ID;

                book.CellRemoved += Book_CellRemoved;

                OnPropertyChanged(nameof(Book));
            }
        }

        private void Book_CellRemoved(object sender, EventArgs e)
        {
            TablesLibrary.Interpreter.Table.Table<PriorityBook> table = (TablesLibrary.Interpreter.Table.Table<PriorityBook>)ParentTable;
            table.Remove(this);
        }

        public int BookId
        {
            get { return bookId; }
        }
    }
}
