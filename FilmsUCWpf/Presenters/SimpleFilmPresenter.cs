using FilmsUCWpf.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TL_Objects;

namespace FilmsUCWpf.Presenters
{
    public class SimpleFilmPresenter : FilmPresenter
    {
        public SimpleFilmPresenter(Film film, IView<Film> view, IMenu<Film> menu) : base(film, view, menu)
        {
            
        }

        public void MoreInfoMenu()
        {
            Control control = new Control();
            if (Model.Serie == null)
            {
                control = new FilmControl(Model, menu);
            }
            else
            {
                control = new SerieControl(Model, menu);
            }

            menu.MoreInfoFormVisualizer.OpenMoreInfoForm(control);
        }
    }
}
