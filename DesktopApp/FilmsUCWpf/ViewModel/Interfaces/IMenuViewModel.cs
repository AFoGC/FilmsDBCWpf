using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter.TableCell;
using TL_Objects.CellDataClasses;

namespace FilmsUCWpf.ViewModel.Interfaces
{
    public interface IMenuViewModel<T> where T : Cell
    {
        BaseViewModel<T> SelectedElement { get; set; }
        void OpenInfoMenu(Cell model);
        void OpenUpdateMenu(Cell model);
        void OpenSourcesMenu(ObservableCollection<Source> sources);
    }
}
