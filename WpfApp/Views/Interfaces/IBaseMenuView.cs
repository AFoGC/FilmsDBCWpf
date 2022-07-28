using FilmsUCWpf.View.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TablesLibrary.Interpreter.TableCell;
using TL_Objects.CellDataClasses;

namespace WpfApp.Views.Interfaces
{
    public interface IBaseMenuView
    {
        IList MenuControls { get; }
        IList GenresControls { get; }
        void OpenMoreInfo(IView uiElement);
        void OpenUpdateInfo(IUpdateControl uiElement);
        void UpdateInUpdateInfo();
        void OpenSourcesInfo(ObservableCollection<Source> sources);
        void CloseAllInfos();
    }
}
