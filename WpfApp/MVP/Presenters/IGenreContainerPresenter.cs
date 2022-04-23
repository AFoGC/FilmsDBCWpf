using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.MVP.Presenters
{
    public enum GenrePresenterEnum
    {
        BookGenre,
        FilmGenre
    }

    public interface IGenreContainerPresenter
    {
        void RefreshControl();
        void AddGenre();
    }
}
