using FilmsUCWpf.View.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter.TableCell;
using TL_Objects.CellDataClasses;

namespace FilmsUCWpf.Presenter.Interfaces
{
    public interface IMenuPresenter<T> where T : Cell
    {
        IMenuModel<T> Model { get; }
        void OpenMoreInfo(Object uiElement);
        void OpenSourcesInfo(ObservableCollection<Source> sources);
    }
}
