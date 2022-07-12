using InfoMenusWpf.MoreInfo;
using InfoMenusWpf.UpdateInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter.TableCell;

namespace FilmsUCWpf.Presenter.Interfaces
{
    public interface IMenu<T> : IBaseMenu where T : Cell
    {
        BasePresenter<T> SelectedElement { get; set; }
        bool RemovePresenter(BasePresenter<T> presenter);
        bool AddPresenter(BasePresenter<T> presenter);
    }
}
