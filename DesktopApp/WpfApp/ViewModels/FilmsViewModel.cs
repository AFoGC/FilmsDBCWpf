using FilmsUCWpf.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter.TableCell;
using TL_Objects;
using TL_Objects.CellDataClasses;

namespace WpfApp.ViewModels
{
    public class FilmsViewModel : BaseViewModel, IMenuViewModel<Book>
    {
        public BaseViewModel<Book> SelectedElement { get; set; }

        public void OpenInfoMenu(Cell model)
        {
            throw new NotImplementedException();
        }

        public void OpenSourcesMenu(ObservableCollection<Source> sources)
        {
            throw new NotImplementedException();
        }

        public void OpenUpdateMenu(Cell model)
        {
            throw new NotImplementedException();
        }
    }
}
