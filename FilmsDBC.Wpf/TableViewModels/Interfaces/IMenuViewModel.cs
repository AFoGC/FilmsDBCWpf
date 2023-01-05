using NewTablesLibrary;
using System.Collections.ObjectModel;
using TL_Objects.CellDataClasses;

namespace WpfApp.TableViewModels.Interfaces
{
    public interface IMenuViewModel<T> where T : Cell
    {
        BaseViewModel<T> SelectedElement { get; set; }
        void OpenInfoMenu(Cell model);
        void OpenUpdateMenu(Cell model);
        void OpenSourcesMenu(ObservableCollection<Source> sources);
    }
}
