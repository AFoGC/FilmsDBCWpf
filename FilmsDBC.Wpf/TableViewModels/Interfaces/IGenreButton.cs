using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL_Objects.Interfaces;

namespace FilmsDBC.Wpf.TableViewModels.Interfaces
{
    public interface IGenreButton
    {
        bool IsChecked { get; set; }
        IGenre Model { get; }
    }
}
