using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL_Objects.Interfaces;

namespace FilmsUCWpf.Presenter.Interfaces
{
    public interface IHasGenre
    {
        bool HasSelectedGenre(IGenre[] selectedGenres);
    }
}
