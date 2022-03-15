using FilmsUCWpf.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL_Objects;

namespace FilmsUCWpf.Presenters
{
    public class SeriePresenter : FilmPresenter
    {
        public Serie SerieModel { get; protected set; }
        public SeriePresenter(Film film, IView<Film> view, IMenu<Film> menu) : base(film, view, menu)
        {
            if (film.Serie != null)
            {
                SerieModel = film.Serie;
            }
            else
            {
                throw new Exception("Не правильно! Нельзя создавать пользовательский элемент серии без серии");
            }
        }
    }
}
