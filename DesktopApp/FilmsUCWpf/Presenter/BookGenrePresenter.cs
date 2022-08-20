using FilmsUCWpf.View.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter;
using TL_Objects;
using TL_Tables;

namespace FilmsUCWpf.Presenter
{
    public class BookGenrePresenter
    {
        private readonly BookGenre model;
        private readonly IGenreView view;
        private readonly TableCollection tableCollection;
        public BookGenrePresenter(BookGenre model, IGenreView view, TableCollection collection)
        {
            this.model = model;
            this.view = view;
            tableCollection = collection;
        }

        public void DeleteGenre()
        {
            BooksTable filmsTable = (BooksTable)tableCollection.GetTable<Book>();
            if (!filmsTable.GenreHasBook(model))
            {
                view.RemoveFromview();
                tableCollection.GetTable<BookGenre>().Remove(model);
            }

        }

        public String Name { get => model.Name; set => model.Name = value; }
    }
}
