using System;
using System.Collections.Generic;
using System.Text;
using TL_Objects.Interfaces;

namespace MobileApp.PresenterInterfaces
{
    public interface IHasGenre
    {
        bool HasSelectedGenre(IGenre[] selectedGenres);
    }
}
