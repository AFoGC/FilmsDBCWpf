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
        bool RemoveElement(T element);
        bool AddElement(T element);
        /*
        void OpenMoreInfo(Object uiElement);
        void OpenSourcesInfo(ObservableCollection<Source> sources);
        */
    }
}
