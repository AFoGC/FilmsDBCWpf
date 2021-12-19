using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL_Objects;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.ControlsInterface;

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.BooksMenu.BooksControls
{
    public interface IBooksControls : IControls
    {
        bool HasSelectedGenre(BookGenre[] selectedGenres);
        bool HasReadedProperty(bool isReaded);
    }
}
