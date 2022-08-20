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
        private Book _book;
        private int _bookId;

        public PriorityBook()
        {
            _bookId = 0;
        }

        protected override void loadBody(Comand comand)
        {
            switch (comand.Paramert)
            {
                case "book":
                    _bookId = Convert.ToInt32(comand.Value);
                    break;

                default:
                    break;
            }
        }

        protected override void saveBody(StreamWriter streamWriter, Cell defaultCell)
        {
            streamWriter.Write(FormatParam("book", _bookId, 0, 2));
        }

        protected override void updateThisBody(Cell cell)
        {
            PriorityBook priorityBook = (PriorityBook)cell;

            Book = priorityBook.Book;
        }

        public Book Book
        {
            get { return _book; }
            set
            {
                if (_book != null)
                    _book.CellRemoved -= Book_CellRemoved;

                _book = value;
                _bookId = _book.ID;

                _book.CellRemoved += Book_CellRemoved;

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
            get { return _bookId; }
        }
    }
}
