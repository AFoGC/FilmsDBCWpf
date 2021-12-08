using WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.ControlsInterface;
using System;
using System.Collections.Generic;
using System.Text;
using TL_Objects;

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.FilmsMenu.FilmsControls
{
    interface IFilmsControl : IControls
    {
        bool HasSelectedGenre(Genre[] selectedGenres);
        bool HasWatchedProperty(bool isWached);
    }
}
