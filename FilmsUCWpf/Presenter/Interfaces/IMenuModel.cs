using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter.TableCell;

namespace FilmsUCWpf.Presenter.Interfaces
{
    public interface IMenuModel<T> where T : Cell
    {
        BasePresenter<T> SelectedElement { get; set; }
        bool RemoveElement(T element);
        bool AddElement(T element);
    }
}
