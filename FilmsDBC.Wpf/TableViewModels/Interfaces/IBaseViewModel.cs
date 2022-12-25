using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.TableViewModels.Interfaces;

namespace FilmsDBC.Wpf.TableViewModels.Interfaces
{
    public interface IBaseViewModel : IHasGenre, IFilter, IFinded
    {

    }
}
