using FilmsUCWpf.PresenterInterfaces;
using FilmsUCWpf.View.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.Table;
using TL_Objects;
using TL_Tables;

namespace FilmsUCWpf.Presenter
{
    public class FilmGenrePresenter
    {
        private readonly Genre model;
        private readonly IGenreView view;
        private readonly TableCollection tableCollection;
        public FilmGenrePresenter(Genre model, IGenreView view, TableCollection collection)
        {
            this.model = model;
            this.view = view;
            tableCollection = collection;
        }

        public void DeleteGenre()
        {
            FilmsTable filmsTable = (FilmsTable)tableCollection.GetTable<Film>();
            if (!filmsTable.GenreHasFilm(model))
            {
                view.RemoveFromview();
                tableCollection.GetTable<Genre>().Remove(model);
            }
            
        }

        public String Name { get => model.Name; set => model.Name = value; }
        public Boolean IsSerialGenre { get => model.IsSerialGenre; set => model.IsSerialGenre = value; }
    }
}
