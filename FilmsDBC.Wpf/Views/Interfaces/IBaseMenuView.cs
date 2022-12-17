using System;
using System.Collections;
using System.Collections.ObjectModel;
using TL_Objects.CellDataClasses;

namespace WpfApp.Views.Interfaces
{
    public interface IBaseMenuView
    {
        IList MenuControls { get; }
        IList GenresControls { get; }
        void OpenMoreInfo(Object uiElement);
        void OpenSourcesInfo(ObservableCollection<Source> sources);
        void CloseAllInfos();
    }
}
